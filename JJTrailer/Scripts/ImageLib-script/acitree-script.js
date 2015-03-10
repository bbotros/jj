$(function () {

    // listen for the events before we init the tree
    $('#tree').on('acitree', function (event, api, item, eventName, options) {
        // do some stuff on init
        if (eventName == 'init') {

            // get the first item
            var firstItem = api.first();

            // then select it
            api.select(firstItem);

        }
    });

    // init the tree
    var tree=$('#tree').aciTree({
        ajax: {
            url: '/Admin/ImageLibs/gettree'
        },
        selectable: true
    });
    console.log(tree);
});