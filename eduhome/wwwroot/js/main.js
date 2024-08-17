(function ($) {
"use strict";  
    document.querySelectorAll(".buy").forEach(btn => {
        btn.addEventListener("click", function () {
            const courseId = this.getAttribute("course-id");

            $.ajax({
                url: `/Basket/Add?courseId=${courseId}`,
                method: "POST",
                success: function (response) {
                    if (response.success) {
                        Swal.fire({
                            icon: "success",
                            title: response.message,
                            showConfirmButton: false,
                            timer: 1500
                        });
                        btn.textContent = "Acquired";
                        btn.disabled = true;
                    } else if (response.redirectUrl) {
                        window.location.href = response.redirectUrl;
                    }
                }
            });
        });
    });


    const deleteBtn = document.querySelectorAll(".course-delete");
    deleteBtn.forEach(btn => {
        btn.addEventListener("click", function () {
            Swal.fire({
                title: "Are you sure?",
                text: "You won't be able to revert this!",
                icon: "warning",
                showCancelButton: true,
                confirmButtonColor: "#3085d6",
                cancelButtonColor: "#d33",
                confirmButtonText: "Yes, delete it!"
            }).then((result) => {
                if (result.isConfirmed) {
                    Swal.fire({
                        title: "Deleted!",
                        text: "Your file has been deleted.",
                        icon: "success"
                    });
                    const courseId = $(this).attr("course-id");
                    const deleteBtn=$(this)
                    $.ajax({
                        url: `/Basket/Remove?courseId=${courseId}`,
                        method: "Post",
                        success: function () {
                            deleteBtn.closest("tr").remove();
                        }
                    })
                }
            });
        })
    })



    //comment
    $(".reply-btn").on("click", function () {
        const courseId = $(this).attr("course-id");
        const message = $(".comment").val();
        $.ajax({
            url: `/course/addComment?courseId=${courseId}&message=${message}`,
            method: "Post",
            success: function (datas) {
              console.log(message)
            },
         
        })
    });

    //loadmore
    let skip = 3;
    $("#loadmore").on("click", function () {
        $.ajax({
            url: "/course/LoadMore?offset=" + skip,
            method: "get",
            success: function (datas) {
                $('#courseList').append(datas);
                skip += 3;
                if (skip >= $("#CourseCount").val()) {
                    $('#loadmore').remove();
                }
            },
            error: function (error) {
                console.log(error);
            }
        });
    })










    //search
    $("#input-search").on("keyup", function () {
        $("#searchList li").slice(1).remove();
        var value = $(this).val().trim();
        if (value) {
            $.ajax({
                url: "/blog/SearchBlog?text=" + value,
                method: "get",
                success: function (datas) {
                    $("#searchList").append(datas)
                },
                error: function (error) {
                    console.log(error);
                }
            })
        }
    });

    $("#input-search").on("keyup", function () {
        $("#searchList li").slice(1).remove();
        var value = $(this).val().trim();
        if (value) {
            $.ajax({
                url: "/course/CourseSearch?text=" + value,
                method: "get",
                success: function (datas) {
                    $("#searchList").append(datas)
                },
                error: function (error) {
                    console.log(error);
                }
            })
        }
    });

    $("#input-search").on("keyup", function () {
        $("#searchList li").slice(1).remove();
        var value = $(this).val().trim();
        if (value) {
            $.ajax({
                url: "/event/EventSearch?text=" + value,
                method: "get",
                success: function (datas) {
                    $("#searchList").append(datas)
                },
                error: function (error) {
                    console.log(error);
                }
            })
        }
    });






/*------------------------------------
	Sticky Menu 
--------------------------------------*/
 var windows = $(window);
    var stick = $(".header-sticky");
	windows.on('scroll',function() {    
		var scroll = windows.scrollTop();
		if (scroll < 5) {
			stick.removeClass("sticky");
		}else{
			stick.addClass("sticky");
		}
	});  
/*------------------------------------
	jQuery MeanMenu 
--------------------------------------*/
	$('.main-menu nav').meanmenu({
		meanScreenWidth: "767",
		meanMenuContainer: '.mobile-menu'
	});
    
    
    /* last  2 li child add class */
    $('ul.menu>li').slice(-2).addClass('last-elements');
/*------------------------------------
	Owl Carousel
--------------------------------------*/
    $('.slider-owl').owlCarousel({
        loop:true,
        nav:true,
        animateOut: 'fadeOut',
        animateIn: 'fadeIn',
        smartSpeed: 2500,
        navText:['<i class="fa fa-angle-left"></i>','<i class="fa fa-angle-right"></i>'],
        responsive:{
            0:{
                items:1
            },
            768:{
                items:1
            },
            1000:{
                items:1
            }
        }
    });

    $('.partner-owl').owlCarousel({
        loop:true,
        nav:true,
        navText:['<i class="fa fa-angle-left"></i>','<i class="fa fa-angle-right"></i>'],
        responsive:{
            0:{
                items:1
            },
            768:{
                items:3
            },
            1000:{
                items:5
            }
        }
    });  

    $('.testimonial-owl').owlCarousel({
        loop:true,
        nav:true,
        navText:['<i class="fa fa-angle-left"></i>','<i class="fa fa-angle-right"></i>'],
        responsive:{
            0:{
                items:1
            },
            768:{
                items:1
            },
            1000:{
                items:1
            }
        }
    });
/*------------------------------------
	Video Player
--------------------------------------*/
    $('.video-popup').magnificPopup({
        type: 'iframe',
        mainClass: 'mfp-fade',
        removalDelay: 160,
        preloader: false,
        zoom: {
            enabled: true,
        }
    });
    
    $('.image-popup').magnificPopup({
        type: 'image',
        gallery:{
            enabled:true
        }
    }); 
/*----------------------------
    Wow js active
------------------------------ */
    new WOW().init();
/*------------------------------------
	Scrollup
--------------------------------------*/
    $.scrollUp({
        scrollText: '<i class="fa fa-angle-up"></i>',
        easingType: 'linear',
        scrollSpeed: 900,
        animation: 'fade'
    });
/*------------------------------------
	Nicescroll
--------------------------------------*/
     $('body').scrollspy({ 
            target: '.navbar-collapse',
            offset: 95
        });
$(".notice-left").niceScroll({
            cursorcolor: "#EC1C23",
            cursorborder: "0px solid #fff",
            autohidemode: false,
            
        });

})(jQuery);	