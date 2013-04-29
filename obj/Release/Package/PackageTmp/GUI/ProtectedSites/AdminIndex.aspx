<%@ Page Title="" Language="C#" MasterPageFile="../ProtectedSites/AdminMaster.master" AutoEventWireup="true" CodeBehind="AdminIndex.aspx.cs" Inherits="EbalitWebForms.GUI.ProtectedSites.WebForm2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
    <div id="Container">    
        <div id="LeftColumn"></div>
                <div id="MainColumn">
                    <asp:LinkButton ID="lnkLogout" runat="server" CausesValidation="false" OnCommand="lnkLogout_Command">Logout</asp:LinkButton>
        </div>
        <div id="RightColumn"></div>
        </div>

</asp:Content>
