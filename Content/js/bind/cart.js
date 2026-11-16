var app = angular.module('MyApp', [])



 

app.controller('cart', function ($scope, $rootScope, $http, $filter) {
      
    //$scope.getfreight =  $http({
    //    method: "GET",
    //    url: "/access/getfreight",
    //    dataType: 'json',
    //    //data: customer,
    //    //headers: { "Content-Type": "application/json" }
    //})
    //    .then(function (response) {
    //        if (response.status == 200 && response.data != "") {
    //            
    //            $scope.topics = response.data;
    //            var product = $scope.topics[0];
    //            $scope.freighttot = (product.Amount);
    //            $scope.freightype = (product.Type);
    //        }
    //    });

    $http({
        method: "GET",
        url: "/access/fillsidecart",
        dataType: 'json',
        //data: customer,
        //headers: { "Content-Type": "application/json" }
    }).then(function (response) {
        
        if (response.status == 200 && response.data != "") {
            $scope.bindcart(response.data);
           
        }
    });

    //$scope.$on('addcarton', function (event, value) {
       
    //    $scope.bindcart(value);

    //});
    
    $scope.$on('addressfill', function (event, value) {
        
        $scope.filladdr = value;
        $rootScope.poshipaddr = $scope.filladdr;
        $rootScope.selecteid1 = $scope.filladdr.Id;
        $rootScope.StateName = $scope.filladdr.Etcstring1;
        //if ($rootScope.StateName != "Maharashtra" ||  $rootScope.StateName != "Uttar Pradesh") {
        //    $rootScope.StateName = 'Other';
        //}
        //if ($rootScope.StateName != "Maharashtra" || $rootScope.StateName != "Gujarat" || $rootScope.StateName != "Goa" || $rootScope.StateName != "Telangana"
        //    || $rootScope.StateName != "Punjab" || $rootScope.StateName != "Haryana" || $rootScope.StateName != "Delhi" || $rootScope.StateName != "Uttar Pradesh") {
        //    $rootScope.StateName = 'Other';
        //}
    });
    //$http({
    //    method: "GET",
    //    url: "/access/FillShipAddress",
    //    dataType: 'json',
    //    //data: customer,
    //    //headers: { "Content-Type": "application/json" }
    //}).then(function (response) {

    //    if (response.status == 200 && response.data != "") {
    //        if (response.data == '/login') {
    //            window.location.href = "/login";

    //        }
    //        else {
    //            $scope.filladdr = response.data[0];
    //            $rootScope.poshipaddr = $scope.filladdr;
    //            $rootScope.selecteid1 = $scope.filladdr.Id;
    //            $rootScope.StateName = $scope.filladdr.Etcstring1;

    //        }
    //    }

    //});

    $scope.$on('addressfill', function (event, value) {
        $scope.filladdr = value;
        $rootScope.poshipaddr = $scope.filladdr;
        $rootScope.selecteid1 = $scope.filladdr.Id;
        $rootScope.StateName = $scope.filladdr.Etcstring1;
        //if ($rootScope.StateName != "Maharashtra" ||  $rootScope.StateName != "Uttar Pradesh") {
        //    $rootScope.StateName = 'Other';
        //}
        //if ($rootScope.StateName != "Maharashtra" || $rootScope.StateName != "Gujarat" || $rootScope.StateName != "Goa" || $rootScope.StateName != "Telangana"
        //    || $rootScope.StateName != "Punjab" || $rootScope.StateName != "Haryana" || $rootScope.StateName != "Delhi" || $rootScope.StateName != "Uttar Pradesh") {
        //    $rootScope.StateName = 'Other';
        //}
    });

    $http({
        method: "GET",
        url: "/access/FillShipAddress",
        dataType: 'json',
        //data: customer,
        //headers: { "Content-Type": "application/json" }
    }).then(function (response) {
        if (response.status == 200 && response.data != "") {
            if (response.data == '/login') {
                window.location.href = "/login";
            } else {
                //  Only assign if no address is already selected
                if (!$scope.filladdr || !$scope.filladdr.Id) {
                    $scope.filladdr = response.data[0];
                    $rootScope.poshipaddr = $scope.filladdr;
                    $rootScope.selecteid1 = $scope.filladdr.Id;
                    $rootScope.StateName = $scope.filladdr.Etcstring1;
                }
            }
        }
    });


    $http({
        method: "GET",
        url: "/access/FillBillAddress",
        dataType: 'json',
        //data: customer,
        //headers: { "Content-Type": "application/json" }
    }).then(function (response) {

        if (response.status == 200 && response.data != "") {
            if (response.data == '/login') {
                window.location.href = "/login";

            }
            else {
                $scope.fillbilladdr = response.data[0];
                //$rootScope.poshipaddr = $scope.filladdr;
                //$rootScope.selecteid1 = $scope.filladdr.Id;
            }
        }

    });




    $scope.bindcart = async function (value) {
        $rootScope.cart = value;
       // $scope.freightcalc();// = [$scope.freighttot];
      await  $scope.getSTotal();
      await $scope.getVatTotal();
      await  $scope.getTotal();
     //   $scope.Getimage();
       // angular.element(document.getElementById('cart')).scope().$apply();
    }
    
    $scope.freightcalc = async function () {
       // 
       // $scope.getfreight();
        ////
        //$rootScope.freighttot = null;
        //if ($rootScope.freighttot == '' || $rootScope.freighttot == null) {
        //    $rootScope.freighttot = 9999;
        //}
        ////
       // ; debugger
       // alert($rootScope.freighttot);
        var total = 0;
        if ($rootScope.freightype == 'Flat') {
            total = await $rootScope.freighttot;
        }
        else {

            var ttww = $rootScope.TotalWeight 
            var ttftot = $rootScope.freighttot
            total = ($rootScope.TotalWeight * $rootScope.freighttot);
           // alert(total);
        }
       

        $rootScope.freight = 0; // zayan 07sept23 total;

    }
    $scope.getSTotal = function () {
        ; debugger
        var total = 0;
        var Tweight = 0;
        if ($scope.cart != undefined) {
            for (var i = 0; i < $scope.cart.length; i++) {
                var product = $scope.cart[i];
                total += (product.Amount);
                Tweight += (product.TotalWeight);
            }
        }
        $rootScope.getSTotal1 = total;
        $rootScope.TotalWeight = Math.ceil(Tweight);
        $scope.freightcalc();
    }

    $scope.getVatTotal = function () {
        ;debugger
        var total = 0;
        var freightAmt = 0;
        if ($scope.cart != undefined) {
            for (var i = 0; i < $scope.cart.length; i++) {
                var product = $scope.cart[i];
                total += (product.vatinr);
            }
            var Freighttaxper = 5;

            freightAmt = $rootScope.freight * (Freighttaxper / 100);
            total += 0; // zayan 07sept23 freightAmt;
        }
        $rootScope.getVatTotal1 = total;
    }
    $scope.getTotal = function () {
         ; debugger
        var total = 0;
        var Stotal = 0;
        var Vtotal = 0;
        if ($scope.cart != undefined) {
            for (var i = 0; i < $scope.cart.length; i++) {
                var product = $scope.cart[i];
                Stotal += (product.Amount);
                Vtotal += (product.vatinr);
            }
            total = Stotal + $scope.getVatTotal1; // zayan 07sept23 + $scope.freight;
            total = total;
        }
        $rootScope.getTotal1 = total;
    }
    //fk



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
         
    //}
    $scope.onChange = function (collection, item) {
      //  ;debugger
        collection.forEach(function (prod) {
            if (prod.Product_Id === item.Product_Id) {
                
                if (prod.Qty == 0 || prod.Qty == null)
                {
                    prod.Qty = 1;
                }

              //  prod.Amount = prod.Qty * prod.Product_Rate;
                   $http({
                    method: "POST",
                    url: "/access/changeqty",
                   // data: { "D": 13, "Q": 1, "M": 0, "L": prod.Product_linkas },
                    //data: '{D : "' + prod.Product_Id + '", Q : "' + prod.Qty + '", M : "' + prod.Product_MOQ + '", L : "' + prod.Product_linkas + '"}',
                    data: '{D : "' + prod.Product_Id + '", Q : "' + prod.Qty + '", M : "' + prod.Product_MOQ + '", L : "' + prod.Product_linkas + '"}',
                    dataType: 'json'
                    //data: { "D": prod.Product_Id, "Q": prod.Qty,  "M": prod.Product_MOQ, "L": prod.Product_linkas }
                   // data: { "D": '13', "Q": 1, "M": 0, "L": prod.Product_linkas }
                    //data: customer,
                    //headers: { "Content-Type": "application/json" }
                }).then(function (response) {
                    
                    if (response.status == 200 && response.data != "") {
                        // $scope.bindcart(response.data);
                        
                        // $scope.val = response.data;
                        // $filter('filter')(response.data, { id: 2 });
                        // var product = $scope.val[0];
                        if (response.data == '/login') {
                            window.location.href = "/login";

                        }
                        else{
                        if (response.data.includes("Minimum Order") || response.data.includes("sample")) {
                            prod.Qty = prod.Product_MOQ;
                            alert(response.data);
                        } else {
                            if (prod.Product_linkas == "Sample" || prod.Qty == 0 || prod.Qty == null) {
                                prod.Qty = 1;
                            }
                            //var product1 = $filter('filter')(response.data, { Product_Id: prod.Product_Id })[0];
                            var product1 = $filter('filter')(response.data, function (d) { return d.Product_Id === prod.Product_Id; })[0];
                            prod.Product_Rate = (product1.Product_Rate);
                            prod.TotalWeight = (product1.TotalWeight);
                            prod.vatinr = (product1.vatinr);
                            prod.Amount = (product1.Amount);
                           // ;debugger
                            $scope.getSTotal();
                            $scope.getVatTotal();
                            $scope.getTotal();
                        //    $scope.Getimage();
                            angular.element(document.getElementById('side-cart')).scope().$emit('addcarton', response.data);
                        }
                    }
                    }
                });
            }
        });
    }


    $scope.removeRow = function (D) {
        
        var index = -1;
        var comArr = eval($scope.cart);
        
        for (var i = 0; i < comArr.length; i++) {
            if (comArr[i].Product_Id === D) {
                index = i;
                $http({
                    method: "POST",
                    url: "/access/RemoveItem",
                    data: '{PID : "' + D + '"}',
                    dataType: 'json'
                }).then(function (response) {

                    if (response.status == 200 && response.data != "") {
                        
                        if (response.data.length > 0) {
                            if (response.data == '/login') {
                                window.location.href = "/login";

                            }
                            else {
                                $scope.getSTotal();
                                $scope.getVatTotal();
                                $scope.getTotal();
                              //  $scope.Getimage();
                                angular.element(document.getElementById('side-cart')).scope().$emit('addcarton', response.data);
                            }
                        }
                        else {
                            window.location.href = "/cart";
                        }
                    }
                    else {
                        window.location.href = "/cart";
                    }
                    });
                break;
            }
        }
        if (index === -1) {
            alert("Something gone wrong");
        }
       
        $rootScope.cart.splice(index, 1);
        //$scope.getSTotal();
        //$scope.getVatTotal();
        //$scope.getTotal();
        //angular.element(document.getElementById('side-cart')).scope().$emit('addcarton', response.data);
    };

});

