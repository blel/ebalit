<%@ Page Title="" Language="C#" MasterPageFile="../ProtectedSites/AdminMaster.master" AutoEventWireup="true" CodeBehind="ManageBlogCategories.aspx.cs" Inherits="EbalitWebForms.GUI.ProtectedSites.WebForm4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
    <asp:ObjectDataSource ID="odsBlogCategories" runat="server" SelectMethod="ReadBlogCategory" TypeName="EbalitWebForms.BusinessLogicLayer.BlogCategoryDAL" DataObjectTypeName="EbalitWebForms.DataLayer.BlogCategory" DeleteMethod="DeleteBlogCategory" InsertMethod="CreateBlogCategory" UpdateMethod="UpdateBlogCategory"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsBlogTopics" runat="server" SelectMethod="ReadBlogTopic" TypeName="EbalitWebForms.BusinessLogicLayer.BlogTopicDAL"></asp:ObjectDataSource>
    <div id="Container">
        <asp:ListView ID="lsvBlogCategories" runat="server" DataSourceID="odsBlogCategories" InsertItemPosition="LastItem" DataKeyNames="Id">
            <AlternatingItemTemplate>
                <tr style="">
                    <td>
                        <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" Text="Delete" CausesValidation="False" />
                        <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="Edit" CausesValidation="False" />
                    </td>

                    <td>
                        <asp:Label ID="CategoryLabel" runat="server" Text='<%# Eval("Category") %>' />
                    </td>


                    <td>
                        <asp:Label ID="BlogTopicLabel" runat="server" Text='<%# Eval("BlogTopic.Topic") %>' />
                    </td>
                </tr>
            </AlternatingItemTemplate>
            <EditItemTemplate>
                <tr style="">
                    <td>
                        <asp:Button ID="UpdateButton" runat="server" CommandName="Update" Text="Update" ValidationGroup="UpdateValidation" />
                        <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Cancel" CausesValidation="False" />
                    </td>

                    <td>
                        <asp:TextBox ID="UpdateCategoryTextBox" runat="server" Text='<%# Bind("Category") %>' />
                    </td>


                    <td>
                        <asp:DropDownList ID="ddlBlogTopic" runat="server" SelectedValue='<%# Bind("FK_Topic") %>' DataSourceID="odsBlogTopics" DataTextField="Topic" DataValueField="Id"></asp:DropDownList>
                    </td>
                </tr>
                <asp:RequiredFieldValidator ValidationGroup="UpdateValidation" ID="RequiredFieldValidator2" runat="server" ErrorMessage="The field 'category' is mandatory." ControlToValidate="UpdateCategoryTextBox"></asp:RequiredFieldValidator>
            </EditItemTemplate>
            <EmptyDataTemplate>
                <table runat="server" style="">
                    <tr>
                        <td>No data was returned.</td>
                    </tr>
                </table>
            </EmptyDataTemplate>
            <InsertItemTemplate>
                <tr style="">
                    <td>
                        <asp:Button ID="InsertButton" runat="server" CommandName="Insert" Text="Insert" ValidationGroup="InsertValidation" />
                        <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Clear" CausesValidation="False" />
                    </td>
                    <td>
                        <asp:TextBox ID="CategoryTextBox" runat="server" Text='<%# Bind("Category") %>' CausesValidation="True" />
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlBlogTopic" runat="server" SelectedValue='<%# Bind("FK_Topic") %>' DataSourceID="odsBlogTopics" DataTextField="Topic" DataValueField="Id"></asp:DropDownList>
                    </td>
                </tr>
                <asp:RequiredFieldValidator ValidationGroup="InsertValidation" ID="RequiredFieldValidator1" runat="server" ErrorMessage="The field 'category' is mandatory." ControlToValidate="CategoryTextBox"></asp:RequiredFieldValidator>
            </InsertItemTemplate>
            <ItemTemplate>
                <tr style="">
                    <td>
                        <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" Text="Delete" CausesValidation="False" />
                        <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="Edit" CausesValidation="False" />
                    </td>

                    <td>
                        <asp:Label ID="CategoryLabel" runat="server" Text='<%# Eval("Category") %>' />
                    </td>


                    <td>
                        <asp:Label ID="BlogTopicLabel" runat="server" Text='<%# Eval("BlogTopic.Topic") %>' />
                    </td>
                </tr>
            </ItemTemplate>
            <LayoutTemplate>
                <table runat="server">
                    <tr runat="server">
                        <td runat="server">
                            <table id="itemPlaceholderContainer" runat="server" border="0" style="">
                                <tr runat="server" style="">
                                    <th runat="server"></th>
                                    <th runat="server">Category</th>
                                    <th runat="server">Topic</th>
                                </tr>
                                <tr id="itemPlaceholder" runat="server">
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr runat="server">
                        <td runat="server" style=""></td>
                    </tr>
                </table>
            </LayoutTemplate>
            <SelectedItemTemplate>
                <tr style="">
                    <td>
                        <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" Text="Delete" CausesValidation="False" />
                        <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="Edit" CausesValidation="False" />
                    </td>

                    <td>
                        <asp:Label ID="CategoryLabel" runat="server" Text='<%# Eval("Category") %>' />
                    </td>

                    <td>
                        <asp:Label ID="BlogTopicLabel" runat="server" Text='<%# Eval("BlogTopic.Topic") %>' />
                    </td>
                </tr>
            </SelectedItemTemplate>

        </asp:ListView>
        <div runat="server" id="StatusLine">
        </div>
    </div>
</asp:Content>
