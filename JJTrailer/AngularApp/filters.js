var JJTrailerFilter = angular.module('JJTrailerFilter', []);
JJTrailerFilter.filter('checkmark', function () {
    return function (input) {
        return input ? '\u2713' : '\u2718';
    };
});