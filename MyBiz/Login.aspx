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
    
    <div class="mdl-layout mdl-js-layout <%--mdl-color--blue-grey-200--%>"> <%--maknuti color ako se bude koristio bg...--%>
	    <main class="mdl-layout__content">
		    <div class="mdl-card mdl-shadow--8dp">
			    <div class="mdl-card__title mdl-color--primary mdl-color-text--white">
				    <h2 class="mdl-card__title-text">MyBiz</h2>
			    </div>

                <div class="mdl-tabs mdl-js-tabs mdl-js-ripple-effect">
                    <div class="mdl-tabs__tab-bar">
                        <a href="#login-panel" class="mdl-tabs__tab is-active" style="width:50%">Login</a>
                        <a href="#register-panel" class="mdl-tabs__tab" style="width:50%">Registration</a>
                    </div>

                    <div class="mdl-tabs__panel is-active" id="login-panel">
                            
                        <div class="mdl-card__supporting-text">                   
                            <div style="display: none" id="divLoginAlert" class="mdl-color-text--red"></div>
			                <form action="#" id="loginForm" role="form" runat="server">                     
                                <div style="float:left; width:10%">
                                    <i class="material-icons mdl-textfield__label__icon mdl-color-text--primary-dark">person_outline</i>
                                </div>
				                <div class="mdl-textfield mdl-js-textfield" style="float:left; width:86%; margin-left:3px;">                            
					                <input class="mdl-textfield__input" type="text" id="txtUsername" name ="username"/>
					                <label class="mdl-textfield__label" for="txtUsername">Username</label>                            
				                </div>    

                                <div style="float:left; width:10%">
                                    <i class="material-icons mdl-textfield__label__icon mdl-color-text--primary-dark">lock_outline</i>
                                </div>
				                <div class="mdl-textfield mdl-js-textfield" style="float:left; width:86%; margin-left:3px;">
					                <input class="mdl-textfield__input" type="password" id="txtPassword" name="password"/>
					                <label class="mdl-textfield__label" for="txtPassword">Password</label>
				                </div>

                                <label class="mdl-checkbox mdl-js-checkbox mdl-js-ripple-effect" for="chkRemember" style="margin:20px 0 16px 5px">
                                    <input type="checkbox" id="chkRemember" class="mdl-checkbox__input" name="remember">
                                    <span class="mdl-checkbox__label" style="margin-left:6px">Remember me</span>
                                </label>

			                </form>
		                </div>

			            <div class="mdl-card__actions mdl-card--border" style="padding:12px;">
				            <button class="mdl-button mdl-button--colored mdl-js-button mdl-js-ripple-effect" id="btnLogin" style="font-weight:bold">Log in</button>
			            </div>

                    </div> <%--login-panel--%>

                    <div class="mdl-tabs__panel" id="register-panel">

                        <div class="mdl-card__supporting-text">                   
                            <div style="display: none" id="divRegisterAlert" class="mdl-color-text--red"></div> 
                            
                                <div style="float:left; width:10%">
                                    <i class="material-icons mdl-textfield__label__icon mdl-color-text--primary-dark">mail_outline</i>
                                </div>
				                <div class="mdl-textfield mdl-js-textfield" style="float:left; width:86%; margin-left:3px;">                            
					                <input class="mdl-textfield__input" type="text" id="txtEmail_reg" name ="e-mail"/>
					                <label class="mdl-textfield__label" for="txtEmail_reg">E-mail</label>                            
				                </div> 
                                             
                                <div style="float:left; width:10%">
                                    <i class="material-icons mdl-textfield__label__icon mdl-color-text--primary-dark">person_outline</i>
                                </div>
				                <div class="mdl-textfield mdl-js-textfield" style="float:left; width:86%; margin-left:3px;">                            
					                <input class="mdl-textfield__input" type="text" id="txtUsername_reg" name ="username"/>
					                <label class="mdl-textfield__label" for="txtUsername_reg">Username</label>                            
				                </div>    

                                <div style="float:left; width:10%">
                                    <i class="material-icons mdl-textfield__label__icon mdl-color-text--primary-dark">lock_outline</i>
                                </div>
				                <div class="mdl-textfield mdl-js-textfield" style="float:left; width:86%; margin-left:3px;">
					                <input class="mdl-textfield__input" type="password" id="txtPassword_reg" name="password"/>
					                <label class="mdl-textfield__label" for="txtPassword_reg">Password</label>
				                </div>

                                <div style="float:left; width:10%">
                                    <i class="material-icons mdl-textfield__label__icon mdl-color-text--primary-dark">lock_outline</i>
                                </div>
				                <div class="mdl-textfield mdl-js-textfield" style="float:left; width:86%; margin-left:3px;">
					                <input class="mdl-textfield__input" type="password" id="txtPassword2_reg" name="password"/>
					                <label class="mdl-textfield__label" for="txtPassword2_reg">Confirm password</label>
				                </div>
		                </div>

			            <div class="mdl-card__actions mdl-card--border" style="padding:12px; margin-top:7px;">
				            <button class="mdl-button mdl-button--colored mdl-js-button mdl-js-ripple-effect" id="btnRegister" style="font-weight:bold">Register</button>
			            </div>

                    </div> <%--register-panel--%>

	  	        </div>
		    </div>
	    </main>

        <footer>
            <small style="margin-top:100px; color:#555">Copyright &copy; <script>document.write(new Date().getFullYear())</script>. Ivan Martić<!--. All rights reserved.--></small>
        </footer>
    </div>

    <script src="Content/jquery-3.1.1.min.js"></script>
    <script src="Content/mdl-v1.1.2/material.min.js"></script>
    <script>
        //var lang = 'hr';

        $(document).ready(function () {
            //$("#txtUsername").focus();
            //getTranslationLogin(lang);
            $('#txtUsername').focus();
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
