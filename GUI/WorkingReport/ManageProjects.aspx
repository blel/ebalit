<%@ Page Title="" Language="C#" MasterPageFile="~/GUI/ProtectedSites/ToolsMaster.master" AutoEventWireup="true" CodeBehind="ManageProjects.aspx.cs" Inherits="EbalitWebForms.GUI.WorkingReport.ManageProjects" %>

<%@ Register Src="~/GUI/WebUserControls/StatusBar.ascx" TagPrefix="uc1" TagName="StatusBar" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ToolsContent" runat="server">
    <asp:ObjectDataSource ID="odsProjects" runat="server"
        TypeName="EbalitWebForms.BusinessLogicLayer.WorkingReport.ProjectBll" OldValuesParameterFormatString="original_{0}" SelectMethod="GetItems" 
        DataObjectTypeName="EbalitWebForms.DataLayer.ProjectProject" DeleteMethod="DeleteItem"
        OnDeleted="odsProjects_OnDeleted"></asp:ObjectDataSource>
    <div id="Container">
        <div id="LeftColumn">
        </div>
        <div id="MainColumn">
            <h2>Manage Projects</h2>

            <asp:ListView ID="lvwProjects" runat="server" DataSourceID="odsProjects" DataKeyNames="Id">
                <AlternatingItemTemplate>
                    <tr style="background-color: #FFF8DC;">
                        <td>
                            <asp:LinkButton ID="DeleteButton" runat="server" OnClientClick="return confirm('Do you really want to delete this record?')" CommandName="Delete" Text="Delete" CssClass="CommandButton" />
                            <asp:LinkButton ID="btnDetails" runat="server" CommandArgument='<%# Eval("Id") %>' CssClass="CommandButton" OnCommand="btnDetails_Command">Details</asp:LinkButton></td>
                        <td>
                            <asp:Label ID="NameLabel" runat="server" Text='<%# Eval("Name") %>' />
                        </td>
                    </tr>
                </AlternatingItemTemplate>
                <EmptyDataTemplate>
                    <table runat="server" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px;">
                        <tr>
                            <td>No data was returned.</td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <ItemTemplate>
                    <tr style="background-color: #DCDCDC; color: #000000;">
                        <td>
                            <asp:LinkButton ID="DeleteButton" runat="server" OnClientClick="return confirm('Do you really want to delete this record?')" CommandName="Delete" Text="Delete" CssClass="CommandButton" />
                            <asp:LinkButton ID="btnDetails" runat="server" CommandArgument='<%# Eval("Id") %>' CssClass="CommandButton" OnCommand="btnDetails_Command">Details</asp:LinkButton></td>
                        <td>
                            <asp:Label ID="NameLabel" runat="server" Text='<%# Eval("Name") %>' />
                        </td>
                    </tr>
                </ItemTemplate>
                <LayoutTemplate>
                    <table runat="server">
                        <tr runat="server">
                            <td runat="server">
                                <table id="itemPlaceholderContainer" runat="server" border="1" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px; font-family: Verdana, Arial, Helvetica, sans-serif;">
                                    <tr runat="server" style="background-color: #DCDCDC; color: #000000;">
                                        <th runat="server"></th>
                                        <th runat="server">Name</th>

                                    </tr>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr runat="server">
                            <td runat="server" style="text-align: center; background-color: #CCCCCC; font-family: Verdana, Arial, Helvetica, sans-serif; color: #000000;">
                                <asp:DataPager ID="DataPager1" runat="server">
                                    <Fields>
                                        <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False" />
                                        <asp:NumericPagerField />
                                        <asp:NextPreviousPagerField ButtonType="Button" ShowLastPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False" />
                                    </Fields>
                                </asp:DataPager>
                            </td>
                        </tr>
                    </table>
                </LayoutTemplate>
                <SelectedItemTemplate>
                    <tr style="background-color: #008A8C; font-weight: bold; color: #FFFFFF;">
                        <td>
                            <asp:Label ID="IdLabel" runat="server" Text='<%# Eval("Id") %>' />
                        </td>
                        <td>
                            <asp:Label ID="NameLabel" runat="server" Text='<%# Eval("Name") %>' />
                        </td>
                        <td>
                            <asp:Label ID="GuidLabel" runat="server" Text='<%# Eval("Guid") %>' />
                        </td>
                        <td>
                            <asp:Label ID="ProjectResourcesLabel" runat="server" Text='<%# Eval("ProjectResources") %>' />
                        </td>
                        <td>
                            <asp:Label ID="ProjectTasksLabel" runat="server" Text='<%# Eval("ProjectTasks") %>' />
                        </td>
                        <td>
                            <asp:Label ID="ProjectWorkingReportsLabel" runat="server" Text='<%# Eval("ProjectWorkingReports") %>' />
                        </td>
                    </tr>
                </SelectedItemTemplate>
            </asp:ListView>
            <uc1:StatusBar runat="server" id="StatusBar" ClearOnPostback="True" />
        </div>
        <div id="RightColumn">
        </div>
    </div>
</asp:Content>
