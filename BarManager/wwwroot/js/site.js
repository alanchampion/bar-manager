// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.

function getCookie(cname) {
    var name = cname + "=";
    var decodedCookie = decodeURIComponent(document.cookie);
    var ca = decodedCookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}

"use strict";
if (typeof signalR !== 'undefined') {
    var connection = new signalR.HubConnectionBuilder().withUrl("/ingredientHub").build();

    //Disable send button until connection is established
    var ownedChecks = document.getElementsByClassName("ownedCheck")
    var favoriteChecks = document.getElementsByClassName("favoriteCheck")
    for (var i = 0; i < ownedChecks.length; i++) {
        ownedChecks.item(i).disabled = true;
        favoriteChecks.item(i).disabled = true;
    }

    connection.start().then(function () {
        for (var i = 0; i < ownedChecks.length; i++) {
            ownedChecks.item(i).disabled = false;
            favoriteChecks.item(i).disabled = false;
        }
    }).catch(function (err) {
        return console.error(err.toString());
    });

    for (var i = 0; i < ownedChecks.length; i++) {
        ownedChecks.item(i).addEventListener('change', function (event) {
            // alert("Changing " + this.checked);
            // alert("Changing " + this.id);
            var id = this.id;
            var owned = this.checked;
            connection.invoke("UpdateIngredientOwned", id, owned).catch(function (err) {
                return console.error(err.toString());
            });
            event.preventDefault();
        });

        favoriteChecks.item(i).addEventListener('change', function (event) {
            // alert("Changing " + this.checked);
            // alert("Changing " + this.id);
            var id = this.id;
            var favorite = this.checked;
            connection.invoke("UpdateFavorite", id, favorite).catch(function (err) {
                return console.error(err.toString());
            });
            event.preventDefault();
        });
    }
}

$(document).ready(function () {
    $('.rating-enabled').rating({
        hoverOnClear: false,
        theme: 'krajee-svg',
        showCaption: false,
        animate: false
    });
    $('.rating-disabled').rating({
        hoverOnClear: false,
        theme: 'krajee-svg',
        showCaption: false,
        showClear: false,
        animate: false,
        displayOnly: true
    });
});