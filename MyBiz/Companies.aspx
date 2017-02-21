<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Companies.aspx.cs" Inherits="MyBiz.Companies" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="description" content="MyBiz makes writing business proposals and organizing tasks simple and straightforward." />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0" />
    <title>MyBiz | Your Companies</title>

    <meta name="mobile-web-app-capable" content="yes" />

    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black" />
    <meta name="apple-mobile-web-app-title" content="MyBiz" />

    <meta name="msapplication-TileImage" content="images/touch/ms-touch-icon-144x144-precomposed.png" />
    <meta name="msapplication-TileColor" content="#3372DF" />

    <link rel="stylesheet" href="Content/mdl-v1.1.2/material.min.css" />
    <link rel="stylesheet" href="https://fonts.googleapis.com/icon?family=Material+Icons" />
    <link rel="stylesheet" href="Content/dialog-polyfill.css" />
    <link rel="stylesheet" href="Content/style.css" />
</head>
<body>
    <form id="form1" runat="server">
    </form>

    <div class="mdl-layout mdl-js-layout mdl-layout--fixed-header">
        <header class="mdl-layout__header">
            <div class="mdl-layout__header-row">
                <!-- Title -->
                <span class="mdl-layout-title">Your Companies</span>
                <div class="mdl-layout-spacer"></div>
                <!-- Navigation. We hide it in small screens. -->
                <nav class="mdl-navigation mdl-layout--large-screen-only">
                    <a class="mdl-navigation__link show-modal" href="#" id="companiesMenuIcon"><i class="material-icons">add</i></a>
                    <div class="mdl-tooltip" for="companiesMenuIcon">New Company</div>
                    <a class="mdl-navigation__link" href="Home.aspx" id="homeMenuIcon"><i class="material-icons">home</i></a>
                    <div class="mdl-tooltip" for="homeMenuIcon">Home</div>
                </nav>
                <!-- Right aligned menu below button -->
                <button id="demo-menu-lower-right" class="mdl-button mdl-js-button mdl-button--icon" onclick="return false;">
                    <i class="material-icons">more_vert</i>
                </button>

                <ul class="mdl-menu mdl-menu--bottom-right mdl-js-menu mdl-js-ripple-effect" for="demo-menu-lower-right">
                    <li class="mdl-menu__item"><i class="material-icons" id="moreMenuIcon">help_outline</i>Help</li>
                    <hr />
                    <li class="mdl-menu__item" id="btnLogout"><i class="material-icons" id="moreMenuIcon">power_settings_new</i>Logout</li>
                </ul>
            </div>
        </header>

        <div class="mdl-layout__drawer">
            <span class="mdl-layout-title">MyBiz</span>
            <nav class="mdl-navigation">
                <a class="mdl-navigation__link" href="Home.aspx"><i class="material-icons" id="drawerIcon">home</i>Home</a>
                <a class="mdl-navigation__link" href="Proposals.aspx"><i class="material-icons" id="drawerIcon">subject</i>Proposals</a>
                <a class="mdl-navigation__link" href="Schedule.aspx"><i class="material-icons" id="drawerIcon">event</i>Schedule</a>
                <a class="mdl-navigation__link" href="Companies.aspx"><i class="material-icons" id="drawerIcon">business</i>Your Companies</a>
                <a class="mdl-navigation__link" href="Settings.aspx"><i class="material-icons" id="drawerIcon">settings</i>Settings</a>

                <footer style="position:fixed; bottom:25px; left:40px;">
                    <small style="margin-top:100px; color:#555">Copyright &copy; <script>document.write(new Date().getFullYear())</script>. Ivan Martić<!--. All rights reserved.--></small>
                </footer>
            </nav>
        </div>

        <%--MODAL--%>
        <dialog class="mdl-dialog">
            <div class="mdl-dialog__title" style="font-size:20px; text-align:center; margin-bottom:7px">Company data</div>
            <div class="mdl-dialog__content">
                <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                    <input class="mdl-textfield__input dlg-input" type="text" id="name"/>
                    <label class="mdl-textfield__label mdl-color-text--primary-dark" for="name">Company name</label>
                </div>
                <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                    <input class="mdl-textfield__input dlg-input" type="text" id="address"/>
                    <label class="mdl-textfield__label mdl-color-text--primary-dark" for="address">Address</label>
                </div>
                    <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                    <input class="mdl-textfield__input dlg-input" type="text" id="city"/>
                    <label class="mdl-textfield__label mdl-color-text--primary-dark" for="city">City</label>
                </div>
                <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                    <input class="mdl-textfield__input dlg-input" type="text" id="phone"/>
                    <label class="mdl-textfield__label mdl-color-text--primary-dark" for="phone">Phone number</label>
                </div>
                <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                    <input class="mdl-textfield__input dlg-input" type="text" id="fax"/>
                    <label class="mdl-textfield__label mdl-color-text--primary-dark" for="fax">Fax number</label>
                </div>
                <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                    <input class="mdl-textfield__input dlg-input" type="text" id="email"/>
                    <label class="mdl-textfield__label mdl-color-text--primary-dark" for="email">E-mail address</label>
                </div>
                <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                    <input class="mdl-textfield__input dlg-input" type="text" id="pin"/>
                    <label class="mdl-textfield__label mdl-color-text--primary-dark" for="pin">Personal Identification Number</label>
                </div>
                <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                    <input class="mdl-textfield__input dlg-input" type="text" id="iban"/>
                    <label class="mdl-textfield__label mdl-color-text--primary-dark" for="iban">IBAN</label>
                </div>
            </div>
            <div class="mdl-dialog__actions">
                <button type="button" class="mdl-button close"><i class="material-icons" style="margin-bottom:3px; color:red">close</i>&nbsp;Cancel</button>
                <button type="button" class="mdl-button save"><i class="material-icons" style="margin-bottom:3px; color:green">check</i>&nbsp;<b>Save</b></button>
            </div>
        </dialog>
        <%--MODAL--%>

        <main class="mdl-layout__content mdl-color--grey-200">
            <div class="page-content mdl-grid" id="page-content" style="margin-top:20px;">
                <div class="mdl-cell--10-col content contentCompanies">

                    <%--<div class="mdl-layout__header-row">
                        <!-- Title -->
                        <span class="mdl-layout-title">Your Companies</span>
                        <div class="mdl-layout-spacer"></div>

                        <button class="mdl-button mdl-js-button mdl-button--icon"
                                id="newCompany"
                                style="margin-right:13px;"
                                onclick="">
                            <i class="material-icons mdl-color-text--primary-dark">add</i>
                        </button>
                        <div class="mdl-tooltip mdl-tooltip--left" for="newCompany">New Company</div>  

                        <div class="mdl-textfield mdl-js-textfield mdl-textfield--expandable
                                    mdl-textfield--floating-label mdl-textfield--align-right">
                            <label class="mdl-button mdl-js-button mdl-button--icon" for="searchCompanies">
                                <i class="material-icons mdl-color-text--primary-dark" id="searchCompanies_icon">search</i>
                            </label>
                            <div class="mdl-tooltip" for="searchCompanies_icon">Search Companies</div> 
                            <div class="mdl-textfield__expandable-holder">
                                <input class="mdl-textfield__input" type="text" name="searchCompanies" id="searchCompanies"/>
                            </div>
                        </div>
                    </div>

                     <hr />--%>

                    <div>
                        <div class="mdl-card mdl-cell mdl-cell--8-col mdl-cell--4-col-tablet mdl-shadow--2dp companies-card">
                            <div class="mdl-card__title mdl-color--primary mdl-color-text--white" style="padding:10px 10px 10px 15px;">
                                <h4 class="mdl-card__title-text" style="margin-bottom:5px;">Your Companies</h4>
                                <button class="mdl-button mdl-js-button mdl-button--icon show-modal"
                                    id="newCompany"
                                    style="margin-left:auto;"
                                    onclick="">
                                    <i class="material-icons">add</i>
                                </button>
                                <div class="mdl-tooltip mdl-tooltip--left" for="newCompany">New Company</div>
                            </div>
                                
                            <table class="mdl-data-table mdl-js-data-table full-width" id="companiesTbl">
                            <%--<thead>
                                    <tr>
                                        <th class="mdl-data-table__cell--non-numeric" width="50%">Company name</th>
                                        <th></th>
                                    </tr>
                                </thead>--%>
                                <tbody>
                           
                                </tbody>
                            </table>
                        </div>
                        <div class="mdl-card mdl-cell mdl-cell--4-col mdl-cell--4-col-tablet mdl-shadow--2dp companies-card">                           
                            <div class="mdl-card__title mdl-color--primary mdl-color-text--white" style="padding:15px;">
                                <h4 class="mdl-card__title-text">Company data</h4>
                            </div>  
                            
                            <div class="mdl-card__supporting-text"> 
                                <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label full-width">
                                    <input class="mdl-textfield__input co-input" type="text" id="co-name"/>
                                    <label class="mdl-textfield__label mdl-color-text--primary" for="co-name">Company name</label>
                                </div>
                                <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label full-width">
                                    <input class="mdl-textfield__input co-input" type="text" id="co-address"/>
                                    <label class="mdl-textfield__label mdl-color-text--primary" for="co-address">Address</label>
                                </div>
                                    <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label full-width">
                                    <input class="mdl-textfield__input co-input" type="text" id="co-city"/>
                                    <label class="mdl-textfield__label mdl-color-text--primary" for="co-city">City</label>
                                </div>
                                <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label full-width">
                                    <input class="mdl-textfield__input co-input" type="text" id="co-phone"/>
                                    <label class="mdl-textfield__label mdl-color-text--primary" for="co-phone">Phone number</label>
                                </div>
                                <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label full-width">
                                    <input class="mdl-textfield__input co-input" type="text" id="co-fax"/>
                                    <label class="mdl-textfield__label mdl-color-text--primary" for="co-fax">Fax number</label>
                                </div>
                                <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label full-width">
                                    <input class="mdl-textfield__input co-input" type="text" id="co-email"/>
                                    <label class="mdl-textfield__label mdl-color-text--primary" for="co-email">E-mail address</label>
                                </div>
                                <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label full-width">
                                    <input class="mdl-textfield__input co-input" type="text" id="co-pin"/>
                                    <label class="mdl-textfield__label mdl-color-text--primary" for="co-pin">Personal Identification Number</label>
                                </div>
                                <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label full-width">
                                    <input class="mdl-textfield__input co-input" type="text" id="co-iban"/>
                                    <label class="mdl-textfield__label mdl-color-text--primary" for="co-iban">IBAN</label>
                                </div>
                            </div>  
                                                 
                        </div> 
                    </div>


                </div>
            </div>
        </main>
    </div>


    <script src="Content/jquery-3.1.1.min.js"></script>
    <script src="Content/mdl-v1.1.2/material.min.js"></script>
    <script src="Content/dialog-polyfill.js"></script>
    <script src="Content/JS/common.js"></script>
    <script src="Content/JS/companies.js"></script>
</body>
</html>