app.controller('PO', function ($scope, $http, $window, $rootScope) {
    
  //  $rootScope.poshipaddr = $scope.filladdr;
    $scope.fillPoshipaddr = $rootScope.poshipaddr;


    $scope.copypo = function () {
        

        $scope.fillPoshipaddr = $rootScope.poshipaddr;
        $scope.POcart = $rootScope.cart;
        $scope.freightcalc = 0; // zayan 07sept23 $rootScope.freight;
        $scope.getSTotal = $rootScope.getSTotal1;
        $scope.POTotalWeight = $rootScope.TotalWeight;
        //$rootScope.getSTotal1 = total;
        //$rootScope.TotalWeight = Math.ceil(Tweight);
        //$scope.freightcalc();
        $scope.getVatTotal = $rootScope.getVatTotal1;
        $scope.getTotal = $rootScope.getTotal1;
        $scope.POwordsamount = convertNumberToWords(Math.ceil($scope.getTotal));
    };


});


app.controller('shippaddr', function ($scope, $http, $window, $rootScope) {

    $scope.load = function () {
        $http({
            method: "GET",
            url: "/access/GetShipAddress",
            dataType: 'json',
            //data: customer,
            //headers: { "Content-Type": "application/json" }
        }).then(function (response) {

            if (response.status == 200 && response.data != "") {
                
                $scope.selecteid = $rootScope.selecteid1;
                $scope.Address = response.data;
               // $rootScope.StateName = response.data[0]["Etcstring1"];
            }

        });
    }

    $scope.load();




    $scope.IsVisible = true;
    $scope.IsHidden = false;
    $scope.ShowHide = function () {
        //If DIV is visible it will be hidden and vice versa.

        //$scope.add = ['1', '2', '3', '4', '5', '6', '7', '8', '9', '10', '11'];
        $scope.IsVisible = $scope.IsVisible ? false : true;
        $scope.IsHidden = $scope.IsHidden ? false : true;
        $scope.mode = "1";
        $scope.add = [{
            Address: "",
            City: "",
            CompName: "",
            Country: "",
            Email: "",
            Etcstring1: "",
            Gst: "",
            Id: "",
            Name: "",
            Pin: "",
            State: "",
            Telephone: ""
        }];
    }


    $scope.editAdd = function (Id) {
        $http({
            method: "POST",
            url: "/access/GetShipAddress",
            dataType: 'json',
            data: '{adid : "' + Id + '"}',
            //headers: { "Content-Type": "application/json" }
        }).then(function (response) {


            var j = JSON.stringify(response.data);

            if (response.status == 200 && response.data != "") {
                $scope.add = response.data[0];
                $scope.mode = "2"
                var d = response.data[0].State;



                $scope.IsVisible = $scope.IsVisible ? false : true;
                $scope.IsHidden = $scope.IsHidden ? false : true;
                setTimeout(function () {
                    $("#State option[value=" + d + "]").attr("selected", "selected");
                }, 200);
            }

        });

    }
    $scope.selectAdd = function (Id) {
        $http({
            method: "POST",
            url: "/access/SelectShipAddress",
            dataType: 'json',
            data: '{adid : "' + Id + '"}',
            //headers: { "Content-Type": "application/json" }
        }).then(function (response) {


            var j = JSON.stringify(response.data);

            if (response.status == 200 && response.data != "") {
                //$scope.add = response.data[0];
                //$scope.mode = "2"
                //var d = response.data[0].State;

                
                $scope.filladdr = response.data[0];
                $rootScope.poshipaddr = $scope.filladdr;
                $rootScope.selecteid1 = $scope.filladdr.Id;
                $rootScope.StateName = $scope.filladdr.Etcstring1;
                //var st = $scope.filladdr.Etcstring1;
                //if (st != 'Uttar Pradesh' && st != 'Maharashtra' ) {
                //    $rootScope.StateName = 'Other';
                //}
        //if ($rootScope.StateName != "Maharashtra" || $rootScope.StateName != "Gujarat" || $rootScope.StateName != "Goa" || $rootScope.StateName != "Telangana"
        //    || $rootScope.StateName != "Punjab" || $rootScope.StateName != "Haryana" || $rootScope.StateName != "Delhi" || $rootScope.StateName != "Uttar Pradesh") {
        //    $rootScope.StateName = 'Other';
        //}
                angular.element(document.getElementById('cart')).scope().$emit('addressfill', response.data[0]);
                $scope.load();
               // $scope.IsVisible = $scope.IsVisible ? false : true;
               // $scope.IsHidden = $scope.IsHidden ? false : true;
             //   $scope.load();
                //setTimeout(function () {
                //    $("#State option[value=" + d + "]").attr("selected", "selected");
                //}, 200);
            }

        });

    }

    $scope.submitForm = function () {
        
        var obj = {};
        obj.Address = $scope.add.Address;
        obj.City = $scope.add.City;
        obj.Mail = $scope.add.Email;
        obj.Name = $scope.add.CompName;
        obj.Pin = $scope.add.Pin;
        obj.State = $scope.add.State;
        obj.Telephone = $scope.add.Telephone;
        obj.id = $scope.CId;
        obj.Aid = $scope.add.Id;
        obj.mode = $scope.mode;
        obj.Gst = $scope.add.Gst;

        //$scope.addr = [];
        //$scope.add.splice(0, 12);
        //$scope.addr.push($scope.add);
        //var j = JSON.stringify($scope.addr);
        $http({
            method: 'POST',
            url: '/access/UpdateShipAdd',
            data: JSON.stringify(obj),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            //headers: { 'Content-Type': 'application/json; charset=utf-8' }
        }).then(function (response) {
            
            $scope.load();
            $scope.IsVisible = $scope.IsVisible ? false : true;
            $scope.IsHidden = $scope.IsHidden ? false : true;
            $scope.mode = "1";
            $scope.add = [{
                Address: "",
                City: "",
                CompName: "",
                Country: "",
                Email: "",
                Etcstring1: "",
                Gst: "",
                Id: "",
                Name: "",
                Pin: "",
                State: "",
                Telephone: ""
            }];
        });
    };




});

