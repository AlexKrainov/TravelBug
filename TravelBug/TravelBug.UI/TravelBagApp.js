//(function () {
//    var version = "1.2.2.0";
//    'use strict';
//    var app = angular.module("TravelBagApp");
//    app.config(function ($mdDateLocaleProvider, $routeProvider, $mdThemingProvider, $httpProvider, $mdDialogProvider) {
//        $httpProvider.interceptors.push(function ($templateCache, $q, $rootScope) {
//            return {
//                'request': function (request) {
//                    if ($templateCache.get(request.url) === undefined) { // cache miss
//                        // Item is not in $templateCache so add our query string
//                        if (request.url.indexOf(".html") !== -1) {
//                            request.headers["Accept"] = "application/json, text/plain, */*";
//                            request.url = request.url + '?appVersion=' + encodeURIComponent(version);
//                        } else {
//                        }
//                    }
//                    request.headers['X-RequestDigest'] = $rootScope.formRequestDigest;
//                    return request || $q.when(request);
//                }
//            };
//        });
//    });
//})();


//(function () {
//    var version = "1.2.2.0";
//    'use strict';
//    var app = angular.module("TravelBagApp",
//          [
//              //"ngMessages",
//              //"ngSanitize",
//              //"ngRoute",
//              //"ngMaterial",
//              ////"ngFileUpload",
//              ////"kendo.directives",
//              ////'vAccordion',
//              //'ngAnimate',
//              "TravelBagApp.Common",
//              "TravelBagApp.Services",
//              "TravelBagApp.Controllers"
//          ]);
//    angular.module("TravelBagApp.Common", []); //ToDo ??
//    angular.module("TravelBagApp.Services", []);
//    angular.module("TravelBagApp.Controllers", []);
//    app.config(function ($mdDateLocaleProvider, $routeProvider, $mdThemingProvider, $httpProvider, $mdDialogProvider) {
//        $httpProvider.interceptors.push(function ($templateCache, $q, $rootScope) {
//            return {
//                'request': function (request) {
//                    if ($templateCache.get(request.url) === undefined) { // cache miss
//                        // Item is not in $templateCache so add our query string
//                        if (request.url.indexOf(".html") !== -1) {
//                            request.headers["Accept"] = "application/json, text/plain, */*";
//                            request.url = request.url + '?appVersion=' + encodeURIComponent(version);
//                        } else {
//                        }
//                    }
//                    request.headers['X-RequestDigest'] = $rootScope.formRequestDigest;
//                    return request || $q.when(request);
//                }
//            };
//        });
//    });
//})();