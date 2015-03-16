$(document).ready(function () {
    ////amazon menu
    amazonmenu.init({
        menuid: 'mysidebarmenu'
    });
 
    //    $("#menu").menu();




   $.getJSON('/Admin/DepartmentMenus/GetDep', function (data) {
       console.log(data);
       var getMenuItem = function (itemData) {
           var item = $("<li>")
               .append(
           $("<a>", {
               href: '#' + itemData.path,
               html: itemData.name
           }));
           if (itemData.children) {
               var subList = $("<ul>");
               $.each(itemData.children, function () {
                   subList.append(getMenuItem(this));
               });
               item.append(subList);
           }
           return item;
       };

       var $menu = $("#menu3");
       $.each(data, function () {
           $menu.append(
               getMenuItem(this)
           );
       });

   });



    $.typeahead({
        input: "#game_v1-query",
        minLength: 1,
        maxItem: 15,
        order: "asc",
        hint: true,
        group: true,
        groupMaxItem: 10,
        filter: "all",
        source: {
            game: {
                data: [{
                    id: 777,
                    display: "Sky Shooter"
                }, {
                    id: 1001,
                    display: "Elite shooter 6"
                }],
                url: ["http://www.gamer-hub.com/game/list.json/{callback}", "data"]
            },
            category: {
                url: ["http://www.gamer-hub.com/category/list.json/{callback}", "data"]
            },
            tag: {
                url: ["http://www.gamer-hub.com/tag/list.json/{callback}", "data"]
            }
        },
        callback: {
            onClick: function (node, a, obj, e) {
                window.open(
                    "http://www.gamer-hub.com/" +
                        obj.group + "/" +
                        obj.id + "/" +
                        obj.display.replace(/[\s]|:\s/g, "-")
                            .replace("'", "-")
                            .toLowerCase()
                        + "/"
                );
            },
            onMouseEnter: function (node, a, obj, e) {
                if (obj.group !== "game") {
                    return false;
                }
 
                if (!$(a).find('.popover')[0]) {
 
                    $(a).append(
                        $('<div>', {
                            "class": "popover fade right in",
                            "html": $('<div>', {
                                "class": "popover-content",
                                "html": $('<img>', {
                                    "src": "http://cdn.gamer-hub.com/images/" +
                                        obj.display.replace(/[\s]|:\s/g, "-")
                                            .replace("'", "-")
                                            .toLowerCase()
                                        + ".jpg"
                                })
                            }).prepend($('<div>', {
                                "class": "arrow"
                            }))
                        })
                    );
 
                } else {
                    $(a).find('.popover').removeClass('out').addClass('in');
                }
            },
            onMouseLeave: function (node, a, obj, e) {
                if (obj.group !== "game") {
                    return false;
                }
 
                $(a).find('.popover').removeClass('in').addClass('out');
            }
        }
    });
 


})






////department search 
//    var data = [{ id: 0, text: 'enhancement' }, { id: 1, text: 'bug' }, { id: 2, text: 'duplicate' }, { id: 3, text: 'invalid' }, { id: 4, text: 'wontfix' }];

//    var departmentselect = $(".js-example-data-array-selected");
//    departmentselect.select2({
      
//        data: data,
//        minimumResultsForSearch: Infinity,
//        placeholder: "All",
//        maximumSelectionLength: 3,
//        allowClear: true,theme:'classic'
//    }).on("select2:select", (function () {


//        console.log(departmentselect.val());
//    }));
//    // type ahead
//    var countries = new Bloodhound({
//        datumTokenizer: Bloodhound.tokenizers.obj.whitespace('name'),
//        queryTokenizer: Bloodhound.tokenizers.whitespace,
//        limit: 10,
//        prefetch: {
//            // url points to a json file that contains an array of country names, see
//            // https://github.com/twitter/typeahead.js/blob/gh-pages/data/countries.json
//            url: '../data/countries.json',
//            // the json file contains an array of strings, but the Bloodhound
//            // suggestion engine expects JavaScript objects so this converts all of
//            // those strings
//            filter: function (list) {
//                return $.map(list, function (country) { return { name: country }; });
//            }
//        }
//    });

//    // kicks off the loading/processing of `local` and `prefetch`
//    countries.initialize();

//    // passing in `null` for the `options` arguments will result in the default
//    // options being used
//    $('#prefetch .typeahead').typeahead(null, {
//        name: 'countries',
//        displayKey: 'name',
//        // `ttAdapter` wraps the suggestion engine in an adapter that
//        // is compatible with the typeahead jQuery plugin
//        source: countries.ttAdapter()
//    });


//$(".js-data-example-ajax").select2({
//    ajax: {
//        url: "https://api.github.com/search/repositories",
//        dataType: 'json',
//        delay: 250,
//        data: function (params) {
//            return {
//                q: params.term, // search term
//                page: params.page
//            };
//        },
//        processResults: function (data, page) {
//            // parse the results into the format expected by Select2.
//            // since we are using custom formatting functions we do not need to
//            // alter the remote JSON data
//            return {
//                results: data.items
//            };
//        },
//        cache: true
//    },
//    escapeMarkup: function (markup) { return markup; }, // let our custom formatter work
//    minimumInputLength: 3,
//    templateResult: formatRepo, // omitted for brevity, see the source of this page
//    templateSelection: formatRepoSelection, // omitted for brevity, see the source of this page
//    minimumResultsForSearch: Infinity,
//    placeholder: "Search here....",
//    allowClear: true

//});

//function formatRepo(repo) {
//    if (repo.loading) return repo.text;

//    var markup = '<div class="clearfix">' +
//    '<div class="col-sm-2">' +
//    '<img src="' + repo.owner.avatar_url + '" style="max-width: 100%" />' +
//    '</div>' +
//    '<div clas="col-sm-4">' +
//    '<div class="clearfix">' +
//    '<div class="col-sm-1">' + repo.full_name + '</div>' +
//    '<div class="col-sm-1"><i class="fa fa-code-fork"></i> ' + repo.forks_count + '</div>' +
//    '<div class="col-sm-1"><i class="fa fa-star"></i> ' + repo.stargazers_count + '</div>' +
//    '</div>';

//    if (repo.description) {
//        markup += '<div>' + repo.description + '</div>';
//    }

//    markup += '</div></div>';

//    return markup;
//}

//function formatRepoSelection(repo) {
//    return repo.full_name || repo.text;
//}
