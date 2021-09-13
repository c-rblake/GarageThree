// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


window.setTimeout("closeDiv();", 10000);
function closeDiv() {
    let Temp = document.getElementById("threeSeconds")
    /*let Temp = document.getElementsByClassName("disappears");*/
    if (Temp != null)
        Temp.style.display = "none";

    // jQuery version
    $("#threeSeconds").fadeOut("slow", null);
}