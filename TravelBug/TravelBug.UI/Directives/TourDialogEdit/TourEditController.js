(function () {
    'use strict';

    angular.module('TravelBagApp.Common')
    .controller("DialogController", DialogController);

    DialogController.$inject = [
           "$scope",
            "$q",
            "$mdDialog",
            "ID",
            "AdminTableService",
            "$http"
    ];

    function DialogController($scope, $q, $mdDialog, ID, AdminTableService, $http) {

        $scope.myForm = {};
        $scope.Card = null;
        $scope.languages = [
            { name: "Russian", value: false },
            { name: "English", value: false },
            { name: "France", value: false }
        ];
        $scope.images = [];
        $scope.costs = [];
        $scope.isReady = false;

        //###############    Methods
        $scope.SaveAs = SaveAs;
        $scope.closeDialog = closeDialog;
        $scope.checkClick = checkClick;
        $scope.checkClickImage = checkClickImage;
        $scope.checkCosts = checkCosts;
        $scope.removeTour = removeTour;

        //#################    Methods

        //function getTheFiles($files) {
        //    var formdata = new FormData();
        //    angular.forEach($files, function (value, key) {
        //        formdata.append(key, value);
        //    });
        //}

        function checkClick(language, selected) {
            if ($scope.isReady) {
                language.value = !language.value;
            }
        }
        function checkClickImage(image, type) {
            if ($scope.isReady) {
                if (type == "hide") {
                    image.hide = !image.hide
                } else if (type == "_delete") {
                    image._delete = !image._delete
                }
            }
        }
        function checkCosts(cost) {
            if ($scope.isReady && cost) {
                $scope.Card.Cost = cost.Money;
            }
        }

        function SaveAs(draft) {
            if ($scope.myForm.invitation.$valid) {

                $scope.isReady = false;

                var fileUpload = $("#Pictures").get(0);
                var files = fileUpload.files;

                if (files.length == 0) {
                    saveWithoutImages(draft);
                    return;
                }

                // Checking whether FormData is available in browser
                if (window.FormData !== undefined) {

                    // Create FormData object
                    var fileData = new FormData();

                    // Looping over all files and add it to FormData object
                    for (var i = 0; i < files.length; i++) {
                        fileData.append(files[i].name, files[i]);
                    }

                    // Adding one more key to FormData object
                    fileData.append('username', 'Manas');//??

                    $.ajax({
                        url: '/Admin/UploadFiles/' + $scope.Card.Id,
                        type: "POST",
                        contentType: false, // Not to set any content header
                        processData: false, // Not to process data
                        data: fileData,
                        success: function (result) {
                            saveWithoutImages(draft)
                        },
                        error: function (err) {
                            alert(err.statusText);
                        }
                    });
                } else {
                    if (confirm("Не удается сохранить файлы. Продолжить сохранение без них ?"))
                        saveWithoutImages(draft);
                }
            }
        }

        function saveWithoutImages(draft) {
            AdminTableService.onUpdate(cardToJSON())
                .then(function (res) {

                    if (ID == 0) {

                        if (!draft) {
                            closeDialog();
                        } else {
                            $scope.isReady = true;
                        }

                    } else {

                        AdminTableService.OnRemoveImages(ID, removeImagesToJSON())
                    .then(function (result) {
                        if (!draft) {
                            closeDialog();
                        } else {
                            $scope.isReady = true;
                        }

                    });
                    }

                });
        }

        //Format json [ key: "value" ]
        function cardToJSON() {
            var sendItem = {
                "Id": $scope.Card.Id,
                "Title": $scope.Card.Title,
                "Description": $scope.Card.Description,
                "TimeID": $scope.Card.TimeID,
                "Cost": [{
                    "ID": 0,
                    "Money": $scope.Card.Cost
                }],
                "Language": languagesToJSON()
            };
            return JSON.stringify(sendItem);
        }

        //Format "English Russian "
        function languagesToJSON() {
            var langValue = [];
            for (var i = 0; i < $scope.languages.length; i++) {
                if ($scope.languages[i].value) {
                    langValue.push({ "ID": 0, "Name_Language": $scope.languages[i].name });
                }
            }
            return langValue;
        }

        function removeImagesToJSON() {
            var removeImages = [];

            for (var i = 0; i < $scope.images.length; i++) {
                removeImages.push({
                    "ID": $scope.images[i].image.ID,
                    "Hide": $scope.images[i].hide,
                    "Delete": $scope.images[i]._delete,
                    "ExcursionID": ID
                });
            }
            return JSON.stringify(removeImages);
        }

        function removeTour() {
            $scope.isReady = false;
            if ($scope.Card.Id && confirm("Are your sure, you wanna remove tour ?")) {
                AdminTableService.onRemoveTour($scope.Card.Id)
                .then(function (result) {
                    if (result && result.data) {
                        closeDialog();
                    }
                });
            }
            $scope.isReady = true;
        }

        function loadFile() {
            return AdminTableService.loadFile(ID)
            .then(function (result) {
                var data = JSON.parse(result.data);
                for (var i = 0; i < data.length; i++) {
                    $scope.images.push({ image: data[i], hide: data[i].Delete, _delete: false });
                }
                return $scope.images;
            });
        }

        function activate() {
            var promises = [];

            if (ID == 0) {
                promises.push(AdminTableService.onCreate());
            } else {
                promises.push(AdminTableService.onEdit(ID));
            }
            promises.push(AdminTableService.getTimes());        //1
            promises.push(AdminTableService.getMoney());

            $q.all(promises).then(function (results) {
                $scope.costs = JSON.parse(results[2].data);
                $scope.times = JSON.parse(results[1].data);
                $scope.Card = isJsonString(results[0].data) ? JSON.parse(results[0].data) : results[0].data;


                if ($scope.Card.Language) {
                    var lang = $scope.Card.Language;
                    for (var i = 0; i < lang.length; i++) {
                        for (var j = 0; j < $scope.languages.length; j++) {
                            if ($scope.languages[j].name == lang[i].Name_Language) {
                                $scope.languages[j].value = true;
                            }
                        }
                    }
                }

                if ($scope.Card.Cost && $scope.Card.Cost.length == 1) {
                    $scope.Card.Cost = $scope.Card.Cost[0].Money;
                }

                loadFile()
                .then(function (result) {
                    $scope.isReady = true;
                });

            });
        }





        function isJsonString(str) {
            try {
                JSON.parse(str);
            } catch (e) {
                return false;
            }
            return true;
        }

        function closeDialog() {
            $mdDialog.cancel();
        }

        activate();

    }
})();