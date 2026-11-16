///*app.js tabs*/

//var app = angular.module('MyApp', [])
//;debugger
////app.controller('MainCtrl', ['$scope', function ($scope) {
//app.controller('MainCtrl', function ($scope, $rootScope, $http, $filter) {
//    ; debugger
//   // $scope.states = ["NY", "CA", "WA"];


//    $http({
//        method: "GET",
//        url: "/access/fillQuickCat",
//        dataType: 'json',
//        //data: customer,
//        //headers: { "Content-Type": "application/json" }
//    }).then(function (response) {

//        if (response.status == 200 && response.data != "") {
//            $scope.bindQcat(response.data);

//        }
//    });
//    $scope.bindQcat = function (value) {
//        $rootScope.QCat = value;
//    }

//});

////app.controller('InsideCtrl', ['$scope', function ($scope) {
//app.controller('InsideCtrl', function ($scope, $rootScope, $http, $filter) {
//    //var vm = this;
//    //vm.states2 = [$scope.state +" "+"A1", "B", "C"];

//    var vm = this;
//    vm.states2 = [$scope.QCat[$scope.$index]["CatId"]  + $scope.QCat[$scope.$index]["PrdCategory_Name"] + "-" +"A1", "B", "C"];


//    $http({
//        method: "GET",
//        url: "/access/fillQuickProd/" + $scope.QCat[$scope.$index]["CatId"],
//        dataType: 'json',
//        //data: customer,
//        //headers: { "Content-Type": "application/json" }
//    }).then(function (response) {

//        if (response.status == 200 && response.data != "") {
//            $scope.bindcart(response.data);

//        }
//    });


//});


$(document).scroll(function () {

    if ($(window).scrollTop() > 120){
        $(".cartbtnblk").addClass("sticky");
    }
    else {
        $(".cartbtnblk").removeClass("sticky");
    }

});



var app = angular.module('MyApp', [])

