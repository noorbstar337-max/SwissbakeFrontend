//var app = angular.module('MyApp', [])

app.controller('myAddress', function ($scope, $http, $window) {
    
    //$scope.add = [{
    //    Address: "",
    //    City: "",
    //    CompName: "",
    //    Country: "",
    //    Email: "",
    //    Etcstring1: "",
    //    Gst: "",
    //    Id: "",
    //    Name: "",
    //    Pin: "",
    //    State: "",
    //    Telephone: ""
    //}];

    

    $scope.load = function () {
        $http({
            method: "GET",
            url: "/access/GetShipAddress",
            dataType: 'json',
            //data: customer,
            //headers: { "Content-Type": "application/json" }
        }).then(function (response) {
            
            if (response.status == 200) {
                $scope.Address = response.data;
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
            data: '{adid : "'+ Id +'"}',
            //headers: { "Content-Type": "application/json" }
        }).then(function (response) {
            

            var j = JSON.stringify(response.data);

            if (response.status == 200) {
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


    $scope.deleteAdd = function (Id, index) {

        $.confirm({
            title: 'Delete subscription?',
            //theme: 'supervan',
            //content: '' +
            //'<form action="" class="formName">' +
            //'<div class="form-group">' +
            //'<label>Enter OTP Code</label>' +
            //'<input type="text" placeholder="OTP" class="otp form-control" required />' +
            //'resend (<span class="Counter"></span>)'+
            //'</div>' +
            //'</form>',
            closeIcon: true,
            closeIconClass: 'close-verify',
            buttons: {
                formSubmit: {
                    text: 'Confirm',
                    btnClass: 'btn-blue',
                    action: function () {
                        var ms = "";
                        var res = "";
                        $http({
                            method: "POST",
                            url: "/access/DeleteShipAddr",
                            dataType: 'json',
                            data: '{adid : "' + Id + '"}',
                            //headers: { "Content-Type": "application/json" }
                        }).then(function (response) {

                            if (response == "true") {

                            }
                            $scope.Address.splice(index, 1);
                            $(".jconfirm-closeIcon").click();
                            $.confirm({
                                backgroundDismiss: false,
                                title: false,
                                closeIcon: true,
                                closeIconClass: 'close-verify',
                                content: '<div class="checkbox" style="text-align:center;"><label style="font-size: 21px;">Deleted Successfully <i class="fas fa-check-circle success"></i></label></div>',
                                buttons: false
                            });
                            setTimeout(function () {
                                $(".jconfirm-closeIcon").click();
                                //$('#datatable1').dataTable({
                                //"destroy": true,
                                //"responsive": true,
                                //"oLanguage": {
                                //    "sEmptyTable": "No record found"
                                //}
                                //}).clear().draw();
                            }, 1500);
                            //var j = JSON.stringify(response.data);

                            //if (response.status == 200) {
                            //    $scope.add = response.data[0];
                            //    $scope.mode = "2"
                            //    var d = response.data[0].State;



                            //    $scope.IsVisible = $scope.IsVisible ? false : true;
                            //    $scope.IsHidden = $scope.IsHidden ? false : true;
                            //    setTimeout(function () {
                            //        $("#State option[value=" + d + "]").attr("selected", "selected");
                            //    }, 200);
                            //}

                        });

                        return false;
                    }
                },
                cancel: function () {
                    //close
                },
            },
            onContentReady: function () {
                // bind to events
                var jc = this;
                this.$content.find('form').on('submit', function (e) {
                    // if the user submits the form by pressing enter in the field.
                    e.preventDefault();
                    jc.$$formSubmit.trigger('click'); // reference the button and click it
                });
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




    $scope.Paynow = function (index, code, amount, id) {
        alert("Order Code:" + code + "Order Amount: " + amount + "Order Id: " + id + "Index:" + index);
    }

    
    

});



$(document).ready(function () {
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
            Gst: {
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
            Gst: {
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
                angular.element(document.getElementById('content')).scope().submitForm();
                angular.element(document.getElementById('content')).scope().$apply();
                //form.submit()
            }
            //add here some ajax code to submit your form or just call form.submit() if you want to submit the form without ajax
        }
    });
});