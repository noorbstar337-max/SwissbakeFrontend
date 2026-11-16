var revapi;
$(document).ready(function () {
    revapi = $("#rev_slider").revolution({
        sliderType: "standard",
        sliderLayout: "fullwidth",
        delay: 5000,
        navigation: {
            arrows: { enable: true }
        },
        gridwidth: 1230,
        gridheight: 300
    });
    revapi = $("#rev_slider2").revolution({
        sliderType: "standard",
        sliderLayout: "fullwidth",
        delay: 5000,
        navigation: {
            arrows: { enable: true }
        },
        gridwidth: 1230,
        gridheight: 300
    });
});	/*ready*/

var acc = document.getElementsByClassName("profileaccordion");
var i;

for (i = 0; i < acc.length; i++) {
    acc[i].addEventListener("click", function() {
        this.classList.toggle("active");
        var panel = this.nextElementSibling;
        if (panel.style.maxHeight){
            panel.style.maxHeight = null;
        } else {
            panel.style.maxHeight = panel.scrollHeight + "px";
        } 
    });
}

$(document).ready(function () {
    var form = $('#Profile');
    var error = $('.sm-form-control', form);

    $.validator.addMethod(
        "regex",
        function (value, element, regexp) {
            var re = new RegExp(regexp);
            return this.optional(element) || re.test(value);
        },
        "This is invalid"
    );

    $.validator.addMethod("pwcheck", function (value) {
        return /^(?=[^\d_].*?\d)\w(\w|[!@#$%]){7,20}$/.test(value) // consists of only these
            && /[a-z]/.test(value) // has a lowercase letter
            && /\d/.test(value) // has a digit
    }, "Min length must be 8, use only characters, numbers and any !@#$%");

    $.validator.addMethod("notEqualTo", function (value, element, param) {
        return this.optional(element) || value != $(param).val();
    }, "This has to be different...");

    jQuery.validator.addMethod("notEqualstatic", function (value, element, param) {
        return this.optional(element) || value != param;
    }, "Please specify a different (non-default) value");

    var $validator = $('#Profile').validate({
        //doNotHideMessage: true, //this option enables to show the error/success messages on tab switch.
        //errorElement: 'span', //default input error message container
        //errorClass: 'error-block1', // default input error message class
        //focusInvalid: false, // do not focus the last invalid input
        rules: {
            Companyname: {
                required: true,
                //regex: /^([a-zA-Z]+\s)*[a-zA-Z]+$/,
                regex: /^[a-zA-Z0-9. ]+$/,
                minlength: 3,
                //notEqualTo: "#lstnm"
            },
            fullname: {
                required: true,
                regex: /^([a-zA-Z]+\s)*[a-zA-Z]+$/,
                minlength: 3,
                //notEqualTo: "#fstnm"
            },
            storename: {
                required: true,
                regex: /^([a-zA-Z]+\s)*[a-zA-Z]+$/,
                minlength: 3,
            },
            desig: {
                required: false
            },
            emailid: {
                required: true,
                email: true,
                regex: /^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/,
                remote: {
                    url: "/Access/Checkmail",
                    type: "post",

                    complete: function (data) {
                        
                        if (data.responseJSON == false) {

                            setTimeout(function () {
                                {


                                    $.confirm({
                                        theme: 'supervan',
                                        title: 'Email Already Exist!',
                                        content: 'Do you want to login or continue with another email.',
                                        buttons: {
                                            confirm: {
                                                text: 'Login',
                                                btnClass: 'btn-red',
                                                keys: ['enter', 'shift'],
                                                action: function () {
                                                    window.location.href = '/login';
                                                }
                                            },
                                            cancel: {
                                                text: 'Continue',
                                                action: function () {
                                                    $.alert({ title: 'Caution!', theme: 'supervan', content: 'Please continue with another email id.' });
                                                }
                                            },
                                            //somethingElse: {
                                            //    text: 'Something else',
                                            //    btnClass: 'btn-blue',
                                            //    keys: ['enter', 'shift'],
                                            //    action: function () {
                                            //        $.alert('Something else?');
                                            //    }
                                            //}
                                        }
                                    });
                                }
                            }, 500);

                        }




                    }
                }
            },
            mobile: {
                required: true,
                regex: /([0-9]{8}$)|(^[7-9][0-9]{8}$)/,
                minlength: 8,
                maxlength: 10,
                remote: {
                    url: "/Access/Checkmobile",
                    type: "post",
                    complete: function (data) {
                        
                        if (data.responseJSON == false) {

                            setTimeout(function () {
                                {


                                    $.confirm({
                                        title: 'Mobile No. Already Exist!',
                                        theme: 'supervan',
                                        content: 'Do you want to login or continue with another mobile no.',
                                        buttons: {
                                            confirm: {
                                                text: 'Login',
                                                btnClass: 'btn-red',
                                                keys: ['enter', 'shift'],
                                                action: function () {
                                                    window.location.href = '/login';
                                                }
                                            },
                                            cancel: {
                                                text: 'Continue',
                                                action: function () {
                                                    $.alert({ title: 'Caution!', theme: 'supervan', content: 'Please continue with another mobile no.' });
                                                }
                                            },
                                            //somethingElse: {
                                            //    text: 'Something else',
                                            //    btnClass: 'btn-blue',
                                            //    keys: ['enter', 'shift'],
                                            //    action: function () {
                                            //        $.alert('Something else?');
                                            //    }
                                            //}
                                        }
                                    });
                                }
                            }, 500);

                        }




                    }
                }
            },
            telephone: {
                required: true,
                regex: /([0-9]{8,12}$)/,
                maxlength: 12
            },
            password: {
                required: true,
                pwcheck: true,
                notEqualstatic: 'Pass@123'
            },
            CPassword: {
                required: true,
                equalTo: "#pass",
                notEqualstatic: 'Pass@123'
            },
            ACity: {
                required: true
            },
            //typeoption: {
            //    required:true
            //},
            BillingName: {
                required: true,
                regex: /^[a-zA-Z. ]+$/,
            },
            Email: {
                required: true,
                regex: /^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/
            },
            Mobile: {
                required: true,
                regex: /([0-9]{8}$)|(^[7-9][0-9]{8}$)/,
                minlength: 8,
                maxlength: 10,
            },
            ShippingName: {
                required: function () {
                    if ($("[name='billing']").is(":checked")) {
                        return false;
                    }
                    else {
                        return true;
                    }
                },
                regex: /^[a-zA-Z. ]+$/,
            },
            Email2: {
                required: function () {
                    if ($("[name='billing']").is(":checked")) {
                        return false;
                    }
                    else {
                        return true;
                    }
                },
                regex: /^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/
            },
            Mobile2: {
                required: function () {
                    if ($("[name='billing']").is(":checked")) {
                        return false;
                    }
                    else {
                        return true;
                    }
                },
                regex: /([0-9]{11}$)|(^[7-9][0-9]{9}$)/,
                minlength: 8,
                maxlength: 10,
            },
            StreetName: {
                required: true
            },
            pinCode: {
                required: true
            },
            City: {
                required: true
            },
            State: {
                required: true
            },
            country: {
                required: true
            },
            sname: {
                required: function () {
                    if ($("[name='billing']").is(":checked")) {
                        return false;
                    }
                    else {
                        return true;
                    }
                }
            },
            pin: {
                required: function () {
                    if ($("[name='billing']").is(":checked")) {
                        return false;
                    }
                    else {
                        return true;
                    }
                }
            },
            cty: {
                required: function () {
                    if ($("[name='billing']").is(":checked")) {
                        return false;
                    }
                    else {
                        return true;
                    }
                }
            },
            stt: {
                required: function () {
                    if ($("[name='billing']").is(":checked")) {
                        return false;
                    }
                    else {
                        return true;
                    }
                }
            },
            cntry: {
                required: function () {
                    if ($("[name='billing']").is(":checked")) {
                        return false;
                    }
                    else {
                        return true;
                    }
                }
            },
            pan: {
                required: false,
                //regex: /([a-zA-Z]){5}([0-9]){4}([a-zA-Z]){1}/,
                minlength: 10,
                maxlength: 10
            },
            gst: {
                required:false,
                minlength: 15,
                maxlength: 15
            },
            sgst: {
                regex: /([0-9]{2}[a-zA-Z]{4}([a-zA-Z]{1}|[0-9]{1})[0-9]{4}[a-zA-Z]{1}([a-zA-Z]|[0-9]){3})/,
                minlength: 15,
                maxlength: 15
            }

        },

        messages: {
            Companyname: {
                required: "This is required",
                regex: "This is invalid",
                notEqualTo: "Company Name should not be same as Full Name"
            },
            fullname: {
                required: "This is required",
                regex: "This is invalid",
                notEqualTo: "Full Name should not be same as company name"
            },
            storename: {
                required: "This is required",
                regex: "This is invalid"
            },
            desig: {
                required: "This is required"
            },
            emailid: {
                required: "This is required",
                regex: "This is invalid",
                remote: "Email already exist!"
            },
            mobile: {
                required: "This is required",
                regex: "This is invalid",
                remote: "Mobile No. already exist!",
                maxlength: "Max lenght 10 digits"
            },
            telephone: {
                required: "This is required",
                regex: "This is invalid",
                maxlength: "Max lenght 12 digits"
            },
            password: {
                required: "This is required",
                notEqualstatic: "Entered password should'nt be same as default eg."
            },
            CPassword: {
                required: "This is required",
                equalTo: "Enter same password again",
                notEqualstatic: "Entered password should'nt be same as default eg."
            },
            ACity: {
                required: "City is required",
            },
            //typeoption: {
            //    required:true
            //},
            StreetName: {
                required: "This is required"
            },
            pinCode: {
                required: "This is required"
            },
            City: {
                required: "This is required"
            },
            State: {
                required: "This is required"
            },
            Country: {
                required: "This is required"
            },
            sname: {
                required: function () {
                    if ($("[name='billing']").is(":checked")) {
                        return "";
                    }
                    else {
                        return "This is required";
                    }
                }
            },
            pin: {
                required: function () {
                    if ($("[name='billing']").is(":checked")) {
                        return "";
                    }
                    else {
                        return "This is required";
                    }
                }
            },
            cty: {
                required: function () {
                    if ($("[name='billing']").is(":checked")) {
                        return "";
                    }
                    else {
                        return "This is required";
                    }
                }
            },
            stt: {
                required: function () {
                    if ($("[name='billing']").is(":checked")) {
                        return "";
                    }
                    else {
                        return "This is required";
                    }
                }
            },
            cntry: {
                required: function () {
                    if ($("[name='billing']").is(":checked")) {
                        return "";
                    }
                    else {
                        return "This is required";
                    }
                }
            },
            pan: {
                required: "This is required",
                regex: "This is Invalid",
                minlength: "This is Invalid",
                maxlength: "This is Invalid"
            },
            gst: {
                required: "This is required",
                regex: "Format is Invalid",
                minlength: "This is Invalid Min",
                maxlength: "This is Invalid Max"
            },
            sgst: {
                regex: "This is Invalid",
                minlength: "This is Invalid",
                maxlength: "This is Invalid"
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
            var $valid = $('#Profile').valid();
            if (!$valid) {
                $validator.focusInvalid();
                return false;
            }
            else {
                //if ($("input[name=mode]").val() == "login") {
                //    $("input[name=login]").val("Logging...").attr("disabled", "disabled");
                //    var obj = {};

                //    obj.mail = $("input[name=mail]").val();
                //    obj.pas = $("input[name=pswd]").val();
                //    obj.me = "";
                //    if ($('input[name=rememberme]').is(":checked")) {
                //        obj.me = "1";
                //    }

                //    return checklog(obj.mail, obj.pas, obj.me);
                //}
                //else {
                //    return update();
                //}
                //alert("done")
                $("input[type=text]:disabled").removeAttr("disabled");
                form.submit()
                return false;




                
            }
            //add here some ajax code to submit your form or just call form.submit() if you want to submit the form without ajax
        }
    });
});


    
	