app.controller('quickorder', function ($scope, $http, $window,$location) {

    //; debugger
 //   var weburl = $location.absUrl();
    //var urlhost = $location.host();
    //var dUrl = weburl.replace(urlhost+"/account", "");
    $scope.type = "recent";
    $http({
        method: "GET",
        url: "/access/fillQuickCat?type=" + $scope.type,
        dataType: 'json',
        //data: customer,
        //headers: { "Content-Type": "application/json" }
    }).then(function (response) {

        if (response.status == 200 && response.data != "") {
            $scope.CatDDList = response.data;
            $scope.category = $scope.CatDDList[0];
            $scope.selectCatID = response.data[0].CatId;
           
          
        }
        else {
            $scope.type = "all";
            $http({
                method: "GET",
                url: "/access/fillQuickCat?type=" + $scope.type,
                dataType: 'json',
                //data: customer,
                //headers: { "Content-Type": "application/json" }
            }).then(function (response) {

                if (response.status == 200 && response.data != "") {
                    $scope.CatDDList = response.data;
                    $scope.category = $scope.CatDDList[0];
                    $scope.selectCatID = response.data[0].CatId;


                }
                }).then(
                function (response) {

                    $http({
                        method: "GET",
                        url: "/access/fillQuickProd?id=" + $scope.selectCatID + "&type=" + $scope.type,
                        dataType: 'json',

                        //data: customer,
                        //headers: { "Content-Type": "application/json" }
                    }).then(function (response) {
                        if (response.status == 200 && response.data != "") {
                            $scope.catPrd = response.data;

                        }
                    });
                }
                );
        }
        }).then(
        function (response) {
            
            $http({
                method: "GET",
                url: "/access/fillQuickProd?id=" + $scope.selectCatID + "&type=" + $scope.type,
                dataType: 'json',
               
                //data: customer,
                //headers: { "Content-Type": "application/json" }
            }).then(function (response) {
                if (response.status == 200 && response.data != "") {
                    $scope.catPrd=response.data;

                }
            });
        }
        );
       

    
    //$scope.CatDDList = $http({

    //    method: "GET",
    //    url: "/access/fillQuickCat",
    //    dataType: 'json',
    //    //data: customer,
    //    //headers: { "Content-Type": "application/json" }
    //}).then(function (response) {

    //    //if (response.status == 200 && response.data != "") {
    //    //    $scope.bindQcat(response.data);

    //    //}
    //});




    $scope.Qu = [{
        Kgs: "0",
        Pack: "",
        ProTtl: 0,
        Code: "B13320",
        ProSku: "20 kg Paper Bag",
        ProPrice: 999.50
    },
    {
    Kgs: "0",
    Pack: "",
    ProTtl: 0,
    Code: "B14420",
    ProSku: "5 kg Paper Bag",
    ProPrice: 459.00
    },
    {
        Kgs: "0",
        Pack: "",
        ProTtl: 0,
        Code: "B146520",
        ProSku: "25 kg Paper Bag",
        ProPrice: 150.00
    },
    {
        Kgs: "0",
        Pack: "",
        ProTtl: 0,
        Code: "B19520",
        ProSku: "10 kg Paper Bag",
        ProPrice: 79.65
    }
    ];

    $scope.ttlkgs = 0;
    $scope.ttlAmt = 0;
    //$scope.Qu.push($scope.Qu[0]);

    //$scope.Q = $scope.Q[0];
  
 
    $scope.changecat= function () {
      // ;debugger
      //  console.log($scope.category.PrdCategory_Name, $scope.category.CatId)
        $http({
            method: "GET",
            url: "/access/fillQuickProd?id=" + $scope.category.CatId + "&type=" + $scope.type,
            dataType: 'json',
            //data: customer,
            //headers: { "Content-Type": "application/json" }
        }).then(function (response) {

            if (response.status == 200 && response.data != "") {
                $scope.catPrd = response.data;

            }
        });
    }
    $scope.ShowHide = function () {
        //If DIV is visible it will be hidden and vice versa.

        //$scope.add = ['1', '2', '3', '4', '5', '6', '7', '8', '9', '10', '11'];
        $scope.IsVisible = $scope.IsVisible ? false : true;
        $scope.IsHidden = $scope.IsHidden ? false : true;
    }

    $scope.change = function (index) {
        // ; debugger
        var etc = $scope.catPrd[index].Etcstring1;
        if (etc == '') {
            $scope.catPrd[index].TotalWeight = 0;
            $scope.catPrd[index].EtcDouble1 = 0;
            $scope.catPrd[index].Amount = $scope.catPrd[index].EtcDouble1;
        }
        else
        {
            $http({
                method: "POST",
                url: "/access/changeQOqty",

                data: '{D : "' + $scope.catPrd[index].Product_Id + '", Q : "' + $scope.catPrd[index].Etcstring1 + '", M : "' + $scope.catPrd[index].Product_MOQ + '", L : ""}',
                dataType: 'json'

            }).then(function (response) {

                if (response.status == 200 && response.data != "") {
                    if (response.data == '/login') {
                        window.location.href = "/login";

                    }
                    else {
                        if (response.data.includes("Minimum Order") || response.data.includes("sample")) {
                            $scope.catPrd[index].Etcstring1 = $scope.catPrd[index].Product_MOQ;
                            alert(response.data);
                        } else {
                           
                            $scope.catPrd[index].Product_Rate = response.data[0].Product_Rate;
                           
                        }
                      //  ; debugger
                        var string = $scope.catPrd[index].Packing_Weight;
                        var sku = string;//string.match(/\d+/g).map(Number);
                        $scope.catPrd[index].TotalWeight = sku * $scope.catPrd[index].Etcstring1;
                        $scope.catPrd[index].EtcDouble1 = $scope.catPrd[index].TotalWeight * $scope.catPrd[index].Product_Rate;
                        $scope.catPrd[index].Amount = $scope.catPrd[index].EtcDouble1;
                        var c = $scope.catPrd.length;
                        $scope.ttlkgs = 0;
                        $scope.ttlAmt = 0;
                        //for (var i = 0; i < c; i++) {
                        //    $scope.ttlkgs = (parseInt($scope.ttlkgs) + parseInt($scope.catPrd[i].TotalWeight));
                        //    $scope.ttlAmt = $scope.ttlAmt + $scope.catPrd[i].EtcDouble1;
                        //}
                    }
                }
            });
        }

       
       
        //alert(numbers);
    }
    $scope.x = function () {
        $scope.count++;
    };
  

    $scope.addtocart = function () {
       // ;debugger
        var c = $scope.catPrd.length;
        var Pid;
        var qty;
        var totalQty = 0;
        for (var i = 0; i < c; i++) {
            Pid = parseInt($scope.catPrd[i].Product_Id);
            qty = parseInt($scope.catPrd[i].Etcstring1);
            if (qty > 0) {

            totalQty = totalQty + qty;
            }
            if (qty >= 1)
            {
                $http({
                    method: "POST",
                    url: "/Access/QOaddcart",

                    data: { "D": Pid, "Q": qty }
                    //data: customer,
                    //headers: { "Content-Type": "application/json" }
                }).then(function (response) {

                    if (response.status == 200) {
                        if (response.data.includes("Minimum Order") || response.data.includes("sample")) {
                            //alert(response.data);
                        } else {
                            if (response.data == '/login') {
                                window.location.href = "/login";

                            }
                            else {
                                //alert('Product has beed added to your cart');
                                //window.location = '/account/quickorder';
                            }
                        }
                    }

                });
            }
           

        }

        if (totalQty > 0) {
            alert('Product has beed added to your cart');
            window.location = '/account/quickorder';
            // without postback
            //$http({
            //    method: "GET",
            //    url: "/access/fillsidecart",
            //    dataType: 'json',
            //}).then(function (response) {

            //    if (response.status == 200 && response.data != "") {
            //        angular.element(document.getElementById('side-cart')).scope().$emit('addcarton', response.data);
            //        var c = $scope.catPrd.length;
            //        ;debugger
            //        for (var i = 0; i < c; i++) {

            //            $scope.catPrd[i].Etcstring1 = '';
            //            $scope.catPrd[i].TotalWeight = 0;
            //            $scope.catPrd[i].EtcDouble1 = 0;
            //            $scope.catPrd[i].Amount = $scope.catPrd[i].EtcDouble1;
            //            }
            //    }
            //});
            
        } else {
            alert('Please add no. of pack in atleast one product');
        }

        
    }
    //$scope.change = function (index) {

    //    var string = $scope.Qu[index].ProSku;
    //    var sku = string.match(/\d+/g).map(Number);
    //    $scope.Qu[index].Kgs = sku * $scope.Qu[index].Pack;
    //    $scope.Qu[index].ProTtl = $scope.Qu[index].Kgs * $scope.Qu[index].ProPrice;
    //    var c = $scope.Qu.length;
    //    $scope.ttlkgs = 0;
    //    $scope.ttlAmt = 0;
    //    for (var i = 0; i < c; i++) {
    //        $scope.ttlkgs = (parseInt($scope.ttlkgs) + parseInt($scope.Qu[i].Kgs));
    //        $scope.ttlAmt = $scope.ttlAmt + $scope.Qu[i].ProTtl;
    //    }

    //    //alert(numbers);
    //}


    $scope.recentProd = function () {
       
       // var weburl = $location.absUrl();
        //var urlhost = $location.host();
        //var dUrl = weburl.replace(urlhost+"/account", "");
        $scope.type = "recent";
        $http({
            method: "GET",
            url: "/access/fillQuickCat?type=" + $scope.type,
            dataType: 'json',
            //data: customer,
            //headers: { "Content-Type": "application/json" }
        }).then(function (response) {

            if (response.status == 200 && response.data != "") {
                $scope.CatDDList = response.data;
                $scope.category = $scope.CatDDList[0];
                $scope.selectCatID = response.data[0].CatId;


            }
            else {
                $scope.type = "all";
                $http({
                    method: "GET",
                    url: "/access/fillQuickCat?type=" + $scope.type,
                    dataType: 'json',
                    //data: customer,
                    //headers: { "Content-Type": "application/json" }
                }).then(function (response) {

                    if (response.status == 200 && response.data != "") {
                        $scope.CatDDList = response.data;
                        $scope.category = $scope.CatDDList[0];
                        $scope.selectCatID = response.data[0].CatId;


                    }
                });
            }
        }).then(
            function (response) {
                $http({
                    method: "GET",
                    url: "/access/fillQuickProd?id=" + $scope.selectCatID + "&type=" + $scope.type,
                    dataType: 'json',

                    //data: customer,
                    //headers: { "Content-Type": "application/json" }
                }).then(function (response) {

                    if (response.status == 200 && response.data != "") {
                        $scope.catPrd = response.data;

                    }
                });
            }
            );

    }
    $scope.allProd = function () {

        // var weburl = $location.absUrl();
        //var urlhost = $location.host();
        //var dUrl = weburl.replace(urlhost+"/account", "");
        $scope.type = "all";
        $http({
            method: "GET",
            url: "/access/fillQuickCat?type=" + $scope.type,
            dataType: 'json',
            //data: customer,
            //headers: { "Content-Type": "application/json" }
        }).then(function (response) {

            if (response.status == 200 && response.data != "") {
                $scope.CatDDList = response.data;
                $scope.category = $scope.CatDDList[0];
                $scope.selectCatID = response.data[0].CatId;


            }
            else {
                $scope.type = "all";
                $http({
                    method: "GET",
                    url: "/access/fillQuickCat?type=" + $scope.type,
                    dataType: 'json',
                    //data: customer,
                    //headers: { "Content-Type": "application/json" }
                }).then(function (response) {

                    if (response.status == 200 && response.data != "") {
                        $scope.CatDDList = response.data;
                        $scope.category = $scope.CatDDList[0];
                        $scope.selectCatID = response.data[0].CatId;


                    }
                });
            }
        }).then(
            function (response) {
                $http({
                    method: "GET",
                    url: "/access/fillQuickProd?id=" + $scope.selectCatID + "&type=" + $scope.type,
                    dataType: 'json',

                    //data: customer,
                    //headers: { "Content-Type": "application/json" }
                }).then(function (response) {

                    if (response.status == 200 && response.data != "") {
                        $scope.catPrd = response.data;

                    }
                });
            }
            );

    }
});