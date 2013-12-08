<%@ Page Title="" Language="C#" MasterPageFile="~/GUI/ProtectedSites/ToolsMaster.master" AutoEventWireup="true" CodeBehind="AdminWorkingReport.aspx.cs" Inherits="EbalitWebForms.GUI.WorkingReport.AdminWorkingReport" %>

<%@ Register Src="~/GUI/WebUserControls/StatusBar.ascx" TagPrefix="uc1" TagName="StatusBar" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ToolsContent" runat="server">
    <asp:ObjectDataSource ID="odsUsers" runat="server" SelectMethod="GetUsers" TypeName="EbalitWebForms.BusinessLogicLayer.User.MembershipBll"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsProjects" runat="server" SelectMethod="GetProjects" TypeName="EbalitWebForms.BusinessLogicLayer.WorkingReport.WorkingReportBll"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsAvailableResources" runat="server" SelectMethod="GetAvailableResources"
        TypeName="EbalitWebForms.BusinessLogicLayer.WorkingReport.WorkingReportBll" OnSelecting="odsAvailableResources_OnSelecting">
        <SelectParameters>
            <asp:Parameter Name="projectId" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsAssignedResources" runat="server" SelectMethod="GetAssignedResources" TypeName="EbalitWebForms.BusinessLogicLayer.WorkingReport.WorkingReportBll"
        OnSelecting="odsAssignedResources_Selecting">
        <SelectParameters>
            <asp:Parameter Name="userName" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <div id="Container">
        <div id="LeftColumn">
        </div>
        <div id="MainColumn">
            <asp:Table ID="tblAdministration" runat="server" CssClass="detailsview" Width="768px">
                <asp:TableRow runat="server">
                    <asp:TableCell runat="server">
                        <asp:Label ID="Label1" runat="server" Text="Select user"></asp:Label><br />
                        <asp:DropDownList ID="ddlUsers" runat="server" DataSourceID="odsUsers" DataTextField="UserName" OnTextChanged="ddlUsers_TextChanged" AutoPostBack="true"></asp:DropDownList>
                    </asp:TableCell>
                    <asp:TableCell runat="server">
                        <asp:Label ID="Label2" runat="server" Text="Select project"></asp:Label>
                        <br />
                        <asp:DropDownList ID="ddlProjects" runat="server" DataSourceID="odsProjects" DataTextField="Name" DataValueField="Id" AutoPostBack="true" OnTextChanged="ddlProjects_TextChanged"></asp:DropDownList>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server">
                    <asp:TableCell runat="server">
                        <asp:Label ID="Label3" runat="server" Text="Assigned Resources"></asp:Label><br />
                        <asp:ListBox ID="lsbAssignedUsers" runat="server" DataSourceID="odsAssignedResources" DataTextField="Name" DataValueField="Id"></asp:ListBox>
                    </asp:TableCell>
                    <asp:TableCell runat="server">
                        <asp:Label ID="Label4" runat="server" Text="Available Resources"></asp:Label><br />
                        <asp:ListBox ID="lsbAvailableResources" runat="server" DataSourceID="odsAvailableResources" DataTextField="Name" DataValueField="Id"></asp:ListBox>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server">
                    <asp:TableCell runat="server">
                        <asp:Button ID="btnRemove" runat="server" Text="Remove" OnClick="btnRemove_Click" />
                    </asp:TableCell>
                    <asp:TableCell runat="server">
                        <asp:Button ID="btnAssign" runat="server" Text="Assign" OnClick="btnAssign_Click" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server">
                    <asp:TableCell runat="server">
                        <asp:CheckBox runat="server" Text="Restrict Working Reports to assigned users only"></asp:CheckBox>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server">
                    <asp:TableCell runat="server">
                        <asp:CheckBox ID="chkDeleteResources" runat="server" Text="Delete Resources on MPS Server if removed in MS Project" /><br />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server">
                    <asp:TableCell runat="server">
                       <asp:CheckBox ID="chkDeleteTasks" runat="server" Text="Delete Tasks on MPS Server if removed in MS Project" /><br />
                    </asp:TableCell>
                </asp:TableRow>
                                <asp:TableRow runat="server">
                    <asp:TableCell runat="server">
                       <asp:CheckBox ID="chkGetActualWork" runat="server" Text="Get Actual Work from MS Project" /><br />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server">
                    <asp:TableCell runat="server">
                        <asp:LinkButton ID="lnkSave" CssClass="CommandButton" runat="server" OnCommand="lnkSave_OnCommand">Save Settings</asp:LinkButton></asp:TableCell>
                </asp:TableRow>
            </asp:Table>
            <uc1:StatusBar runat="server" ID="StatusBar" ClearOnPostback="true"  />

        </div>
        <div id="RightColumn"></div>
    </div>

</asp:Content>
