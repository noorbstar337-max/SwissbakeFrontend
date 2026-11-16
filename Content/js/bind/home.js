$(document).ready(function () {
    // $('#datatable1').dataTable();
    get("@Request.Url.Segments[2]");
    //$('select[name=Year]').click(function () {
    //    //$('button[name=yearsubmit]').click();
    //    getOrder($("select[name=Year]").val(), $("#type").val());
    //});
});
function get(url) {

    var id = '@Session["U_Id"]';
    if (id == '' || id == null) {
        id = 0;
    }
    $.ajax({
        url: '@Url.Action("Product","Access",null)',
        type: "Post",
        beforeSend: function () {
            //_divPartialView.style = "Display: none";
            //$("#divWaiting").removeClass("display-none");
        },
        data: { u: url, u_id: id },
        success: function (response) {

            //_divPartialView.style = "";
            //$("#divWaiting").addClass("display-none");
            $("#shop").html(response).removeAttr("style");
            $("#shop .product").removeAttr("style");
            window.history.pushState("state", "title", url);
            // $('#datatable1').dataTable();
        }
    });
    //  return false;// if it's a link to prevent post
}