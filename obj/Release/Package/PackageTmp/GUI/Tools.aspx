<%@ Page Title="" Language="C#" MasterPageFile="Master.Master" AutoEventWireup="true" CodeBehind="Tools.aspx.cs" Inherits="EbalitWebForms.GUI.WebForm4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <div id="TitleContainer">
        <div id="Title" class="pageHeader">
        >>  Tools
    </div></div>
    <div id="Container">
        


        <div id="LeftColumn">
            
        </div>
        <div id="MainColumn">
            <asp:LinkButton ID="lnkTaskCategories" OnCommand="lnkTaskCategories_Command" CausesValidation="false" runat="server">Manage Task Categories</asp:LinkButton>  
            <br />
            <asp:LinkButton ID="lnkCreateTask" CausesValidation="false" runat="server" OnCommand="lnkCreateTask_Command">Create Task</asp:LinkButton>
            <br />
            <asp:LinkButton ID="lnkTaskList" CausesValidation="false" runat="server" OnCommand="lnkTaskList_Command">Task List</asp:LinkButton>
        </div>
        <div id="RightColumn">
       
        </div>
    </div>
</asp:Content>
