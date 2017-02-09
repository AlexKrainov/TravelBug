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

        //###############    Methods
        $scope.SaveAs = SaveAs;
        $scope.closeDialog = closeDialog;
        $scope.checkClick = checkClick;
        $scope.getTheFiles = getTheFiles;
        $scope.loadFile = loadFile;

        //#################    Methods

        function getTheFiles($files) {
            var formdata = new FormData();
            angular.forEach($files, function (value, key) {
                formdata.append(key, value);
            });
        }

        function checkClick(language, selected) {
            if ($scope.isReady) {
                language.value = !language.value;
            }
        }

        function SaveAs(draft) {
            if ($scope.myForm.invitation.$valid) {

                $scope.isReady = false;

                var fileUpload = $("#Pictures").get(0);
                var files = fileUpload.files;

                if (files.length == 0) {
                    saveWithoutImages(draft);
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
            AdminTableService.onUpdate(cardToJSON(), languagesToJSON())
                .then(function () {
                    if (!draft) {
                        closeDialog();
                    } else {
                        $scope.isReady = true;
                    }
                });
        }

        function loadFile() {

            return $http.get('/Admin/getFiles/' + $scope.Card.Id)
                     .success(function (data, status, headers, config) {
                         var data = JSON.parse(data);
                         for (var i = 0; i < data.length; i++) {
                             $scope.images.push({ image: data[i], hide: data[i].Delete, _delete: false });
                         }
                     });

        }

        //Format json [ key: "value" ]
        function cardToJSON() {
            var sendItem = {
                "Id": $scope.Card.Id,
                "Title": $scope.Card.Title,
                "Description": $scope.Card.Description,
                "TimeID": $scope.Card.TimeID
            };
            return JSON.stringify(sendItem);
        }

        //Format "English Russian "
        function languagesToJSON() {
            var langValue = "";
            for (var i = 0; i < $scope.languages.length; i++) {
                if ($scope.languages[i].value) langValue = langValue + " " + $scope.languages[i].name;
            }

            return langValue;
        }

        function closeDialog() {
            $mdDialog.cancel();
        }

        function activate() {
            var promises = [];

            if (ID == 0) {
                promises.push(AdminTableService.onCreate());
            } else {
                promises.push(AdminTableService.onEdit(ID));
            }
            promises.push(AdminTableService.getTimes());        //1
            //promises.push(AdminTableService.);               //2
            //promises.push(AdminTableService.getLanguage);

            $q.all(promises).then(function (results) {
                $scope.times = JSON.parse(results[1].data);

                $scope.Card = JSON.parse(results[0].data);

                var lang = $scope.Card.Language;
                for (var i = 0; i < lang.length; i++) {
                    for (var j = 0; j < $scope.languages.length; j++) {
                        if ($scope.languages[j].name == lang[i].Name_Language) {
                            $scope.languages[j].value = true;
                        }
                    }
                }

                initFileUpload();

                $scope.isReady = true;
            });
        }


        activate();

        function initFileUpload() {
            var oldPlace = $("#AddImage");
            var newPlace = $("#NewPlaceImage");
            newPlace.append(oldPlace);

        }
    }
})();