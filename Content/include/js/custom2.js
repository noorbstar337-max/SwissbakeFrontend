$(".cat-button").hover(function () {
    $(".cat-menu").toggleClass("transition-off");
});
$(window).resize(function () {
    // This will execute whenever the window is resized
    $('#header #header-wrap .container-fluid .searchbar').css("top", $(window).height() - 60);
});
$(document).ready(function () {
    
    $('#header #header-wrap .container-fluid .searchbar').css("top", $(window).height() - 60)
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
    $('.body-overlay,#side-panel-trigger-close').click(function () {

        if ($('.side-panel-wrap .side-log').hasClass("d-none")) {

        }
        else {
            $('.side-panel-wrap .side-log').toggleClass("d-none");
            //$('body').toggleClass("side-panel-left");
        }
        if ($('.side-panel-wrap .side-cart').hasClass("d-none")) {

        }
        else {
            $('.side-panel-wrap .side-cart').toggleClass("d-none");
            $('.side-panel-wrap .side-cart .mast-cart-ttl').toggleClass("open");
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

    setTimeout(function () {
        var i = 0;
        var oc = $("#top-bar .top-menu a").length;
        $("#top-bar .row:nth-child(2).animated").addClass("fadeIn").removeClass("op-0");
        $("#top-bar.animated").addClass("slideInUp").removeClass("op-0");
        //$("#primary-menu.animated").addClass("slideInLeft");
        $("#header #header-wrap .container-fluid.animated").addClass("slideInDown").removeClass("op-0");
        $("#slider.animated").addClass("fadeIn");
        if (screen.width <= 479) {
            $("#logo.animated").addClass("fadeInDown");
            $("#primary-menu-trigger").addClass("bubble").removeClass("op-0");
        }
        else {
            $("#logo.animated").addClass("fadeInLeft");
        }

        setInterval(function () {
            i++;
            $("#top-bar .top-menu a:nth-child(" + i + ").animated").addClass("fadeIn").removeClass("op-0");

            if (i >= oc) {
                clearInterval();
                $("#top-bar .country-blk.animated").addClass("fadeIn").removeClass("op-0");
                $("#top-bar .social-set a:nth-child(1).animated").addClass("fadeIn").removeClass("op-0");
                $("#top-bar .social-set a:nth-child(2).animated").addClass("fadeIn").removeClass("op-0");
                $("#top-bar .social-set a:nth-child(3).animated").addClass("fadeIn").removeClass("op-0");
                //var os = $("#top-bar .social-set a").length;
                //for (var j = 1; j >= os; j++) {
                //    $("#top-bar .social-set a:nth-child(" + j + ").animated").addClass("fadeIn").removeClass("op-0");
                //}
                //setInterval(function () {
                //    i++;
                //    $("#top-bar .social-set a:nth-child(" + i + ").animated").addClass("fadeIn").removeClass("op-0");
                //    if (i >= os){
                //        clearInterval();
                //    }
                //},500);
            }
        }, 500);
        $(".right-menu .animated,#top-bar .row:nth-child(1).animated,#top-bar .row:nth-child(2).animated,#logo.animated,#slider.animated,#primary-menu.animated").removeClass("op-0");

    }, 2000);

    $(window).scroll(function () {

        if ($('.cat-grid').visible(true)){
            var cg = $('.cat-grid .product-grid').length;
            setTimeout(function () {
                for (var a = 1; a <= cg + 1; a++) {

                    $('.cat-grid .pr-grd .col-md-3.animated:nth-child(' + a + ')').addClass("zoomIn").removeClass("op-0");
                }
            },700);
            
            var cgl = $('.cat-grid .side-tab ul li').length;
            var b = 0;
            setInterval(function () {

                b++;
                $('.cat-grid .side-tab ul li:nth-child(' + b + ')').addClass("slideInRight").removeClass("op-0");
                if (b >= cgl) {
                    clearInterval();
                    $('.cat-grid .side-tab h5').addClass("fadeIn").removeClass("op-0");
                    $('.cat-grid .pr-grd .col-md-12 h5, .cat-grid .pr-grd .col-md-12 a').addClass("fadeInDown").removeClass("op-0");
                }
            }, 500);
        }
        if ($('.about').visible(true)) {
            $('.about .col-md-6:nth-child(1)').addClass("fadeInLeft").removeClass("op-0");
            $('.about .col-md-6:nth-child(2)').addClass("fadeInRight").removeClass("op-0");
        }

        if ($('.tabs').visible(true)) {

            var c = 0;
            setInterval(function () {
                c++;
                $('.tabs ul li:nth-child(' + c + ')').addClass("fadeInDown").removeClass("op-0");
                if (c >= 3) {
                    clearInterval();
                    $('.tabs .slider').addClass("fadeInUp").removeClass("op-0");
                }
                $('.tabs .content .animated').addClass("zoomIn").removeClass("op-0");

            })
            
            
        }
        if ($('#footer').visible(true)) {
            $('#footer .subscribe-widget').addClass("fadeInDown").removeClass("op-0");
            $('#footer .footer-about').addClass("fadeIn").removeClass("op-0");
            $('#footer .policy h5,#footer .quick-links h5,#footer .misc h5,#footer .home-bakr img').addClass("fadeIn").removeClass("op-0");

            var fl = 0;

            setInterval(function () {
                fl++;
                $('#footer .policy ul li:nth-child(' + fl + '),#footer .quick-links ul li:nth-child(' + fl + '),#footer .misc ul li:nth-child(' + fl + ')').addClass("fadeInUp").removeClass("op-0");
                $('#footer .home-bakr ul li:nth-child(' + fl + ')').addClass("fadeInRight").removeClass("op-0");
                if (fl > 5) {
                    clearInterval();
                }
            }, 500);

            setTimeout(function () {
                $('#footer .addr').addClass("fadeInUp").removeClass("op-0");
                var bfl = 0;
                setInterval(function () {
                    
                    $('#footer .addr h6,#footer .addr h6').addClass("fadeIn").removeClass("op-0");
                    bfl++;
                    $('#footer .addr ul li:nth-child(' + bfl + ')').addClass("fadeInUp").removeClass("op-0");

                    if (bfl > 3) {
                        $('#footer .bottom-foot').addClass("fadeInUp").removeClass("op-0");
                    }

                },500);
                
            },2000);
        }

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
});



