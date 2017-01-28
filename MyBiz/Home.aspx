<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="MyBiz.Home" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="description" content="MyBiz makes writing business proposals and organizing tasks simple and straightforward." />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0" />
    <title>MyBiz | Home</title>

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

    <div class="mdl-layout mdl-js-layout mdl-layout--fixed-header">
        <header class="mdl-layout__header">
            <div class="mdl-layout__header-row">
                <!-- Title -->
                <span class="mdl-layout-title">MyBiz</span>
                <div class="mdl-layout-spacer"></div>
                <!-- Navigation. We hide it in small screens. -->
                <nav class="mdl-navigation mdl-layout--large-screen-only">
                    <a class="mdl-navigation__link" href="Proposals.aspx" id="proposalsMenuIcon"><i class="material-icons">subject</i></a>
                    <div class="mdl-tooltip" for="proposalsMenuIcon">Proposals</div>
                    <a class="mdl-navigation__link" href="Schedule.aspx" id="scheduleMenuIcon"><i class="material-icons">event</i></a>
                    <div class="mdl-tooltip" for="scheduleMenuIcon">Schedule</div>
                    <a class="mdl-navigation__link" href="Settings.aspx" id="settingsMenuIcon"><i class="material-icons">settings</i></a>
                    <div class="mdl-tooltip" for="settingsMenuIcon">Settings</div>
                </nav>
                <!-- Right aligned menu below button -->
                <button id="demo-menu-lower-right" class="mdl-button mdl-js-button mdl-button--icon" onclick="return false;">
                    <i class="material-icons">more_vert</i>
                </button>

                <ul class="mdl-menu mdl-menu--bottom-right mdl-js-menu mdl-js-ripple-effect" for="demo-menu-lower-right">
                    <li class="mdl-menu__item"><i class="material-icons" id="moreMenuIcon">help_outline</i>Help</li>
                    <hr/>
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
                <a class="mdl-navigation__link" href="Settings.aspx"><i class="material-icons" id="drawerIcon">settings</i>Settings</a>
            </nav>
        </div>


        <main class="mdl-layout__content mdl-color--grey-50">
            <div class="page-content mdl-grid" id="page-content">
                <div class="mdl-cell--10-col content contentHome">
                    <h2>Welcome to MyBiz!</h2>
                    <p class ="description">Your online business assistant!</p>
                    <p class ="description">MyBiz makes writing business proposals and organizing tasks and events simple and straightforward.</p>
                    
                    <hr />
                    
                    <div class="mdl-cell mdl-cell--6-col mdl-card mdl-shadow--2dp box homeProposals">
                            
                        <div class="mdl-card__title mdl-color--primary mdl-color-text--white">
                            <h4 class="mdl-card__title-text">Proposals</h4>
                            
                            <button class="mdl-button mdl-js-button mdl-button--icon" 
                                    id="newProposal_box" 
                                    style="margin-left:auto;"
                                    onclick="window.location.href='NewEditProposal.aspx'; return false;">
                                <i class="material-icons">add</i>
                            </button>   
                            <div class="mdl-tooltip mdl-tooltip--left" for="newProposal_box">New Proposal</div>                         
                        </div>

                            <table class="mdl-data-table mdl-js-data-table mdl-shadow--2dp homeProposalsTbl">
                                <!--<tbody class="homeProposalsTbody">
                                    <tr>
                                        <td class="mdl-data-table__cell--non-numeric">Vukovarska 50</td>
                                        <td>25.11.2016</td>
                                    </tr>
                                    <tr>
                                        <td class="mdl-data-table__cell--non-numeric">g. Ivo Ivić</td>
                                        <td>4.10.2016</td>
                                    </tr>
                                    <tr>
                                        <td class="mdl-data-table__cell--non-numeric">Tvrtka d.o.o.</td>
                                        <td>10.8.2016</td>
                                    </tr>
                                </tbody>-->
                            </table>

                            <!-- Accent-colored raised button with ripple -->
                            <button class="mdl-button mdl-js-button mdl-button--raised mdl-js-ripple-effect mdl-button--accent"
                                    onclick="window.location.href='Proposals.aspx'; return false;"
                                    style="margin: 0 auto 27px auto">
                                View all Proposals
                            </button>

                        </div>   
                                     
                    <div class="mdl-cell mdl-cell--6-col mdl-card mdl-shadow--2dp box homeSchedule">
                        <div class="mdl-card__title mdl-color--primary mdl-color-text--white">
                            <h4 class="mdl-card__title-text">Schedule</h4>
                            
                            <button class="mdl-button mdl-js-button mdl-button--icon"
                                    id="newEvent_box"
                                    style="margin-left:auto;"
                                    onclick="window.location.href='Schedule.aspx'; return false;">
                                <i class="material-icons">add</i>
                            </button>
                            <div class="mdl-tooltip mdl-tooltip--left" for="newEvent_box">New Event</div> 
                        </div>

                        <table class="mdl-data-table mdl-js-data-table mdl-data-table--selectable mdl-shadow--2dp homeScheduleTbl">
                            <tbody>
                                <tr>
                                    <td class="mdl-data-table__cell--non-numeric">Predviđeni kraj radova u Ul. Franje Čandeka 22</td>
                                    <td>20.12.2016</td>
                                </tr>
                                <tr>
                                    <td class="mdl-data-table__cell--non-numeric">Sastanak - g. Ivo Ivić</td>
                                    <td>9.1.2017</td>
                                </tr>
                                <tr>
                                    <td class="mdl-data-table__cell--non-numeric">Početak radova - Vukovarska 50</td>
                                    <td>18.3.2017</td>
                                </tr>
                            </tbody>
                        </table>

                        <!-- Accent-colored raised button with ripple -->
                        <button class="mdl-button mdl-js-button mdl-button--raised mdl-js-ripple-effect mdl-button--accent" 
                                onclick="window.location.href='Schedule.aspx'; return false;"
                                style="margin: 0 auto 27px auto">
                            View Schedule
                        </button>

                    </div>
                </div>
            </div>
        </main>
    </div>
    </form>

    <script src="Content/jquery-3.1.1.min.js"></script>
    <script src="Content/mdl-v1.1.2/material.min.js"></script>
    <script src="Content/JS/home.js"></script>
    <script src="Content/JS/common.js"></script>
</body>
</html>