$(document).ready(function () {
    document.addEventListener('contextmenu', function (e) {
        e.preventDefault();
    });
    document.onkeydown = function (e) {
        if (event.keyCode == 123) {
            return false;
        }
        if (e.ctrlKey && e.shiftKey && e.keyCode == 'I'.charCodeAt(0)) {
            return false;
        }
        if (e.ctrlKey && e.shiftKey && e.keyCode == 'C'.charCodeAt(0)) {
            return false;
        }
        if (e.ctrlKey && e.shiftKey && e.keyCode == 'J'.charCodeAt(0)) {
            return false;
        }
        if (e.ctrlKey && e.keyCode == 'U'.charCodeAt(0)) {
            return false;
        }
    }

    var ngL;    
    var element = new Image();
    Object.defineProperty(element, 'id', {
        get: function () {
            ngL = 'lamda';
              throw new Error("Dev tools checker");
        }
    });

    requestAnimationFrame(function check() {
        ngL = 'gama';
        //;debugger
        console.dir(element);
        //document.querySelector('#devtool-status').className = checkStatus;
        if (ngL == 'lamda') {

            $('.checkoutbtn').css('display', 'none');
            window.location.replace("/");
        }
        else {

            $('.checkoutbtn').removeAttr('style');
        }
        requestAnimationFrame(check);
    });

    

    // Code for the Validator
    var form = $('#reqest-form');
    var error = $('.form-control', form);

    $.validator.addMethod(
        "regex",
        function (value, element, regexp) {
            var re = new RegExp(regexp);
            return this.optional(element) || re.test(value);
        },
        "This is invalid"
    );



    var $validator = $('#reqest-form').validate({
        //doNotHideMessage: true, //this option enables to show the error/success messages on tab switch.
        //errorElement: 'span', //default input error message container
        //errorClass: 'error-block1', // default input error message class
        //focusInvalid: false, // do not focus the last invalid input
        rules: {
            Name: {
                required: true,
                regex: /^([a-zA-Z]+\s)*[a-zA-Z]+$/,
                minlength: 3,
            },
            Email: {
                required: true,
                regex: /^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/
            },
            Mobile: {
                required: true,
                regex: /([0-9]{10}$)/,
                minlength: 10,
                maxlength: 10,
            },
            Organisation: {
                required: true
            },
            Address: {
                required: true
            },
            Pin: {
                required: true,
                regex: /([0-9]{4,6}$)/,
            },
            City: {
                required: true,
                regex: /^([a-zA-Z]+\s)*[a-zA-Z]+$/,
            },
            Comment: {
                required: true
            }

        },

        messages: {
            Name: {
                required: "This is required",
                regex: "This is invalid",
                minlength: "This is invalid",
            },
            Email: {
                required: "This is required",
                regex: "This is invalid"
            },
            Tele: {
                required: "This is required",
                regex: "This is invalid",
                minlength: "This is invalid",
                maxlength: "This is invalid",
            },
            Organisation: {
                required: "This is required"
            },
            Address: {
                required: "This is required"
            },
            Pin: {
                required: "This is required",
                regex: "This is invalid",
            },
            City: {
                required: "This is required",
                regex: "This is invalid",
            },
            Comment: {
                required: "This is required"
            }
        },

        errorPlacement: function (error, element) {
            //if (element.attr("name") == "typeoption") { // for uniform radio buttons, insert the after the given container
            //    error.insertAfter("#show-error");
            //}
            ////else if (element.attr("name") == "payment[]") { // for uniform checkboxes, insert the after the given container
            ////    error.insertAfter("#form_payment_error");
            ////}
            //else {
            //    error.insertAfter(element); // for other inputs, just perform default behavior
            //}
            error.insertAfter(element);
        },
        //success: function (label) {
        //    if (label.attr("for") == "typeoption") { // for checkboxes and radio buttons, no need to show OK icon
        //        label.remove(); // remove error label here
        //    }
        //},
        submitHandler: function (form) {
            var $valid = $('#reqest-form').valid();
            if (!$valid) {
                $validator.focusInvalid();
                return false;
            }
            else {
                submitrequest()
                return false;
            }
            //add here some ajax code to submit your form or just call form.submit() if you want to submit the form without ajax
        }
    });







   // $("#Po-button").click();
    $('#po-modal').modal({ show: false, backdrop: 'static', keyboard: false })  
    $("#copypo").addClass("d-none");
    $("[name=PoAttach]").change(function () {
        var att = $("[name=PoAttach]").val();
        
        if (att != "") {
            $("[name=Mode]").val("2").change();
         //   $("[name=Mode]").addClass("d-none");
            $("#copypo").addClass("d-none");
        }
        else {
            $("[name=Mode]").val("").change();
          //  $("[name=Mode]").removeClass("d-none");
            $("#copypo").removeClass("d-none");

            
        }
    })


    $("[name=Mode]").change(function () {
        var att = $("[name=Mode]").val();

        if (att != "1") {
         //   $("[name=Mode]").val("2").change();
         //   $("[name=Mode]").addClass("d-none");
            $("#copypo").addClass("d-none");
            $("#PoAttach").removeClass("d-none");
            $("#lblPoAttach").removeClass("d-none");
        }
        else {
          //  $("[name=Mode]").val("").change();
         //   $("[name=Mode]").removeClass("d-none");
            $("#copypo").removeClass("d-none");
           
            $("#PoAttach").addClass("d-none");
            $("#lblPoAttach").addClass("d-none");
            

        }
    })
    

    


    // Code for the Validator
    var form = $('#billing-shipping-form');
    var error = $('.sm-form-control', form);

    $.validator.addMethod(
        "regex",
        function (value, element, regexp) {
            var re = new RegExp(regexp);
            return this.optional(element) || re.test(value);
        },
        "This is invalid"
    );



    var $validator = $('#billing-shipping-form').validate({
        //doNotHideMessage: true, //this option enables to show the error/success messages on tab switch.
        //errorElement: 'span', //default input error message container
        //errorClass: 'error-block1', // default input error message class
        //focusInvalid: false, // do not focus the last invalid input
        rules: {
            Name: {
                required: true,
                regex: /^([a-zA-Z]+\s)*[a-zA-Z]+$/,
                minlength: 3,
            },
            Email: {
                required: true,
                regex: /^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/
            },
            Tele: {
                required: true,
                regex: /([0-9]{8}$)/,
                minlength: 10,
                maxlength: 10,
            },
            Address: {
                required: true
            },
            Pin: {
                required: true,
                regex: /([0-9]{4}$)/,
            },
            City: {
                required: true,
                regex: /^([a-zA-Z]+\s)*[a-zA-Z]+$/,
            },
            State: {
                required: true
            },
            Gst_stop: {
                required: true,
                regex: /([0-9]{2}[a-zA-Z]{4}([a-zA-Z]{1}|[0-9]{1})[0-9]{4}[a-zA-Z]{1}([a-zA-Z]|[0-9]){3})/,
                minlength: 15,
                maxlength: 15
            }

        },

        messages: {
            Name: {
                required: "This is required",
                regex: "This is invalid",
                minlength: "This is invalid",
            },
            Email: {
                required: "This is required",
                regex: "This is invalid"
            },
            Tele: {
                required: "This is required",
                regex: "This is invalid",
                minlength: "This is invalid",
                maxlength: "This is invalid",
            },
            Address: {
                required: "This is required"
            },
            Pin: {
                required: "This is required",
                regex: "This is invalid",
            },
            City: {
                required: "This is required",
                regex: "This is invalid",
            },
            State: {
                required: "This is required"
            },
            Gst_stop: {
                required: "This is required",
                regex: "This is invalid",
                minlength: "This is invalid",
                maxlength: "This is invalid"
            }
        },

        errorPlacement: function (error, element) {
            //if (element.attr("name") == "typeoption") { // for uniform radio buttons, insert the after the given container
            //    error.insertAfter("#show-error");
            //}
            ////else if (element.attr("name") == "payment[]") { // for uniform checkboxes, insert the after the given container
            ////    error.insertAfter("#form_payment_error");
            ////}
            //else {
            //    error.insertAfter(element); // for other inputs, just perform default behavior
            //}
            error.insertAfter(element);
        },
        //success: function (label) {
        //    if (label.attr("for") == "typeoption") { // for checkboxes and radio buttons, no need to show OK icon
        //        label.remove(); // remove error label here
        //    }
        //},
        submitHandler: function (form) {
            var $valid = $('#billing-shipping-form').valid();
            if (!$valid) {
                $validator.focusInvalid();
                return false;
            }
            else {
                
                angular.element(document.getElementById('addresspopupmodel')).scope().submitForm();
                angular.element(document.getElementById('addresspopupmodel')).scope().$apply();
                //form.submit()
            }
            //add here some ajax code to submit your form or just call form.submit() if you want to submit the form without ajax
        }
    });

    var Orderform = $('#orderform');
    var Ordererror = $('.sm-form-control', form);
    var $Ordervalidator = $('#orderform').validate({
        //doNotHideMessage: true, //this option enables to show the error/success messages on tab switch.
        //errorElement: 'span', //default input error message container
        //errorClass: 'error-block1', // default input error message class
        //focusInvalid: false, // do not focus the last invalid input
        rules: {
            Name: {
                required: true,
                regex: /^([a-zA-Z]+\s)*[a-zA-Z]+$/,
                minlength: 3,
            },
            Email: {
                required: true,
                regex: /^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/
            },
            Tele: {
                required: true,
                regex: /([0-9]{8}$)/,
                minlength: 10,
                maxlength: 10,
            },
            Address: {
                required: true
            },
            Pin: {
                required: true,
                regex: /([0-9]{4}$)/,
            },
            City: {
                required: true,
                regex: /^([a-zA-Z]+\s)*[a-zA-Z]+$/,
            },
            State: {
                required: true
            },
            Gst_stop: {
                regex: /([0-9]{2}[a-zA-Z]{4}([a-zA-Z]{1}|[0-9]{1})[0-9]{4}[a-zA-Z]{1}([a-zA-Z]|[0-9]){3})/,
                minlength: 15,
                maxlength: 15
            }

        },

        messages: {
            Name: {
                required: "This is required",
                regex: "This is invalid",
                minlength: "This is invalid",
            },
            Email: {
                required: "This is required",
                regex: "This is invalid"
            },
            Tele: {
                required: "This is required",
                regex: "This is invalid",
                minlength: "This is invalid",
                maxlength: "This is invalid",
            },
            Address: {
                required: "This is required"
            },
            Pin: {
                required: "This is required",
                regex: "This is invalid",
            },
            City: {
                required: "This is required",
                regex: "This is invalid",
            },
            State: {
                required: "This is required"
            },
            Gst_stop: {
                regex: "This is invalid",
                minlength: "This is invalid",
                maxlength: "This is invalid"
            }
        },

        errorPlacement: function (error, element) {
            //if (element.attr("name") == "typeoption") { // for uniform radio buttons, insert the after the given container
            //    error.insertAfter("#show-error");
            //}
            ////else if (element.attr("name") == "payment[]") { // for uniform checkboxes, insert the after the given container
            ////    error.insertAfter("#form_payment_error");
            ////}
            //else {
            //    error.insertAfter(element); // for other inputs, just perform default behavior
            //}
            error.insertAfter(element);
        },
        //success: function (label) {
        //    if (label.attr("for") == "typeoption") { // for checkboxes and radio buttons, no need to show OK icon
        //        label.remove(); // remove error label here
        //    }
        //},
        submitHandler: function (form) {
            var $valid = $('#orderform').valid();
            if (!$valid) {
                $validator.focusInvalid();
                return false;
            }
            else {
                
                $.ajax({
                    url: '/Access/checkTaxation',
                    type: "Post",
                    beforeSend: function () {
                        //$("body").removeAttr("ng-app");
                       // $(".proceedshop button").attr("disabled", "disabled");
                        $.confirm({
                            backgroundDismiss: true,
                            title: false,
                            content: '<div class="checkbox" style="text-align:center;"><label style="font-size: 16px;">Processing your order... <i class="fas fa-circle-notch fa-spin"></i></label></div>',
                            buttons: false,
                            closeIcon: true,
                            closeIconClass: 'sub-dialog',

                        });
                    },
                    success: function (response) {
                        
                        if (response != '') {
                            if (response == '/login')
                            {
                                window.location.href = "/login";
                            }
                            else {
                                submitorder();
                            //    var modeofpay = $("[name=ddmodeofpayment]").val();
                            //var days = response[0].Etcstring2;
                            //if (response[0].Gst == '' || response[0].Etcstring1 == '') {
                            //    $(".jconfirm-closeIcon").click();

                            //    $.confirm({
                            //        title: 'Fill taxation',

                            //        content: '<form action="" class="form-sub mb-0"><div class="form-group">CR No. <input type="text" name="pan" placeholder="Enter CR No." maxlength="10" value="' + response[0].Etcstring1 + '"  class="sm-form-control" required />VAT No.<input   name="GST" placeholder="Enter VAT No." maxlength="15" value="' + response[0].Gst + '" class="sm-form-control mb-0" required /></div></form>',

                            //        contentLoaded: function (data, status, xhr) {
                            //            this.$content.find('input[name=pan]') = 'Some Value';
                            //            this.form.elements["element_name"].value = 'Some Value';

                            //        },
                            //        closeIcon: true,
                            //        //closeIconClass: 'sub-dialog',
                            //        buttons: {
                            //            Skip: {
                            //                text: 'Skip & place order',
                            //                btnClass: 'btn-danger',
                            //                action: function () {
                            //                    if (modeofpay == 'Credit Account') {
                            //                        $(".close-dialog, .jconfirm-closeIcon").click();
                            //                        $("#Po-button").click();

                            //                    }
                            //                    else {
                            //                        submitorder();
                            //                    }
                            //                }
                            //            },
                            //            formSubmit: {
                            //                text: 'Place Order',
                            //                btnClass: 'subsc',
                            //                action: function () {
                            //                    var pan = this.$content.find('input[name=pan]').val();
                            //                    var GST = this.$content.find('input[name=GST]').val();
                            //                    $('.jconfirm-content label.text-danger').remove();
                            //                    //alert(VAL);
                            //                    var gstcHECK = /^\d{2}[A-Za-z]{5}\d{4}[A-Za-z]\d{1}[Zz]{1}[A-Za-z\d]{1}$/i;
                            //                    var pancHECK = /^[A-Za-z]{5}\d{4}[A-Za-z]{1}$/i;
                            //                    var nn = gstcHECK.test(GST);
                            //                    //alert(email);
                            //                    if (GST == "" || pan == "") {
                            //                        this.setContentAppend('<label class="text-danger" style="font-size:11px">All fields required</label>');
                            //                    }
                            //                    else if (GST != "" && gstcHECK.test(GST) && pan != "" && pancHECK.test(pan)) {
                            //                        $.ajax({
                            //                            type: "POST",
                            //                            url: "/Access/updatetaxation",
                            //                            //data: '{Compname: "' + Compname + '", Fullname: "' + Fullname + '", Email: "' + Email + '", Mobile:"' + Mobile + '", Password: "' + Passsword + '",Type:"' + mtype + '", Bname:"' + Bname + '", Bemail:"' + Bemail + '", Bmobile:"' + Bmobile + '", Bstreet:"' + Bstreet + '", Bpin:"' + Bpin + '", Bcity:"' + Bcity + '", Bstate:"' + Bstate + '", , Sname:"' + Sname + '", Semail:"' + Semail + '", Smobile:"' + Smobile + '", Sstreet:"' + Sstreet + '", Spin:"' + Spin + '", Scity:"' + Scity + '", Sstate:"' + Sstate + '", step:"' + step + '"  }',
                            //                            data: '{P: "' + pan + '",G:"' + GST + '"}',
                            //                            contentType: "application/json; charset=utf-8",
                            //                            dataType: "json",
                            //                            beforeSend: function () {

                            //                                $(".jconfirm-closeIcon").click();
                            //                                $.dialog({
                            //                                    title: false,
                            //                                    content: '<div class="checkbox" style="text-align:center;"><label style="font-size: 15px;margin: 0;padding: 0;margin-top: 5px;color: #848484;">Updating your details...<i class="fas fa-circle-notch fa-spin"></i></label></div>',
                            //                                    closeIconClass: 'close-dialog',
                            //                                });
                            //                            },
                            //                            success: function (response) {

                            //                                //if (response == true) {
                            //                                //    ms = '<label style="font- size: 12px;margin: 0;padding: 0;margin-top: 5px;color: #848484; ">Thank You. We have received your details.</label>';
                            //                                //}
                            //                                //else {
                            //                                //    ms = '<label style="font- size: 12px;margin: 0;padding: 0;margin-top: 5px;color: #848484; ">Something went wrong. Please contact our support team.</label>';
                            //                                //}

                            //                                //$.dialog({
                            //                                //    title: false,
                            //                                //    content: ms,
                            //                                //    closeIconClass: 'close-dialog',
                            //                                //});
                            //                                setTimeout(function () {
                            //                                    $(".close-dialog, .jconfirm-closeIcon").click();
                            //                                }, 5000);

                            //                                setTimeout(function () {
                            //                                    // form.submit()
                            //                                    //if (days > '0') {
                            //                                    if (modeofpay == 'Credit Account') {
                            //                                        $(".close-dialog, .jconfirm-closeIcon").click();
                            //                                        $("#Po-button").click();

                            //                                    }
                            //                                    else {
                            //                                        submitorder();
                            //                                    }
                            //                                }, 5000);
                            //                                //setTimeout(function () {
                            //                                //    if (document.referrer != "" && document.referrer != undefined) {
                            //                                //        window.location.href = document.referrer;
                            //                                //    }
                            //                                //    else {
                            //                                //        window.location.href = "/login";
                            //                                //    }
                            //                                //}, 6000);
                            //                            },
                            //                        });
                            //                        $(".close-dialog, .jconfirm-closeIcon").click();
                            //                    }
                            //                    else if (gstcHECK.test(GST) != true) {
                            //                        this.setContentAppend('<label class="text-danger" style="font-size:11px">GST is Invalid</label>');
                            //                    }
                            //                    else if (pancHECK.test(pan) != true) {
                            //                        this.setContentAppend('<label class="text-danger" style="font-size:11px">PAN is Invalid</label>');
                            //                    }

                            //                    return false;
                            //                }
                            //            },

                            //        },
                            //        onContentReady: function () {

                            //        }
                            //    });
                            //}
                            ////else if (response[0].Etcstring2 > '0') {
                            //else if  (modeofpay == 'Credit Account'){
                            //    $(".close-dialog, .jconfirm-closeIcon").click();
                            //    $("#Po-button").click();

                            //}
                            //else {
                            //    //  form.submit();
                            //    submitorder();
                            //}
                        }
                    }
                        else {
                          //  form.submit();
                            submitorder();
                        }
                        //setTimeout(function () {
                        //    $(".jconfirm-closeIcon").click();
                        //}, 800);
                         
                    }
                });
                //angular.element(document.getElementById('addresspopupmodel')).scope().submitForm();
               // angular.element(document.getElementById('addresspopupmodel')).scope().$apply();
                //form.submit()
            }
            //add here some ajax code to submit your form or just call form.submit() if you want to submit the form without ajax
        }
    });


    var Orderform = $('#PO-Form');
    var Ordererror = $('.sm-form-control', form);
    var $Ordervalidator = $('#PO-Form').validate({
        //doNotHideMessage: true, //this option enables to show the error/success messages on tab switch.
        //errorElement: 'span', //default input error message container
        //errorClass: 'error-block1', // default input error message class
        //focusInvalid: false, // do not focus the last invalid input
        rules: {
            PONum: {
                required: true,
                minlength: 1,
            },
            PODate: {
                required: true,
            },
            PoAttach: {
                required: function () {
                    if ($("[name='Mode']").val() != "2") {
                        return false;
                    }
                    else {
                        return true;
                    }
                },
            },
            Mode: {
                required: true
            }
        },

        messages: {
            PONum: {
                required: "This is required",
                minlength: "This is invalid",
            },
            PODate: {
                required: "This is required"
            },
            PoAttach: {
                required: "This is required",
            },
            Mode: {
                required: "This is required"
            }
        },

        errorPlacement: function (error, element) {
            //if (element.attr("name") == "typeoption") { // for uniform radio buttons, insert the after the given container
            //    error.insertAfter("#show-error");
            //}
            ////else if (element.attr("name") == "payment[]") { // for uniform checkboxes, insert the after the given container
            ////    error.insertAfter("#form_payment_error");
            ////}
            //else {
            //    error.insertAfter(element); // for other inputs, just perform default behavior
            //}
            error.insertAfter(element);
        },
        //success: function (label) {
        //    if (label.attr("for") == "typeoption") { // for checkboxes and radio buttons, no need to show OK icon
        //        label.remove(); // remove error label here
        //    }
        //},
        submitHandler: function (form) {
            var $valid = $('#PO-Form').valid();
            if (!$valid) {
                $validator.focusInvalid();
                return false;
            }
            else {
                
        
                //angular.element(document.getElementById('addresspopupmodel')).scope().submitForm();
                // angular.element(document.getElementById('addresspopupmodel')).scope().$apply();
                //form.submit()
                submitorder();
                return false;
            }
            //add here some ajax code to submit your form or just call form.submit() if you want to submit the form without ajax
        }
    });
});

