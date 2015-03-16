var JJTrailerApp = angular.module('JJTrailerApp', [
  'ngRoute',
  'JJTrailerAnimations',
  'JJTailerControllers',
  'JJTrailerServices',
  'JJTrailerFilter'
]);
//JJTrailerApp.config(['$routeProvider',
//  function($routeProvider) {
//    $routeProvider.
//      when('/all/:phoneId', {
//        templateUrl: '/AngularApp/Templates/template1.html',
//        controller: 'JJTailerControllers'
//      }).
//      otherwise({
//        redirectTo: '/all'
//      });
//  }]);
//var JJTrailerApp2 = angular.module('JJTrailerApp2', [
//  'ngRoute',
//  'JJTrailerAnimations',
//  'JJTailerControllers',
//  'JJTrailerServices',
//  'JJTrailerFilter'
//]);