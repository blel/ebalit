<%@ Page Title="" Language="C#" MasterPageFile="~/GUI/ProtectedSites/ToolsMaster.master" AutoEventWireup="true" CodeBehind="UploadWorkingReports.aspx.cs" Inherits="EbalitWebForms.GUI.WorkingReport.UploadWorkingReports" %>

<%@ Register Src="~/GUI/WebUserControls/StatusBar.ascx" TagPrefix="uc1" TagName="StatusBar" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ToolsContent" runat="server">
    <div id="Container">
        <div id="LeftColumn">
        </div>
        <div id="MainColumn">
            <asp:FileUpload ID="fulCsvFileUpload" runat="server" Width="300" />&nbsp;
                        <asp:LinkButton ID="lnkUpload" runat="server" CausesValidation="false" CssClass="CommandButton" OnCommand="lnkUpload_OnCommand">Import</asp:LinkButton>&nbsp;
        <uc1:StatusBar runat="server" ID="StatusBar" />
        </div>
        <div id="RightColumn">
        </div>
    </div>
</asp:Content>