//submit finction

function submitrequest() {
    

    //var Pid = $('.skurange.active').attr('id');
    //if (Pid == undefined) {
    //    Pid = $('.sampleskurange').attr('id');
    //}
    // var qty = $('.pack-box').val();
    var Pid = 0;
    
    var ty = $('.side-req h4').text();
    if (ty == 'Get Quote/Notify') { ty = 'Quote' } else { ty = 'Sample' }
    var na = $('.side-req #Name').val();
    var mo = $('.side-req #Mobile').val();
    var Em = $('.side-req #Email').val();
    var or = $('.side-req #Organisation').val();
    var ad = $('.side-req #Address').val();
    var ci = $('.side-req #City').val();
    var pi = $('.side-req #Pin').val();
    var com = $('.side-req #Comment').val();
    var rdd = $('.side-req #redirect').val();
    var qty = $('.side-req #schqty_req').val();
    var req = $('.side-req #schtype_req').val();
    var code = $('.side-req #pdcode').text();
    var pname = "Cart";
    $.ajax({
        url: '/Access/RequestB',
        type: "Post",
        beforeSend: function () {
            $(".req-btn").attr("disabled", "disabled");
            //$(".close-dialog").click();
            $.dialog({
                title: false,
                content: '<div class="checkbox" style="text-align:center;"><label style="font-size: 17px;margin: 0;padding: 0;margin-top: 5px;color: #848484;">Sending Request... <i class="fas fa-circle-notch fa-spin"></i></label></div>',
                closeIconClass: 'close-dialog',
            });
        },
        data: { t: ty, n: na, m: mo, e: Em, o: or, a: ad, c: ci, p: pi, cmm: com, rd: rdd, pi: Pid, co: code, pn: pname },
        success: function (response) {
            //$('#reqest-form').clear();
            $(".close-dialog").click();
            $(".req-btn").removeAttr("disabled");
            if (response == "Redirect"){
                window.location.href = "/login";
            }
            

            //$('#side-panel-trigger-close').click();
            //$.confirm({
            //    backgroundDismiss: false,
            //    title: false,
            //    content: '<div class="checkbox" style="text-align:center;"><label style="font-size: 15px;">We have received your request, we will get back to you soon.</label></div>',
            //    buttons: false
            //});
            //setTimeout(function () {
            //    $('.jconfirm-closeIcon').click();
            //}, 5000);
            setTimeout(function () { location.reload(); }, 500);

        }
    });
    //  return false;// if it's a link to prevent post
}



