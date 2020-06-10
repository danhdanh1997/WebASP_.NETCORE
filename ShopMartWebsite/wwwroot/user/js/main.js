/*Slider Init*/
$(document).ready(function(){
    $('.site-slider .slider-one').not('.slick-intialized').slick({
        autoplay: true,
        speed: 50,
        dots: true,
        prevArrow: ".site-slider .slider-btn .prev",
        nextArrow: ".site-slider .slider-btn .next"
    });
});
