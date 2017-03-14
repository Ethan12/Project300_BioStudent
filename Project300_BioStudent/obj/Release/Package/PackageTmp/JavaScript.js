// JavaScript Document
"use script";
function carousel() {
    var img = new Image();
    var div = document.getElementById('mySlide');

    img.onload = function () {
        div.appendChild(img);
    };

    var slideArray = ["images/LoginRegistrationImage.jpg", "images/Attendance.jpg", "images/Attendance.jpg"];

    var slideIndex = 0;

    function imageChange() {
        img.setAttribute("src", slideArray[slideIndex]);
        img.setAttribute("width", "900px");
        img.setAttribute("height", "480px");
        img.style.backgroundColor = "#e8e8e8";
        img.style.padding = "10px";
        img.style.marginTop = "25px";
        img.style.boxShadow = "0px 1px 2px 1px rgba(0,0,0,0.1)";
        slideIndex++;
        if (slideIndex >= slideArray.length) {
            slideIndex = 0;
        }
    }

    var intervalHandle = setInterval(imageChange, 2000);

    img.onclick = function () {
        clearInterval(intervalHandle);
    };

    div.appendChild(img);
}
