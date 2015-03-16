var JJTailerControllers = angular.module('JJTailerControllers', []);

JJTailerControllers.controller('DepartmentListCtrl', ['$scope', 'Department', function ($scope, Department) {
    $scope.Department = Department.query();
}]);

//phonecatControllers.controller('PhoneDetailCtrl', ['$scope', '$routeParams', 'Phone', function ($scope, $routeParams, Phone) {
//    $scope.phone = Phone.get({ phoneId: $routeParams.phoneId }, function (phone) {
//        $scope.mainImageUrl = phone.images[0];
//    });

//    $scope.setImage = function (imageUrl) {
//        $scope.mainImageUrl = imageUrl;
//    }
//}]);