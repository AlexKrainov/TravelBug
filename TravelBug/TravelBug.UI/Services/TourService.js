(function () {
    'use strict';

    angular
        .module("TravelBagApp.Services")
        .factory("TourService", TourService);

    TourService.$inject = ["$http", "$q"];

    function TourService($http, $q) {
        var service = {
            getTours: getTours,
            getPhotoByTourID: getPhotoByTourID,
            getTourByID: getTourByID
        };

        return service;


        function getTours() {
            return $http.get('/Home/GetTours')
           .success(function (data, status, headers, config) {
               return data;
           });
        }

        function getPhotoByTourID(id) {
            return $http.get('/Home/GetPhotoByTourID')
          .success(function (data, status, headers, config) {
              return data;
          });
        }

        function getTourByID(id) {
            return $http.get('/Excursion/GetTourByID/' + id)
          .success(function (data, status, headers, config) {
              return data;
          });
        }
    }
})();