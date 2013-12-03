<%@ Page Title="" Language="C#" MasterPageFile="~/GUI/ProtectedSites/ToolsMaster.master" AutoEventWireup="true" CodeBehind="UploadWorkingReports.aspx.cs" Inherits="EbalitWebForms.GUI.WorkingReport.UploadWorkingReports" %>

<%@ Register Src="~/GUI/WebUserControls/StatusBar.ascx" TagPrefix="uc1" TagName="StatusBar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ToolsContent" runat="server">
    <asp:ObjectDataSource ID="odsErroneousWorkingReports" runat="server" TypeName="EbalitWebForms.BusinessLogicLayer.WorkingReport.WorkingReportBll" OldValuesParameterFormatString="original_{0}" SelectMethod="GetErroneousWorkingReports"></asp:ObjectDataSource>
    <div id="Container">
        <div id="LeftColumn">
        </div>
        <div id="MainColumn">
            <asp:FileUpload ID="fulCsvFileUpload" runat="server" Width="300" />&nbsp;
            <asp:LinkButton ID="lnkUpload" runat="server" CausesValidation="false" CssClass="CommandButton" OnCommand="lnkUpload_OnCommand">Import</asp:LinkButton>&nbsp;
            <asp:ListView ID="lvwErroneousRecords" runat="server" DataSourceID="odsErroneousWorkingReports">
                <AlternatingItemTemplate>
                    <tr style="background-color: #FFF8DC;">
                        <td>
                            <asp:Label ID="IdLabel" runat="server" Text='<%# Eval("Id") %>' />
                        </td>
                        <td>
                            <asp:Label ID="ProjectNameLabel" runat="server" Text='<%# Eval("ProjectName") %>' />
                        </td>
                        <td>
                            <asp:Label ID="ResourceNameLabel" runat="server" Text='<%# Eval("ResourceName") %>' />
                        </td>
                        <td>
                            <asp:Label ID="DateLabel" runat="server" Text='<%# Eval("Date") %>' />
                        </td>
                        <td>
                            <asp:Label ID="WorkingTimeLabel" runat="server" Text='<%# Eval("WorkingTime") %>' />
                        </td>
                        <td>
                            <asp:Label ID="TfsTaskNameLabel" runat="server" Text='<%# Eval("TfsTaskName") %>' />
                        </td>
                        <td>
                            <asp:Label Text='<%# Eval("Description") %>' runat="server" ID="DescriptionLabel" /></td>
                    </tr>
                </AlternatingItemTemplate>
                <EditItemTemplate>
                    <tr style="background-color: #008A8C; color: #FFFFFF;">
                        <td>
                            <asp:Button ID="UpdateButton" runat="server" CommandName="Update" Text="Update" />
                            <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Cancel" />
                        </td>
                        <td>
                            <asp:TextBox ID="IdTextBox" runat="server" Text='<%# Bind("Id") %>' />
                        </td>
                        <td>
                            <asp:TextBox ID="ProjectNameTextBox" runat="server" Text='<%# Bind("ProjectName") %>' />
                        </td>
                        <td>
                            <asp:TextBox ID="ResourceNameTextBox" runat="server" Text='<%# Bind("ResourceName") %>' />
                        </td>
                        <td>
                            <asp:TextBox ID="DateTextBox" runat="server" Text='<%# Bind("Date") %>' />
                        </td>
                        <td>
                            <asp:TextBox ID="WorkingTimeTextBox" runat="server" Text='<%# Bind("WorkingTime") %>' />
                        </td>
                        <td>
                            <asp:TextBox ID="TfsTaskNameTextBox" runat="server" Text='<%# Bind("TfsTaskName") %>' />
                        </td>
                        <td>
                            <asp:TextBox Text='<%# Bind("Description") %>' runat="server" ID="DescriptionTextBox" /></td>
                    </tr>
                </EditItemTemplate>
                <EmptyDataTemplate>
                    <table runat="server" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px;">
                        <tr>
                            <td>No data was returned.</td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <InsertItemTemplate>
                    <tr style="">
                        <td>
                            <asp:Button ID="InsertButton" runat="server" CommandName="Insert" Text="Insert" />
                            <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Clear" />
                        </td>
                        <td>
                            <asp:TextBox ID="IdTextBox" runat="server" Text='<%# Bind("Id") %>' />
                        </td>
                        <td>
                            <asp:TextBox ID="ProjectNameTextBox" runat="server" Text='<%# Bind("ProjectName") %>' />
                        </td>
                        <td>
                            <asp:TextBox ID="ResourceNameTextBox" runat="server" Text='<%# Bind("ResourceName") %>' />
                        </td>
                        <td>
                            <asp:TextBox ID="DateTextBox" runat="server" Text='<%# Bind("Date") %>' />
                        </td>
                        <td>
                            <asp:TextBox ID="WorkingTimeTextBox" runat="server" Text='<%# Bind("WorkingTime") %>' />
                        </td>
                        <td>
                            <asp:TextBox ID="TfsTaskNameTextBox" runat="server" Text='<%# Bind("TfsTaskName") %>' />
                        </td>
                        <td>
                            <asp:TextBox Text='<%# Bind("Description") %>' runat="server" ID="DescriptionTextBox" /></td>
                    </tr>
                </InsertItemTemplate>
                <ItemTemplate>
                    <tr style="background-color: #DCDCDC; color: #000000;">
                        <td>
                            <asp:Label ID="IdLabel" runat="server" Text='<%# Eval("Id") %>' />
                        </td>
                        <td>
                            <asp:Label ID="ProjectNameLabel" runat="server" Text='<%# Eval("ProjectName") %>' />
                        </td>
                        <td>
                            <asp:Label ID="ResourceNameLabel" runat="server" Text='<%# Eval("ResourceName") %>' />
                        </td>
                        <td>
                            <asp:Label ID="DateLabel" runat="server" Text='<%# Eval("Date") %>' />
                        </td>
                        <td>
                            <asp:Label ID="WorkingTimeLabel" runat="server" Text='<%# Eval("WorkingTime") %>' />
                        </td>
                        <td>
                            <asp:Label ID="TfsTaskNameLabel" runat="server" Text='<%# Eval("TfsTaskName") %>' />
                        </td>
                        <td>
                            <asp:Label Text='<%# Eval("Description") %>' runat="server" ID="DescriptionLabel" /></td>
                    </tr>
                </ItemTemplate>
                <LayoutTemplate>
                    <table runat="server">
                        <tr runat="server">
                            <td runat="server">
                                <table id="itemPlaceholderContainer" runat="server" border="1" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px; font-family: Verdana, Arial, Helvetica, sans-serif;">
                                    <tr runat="server" style="background-color: #DCDCDC; color: #000000;">
                                        <th runat="server">Id</th>
                                        <th runat="server">ProjectName</th>
                                        <th runat="server">ResourceName</th>
                                        <th runat="server">Date</th>
                                        <th runat="server">WorkingTime</th>
                                        <th runat="server">TfsTaskName</th>
                                        <th runat="server">Description</th>
                                    </tr>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr runat="server">
                            <td runat="server" style="text-align: center; background-color: #CCCCCC; font-family: Verdana, Arial, Helvetica, sans-serif; color: #000000;">
                                <asp:DataPager ID="DataPager2" runat="server">
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
                            <asp:Label ID="ProjectNameLabel" runat="server" Text='<%# Eval("ProjectName") %>' />
                        </td>
                        <td>
                            <asp:Label ID="ResourceNameLabel" runat="server" Text='<%# Eval("ResourceName") %>' />
                        </td>
                        <td>
                            <asp:Label ID="DateLabel" runat="server" Text='<%# Eval("Date") %>' />
                        </td>
                        <td>
                            <asp:Label ID="WorkingTimeLabel" runat="server" Text='<%# Eval("WorkingTime") %>' />
                        </td>
                        <td>
                            <asp:Label ID="TfsTaskNameLabel" runat="server" Text='<%# Eval("TfsTaskName") %>' />
                        </td>
                        <td>
                            <asp:Label Text='<%# Eval("Description") %>' runat="server" ID="DescriptionLabel" /></td>
                    </tr>
                </SelectedItemTemplate>
            </asp:ListView>        
            
            
            <uc1:StatusBar runat="server" ID="StatusBar" ClearOnPostback="True"/>
        </div>
        <div id="RightColumn">
        </div>
    </div>
</asp:Content>
