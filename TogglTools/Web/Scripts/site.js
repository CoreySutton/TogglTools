var Site = (function() {

    var pub = {};

    pub.Init = function() {
        $(document).on("click",
            "#advanced-settings-checkbox",
            function(eventObject) {
                if ($(this).is(':checked')) {
                    $(".advanced-setting").prop("readonly", false);
                } else {
                    $(".advanced-setting").prop("readonly", true);
                    $("#advanced-setting-round-at").val(5);
                    $("#advanced-setting-round-to").val(15);
                }
            }
        );
    };

    pub.onSelectWorkspaceRadio = function() {
        var selectedWorkspaceRadioButton = $(".workspace-radio-item:checked")[0];

        $("#workspace-id-form-control").val($(selectedWorkspaceRadioButton).val());
    };

    pub.onBeginWorkspaceRequest = function() {
        $("#workspaces-response-sub-section").remove();
        $("#report-response-sub-section").remove();

        $("#workspaces-response-section").html("<div id=\"loading-gif-section\"><img src=\"/Content/Loading.gif\"</div>");
    }

    pub.onBeginReportRequest = function ()
    {
        $("#report-response-sub-section").remove();

        $("#report-response-section").html("<div id=\"loading-gif-section\"><img src=\"/Content/Loading.gif\"</div>");
    }

    pub.onFailureReportRequest = function (p1, p2, p3) {
        $("#report-response-section").html("<p>Failed to retrieve report</p>")
    }

    var wait = function(ms)
    {
        var start = Date.now(),
            now = start;
        while (now - start < ms)
        {
            now = Date.now();
        }
    }

    var hideForm = function()
    {
        $("#workspaces-request-form").hide();
    }

    var showFailure = function()
    {
        $('#workspaces-request-message-area').html("<div class='alert alert-danger'><strong>Error!</strong>The server could not be contacted and your message has not been sent. Please check your internet connection and try again later.</div>");
    }

    var onSuccess = function()
    {
        alert("Success");
    }

    return pub;
})();

$(document).ready(Site.Init);