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
          "AdminTableService",
           "$mdDialog"
    ];

    function AdminCtrl($scope, $q, $http, $mdEditDialog, $timeout, AdminTableService, $mdDialog) {

        $scope.editComment = editComment;
        $scope.toggleLimitOptions = toggleLimitOptions;
        $scope.onPaginate = onPaginate;
        $scope.deselect = deselect;
        $scope.refresh = refresh;
        $scope.deleteTour = deleteTour;
        $scope.showDialog = showDialog;

        //#########   Table Field
        $scope.isReady = false;
        $scope.options = {
            rowSelection: true,
            multiSelect: false,
            autoSelect: false,
            //decapitate: false, //header show\hide 
            largeEditDialog: false,
            boundaryLinks: false,
            limitSelect: true,
            pageSelect: true
        };

        $scope.selected = [];
        $scope.limitOptions = [5, 10, 15, {
            label: 'All',
            value: function () {
                return $scope.tours ? $scope.tours.count : 0;
            }
        }];

        $scope.columns = [{
            name: 'ID',
            orderBy: 'id'
        }, {
            descendFirst: true,
            name: 'Title',
            orderBy: 'Title'
        }, {
            name: 'Description'
        }];
        //#########   Table Field





        //###################      Table Method

        function showDialog(event, id) {
            $mdDialog.show({
                controller: "DialogController",
                templateUrl: '../TravelBug.UI/Directives/TourDialogEdit/TourEditTabs.html',
                //parent: angular.element(document.body),
                targetEvent: event,
                clickOutsideToClose: true,
                fullscreen: false, // Only for -xs, -sm breakpoints.
                locals: {
                    ID: id
                }
            }).then(function (answer) {
                if (answer == "Save")
                    refresh();
            }, function () {
                refresh();
            });

        }

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

        function deleteTour() {
            if ($scope.selected && $scope.selected.length != 0) {
                if (confirm("Вы уверены, что хотите удалить этот " + $scope.selected[0].Title + "?")) {

                    var promises = [];
                    promises.push(AdminTableService.onDelete($scope.selected[0].id));
                    $q.all(promises).then(function (results) {
                        refresh();
                    });
                }

            }

        }

        function toggleLimitOptions() {
            $scope.limitOptions = $scope.limitOptions ? undefined : [5, 10, 15];
        };

        function onPaginate(page, limit) {
            console.log('Scope Page: ' + $scope.query.page + ' Scope Limit: ' + $scope.query.limit);
            console.log('Page: ' + page + ' Limit: ' + limit);

            $scope.promise = $timeout(function () {

            }, 2000);
        };

        function deselect(item) {
            console.log(item, 'was deselected');
        };


        function refresh() {
            $scope.promise = $timeout(function () {
                var promises = [];
                promises.push(AdminTableService.getDataForTable());                                 //0

                $q.all(promises).then(function (results) {
                    $scope.tours = results[0].data;
                    $scope.isReady = true;
                });
            }, 1000);
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
            refresh();


        }
    }
})();