$(document).ready(function () {

    angular.element(document.getElementById('content')).scope().load();

});


var app = angular.module('MyApp', [])

app.controller('recipegrid', function ($scope, $http, $window) {

    $scope.load = function () {
        $http({
            method: "GET",
            url: "/access/GetRecipegrid",
            dataType: 'json',
            //data: customer,
            //headers: { "Content-Type": "application/json" }
        }).then(function (response) {

            if (response.status == 200) {
                $scope.Recipe = response.data;
            }

        });
    }

    //$scope.load();
    

});