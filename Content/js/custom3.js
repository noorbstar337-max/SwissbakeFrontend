$(".cat-button").hover(function () {
    $(".cat-menu").toggleClass("transition-off");
});
$(window).resize(function () {
    // This will execute whenever the window is resized
    
    $("#content").css("min-height", ($(window).innerHeight() - 200) + "px")
});
$(window).on('beforeunload', function () {
    $('body').fadeOut(0);
});

$(document).ready(function () {
    
    //$('.dual-banner').css("margin-top", $('#header').outerHeight());
    setTimeout(function () {
        //$('.dual-banner').css("margin-top", $('#header').outerHeight()).animate(300);
        $('.dual-banner').animate({
            'paddingTop': $('#header').outerHeight()
        }, 0)
        //$('.dual-banner .col-md-6:nth-child(1)').addClass("fadeInLeft").removeClass("op-0");
        //$('.dual-banner .col-md-6:nth-child(2)').addClass("fadeInRight").removeClass("op-0");

        


    }, 100);
    //debugger;

    if ($('#page-title').outerHeight() > 0) {
       // debugger;
        $("#content, #turms, #ship-policy, .cat-grid, .login, .registerfrm, .trckordr").css("min-height", ($(window).height() - ($('footer').outerHeight() + 200)))
    }
    else {
        $("#content").css("min-height", ($(window).height() - ($('footer').outerHeight() + 200)))
    }

    





    if ($(".side-sch form .sch-type").val() == "Custom") {
        $(".side-sch form #subsdate .label-floating").text("Days");
        var d = $(".side-sch form #subsdate select").val();
        var dy = "day";
        if (d > 1) {
            dy = "days";
        }
        $(".side-sch form #subsdate .error").text("System will remind you after every " + d + " " + dy);
    }
    else {
        $(".side-sch form #subsdate .label-floating").text("Day");
        var d = $(".side-sch form #subsdate select").val();
        $(".side-sch form #subsdate .error").text("System will remind you " + $(".side-sch form .sch-type").val() + " on " + d + " Day");
    }


    $(".side-sch form .sch-type, .side-sch form #subsdate select").change(function () {
        if ($(".side-sch form .sch-type").val() == "Custom") {
            $(".side-sch form #subsdate .label-floating").text("Days");
            var d = $(".side-sch form #subsdate select").val();
            var dy = "day";
            if (d > 1) {
                dy = "days";
            }
            $(".side-sch form #subsdate .error").text("System will remind you after every " + d + " " + dy);
        }
        else {
            $(".side-sch form #subsdate .label-floating").text("Day");
            var d = $(".side-sch form #subsdate select").val();
            $(".side-sch form #subsdate .error").text("System will remind you " + $(".side-sch form .sch-type").val() + " on Day " + d);
        }
    });

    $('#top-bar').on('click', '#close-bar', function () {
        $(this).parents('#top-bar').slideUp(300);
        $('body').css('padding-top', 0);
        setTimeout(function () {
            //$('.dual-banner').css("margin-top", $('#header').outerHeight()).animate(300);
            //$('.dual-banner').animate({
            //    'marginTop': $('#header').outerHeight()
            //},300)
            $("#slider.full-screen").css("display", "none");
            $('.dual-banner').animate({
                'paddingTop': $('#header').outerHeight(),
                'marginTop': 0,
            }, 00)
        },300);
        $('#page-title').addClass("pt-9").removeClass("pt-11");
    })
    //responsive header js
    var win = $(window).width()
    setTimeout(function () {
        win = $(window).width();
        var head = $('#header .container').width();
        var logo = $('#logo').outerWidth();
        var btns = $('.mid-head-btn-blk').outerWidth();
        var search = $('.searchbar').outerWidth();
        var user = $('.head-user').outerWidth();
        var notify = $('.head-notify').outerWidth();
        var cart = $('.head-cart').outerWidth();

        if (win >= 992) {
            if (notify == undefined){
                notify = 0;
            }
            //console.log(notify);
            //console.log(head - (logo + btns + user + notify + cart) - 100);
            //console.log(head - (logo + btns + user + cart) - 100);

            var tot = head - (logo + btns + user + notify + cart) - 100;

            if (tot > 0){
                $('.searchbar').css("width", tot + "px");

                $('.searchbar input').css("width", (tot - 40) + "px");
            }

            
        }

        
    }, 100);

    

    //setTimeout(function () {
    //    $('#close-bar').click();
    //    //$('#page-title').addClass("pt-9").removeClass("pt-11");

    //}, 8000);
    if (win <= 500) {
        $('.searchbar').addClass("d-none");

        $(window).scroll(function () {

            $("body").removeClass("primary-menu-open");
            $(".main-nav").removeClass("d-block");

        });

        $("#mob-search, .side-panel-trigger").click(function () {

            $("body").removeClass("primary-menu-open");
            $(".main-nav").removeClass("d-block");
            

        });

        $("#primary-menu-trigger").click(function () {

            $(".searchbar").removeClass("active").removeAttr("style").addClass("d-none");

        });


       
        $(window).click(function () {

            $("body").removeClass("primary-menu-open");
            $(".main-nav").removeClass("d-block");

        });

    }
    else {
        $('.searchbar').removeClass("d-none");
    }
    
    $(window).resize(function () {
        var win1 = $(window).width()
        var head1 = $('#header .container').width();
        var logo1 = $('#logo').outerWidth();
        var btns1 = $('.mid-head-btn-blk').outerWidth();
        var search1 = $('.searchbar').outerWidth();
        var user1 = $('.head-user').outerWidth();
        var notify1 = $('.head-notify').outerWidth();
        var cart1 = $('.head-cart').outerWidth();

        if(win1 >= 992){

            if (notify1 == undefined) {
                notify1 = 0;
            }
        var tot1 = head1 - (logo1 + btns1 + user1 + notify1 + cart1) - 100;

        if (tot1 > 0){
            $('.searchbar').css("width", tot1 + "px");

            $('.searchbar input').css("width", (tot1 - 40) + "px");
        }


        
        }
        $('#header #header-wrap .container-fluid .searchbar').css("top", $(window).height() - 60);
        if (win1 <= 500){
            
            if ($(".searchbar").hasClass("active")) {
                $('.searchbar').css("top", $('#header').outerHeight());
            }
            else {
                $('.searchbar').addClass("d-none");
            }
        }
        else {
            $('.searchbar').removeClass("d-none");
        }

        if (win1 > 1600){
            $(".login, #turms, #ship-policy, .cat-grid, .registerfrm").css("min-height", ($(window).height() - ($('footer').outerHeight() + $('#page-title').outerHeight())));
        }
        else {
            $(".login, #turms, #ship-policy, .cat-grid, .registerfrm").removeAttr("style");
        }
        


    });



    $('#header #header-wrap .container-fluid .searchbar').css("top", $(window).height() - 60)
    //var h = $(window).height() - $('#header').outerHeight();
    //$('.dual-banner .row .col-md-6').css("max-height", 600).css("min-height", h);
    $('.log-reg,.head-user').click(function () {
        //$('.side-panel-wrap .side-log').removeClass("d-none");
        $('.side-panel-wrap .side-log').toggleClass("d-none");
        //$('body').toggleClass("side-panel-left");
    });
    $('.cart-sect,.cart-blk,.head-cart').click(function () {
        $('.side-panel-wrap .side-cart').toggleClass("d-none");
        setTimeout(function () {
            $('.side-panel-wrap .side-cart .mast-cart-ttl').toggleClass("open");
        }, 100);

    });
    $('.req-form').click(function () {
        
        $('.side-panel-wrap .side-req').toggleClass("d-none");
        $('.side-req h4').text("Request for sample");
        $('.side-req #schproduct_req').val($('#protitle').text());
        $('.side-req button').text("Send Request");
    });
    $('.req-quote').click(function () {
        console.log($('#protitle').text());
        $('.side-panel-wrap .side-sch').toggleClass("d-none");
        $('.side-sch h4').text("Get Quote/Notify");
        $('.side-sch #schproduct').val($('#protitle').text());
        $('.side-sch button').text("Submit");
    });
    $('.req-sch').click(function () {
        console.log($('#protitle').text());
        $('.side-panel-wrap .side-sch').toggleClass("d-none");
        $('.side-sch h4').text("Remind Me");
        $('.side-sch #schproduct').val($('#protitle').text());
        $('.side-sch button').text("Submit");
    });

    $("#mob-search").click(function () {
        if ($(".searchbar").hasClass("active")){
            $('.searchbar').toggleClass("active").css("top", "0");
            setTimeout(function () {
                $('.searchbar').addClass("d-none");
            },50);
        }
        else {
            $('.searchbar').removeClass("d-none");
            setTimeout(function () {
                $('.searchbar').toggleClass("active").css("top", $('#header').outerHeight());   
            },50);
            
        }
        
    });
    

    $('.body-overlay,#side-panel-trigger-close').click(function () {

        if ($('.side-panel-wrap .side-log').hasClass("d-none")) {

        }
        else {
            $('.side-panel-wrap .side-log').toggleClass("d-none");
            //$('body').toggleClass("side-panel-left");
        }
        if ($('.side-panel-wrap .side-req').hasClass("d-none")) {

        }
        else {
            $('.side-panel-wrap .side-req').toggleClass("d-none");
            //$('body').toggleClass("side-panel-left");
        }
        if ($('.side-panel-wrap .side-cart').hasClass("d-none")) {

        }
        else {
            $('.side-panel-wrap .side-cart').toggleClass("d-none");
            $('.side-panel-wrap .side-cart .mast-cart-ttl').toggleClass("open");
            
        }
        if ($('.side-panel-wrap .side-sch').hasClass("d-none")) {

        }
        else {
            $('.side-panel-wrap .side-sch').toggleClass("d-none");
            $('.side-panel-wrap .side-sch .mast-cart-ttl').toggleClass("open");

        }

    });
    $('.seach-sect').click(function () {
        $('.search-bar').toggleClass("open");
    });
    $('.searchbar button').click(function () {
        $('#header').toggleClass("searchopen");
    });
    setTimeout(function () {
        $("header").removeAttr("style");
    }, 1000);


    //setTimeout(function () {
    //    //var i = 0;
    //    //var oc = $("#top-bar .top-menu a").length;
    //    //$("#top-bar .row:nth-child(2).animated").addClass("fadeIn").removeClass("op-0");
    //    //$("#top-bar.animated").addClass("slideInUp").removeClass("op-0");
    //    ////$("#primary-menu.animated").addClass("slideInLeft");
    //    ////$("#header #header-wrap .container-fluid.animated").addClass("slideInDown").removeClass("op-0");
    //    //$("#slider.animated").addClass("fadeIn");
    //    //if (screen.width <= 479) {
    //    //    $("#logo.animated").addClass("fadeInDown");
    //    //    $("#primary-menu-trigger").addClass("bubble").removeClass("op-0");
    //    //}
    //    //else {
    //    //    $("#logo.animated").addClass("fadeInLeft");
    //    //}

    //    setInterval(function () {
    //        i++;
    //        $("#top-bar .top-menu a:nth-child(" + i + ").animated").addClass("fadeIn").removeClass("op-0");

    //        if (i >= oc) {
    //            clearInterval();
    //            $("#top-bar .country-blk.animated").addClass("fadeIn").removeClass("op-0");
    //            $("#top-bar .social-set a:nth-child(1).animated").addClass("fadeIn").removeClass("op-0");
    //            $("#top-bar .social-set a:nth-child(2).animated").addClass("fadeIn").removeClass("op-0");
    //            $("#top-bar .social-set a:nth-child(3).animated").addClass("fadeIn").removeClass("op-0");
    //            //var os = $("#top-bar .social-set a").length;
    //            //for (var j = 1; j >= os; j++) {
    //            //    $("#top-bar .social-set a:nth-child(" + j + ").animated").addClass("fadeIn").removeClass("op-0");
    //            //}
    //            //setInterval(function () {
    //            //    i++;
    //            //    $("#top-bar .social-set a:nth-child(" + i + ").animated").addClass("fadeIn").removeClass("op-0");
    //            //    if (i >= os){
    //            //        clearInterval();
    //            //    }
    //            //},500);
    //        }
    //    }, 500);
    //    $(".right-menu .animated,#top-bar .row:nth-child(1).animated,#top-bar .row:nth-child(2).animated,#logo.animated,#slider.animated,#primary-menu.animated").removeClass("op-0");

    //}, 2000);

    var distance = 0;
    setTimeout(function () {
        distance = $('#header').offset().top;
    },200);
    
    var distance1 = 0;
    
    $(window).scroll(function () {
        //var scroll = $(window).offset().top;
        var win1 = $(window).width();
        var slid = $('#slider').outerHeight()
            if ($(this).scrollTop() >= distance) {

                

                $('#header').addClass("fixed-head");
            }

            //if (scroll > 10 && scroll < 50) {
            //    $('html, body').animate({
            //        scrollTop: $('#header').offset().top //#DIV_ID is an example. Use the id of your destination on the page
            //    }, 3000);
            //}


            if (win1 >= 480 && $(this).scrollTop() >= distance + 20){
                $('#header').addClass("hide-bottom");
                $('#tab-btn').removeClass('hide-tb-btns');
            }
            if (win1 >= 480 && $(this).scrollTop() < distance1) {
                $('#header').removeClass("hide-bottom");
                $('#tab-btn').addClass('hide-tb-btns');
            }
            else {
                
            }
            distance1 = $(this).scrollTop();
            if ($(this).scrollTop() <= distance) {
                $('#header').removeClass("fixed-head");
            }

            if ($(this).scrollTop() >= slid) {
                //$("#slider.full-screen").css("display", "none");
                $("#slider.full-screen").remove();

                $('.dual-banner').animate({
                    'paddingTop': $('#header').outerHeight(),
                    'marginTop': 0,
                }, 00)
            }
            

        //if ($('.cat-grid').visible(true)){
        //    var cg = $('.cat-grid .product-grid').length;
        //    setTimeout(function () {
        //        for (var a = 1; a <= cg + 1; a++) {

        //            $('.cat-grid .pr-grd .col-md-3.animated:nth-child(' + a + ')').addClass("zoomIn").removeClass("op-0");
        //        }
        //    },700);
            
        //    var cgl = $('.cat-grid .side-tab ul li').length;
        //    var b = 0;
        //    setInterval(function () {

        //        b++;
        //        $('.cat-grid .side-tab ul li:nth-child(' + b + ')').addClass("slideInRight").removeClass("op-0");
        //        if (b >= cgl) {
        //            clearInterval();
        //            $('.cat-grid .side-tab h5').addClass("fadeIn").removeClass("op-0");
        //            $('.cat-grid .pr-grd .col-md-12 h5, .cat-grid .pr-grd .col-md-12 a').addClass("fadeInDown").removeClass("op-0");
        //        }
        //    }, 500);
        //}

        //if ($('header').visible(true)) {
        //    setTimeout(function () {
        //        $("#header").addClass("slideInUp").removeClass("op-0");
        //    },500)
            
        //}
        
        

        //if ($('header').visible(true)) {
        //    $("#header").addClass("slideInUp").removeClass("op-0");
        //}

        //if ($('.dual-banner').visible(true)) {
        //    setTimeout(function () {
        //        $('.dual-banner .col-md-6:nth-child(1)').addClass("fadeInLeft").removeClass("op-0");
        //        $('.dual-banner .col-md-6:nth-child(2)').addClass("fadeInRight").removeClass("op-0");
        //    },1500);
            
        //}

        //if ($('.latest-group').visible(true)) {
        //    setTimeout(function () {
        //        $('.latest img').addClass("zoomIn").removeClass("op-0");
        //    }, 1000);
        //    setTimeout(function () {
        //        $('.latest .grid-info').addClass("fadeInLeft").removeClass("op-0");
        //    }, 2000);
        //}

        //if ($('.cat-big-grid').visible(true)) {
        //    setTimeout(function () {
        //        $('.cat-big-grid .heading-block').addClass("fadeInDown").removeClass("op-0");
        //    }, 1000);
        //    setTimeout(function () {
        //        $('.cat-big-grid .ecommerce-categories>div').addClass("zoomIn").removeClass("op-0");
        //    }, 1500);
        //}


        //if ($('.tabs').visible(true)) {

        //    var c = 0;
        //    setInterval(function () {
        //        c++;
        //        $('.tabs ul li:nth-child(' + c + ')').addClass("fadeInDown").removeClass("op-0");
        //        if (c >= 3) {
        //            clearInterval();
        //            $('.tabs .slider').addClass("fadeInUp").removeClass("op-0");
        //        }
        //        $('.tabs .content .animated').addClass("zoomIn").removeClass("op-0");

        //    })
            
            
        //}

        //if ($('.cat-icon-grid').visible(true)) {
        //    setTimeout(function () {
        //        $('.cat-icon-grid .heading-block').addClass("fadeInDown").removeClass("op-0");
        //    }, 500);
        //    setTimeout(function () {
        //        $('.cat-icon-grid .icon-cat-grid > div h3').addClass("fadeInUp").removeClass("op-0");
        //    }, 1500);
        //    setTimeout(function () {
        //        $('.cat-icon-grid .icon-cat-grid > div .fbox-image').addClass("fadeInDown").removeClass("op-0");
        //    }, 2000);
        //}


        //if ($('#footer').visible(true)) {
        //    $('#footer .subscribe-widget').addClass("fadeInDown").removeClass("op-0");
        //    $('#footer .footer-about').addClass("fadeIn").removeClass("op-0");
        //    $('#footer .policy h5,#footer .quick-links h5,#footer .misc h5,#footer .home-bakr img').addClass("fadeIn").removeClass("op-0");

        //    var fl = 0;

        //    setInterval(function () {
        //        fl++;
        //        $('#footer .policy ul li:nth-child(' + fl + '),#footer .quick-links ul li:nth-child(' + fl + '),#footer .misc ul li:nth-child(' + fl + ')').addClass("fadeInUp").removeClass("op-0");
        //        $('#footer .home-bakr ul li:nth-child(' + fl + ')').addClass("fadeInRight").removeClass("op-0");
        //        if (fl > 5) {
        //            clearInterval();
        //        }
        //    }, 500);

        //    setTimeout(function () {
        //        $('#footer .addr').addClass("fadeInUp").removeClass("op-0");
        //        var bfl = 0;
        //        setInterval(function () {
                    
        //            $('#footer .addr h6,#footer .addr h6').addClass("fadeIn").removeClass("op-0");
        //            bfl++;
        //            $('#footer .addr ul li:nth-child(' + bfl + ')').addClass("fadeInUp").removeClass("op-0");

        //            if (bfl > 3) {
        //                $('#footer .bottom-foot').addClass("fadeInUp").removeClass("op-0");
        //            }

        //        },500);
                
        //    },2000);
        //}

        //if (window.scrollY > 400) {
        //    var cg = $('.cat-grid .product-grid').length;
        //    for (var a = 1; a <= cg + 1; a++){

        //        $('.cat-grid .pr-grd .col-md-3.animated:nth-child(' + a + ')').addClass("zoomIn").removeClass("op-0");
        //    }
        //    var cgl = $('.cat-grid .side-tab ul li').length;
        //    var b = 0;
        //    setInterval(function () {
                
        //        b++;
        //        $('.cat-grid .side-tab ul li:nth-child(' + b + ')').addClass("slideInRight").removeClass("op-0");
        //        if (b >= cgl) {
        //            clearInterval();
        //            $('.cat-grid .side-tab h5').addClass("fadeIn").removeClass("op-0");
        //            $('.cat-grid .pr-grd .col-md-12 h5, .cat-grid .pr-grd .col-md-12 a').addClass("fadeInDown").removeClass("op-0");
        //        }
        //    },500);
        //}
        //if (window.scrollY > 900){
        //    $('.about .col-md-6:nth-child(1)').addClass("fadeInLeft").removeClass("op-0");
        //    $('.about .col-md-6:nth-child(2)').addClass("fadeInRight").removeClass("op-0");
        //}
        
    });

    //if (/Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent)) {
        
    //}
    if (navigator.userAgent.match(/(\(iPod|\(iPhone|\(iPad)/)) {
        
    }

    $('#vertical-nav nav ul li:has(ul)').addClass('sub-menu');
    $('#vertical-nav nav ul li:has(ul) > a').append(' <i class="icon-angle-down"></i>');

    $('#vertical-nav nav ul li:has(ul) > a').click(function (e) {
        var element = $(this);
        $('#vertical-nav nav ul li').not(element.parents()).removeClass('active');
        element.parent().children('ul').slideToggle(function () {
            $(this).find('ul').hide();
            $(this).find('li.active').removeClass('active');
        });
        $('#vertical-nav nav ul li > ul').not(element.parent().children('ul')).not(element.parents('ul')).slideUp();
        element.parent('li:has(ul)').toggleClass('active');
        e.preventDefault();
    });
    
    //Mobile Verification popup

    $("#searchbtn").click(function () {
        search();
    });
    
    $("#topsearch").on('keyup', function (e) {
        if (e.keyCode == 13) {
            // Do something
            search();
        }
    });

    $("#forgot").click(function () {
        var ml = "";
        if ($(".login input[name=Email]").val() != "") {
            ml = $(".login input[name=Email]").val();
        }

        

        $.confirm({
            title: 'Forgot Password',
            //theme: 'supervan',
            //content: function () {
            //    
            //    var self = this;
            //    self.setContentAppend('Checking callback flow');
            //    //
            //    //var ml = "";
            //    //if ($(".login input[name=mail]").val() != "") {
            //    //    ml = $(".login input[name=mail]").val();
            //    //}
            //    //self.setContentAppend('<form action="" class="form-sub mb-0"><div class="form-group"><input type="email" placeholder="Email Id" class="form-control" required /></div></form>');
            //},
            content: '<form action="" class="form-sub mb-0"><div class="form-group"><input type="email" name="mail" placeholder="Email Id" value="' + ml +'" class="form-control mb-0" required /></div></form>',
            //content: function () {
            //    var self = this;
            //    return $.ajax({
            //        type: "POST",
            //        url: "/Access/SendOtp",
            //        //data: '{Compname: "' + Compname + '", Fullname: "' + Fullname + '", Email: "' + Email + '", Mobile:"' + Mobile + '", Password: "' + Passsword + '",Type:"' + mtype + '", Bname:"' + Bname + '", Bemail:"' + Bemail + '", Bmobile:"' + Bmobile + '", Bstreet:"' + Bstreet + '", Bpin:"' + Bpin + '", Bcity:"' + Bcity + '", Bstate:"' + Bstate + '", , Sname:"' + Sname + '", Semail:"' + Semail + '", Smobile:"' + Smobile + '", Sstreet:"' + Sstreet + '", Spin:"' + Spin + '", Scity:"' + Scity + '", Sstate:"' + Sstate + '", step:"' + step + '"  }',
            //        data: '{Id: "' + id + '"}',
            //        contentType: "application/json; charset=utf-8",
            //        dataType: "json",
            //        success: function (response) {
            //            if (response[0].Resp != "Request Timeout") {
            //                self.setContentAppend('<form action="" class="formName"><div class="form-group"><label>Enter OTP Code</label><input type="text" placeholder="OTP" class="otp form-control" required /></div></form>');
            //                self.setContentAppend('<small class="text-danger">OTP has sent on ' + response[0].Number + '</small>');
            //            }
            //            else {

            //            }
            //        },
            //    }).done(function (response) {
            //        true;
            //    }).fail(function () {
            //        false;
            //    });
            //},
            contentLoaded: function (data, status, xhr) {
                //if (data != "Request Timeout") {
                //    $(function () {
                //        //var res = jQuery("#" + '<%= hfresend.ClientID%>').val();
                //        //if (res == 1) {

                //        //}
                //        $('.btn-resend').timedDisable(120);
                //    });

                //}
            },
            closeIcon: true,
            closeIconClass: 'close-dialog',
            backgroundDismiss:true,
            buttons: {
                formSubmit: {
                    text: 'Send',
                    btnClass: 'subsc',
                    action: function () {
                        
                        var VAL = this.$content.find('input').val();
                        $('.jconfirm-content label.text-danger').remove();
                        //alert(VAL);
                        var email = /^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$/i;
                        //alert(email);
                        if (VAL != "" && email.test(VAL)) {
                            $.ajax({
                            type: "POST",
                            url: "/Access/ForgotPass",
                            //data: '{Compname: "' + Compname + '", Fullname: "' + Fullname + '", Email: "' + Email + '", Mobile:"' + Mobile + '", Password: "' + Passsword + '",Type:"' + mtype + '", Bname:"' + Bname + '", Bemail:"' + Bemail + '", Bmobile:"' + Bmobile + '", Bstreet:"' + Bstreet + '", Bpin:"' + Bpin + '", Bcity:"' + Bcity + '", Bstate:"' + Bstate + '", , Sname:"' + Sname + '", Semail:"' + Semail + '", Smobile:"' + Smobile + '", Sstreet:"' + Sstreet + '", Spin:"' + Spin + '", Scity:"' + Scity + '", Sstate:"' + Sstate + '", step:"' + step + '"  }',
                            data: '{mail: "' + VAL + '"}',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            beforeSend: function () {
                                
                                $(".close-dialog").click();
                                $.dialog({
                                    title: false,
                                    content: '<div class="checkbox" style="text-align:center;"><label style="font-size: 17px;margin: 0;padding: 0;margin-top: 5px;color: #848484;">Finding Account... <i class="fas fa-circle-notch fa-spin"></i></label></div>',
                                    closeIconClass: 'close-dialog',
                                });
                            },
                            success: function (response) {
                                res = response;
                                $(".close-dialog").click();
                                if (response == "true") {
                                    ms = '<label style="font- size: 12px;margin: 0;padding: 0;margin-top: 5px;color: #848484; ">Password sent to your email id</label>';
                                }
                                else if (response == "false"){
                                    ms = '<label style="font- size: 12px;margin: 0;padding: 0;margin-top: 5px;color: #848484; ">Incorrect Email Id</label>';
                                }
                                else {
                                    ms = response;
                                }
                                $.dialog({
                                    title: false,
                                    content: ms,
                                    closeIconClass: 'close-dialog',
                                });
                                setTimeout(function () {
                                    $(".close-dialog").click();
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
                        else if (VAL != ""){
                            this.setContentAppend('<label class="text-danger" style="font-size:11px">This is Invalid</label>');
                        }
                        else {
                            this.setContentAppend('<label class="text-danger" style="font-size:11px">This is required</label>');
                        }
                        //this.setContentAppend("required");
                        //var otp = this.$content.find('.otp').val();
                        //var ms = "";
                        //var res = "";
                        //$.ajax({
                        //    type: "POST",
                        //    url: "/Access/VerifyMob",
                        //    //data: '{Compname: "' + Compname + '", Fullname: "' + Fullname + '", Email: "' + Email + '", Mobile:"' + Mobile + '", Password: "' + Passsword + '",Type:"' + mtype + '", Bname:"' + Bname + '", Bemail:"' + Bemail + '", Bmobile:"' + Bmobile + '", Bstreet:"' + Bstreet + '", Bpin:"' + Bpin + '", Bcity:"' + Bcity + '", Bstate:"' + Bstate + '", , Sname:"' + Sname + '", Semail:"' + Semail + '", Smobile:"' + Smobile + '", Sstreet:"' + Sstreet + '", Spin:"' + Spin + '", Scity:"' + Scity + '", Sstate:"' + Sstate + '", step:"' + step + '"  }',
                        //    data: '{Otp: "' + otp + '"}',
                        //    contentType: "application/json; charset=utf-8",
                        //    dataType: "json",
                        //    success: function (response) {
                        //        res = response;
                        //        if (response == "Verified") {
                        //            ms = "Thankyou for verifying your mobile number."
                        //        }
                        //        else if (response == "Unverified") {
                        //            ms = "Entered OTP is expired or incorrect."
                        //        }
                        //        else if (response == "Timeout") {
                        //            ms = "The entered otp is expired. we have sent you a new OTP."
                        //        }
                        //        else if (response == "Something went wrong") {
                        //            ms = "Please Login and try again verifing your number."
                        //        }
                        //        else {
                        //            ms = "Please Login and try again verifing your number."
                        //        }

                        //        $.dialog({
                        //            title: false,
                        //            content: ms,
                        //            closeIconClass: 'close-dialog',
                        //        });
                        //        setTimeout(function () {
                        //            $(".close-dialog").click();
                        //        }, 5000);
                        //        setTimeout(function () {
                        //            if (document.referrer != "" && document.referrer != undefined) {
                        //                window.location.href = document.referrer;
                        //            }
                        //            else {
                        //                window.location.href = "/";
                        //            }
                        //        }, 6000);
                        //    },
                        //});

                        //setInterval(function () {
                        //    if (res == "Verified") {
                        //        $(".close-verify").click();
                        //    }
                        //}, 2000);
                        //alert();
                        return false;
                    }
                },
                //resend: {
                //    text: 'Cancel',
                //    btnClass: 'btn-resend',
                //    action: function () {
                //        //$.ajax({
                //        //    type: "POST",
                //        //    url: "/Access/SendOtp",
                //        //    //data: '{Compname: "' + Compname + '", Fullname: "' + Fullname + '", Email: "' + Email + '", Mobile:"' + Mobile + '", Password: "' + Passsword + '",Type:"' + mtype + '", Bname:"' + Bname + '", Bemail:"' + Bemail + '", Bmobile:"' + Bmobile + '", Bstreet:"' + Bstreet + '", Bpin:"' + Bpin + '", Bcity:"' + Bcity + '", Bstate:"' + Bstate + '", , Sname:"' + Sname + '", Semail:"' + Semail + '", Smobile:"' + Smobile + '", Sstreet:"' + Sstreet + '", Spin:"' + Spin + '", Scity:"' + Scity + '", Sstate:"' + Sstate + '", step:"' + step + '"  }',
                //        //    data: '{Id: "' + id + '"}',
                //        //    contentType: "application/json; charset=utf-8",
                //        //    dataType: "json",
                //        //    success: function (response) {
                //        //        //if (response != "Request Timeout") {
                //        //        //    self.setContentAppend('<form action="" class="formName"><div class="form-group"><label>Enter OTP Code</label><input type="text" placeholder="OTP" class="otp form-control" required />resend (<span class="Counter"></span>)</div></form>');
                //        //        //}
                //        //        //else {

                //        //        //}
                //        //    },
                //        //});
                //    }
                //},
                //cancel: function () {
                //    //close
                //},
            },
            onContentReady: function () {
                // bind to events
                //var jc = this;
                //this.$content.find('form').on('submit', function (e) {
                //    // if the user submits the form by pressing enter in the field.
                //    e.preventDefault();
                //    jc.$$formSubmit.trigger('click'); // reference the button and click it
                //});
            }
        });
    });

    var showChar = 120;  // How many characters are shown by default
    var ellipsestext = "...";
    var moretext = "Show more";
    var lesstext = "Show less";


    

    $("#btn-Credit").click(function () {
        $.confirm({
            title: 'Apply for Credit',
            //theme: 'supervan',

            content: '<form action="" class="form-sub mb-0"><div class="form-group"><label class="mb-0">Message</label><textarea name="Message" placeholder="Message" class="sm-form-control" required ></textarea></div></form>',
            
            contentLoaded: function (data, status, xhr) {
                
            },
            closeIcon: true,
            backgroundDismiss: true,
            closeIconClass: 'd-block',
            buttons: {
                formSubmit: {
                    text: 'Submit',
                    btnClass: 'btn btn-success bg-success text-white float-right text-center',
                    action: function () {
                        
                        var Message = this.$content.find('[name=Message]').val();
                        $('.jconfirm-content label.text-danger').remove();

                        var ty = "Credit";
                        var mess = Message;

                        if (Message == "") {
                            this.setContentAppend('<label class="text-danger" style="font-size:11px">Message is required</label>');
                        }
                        else if (Message != "") {
                            $.ajax({
                                url: '/Access/ApplyCredit',
                                type: "Post",
                                data: { t: ty, m: mess },
                                beforeSend: function () {

                                    $(".jconfirm-closeIcon").click();
                                    $.dialog({
                                        title: false,
                                        content: '<div class="checkbox" style="text-align:center;"><label style="font-size: 15px;margin: 0;padding: 0;margin-top: 5px;color: #848484;">Sending Request...<i class="fas fa-circle-notch fa-spin"></i></label></div>',
                                        closeIconClass: 'close-dialog',
                                    });
                                },
                                success: function (response) {
                                    res = response;
                                    if (response == "true") {
                                        ms = '<label style="font- size: 12px;margin: 0;padding: 0;margin-top: 5px;color: #848484; ">Thank You. We have received your request.</label>';
                                    }
                                    else {
                                        ms = '<label style="font- size: 12px;margin: 0;padding: 0;margin-top: 5px;color: #848484; ">Something went wrong. Please contact our support team.</label>';
                                    }

                                    $.dialog({
                                        title: false,
                                        content: ms,
                                        closeIconClass: 'close-dialog',
                                    });
                                    setTimeout(function () {
                                        $(".close-dialog, .jconfirm-closeIcon").click();
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
                            //$(".close-dialog, .jconfirm-closeIcon").click();
                        }
                       

                        return false;
                    }
                },
                cancel: function () {
                    //close
                },
            },
            onContentReady: function () {
               
            }
        });
    });

    $('.nocpc').bind('copy paste cut', function (e) {
        e.preventDefault(); //disable cut,copy,paste
        //alert('cut,copy & paste options are disabled !!');
    });

    $('input[type=text],input[type=email]').on("blur change",function () {
        this.value = this.value.trim();
    });

    setTimeout(function () {
        $("#popup-all").addClass("d-block");
    }, 5000);


    $(".close-popup-all").on("click", function () {

        var pr = this.classList[0].replace("close-", "");
        
        $.ajax({
            type: "POST",
            url: "/home/closepopup",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (response == true) {
                    $("#" + pr).remove();
                }
                else {

                }
            },
        })

    })


});


function charkey(evt) {
       
    //alert((evt.length - 1).value);
    //alert([evt.value.length - 1]);
    //var sdcsd = evt.value;
    //var str = evt.value;
    //var res = evt.value.charAt([evt.value.length - 1])
    //alert(res);
    //var lastChar = myString[myString.length - 1];
    //alert(lastChar);
    //alert(evt.value.charAt(evt.value.length));
   // alert()

    var myString= evt.value;
    var charCode = (evt.which) ? evt.which : event.keyCode

    if (myString.length == 0) {
        if (charCode == 32) {
            return false;
        }
    }
    else {
        var lastChar = myString[myString.length - 1];
        if (charCode == 32 && lastChar == ' ') {
            return false;
        }

    }

    if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123) || (charCode == 32)) {
        return true;
    }
    return false;
}


function numkey(evt) {
    
    var charCode = (evt.which) ? evt.which : event.keyCode
    //alert(charCode);
    if ((charCode > 47 && charCode < 58)) {
        return true;
    }
    return false;
}


function numwithdotkey(evt) {

    var charCode = (evt.which) ? evt.which : event.keyCode
    if ((charCode > 47 && charCode < 58) || charCode == 46 ) {
        return true;
    }
    return false;
}

function ngumericwithdecimal(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode
    if ((charCode > 47 && charCode < 58) || (charCode == 46 && evt.value.includes(".") != true)) {
        return true;
    }
    return false;
}

     $(".allownumericwithdecimal").on("keypress keyup blur", function (event) {
            //this.value = this.value.replace(/[^0-9\.]/g,'');
            $(this).val($(this).val().replace(/[^0-9\.]/g, ''));
            if ((event.which != 46 || $(this).val().indexOf('.') != -1) && (event.which < 48 || event.which > 57)) {
                event.preventDefault();
            }
        });

function Qtynumkey(evt) {
   // ; debugger
    var x = evt.value;
    var charCode = (evt.which) ? evt.which : event.keyCode
    if (x.length >= 3 && (charCode > 47 && charCode < 58)) {
        return false;
    }
    if ((charCode > 47 && charCode < 58)) {
        return true;
    }
    return false;
}

//var catcher = jQuery('#catcher');
//var sticky = jQuery('#cat-side');
//jQuery(window).scroll(function () {
//    var scroll = jQuery(window).scrollTop();
//    if (scroll >= 200) {
//        var elementTop = jQuery('#footer').offset().top + 10;
//        var elementBottom = elementTop + jQuery('#footer').outerHeight();

//        var viewportTop = jQuery(window).scrollTop();
//        var viewportBottom = viewportTop + jQuery(window).height();

//        if (elementBottom > viewportTop && elementTop < viewportBottom) {

//        } else {
//            chkscroll = scroll - 180;
//            scroll = scroll - (jQuery('#header').outerHeight() + 150);

//            sticky.attr('style', 'position:absolute;top:' + scroll + 'px;right:0;');

//            //jQuery('.chkout-ttl').attr('style', 'position:absolute;top:' + chkscroll + 'px;right:0;');
//        }

//    }
//    else {
//        sticky.removeAttr('style');
//    }
//});

//var input = document.getElementById("topsearch");
//input.addEventListener("keyup", function (event) {
//    event.preventDefault();
//    if (event.keyCode === 13) {
//        alert();
//        //document.getElementById("myBtn").click();
//    }
//});

function search() {
    var path = window.location.pathname;
    var paths = path.split("/");
    var search = $("#topsearch").val();
    search = $.trim(search);
    search = search.replace(/ /g, "-");
    if (paths[1] != "search") {
        if (search.length > 0) {

            window.location.href = "/search/" + search;
        }
    }
    else if (paths[1] == "search") {
        //get(search)
        window.location.href = "/search/" + search;

    }
}


function VerifyMob(id) {
    $.confirm({
        title: 'Verify your Mobile Number',
        //theme: 'supervan',
        //content: '' +
        //'<form action="" class="formName">' +
        //'<div class="form-group">' +
        //'<label>Enter OTP Code</label>' +
        //'<input type="text" placeholder="OTP" class="otp form-control" required />' +
        //'resend (<span class="Counter"></span>)'+
        //'</div>' +
        //'</form>',
        content: function () {
            var self = this;
            return $.ajax({
                type: "POST",
                url: "/Access/SendOtp",
                //data: '{Compname: "' + Compname + '", Fullname: "' + Fullname + '", Email: "' + Email + '", Mobile:"' + Mobile + '", Password: "' + Passsword + '",Type:"' + mtype + '", Bname:"' + Bname + '", Bemail:"' + Bemail + '", Bmobile:"' + Bmobile + '", Bstreet:"' + Bstreet + '", Bpin:"' + Bpin + '", Bcity:"' + Bcity + '", Bstate:"' + Bstate + '", , Sname:"' + Sname + '", Semail:"' + Semail + '", Smobile:"' + Smobile + '", Sstreet:"' + Sstreet + '", Spin:"' + Spin + '", Scity:"' + Scity + '", Sstate:"' + Sstate + '", step:"' + step + '"  }',
                data: '{Id: "'+id+'"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response[0].Resp != "Request Timeout") {
                        self.setContentAppend('<form action="" class="formName"><div class="form-group"><label>Enter OTP Code</label><input type="text" placeholder="OTP" maxlength="6" class="otp form-control" required /><span style="color:red;"></span></div></form>');
                        self.setContentAppend('<small class="text-danger">OTP has been sent on ' + response[0].Number+'</small>');
                    }
                    else {

                    }
                },
            }).done(function (response) {
                true;
            }).fail(function () {
                false;
            });
        },
        contentLoaded: function (data, status, xhr) {
            if (data != "Request Timeout") {
                $(function () {
                    //var res = jQuery("#" + '<%= hfresend.ClientID%>').val();
                    //if (res == 1) {

                    //}
                 //   $('.btn-resend').timedDisable(120);  - zayan 15sept23
                });
                
            }
        },
        closeIcon: true,
        //closeIconClass: 'close-verify',
        buttons: {
            formSubmit: {
                text: 'Submit',
                btnClass: 'btn-blue',
                action: function () {
                    var otp = this.$content.find('.otp').val();
                    var ms = "";
                    var res = "";
                    var intrvl;
                    $.ajax({
                        type: "POST",
                        url: "/Access/VerifyMob",
                        beforeSend: function () {
                            $(".jconfirm-buttons button.btn-blue").attr("disabled", "disabled");
                        },
                        //data: '{Compname: "' + Compname + '", Fullname: "' + Fullname + '", Email: "' + Email + '", Mobile:"' + Mobile + '", Password: "' + Passsword + '",Type:"' + mtype + '", Bname:"' + Bname + '", Bemail:"' + Bemail + '", Bmobile:"' + Bmobile + '", Bstreet:"' + Bstreet + '", Bpin:"' + Bpin + '", Bcity:"' + Bcity + '", Bstate:"' + Bstate + '", , Sname:"' + Sname + '", Semail:"' + Semail + '", Smobile:"' + Smobile + '", Sstreet:"' + Sstreet + '", Spin:"' + Spin + '", Scity:"' + Scity + '", Sstate:"' + Sstate + '", step:"' + step + '"  }',
                        data: '{Otp: "' + otp + '"}',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            res = response;
                            if (response == "Verified") {
                                ms = "Thank you for verifying your mobile no. Your Registration Request has been submitted Successfully."

                                setTimeout(function () {
                                    clearInterval(intrvl);
                                    VerifyEmail(id);
                                },2500);
                                
                            }
                            else if (response == "Unverified") {
                             //   debugger;
                                ms = "Invalid verification code"
                                //$(".jconfirm-buttons button.btn-blue").removeAttr("disabled");
                                //self.innerText("Invalid verification code");
                                $.dialog({
                                    title: false,
                                    content: ms,
                                    closeIconClass: 'close-dialog',
                                });
                            }
                            else if (response == "Timeout") {
                                ms = "Verification code has been expired. we have sent you a new OTP."
                                $.dialog({
                                    title: false,
                                    content: ms,
                                    closeIconClass: 'close-dialog',
                                });
                            }
                            else if (response == "Something went wrong") {
                                ms = "Please login and try again verifing your number."
                                $.dialog({
                                    title: false,
                                    content: ms,
                                    closeIconClass: 'close-dialog',
                                });
                            }
                            else {
                                ms = "Please login and try again verifing your number."
                                $.dialog({
                                    title: false,
                                    content: ms,
                                    closeIconClass: 'close-dialog',
                                });
                            }

                            
                            setTimeout(function () {
                                $(".close-dialog").click();
                                if ($("input[name=login]").val() == "Logging..."){
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

                    intrvl =  setInterval(function () {
                        if (res == "Verified") {
                            $(".jconfirm-closeIcon").click();
                        }
                    },2000);

                    return false;
                }
            },
            resend: {
                text: 'Resend OTP',
                btnClass: 'btn-resend',
                action: function () {
                    $.ajax({
                        type: "POST",
                        url: "/Access/SendOtp",
                        //data: '{Compname: "' + Compname + '", Fullname: "' + Fullname + '", Email: "' + Email + '", Mobile:"' + Mobile + '", Password: "' + Passsword + '",Type:"' + mtype + '", Bname:"' + Bname + '", Bemail:"' + Bemail + '", Bmobile:"' + Bmobile + '", Bstreet:"' + Bstreet + '", Bpin:"' + Bpin + '", Bcity:"' + Bcity + '", Bstate:"' + Bstate + '", , Sname:"' + Sname + '", Semail:"' + Semail + '", Smobile:"' + Smobile + '", Sstreet:"' + Sstreet + '", Spin:"' + Spin + '", Scity:"' + Scity + '", Sstate:"' + Sstate + '", step:"' + step + '"  }',
                        data: '{Id: "' + id + '"}',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            //if (response != "Request Timeout") {
                            //    self.setContentAppend('<form action="" class="formName"><div class="form-group"><label>Enter OTP Code</label><input type="text" placeholder="OTP" class="otp form-control" required />resend (<span class="Counter"></span>)</div></form>');
                            //}
                            //else {
                            $('.btn-resend').timedDisable(120);
                            //}
                        },
                    });
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
}

function VerifyEmail(id) {

    var self = this;
    return $.ajax({
        type: "POST",
        url: "/Access/SendVerifyEmail",
        //data: '{Compname: "' + Compname + '", Fullname: "' + Fullname + '", Email: "' + Email + '", Mobile:"' + Mobile + '", Password: "' + Passsword + '",Type:"' + mtype + '", Bname:"' + Bname + '", Bemail:"' + Bemail + '", Bmobile:"' + Bmobile + '", Bstreet:"' + Bstreet + '", Bpin:"' + Bpin + '", Bcity:"' + Bcity + '", Bstate:"' + Bstate + '", , Sname:"' + Sname + '", Semail:"' + Semail + '", Smobile:"' + Smobile + '", Sstreet:"' + Sstreet + '", Spin:"' + Spin + '", Scity:"' + Scity + '", Sstate:"' + Sstate + '", step:"' + step + '"  }',
        //data: '{Id: "' + id + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response == true) {
                //self.setContentAppend('<h4 style="margin-bottom:15px">Verification email has been sent to your registered Email ID</h4>');
                // email popup - zayan sept23 - self.setContentAppend('<p style="margin-bottom:0px">An email has been sent to your email address, kindly verify it before login.</p>');
                //self.setContentAppend('<p style="margin-bottom:0px">Please verify your email to complete your registration</p>');
            }
            else {

            }
        },
    }).done(function (response) {
        true;
    }).fail(function () {
        false;
        });

}

function VerifyEmailWithPopUp(id) {
    $.confirm({
        title: false,
        //theme: 'supervan',
        //content: '' +
        //'<form action="" class="formName">' +
        //'<div class="form-group">' +
        //'<label>Enter OTP Code</label>' +
        //'<input type="text" placeholder="OTP" class="otp form-control" required />' +
        //'resend (<span class="Counter"></span>)'+
        //'</div>' +
        //'</form>',
        content: function () {
            var self = this;
            return $.ajax({
                type: "POST",
                url: "/Access/SendVerifyEmail",
                //data: '{Compname: "' + Compname + '", Fullname: "' + Fullname + '", Email: "' + Email + '", Mobile:"' + Mobile + '", Password: "' + Passsword + '",Type:"' + mtype + '", Bname:"' + Bname + '", Bemail:"' + Bemail + '", Bmobile:"' + Bmobile + '", Bstreet:"' + Bstreet + '", Bpin:"' + Bpin + '", Bcity:"' + Bcity + '", Bstate:"' + Bstate + '", , Sname:"' + Sname + '", Semail:"' + Semail + '", Smobile:"' + Smobile + '", Sstreet:"' + Sstreet + '", Spin:"' + Spin + '", Scity:"' + Scity + '", Sstate:"' + Sstate + '", step:"' + step + '"  }',
                //data: '{Id: "' + id + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response == true) {
                        self.setContentAppend('<h4 style="margin-bottom:15px">Verification email has been sent to your registered Email ID</h4>');
                        // email popup - zayan sept23 - self.setContentAppend('<p style="margin-bottom:0px">An email has been sent to your email address, kindly verify it before login.</p>');
                        self.setContentAppend('<p style="margin-bottom:0px">Please verify your email to complete your registration</p>');
                    }
                    else {

                    }
                },
            }).done(function (response) {
                true;
            }).fail(function () {
                false;
            });
        },
        contentLoaded: function (data, status, xhr) {
            if (data != "Request Timeout") {
                $(function () {
                    var res = jQuery("#" + '<%= hfresend.ClientID%>').val();
                    if (res == 1) {

                    }
                  $('.btn-resend').timedDisable(120);
                });

            }
        },
        closeIcon: true,
        //closeIconClass: 'close-verifymail',
        buttons: {
            //formSubmit: {
            //    text: 'Submit',
            //    btnClass: 'btn-blue',
            //    action: function () {
            //        var otp = this.$content.find('.otp').val();
            //        var ms = "";
            //        var res = "";
            //        $.ajax({
            //            type: "POST",
            //            url: "/Access/VerifyMob",
            //            beforeSend: function () {
            //                $(".jconfirm-buttons button.btn-blue").attr("disabled", "disabled");
            //            },
            //            //data: '{Compname: "' + Compname + '", Fullname: "' + Fullname + '", Email: "' + Email + '", Mobile:"' + Mobile + '", Password: "' + Passsword + '",Type:"' + mtype + '", Bname:"' + Bname + '", Bemail:"' + Bemail + '", Bmobile:"' + Bmobile + '", Bstreet:"' + Bstreet + '", Bpin:"' + Bpin + '", Bcity:"' + Bcity + '", Bstate:"' + Bstate + '", , Sname:"' + Sname + '", Semail:"' + Semail + '", Smobile:"' + Smobile + '", Sstreet:"' + Sstreet + '", Spin:"' + Spin + '", Scity:"' + Scity + '", Sstate:"' + Sstate + '", step:"' + step + '"  }',
            //            data: '{Otp: "' + otp + '"}',
            //            contentType: "application/json; charset=utf-8",
            //            dataType: "json",
            //            success: function (response) {
            //                res = response;
            //                if (response == "Verified") {
            //                    ms = "Thank you for verifying your mobile no. Your Registration Request has been submitted Sucessfully."
            //                }
            //                else if (response == "Unverified") {
            //                    ms = "Invalid verification code"
            //                }
            //                else if (response == "Timeout") {
            //                    ms = "Verification code has been expired. we have sent you a new OTP."
            //                }
            //                else if (response == "Something went wrong") {
            //                    ms = "Please login and try again verifing your number."
            //                }
            //                else {
            //                    ms = "Please login and try again verifing your number."
            //                }

            //                $.dialog({
            //                    title: false,
            //                    content: ms,
            //                    closeIconClass: 'close-dialog',
            //                });
            //                setTimeout(function () {
            //                    $(".close-dialog").click();
            //                    if ($("input[name=login]").val() == "Logging...") {
            //                        $("input[name=login]").val("Login").removeAttr("disabled");
            //                    }
            //                    $(".wizard-card form")[0].reset();
            //                    $(".wizard-card form input[name=Email]").focus();
            //                }, 5000);
            //                //setTimeout(function () {
            //                //    if (document.referrer != "" && document.referrer != undefined){
            //                //        window.location.href = document.referrer;
            //                //    }
            //                //    else {
            //                //        window.location.href = "/account/index";
            //                //    }
            //                //}, 6000);
            //            },
            //        });

            //        setInterval(function () {
            //            if (res == "Verified") {
            //                $(".close-verify").click();
            //            }
            //        }, 2000);

            //        return false;
            //    }
            //},
            resend: {
                text: 'Resend',
                btnClass: 'btn-resend',
                action: function () {
                    alert('Resend..');
                    $('.btn-resend').attr("disabled", "disabled");
                    $.ajax({
                        type: "POST",
                        url: "/Access/SendVerifyEmail",
                        //data: '{Compname: "' + Compname + '", Fullname: "' + Fullname + '", Email: "' + Email + '", Mobile:"' + Mobile + '", Password: "' + Passsword + '",Type:"' + mtype + '", Bname:"' + Bname + '", Bemail:"' + Bemail + '", Bmobile:"' + Bmobile + '", Bstreet:"' + Bstreet + '", Bpin:"' + Bpin + '", Bcity:"' + Bcity + '", Bstate:"' + Bstate + '", , Sname:"' + Sname + '", Semail:"' + Semail + '", Smobile:"' + Smobile + '", Sstreet:"' + Sstreet + '", Spin:"' + Spin + '", Scity:"' + Scity + '", Sstate:"' + Sstate + '", step:"' + step + '"  }',
                        //data: '{Id: "' + id + '"}',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                           //zayan sept23 -  $('.btn-resend').removeAttr("disabled");
                            //if (response != "Request Timeout") {
                            //    self.setContentAppend('<form action="" class="formName"><div class="form-group"><label>Enter OTP Code</label><input type="text" placeholder="OTP" class="otp form-control" required />resend (<span class="Counter"></span>)</div></form>');
                            //}
                            //else {
                           //zayan sept23 - $('.btn-resend').timedDisable(120);
                            //}
                        },
                    });
                    return false;
                }
            },
            //cancel: function () {
            //    //close
            //},
        },
        onContentReady: function () {
            // bind to events
            //var jc = this;
            //this.$content.find('form').on('submit', function (e) {
            //    // if the user submits the form by pressing enter in the field.
            //    e.preventDefault();
            //    jc.$$formSubmit.trigger('click'); // reference the button and click it
            //});
        }
    });
}



function checklog(mail, pass, me) {
    ;debugger
    var obj = {};
    obj.email = mail;
    obj.pass = pass;
    obj.me = me;
    $.ajax({
        type: "POST",
        url: "/Access/Checklogin",
        //data: '{Compname: "' + Compname + '", Fullname: "' + Fullname + '", Email: "' + Email + '", Mobile:"' + Mobile + '", Password: "' + Passsword + '",Type:"' + mtype + '", Bname:"' + Bname + '", Bemail:"' + Bemail + '", Bmobile:"' + Bmobile + '", Bstreet:"' + Bstreet + '", Bpin:"' + Bpin + '", Bcity:"' + Bcity + '", Bstate:"' + Bstate + '", , Sname:"' + Sname + '", Semail:"' + Semail + '", Smobile:"' + Smobile + '", Sstreet:"' + Sstreet + '", Spin:"' + Spin + '", Scity:"' + Scity + '", Sstate:"' + Sstate + '", step:"' + step + '"  }',
        data: JSON.stringify(obj),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {

            ;debugger


            if (response == false) {
                $.alert({ title: 'Caution!', theme: 'supervan', content: 'Invalid email id or password' });
                $("input[name=login]").val("Login").removeAttr("disabled", "disabled");
            }
            else if (response == "Block") {
                $.alert({ title: 'Caution!', theme: 'supervan', content: 'Your login is blocked, kindly contact admin at<br> +973 77990501' });
                $("input[name=login]").val("Login").removeAttr("disabled");

            }
            else if (response == "Inactive") {
                $.alert({ title: 'Caution!', theme: 'supervan', content: 'Your account is not active, Please contact customer care.' });
                $("input[name=login]").val("Login").removeAttr("disabled");
                
            }
            else if (response != false) {
                var accid = response[0]["Acc_Id"];
                var Redirect = response[0]["Redirect"];
                var Reg = response[0]["Reg"];
                var Verify = response[0]["Verify"];
                var EmailVerify = response[0]["UnverifiedEmail"];
                var Id = response[0]["Id"];
                if (accid != "" && accid != undefined) {
                    //alert(response[0]["Acc_Id"]);
                    if (Redirect != "" && Redirect != undefined) {
                        window.location.href = Redirect;
                    }
                    else {
                        window.location.href = "/Account/Index";
                    }
                    return true;
                }
                else if (Reg == "1" && Reg != undefined) {
                    window.location.href = Redirect;  
                    return true;
                }
                else if (Verify == "1" && Verify != undefined) {
                    VerifyEmailWithPopUp(Id); //VerifyEmail(Id); //  VerifyMob(Id);  //-- [faisal]this condition is added as OTP sms was not being sent when done remove VerifyEmail() and add VerifyMob()
                    return true;
                }
                else if (EmailVerify == "1" && EmailVerify != undefined) {
                    VerifyEmailWithPopUp(Id);
                    //VerifyEmail(Id);
                    return true;
                }
                else if (EmailVerify == "2" && EmailVerify != undefined) {   // email verify wih popup
                    VerifyEmailWithPopUp(Id);
                    return true;
                }
              
            }
            
        },
        failure: function (response) {
            return false;
        },
        error: function (response) {
            return false;
        }
    });
}


function subscribe(email) {
    $.confirm({
        title: 'SUBSCRIBE TO OUR NEWSLETTER',
        //theme: 'supervan',
        //content: function () {
        //    
        //    var self = this;
        //    self.setContentAppend('Checking callback flow');
        //    //
        //    //var ml = "";
        //    //if ($(".login input[name=mail]").val() != "") {
        //    //    ml = $(".login input[name=mail]").val();
        //    //}
        //    //self.setContentAppend('<form action="" class="form-sub mb-0"><div class="form-group"><input type="email" placeholder="Email Id" class="form-control" required /></div></form>');
        //},
        content: '<form action="" class="form-sub mb-0"><div class="form-group"><input type="text" name="fullname" placeholder="Full Name" class="sm-form-control" required /><input type="email" name="mail" placeholder="Email Id" value="' + email + '" class="sm-form-control mb-0" required /></div></form>',
        //content: function () {
        //    var self = this;
        //    return $.ajax({
        //        type: "POST",
        //        url: "/Access/SendOtp",
        //        //data: '{Compname: "' + Compname + '", Fullname: "' + Fullname + '", Email: "' + Email + '", Mobile:"' + Mobile + '", Password: "' + Passsword + '",Type:"' + mtype + '", Bname:"' + Bname + '", Bemail:"' + Bemail + '", Bmobile:"' + Bmobile + '", Bstreet:"' + Bstreet + '", Bpin:"' + Bpin + '", Bcity:"' + Bcity + '", Bstate:"' + Bstate + '", , Sname:"' + Sname + '", Semail:"' + Semail + '", Smobile:"' + Smobile + '", Sstreet:"' + Sstreet + '", Spin:"' + Spin + '", Scity:"' + Scity + '", Sstate:"' + Sstate + '", step:"' + step + '"  }',
        //        data: '{Id: "' + id + '"}',
        //        contentType: "application/json; charset=utf-8",
        //        dataType: "json",
        //        success: function (response) {
        //            if (response[0].Resp != "Request Timeout") {
        //                self.setContentAppend('<form action="" class="formName"><div class="form-group"><label>Enter OTP Code</label><input type="text" placeholder="OTP" class="otp form-control" required /></div></form>');
        //                self.setContentAppend('<small class="text-danger">OTP has sent on ' + response[0].Number + '</small>');
        //            }
        //            else {

        //            }
        //        },
        //    }).done(function (response) {
        //        true;
        //    }).fail(function () {
        //        false;
        //    });
        //},
        contentLoaded: function (data, status, xhr) {
            //if (data != "Request Timeout") {
            //    $(function () {
            //        //var res = jQuery("#" + '<%= hfresend.ClientID%>').val();
            //        //if (res == 1) {

            //        //}
            //        $('.btn-resend').timedDisable(120);
            //    });

            //}
        },
        closeIcon: true,
        //closeIconClass: 'dialog',
        buttons: {
            //cancel: function () {
            //    //close
            //},
            formSubmit: {
                text: 'Subscribe',
                btnClass: 'subsc pl-4',
                action: function () {
                    
                    var VAL = this.$content.find('input[name=mail]').val();
                    var Name = this.$content.find('input[name=fullname]').val();
                    $('.jconfirm-content label.text-danger').remove();
                    //alert(VAL);
                    var email = /^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$/i;
                    //alert(email);
                    if (VAL == "" || Name == "") {
                        this.setContentAppend('<label class="text-danger" style="font-size:11px">All fields required</label>');
                    }
                    else if (VAL != "" && email.test(VAL) && Name != "") {
                        $.ajax({
                            type: "POST",
                            url: "/Access/Subscribe",
                            //data: '{Compname: "' + Compname + '", Fullname: "' + Fullname + '", Email: "' + Email + '", Mobile:"' + Mobile + '", Password: "' + Passsword + '",Type:"' + mtype + '", Bname:"' + Bname + '", Bemail:"' + Bemail + '", Bmobile:"' + Bmobile + '", Bstreet:"' + Bstreet + '", Bpin:"' + Bpin + '", Bcity:"' + Bcity + '", Bstate:"' + Bstate + '", , Sname:"' + Sname + '", Semail:"' + Semail + '", Smobile:"' + Smobile + '", Sstreet:"' + Sstreet + '", Spin:"' + Spin + '", Scity:"' + Scity + '", Sstate:"' + Sstate + '", step:"' + step + '"  }',
                            data: '{mail: "' + VAL + '",name:"' + Name +'"}',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            beforeSend: function () {

                                $(".jconfirm-closeIcon").click();
                                $.dialog({
                                    title: false,
                                    content: '<div class="checkbox" style="text-align:center;"><label style="font-size: 15px;margin: 0;padding: 0;margin-top: 5px;color: #848484;">Subscribing...<i class="fas fa-circle-notch fa-spin"></i></label></div>',
                                    closeIconClass: 'close-dialog',
                                });
                            },
                            success: function (response) {
                                res = response;
                                if (response == "true") {
                                    ms = '<label style="font- size: 12px;margin: 0;padding: 0;margin-top: 5px;color: #848484; ">Thank You. We have received your details.</label>';
                                }
                                else {
                                    ms = '<label style="font- size: 12px;margin: 0;padding: 0;margin-top: 5px;color: #848484; ">Something went wrong. Please contact our support team.</label>';
                                }

                                $.dialog({
                                    title: false,
                                    content: ms,
                                    closeIconClass: 'close-dialog',
                                });
                                setTimeout(function () {
                                    $(".close-dialog, .jconfirm-closeIcon").click();
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
                        //$(".close-dialog, .jconfirm-closeIcon").click();
                    }
                    else if (VAL != "") {
                        this.setContentAppend('<label class="text-danger" style="font-size:11px">Email is Invalid</label>');
                    }
                    //this.setContentAppend("required");
                    //var otp = this.$content.find('.otp').val();
                    //var ms = "";
                    //var res = "";
                    //$.ajax({
                    //    type: "POST",
                    //    url: "/Access/VerifyMob",
                    //    //data: '{Compname: "' + Compname + '", Fullname: "' + Fullname + '", Email: "' + Email + '", Mobile:"' + Mobile + '", Password: "' + Passsword + '",Type:"' + mtype + '", Bname:"' + Bname + '", Bemail:"' + Bemail + '", Bmobile:"' + Bmobile + '", Bstreet:"' + Bstreet + '", Bpin:"' + Bpin + '", Bcity:"' + Bcity + '", Bstate:"' + Bstate + '", , Sname:"' + Sname + '", Semail:"' + Semail + '", Smobile:"' + Smobile + '", Sstreet:"' + Sstreet + '", Spin:"' + Spin + '", Scity:"' + Scity + '", Sstate:"' + Sstate + '", step:"' + step + '"  }',
                    //    data: '{Otp: "' + otp + '"}',
                    //    contentType: "application/json; charset=utf-8",
                    //    dataType: "json",
                    //    success: function (response) {
                    //        res = response;
                    //        if (response == "Verified") {
                    //            ms = "Thankyou for verifying your mobile number."
                    //        }
                    //        else if (response == "Unverified") {
                    //            ms = "Entered OTP is expired or incorrect."
                    //        }
                    //        else if (response == "Timeout") {
                    //            ms = "The entered otp is expired. we have sent you a new OTP."
                    //        }
                    //        else if (response == "Something went wrong") {
                    //            ms = "Please Login and try again verifing your number."
                    //        }
                    //        else {
                    //            ms = "Please Login and try again verifing your number."
                    //        }

                    //        $.dialog({
                    //            title: false,
                    //            content: ms,
                    //            closeIconClass: 'close-dialog',
                    //        });
                    //        setTimeout(function () {
                    //            $(".close-dialog").click();
                    //        }, 5000);
                    //        setTimeout(function () {
                    //            if (document.referrer != "" && document.referrer != undefined) {
                    //                window.location.href = document.referrer;
                    //            }
                    //            else {
                    //                window.location.href = "/";
                    //            }
                    //        }, 6000);
                    //    },
                    //});

                    //setInterval(function () {
                    //    if (res == "Verified") {
                    //        $(".close-verify").click();
                    //    }
                    //}, 2000);
                    //alert();
                    return false;
                }
            },
            //resend: {
            //    text: 'Cancel',
            //    btnClass: 'btn-resend',
            //    action: function () {
            //        //$.ajax({
            //        //    type: "POST",
            //        //    url: "/Access/SendOtp",
            //        //    //data: '{Compname: "' + Compname + '", Fullname: "' + Fullname + '", Email: "' + Email + '", Mobile:"' + Mobile + '", Password: "' + Passsword + '",Type:"' + mtype + '", Bname:"' + Bname + '", Bemail:"' + Bemail + '", Bmobile:"' + Bmobile + '", Bstreet:"' + Bstreet + '", Bpin:"' + Bpin + '", Bcity:"' + Bcity + '", Bstate:"' + Bstate + '", , Sname:"' + Sname + '", Semail:"' + Semail + '", Smobile:"' + Smobile + '", Sstreet:"' + Sstreet + '", Spin:"' + Spin + '", Scity:"' + Scity + '", Sstate:"' + Sstate + '", step:"' + step + '"  }',
            //        //    data: '{Id: "' + id + '"}',
            //        //    contentType: "application/json; charset=utf-8",
            //        //    dataType: "json",
            //        //    success: function (response) {
            //        //        //if (response != "Request Timeout") {
            //        //        //    self.setContentAppend('<form action="" class="formName"><div class="form-group"><label>Enter OTP Code</label><input type="text" placeholder="OTP" class="otp form-control" required />resend (<span class="Counter"></span>)</div></form>');
            //        //        //}
            //        //        //else {

            //        //        //}
            //        //    },
            //        //});
            //    }
            //},
            
        },
        onContentReady: function () {
            // bind to events
            //var jc = this;
            //this.$content.find('form').on('submit', function (e) {
            //    // if the user submits the form by pressing enter in the field.
            //    e.preventDefault();
            //    jc.$$formSubmit.trigger('click'); // reference the button and click it
            //});
        }
    });
    //$.confirm({
    //    title: 'Subscribe our Newsletter',
    //    //theme: 'supervan',
    //    content: '' +
    //    '<form action="" class="form-sub">' +
    //    '<div class="form-group">' +
    //    //'<label>Enter OTP Code</label>' +
    //    '<input type="text" placeholder="Full Name" class="form-control" required />' +
    //    '<input type="email" placeholder="Email Id" class="form-control" required />'+
    //    '</div>' +
    //    '</form>',
    //    //content: function () {
    //    //    var self = this;
    //    //    return $.ajax({
    //    //        type: "POST",
    //    //        url: "/Access/SendOtp",
    //    //        //data: '{Compname: "' + Compname + '", Fullname: "' + Fullname + '", Email: "' + Email + '", Mobile:"' + Mobile + '", Password: "' + Passsword + '",Type:"' + mtype + '", Bname:"' + Bname + '", Bemail:"' + Bemail + '", Bmobile:"' + Bmobile + '", Bstreet:"' + Bstreet + '", Bpin:"' + Bpin + '", Bcity:"' + Bcity + '", Bstate:"' + Bstate + '", , Sname:"' + Sname + '", Semail:"' + Semail + '", Smobile:"' + Smobile + '", Sstreet:"' + Sstreet + '", Spin:"' + Spin + '", Scity:"' + Scity + '", Sstate:"' + Sstate + '", step:"' + step + '"  }',
    //    //        data: '{Id: "' + id + '"}',
    //    //        contentType: "application/json; charset=utf-8",
    //    //        dataType: "json",
    //    //        success: function (response) {
    //    //            if (response[0].Resp != "Request Timeout") {
    //    //                self.setContentAppend('<form action="" class="formName"><div class="form-group"><label>Enter OTP Code</label><input type="text" placeholder="OTP" class="otp form-control" required /></div></form>');
    //    //                self.setContentAppend('<small class="text-danger">OTP has sent on ' + response[0].Number + '</small>');
    //    //            }
    //    //            else {

    //    //            }
    //    //        },
    //    //    }).done(function (response) {
    //    //        true;
    //    //    }).fail(function () {
    //    //        false;
    //    //    });
    //    //},
    //    contentLoaded: function (data, status, xhr) {
    //        //if (data != "Request Timeout") {
    //        //    $(function () {
    //        //        //var res = jQuery("#" + '<%= hfresend.ClientID%>').val();
    //        //        //if (res == 1) {

    //        //        //}
    //        //        $('.btn-resend').timedDisable(120);
    //        //    });

    //        //}
    //    },
    //    closeIcon: true,
    //    //closeIconClass: 'close-verify',
    //    buttons: {
    //        formSubmit: {
    //            text: 'Subcribe',
    //            btnClass: 'subsc',
    //            action: function () {
    //                var Name = this.$content.find('input:nth-child(1)').val();
    //                var VAL = this.$content.find('input:nth-child(2)').val();

    //                $('.jconfirm-content label.text-danger').remove();
    //                //alert(VAL);
    //                var email = /^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$/i;
    //                if (VAL != "" && Name != "") {
    //                    this.setContentAppend('<label class="text-danger" style="font-size:11px">All Fields Required</label>');
    //                }
    //                if (VAL != "" && email.test(VAL)){
    //                    this.setContentAppend('<label class="text-danger" style="font-size:11px">Email is Invalid</label>');
    //                }
                    
    //                //var otp = this.$content.find('.otp').val();
    //                //var ms = "";
    //                //var res = "";
    //                //$.ajax({
    //                //    type: "POST",
    //                //    url: "/Access/VerifyMob",
    //                //    //data: '{Compname: "' + Compname + '", Fullname: "' + Fullname + '", Email: "' + Email + '", Mobile:"' + Mobile + '", Password: "' + Passsword + '",Type:"' + mtype + '", Bname:"' + Bname + '", Bemail:"' + Bemail + '", Bmobile:"' + Bmobile + '", Bstreet:"' + Bstreet + '", Bpin:"' + Bpin + '", Bcity:"' + Bcity + '", Bstate:"' + Bstate + '", , Sname:"' + Sname + '", Semail:"' + Semail + '", Smobile:"' + Smobile + '", Sstreet:"' + Sstreet + '", Spin:"' + Spin + '", Scity:"' + Scity + '", Sstate:"' + Sstate + '", step:"' + step + '"  }',
    //                //    data: '{Otp: "' + otp + '"}',
    //                //    contentType: "application/json; charset=utf-8",
    //                //    dataType: "json",
    //                //    success: function (response) {
    //                //        res = response;
    //                //        if (response == "Verified") {
    //                //            ms = "Thankyou for verifying your mobile number."
    //                //        }
    //                //        else if (response == "Unverified") {
    //                //            ms = "Entered OTP is expired or incorrect."
    //                //        }
    //                //        else if (response == "Timeout") {
    //                //            ms = "The entered otp is expired. we have sent you a new OTP."
    //                //        }
    //                //        else if (response == "Something went wrong") {
    //                //            ms = "Please Login and try again verifing your number."
    //                //        }
    //                //        else {
    //                //            ms = "Please Login and try again verifing your number."
    //                //        }

    //                //        $.dialog({
    //                //            title: false,
    //                //            content: ms,
    //                //            closeIconClass: 'close-dialog',
    //                //        });
    //                //        setTimeout(function () {
    //                //            $(".close-dialog").click();
    //                //        }, 5000);
    //                //        setTimeout(function () {
    //                //            if (document.referrer != "" && document.referrer != undefined) {
    //                //                window.location.href = document.referrer;
    //                //            }
    //                //            else {
    //                //                window.location.href = "/";
    //                //            }
    //                //        }, 6000);
    //                //    },
    //                //});

    //                //setInterval(function () {
    //                //    if (res == "Verified") {
    //                //        $(".close-verify").click();
    //                //    }
    //                //}, 2000);
    //                //alert();
    //                return false;
    //            }
    //        },
    //        //resend: {
    //        //    text: 'Cancel',
    //        //    btnClass: 'btn-resend',
    //        //    action: function () {
    //        //        //$.ajax({
    //        //        //    type: "POST",
    //        //        //    url: "/Access/SendOtp",
    //        //        //    //data: '{Compname: "' + Compname + '", Fullname: "' + Fullname + '", Email: "' + Email + '", Mobile:"' + Mobile + '", Password: "' + Passsword + '",Type:"' + mtype + '", Bname:"' + Bname + '", Bemail:"' + Bemail + '", Bmobile:"' + Bmobile + '", Bstreet:"' + Bstreet + '", Bpin:"' + Bpin + '", Bcity:"' + Bcity + '", Bstate:"' + Bstate + '", , Sname:"' + Sname + '", Semail:"' + Semail + '", Smobile:"' + Smobile + '", Sstreet:"' + Sstreet + '", Spin:"' + Spin + '", Scity:"' + Scity + '", Sstate:"' + Sstate + '", step:"' + step + '"  }',
    //        //        //    data: '{Id: "' + id + '"}',
    //        //        //    contentType: "application/json; charset=utf-8",
    //        //        //    dataType: "json",
    //        //        //    success: function (response) {
    //        //        //        //if (response != "Request Timeout") {
    //        //        //        //    self.setContentAppend('<form action="" class="formName"><div class="form-group"><label>Enter OTP Code</label><input type="text" placeholder="OTP" class="otp form-control" required />resend (<span class="Counter"></span>)</div></form>');
    //        //        //        //}
    //        //        //        //else {

    //        //        //        //}
    //        //        //    },
    //        //        //});
    //        //    }
    //        //},
    //        //cancel: function () {
    //        //    //close
    //        //},
    //    },
    //    onContentReady: function () {
    //        // bind to events
    //        //var jc = this;
    //        //this.$content.find('form').on('submit', function (e) {
    //        //    // if the user submits the form by pressing enter in the field.
    //        //    e.preventDefault();
    //        //    jc.$$formSubmit.trigger('click'); // reference the button and click it
    //        //});
    //    }
    //});
}


function Whatsapp() {
    
    
    $.confirm({
        title: 'Order on Whatsapp',
        //theme: 'supervan',
        
        //content: '<form action="" class="form-sub mb-0"><div class="form-group"><input type="text" name="fullname" placeholder="Full Name" class="sm-form-control" required /><input type="email" name="mail" placeholder="Email Id" value="" class="sm-form-control mb-0" required /></div></form>',
        content: function () {
            var self = this;
            return $.ajax({
                url: '/access/GetUserDetById',
                dataType: 'json',
                method: 'get'
            }).done(function (response) {
                self.setContent('<form action="" class="form-sub mb-0"><div class="form-group"><label class="mb-0">Name</label><input type="text" name="fullname" placeholder="Full Name" value="' + response[0]["Contact_ContactPerson"] + '" class="sm-form-control mb-1 mt-0" required /><label class="mb-0">Email</label><input type="email" name="mail" placeholder="Email Id" value="' + response[0]["Contact_Email"] + '" class="sm-form-control mb-1 mt-0" required /><label class="mb-0">Mobile (+973)</label><input type="text"  name="mobile" placeholder="Mobile No." value="' + response[0]["Contact_Mobile"] + '" class="sm-form-control mb-1 mt-0 nocpc" maxlength="10" onkeypress="return numkey(this);" required /></div></form>')


                //self.setContent('Description: ' + response.description);
                //self.setContentAppend('<br>Version: ' + response.version);
                //self.setTitle(response.name);
            });
        },
        contentLoaded: function (data, status, xhr) {

            //if (data != "Request Timeout") {
            //    $(function () {
            //        //var res = jQuery("#" + '<%= hfresend.ClientID%>').val();
            //        //if (res == 1) {

            //        //}
            //        $('.btn-resend').timedDisable(120);
            //    });

            //}
        },
        closeIcon: true,
        backgroundDismiss:true,
        closeIconClass: 'd-block',
        buttons: {
            formSubmit: {
                text: 'Place Order',
                btnClass: 'btn btn-success bg-success text-white float-right text-center req-btn',
                action: function () {
                    
                    var VAL = this.$content.find('input[name=mail]').val();
                    var mobile = this.$content.find('input[name=mobile]').val();
                    var Name = this.$content.find('input[name=fullname]').val();
                    $('.jconfirm-content label.text-danger').remove();
                    //alert(VAL);
                    var email = /^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$/i;
                    var mob = /^[0-9]{10,10}$/i;
                    //alert(email);
                    //alert(mob.test(mobile) + " " + email.test(VAL));
                    var ur = window.location.pathname.replace("/", "");
                    var ty = "Whatsapp";
                    var pname = $('#protitle').text();
                    var prc = $('#pdsingle .pricing-info .pack-qty').val();
                    if (pname == "") {
                        console.log(pname);
                        if (ur == "cart") {
                            pname = "Cart";
                            console.log(ur);
                        }
                        else {
                            pname = "";
                        }
                        
                    }
                    
                    var Pid = $('.skurange.active').attr('id');
                    var Psku = $('.skurange.active span').text();
                    if (Pid == undefined) {
                        Pid = $('.sampleskurange').attr('id');
                        var Psku = $('.pricing-main .prc-qty small').text();
                    }
                    if (Pid == undefined) {
                        Pid = 0;
                    }

                    if (prc != "" && prc != undefined){
                        prc = " | " + Psku + " Bag | No. of Packs: " + prc ;
                    }
                    else {
                        prc = "";
                    }

                    if (VAL == "" || Name == "" || mobile == "") {
                        this.setContentAppend('<label class="text-danger" style="font-size:11px">All fields required</label>');
                    }
                    else if (VAL != "" && email.test(VAL) && Name != "" && mobile != "" && mob.test(mobile)) {
                        $.ajax({
                            url: '/Access/RequestB',
                            type: "Post",
                            data: { t: ty, n: Name, m: mobile, e: VAL, o: "", a: "", c: "", p: "", cmm: "", rd: "", pi: Pid, co: "", pn: pname + prc },
                            
                            beforeSend: function () {
                                $(".req-btn").attr("disabled", "disabled");
                                $(".jconfirm-closeIcon").click();
                                $.dialog({
                                    title: false,
                                    content: '<div class="checkbox" style="text-align:center;"><label style="font-size: 15px;margin: 0;padding: 0;margin-top: 5px;color: #848484;">Sending Request...<i class="fas fa-circle-notch fa-spin"></i></label></div>',
                                    closeIconClass: 'close-dialog',
                                });
                            },
                            success: function (response) {
                                res = response;
                                if (response.includes("I am")) {
                                    setTimeout(function () {
                                        $(".close-dialog, .jconfirm-closeIcon").click();
                                    }, 5000);
                                    //window.open('https://api.whatsapp.com/send?phone=97333550511&text=' + response, '_blank');
                                    window.location.href = "https://api.whatsapp.com/send?phone=97377990501&text=" + response;
                                }
                                else if (response == "Redirect") {
                                    window.location.href = "/login";
                                }
                                else if (response == "true") {
                                    //ms = '<label style="font- size: 12px;margin: 0;padding: 0;margin-top: 5px;color: #848484; ">Thank You. We have received your request.</label>';
                                    location.reload(true);
                                }
                                else {
                                    ms = '<label style="font- size: 12px;margin: 0;padding: 0;margin-top: 5px;color: #848484; ">Something went wrong. Please contact our support team.</label>';
                                }

                                $.dialog({
                                    title: false,
                                    content: ms,
                                    closeIconClass: 'close-dialog',
                                });
                                setTimeout(function () {
                                    $(".close-dialog, .jconfirm-closeIcon").click();
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
                    else if (email.test(VAL) == false) {
                        this.setContentAppend('<label class="text-danger" style="font-size:11px">Email is Invalid</label>');
                    }
                    else if (mob.test(mobile) == false) {
                        this.setContentAppend('<label class="text-danger" style="font-size:11px">Mobile No. is Invalid</label>');
                    }
                    
                    return false;
                }
            },
            //resend: {
            //    text: 'Cancel',
            //    btnClass: 'btn-resend',
            //    action: function () {
            //        //$.ajax({
            //        //    type: "POST",
            //        //    url: "/Access/SendOtp",
            //        //    //data: '{Compname: "' + Compname + '", Fullname: "' + Fullname + '", Email: "' + Email + '", Mobile:"' + Mobile + '", Password: "' + Passsword + '",Type:"' + mtype + '", Bname:"' + Bname + '", Bemail:"' + Bemail + '", Bmobile:"' + Bmobile + '", Bstreet:"' + Bstreet + '", Bpin:"' + Bpin + '", Bcity:"' + Bcity + '", Bstate:"' + Bstate + '", , Sname:"' + Sname + '", Semail:"' + Semail + '", Smobile:"' + Smobile + '", Sstreet:"' + Sstreet + '", Spin:"' + Spin + '", Scity:"' + Scity + '", Sstate:"' + Sstate + '", step:"' + step + '"  }',
            //        //    data: '{Id: "' + id + '"}',
            //        //    contentType: "application/json; charset=utf-8",
            //        //    dataType: "json",
            //        //    success: function (response) {
            //        //        //if (response != "Request Timeout") {
            //        //        //    self.setContentAppend('<form action="" class="formName"><div class="form-group"><label>Enter OTP Code</label><input type="text" placeholder="OTP" class="otp form-control" required />resend (<span class="Counter"></span>)</div></form>');
            //        //        //}
            //        //        //else {

            //        //        //}
            //        //    },
            //        //});
            //    }
            //},
            cancel: function () {
                //close
            },
        },
        onContentReady: function () {
            // bind to events
            //var jc = this;
            //this.$content.find('form').on('submit', function (e) {
            //    // if the user submits the form by pressing enter in the field.
            //    e.preventDefault();
            //    jc.$$formSubmit.trigger('click'); // reference the button and click it
            //});
        }
    });
}






// zayan 02oct23
$.fn.timedDisable_stop = function (time) {
    
    if (time == null) {
        time = 5;
    }
    var seconds = Math.ceil(time); // Calculate the number of seconds
    return $(this).each(function () {
        $(this).attr('disabled', 'disabled');
        var disabledElem = jQuery(this);
        //var originalText = this.html; // Remember the original text content
        var minutes = Math.floor(time / 60);
        var seconds = time - minutes * 60;
        function str_pad_left(string, pad, length) {
            return (new Array(length + 1).join(pad) + string).slice(-length);
        }

        var finalTime = str_pad_left(minutes, '0', 2) + ':' + str_pad_left(seconds, '0', 2);
        // append the number of seconds to the text
        disabledElem.html('Resend (' + finalTime + ')');

        // do a set interval, using an interval of 1000 milliseconds
        //     and clear it after the number of seconds counts down to 0
        var interval = setInterval(function () {
            time = time - 1;
            var minutes = Math.floor(time / 60);
            var seconds = time - minutes * 60;
            function str_pad_left(string, pad, length) {
                return (new Array(length + 1).join(pad) + string).slice(-length);
            }

            var finalTime = str_pad_left(minutes, '0', 2) + ':' + str_pad_left(seconds, '0', 2);
            // decrement the seconds and update the text
            disabledElem.html('Resend (' + finalTime + ')');
            if (time === 0) { // once seconds is 0...
                disabledElem.removeAttr('disabled')
                    .html('Resend'); //reset to original text
                clearInterval(interval); // clear interval
            }
        }, 1000);
    });
};



//var app = angular.module('MyApp', ['rzModule']);
//app.controller('single', function ($scope, $rootScope) {
//    $scope.slider = {
//        value: 1,
//        options: {
//            floor: 1,
//            ceil: 1000,
//            step: 1,
//            minRange: 1,
//            maxRange: 1000,
//            showSelectionBar: true,
//            getSelectionBarColor: function (value) {
//                if (value <= 3)
//                    return 'red';
//                if (value <= 6)
//                    return 'orange';
//                if (value <= 9)
//                    return 'yellow';
//                return '#2AE02A';
//            },
            
//            translate: function (value, sliderId, label) {
//                switch (label) {
//                    default:
//                        return 'Quantity ' + value
//                }
//            },
//            hidePointerLabels: true,
//            onChange: function (id, value) {
//                $scope.pack = value;
//                if (value <= 300){
//                    $scope.price = 250.75;
//                    $scope.discount = 250.75 * 10 / 100
//                }
//                else if (value <= 650){
//                    $scope.price = 230;
//                    $scope.discount = 230.00 * 15 / 100
//                }
//                else if (value <= 999) {
//                    $scope.price = 180;
//                    $scope.discount = 180.00 * 18 / 100
//                }
//            },
//        }
//    };
//    $scope.pack = 1;

//    $scope.maketotal = function () {
//        if ($scope.pack <= 1000 || $scope.pack >= 0){
//                $scope.slider.value = $scope.pack;
//            if ($scope.slider.value <= 300) {
//                $scope.price = 250.75;
//                $scope.discount = 250.75 * 10 / 100
//            }
//            else if ($scope.slider.value <= 650) {
//                $scope.price = 230;
//                $scope.discount = 230.00 * 15 / 100
//            }
//            else if ($scope.slider.value <= 999) {
//                $scope.price = 180;
//                $scope.discount = 180.00 * 18 / 100
//                }
//            if ($scope.pack > 1000) {
//                $scope.slider.value = 1000;
//                $scope.pack = 1000;
//            }
//        }
//        else if ($scope.pack > 1000){
//            $scope.slider.value = 1000;
//            $scope.pack = 1000;
//        }
//        if($scope.pack == 0) {
//            $scope.slider.value = 1;
//            $scope.pack = 1;
//        }
//    }

//});


//uncomment to disable right click and all view source nad inspect element.

//$(document).keydown(function (event) {
//    if (event.keyCode == 123) {
//        return false;
//    }
//    else if (event.ctrlKey && event.shiftKey && event.keyCode == 73) {
//        return false;
//    }
//});

//shortcut = {
//    all_shortcuts: {},
//    add: function (e, t, n) {
//        var r = {
//            type: "keydown",
//            propagate: !1,
//            disable_in_input: !1,
//            target: document,
//            keycode: !1
//        };
//        if (n) for (var i in r) "undefined" == typeof n[i] && (n[i] = r[i]);
//        else n = r;
//        r = n.target, "string" == typeof n.target && (r = document.getElementById(n.target)), e = e.toLowerCase(), i = function (r) {
//            r = r || window.event;
//            if (n.disable_in_input) {
//                var i;
//                r.target ? i = r.target : r.srcElement && (i = r.srcElement), 3 == i.nodeType && (i = i.parentNode);
//                if ("INPUT" == i.tagName || "TEXTAREA" == i.tagName) return
//            }
//            r.keyCode ? code = r.keyCode : r.which && (code = r.which), i = String.fromCharCode(code).toLowerCase(), 188 == code && (i = ","), 190 == code && (i = ".");
//            var s = e.split("+"),
//                o = 0,
//                u = {
//                    "`": "~",
//                    1: "!",
//                    2: "@",
//                    3: "#",
//                    4: "$",
//                    5: "%",
//                    6: "^",
//                    7: "&",
//                    8: "*",
//                    9: "(",
//                    0: ")",
//                    "-": "_",
//                    "=": "+",
//                    ";": ":",
//                    "'": '"',
//                    ",": "<",
//                    ".": ">",
//                    "/": "?",
//                    "\\": "|"
//                }, f = {
//                    esc: 27,
//                    escape: 27,
//                    tab: 9,
//                    space: 32,
//                    "return": 13,
//                    enter: 13,
//                    backspace: 8,
//                    scrolllock: 145,
//                    scroll_lock: 145,
//                    scroll: 145,
//                    capslock: 20,
//                    caps_lock: 20,
//                    caps: 20,
//                    numlock: 144,
//                    num_lock: 144,
//                    num: 144,
//                    pause: 19,
//                    "break": 19,
//                    insert: 45,
//                    home: 36,
//                    "delete": 46,
//                    end: 35,
//                    pageup: 33,
//                    page_up: 33,
//                    pu: 33,
//                    pagedown: 34,
//                    page_down: 34,
//                    pd: 34,
//                    left: 37,
//                    up: 38,
//                    right: 39,
//                    down: 40,
//                    f1: 112,
//                    f2: 113,
//                    f3: 114,
//                    f4: 115,
//                    f5: 116,
//                    f6: 117,
//                    f7: 118,
//                    f8: 119,
//                    f9: 120,
//                    f10: 121,
//                    f11: 122,
//                    f12: 123
//                }, l = !1,
//                c = !1,
//                h = !1,
//                p = !1,
//                d = !1,
//                v = !1,
//                m = !1,
//                y = !1;
//            r.ctrlKey && (p = !0), r.shiftKey && (c = !0), r.altKey && (v = !0), r.metaKey && (y = !0);
//            for (var b = 0; k = s[b], b < s.length; b++) "ctrl" == k || "control" == k ? (o++ , h = !0) : "shift" == k ? (o++ , l = !0) : "alt" == k ? (o++ , d = !0) : "meta" == k ? (o++ , m = !0) : 1 < k.length ? f[k] == code && o++ : n.keycode ? n.keycode == code && o++ : i == k ? o++ : u[i] && r.shiftKey && (i = u[i], i == k && o++);
//            if (o == s.length && p == h && c == l && v == d && y == m && (t(r), !n.propagate)) return r.cancelBubble = !0, r.returnValue = !1, r.stopPropagation && (r.stopPropagation(), r.preventDefault()), !1
//        }, this.all_shortcuts[e] = {
//            callback: i,
//            target: r,
//            event: n.type
//        }, r.addEventListener ? r.addEventListener(n.type, i, !1) : r.attachEvent ? r.attachEvent("on" + n.type, i) : r["on" + n.type] = i
//    },
//    remove: function (e) {
//        var e = e.toLowerCase(),
//            t = this.all_shortcuts[e];
//        delete this.all_shortcuts[e];
//        if (t) {
//            var e = t.event,
//                n = t.target,
//                t = t.callback;
//            n.detachEvent ? n.detachEvent("on" + e, t) : n.removeEventListener ? n.removeEventListener(e, t, !1) : n["on" + e] = !1
//        }
//    }
//},
//    shortcut.add("Ctrl+U", function () {
//        //alert('Sorry\nNo CTRL+U is allowed. Be creative!')
//    }),
//    shortcut.add("Meta+Alt+U", function () {
//        //alert('Sorry\nNo Command+Option+U is allowed. Be creative!')
//    }),
//    shortcut.add("Ctrl+C", function () {
//        //alert('Sorry\nNever duplicate this article...')
//    }),
//    shortcut.add("Meta+C", function () {
//        //alert('Sorry\nNever duplicate this article...')
//    });



$('.relate-prod').owlCarousel({
    loop: false,
    autoplay: true,
    autoplayTimeout: 3000,
    margin: 20,
    nav: true,
    dots: false,
    responsive: {
        320: {
            items: 2
        },

        768: {
            items: 2
        },

        1024: {
            items: 3
        },

        1200: {
            items: 4
        },
    },
    
})