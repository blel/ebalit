<%@ Page Title="" Language="C#" MasterPageFile="~/GUI/Master.Master" AutoEventWireup="true" CodeBehind="TaskList.aspx.cs" Inherits="EbalitWebForms.GUI.TaskManager.TaskList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <asp:ObjectDataSource ID="odsTasks" runat="server" DataObjectTypeName="EbalitWebForms.DataLayer.Task" DeleteMethod="DeleteTask" InsertMethod="CreateTask" SelectMethod="GetTasks" TypeName="EbalitWebForms.BusinessLogicLayer.TaskBLL" UpdateMethod="UpdateTask"></asp:ObjectDataSource>
    <div id="TitleContainer">
        <div id="Title">
            >> Task > Task List
        </div>
    </div>
    <div id="Container">
        <div id="LeftColumn"></div>
            <asp:ListView ID="lvwTasks" runat="server" DataSourceID="odsTasks" DataKeyNames="Id">
                <AlternatingItemTemplate>
                    <tr style="background-color: #FFF8DC;">
                        <td>
                            <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" Text="Delete" />
                         </td>

                        <td>
                            <asp:Label ID="SubjectLabel" runat="server" Text='<%# Eval("Subject") %>' />
                        </td>

                        <td>
                            <asp:Label ID="DueDateLabel" runat="server" Text='<%# Eval("DueDate") %>' />
                        </td>
                        <td>
                            <asp:Label ID="FK_TaskCategoryLabel" runat="server" Text='<%# Eval("TaskCategory.TaskCategory1") %>' />
                        </td>
                        <td>
                            <asp:Label ID="StateLabel" runat="server" Text='<%# Eval("State") %>' />
                        </td>
                        <td>
                            <asp:Label ID="PriorityLabel" runat="server" Text='<%# Eval("Priority") %>' />
                        </td>
                        <td>
                            <asp:Label ID="ClosingTypeLabel" runat="server" Text='<%# Eval("ClosingType") %>' />
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
                            <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" Text="Delete" />
                      
                        </td>

                        <td>
                            <asp:Label ID="SubjectLabel" runat="server" Text='<%# Eval("Subject") %>' />
                        </td>

                        <td>
                            <asp:Label ID="DueDateLabel" runat="server" Text='<%# Eval("DueDate") %>' />
                        </td>
                        <td>
                            <asp:Label ID="FK_TaskCategoryLabel" runat="server" Text='<%# Eval("TaskCategory.TaskCategory1") %>' />
                        </td>
                        <td>
                            <asp:Label ID="StateLabel" runat="server" Text='<%# Eval("State") %>' />
                        </td>
                        <td>
                            <asp:Label ID="PriorityLabel" runat="server" Text='<%# Eval("Priority") %>' />
                        </td>
                        <td>
                            <asp:Label ID="ClosingTypeLabel" runat="server" Text='<%# Eval("ClosingType") %>' />
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
                                        <th runat="server">Subject</th>
                                        <th runat="server">DueDate</th>
                                        <th runat="server">Task Category</th>
                                        <th runat="server">State</th>
                                        <th runat="server">Priority</th>
                                        <th runat="server">ClosingType</th>

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
                            <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="Edit" />
                        </td>
                        <td>
                            <asp:Label ID="IdLabel" runat="server" Text='<%# Eval("Id") %>' />
                        </td>
                        <td>
                            <asp:Label ID="SubjectLabel" runat="server" Text='<%# Eval("Subject") %>' />
                        </td>
                        <td>
                            <asp:Label ID="ContentLabel" runat="server" Text='<%# Eval("Content") %>' />
                        </td>
                        <td>
                            <asp:Label ID="DueDateLabel" runat="server" Text='<%# Eval("DueDate") %>' />
                        </td>
                        <td>
                            <asp:Label ID="FK_TaskCategoryLabel" runat="server" Text='<%# Eval("FK_TaskCategory") %>' />
                        </td>
                        <td>
                            <asp:Label ID="StateLabel" runat="server" Text='<%# Eval("State") %>' />
                        </td>
                        <td>
                            <asp:Label ID="PriorityLabel" runat="server" Text='<%# Eval("Priority") %>' />
                        </td>
                        <td>
                            <asp:Label ID="ClosingTypeLabel" runat="server" Text='<%# Eval("ClosingType") %>' />
                        </td>
                        <td>
                            <asp:Label ID="CreatedOnLabel" runat="server" Text='<%# Eval("CreatedOn") %>' />
                        </td>
                        <td>
                            <asp:Label ID="CreatedByLabel" runat="server" Text='<%# Eval("CreatedBy") %>' />
                        </td>
                        <td>
                            <asp:Label ID="ChangedOnLabel" runat="server" Text='<%# Eval("ChangedOn") %>' />
                        </td>
                        <td>
                            <asp:Label ID="ChangedByLabel" runat="server" Text='<%# Eval("ChangedBy") %>' />
                        </td>
                        <td>
                            <asp:Label ID="TaskCategoryLabel" runat="server" Text='<%# Eval("TaskCategory") %>' />
                        </td>
                        <td>
                            <asp:Label ID="TaskCommentsLabel" runat="server" Text='<%# Eval("TaskComments") %>' />
                        </td>
                    </tr>
                </SelectedItemTemplate>
            </asp:ListView>

    </div>
</asp:Content>
