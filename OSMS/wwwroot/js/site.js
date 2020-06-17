// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).on("click", "#BtnShowRegistrationModalForm", function () {
    FetchData("/Home/ShowRegistrationForm", null).done(function (modalContent) {
        $("#modalContentRegistrationForm").html(modalContent);
    });
});

$(document).on("click", "#BtnShowLoginModalForm", function () {
    FetchData("/Home/ShowLoginModalForm", null).done(function (modalContent) {
        $("#modalContentLoginForm").html(modalContent);
    });
});