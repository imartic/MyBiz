<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewEditProposal.aspx.cs" Inherits="MyBiz.NewEditProposal" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="description" content="MyBiz makes writing business proposals and organizing tasks simple and straightforward." />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0" />
    <title>MyBiz | Proposal</title>

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
                <span class="mdl-layout-title" id="proposalTitle">Proposal</span>
                <div class="mdl-layout-spacer"></div>
                <!-- Navigation. We hide it in small screens. -->
                <nav class="mdl-navigation mdl-layout--large-screen-only">
                    <%--<a class="mdl-navigation__link" href="Proposals.aspx" id="proposalsMenuIcon"><i class="material-icons">add</i></a>
                    <div class="mdl-tooltip" for="proposalsMenuIcon">New Proposal</div>--%>
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
            </nav>
        </div>

        <main class="mdl-layout__content mdl-color--grey-200">
            <div class="page-content mdl-grid" id="page-content">
                <div class="mdl-cell--10-col content contentProposal">                 

                    <div class="mdl-layout__header-row">
                        <button class="mdl-button mdl-js-button mdl-button--icon"
                                id="proposal_arrowBack"
                                style="margin-right:13px;"
                                onclick="JavaScript:window.history.back(1);return false;">
                            
                            <i class="material-icons mdl-color-text--primary-dark">arrow_back</i>
                            
                        </button>
                        <div class="mdl-tooltip mdl-tooltip--left" for="proposal_arrowBack">Go back</div>  

                        <!-- Title -->
                        <div class="mdl-layout-title">
                            <div class="mdl-textfield mdl-js-textfield mdl-textfield proposalInput">
                                <input class="mdl-textfield__input" type="text" id="proposalName"/>
                                <label class="mdl-textfield__label" for="proposalName">Proposal name</label>
                            </div>
                            <%--<div class="mdl-textfield mdl-js-textfield mdl-textfield proposalInput">
                                <input class="mdl-textfield__input" type="text" id="date"/>
                                <label class="mdl-textfield__label" for="date">Date</label>
                            </div>--%>
                        </div>                      

                        <%--<div class="mdl-layout-spacer"></div>

                        <button class="mdl-button mdl-js-button mdl-button--icon"
                                id="proposal_arrowBack"
                                style="margin-right:13px;"
                                onclick="window.location.href='Proposals.aspx'; return false;">
                            <i class="material-icons mdl-color-text--primary-dark">arrow_back</i>
                        </button>
                        <div class="mdl-tooltip mdl-tooltip--left" for="proposal_arrowBack">Go back</div>--%>  
                    </div>

                    <hr />

                    <form class="proposalForm">

                        <%-- Company data section --%>
                        <div class="section company-section"><p class="sectionTitle">Company data</p></div>

                        <div class="company-data section-data mdl-card mdl-shadow--2dp full-width section-card">
                            <div>
                                <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label proposalInput">
                                    <input class="mdl-textfield__input" type="text" id="company"/>
                                    <label class="mdl-textfield__label mdl-color-text--primary-dark" for="company">Company name</label>
                                </div>
                                <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label proposalInput">
                                    <input class="mdl-textfield__input" type="text" id="companyAddress"/>
                                    <label class="mdl-textfield__label mdl-color-text--primary-dark" for="companyAddress">Address</label>
                                </div>
                                 <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label proposalInput">
                                    <input class="mdl-textfield__input" type="text" id="companyCity"/>
                                    <label class="mdl-textfield__label mdl-color-text--primary-dark" for="companyCity">City</label>
                                </div>
                                <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label proposalInput">
                                    <input class="mdl-textfield__input" type="text" id="companyPhone"/>
                                    <label class="mdl-textfield__label mdl-color-text--primary-dark" for="companyPhone">Phone number</label>
                                </div>
                                <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label proposalInput">
                                    <input class="mdl-textfield__input" type="text" id="companyFax"/>
                                    <label class="mdl-textfield__label mdl-color-text--primary-dark" for="companyFax">Fax number</label>
                                </div>
                                <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label proposalInput">
                                    <input class="mdl-textfield__input" type="text" id="companyEmail"/>
                                    <label class="mdl-textfield__label mdl-color-text--primary-dark" for="companyEmail">E-mail address</label>
                                </div>
                                <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label proposalInput">
                                    <input class="mdl-textfield__input" type="text" id="companyPIN"/>
                                    <label class="mdl-textfield__label mdl-color-text--primary-dark" for="companyPIN">Personal Identification Number</label>
                                </div>
                                <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label proposalInput">
                                    <input class="mdl-textfield__input" type="text" id="companyIBAN"/>
                                    <label class="mdl-textfield__label mdl-color-text--primary-dark" for="companyIBAN">IBAN</label>
                                </div>
                            </div>
                        </div>
                        

                        <%-- Client data section --%>
                        <div class="section client-section"><p class="sectionTitle">Client data</p></div>

                        <div class="client-data section-data mdl-card mdl-shadow--2dp full-width section-card">
                            <div>
                            <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label proposalInput">
                                <input class="mdl-textfield__input" type="text" id="client"/>
                                <label class="mdl-textfield__label mdl-color-text--primary-dark" for="client">Client name</label>
                            </div>
                            <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label proposalInput">
                                <input class="mdl-textfield__input" type="text" id="clientAddress"/>
                                <label class="mdl-textfield__label mdl-color-text--primary-dark" for="clientAddress">Address</label>
                            </div>
                            <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label proposalInput">
                                <input class="mdl-textfield__input" type="text" id="clientCity"/>
                                <label class="mdl-textfield__label mdl-color-text--primary-dark" for="clientCity">City</label>
                            </div>
                            <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label proposalInput">
                                <input class="mdl-textfield__input" type="text" id="clientPhone"/>
                                <label class="mdl-textfield__label mdl-color-text--primary-dark" for="clientPhone">Phone number</label>
                            </div>
                            <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label proposalInput">
                                <input class="mdl-textfield__input" type="text" id="clientEmail"/>
                                <label class="mdl-textfield__label mdl-color-text--primary-dark" for="clientEmail">E-mail address</label>
                            </div>
                            <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label proposalInput">
                                <input class="mdl-textfield__input" type="text" id="clientPIN"/>
                                <label class="mdl-textfield__label mdl-color-text--primary-dark" for="clientPersonalNumber">Personal Identification Number</label>
                            </div>
                                </div>
                        </div>


                        <%-- Items section --%>
                        <div class="section items-section"><p class="sectionTitle">Items</p></div>  
                                            
                        <div class="items-data section-data">
                            <div class="items-title mdl-typography--text-center">
                                <div class="mdl-textfield mdl-js-textfield proposalInput">
                                    <input class="mdl-textfield__input" type="text" id="itemsTitle"/>
                                    <label class="mdl-textfield__label mdl-color-text--primary-dark" for="itemsTitle">Title</label>
                                </div>
                            </div>

                            <%--<hr />--%>
                            
                            <div class="items">
                                <%--ne koristim mdl-card jer nije bas responsive za mobitele--%>
                                <div id="item0" class="item mdl-card mdl-card mdl-shadow--2dp full-width item-card"> 
                                    <div>
                                        <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label proposalInput mdl-cell mdl-cell--1-col" id="itemNumberDiv0" style="min-width:30px">
                                            <input class="mdl-textfield__input" type="text" id="itemNumber0" value="1"/>
                                            <label class="mdl-textfield__label mdl-color-text--primary-dark" for="itemNumber0">#</label>
                                        </div> 
                                         <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label proposalInput mdl-cell mdl-cell--5-col" id="itemTextDiv0" style="min-width:200px">
                                            <input class="mdl-textfield__input" type="text" <%--rows="2"--%> id="itemText0" ></input>
                                            <label class="mdl-textfield__label mdl-color-text--primary-dark" for="itemText0">Item text</label>
                                        </div> 
                                        <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label proposalInput mdl-cell mdl-cell--1-col" id="itemUnitDiv0" style="min-width:70px">
                                            <input class="mdl-textfield__input" type="text" id="itemUnit0"/>
                                            <label class="mdl-textfield__label mdl-color-text--primary-dark" for="itemUnit0">Unit</label>
                                        </div> 
                                        <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label proposalInput mdl-cell mdl-cell--1-col" id="itemQuantityDiv0" style="min-width:70px">
                                            <input type="number" step="any" min="0" class="mdl-textfield__input" type="text" id="itemQuantity0" onchange="onQuantityChange(this)"/>
                                            <label class="mdl-textfield__label mdl-color-text--primary-dark" for="itemQuantity0">Quantity</label>
                                        </div> 
                                        <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label proposalInput mdl-cell mdl-cell--1-col" id="itemPriceDiv0" style="min-width:70px">
                                            <input type="number" step="any" min="0" class="mdl-textfield__input" type="text" id="itemPrice0" onchange="onPriceChange(this)"/>
                                            <label class="mdl-textfield__label mdl-color-text--primary-dark" for="itemPrice0">Unit price</label>
                                        </div>
                                        <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label proposalInput mdl-cell mdl-cell--1-col" id="itemTotalPriceDiv0" style="min-width:75px">
                                            <input type="number" step="any" min="0" class="mdl-textfield__input mdl-color-text--primary itemTotalPrice" type="text" id="itemTotalPrice0" disabled onchange="onTotalPriceChange(this)"/>
                                            <label class="mdl-textfield__label mdl-color-text--primary-dark" for="itemTotalPrice0">Total price</label>
                                        </div>    
                                        <button type="button" class="mdl-button mdl-js-button mdl-button--icon mdl-color-text--red" id="btnDeleteItem0" onclick="deleteItem(this)">
                                            <i class="material-icons">delete</i>
                                        </button>   
                                        <div class="mdl-tooltip mdl-tooltip--right" for="btnDeleteItem0">Delete item</div>  
                                        <%--<hr />--%>                      
                                    </div>   
                                </div>                                       
                            </div>

                            <button class="mdl-button mdl-js-button mdl-button--fab mdl-button--accent mdl-button--mini-fab mdl-color-text--primary-dark" id="btnAddItem" style="margin:10px" onclick="return false;">
                                <i class="material-icons">add</i>
                            </button>   
                            <div class="mdl-tooltip" for="btnAddItem">Add new item</div>  

                            <div class="total section-data">
                                <div class="totalLabels">
                                    <div>
                                        <label for="amount"><b>Amount:&nbsp;</b><span id="amount">0.00</span></label>
                                    </div>
                                    <div>
                                        <label class="mdl-checkbox mdl-js-checkbox mdl-js-ripple-effect" for="cbTax">
                                            <input type="checkbox" id="cbTax" class="mdl-checkbox__input" checked>
                                            <span class="mdl-checkbox__label"><b>Tax:&nbsp;</b><span id="tax">0.00</span></span>
                                        </label>
                                    </div>
                                    <div>
                                        <label for="total"><b>Total:&nbsp;</b><span id="total">0.00</span></label>
                                    </div>
                                </div>
                            </div>

                        </div>


                        <div class="section summary-section"><p class="sectionTitle">Note & Signature</p></div>
                        <div class="section-data mdl-card mdl-card mdl-shadow--2dp full-width section-card">
                            <div>
                                <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label proposalInput" style="width:45%">
                                    <textarea class="mdl-textfield__input" type="text" rows= "3" id="note" ></textarea>
                                    <label class="mdl-textfield__label mdl-color-text--primary-dark" for="note">Note...</label>
                                </div>
                                <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label proposalInput" style="float:right;width:45%">
                                    <textarea class="mdl-textfield__input" type="text" rows= "3" id="signature" ></textarea>
                                    <label class="mdl-textfield__label mdl-color-text--primary-dark" for="signature">Signature</label>
                                </div>
                            </div>
                        </div>

                    </form>


                    <button class="mdl-button mdl-js-button mdl-button--raised mdl-js-ripple-effect mdl-button--accent proposalBtns" id="btnSave" 
                            onclick="return false;">
                        Save proposal
                    </button>
                    <button class="mdl-button mdl-js-button mdl-button--raised mdl-js-ripple-effect mdl-button--accent proposalBtns" id="btnExport" 
                            onclick="return false;" disabled>
                        Export proposal
                    </button>

                </div>
            </div>
        </main>
        </div>

        <script src="Content/jquery-3.1.1.min.js"></script>
        <script src="Content/mdl-v1.1.2/material.min.js"></script>
        <script src="Content/JS/newEditProposal.js"></script>
        <script src="Content/JS/common.js"></script>
</body>
</html>
