(function () {
    'use strict';

    angular.module('TravelBagApp.Controllers')
    .controller("excursionCtrl", excursionsCtrl);

    excursionsCtrl.$inject = [
            "$scope",
            "$q",
            '$http',
            '$timeout',
            "TourService",
            "$mdDialog"
    ];

    function excursionsCtrl($scope, $q, $http, $timeout, TourService, $mdDialog) {

        $scope.tour = null;
        $scope.isReady = false;

        //#######      Method

        $scope.showNextImage = showNextImage;

        //########      End Method

        function showNextImage() {
            if (!$scope.tour.Photo) return;

            var number = $scope.tour.Photo.length;

            for (var i = 0; i < number; i++) {
                if ($scope.tour.Photo[i].Delete) {
                    $scope.tour.Photo[i].Delete = false;

                    if ((i + 1) < number) {
                        $scope.tour.Photo[i + 1].Delete = true;
                        return;
                    }
                    else {
                        $scope.tour.Photo[0].Delete = true;
                        return;
                    }
                }
                //var tmpPhoto = $scope.tour.Photo[i];
            }
        }

        function getTour() {
            $scope.promise = $timeout(function () {
                var promises = [];
                promises.push(TourService.getTourByID(tourID));

                $q.all(promises).then(function (results) {
                    $scope.tour = JSON.parse(results[0].data);
                    $scope.isReady = true;
                });
            }, 1000);
        }


        activate();

        function activate() {
            return getTour();
        }
    }
})();