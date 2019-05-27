
$(document).ready(function () {

    $(".collapsible").live("click", function (e) {
        e.preventDefault();
        var path="";
        var call = function (rec)
        {
            if(rec.parent().attr('name')==null)
            {
                return path;
            }
            else
            {
                path = rec.parent().attr('name') + '\\' + path;
                return call(rec.parent().parent());
            }
        }
        call($(this));
        var this1 = $(this); // Get Click item 
        var data = {
            nodePath: path.substring(0, path.length - 1)
        };
        
        var isLoaded = $(this1).attr('data-loaded'); // Check data already loaded or not
        if (isLoaded == "false") {
            $(this1).addClass("loadingP");   // Show loading panel
            $(this1).removeClass("collapse");

            // Now Load Data Here 
            $.ajax({
                url: "/api/Commodities/getNode",
                type: "GET",
                data: data,
                dataType: "json",
                success: function (obj) {
                    $(this1).removeClass("loadingP");
                    if (!obj.Successful)
                    {
                        alert(obj.Message);
                    }
                    var data = obj.Data;
                    if (data.ChildNodes.length > 0) {

                        var $ul = $("<ul></ul>");
                        $.each(data.ChildNodes, function (i, ele) {
                            $ul.append(
                                    $("<li name ='"+ele.Name+"' ></li>").append(
                                        "<span class='collapse collapsible' data-loaded='false' Name='"+ele.Name+"'>&nbsp;</span>" + 
                                        "<span>" + ele.Name + "</span>"
                                    )
                                )
                        });

                        $(this1).parent().append($ul);
                        $(this1).addClass('collapse');
                        $(this1).toggleClass('collapse expand');
                        $(this1).closest('li').children('ul').slideDown();
                    }
                    else {
                        // no sub menu
                        $(this1).css({'dispaly':'inline-block','width':'15px'});
                    }

                    $(this1).attr('data-loaded', true);
                },
                error: function () {
                    alert("Some error occured, please reload!");
                }
            });
        }
        else {
            // if already data loaded
            $(this1).toggleClass("collapse expand");
            $(this1).closest('li').children('ul').slideToggle();  
        }

    });
});