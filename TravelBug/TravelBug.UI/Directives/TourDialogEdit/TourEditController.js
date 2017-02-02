(function () {
    'use strict';

    angular.module('TravelBagApp.Common')
    .controller("DialogController", DialogController);

    DialogController.$inject = [
           "$scope",
            "$q",
            "$mdDialog",
            "ID",
            "AdminTableService"
    ];

    function DialogController($scope, $q, $mdDialog, ID, AdminTableService) {

        $scope.myForm = {};
        $scope.Card = null;
        $scope.lenguages = [
            { name: "Russian", value: false },
            { name: "English", value: false },
            { name: "France", value: false }
        ];

        //###############    Methods
        $scope.Update = Update;
        $scope.closeDialog = closeDialog;
        $scope.checkClick = checkClick;

        //#################    Methods

        function checkClick(language, selected) {
            if ($scope.isReady) {
                language.value = !language.value;

            }
        }

        function Update() {
            if ($scope.myForm.invitation.$valid) {
                $scope.isReady = false;
                var promises = [];

                promises.push(AdminTableService.onUpdate(cardToJSON()));

                $q.all(promises).then(function (results) {
                    //$scope.isReady = true;
                    closeDialog();
                });
            }

        }

        function cardToJSON() {
            var sendItem = {
                "Id": $scope.Card.Id,
                "Title": $scope.Card.Title,
                "Description": $scope.Card.Description,
                "TimeID": $scope.Card.TimeID
            };
            return JSON.stringify(sendItem);
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
            //promises.push(AdminTableService.getMoney);
            //promises.push(AdminTableService.getLanguage);

            $q.all(promises).then(function (results) {
                $scope.times = JSON.parse(results[1].data);

                $scope.Card = JSON.parse(results[0].data);

                var lang = $scope.Card.Language;
                for (var i = 0; i < lang.length; i++) {
                    for (var j = 0; j < $scope.lenguages.length; j++) {
                        if ($scope.lenguages[j].name == lang[i].Name_Language) {
                            $scope.lenguages[j].value = true;
                        }
                    }
                }

                $scope.isReady = true;
            });
        }


        activate();

    }
})();