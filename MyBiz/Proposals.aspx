﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Proposals.aspx.cs" Inherits="MyBiz.Proposals" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="description" content="MyBiz makes writing business proposals and organizing tasks simple and straightforward." />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0" />
    <title>MyBiz | Proposals</title>

    <meta name="mobile-web-app-capable" content="yes" />

    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black" />
    <meta name="apple-mobile-web-app-title" content="MyBiz" />

    <meta name="msapplication-TileImage" content="images/touch/ms-touch-icon-144x144-precomposed.png" />
    <meta name="msapplication-TileColor" content="#3372DF" />

    <link rel="stylesheet" href="Content/mdl-v1.1.2/material.min.css" />
    <link rel="stylesheet" href="https://fonts.googleapis.com/icon?family=Material+Icons" />
    <link rel="stylesheet" href="Content/style.css" />
</head>
<body>
    <form id="form1" runat="server">
    </form>

    <div class="mdl-layout mdl-js-layout mdl-layout--fixed-header">
        <header class="mdl-layout__header">
            <div class="mdl-layout__header-row">
                <!-- Title -->
                <span class="mdl-layout-title">Proposals</span>
                <div class="mdl-layout-spacer"></div>
                <!-- Navigation. We hide it in small screens. -->
                <nav class="mdl-navigation mdl-layout--large-screen-only">
                    <a class="mdl-navigation__link" href="NewEditProposal.aspx" id="proposalsMenuIcon"><i class="material-icons">add</i></a>
                    <div class="mdl-tooltip" for="proposalsMenuIcon">New Proposal</div>
                    <a class="mdl-navigation__link" href="Home.aspx" id="homeMenuIcon"><i class="material-icons">home</i></a>
                    <div class="mdl-tooltip" for="homeMenuIcon">Home</div>
                    <a class="mdl-navigation__link" href="Settings.aspx" id="settingsMenuIcon"><i class="material-icons">settings</i></a>
                    <div class="mdl-tooltip" for="settingsMenuIcon">Settings</div>
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

        <main class="mdl-layout__content mdl-color--grey-200">
            <div class="page-content mdl-grid" id="page-content">
                <div class="mdl-cell--10-col content contentProposals">                 

                    <div class="mdl-layout__header-row">
                        <!-- Title -->
                        <span class="mdl-layout-title">Your Proposals</span>
                        <div class="mdl-layout-spacer"></div>

                        <button class="mdl-button mdl-js-button mdl-button--icon"
                                id="newProposal_box"
                                style="margin-right:13px;"
                                onclick="window.location.href='NewEditProposal.aspx'; return false;">
                            <i class="material-icons mdl-color-text--primary-dark">add</i>
                        </button>
                        <div class="mdl-tooltip mdl-tooltip--left" for="newProposal_box">New Proposal</div>  

                        <div class="mdl-textfield mdl-js-textfield mdl-textfield--expandable
                                    mdl-textfield--floating-label mdl-textfield--align-right">
                            <label class="mdl-button mdl-js-button mdl-button--icon" for="searchProposals">
                                <i class="material-icons mdl-color-text--primary-dark" id="searchProposals_icon">search</i>
                            </label>
                            <div class="mdl-tooltip" for="searchProposals_icon">Search Proposals</div> 
                            <div class="mdl-textfield__expandable-holder">
                                <input class="mdl-textfield__input" type="text" name="searchProposals" id="searchProposals">
                            </div>
                        </div>
                    </div>

                    <hr />


                    <section id="proposalsTbl">

                        <table class="mdl-data-table mdl-js-data-table mdl-data-table mdl-shadow--2dp full-width dt-def" id="#proposalsTbl" style="margin-bottom:20px">
                            <thead>
                                <tr>
                                    <th class="mdl-data-table__cell--non-numeric" style="width:35%">Proposal</th>
                                    <th class="mdl-data-table__cell--non-numeric" style="width:30%">Client</th>
                                    <th class="mdl-data-table__cell--non-numeric" style="width:15%">Last change</th>
                                    <th class="mdl-data-table__cell--non-numeric" style="width:20%" id="tblBtns"></th>
                                </tr>
                            </thead>
                            <tbody>
                           
                            </tbody>
                        </table>

                    </section>

                </div>
            </div>
        </main>
        </div>

        <script src="Content/jquery-3.1.1.min.js"></script>
        <script src="Content/mdl-v1.1.2/material.min.js"></script>
        <script src="Content/JS/common.js"></script>
        <script src="Content/JS/proposals.js"></script>
</body>
</html>
