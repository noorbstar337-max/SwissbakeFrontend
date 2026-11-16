var app = angular.module('MyApp', [])

app.controller('MyDash', function ($scope, $http, $window) {
    
    //$scope.IsVisible = false;
    //$scope.Search = function () {
    //    //var customer = '{Name: "' + $scope.Prefix + '" }';
        

    //    //post.success(function (data, status) {
    //    //    $scope.Customers = data;
    //    //    $scope.IsVisible = true;
    //    //});

    //    //post.error(function (data, status) {
    //    //    $window.alert(data.Message);
    //    //});
    //}
    $http({
        method: "GET",
        url: "/access/GetOrderDash",
        dataType: 'json',
        //data: customer,
        //headers: { "Content-Type": "application/json" }
    }).then(function (response) {
        
        if (response.status == 200){
            $scope.Orders = response.data;
        }
        
    });


    $scope.unread = function () {
        
        var update = '{T: "False" }';
        $http({
            method: "POST",
            url: "/access/GetAllNotifi",
            dataType: 'json',
            data: update,
            headers: { "Content-Type": "application/json" }
        }).then(function (response) {

            if (response.status == 200) {
                $scope.Notifi = response.data;
                $scope.al = "";
                $scope.r = "";
                $scope.u = "active";
            }
            //loadmore();
        });
    }

    $scope.unread();

    $scope.update = function (i, id) {
        //alert(i + "/" + id);
        var update = '{Id: "' + id + '" }';
        $http({
            method: "POST",
            url: "/access/UpdateNotifi",
            dataType: 'json',
            data: update,
            headers: { "Content-Type": "application/json" }
        }).then(function (response) {

            if (response.status == 200) {
                $scope.Notifi[i].Status = "True";
                
                var count = $('#notifLcount').text();
                count -= 1;
                if (count > 0) {
                    $('#notifLcount').text(count);
                    $("#notifLcount").removeClass("d-none");
                    $('#notifScount').text(count);
                    $("#notifScount").removeClass("d-none");
                }
                else {
                    $('#notifLcount').text(count);
                    $("#notifLcount").addClass("d-none");
                    $('#notifScount').text(count);
                    $("#notifScount").addClass("d-none");
                }
            }
            //loadmore();
        });


    }

    $scope.setPageNumber = function (index) {
        $scope.pageNumber = index;
    }

    $scope.Paynow = function (index, code, amount, D) {
        //alert("Order Code:" + code + "Order Amount: " + amount + "Order Id: " + id + "Index:"+index);
        $http({
            method: "POST",
            url: "/access/PaynowOrder",
            data: '{ID : "' + D + '"}',
            dataType: 'json'
        }).then(function (response) {

            if (response.status == 200 && response.data != "") {
                
                if (response.data.includes("Already Paid") ) {
                    //$scope.getSTotal();
                    //$scope.getVatTotal();
                    //$scope.getTotal();
                    //angular.element(document.getElementById('side-cart')).scope().$emit('addcarton', response.data);
                    alert('You have already made the payment of this order');
                }
                else if (response.data.includes("/Paynow")){
                    window.location.href = "/Paynow";
                }
            }
            else {
                window.location.href = "/Account/Index";
            }
        });
    }

    $http({
        method: "GET",
        url: "/access/GetInvoiceDash",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        //data: customer,
        //headers: { "Content-Type": "application/json" }
    }).then(function (response) {
        
        if (response.status == 200) {
            $scope.Invoice = response.data;
        }

    });


    $http({
        method: "GET",
        url: "/access/GetPaymentDash",
        dataType: 'json',
        //data: customer,
        //headers: { "Content-Type": "application/json" }
    }).then(function (response) {
        
        if (response.status == 200) {
            $scope.Payment = response.data;
        }

    });


    $http({
        method: "GET",
        url: "/access/GetComplaintHistDash",
        dataType: 'json',
        //data: customer,
        //headers: { "Content-Type": "application/json" }
    }).then(function (response) {
        
        if (response.status == 200) {
            $scope.Complaint = response.data;
        }

    });

    $scope.getStatus = function (caseStr) {
        switch (caseStr) {
            case 'Pending':
                return 'pend';
            case 'In-Process':
                return 'pend';
            case 'Pending Approval':
                return 'pend';
            case 'Complete':
                return 'Complet';
            case 'Paid':
                return 'Complet';
            case 'Confirmation Sent':
                return 'Approve';
            case 'Resolved':
                return 'Complet';
            case 'Approved':
                return 'Approve';
            case 'Placed':
                return 'Place';
            case 'Cancelled':
                return 'cancl';
            case 'Cancel':
                return 'cancl';
            case 'Invoice Sent':
                return 'sent';
            case 'Delivered':
                return 'Complet';
            case 'Dispatched':
                return 'sent';
            case 'Part Dispatched':
                return 'PartDisp';
            case 'Partial':
                return 'PartDisp';
            case 'On Hold':
                return 'onhold';
            default:

        }
    };



});

