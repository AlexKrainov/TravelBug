(function () {
    'use strict';

    angular
        .module("TravelBagApp.Services")
        .factory("AdminTableService", AdminTableService);

    AdminTableService.$inject = ["$http", "$q"];

    function AdminTableService($http, $q) {
        var service = {
            getDataForTable: getDataForTable

        };

        return service;

        //ToDo: Don't work
        function getDataForTable() {
            var deferred = $q.defer();
            $http({
                method: 'GET',
                url: '/Admin/GetDataForTable'
            }).
             success(function (data, status, headers, config) {
                 deferred.resolve(data)
             }).
             error(function (data, status) {
                 deferred.reject(data);
             });

            return deferred;

           // $http.get('/Admin/GetDataForTable')
           //.success(function (data, status, headers, config) {
           //    return data;
           //});

        }

    }
})();