﻿<%@ Page Title="" Language="C#" MasterPageFile="Master.Master" AutoEventWireup="true" CodeBehind="Varia.aspx.cs" Inherits="EbalitWebForms.GUI.WebForm5" %>

<%@ Register Src="~/GUI/WebUserControls/CategoryBrowser.ascx" TagPrefix="uc1" TagName="CategoryBrowser" %>
<%@ Register Src="~/GUI/WebUserControls/SearchUserControl.ascx" TagPrefix="uc1" TagName="SearchUserControl" %>
<%@ Register Src="~/GUI/WebUserControls/BlogContentUserControl.ascx" TagPrefix="uc1" TagName="BlogContentUserControl" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">

    <div id="TitleContainer">
        <div id="Title" class="pageHeader">
        >>  Varia
    </div></div>
    <div id="Container">
        <div id="LeftColumn">
            <uc1:CategoryBrowser runat="server" ID="CategoryBrowser" BlogTopic="Varia" OnLinkButtonPressed="linkButton_Command" />
        </div>
        <div id="MainColumn">
            <uc1:BlogContentUserControl runat="server" ID="BlogContentUserControl" BlogTopic="Varia" IsCommentsVisible="true" IsPublishedDateVisible="true" />
        </div>
        <div id="RightColumn">
            <uc1:SearchUserControl runat="server" ID="SearchUserControl" OnSearchButtonClick="btnSearch_Click" />
        </div>
    </div>
</asp:Content>
