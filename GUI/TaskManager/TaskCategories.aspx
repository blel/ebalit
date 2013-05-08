<%@ Page Title="" Language="C#" MasterPageFile="~/GUI/ProtectedSites/ToolsMaster.Master" AutoEventWireup="true" CodeBehind="TaskCategories.aspx.cs" Inherits="EbalitWebForms.GUI.WebForm11" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ToolsContent" runat="server">
    <asp:ObjectDataSource ID="odsTaskCategory" runat="server" DataObjectTypeName="EbalitWebForms.DataLayer.TaskCategory" DeleteMethod="Delete" InsertMethod="Create" SelectMethod="GetTaskCategories" TypeName="EbalitWebForms.BusinessLogicLayer.TaskCategoryBLL" UpdateMethod="Update"></asp:ObjectDataSource>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div id="Container">
        <div id="LeftColumn">
        </div>
        <div id="MainColumn">

            <asp:ListView ID="lvwTaskCategory" runat="server" DataSourceID="odsTaskCategory" InsertItemPosition="LastItem" DataKeyNames="Id" EnableTheming="True">

                <EditItemTemplate>
                    <tr >
                        <td>
                            <asp:LinkButton ID="UpdateButton" runat="server" CommandName="Update" CssClass="CommandButton">Update</asp:LinkButton>

                            <asp:LinkButton ID="CancelButton" runat="server" CommandName="Cancel" Text="Cancel" CssClass="CommandButton" />
                        </td>
                        <td>
                            <asp:TextBox ID="TaskCategory1TextBox" runat="server" Text='<%# Bind("TaskCategory1") %>' />
                        </td>
                        <td>
                            <asp:TextBox ID="DescriptionTextBox" runat="server" Text='<%# Bind("Description") %>' />
                        </td>
                    </tr>
                </EditItemTemplate>
                <EmptyDataTemplate>
                    <table runat="server" >
                        <tr>
                            <td>No data was returned.</td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <InsertItemTemplate>
                    <tr >
                        <td>
                            <asp:LinkButton ID="InsertButton" runat="server" CommandName="Insert" Text="Insert" CssClass="CommandButton" />
                            <asp:LinkButton ID="CancelButton" runat="server" CommandName="Cancel" Text="Clear" CssClass="CommandButton" />
                        </td>
                        <td>
                            <asp:TextBox ID="TaskCategory1TextBox" runat="server" Text='<%# Bind("TaskCategory1") %>' />
                        </td>
                        <td>
                            <asp:TextBox ID="DescriptionTextBox" runat="server" Text='<%# Bind("Description") %>' />
                        </td>
                    </tr>
                </InsertItemTemplate>
                <ItemTemplate>
                    <tr >
                        <td>
                            <asp:LinkButton ID="DeleteButton" runat="server" CommandName="Delete" Text="Delete" CssClass="CommandButton" />
                            <asp:LinkButton ID="EditButton" runat="server" CommandName="Edit" Text="Edit" CssClass="CommandButton" />
                        </td>
                        <td>
                            <asp:Label ID="TaskCategory1Label" runat="server" Text='<%# Eval("TaskCategory1") %>' />
                        </td>
                        <td>
                            <asp:Label ID="DescriptionLabel" runat="server" Text='<%# Eval("Description") %>' />
                        </td>
                    </tr>
                </ItemTemplate>
                <LayoutTemplate>
                    <table runat="server" class="listview">
                        <tr runat="server">
                            <td runat="server">
                                <table id="itemPlaceholderContainer" runat="server" border="0">
                                    <tr runat="server" style="">
                                        <th runat="server"></th>
                                        <th runat="server">Task Category</th>
                                        <th runat="server">Description</th>
                                    </tr>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr runat="server">
                            <td runat="server" >
                                <asp:DataPager ID="DataPager1" runat="server">
                                    <Fields>
                                        <asp:NextPreviousPagerField ButtonType="Link" ShowFirstPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False" />
                                        <asp:NumericPagerField />
                                        <asp:NextPreviousPagerField ButtonType="Link" ShowLastPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False" />
                                    </Fields>
                                </asp:DataPager>
                            </td>
                        </tr>
                    </table>
                </LayoutTemplate>
                <SelectedItemTemplate>
                    <tr >
                        <td>
                            <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" Text="Delete" />
                            <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="Edit" />
                        </td>
                        <td>
                            <asp:Label ID="TaskCategory1Label" runat="server" Text='<%# Eval("TaskCategory1") %>' />
                        </td>
                        <td>
                            <asp:Label ID="DescriptionLabel" runat="server" Text='<%# Eval("Description") %>' />
                        </td>
                    </tr>
                </SelectedItemTemplate>
            </asp:ListView>

        </div>
        <div id="RightColumn">
        </div>
    </div>
</asp:Content>
