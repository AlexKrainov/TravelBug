(function () {
    'use strict';

    angular.module('TravelBagApp.Controllers')
    .controller("excursionsCtrl", excursionsCtrl);

    excursionsCtrl.$inject = [
           "$scope",
            "$q",
          '$http',
          '$timeout',
          "TourService",
           "$mdDialog"
    ];

    function excursionsCtrl($scope, $q, $http, $timeout, TourService, $mdDialog) {

        $scope.tours = [];
        $scope.isReady = false;
        $scope.urlPhoto = "http://localhost:1520/Show/MainImage";
        //#######      Method
        $scope.getMainImage = getMainImage;
        $scope.clickLink = clickLink;

        //########      End Method

        function clickLink(a) {
            console.log(a);
            $("#link_" + a).click();

        }

        function getTours() {
            $scope.promise = $timeout(function () {
                var promises = [];
                promises.push(TourService.getTours());

                $q.all(promises).then(function (results) {
                    $scope.tours = results[0].data;
                    $scope.isReady = true;
                });
            }, 1000);
        }

        function getMainImage(tourID, obj) {
            var photo = TourService.getPhotoByTourID(tourID);
            obj["src"] = photo;
        }

        activate();

        function activate() {
            var promises = [];
            promises.push(TourService.getTours());

            $q.all(promises).then(function (results) {
                $scope.tours = JSON.parse(results[0].data);
                $scope.isReady = true;
            });
            // getTours();
        }
    }
})();