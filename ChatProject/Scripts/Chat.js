$(document).ready(function () {

    var button = $("#btnlogin");
    button.click(function () {
        var name = $("#txtusername").val();
        if (name) {
            LinkFunction("LoginButton", name, true)
            $("#username").text(name);
        }
    });
})
function LoginOnFailure(result) {
    $("#username").text("");
    $("#error").text(result.statusText);
    setTimeout("$('#error').empty();", 2000)
};

function LoginOnSuccess(result) {
    Refresh();
    $("#logout").click(function () {
        var name = $("#username").text();
        LinkFunction("ActionLink", name, false, true);
        document.location.href = "Home";
    })

    $("#sendmsg").click(function () {
        var name = $("#username").text();
        var textarea = $("#message");
        var message = textarea.val();
        
        LinkFunction("ActionLink", name, false, false, message);
        textarea.val("");
    })
};

function Refresh() {
    var name = $("#username").text();
    LinkFunction("ActionLink", name);

    setTimeout("Refresh()", 5000);
}

function OnChatSuccess() {

}

function OnChatFailure() {}

function LinkFunction(linkId, name, logon, logout, message) {
    var encodedName = encodeURIComponent(name);
    var href = "/Home/Index?name=" + encodedName;
    if (logon) {
        href += "&logOn=true";
    }

    if (logout) {
        href += "&logOff=true";
    }
    if (message) {
        var encodedMessage = encodeURIComponent(message);
        href += "&message=" + encodedMessage;
        shouldPlay = true;
    }

    var link = $("#" + linkId);
    link.attr("href", href).click();
}
