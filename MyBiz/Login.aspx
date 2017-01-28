<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="MyBiz.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="description" content="MyBiz makes writing business proposals and organizing tasks simple and straightforward." />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0" />
    <title>MyBiz | Login</title>

    <meta name="mobile-web-app-capable" content="yes" />

    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black" />
    <meta name="apple-mobile-web-app-title" content="MyBiz" />

    <meta name="msapplication-TileImage" content="images/touch/ms-touch-icon-144x144-precomposed.png" />
    <meta name="msapplication-TileColor" content="#3372DF" />

    <link rel="stylesheet" href="Content/mdl-v1.1.2/material.min.css" />
    <link rel="stylesheet" href="https://fonts.googleapis.com/icon?family=Material+Icons" />
    <link rel="stylesheet" href="Content/login.css" />
</head>
<body>
    
    <div class="mdl-layout mdl-js-layout mdl-color--grey-50"> <%--maknuti color ako sebude koristio bg...--%>
	    <main class="mdl-layout__content">
		    <div class="mdl-card mdl-shadow--6dp">
			    <div class="mdl-card__title mdl-color--primary mdl-color-text--white">
				    <h2 class="mdl-card__title-text">MyBiz - Login</h2>
			    </div>
	  	        <div class="mdl-card__supporting-text">
                    <div style="display: none" id="divLoginAlert" class="mdl-color-text--red"></div>
			        <form action="#" id="loginForm" role="form" runat="server">
				        <div class="mdl-textfield mdl-js-textfield">
					        <input class="mdl-textfield__input" type="text" id="txtUsername" name ="username"/>
					        <label class="mdl-textfield__label" for="txtUsername">Username</label>
				        </div>
				        <div class="mdl-textfield mdl-js-textfield">
					        <input class="mdl-textfield__input" type="password" id="txtPassword" name="password"/>
					        <label class="mdl-textfield__label" for="txtPassword">Password</label>
				        </div>
                    
                        <div class="checkbox" style="margin: 6px 0px 6px 0px">
                            <label class="mdl-checkbox mdl-js-checkbox mdl-js-ripple-effect" for="chkRemember">
                                <input type="checkbox" id="chkRemember" class="mdl-checkbox__input" name="remember">
                                <span class="mdl-checkbox__label">Remember me</span>
                            </label>
                        </div>
			        </form>
		        </div>
			    <div class="mdl-card__actions mdl-card--border">
				    <button class="mdl-button mdl-button--colored mdl-js-button mdl-js-ripple-effect" id="btnLogin">Log in</button>
			    </div>
		    </div>
	    </main>
    </div>

    <script src="Content/jquery-3.1.1.min.js"></script>
    <script src="Content/mdl-v1.1.2/material.min.js"></script>
    <script>
        //var lang = 'hr';

        $(document).ready(function () {
            //$("#txtUsername").focus();
            //getTranslationLogin(lang);
        });

        //var opts = { language: lang, pathPrefix: "../lang" };
        //$("[data-localize]").localize("ticketing", opts);

        $("#txtUsername").keypress(function (e) {
            if (e.keyCode == 13) {
                $("#txtPassword").focus();
            }
        });
        $("#txtPassword").keypress(function (e) {
            if (e.keyCode == 13) {
                $("#btnLogin").click();
            }
        });

        $("#btnLogin").click(function () {
            $.ajax({
                type: "POST",
                url: "Login.aspx/LoginUser",
                data: "{'username':'" + $("#txtUsername").val() + "', 'password':'" + $("#txtPassword").val() + "', 'rememberMe':" + $("#chkRemember").prop("checked") + "}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (u) {
                    if (u.d == "error") {
                        showalert("Username or password are incorrect!");
                    }
                    else {
                        window.location = u.d;
                    }
                },
                error: function (response) {
                    showalert("Server error!");
                    $("#txtPassword").text("");
                }
            });
        });


        function showalert(message) {
            $("#divLoginAlert").text(message);
            $("#divLoginAlert").show();
        }
    </script>
</body>
</html>
