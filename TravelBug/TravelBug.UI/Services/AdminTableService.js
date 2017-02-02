(function () {
    'use strict';

    angular
        .module("TravelBagApp.Services")
        .factory("AdminTableService", AdminTableService);

    AdminTableService.$inject = ["$http", "$q"];

    function AdminTableService($http, $q) {
        var service = {
            getDataForTable: getDataForTable,
            onDelete: onDelete,
            onCreate: onCreate,
            onUpdate: onUpdate,
            onEdit: onEdit,
            getTimes: getTimes
        };

        return service;

        function getDataForTable() {
            return $http.get('/Admin/GetDataForTable')
           .success(function (data, status, headers, config) {
               return data;
           });
        }

        function onDelete(id) {
            if (id && id > 0) {
                //ToDo: Переделать на http delete
                return $http.get('/Admin/OnDelete/' + id)
            .success(function (data, status, headers, config) {
                return data;
            });
            }
        }

        function onCreate() {
            return $http.get('/Admin/OnCreate')
                     .success(function (data, status, headers, config) {
                         return data;
                     });
        }

        function onUpdate(jsonValue) {
            return $http.post('/Admin/OnUpdate', jsonValue)
                    .success(function (data, status, headers, config) {
                        return data;
                    });
        }

        function onEdit(id) {
            return $http.get('/Admin/OnEdit/' + id)
                    .success(function (data, status, headers, config) {
                        return data;
                    });
        }

        function getTimes() {
            return $http.get('/Admin/GetTimes')
         .success(function (data, status, headers, config) {
             return data;
         });
        }
    }
})();