window.events = (function (jQuery, kendo, console) {
    var pub = {};

    // public
    pub.root;

    pub.init = function () {
        $(document).delegate(".k-button", "click", function (e) {
            var that = $(this),
                eventData = that.data("event");
            _clickHandlers[eventData](e);
        });
    }

    // private
    _clickHandlers = {
        "addItem": function (event) {
            if ($.trim($("#new-item").val()) != "") {

                app.dataSource.add({ Name: $("#new-item").val() });

                app.dataSource.sync();

                // why do i need a separate read here?
               app.dataSource.read();

                $("#new-item").val("");
            }
        },
        "delete": function (event) {
            console.log("delete");

            var item = $(event.target).parents(".item");
            var itemToDestroy = app.dataSource.get(item.data("id"));

            item.find('h3').addClass('strikethrough')

            console.log("item: " + item.data("id"));

            app.dataSource.remove(itemToDestroy);
        },
        "saveAll": function (event) {
            console.log("save all");

            app.dataSource.sync();
        }
    }

    return pub;

})(jQuery, kendo, console);