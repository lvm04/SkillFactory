document.addEventListener("DOMContentLoaded", function () {

    let slideIndex = 0;
    const slides = document.querySelectorAll(".slider-item");
    const prev = document.querySelector(".prev");
    const next = document.querySelector(".next");
    const dotsWrap = document.querySelector(".slider-dots");
    const dots = document.querySelectorAll(".dot");

    showSlides();

    function showSlides(n) {

        // Циклическое перелистывание
        if (n > slides.length - 1) {
            slideIndex = 0;
        }
        if (n < 0) {
            slideIndex = slides.length - 1;
        }

        // Инициализация слайдов и точек
        slides.forEach((item) => item.style.display = "none");
        dots.forEach((item) => item.classList.remove("dot-active"));

        slides[slideIndex].style.display = "block";
        dots[slideIndex].classList.add("dot-active");
    }

    function nextSlide(n) {
        slideIndex += n;
        showSlides(slideIndex);
    }

    prev.addEventListener("click", function () {
        nextSlide(-1); 
    });

    next.addEventListener("click", function () {
        nextSlide(1);
    });

    dotsWrap.addEventListener("click", function(event) {
        for (let i = 0; i < dots.length; i++) {
            if (event.target.classList.contains("dot") && event.target == dots[i]) {
                showSlides(slideIndex = i);
            }
        }
    });

});