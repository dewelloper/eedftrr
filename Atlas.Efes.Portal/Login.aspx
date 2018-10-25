<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Atlas.Efes.Portal.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <title>Atlas Efes Login</title>

    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport" />

    <link rel="stylesheet" href="bootstrap/css/bootstrap.min.css" />

    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.4.0/css/font-awesome.min.css" />

    <link rel="stylesheet" href="https://code.ionicframework.com/ionicons/2.0.1/css/ionicons.min.css" />

    <link rel="stylesheet" href="dist/css/AdminLTE.min.css" />



    <link href="css/jquery.modal.css" rel="stylesheet" />
    <link href="css/jquery.modal.theme-atlant.css" rel="stylesheet" />
    <link href="css/jquery.modal.theme-xenon.css" rel="stylesheet" />

    <script src="js/jquery.js"></script>
    <script src="js/app.login.js?Date=<%=DateTime.Now %>"></script>
    <script src="js/base.js?Date=<%=DateTime.Now %>"></script>
    <script src="js/jquery.modal.js"></script>
    <script>
        $(document).ready(function () {
            $("#btnLogin").click(function () {
                var username = $("#txtUsername").val();
                var password = $("#txtPassword").val();

                onLogin(username, password);

                return false;
            });
        });
    </script>

</head>
<body class="hold-transition login-page" style="background: #EAEAEA">
    <div class="login-box">
        <div class="login-logo">
            <b>ATLAS</b>EFES
        </div>

        <div class="login-box-body">
            <p class="login-box-msg">Login Portal</p>
            <div class="form-group has-feedback">
                <input type="text" id="txtUsername" class="form-control" placeholder="Username" />
                <span class="glyphicon glyphicon-user form-control-feedback"></span>
            </div>
            <div class="form-group has-feedback">
                <input id="txtPassword" type="password" class="form-control" placeholder="Password" />
                <span class="glyphicon glyphicon-lock form-control-feedback"></span>
            </div>
            <div class="row">
                <div class="col-xs-4">
                    <button type="submit" class="btn btn-primary btn-block btn-flat" id="btnLogin">Login</button>
                </div>
            </div>
        </div>

    </div>
</body>
</html>
