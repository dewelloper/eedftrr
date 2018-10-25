/// <reference path="jquery.js" />
/// <reference path="base.js" />
/// <reference path="jquery.modal.js" />

function onLogin(username, password) {

    if (isNullOrEmpty(username) || isNullOrEmpty(password)) {
        showUIMessage("warning", "Login", "Please Enter Username or Password");
        return false;
    }

    var parameters =
        {
            "Username": username,
            "Password": password,
        };
    var jsonText = JSON.stringify({ parameters: parameters });

    $.ajax
    ({
        type: 'POST',
        url: '../GenericHelper.aspx/LoginUser',
        data: jsonText,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response)
        {
            var result = response.d;
            if (!result.HasError)
            {
                window.location = "Dashboard.aspx?AppVersion=1.1&Uri=None";
            }
            else
            {
                var errorMessage = result.ErrorMessage;
                showUIMessage("error", "Verify Error", errorMessage);
            }
        },
        error: function (jqXHR, textStatus, errorThrown)
        {
            var errorMessage = jqXHR + textStatus + errorThrown;
            showUIMessage("error", "ApplicationError", errorMessage);
        }
    });


}