var x = "false";

function submitorder() {
    var formData = new FormData();
    formData.append('ODComment', $("[name=ODComment]").val());
    formData.append('ddmodeofpayment', $("[name=ddmodeofpayment]").val());
    formData.append('PONum', $("[name=PONum]").val());
    formData.append('PODate', $("[name=PODate]").val());
 //   formData.append('PoAttach', 'previewImg');
    // Attach file
    formData.append('PoAttach', $('input[type=file]')[0].files[0]); 
    if (x == "false") {
        $.ajax({
            type: "POST",
            url: "/placeorder",
            //data: '{Compname: "' + Compname + '", Fullname: "' + Fullname + '", Email: "' + Email + '", Mobile:"' + Mobile + '", Password: "' + Passsword + '",Type:"' + mtype + '", Bname:"' + Bname + '", Bemail:"' + Bemail + '", Bmobile:"' + Bmobile + '", Bstreet:"' + Bstreet + '", Bpin:"' + Bpin + '", Bcity:"' + Bcity + '", Bstate:"' + Bstate + '", , Sname:"' + Sname + '", Semail:"' + Semail + '", Smobile:"' + Smobile + '", Sstreet:"' + Sstreet + '", Spin:"' + Spin + '", Scity:"' + Scity + '", Sstate:"' + Sstate + '", step:"' + step + '"  }',
            data: formData,

            // data: '{form: "' + formData +'"}',
            contentType: false,
            processData: false,
            beforeSend: function () {
                x = "true";
                $('.checkoutbtn').attr('type', 'button').attr('disabled','disabled');
                $(".jconfirm-closeIcon").click();
                $.dialog({
                    title: false,
                    content: '<div class="checkbox" style="text-align:center;"><label style="font-size: 15px;margin: 0;padding: 0;margin-top: 5px;color: #848484;">Updating your details...<i class="fas fa-circle-notch fa-spin"></i></label></div>',
                    closeIconClass: 'close-dialog',
                });
            },
            success: function (response) {

                if (response == "/Paynow") {
                    window.location.href = "/Paynow";
                }
                else {
                    $.dialog({
                        title: false,
                        content: '<div class="checkbox" style="text-align:center;"><label style="font-size: 15px;margin: 0;padding: 0;margin-top: 5px;color: #848484;">Thank You. We have received your order details.<i class="fas fa-circle-notch fa-spin"></i></label></div>',
                        closeIconClass: 'close-dialog',
                    });
                    $(".jconfirm-closeIcon").click();
                    setTimeout(function () {
                        // form.submit()


                        //  alert('Thank You. We have received your details.');
                        window.location.href = response;//"/";
                    }, 4000);

                }
                //if (response == true) {
                //    ms = '<label style="font- size: 12px;margin: 0;padding: 0;margin-top: 5px;color: #848484; ">Thank You. We have received your details.</label>';
                //}
                //else {
                //    ms = '<label style="font- size: 12px;margin: 0;padding: 0;margin-top: 5px;color: #848484; ">Something went wrong. Please contact our support team.</label>';
                //}

                $.dialog({
                    title: false,
                    content: ms,
                    closeIconClass: 'close-dialog',
                });
                setTimeout(function () {
                    $(".close-dialog, .jconfirm-closeIcon").click();
                }, 5000);

                setTimeout(function () {
                    // form.submit()
                }, 5000);
                //setTimeout(function () {
                //    if (document.referrer != "" && document.referrer != undefined) {
                //        window.location.href = document.referrer;
                //    }
                //    else {
                //        window.location.href = "/login";
                //    }
                //}, 6000);
            },
        });
    }
}

