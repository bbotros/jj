var JJTrailerServices = angular.module('JJTrailerServices', ['ngResource']);

JJTrailerServices.factory('Department', ['$resource',
  function ($resource) {
      return $resource('/Admin/DepartmentMenus/GetDep', {}, {
         query: { method: 'GET', isArray: true }
      });
  }]);