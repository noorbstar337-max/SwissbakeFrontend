$(".cat-button").hover(function () {
    $(".cat-menu").toggleClass("transition-off");
});

$(document).ready(function () {
    $('.log-reg').click(function () {
        
        //$('.side-panel-wrap .side-log').removeClass("d-none");
        $('.side-panel-wrap .side-log').toggleClass("d-none");
        //$('body').toggleClass("side-panel-left");
    });
    $('.cart-sect').click(function () {
        
        $('.side-panel-wrap .side-cart').toggleClass("d-none");
        setTimeout(function () {
            $('.side-panel-wrap .side-cart .mast-cart-ttl').toggleClass("open");
        },100);
        
    });
    
    $('.body-overlay').click(function () {

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

    setTimeout(function () {
        $("header").removeAttr("style");
    },3000);

    setTimeout(function () {
        var i = 0;
        var oc = $("#oc-portfolio .oc-item").length;
        $(".right-menu .animated").addClass("zoomIn");
        //$("nav .animated").addClass("slideInDown");
        
        $("#slider.animated").addClass("fadeInUp");
        if (screen.width <= 479){
            $("#logo.animated").addClass("fadeInDown");
            $("#primary-menu-trigger").addClass("bubble").removeClass("op-0");
        }
        else {
            $("#logo.animated").addClass("fadeInLeft");
        }
        //for (var j = 1; j < oc + 1; j++){

        //    $("#oc-portfolio .owl-item:nth-child(" + j + ") > .animated").addClass("fadeInUp");
        //}

        //alert(oc);
        setInterval(function () {
            i++;
            $("#oc-portfolio .owl-item:nth-child(" + i + ") > .animated").addClass("fadeInUp").removeClass("op-0");

            if (i === oc) {
                clearInterval();
            }
        },1000);
        $(".right-menu .animated,nav .animated,#logo.animated,#slider.animated").removeClass("op-0");
        
    },4000);
    
});