$(document).ready(function () {
    $(".btn-id").click(function () {

        $.confirm({
            title: 'Pay Pending Payment',
            //theme: 'supervan',
            content: '' +
            '<form action="" class="formName m-0">' +
            '<div class="form-group">' +
            '<label>Enter Amount</label>' +
            '<input type="text" placeholder="Pending Payment" class="otp form-control" required />' +
            '</div>' +
            '</form>',
            contentLoaded: function (data, status, xhr) {
                if (data != "Request Timeout") {
                    $(function () {
                        //var res = jQuery("#" + '<%= hfresend.ClientID%>').val();
                        //if (res == 1) {

                        //}
                        $('.btn-resend').timedDisable(120);
                    });

                }
            },
            closeIcon: true,
            
            buttons: {
                formSubmit: {
                    text: 'Pay Now',
                    btnClass: 'btn-blue',
                    action: function () {
                        var otp = this.$content.find('.otp').val();
                        $('.jconfirm-content label.text-danger').remove();
                        var valid = /^[0-9]{2,20}$/i;
                        var ms = "";
                        var res = "";
                        if (otp == "") {
                            this.setContentAppend('<label class="text-danger" style="font-size:11px">All fields required</label>');
                        }
                        else if (otp != "" && valid.test(otp)) {
                            
                            $.ajax({
                                type: "POST",
                                url: "/Access/pendingpay",
                                beforeSend: function () {
                                    $(".jconfirm-buttons button.btn-blue").attr("disabled", "disabled");
                                },
                                //data: '{Compname: "' + Compname + '", Fullname: "' + Fullname + '", Email: "' + Email + '", Mobile:"' + Mobile + '", Password: "' + Passsword + '",Type:"' + mtype + '", Bname:"' + Bname + '", Bemail:"' + Bemail + '", Bmobile:"' + Bmobile + '", Bstreet:"' + Bstreet + '", Bpin:"' + Bpin + '", Bcity:"' + Bcity + '", Bstate:"' + Bstate + '", , Sname:"' + Sname + '", Semail:"' + Semail + '", Smobile:"' + Smobile + '", Sstreet:"' + Sstreet + '", Spin:"' + Spin + '", Scity:"' + Scity + '", Sstate:"' + Sstate + '", step:"' + step + '"  }',
                                data: '{M: "' + otp + '"}',
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                success: function (response) {
                                    res = response;
                                    if (response.data != "") {
                                        
                                        if (response == 'Already Paid') {
                                           // alert('You have already made the payment of this order');
                                            window.location.href = "/Account/Index";
                                        }
                                        else if (response == '/Paynow') {
                                            window.location.href = "/Paynow";
                                        }
                                    }
                                    else {
                                        window.location.href = "/Account/Index";
                                    }
                                    $.dialog({
                                        title: false,
                                        content: ms,
                                        closeIconClass: 'close-dialog',
                                    });
                                    setTimeout(function () {
                                        $(".close-dialog").click();
                                        if ($("input[name=login]").val() == "Logging...") {
                                            $("input[name=login]").val("Login").removeAttr("disabled");
                                        }
                                        $(".wizard-card form")[0].reset();
                                        $(".wizard-card form input[name=Email]").focus();
                                    }, 5000);
                                    //setTimeout(function () {
                                    //    if (document.referrer != "" && document.referrer != undefined){
                                    //        window.location.href = document.referrer;
                                    //    }
                                    //    else {
                                    //        window.location.href = "/account/index";
                                    //    }
                                    //}, 6000);
                                },
                            });
                        }
                        else if (otp != "") {
                            this.setContentAppend('<label class="text-danger" style="font-size:11px">This is Invalid</label>');
                        }


                        setInterval(function () {
                            if (res == "Verified") {
                                $(".close-verify").click();
                            }
                        }, 2000);

                        return false;
                    }
                },
                //cancel: function () {
                //    //close
                //},
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

    });


    // Code for the Validator
    var form = $('#subs-form');
    var error = $('.form-control', form);

    var $validator = $('#subs-form').validate({
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
            var $valid = $('#subs-form').valid();
            if (!$valid) {
                $validator.focusInvalid();
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


});



function EditSch(id) {
    
    var p = id.parentElement.parentElement.localName;
    var ri = id.parentElement.parentElement.rowIndex;
    var d;

    var v = $(".subtable "+ p + ":nth-child(" + (ri) + ") td:nth-child(5)").text().trim(" ");
    //$(".side-sch form .sch-qty").val(id.parentNode.parentNode[1].childNodes[1].childNodes[1].childNodes[2].childNodes["0"].childNodes[3].childNodes[1].childNodes[1].innerText);
    $(".side-sch form .sch-qty").val(v.replace(" Kg", ""));

    //if (v.includes("-")) {
    //    d = v;
    //    v = "Custom";

    //}
    var t = $(".subtable " + p + ":nth-child(" + (ri) + ") td:nth-child(3) .requirement").val().trim(" ");
    $(".side-sch form .sch-type").val(t).change();
    var on = $(".subtable " + p + ":nth-child(" + (ri) + ") td:nth-child(3) .SubOn").val();
    //var v = $(p + ":nth-child(" + (ri + 1) + ") td:nth-child(2)").text();
    $(".side-sch form .sch-date").val(on);
    var i = $(".subtable " + p + ":nth-child(" + (ri) + ") td:nth-child(2) input[type=hidden]").val();
    $(".side-sch form #pid").val(i);
    var n = $(".subtable " + p + ":nth-child(" + (ri) + ") td:nth-child(2)").text().trim(" ");
    $(".side-sch form .sch-name").text(n);
    $(".side-sch form button[type=submit]").val(id.id);
    setTimeout(function () {
        $('.side-sch h4').text("Edit reminders");
    }, 100);

    if (t == "Custom") {
        $(".side-sch form #subsdate .label-floating").text("Days");
        var dy = "day";
        if (on > 1){
            dy = "days";
        }
        $(".side-sch form #subsdate .error").text("System will remind you after every " + on + " " + dy);
    }
    else {
        $(".side-sch form #subsdate .label-floating").text("Day");
        $(".side-sch form #subsdate .error").text("System will remind you " + t + " on Day " + on);
    }

    

}

function addSchedule() {
    
    var formData = new FormData();
    //formData.append('SubId', $("form submit[type=submit]").val());
    formData.append('Type', $(".side-sch form .sch-type").val());

    formData.append('Qty', $(".side-sch form .sch-qty").val());
    formData.append('ProName', $(".side-sch form .sch-name").text());

    if ($(".side-sch form button[type=submit]").val() != "" && $(".side-sch form button[type=submit]").val() != undefined) {
        formData.append('Edit', $(".side-sch form button[type=submit]").val());
        formData.append('PId', $(".side-sch form #pid").val());
        //alert($(".side-sch form #pid").val() + " // " + $(".side-sch form button[type=submit]").val());
    }
    //if ($(".side-sch form .sch-type").val() == "Custom") {
        formData.append('Date', $(".side-sch form .sch-date").val());
    //}
    //else {
    //    formData.append('Date', "");
    //}



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
                content: '<div class="checkbox" style="text-align:center;"><label style="font-size: 21px;">Updating... <i class="fas fa-circle-notch fa-spin"></i></label></div>',
                buttons: false
            });
            //_divPartialView.style = "Display: none";
            //$("#divWaiting").removeClass("display-none");
        },
        success: function (response) {
            if (response == true) {
                setTimeout(function () {
                    location.reload(true);
                }, 600);
            }
            else {
                $(".close-dialog").click();
                $(".req-btn").removeAttr("disabled");
            }
            setTimeout(function () {
                $(".jconfirm-closeIcon").click();
            }, 500);
        },
    }).done(function (response) {
        true;
    }).fail(function () {
        false;
    });
}




function Subdel(id) {
    var formData = new FormData();
    formData.append('ID', id.id);
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
                    $.ajax({
                        type: "POST",
                        url: "/Access/DelSubs",
                        //data: '{Compname: "' + Compname + '", Fullname: "' + Fullname + '", Email: "' + Email + '", Mobile:"' + Mobile + '", Password: "' + Passsword + '",Type:"' + mtype + '", Bname:"' + Bname + '", Bemail:"' + Bemail + '", Bmobile:"' + Bmobile + '", Bstreet:"' + Bstreet + '", Bpin:"' + Bpin + '", Bcity:"' + Bcity + '", Bstate:"' + Bstate + '", , Sname:"' + Sname + '", Semail:"' + Semail + '", Smobile:"' + Smobile + '", Sstreet:"' + Sstreet + '", Spin:"' + Spin + '", Scity:"' + Scity + '", Sstate:"' + Sstate + '", step:"' + step + '"  }',
                        data: formData,
                        contentType: false,
                        processData: false,
                        beforeSend: function () {
                            $.confirm({
                                backgroundDismiss: false,
                                title: false,
                                content: '<div class="checkbox" style="text-align:center;"><label style="font-size: 21px;">Fetching Data... <i class="fas fa-circle-notch fa-spin"></i></label></div>',
                                buttons: false
                            });
                            //_divPartialView.style = "Display: none";
                            //$("#divWaiting").removeClass("display-none");
                        },
                        success: function (response) {
                            if (response == true) {
                                setTimeout(function () {
                                    $(".jconfirm-closeIcon").click();
                                }, 500);
                                setTimeout(function () {
                                    $.confirm({
                                        backgroundDismiss: false,
                                        title: false,
                                        content: '<div class="checkbox" style="text-align:center;"><label style="font-size: 21px;">Deleted Successfully <i class="fas fa-circle-notch fa-spin"></i></label></div>',
                                        buttons: false
                                    });
                                    setTimeout(function () {
                                        $(".jconfirm-closeIcon").click();
                                    }, 500);
                                }, 500);
                                id.parentElement.parentElement.remove();
                            }
                            else {
                                setTimeout(function () {
                                    $(".jconfirm-closeIcon").click();
                                }, 500);
                            }
                        },
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