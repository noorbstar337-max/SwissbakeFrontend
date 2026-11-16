$(document).ready(function () {
    var distance2 = $('.tab-buttons').offset().top;
    var tb1 = $('#tab1').offset().top;
    //var tb2 = $('#tab2').offset().top;
    var tb3 = $('#tab3').offset().top;
    var tb4 = $('#tab4').offset().top;
    //var tb5 = $('#tab5').offset().top;
    //var tb6 = $('#tab5').offset().top;
    //var formcart = $('form.cart').offset().top;
    $(window).scroll(function () {
            if ($(this).scrollTop() >= distance2 - 100) {
                $('.tab-buttons').insertAfter('#tab-btn span');

            }
            else {
                $('.tab-buttons').insertAfter('.in-tab-btn');
            }

            if ($(this).scrollTop() >= tb1 - 200) {
                $(".curr").removeClass("curr");
                $("#tb-desc").addClass("curr");
            }
            //if ($(this).scrollTop() >= tb2 - 200) {
            //    $(".curr").removeClass("curr");
            //    $("#tb-nut").addClass("curr");
            //}
            if ($(this).scrollTop() >= tb3 - 200) {
                $(".curr").removeClass("curr");
                $("#tb-Rec").addClass("curr");
            }
            if ($(this).scrollTop() >= tb4 - 200) {
                $(".curr").removeClass("curr");
                $("#tb-faq").addClass("curr");
            }
            //if ($(this).scrollTop() >= tb5 - 200) {
            //    $(".curr").removeClass("curr");
            //    $("#tb-rev").addClass("curr");
            //}
            //if ($(this).scrollTop() >= formcart - 300) {
            //    var top = $(this).scrollTop();
            //    if ($('#tab1').visible(true)){

            //    }
            //    else {
            //        $("form.cart").css("top", top);
            //    }

            //}
            if ($(this).scrollTop() <= tb1 - 200) {
                $(".curr").removeClass("curr");
                $("#tb-prod").addClass("curr");
            }
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
                //required:true
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
                //required: true
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
                //required: "This is required"
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
                //required: "This is required"
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



    // Code for the Validator
    var form1 = $('#subs-form');
    var error1 = $('.form-control', form1);

    var $validator1 = $('#subs-form').validate({
        //doNotHideMessage: true, //this option enables to show the error/success messages on tab switch.
        //errorElement: 'span', //default input error message container
        //errorClass: 'error-block1', // default input error message class
        //focusInvalid: false, // do not focus the last invalid input
        rules: {
            schdate: {
                required: true
            },
            schtype: {
                required: true
            },
            schqty: {
                required: true
            }

        },

        messages: {
            schdate: {
                required: "This is required"
            },
            schtype: {
                required: "This is required"
            },
            schqty: {
                required: "This is required"
            }
        },

        errorPlacement: function (error1, element) {
            //if (element.attr("name") == "typeoption") { // for uniform radio buttons, insert the after the given container
            //    error.insertAfter("#show-error");
            //}
            ////else if (element.attr("name") == "payment[]") { // for uniform checkboxes, insert the after the given container
            ////    error.insertAfter("#form_payment_error");
            ////}
            //else {
            //    error.insertAfter(element); // for other inputs, just perform default behavior
            //}
            error1.insertAfter(element);
        },
        //success: function (label) {
        //    if (label.attr("for") == "typeoption") { // for checkboxes and radio buttons, no need to show OK icon
        //        label.remove(); // remove error label here
        //    }
        //},
        submitHandler: function (form1) {
            var $valid = $('#subs-form').valid();
            if (!$valid) {
                $validator1.focusInvalid();
                return false;
            }
            else {
                //if ($("input[name=mode]").val() == "login") {
                //    $("input[name=login]").val("Logging...").attr("disabled", "disabled");
                //    var obj = {};

                //    obj.mail = $("input[name=Email]").val();
                //    obj.pas = $("input[name=pswd]").val();
                //    obj.me = "";
                //    if ($('input[name=rememberme]').is(":checked")){
                //        obj.me = "1";
                //    }

                //    return checklog(obj.mail, obj.pas, obj.me);
                //}
                //else {
                //    return update();
                //}
                //alert("done")
                addSchedule()
                return false;
            }
            //add here some ajax code to submit your form or just call form.submit() if you want to submit the form without ajax
        }
    });

    

    //dropdown on  click //
    $(".dropdown_click .selected").on('click', function () {
        $(".dropdown_click .drop-content ul").slideToggle();
    });

    $(".dropdown_click .drop-content ul li span").on('click', function () {
        // var bindText = $(this).html();
        $(".dropdown_click .selected  span").html($(this).html());
        $(".dropdown_click .drop-content ul").slideUp();
    }); 


    addQtyBtn();
    






});


function addQtyBtn() {
    $('<div class="quantity-nav"><div class="quantity-button quantity-up">+</div><div class="quantity-button quantity-down">-</div></div>').insertAfter('.quantity input');
    $('.quantity').each(function () {
        var spinner = jQuery(this),
            input = spinner.find('input[type="number"]'),
            btnUp = spinner.find('.quantity-up'),
            btnDown = spinner.find('.quantity-down'),
            min = input.attr('min'),
            max = input.attr('max');

        btnUp.click(function () {
            var oldValue = parseFloat(input.val());
            if (oldValue >= max) {
                var newVal = oldValue;
            } else {
                var newVal = oldValue + 1;
            }
            spinner.find("input").val(newVal);
            spinner.find("input").trigger("change");
        });

        btnDown.click(function () {
            var oldValue = parseFloat(input.val());
            if (oldValue <= min) {
                var newVal = oldValue;
            } else {
                var newVal = oldValue - 1;
            }
            spinner.find("input").val(newVal);
            spinner.find("input").trigger("change");
        });

    });
}



//var app = angular.module('MyApp', ['rzModule']);
function addSchedule() {

    var formData = new FormData();
    //formData.append('SubId', $("form submit[type=submit]").val());
    formData.append('Type', $(".side-sch form .sch-type").val());
    formData.append('PId', $(".side-sch form #pid").val());
    formData.append('Qty', $(".side-sch form .sch-qty").val());
    formData.append('ProName', $("#protitle").html());

    formData.append('Edit', "");
    //if ($(".side-sch form .sch-type").val() == "Custom") {
    //    formData.append('Date', $(".side-sch form .sch-date").val());
    //}
    //else {
    //    formData.append('Date', "");
    //}

    formData.append('Date', $(".side-sch form .sch-date").val());

    $.ajax({
        type: "POST",
        url: "/Access/Subs",
        //data: '{Compname: "' + Compname + '", Fullname: "' + Fullname + '", Email: "' + Email + '", Mobile:"' + Mobile + '", Password: "' + Passsword + '",Type:"' + mtype + '", Bname:"' + Bname + '", Bemail:"' + Bemail + '", Bmobile:"' + Bmobile + '", Bstreet:"' + Bstreet + '", Bpin:"' + Bpin + '", Bcity:"' + Bcity + '", Bstate:"' + Bstate + '", , Sname:"' + Sname + '", Semail:"' + Semail + '", Smobile:"' + Smobile + '", Sstreet:"' + Sstreet + '", Spin:"' + Spin + '", Scity:"' + Scity + '", Sstate:"' + Sstate + '", step:"' + step + '"  }',
        data: formData,
        contentType: false,
        processData: false,
        beforeSend: function () {
            $(".sub-btn").attr("disabled", "disabled");
            $.confirm({
                backgroundDismiss: false,
                title: false,
                content: '<div class="checkbox" style="text-align:center;"><label style="font-size: 21px;">Scheduling... <i class="fas fa-circle-notch fa-spin"></i></label></div>',
                buttons: false
            });
            //_divPartialView.style = "Display: none";
            //$("#divWaiting").removeClass("display-none");
        },
        success: function (response) {
            if (response == true) {
                setTimeout(function () {
                    location.href = "/thankyou"
                }, 600);
            }
            else {
                $(".req-btn").removeAttr("disabled");
            }
            setTimeout(function () {
                $(".close-dialog").click();
                $(".jconfirm-closeIcon").click();
            }, 500);
        },
    }).done(function (response) {
        true;
    }).fail(function () {
        false;
    });
}

function get(url, m,id) {
    
    //var id = '@Session["U_Id"]';
    if (id == '' || id == null) {
        id = 0;
    }
    $('.skurange').removeClass('active');
    $('.skurange#' + url).addClass('active');
    $.ajax({
        url: '/Access/Productdetail',
        type: "Post",
        beforeSend: function () {
            //$("body").removeAttr("ng-app");
            $('.la > div').remove();
            $.confirm({
                backgroundDismiss: true,
                title: false,
                content: '<div class="checkbox" style="text-align:center;"><label style="font-size: 16px;">Calculating Exclusive Pricing... <i class="fas fa-circle-notch fa-spin"></i></label></div>',
                buttons: false
            });
        },
        data: { u: url, u_id: id, mode: m },
        success: function (response) {
            if (response == '/login') {
                window.location.href = "/login";

            }
            else {
            var cnt = $("#cont").val();
            $(".la").append("<div></div>")
            $(".la > div").attr("id", "divpricing" + cnt);

            $("#divpricing" + cnt).html(response).removeAttr("style");



            //   //setTimeout(function () {
            //angular.element( document).ready( function () {
            //    angular.bootstrap(document, ['MyApp']);
            //     });
            //   //}, 1000);

            //  $("#_ProductDetails").html(response.data); 



            //  angular.element(document).scope().$destroy();
            //$("body").attr("ng-app", "MyApp");
            //app = angular.module('MyApp', ['rzModule']);
            //setTimeout(function () {
            app = angular.bootstrap(document.getElementById('divpricing' + cnt), ['MyApp']);
            $("#cont").val(cnt + 1);
            $(".la").removeAttr("id");
            if ($("#buysample").hasClass("d-none")) {
                
            }
            else {
                addQtyBtn();
            }
            
            setTimeout(function () {
                $(".jconfirm-closeIcon").click();
            }, 800);
            //},5000);


            //     if (cnt == ""){

            //angular.bootstrap(document, ['MyApp']);
            //         $("#cont").val("stop");

            //     }
            //     else {
            //         //angular.resumeBootstrap();
            //        // angular.resumeBootstrap([app['MyApp']]);
            //         angular.bootstrap(document.getElementById('single'), ['MyApp']);

            //     }

            //     angular.element(document.getElementById('single')).scope().$apply();

            // var app = angular.module('MyApp', ['rzModule']);

            //   $("#divpricing .product").removeAttr("style");
            //    window.history.pushState("state", "title", url);
        }
        }
    });
    //  return false;// if it's a link to prevent post
}







function scrllto(a) {
    
    //var tb1 = $('#tab1').offset().top;
    ////var tb2 = $('#tab2').offset().top;
    //var tb3 = $('#tab3').offset().top;
    //var tb4 = $('#tab4').offset().top;
    //var tb5 = $('#tab5').offset().top;
    if (a == "product") {
        $('html, body').animate({
            'scrollTop': 0
        });
    }
    if (a == "tb2"){
        $('html, body').animate({
            'scrollTop': $("#tab1").position().top - 150
        });
    }
    //if (a == "tb3") {
    //    $('html, body').animate({
    //        'scrollTop': $("#tab2").position().top - 150
    //    });
    //}
    if (a == "tb4") {
        $('html, body').animate({
            'scrollTop': $("#tab3").position().top - 150
        });
    }
    if (a == "tb5") {
        $('html, body').animate({
            'scrollTop': $("#tab4").position().top - 150
        });
    }
    //if (a == "tb6") {
    //    $('html, body').animate({
    //        'scrollTop': $("#tab5").position().top - 150
    //    });
    //}
    
}


angular.module('MyApp', ['rzModule']).controller('single', function ($scope, $rootScope,$http) {
    $scope.slider = {
        value: 1,
        options: {
            floor: 1,
            ceil: 1000,
            step: 1,
            minRange: 1,
            maxRange: 1000,
            showSelectionBar: true,
            getSelectionBarColor: function (value) {
                if (value <= 3)
                    return 'red';
                if (value <= 6)
                    return 'orange';
                if (value <= 9)
                    return 'yellow';
                return '#2AE02A';
            },
            
            translate: function (value, sliderId, label) {
                switch (label) {
                    default:
                        return 'Quantity ' + value
                }
            },
            hidePointerLabels: true,
            onChange: function (id, value) {
                $scope.pack = value;
                if (value <= 300){
                    $scope.price = 250.75;
                    $scope.discount = 250.75 * 10 / 100
                }
                else if (value <= 650){
                    $scope.price = 230;
                    $scope.discount = 230.00 * 15 / 100
                }
                else if (value <= 999) {
                    $scope.price = 180;
                    $scope.discount = 180.00 * 18 / 100
                }
            },
        }
    };
    $scope.pack = 1;
    $scope.price = $scope.sku;
    $scope.packing = function (sku) {
        
      //  $scope.pack = Math.ceil(pack / sku) * sku;
        $scope.ttlsku = sku;
    }
    $scope.maketotal = function (value, value2) {
        
        if ($scope.pack == null || $scope.pack == 0) {
            $scope.pack = 1;
        }
        if ($scope.pack > 0){
            //$scope.slider.value = $scope.pack;
            //$scope.packchange = $scope.pack;divpricing
            $scope.price = value2;
            $scope.ttlsku = value;
            //$scope.pack = value;
            $scope.total = Math.ceil($scope.price * value);
            //$scope.discount = value2 * $scope.pack
            //    if ($scope.pack <= 300) {
            //        $scope.price = value2;
            //        $scope.discount = value2 * $scope.pack
            //}
            //    else if ($scope.pack <= 650) {
            //    $scope.price = 230;
            //    $scope.discount = 230.00 * 15 / 100
            //}
            //    else if ($scope.pack <= 999) {
            //    $scope.price = 180;
            //    $scope.discount = 180.00 * 18 / 100
            //    }
            //if ($scope.pack >= 1000) {
            //    $scope.slider.value = 1000;
            //    $scope.pack = 1000;
            //    $scope.price = 180;
            //}
        }
        else {
            $scope.pack = 1;
        }
        if($scope.pack == 0) {
            $scope.slider.value = 1;
            $scope.pack = 1;
        }
    }

    $scope.prc = function () {
        
        $scope.total = $scope.pr
    }




    $scope.selectrange = function (pack, rate, sku) {
        
        $scope.pack = Math.ceil(pack / sku);
        var sku = Math.ceil(pack / sku) * sku;
        //alert(value)
        $scope.maketotal(sku, rate);
    }

    $scope.selectpack = function (value) {
        
        $scope.pack = value;
        //alert(value)
        $scope.maketotal();
    }


    $scope.addcarting = function (Pid, qty, btn) {
        
        $http({
            method: "POST",
            url: "/Access/addcart",
           
            data: { "D": Pid, "Q": qty, "A": btn }
            //data: customer,
            //headers: { "Content-Type": "application/json" }
        }).then(function (response) {
            
            if (response.status == 200) {
                if (response.data.includes("Minimum Order") || response.data.includes("sample")) {
                    alert(response.data);
                } else {
                    if (response.data == '/login') {
                        window.location.href = "/login";

                    }
                    else {
                    // $rootScope.$emit('addcarton', response.data);
                    angular.element(document.getElementById('side-cart')).scope().$emit('addcarton', response.data);
                    //  $('.skurange').removeClass('active');
                    $('#addcart').addClass('d-none');
                    $('#buynow').addClass('d-none');
                    $('#gocart').removeClass('d-none');
                    $('#gocart').addClass('buy-now');
                    if (btn == 'buy') {

                        window.location.href = "/cart";
                    }
                }
            }
            }

        });

        //$http({
        //    url: "/Access/addcart",
        //    method: "POST",            
        //    data: {"D": Pid, "Q": qty, "A": btn}
        //}).then(function successCallback(response) {
        //    // this callback will be called asynchronously
        //    // when the response is available
        //  // $scope.cart = response.data;
        //   $rootScope.$broadcast('addcart', response.data);
        //    },

        //    function errorCallback(response) {
        //    // called asynchronously if an error occurs
        //    // or server returns response with an error status.
        //    $scope.error = response.statusText;
        //});
    }
    $scope.addcartingnew = function (Pid, kg, qty, btn) {

        $http({
            method: "POST",
            url: "/Access/addcart",

            data: { "D": Pid, "Q": qty, "A": btn }
            //data: customer,
            //headers: { "Content-Type": "application/json" }
        }).then(function (response) {

            if (response.status == 200) {
                if (response.data.includes("Minimum Order") || response.data.includes("sample")) {
                    alert(response.data);
                } else {
                    if (response.data == '/login') {
                        window.location.href = "/login";

                    }
                    else {
                        // $rootScope.$emit('addcarton', response.data);
                        angular.element(document.getElementById('side-cart')).scope().$emit('addcarton', response.data);
                        //  $('.skurange').removeClass('active');
                        $('#addcart').addClass('d-none');
                        $('#buynow').addClass('d-none');
                        $('#gocart').removeClass('d-none');
                        $('#gocart').addClass('buy-now');
                        if (btn == 'buy') {

                            window.location.href = "/cart";
                        }
                    }
                }
            }

        });

        //$http({
        //    url: "/Access/addcart",
        //    method: "POST",            
        //    data: {"D": Pid, "Q": qty, "A": btn}
        //}).then(function successCallback(response) {
        //    // this callback will be called asynchronously
        //    // when the response is available
        //  // $scope.cart = response.data;
        //   $rootScope.$broadcast('addcart', response.data);
        //    },

        //    function errorCallback(response) {
        //    // called asynchronously if an error occurs
        //    // or server returns response with an error status.
        //    $scope.error = response.statusText;
        //});
    }
});
angular.module('MyApp2', ['rzModule']).controller('yield', function ($scope, $rootScope, $http) {
    
    //  $rootScope.poshipaddr = $scope.filladdr;


    $scope.loadi = function () {
        
        //var name = $('.skurange.active span').text();
        var Pid = $('.skurange.active').attr('id');
        $http({
            method: "POST",
            url: "/access/Getyield",
            dataType: 'json',
            data: '{pid : "' + Pid + '"}',
            //headers: { "Content-Type": "application/json" }
        }).then(function (response) {

            if (response.status == 200 && response.data != "" && response.data != "/ login") {
                
                // $scope.selecteid = $rootScope.selecteid1;
                $scope.Yielddata = response.data;

                //   var oldvalue = $('#lblhidden' + index).val();
                //;debugger
                var i = 0;
                var totalweight = 0;
                var totalprice = 0;
                var unitweight = response.data[0].unit;
                var amount = 0;
                //   var ratio = response.data[0].Qty / oldvalue;
                response.data.forEach(function (prod) {

                    //   prod.Qty = ratio * $('#lblhidden' + i).val();
                  //  prod.Amount = prod.Product_Rate * (prod.Qty / 1000);
                    prod.Amount = parseFloat(prod.Product_Rate * (prod.Qty / 1000)).toFixed(2);
                    i = i + 1;
                    totalweight = totalweight + prod.Qty;
                    //totalprice = totalprice + prod.Amount;
                    totalprice = parseFloat(totalprice) + parseFloat(prod.Amount);
                });
                totalprice = parseFloat(totalprice).toFixed(2);
                $scope.totalprice = totalprice;
                var sctotalweight = parseFloat(totalweight / 1000).toFixed(2);
                var scunitweight = parseFloat(unitweight).toFixed(2);//totalweight / $scope.noofunits;
                var scnoofunits = parseFloat(totalweight / unitweight).toFixed(2);
                var scunitprice = parseFloat(totalprice / scnoofunits).toFixed(2);



                $scope.totalweight = sctotalweight;
                $scope.unitweight = scunitweight;
                $scope.noofunits = scnoofunits;
                $scope.unitprice = scunitprice;

                $scope.Htotalweight = sctotalweight;
                $scope.Hnoofunits = scnoofunits;


                //$scope.totalweight = parseFloat(totalweight / 1000).toFixed(2);
                //$scope.unitweight = parseFloat(unitweight).toFixed(2);//totalweight / $scope.noofunits;
                //$scope.noofunits = parseFloat(totalweight / unitweight).toFixed(2);
                //$scope.unitprice = parseFloat(totalprice / $scope.noofunits).toFixed(2);
            }

        });
    }
    $scope.loadi();
    $scope.onChange = function (collection, item, index) {
        loader();
        

        var oldvalue = $('#lblhidden' + index).val();
        var i = 0;
        var totalweight = 0;
        var totalprice = 0;
        //var unitweight = item.unit;
        var unitweight = $('#txtunitwt').val();
        var amount = 0;
        var ratio = item.Qty / oldvalue;
        collection.forEach(function (prod) {
            if (i != index) {
                //   prod.Qty = ratio * $('#lblhidden' + i).val();
                var xy = parseFloat(parseFloat(ratio) * parseFloat($('#lblhidden' + i).val())).toFixed(2);
                prod.Qty = xy;//ratio * $('#lblhidden' + i).val();
            }
            //if (prod.Product_Id === item.Product_Id) {
            //    
            //    if (prod.Qty == 0 || prod.Qty == null) {
            //        prod.Qty = 1;
            //    }


            //}

            prod.Amount = parseFloat(prod.Product_Rate * (prod.Qty / 1000)).toFixed(2);

            i = i + 1;
            totalweight = parseFloat(totalweight) + parseFloat(prod.Qty);
            totalprice = parseFloat(totalprice) + parseFloat(prod.Amount);
        });

        totalweight = parseFloat(totalweight).toFixed(2);
        totalprice = parseFloat(totalprice).toFixed(2);
        $scope.totalprice = totalprice;
        //$scope.totalweight = totalweight / 1000;
        //$scope.unitweight = unitweight;//totalweight / $scope.noofunits;
        //$scope.noofunits = totalweight / unitweight;
        //$scope.unitprice = totalprice / $scope.noofunits;

        //$scope.totalweight = parseFloat(totalweight / 1000).toFixed(2);
        //$scope.unitweight = parseFloat(unitweight).toFixed(2);//totalweight / $scope.noofunits;
        //$scope.noofunits = parseFloat(totalweight / unitweight).toFixed(2);
        //$scope.unitprice = parseFloat(totalprice / $scope.noofunits).toFixed(2);


        var sctotalweight = parseFloat(totalweight / 1000).toFixed(2);
        var scunitweight = parseFloat(unitweight).toFixed(2);//totalweight / $scope.noofunits;
        var scnoofunits = parseFloat(totalweight / unitweight).toFixed(2);
        var scunitprice = 0;
        if (scnoofunits == 0) {
            scunitprice = 0;
        }
        else {
            var scunitprice = parseFloat(totalprice / scnoofunits).toFixed(2);
        }
        $scope.totalweight = sctotalweight;
        $scope.unitweight = scunitweight;
        $scope.noofunits = scnoofunits;
        $scope.unitprice = scunitprice;
    }

    $scope.onYieldChange = function (collection, item, index) {
        loader();
        
        //  var oldvalue = $('#lblhidden' + index).val();
        var oldupdate = $('#lblnoofunits').val();
        var oldvalue = $('#lbltotalyield').val();
        var i = 0;
        var totalweight = 0;
        var totalprice = 0;
        //  var unitweight = collection[0].unit;
        var unitweight = $('#txtunitwt').val();
        var amount = 0;
        // var ratio = parseFloat(item / oldvalue).toFixed(2);
        var ratio = parseFloat(item / oldvalue);

        // ratio = ratio.toString(); //If it's not already a String
        //// num = num.slice(0, (num.indexOf(".")) + 3); //With 3 exposing the hundredths place
        // ratio = ratio.slice(0, (ratio.indexOf(".")) + 3);
        // Number(ratio); //If you need it back as a Number
        collection.forEach(function (prod) {
            //  if (i != index) {
            // prod.Qty = parseFloat(parseFloat(ratio) * parseFloat($('#lblhidden' + i).val())).toFixed(2);
            var xy = parseFloat(parseFloat(ratio) * parseFloat($('#lblhidden' + i).val())).toFixed(2);
            prod.Qty = xy;//ratio * $('#lblhidden' + i).val();
            // prod.Qty = (prod.Qty).toFixed(2);
            //   }
            //if (prod.Product_Id === item.Product_Id) {
            //    
            //    if (prod.Qty == 0 || prod.Qty == null) {
            //        prod.Qty = 1;
            //    }


            //}

            prod.Amount = parseFloat(prod.Product_Rate * (prod.Qty / 1000)).toFixed(2);

            i = i + 1;
            totalweight = parseFloat(totalweight) + parseFloat(prod.Qty);
            totalprice = parseFloat(totalprice) + parseFloat(prod.Amount);
        });
        totalweight = parseFloat(totalweight).toFixed(2);
        totalprice = parseFloat(totalprice).toFixed(2);
        $scope.totalprice = totalprice;
        //$scope.totalweight = totalweight / 1000;
        //$scope.unitweight = unitweight;//totalweight / $scope.noofunits;
        //$scope.noofunits = totalweight / unitweight;
        //$scope.unitprice = totalprice / $scope.noofunits;

        $scope.totalweight = item;//parseFloat(totalweight / 1000).toFixed(2);
        var scunitweight = parseFloat(unitweight).toFixed(2);//totalweight / $scope.noofunits;
        var scnoofunits = parseFloat(totalweight / unitweight).toFixed(2);
        var scunitprice = 0;
        if (scnoofunits == 0) {
            scunitprice = 0;
        }
        else {
            scunitprice = parseFloat(totalprice / scnoofunits).toFixed(2);
        }
        $('#lbltotalyield').val(oldvalue);
        $scope.unitweight = scunitweight;
        $scope.noofunits = scnoofunits;
        $scope.unitprice = scunitprice;

        $('#lblnoofunits').val(oldupdate);

    }

    $scope.onunitChange = function (collection, item1, index) {
        loader();
        
        //  var oldvalue = $('#lblhidden' + index).val();
        var oldupdate = $('#lblnoofunits').val();
        var oldvalue = $('#lbltotalyield').val();


        var item = (item1 * $('#txtunitwt').val()) / 1000;
        var i = 0;
        var totalweight = 0;
        var totalprice = 0;
        //  var unitweight = collection[0].unit;
        var unitweight = $('#txtunitwt').val();
        var amount = 0;
        // var ratio = parseFloat(item / oldvalue).toFixed(2);
        var ratio = parseFloat(item / oldvalue);

        // ratio = ratio.toString(); //If it's not already a String
        //// num = num.slice(0, (num.indexOf(".")) + 3); //With 3 exposing the hundredths place
        // ratio = ratio.slice(0, (ratio.indexOf(".")) + 3);
        // Number(ratio); //If you need it back as a Number
        collection.forEach(function (prod) {
            //  if (i != index) {
            // prod.Qty = parseFloat(parseFloat(ratio) * parseFloat($('#lblhidden' + i).val())).toFixed(2);
            var xy = parseFloat(parseFloat(ratio) * parseFloat($('#lblhidden' + i).val())).toFixed(2);
            prod.Qty = xy;//ratio * $('#lblhidden' + i).val();
            // prod.Qty = (prod.Qty).toFixed(2);
            //   }
            //if (prod.Product_Id === item.Product_Id) {
            //    
            //    if (prod.Qty == 0 || prod.Qty == null) {
            //        prod.Qty = 1;
            //    }


            //}

            prod.Amount = parseFloat(prod.Product_Rate * (prod.Qty / 1000)).toFixed(2);

            i = i + 1;
            totalweight = parseFloat(totalweight) + parseFloat(prod.Qty);
            totalprice = parseFloat(totalprice) + parseFloat(prod.Amount);
        });
        totalweight = parseFloat(totalweight).toFixed(2);
        totalprice = parseFloat(totalprice).toFixed(2);
        //$scope.totalweight = totalweight / 1000;
        //$scope.unitweight = unitweight;//totalweight / $scope.noofunits;
        //$scope.noofunits = totalweight / unitweight;
        //$scope.unitprice = totalprice / $scope.noofunits;
        $scope.totalprice = totalprice;
        $scope.totalweight = item;//parseFloat(totalweight / 1000).toFixed(2);
        var scunitweight = parseFloat(unitweight).toFixed(2);//totalweight / $scope.noofunits;
        var scnoofunits = item1;// parseFloat(totalweight / unitweight).toFixed(2);
        var scunitprice = 0;
        if (scnoofunits == 0) {
            scunitprice = 0;
        }
        else {
            scunitprice = parseFloat(totalprice / scnoofunits).toFixed(2);
        }
        $('#lbltotalyield').val(oldvalue);
        $scope.unitweight = scunitweight;
        $scope.noofunits = scnoofunits;
        $scope.unitprice = scunitprice;

        $('#lblnoofunits').val(oldupdate);

    }

    //$scope.onunitChange = function (collection, item, index) {
    //    loader(); 
    //    
    //    //  var oldvalue = $('#lblhidden' + index).val();

    //    var oldupdate = $('#lbltotalyield').val();
    //    var oldvalue = $('#lblnoofunits').val();
    //    var i = 0;
    //    var totalweight = 0;
    //    var totalprice = 0;
    //    //var unitweight = collection[0].unit;
    //    var unitweight = $('#txtunitwt').val();
    //    var amount = 0;
    //    var ratio = parseFloat(item / oldvalue);
    //    collection.forEach(function (prod) {
    //     //   
    //        // prod.Qty = parseFloat(parseFloat(ratio) * parseFloat($('#lblhidden' + i).val())).toFixed(2);
    //        var xy = parseFloat(parseFloat(ratio) * parseFloat($('#lblhidden' + i).val())).toFixed(2);
    //        prod.Qty = xy;//ratio * $('#lblhidden' + i).val();
    //        // prod.Qty = (prod.Qty).toFixed(2);
    //        //   }
    //        //if (prod.Product_Id === item.Product_Id) {
    //        //    
    //        //    if (prod.Qty == 0 || prod.Qty == null) {
    //        //        prod.Qty = 1;
    //        //    }




    //        prod.Amount = parseFloat(prod.Product_Rate * (prod.Qty / 1000)).toFixed(2);

    //        i = i + 1;
    //        totalweight = parseFloat(totalweight) + parseFloat(prod.Qty);
    //        totalprice = parseFloat(totalprice) + parseFloat(prod.Amount);
    //    });
    //    totalweight = parseFloat(totalweight).toFixed(2);
    //    totalprice = parseFloat(totalprice).toFixed(2);
    //    //$scope.totalweight = totalweight / 1000;
    //    //$scope.unitweight = unitweight;//totalweight / $scope.noofunits;
    //    //$scope.noofunits = totalweight / unitweight;
    //    //$scope.unitprice = totalprice / $scope.noofunits;

    //    var sctotalweight = parseFloat(totalweight / 1000).toFixed(2);
    //    var scunitweight = parseFloat(unitweight).toFixed(2);//totalweight / $scope.noofunits;
    //    var scnoofunits = item;//parseFloat(totalweight / unitweight).toFixed(2);
    //  //  var scunitprice = parseFloat(totalprice / scnoofunits).toFixed(2);
    //    var scunitprice = 0;
    //    if (scnoofunits == 0) {
    //        scunitprice = 0;
    //    }
    //    else {
    //        scunitprice = parseFloat(totalprice / scnoofunits).toFixed(2);
    //    }
    //    $('#lblnoofunits').val(oldvalue);



    //    $scope.totalweight = sctotalweight;
    //    $scope.unitweight = scunitweight;
    //    $scope.noofunits = scnoofunits;
    //    $scope.unitprice = scunitprice;
    //    $('#lbltotalyield').val(oldupdate);
    //}


    //$scope.onunitChange = function (collection, item, index) {
    //    loader();
    //    //
    //    ////  var oldvalue = $('#lblhidden' + index).val();

    //    //var oldupdate = $('#lbltotalyield').val();
    //    //var oldvalue = $('#lblnoofunits').val();
    //    //var i = 0;
    //    //var totalweight = 0;
    //    //var totalprice = 0;
    //    ////var unitweight = collection[0].unit;
    //    //var unitweight = $('#txtunitwt').val();
    //    //var amount = 0;
    //    //var ratio = parseFloat(item / oldvalue);
    //    //collection.forEach(function (prod) {
    //    //    //   
    //    //    // prod.Qty = parseFloat(parseFloat(ratio) * parseFloat($('#lblhidden' + i).val())).toFixed(2);
    //    //    var xy = parseFloat(parseFloat(ratio) * parseFloat($('#lblhidden' + i).val())).toFixed(2);
    //    //    prod.Qty = xy;//ratio * $('#lblhidden' + i).val();
    //    //    // prod.Qty = (prod.Qty).toFixed(2);
    //    //    //   }
    //    //    //if (prod.Product_Id === item.Product_Id) {
    //    //    //    
    //    //    //    if (prod.Qty == 0 || prod.Qty == null) {
    //    //    //        prod.Qty = 1;
    //    //    //    }




    //    //    prod.Amount = parseFloat(prod.Product_Rate * (prod.Qty / 1000)).toFixed(2);

    //    //    i = i + 1;
    //    //    totalweight = parseFloat(totalweight) + parseFloat(prod.Qty);
    //    //    totalprice = parseFloat(totalprice) + parseFloat(prod.Amount);
    //    //});
    //    //totalweight = parseFloat(totalweight).toFixed(2);
    //    //totalprice = parseFloat(totalprice).toFixed(2);
    //    ////$scope.totalweight = totalweight / 1000;
    //    ////$scope.unitweight = unitweight;//totalweight / $scope.noofunits;
    //    ////$scope.noofunits = totalweight / unitweight;
    //    ////$scope.unitprice = totalprice / $scope.noofunits;

    //    //var sctotalweight = parseFloat(totalweight / 1000).toFixed(2);
    //    //var scunitweight = parseFloat(unitweight).toFixed(2);//totalweight / $scope.noofunits;
    //    //var scnoofunits = item;//parseFloat(totalweight / unitweight).toFixed(2);
    //    ////  var scunitprice = parseFloat(totalprice / scnoofunits).toFixed(2);
    //    //var scunitprice = 0;
    //    //if (scnoofunits == 0) {
    //    //    scunitprice = 0;
    //    //}
    //    //else {
    //    //    scunitprice = parseFloat(totalprice / scnoofunits).toFixed(2);
    //    //}
    //    //$('#lblnoofunits').val(oldvalue);



    //    //$scope.totalweight = sctotalweight;
    //    //$scope.unitweight = scunitweight;
    //    //$scope.noofunits = scnoofunits;
    //    //$scope.unitprice = scunitprice;
    //    //$('#lbltotalyield').val(oldupdate);
    //    

    //    var oldvalue = $('#lblhidden' + 0).val();
    //    var ollnoofunits = $('#lblnoofunits').val();
    //    var check = oldvalue / ollnoofunits;//item;

    //    var newvalue = check;//collection[0].Qty;
    //    var i = 0;
    //    var totalweight = 0;
    //    var totalprice = 0;
    //    //var unitweight = item.unit;
    //    var unitweight = $('#txtunitwt').val();
    //    var amount = 0;
    //    var ratio = newvalue / oldvalue;
    //    collection.forEach(function (prod) {
    //      //  if (i != index) {
    //            //   prod.Qty = ratio * $('#lblhidden' + i).val();
    //            var xy = parseFloat(parseFloat(ratio) * parseFloat($('#lblhidden' + i).val())).toFixed(2);
    //            prod.Qty = xy;//ratio * $('#lblhidden' + i).val();
    //       // }
    //        //if (prod.Product_Id === item.Product_Id) {
    //        //    
    //        //    if (prod.Qty == 0 || prod.Qty == null) {
    //        //        prod.Qty = 1;
    //        //    }


    //        //}

    //        prod.Amount = parseFloat(prod.Product_Rate * (prod.Qty / 1000)).toFixed(2);

    //        i = i + 1;
    //        totalweight = parseFloat(totalweight) + parseFloat(prod.Qty);
    //        totalprice = parseFloat(totalprice) + parseFloat(prod.Amount);
    //    });

    //    totalweight = parseFloat(totalweight).toFixed(2);
    //    totalprice = parseFloat(totalprice).toFixed(2);
    //    //$scope.totalweight = totalweight / 1000;
    //    //$scope.unitweight = unitweight;//totalweight / $scope.noofunits;
    //    //$scope.noofunits = totalweight / unitweight;
    //    //$scope.unitprice = totalprice / $scope.noofunits;

    //    //$scope.totalweight = parseFloat(totalweight / 1000).toFixed(2);
    //    //$scope.unitweight = parseFloat(unitweight).toFixed(2);//totalweight / $scope.noofunits;
    //    //$scope.noofunits = parseFloat(totalweight / unitweight).toFixed(2);
    //    //$scope.unitprice = parseFloat(totalprice / $scope.noofunits).toFixed(2);


    //    var sctotalweight = parseFloat(totalweight / 1000).toFixed(2);
    //    var scunitweight = parseFloat(unitweight).toFixed(2);//totalweight / $scope.noofunits;
    //    var scnoofunits = item;// parseFloat(totalweight / unitweight).toFixed(2);
    //    var scunitprice = 0;
    //    if (scnoofunits == 0) {
    //        scunitprice = 0;
    //    }
    //    else {
    //        var scunitprice = parseFloat(totalprice / scnoofunits).toFixed(2);
    //    }
    //    $scope.totalweight = sctotalweight;
    //    $scope.unitweight = scunitweight;
    //    $scope.noofunits = scnoofunits;
    //    $scope.unitprice = scunitprice;
    //    $('#lblnoofunits').val(item);
    //}

    $scope.onUWChange = function (collection, item, index) {
        loader();
        
        //    var oldvalue = $('#lblhidden' + index).val();

        var oldupdate = $('#lbltotalyield').val();
        var oldvalue = $('#lblnoofunits').val();
        var oldv = $scope.totalweight * 1000;
        var unitweight = $('#txtunitwt').val();
        if (unitweight > oldv) {
            unitweight = $('#lblunitweight').val();
            alert('Per Unit Weight cannot be greater than Total Yield')
        }
        else {
            $('#lblunitweight').val(unitweight);
        }

        var i = 0;
        var totalweight = 0;
        var totalprice = 0;
        //var unitweight = collection[0].unit;
        var amount = 0;
        //  var ratio = parseFloat(item / oldvalue);
        collection.forEach(function (prod) {
            //
            // prod.Qty = parseFloat(parseFloat(ratio) * parseFloat($('#lblhidden' + i).val())).toFixed(2);
            //  var xy = parseFloat(parseFloat(ratio) * parseFloat($('#lblhidden' + i).val())).toFixed(2);
            //   prod.Qty = xy;//ratio * $('#lblhidden' + i).val(); 
            prod.Amount = parseFloat(prod.Product_Rate * (prod.Qty / 1000)).toFixed(2);

            i = i + 1;
            totalweight = parseFloat(totalweight) + parseFloat(prod.Qty);
            totalprice = parseFloat(totalprice) + parseFloat(prod.Amount);
        });
        totalweight = parseFloat(totalweight).toFixed(2);
        totalprice = parseFloat(totalprice).toFixed(2);
        $scope.totalprice = totalprice;
        var sctotalweight = parseFloat(totalweight / 1000).toFixed(2);
        var scunitweight = unitweight;//parseFloat(unitweight).toFixed(2);//totalweight / $scope.noofunits;

        if (unitweight == 0 || unitweight == '') {
            scnoofunits = 0;
        }
        else {
            scnoofunits = parseFloat(totalweight / unitweight).toFixed(2);
        }
        //  var scnoofunits = parseFloat(totalweight / unitweight).toFixed(2);
        //  var scunitprice = parseFloat(totalprice / scnoofunits).toFixed(2);
        var scunitprice = 0;
        if (scnoofunits == 0) {
            scunitprice = 0;
        }
        else {
            scunitprice = parseFloat(totalprice / scnoofunits).toFixed(2);
        }
        $('#lblnoofunits').val(scnoofunits);
        $scope.totalweight = sctotalweight;
        $scope.unitweight = scunitweight;
        $scope.noofunits = scnoofunits;
        $scope.unitprice = scunitprice;
        //  $('#lbltotalyield').val(oldupdate);
    }
});

function loader() {

    //$.confirm({
    //    backgroundDismiss: true,
    //    title: false,
    //    content: '<div class="checkbox" style="text-align:center;"><label style="font-size: 16px;">Adjusting Recipe... <i class="fas fa-circle-notch fa-spin"></i></label></div>',
    //    buttons: false
    //});

    //setTimeout(function () {
    //    $(".jconfirm-closeIcon").click();
    //}, 400);
}
//angular.bootstrap(document.getElementById('side-cart'), ['MyApp']);
//angular.module('MyApp1', ['rzModule']).controller('sidecart', function ($scope, $rootScope, $http) {


//    $scope.$on('addcarton', function (event, value) {
//        
//        $scope.Side = value;
//        angular.element(document.getElementById('side-cart')).scope().$apply();
//        //$scope.ttlsku = parseFloat($scope.ttlsku) + parseFloat(value);

//        //if (parseFloat($scope.ttlsku) <= parseFloat(0.500)) {
//        //    $scope.Ship = 30;
//        //}
//        //else if (parseFloat($scope.ttlsku) <= parseFloat(2.000)) {
//        //    $scope.Ship = 60;
//        //}
//        //else {
//        //    $scope.Ship = 120;
//        //}
//    });
//});

//app.controller('yield', function ($scope, $http, $window, $rootScope) {
