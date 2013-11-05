﻿<%@ Page Title="" Language="C#" MasterPageFile="~/GUI/ProtectedSites/ToolsMaster.master" AutoEventWireup="true" CodeBehind="ManageWorkingReports.aspx.cs" Inherits="EbalitWebForms.GUI.WorkingReport.ManageWorkingReports" %>

<%@ Register Assembly="EbalitWebForms" Namespace="EbalitWebForms.Common" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ToolsContent" runat="server">
    <asp:ObjectDataSource ID="odsWorkingReports" runat="server" DataObjectTypeName="EbalitWebForms.DataLayer.ProjectWorkingReport" DeleteMethod="DeleteWorkingReport" InsertMethod="CreateWorkingReport" SelectMethod="GetWorkingReports" TypeName="EbalitWebForms.BusinessLogicLayer.WorkingReport.WorkingReportBll" UpdateMethod="UpdateWorkingReport"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsProjects" runat="server" SelectMethod="GetProjects" TypeName="EbalitWebForms.BusinessLogicLayer.WorkingReport.WorkingReportBll"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsTasks" runat="server"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsResources" runat="server" SelectMethod="GetResources" TypeName="EbalitWebForms.BusinessLogicLayer.WorkingReport.WorkingReportBll" OnSelecting="odsResources_OnSelecting">
        <SelectParameters>
            <asp:Parameter Name="projectId" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <cc1:HierarchicalTaskDataSource ID="htsTasks" runat="server" ProjectId="1"></cc1:HierarchicalTaskDataSource>
    <asp:ScriptManager ID="scmAjaxToolkit" runat="server"></asp:ScriptManager>
    <div id="Container">
        <div id="LeftColumn">
        </div>

        <div id="MainColumn">
            <div id="Filter">


                <asp:Table ID="tblFilter" runat="server" Width="769px">
                    <asp:TableRow runat="server">
                        <asp:TableCell runat="server">
                            <asp:Label ID="Label1" runat="server" Text="Project"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell ID="TableCell1" runat="server">
                            <asp:DropDownList ID="ddlProjects" runat="server" DataSourceID="odsProjects" DataTextField="Name" DataValueField="Id"></asp:DropDownList>
                        </asp:TableCell>
                        <asp:TableCell ID="TableCell2" runat="server">
                            <asp:Label ID="Label2" runat="server" Text="Task"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell ID="TableCell3" runat="server">
                            <asp:TextBox runat="server" ID="txtTaskDropDown" />
                            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="CommentsPopup" TargetControlID="txtTaskDropDown"></ajaxToolkit:ModalPopupExtender>
                        </asp:TableCell>
                        <asp:TableCell ID="TableCell4" runat="server">
                            <asp:Label ID="Label3" runat="server" Text="Resource"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell ID="TableCell5" runat="server">
                            <asp:DropDownList ID="ddlResource" runat="server" DataSourceID="odsResources" DataTextField="Name" DataValueField="Id"></asp:DropDownList>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="TableRow1" runat="server">
                        <asp:TableCell ID="TableCell6" runat="server">
                            <asp:Label ID="Label4" runat="server" Text="Date From"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell ID="TableCell7" runat="server">
                            <asp:TextBox ID="txtDate" runat="server"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDate"></ajaxToolkit:CalendarExtender>
                        </asp:TableCell>
                        <asp:TableCell ID="TableCell8" runat="server">
                            <asp:Label ID="Label5" runat="server" Text="Date To"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell ID="TableCell9" runat="server">
                            <asp:TextBox ID="txtDateTo" runat="server"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDateTo"></ajaxToolkit:CalendarExtender>
                        </asp:TableCell>
                        <asp:TableCell ID="TableCell10" runat="server">
                            
                        </asp:TableCell>
                        <asp:TableCell ID="TableCell11" runat="server">
                            
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableFooterRow>
                        <asp:TableCell ColumnSpan="2">
                            <asp:LinkButton ID="lnkFind" runat="server" CausesValidation="false" CssClass="CommandButton">Find</asp:LinkButton>&nbsp;
                        <asp:LinkButton ID="lnkClear" runat="server" CausesValidation="false" CssClass="CommandButton">Clear</asp:LinkButton>&nbsp;
                        <asp:LinkButton ID="lnkCreate" runat="server" CausesValidation="false" CssClass="CommandButton">Create</asp:LinkButton>&nbsp;
                        <asp:LinkButton ID="lnkExport" runat="server" CausesValidation="false" CssClass="CommandButton">Export</asp:LinkButton>
                        </asp:TableCell>
                    </asp:TableFooterRow>
                </asp:Table>
            </div>
            <asp:ListView ID="lvwWorkingReports" runat="server" DataSourceID="odsWorkingReports">
                <AlternatingItemTemplate>
                    <tr style="background-color: #FFF8DC;">
                        <td>
                            <asp:LinkButton ID="DeleteButton" runat="server" CommandName="Delete" Text="Delete" CssClass="CommandButton" />
                            <asp:LinkButton ID="EditButton" runat="server" CommandName="Edit" Text="Edit" CssClass="CommandButton" />
                            <asp:LinkButton ID="btnDetails" runat="server" CommandArgument='<%# Eval("Id") %>' CssClass="CommandButton">Details</asp:LinkButton>
                        </td>
                        <td>
                            <asp:Label ID="ProjectIdLabel" runat="server" Text='<%# Eval("ProjectProject.Name") %>' />
                        </td>
                        <td>
                            <asp:Label ID="ResourceIdLabel" runat="server" Text='<%# Eval("ResourceId") %>' />
                        </td>
                        <td>
                            <asp:Label ID="TaskIdLabel" runat="server" Text='<%# Eval("ProjectTask.Name") %>' />
                        </td>
                        <td>
                            <asp:Label ID="DateLabel" runat="server" Text='<%# Eval("From","{0:d}") %>' />
                        </td>
                        <td>
                            <asp:Label ID="FromLabel" runat="server" Text='<%# Eval("From","{0:HH:mm}") %>' />
                        </td>
                        <td>
                            <asp:Label ID="ToLabel" runat="server" Text='<%# Eval("To","{0:HH:mm}") %>' />
                        </td>
                        <td>
                            <asp:Label ID="NotesLabel" runat="server" Text='<%# Eval("Notes") %>' />
                        </td>

                    </tr>
                </AlternatingItemTemplate>
                <EditItemTemplate>
                    <tr style="background-color: #008A8C; color: #FFFFFF;">
                        <td>
                            <asp:LinkButton ID="UpdateButton" runat="server" CommandName="Update" Text="Update" CssClass="CommandButton" />
                            <asp:LinkButton ID="CancelButton" runat="server" CommandName="Cancel" Text="Cancel" CssClass="CommandButton" />&nbsp;
                        </td>
                        <td>
                            <asp:TextBox ID="IdTextBox" runat="server" Text='<%# Bind("Id") %>' />
                        </td>
                        <td>
                            <asp:TextBox ID="ProjectIdTextBox" runat="server" Text='<%# Bind("ProjectId") %>' />
                        </td>
                        <td>
                            <asp:TextBox ID="ResourceIdTextBox" runat="server" Text='<%# Bind("ResourceId") %>' />
                        </td>
                        <td>
                            <asp:TextBox ID="TaskIdTextBox" runat="server" Text='<%# Bind("TaskId") %>' />
                        </td>
                        <td>
                            <asp:TextBox ID="FromTextBox" runat="server" Text='<%# Bind("From") %>' />
                        </td>
                        <td>
                            <asp:TextBox ID="ToTextBox" runat="server" Text='<%# Bind("To") %>' />
                        </td>
                        <td>
                            <asp:TextBox ID="NotesTextBox" runat="server" Text='<%# Bind("Notes") %>' />
                        </td>

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
                            <asp:Button ID="InsertButton" runat="server" CommandName="Insert" Text="Insert" CssClass="CommandButton" />
                            <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Clear" CssClass="CommandButton" />
                        </td>
                        <td>
                            <asp:TextBox ID="IdTextBox" runat="server" Text='<%# Bind("Id") %>' />
                        </td>
                        <td>
                            <asp:TextBox ID="ProjectIdTextBox" runat="server" Text='<%# Bind("ProjectId") %>' />
                        </td>
                        <td>
                            <asp:TextBox ID="ResourceIdTextBox" runat="server" Text='<%# Bind("ResourceId") %>' />
                        </td>
                        <td>
                            <asp:TextBox ID="TaskIdTextBox" runat="server" Text='<%# Bind("TaskId") %>' />
                        </td>
                        <td>
                            <asp:TextBox ID="FromTextBox" runat="server" Text='<%# Bind("From") %>' />
                        </td>
                        <td>
                            <asp:TextBox ID="ToTextBox" runat="server" Text='<%# Bind("To") %>' />
                        </td>
                        <td>
                            <asp:TextBox ID="NotesTextBox" runat="server" Text='<%# Bind("Notes") %>' />
                        </td>

                    </tr>
                </InsertItemTemplate>
                <ItemTemplate>
                    <tr style="background-color: #DCDCDC; color: #000000;">
                        <td>
                            <asp:LinkButton ID="DeleteButton" runat="server" CommandName="Delete" Text="Delete" CssClass="CommandButton" />
                            <asp:LinkButton ID="EditButton" runat="server" CommandName="Edit" Text="Edit" CssClass="CommandButton" />
                            <asp:LinkButton ID="btnDetails" runat="server" CommandArgument='<%# Eval("Id") %>' CssClass="CommandButton">Details</asp:LinkButton>

                        </td>
                        <td>
                            <asp:Label ID="ProjectIdLabel" runat="server" Text='<%# Eval("ProjectProject.Name") %>' />
                        </td>
                        <td>
                            <asp:Label ID="ResourceIdLabel" runat="server" Text='<%# Eval("ResourceId") %>' />
                        </td>
                        <td>
                            <asp:Label ID="TaskIdLabel" runat="server" Text='<%# Eval("ProjectTask.Name") %>' />
                        </td>
                        <td>
                            <asp:Label ID="DateLabel" runat="server" Text='<%# Eval("From","{0:d}") %>' />
                        </td>
                        <td>
                            <asp:Label ID="FromLabel" runat="server" Text='<%# Eval("From","{0:HH:mm}") %>' />
                        </td>
                        <td>
                            <asp:Label ID="ToLabel" runat="server" Text='<%# Eval("To","{0:HH:mm}") %>' />
                        </td>
                        <td>
                            <asp:Label ID="NotesLabel" runat="server" Text='<%# Eval("Notes") %>' />
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
                                        <th runat="server">Project</th>
                                        <th runat="server">Resource</th>
                                        <th runat="server">Task</th>
                                        <th runat="server">Date</th>
                                        <th runat="server">From</th>
                                        <th runat="server">To</th>
                                        <th runat="server">Notes</th>
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
                            <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" Text="Delete" />
                            <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="Edit" />&nbsp;
                        </td>
                        <td>
                            <asp:Label ID="ProjectIdLabel" runat="server" Text='<%# Eval("ProjectId") %>' />
                        </td>
                        <td>
                            <asp:Label ID="ResourceIdLabel" runat="server" Text='<%# Eval("ResourceId") %>' />
                        </td>
                        <td>
                            <asp:Label ID="TaskIdLabel" runat="server" Text='<%# Eval("TaskId") %>' />
                        </td>
                        <td>
                            <asp:Label ID="FromLabel" runat="server" Text='<%# Eval("From") %>' />
                        </td>
                        <td>
                            <asp:Label ID="ToLabel" runat="server" Text='<%# Eval("To") %>' />
                        </td>
                        <td>
                            <asp:Label ID="NotesLabel" runat="server" Text='<%# Eval("Notes") %>' />
                        </td>

                    </tr>
                </SelectedItemTemplate>
            </asp:ListView>
            <div id="CommentsPopup" class="Popup">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">

                    <ContentTemplate>
                        <asp:TreeView ID="trvTask" runat="server" DataSourceID="htsTasks" OnSelectedNodeChanged="trvTask_OnSelectedNodeChanged">
                            <DataBindings>
                                <asp:TreeNodeBinding DataMember="Task" TextField="Name" />
                            </DataBindings>
                        </asp:TreeView>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div id="Buttons">
                    <asp:LinkButton ID="lnkClose" runat="server" CssClass="CommandButton" CausesValidation="false">Close</asp:LinkButton>
                </div>
            </div>

        </div>
        <div id="RightColumn">
        </div>
    </div>
</asp:Content>

