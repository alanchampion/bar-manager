// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.
"use strict";
if (typeof signalR !== 'undefined') {
    var connection = new signalR.HubConnectionBuilder().withUrl("/ingredientHub").build();

    //Disable send button until connection is established
    var checks = document.getElementsByClassName("ownedCheck")
    for (var i = 0; i < checks.length; i++) {
        checks.item(i).disabled = true;
    }

    connection.start().then(function () {
        for (var i = 0; i < checks.length; i++) {
            checks.item(i).disabled = false;
        }
    }).catch(function (err) {
        return console.error(err.toString());
    });

    for (var i = 0; i < checks.length; i++) {
        checks.item(i).addEventListener('change', function (event) {
            // alert("Changing " + this.checked);
            // alert("Changing " + this.IngredientId);
            var user = "achampion"; // TODO get user from logged in info
            var id = this.id;
            var owned = this.checked;
            connection.invoke("UpdateIngredientOwned", user, id, owned).catch(function (err) {
                return console.error(err.toString());
            });
            event.preventDefault();
        });
    }
}