function convertNumberToWords(amount) {
    var words = new Array();
    words[0] = '';
    words[1] = 'One';
    words[2] = 'Two';
    words[3] = 'Three';
    words[4] = 'Four';
    words[5] = 'Five';
    words[6] = 'Six';
    words[7] = 'Seven';
    words[8] = 'Eight';
    words[9] = 'Nine';
    words[10] = 'Ten';
    words[11] = 'Eleven';
    words[12] = 'Twelve';
    words[13] = 'Thirteen';
    words[14] = 'Fourteen';
    words[15] = 'Fifteen';
    words[16] = 'Sixteen';
    words[17] = 'Seventeen';
    words[18] = 'Eighteen';
    words[19] = 'Nineteen';
    words[20] = 'Twenty';
    words[30] = 'Thirty';
    words[40] = 'Forty';
    words[50] = 'Fifty';
    words[60] = 'Sixty';
    words[70] = 'Seventy';
    words[80] = 'Eighty';
    words[90] = 'Ninety';
    amount = amount.toString();
    var atemp = amount.split(".");
    var number = atemp[0].split(",").join("");
    var n_length = number.length;
    var words_string = "";
    if (n_length <= 9) {
        var n_array = new Array(0, 0, 0, 0, 0, 0, 0, 0, 0);
        var received_n_array = new Array();
        for (var i = 0; i < n_length; i++) {
            received_n_array[i] = number.substr(i, 1);
        }
        for (var i = 9 - n_length, j = 0; i < 9; i++ , j++) {
            n_array[i] = received_n_array[j];
        }
        for (var i = 0, j = 1; i < 9; i++ , j++) {
            if (i == 0 || i == 2 || i == 4 || i == 7) {
                if (n_array[i] == 1) {
                    n_array[j] = 10 + parseInt(n_array[j]);
                    n_array[i] = 0;
                }
            }
        }
        value = "";
        for (var i = 0; i < 9; i++) {
            if (i == 0 || i == 2 || i == 4 || i == 7) {
                value = n_array[i] * 10;
            } else {
                value = n_array[i];
            }
            if (value != 0) {
                words_string += words[value] + " ";
            }
            if ((i == 1 && value != 0) || (i == 0 && value != 0 && n_array[i + 1] == 0)) {
                words_string += "Crores ";
            }
            if ((i == 3 && value != 0) || (i == 2 && value != 0 && n_array[i + 1] == 0)) {
                words_string += "Lakhs ";
            }
            if ((i == 5 && value != 0) || (i == 4 && value != 0 && n_array[i + 1] == 0)) {
                words_string += "Thousand ";
            }
            if (i == 6 && value != 0 && (n_array[i + 1] != 0 && n_array[i + 2] != 0)) {
                words_string += "Hundred and ";
            } else if (i == 6 && value != 0) {
                words_string += "Hundred ";
            }
        }
        words_string = words_string.split("  ").join(" ");
    }
    return words_string;
}

