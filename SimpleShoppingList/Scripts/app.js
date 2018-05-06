var currentList = {};

function createShoppingList() {
    currentList.name = $("#shoppingListName").val();
    currentList.items = new Array();

    // Web Service Call
    $.ajax({
        type: "POST",
        dataType: "json",
        url: "api/ShoppingList/",
        data: currentList,
        success: function (result) {
            showShoppingList();
        }
    });
}

function showShoppingList() {
    $("#shoppingListTitle").html(currentList.name);
    $("#shoppingListItems").empty();

    $("#createListDiv").hide();
    $("#shoppingListDiv").show();

    $("#newItemName").focus();
    $("#newItemName").keyup(function (event) {
        if (event.keyCode === 13) {
            addItem();
        }
    });
}

function drawItems() {
    var $list = $("#shoppingListItems").empty();

    for (var i = 0; i < currentList.items.length; i++) {
        var currentItem = currentList.items[i];
        var $li = $("<li>").html(currentItem.name)
            .attr("id", "item_" + i);
        var $deleteBtn = $("<button onclick='deleteItem("+ i +")'>D</button>").appendTo($li);
        var $checkBtn = $("<button onclick='checkItem(" + i +")'>C</button>").appendTo($li);

        $li.appendTo($list);
    }
}

function deleteItem(index) {
    currentList.items.splice(index, 1);
    drawItems();
}

function checkItem(index) {
    if ($("#item_" + index).hasClass("checked")) {
        $("#item_" + index).removeClass("checked");
    } else {
        $("#item_" + index).addClass("checked");
    }   
}

function addItem() {
    var newItem = {};
    newItem.name = $("#newItemName").val();
    newItem.showShoppingListId = currentList.id;

    $.ajax({
        type: "POST",
        dataType: "json",
        url: "api/Item/",
        data: newItem,
        success: function (result) {
            currentList = result;
            drawItems();
            $("#newItemName").val("");
        }
    });

    drawItems();
    $("#newItemName").val("");
}

function getShoppingListById(id) {
    $.ajax({
        type: "GET",
        dataType: "json",
        url: "api/ShoppingList/" + id,
        success: function(result) {
            currentList = result;
            showShoppingList();
            drawItems();
        }
    });
}

$(document).ready(function () {
    console.info("ready");
    $("#shoppingListName").focus();
    $("#shoppingListName").keyup(function(event) {
        if (event.keyCode === 13) {
            createShoppingList();
        }
    });

    var pageUrl = window.location.href;
    var idIndex = pageUrl.indexOf("?id=");
    if (idIndex !== -1) {
        getShoppingListById(pageUrl.substring(idIndex + 4));
    }
});