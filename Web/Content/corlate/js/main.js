jQuery(function ($) {
    'use strict',

    //#main-slider
        $(function () {
            $('#main-slider.carousel').carousel({
                interval: 8000
            });
        });


    // accordian
    $('.accordion-toggle').on('click', function () {
        $(this).closest('.panel-group').children().each(function () {
            $(this).find('>.panel-heading').removeClass('active');
        });

        $(this).closest('.panel-heading').toggleClass('active');
    });

    //Initiat WOW JS
    new WOW().init();

    // portfolio filter
    $(window).load(function () {
        'use strict';
        var $portfolio_selectors = $('.portfolio-filter >li>a');
        var $portfolio = $('.portfolio-items');
        $portfolio.isotope({
            itemSelector: '.portfolio-item',
            layoutMode: 'fitRows'
        });

        $portfolio_selectors.on('click', function () {
            $portfolio_selectors.removeClass('active');
            $(this).addClass('active');
            var selector = $(this).attr('data-filter');
            $portfolio.isotope({ filter: selector });
            return false;
        });
    });

    // Contact form
    var form = $('#main-contact-form');
    form.submit(function (event) {
        event.preventDefault();
        var form_status = $('<div class="form_status"></div>');
        $.ajax({
            url: $(this).attr('action'),

            beforeSend: function () {
                form.prepend(form_status.html('<p><i class="fa fa-spinner fa-spin"></i> Email is sending...</p>').fadeIn());
            }
        }).done(function (data) {
            form_status.html('<p class="text-success">' + data.message + '</p>').delay(3000).fadeOut();
        });
    });


    //goto top
    $('.gototop').click(function (event) {
        event.preventDefault();
        $('html, body').animate({
            scrollTop: $("body").offset().top
        }, 500);
    });

    //Pretty Photo
    $("a[rel^='prettyPhoto']").prettyPhoto({
        social_tools: false
    });
});


$(document).ready(function () {
    
    //Navigation Menu Slider
    $('#nav-expander').on('click', function (e) {
        e.preventDefault();
        $('body').toggleClass('nav-expanded');

        fnFix1();
       
    });
    $('#nav-close').on('click', function (e) {
        e.preventDefault();
        $('body').removeClass('nav-expanded');
        fnFix1();


    });


   
    // Initialize navgoco with default options
    $(".main-menu").navgoco({
        caret: '<span class="caret"></span>',
        accordion: false,
        openClass: 'open',
        save: true,
        cookie: {
            name: 'navgoco',
            expires: false,
            path: '/'
        },
        slide: {
            duration: 300,
            easing: 'swing'
        }
    });
  

});

function fnFix1() {
    var win = $(window); //this = window
    var isShowMenu = $("body").hasClass("nav-expanded");
    if (win.width() <= 767) { /* ... */

        
        if (isShowMenu) {
            $("html").css("overflow", "hidden");
        } else {
            $("html").css("overflow", "auto");
        }

    }
    /*
    
         margin-left: -230px;
      padding-left: 238px;
    */

    if (win.width() >= 1200) { /* ... */
        if (!isShowMenu) {
            $(".vcontainer").css("margin-left", "0px");
            $(".vcontainer").css("padding-left", "0px");
        } else {
            $(".vcontainer").css("margin-left", "-238px");
            $(".vcontainer").css("padding-left", "238px");
        }


        if (isShowMenu) {
            $(".main_products").css("marginRight", "240px");
        } else {
            $(".main_products").css("marginRight", "5px");
        }
    }
    if (win.width() >= 992 && win.width() <= 1199) { /* ... */
        
        if (!isShowMenu) {
            $(".vcontainer").css("margin-left", "0px");
            $(".vcontainer").css("padding-left", "0px");
        } else {
            $(".vcontainer").css("margin-left", "-238px");
            $(".vcontainer").css("padding-left", "238px");
        }



        if (isShowMenu) {
            $(".main_products").css("marginRight", "240px");
        } else {
            $(".main_products").css("marginRight", "5px");
        }
    }

    if (win.width() >= 768 && win.width() <= 991) { /* ... */
        $(".vcontainer").css("margin-left", "0px");
        $(".vcontainer").css("padding-left", "0px");
        $(".main_products").css("marginRight", "5px");
    }
    if (win.width() <= 767) { /* ... */
        $(".vcontainer").css("margin-left", "0px");
        $(".vcontainer").css("padding-left", "0px");
        $(".main_products").css("marginRight", "5px");
    }
   
}


