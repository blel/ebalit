<%@ Page Title="" Language="C#" MasterPageFile="~/GUI/ProtectedSites/ToolsMaster.master" AutoEventWireup="true" CodeBehind="ManageProjects.aspx.cs" Inherits="EbalitWebForms.GUI.Controlling.ManageProjects" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ToolsContent" runat="server">
    <div id="Container">
        <div id="LeftColumn">

        </div>
        <div id="MainColumn">
            <asp:LinkButton ID="lnkProjectStructure" Text ="Work Breakdown Structure"   CssClass="CommandButton" runat="server" OnClick="lnkProjectStructure_Click" ></asp:LinkButton>
        </div>
        <div id="RightColumn">

        </div>
    </div>
</asp:Content>
