(function () {
    'use strict';

    angular.module('TravelBagApp.Controllers')
    .controller("AdminCtrl", AdminCtrl);

    AdminCtrl.$inject = [
           "$scope",
            "$q",
          '$http',
          '$mdEditDialog',
          '$timeout',
          "AdminTableService"
    ];

    function AdminCtrl($scope, $q, $http, $mdEditDialog, $timeout, AdminTableService) {

        $scope.editComment = editComment;
        $scope.toggleLimitOptions = toggleLimitOptions;
        $scope.onPaginate = onPaginate;
        $scope.getTypes = getTypes;
        $scope.deselect = deselect;
        $scope.loadStuff = loadStuff;

        //#########   Table Field
        $scope.options = {
            rowSelection: true,
            multiSelect: true,
            autoSelect: true,
            decapitate: false,
            largeEditDialog: false,
            boundaryLinks: false,
            limitSelect: true,
            pageSelect: true
        };

        $scope.selected = [];
        $scope.limitOptions = [5, 10, 15, {
            label: 'All',
            value: function () {
                return $scope.desserts ? $scope.desserts.count : 0;
            }
        }];

        $scope.columns = [{
            name: 'ID',
            orderBy: 'id',
            unit: '100g serving'
        }, {
            descendFirst: true,
            name: 'Title',
            orderBy: 'Title'
        }, {
            name: 'Description',
            orderBy: 'Description'
        }];
        //#########   Table Field





        //###################      Table Method
        function editComment() {
            event.stopPropagation();

            var dialog = {
                // messages: {
                //   test: 'I don\'t like tests!'
                // },
                modelValue: dessert.comment,
                placeholder: 'Add a comment',
                save: function (input) {
                    dessert.comment = input.$modelValue;
                },
                targetEvent: event,
                title: 'Add a comment',
                validators: {
                    'md-maxlength': 30
                }
            };

            var promise = $scope.options.largeEditDialog ? $mdEditDialog.large(dialog) : $mdEditDialog.small(dialog);

            promise.then(function (ctrl) {
                var input = ctrl.getInput();

                input.$viewChangeListeners.push(function () {
                    input.$setValidity('test', input.$modelValue !== 'test');
                });
            });
        }

        function toggleLimitOptions() {
            $scope.limitOptions = $scope.limitOptions ? undefined : [5, 10, 15];
        };

        function getTypes() {
            return ['Candy', 'Ice cream', 'Other', 'Pastry'];
        };

        function onPaginate(page, limit) {
            console.log('Scope Page: ' + $scope.query.page + ' Scope Limit: ' + $scope.query.limit);
            console.log('Page: ' + page + ' Limit: ' + limit);

            $scope.promise = $timeout(function () {

            }, 2000);
        };

        function deselect(item) {
            console.log(item.name, 'was deselected');
        };


        function loadStuff() {
            $scope.promise = $timeout(function () {

            }, 2000);
        };

        function onReorder(order) {

            console.log('Scope Order: ' + $scope.query.order);
            console.log('Order: ' + order);

            $scope.promise = $timeout(function () {

            }, 2000);
        };


        //###################      Table Method

        activate();

        function activate() {
          //  var data = AdminTableService.getDataForTable();

            var promises = [];
            promises.push(AdminTableService.getDataForTable());                                 //0

            $q.all(promises).then(function (results) {
                $scope.desserts = results[0];

            });
            // var currentItem = dictionaryService.getCurrentItem(
            //agreementService.toJSON($scope.Card, currentItemId, "/Lists/StandardContract/", true)
            //);

            // var promises = [];
            // promises.push(currentItem);                                 //0

            // $q.all(promises).then(function (results) {
            //     $scope.Card = results[0];
            // });
        }
    }
})();