var clsUser = new function () {
    var self = this;
    self.PloginSuccess = function (data) {
        if (data.r) {
            var re = $("#modalLogin").attr("data-redirect");
            if (re!=undefined && re != "") {
                window.location.href = re;
            } else {
                window.location.reload(false);
            }
        } else {
            alert("Tài khoản mật khẩu không chính xác!");
        }
    }
    self.PRegisterSuccess = function (data) {
        if (data.r) {
            window.location.href = "/home/index";
        } else {
            alert(data.m);
        }
    }
    self.CheckUserLogin = function (_url) {
        $.getJSON("/ajax/CheckUserLogin", function (data) {
            if (data.r) {
                window.location.href = _url;
            } else {
                alert(data.m);
            }
        });
    }
    self.showPopLogin = function (title,redirect) { //clsUser.openDangNhap
        $("#modalLogin").modal("show");
        $("#modalLogin").attr("data-redirect", redirect);
        if (title != "") {
            $("#modalLogin").find(".modal-title").html(title);
        }
    }
    self.fnShowDoiMatKhau = function() {
        $("#mymodal").find(".modal-content").load("/user/doimatkhau",function() {
            $("#mymodal").modal("show");
            $.validator.unobtrusive.parse($("#mymodal form"));
        });
    }
    self.fnDoiMatKhauSuccess = function(data) {
        alert(data.m);
        if (data.r) {
            $("#mymodal").modal("hide");
        }
    }

    self.fnShowCapNhatThongTin = function () {
        $("#mymodal").find(".modal-content").load("/user/capnhat", function () {
            $("#mymodal").modal("show");
            $.validator.unobtrusive.parse($("#mymodal form"));
        });
    }
    self.fnCapNhatThongTinSuccess = function (data) {
        if (data.r) {
            toastr.success(data.m);
            $("#mymodal").modal("hide");
            window.location.reload(false);
        } else {
            toastr.error(data.m);
        }
    }
}

var $loading = $('#loadingDiv').hide();
$(document)
  .ajaxStart(function () {
      $loading.show();
  })
  .ajaxStop(function () {
      $loading.hide();
  });




$(window).on('resize', function () {
    var win = $(this); //this = window

    if (win.width() >= 1200) { /* ... */
        $('body').addClass('nav-expanded');
    }
    if (win.width() >= 992 && win.width() <= 1199) { /* ... */ 
        $('body').addClass('nav-expanded');
    }

    if (win.width() >= 768 && win.width() <= 991) { /* ... */
        $('body').removeClass('nav-expanded');
    }
    if (win.width() <= 767) { /* ... */
        $('body').removeClass('nav-expanded');
    }
    fnFix1();
});
$(function() {
    $(window).trigger('resize');
});

var clsThongBao = new function () {
    var self = this;
    self.modalThongBao = $("#modalThongBao");

    self.hienthithongbao= function() {
        $.getJSON("/thongbao/laythongbao", function(data) {
            if (data != null) {
                console.log(data);
                self.modalThongBao.find(".modal-title").html(data.TieuDe);
                self.modalThongBao.find(".modal-body").html(data.NoiDung);
                self.modalThongBao.modal("show");
            }
        });
    }
    self.anthongbao=function() {
        
    }
}


clsThongBao.hienthithongbao();