// This a separate Javascript file that contains functions used throughout the whole website.


// This url should point to API 
const URL = "api/";

// The id of the div in which to put the <p> elements describing the menu's contents
const MENU_CONTENTS_DIV_ID = "#menuContents";


function getMenus() {
    // This function gets the list of menus from the API
    // It displays them as <p> elements within the div with id = "MenuList"
    // It sets the onclick event of each to call showMenu(#), where # is that menu's id number

    // Jquery ajax call to get the list containing the menu info
    $.ajax({                        // The $ (dollar sign) is used to access the Jquery functions, in this case an ajax call
        method: 'GET',              // This is a GET request
        url: URL + "menu",          // The URL we want is api/menu
        dataType: "json",           // The datatype we expect the server to return, the JSON will automatically be parsed and converted into a JavaScript object
        success: function (menuList) {  // This inline function will be called if the request is successful
            // Note that this function is called asynchronously at a later time, so any data you get back must be maniputed here.
            // the variable 'menuList' is now a js array that holds menu objects
            // It cannot be used outside of this function

            // Use Jquery to empty the menuList div of any elements
            $('#menuList').empty();

            // loop through them
            for (let i = 0; i < menuList.length; i++) {
                let menu = menuList[i]; 

                // Using Jquery create a new <p> object with the menu's info
                // It has id property: id = menu#, where # is that menu's id number
                // It also adds an onclick event, so that when clicked it will call showMenu(#), where # is that menu's id number
                $('<p id="menu' + menu.id + '" onclick="showMenu(' + menu.id + ')"><strong>' + menu.name + '</strong></p>'
                ).appendTo($('#menuList')); // Add it to the menuList div
            }

        },
        error: function (jqXHR, status, errorThrown) {      // This function will run if there's an error
            alert("ERROR: Can't retrieve menu list " + errorThrown + " ");   // Pop up a textbox with an error message
        }
    });
    

}

function showMenu(id = 1) {
    // shows a specific menu, the first paramenter is the id, which defaults to 1 (the first menu)
    // The affected div is given by the MENU_CONTENTS_DIV_ID constant 
    // creates nested divs and <p> inside the specified div (also clears any elements currently in the div)

    // Jquery ajax call to get the specific menu
    $.ajax({
        method: 'GET',
        url: URL + "menu/" + id,    // The URL we want is api/menu/#
        dataType: "json",           
        success: function (menu) {  // function called if successful
            // Note that this function is called asynchronously at a later time, so any data you get back must be maniputed here.
            // the variable 'menu' is now a menu object that cannot be used outside of this function

            // Use Jquery to empty the div of any elements
            $(MENU_CONTENTS_DIV_ID).empty();
            
            // Using Jquery create a new <p> elements with the menu's info
            $('<p id="menu' + menu.id + '" ><strong>' + menu.name + '</strong></p>' +
                '<p><i> ' + menu.description + ' </i></p>' +
                '<br />'
            ).appendTo($(MENU_CONTENTS_DIV_ID)); // Add it to the specified div

            // loop through the sectionList variable of the menu object
            for (let i = 0; i < menu.sectionList.length; i++) {
                let section = menu.sectionList[i];

                // display each section 
                showSection(section);
            }
            

        },
        error: function (jqXHR, status, errorThrown) {      // This function will run if there's an error
            alert("ERROR: Can't retrieve that menu " + errorThrown + " ");   // Pop up a textbox with an error message
        }
    });
}

function showSection(section) {
    // takes a section object and adds <p> describing it to the menu contents div (given by the constant MENU_CONTENTS_DIV_ID)

    $('<p id="section' + section.id + '" ><strong>' + section.name + '</strong></p>' +
        '<p><i> ' + section.description + ' </i></p>'
    ).appendTo($(MENU_CONTENTS_DIV_ID)); // Add it to the specified div

    // loop through the itemList variable of the section object
    for (let i = 0; i < section.itemList.length; i++) {
        let item = section.itemList[i];

        // display each item 
        showItem(item);
    }

    // add line break
    $('<br />').appendTo($(MENU_CONTENTS_DIV_ID));
}

function showItem(item) {
    // takes an item object and adds <p> describing it to the menu contents div (given by the constant MENU_CONTENTS_DIV_ID)

    $('<p id="item' + item.id + '" >' + item.position + '. ' + item.name + '</p>' +
        '<p><i> ' + item.description + ' </i></p>'
    ).appendTo($(MENU_CONTENTS_DIV_ID)); // Add it to the specified div

    // check if there's only one priceline item with no description
    if (item.priceLineList.length == 1 && item.priceLineList[0].description == "") {
        // append the price to the end of the item name <p>
        $('#item' + item.id).append($('<span> - $' + item.priceLineList[0].price + '</span>'));
    }
    else {
        // otherwise loop through the priceLineList variable of the item object
        for (let i = 0; i < item.priceLineList.length; i++) {
            let priceline = item.priceLineList[i];

            // display each item 
            showPriceline(priceline);
        }
    }

    // add line break
    $('<br />').appendTo($(MENU_CONTENTS_DIV_ID));
}

function showPriceline(priceline) {
    // takes an priceline object and adds <p> describing it to the menu contents div (given by the constant MENU_CONTENTS_DIV_ID)

    $('<p id="priceline' + priceline.id + '" >' + priceline.description + ' - $' + priceline.price + '</p>'
    ).appendTo($(MENU_CONTENTS_DIV_ID)); // Add it to the specified div


}