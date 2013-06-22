<%@ Page Title="" Language="C#" MasterPageFile="~/GUI/ProtectedSites/ToolsMaster.master" AutoEventWireup="true" CodeBehind="ManageWineAttribute.aspx.cs" Inherits="EbalitWebForms.GUI.WineDatabase.ManageWineAttribute" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ToolsContent" runat="server">
    <asp:ObjectDataSource ID="odsWineAttribute" TypeName="EbalitWebForms.BusinessLogicLayer.Repository`1[[EbalitWebForms.DataLayer.WineAttribute, EbalitWebForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], EbalitWebForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" runat="server" OldValuesParameterFormatString="original_{0}"  SelectMethod="GetItems" DataObjectTypeName="EbalitWebForms.DataLayer.WineAttribute" DeleteMethod="DeleteItem" InsertMethod="CreateItem" UpdateMethod="UpdateItem" OnDeleted="odsWineAttribute_Deleted"></asp:ObjectDataSource>
    <div id="Container">
        <div id="LeftColumn">
        </div>
        <div id="MainColumn">
            <h2>Wine Attributes</h2>
            <asp:ListView ID="lvwWineAttribute" runat="server" DataSourceID="odsWineAttribute" DataKeyNames="Id" InsertItemPosition="LastItem">
            <EmptyDataTemplate>
                <table id="Table2" runat="server" style="">
                    <tr>
                        <td>No data was returned.</td>
                    </tr>
                </table>
            </EmptyDataTemplate>
            <EditItemTemplate>
                <tr style="">
                    <td>
                        <asp:LinkButton CssClass="CommandButton" ID="UpdateButton" runat="server" CommandName="Update" Text="Update" ValidationGroup="UpdateValidation" />
                        <asp:LinkButton CssClass="CommandButton" ID="CancelButton" runat="server" CommandName="Cancel" Text="Cancel" CausesValidation="False" />
                    </td>

                    <td>
                        <asp:TextBox ID="AttributeTextBox" runat="server" Text='<%# Bind("Attribute") %>' />
                    </td>


                    <td>
                        <asp:TextBox ID="txtRemarks" runat="server" Text='<%# Bind("Remarks") %>' />
                    </td>
                </tr>
                <asp:RequiredFieldValidator ValidationGroup="UpdateValidation" ID="RequiredFieldValidator2" runat="server" ErrorMessage="The field 'Attribute' is mandatory." ControlToValidate="AttributeTextBox"></asp:RequiredFieldValidator>
            </EditItemTemplate>
            <InsertItemTemplate>
                <tr style="">
                    <td>
                        <asp:LinkButton CssClass="CommandButton" ID="InsertButton" runat="server" CommandName="Insert" Text="Insert" ValidationGroup="InsertValidation" />
                        <asp:LinkButton CssClass="CommandButton" ID="CancelButton" runat="server" CommandName="Cancel" Text="Clear" CausesValidation="False" />
                    </td>
                    <td>
                        <asp:TextBox ID="AttributeTextBox" runat="server" Text='<%# Bind("Attribute") %>' CausesValidation="True" />
                    </td>
                    <td>
                        <asp:TextBox ID="RemarksTextBox" runat="server" Text='<%# Bind("Remarks") %>' CausesValidation="True" />
                    </td>
                </tr>
                <asp:RequiredFieldValidator ValidationGroup="InsertValidation" ID="RequiredFieldValidator1" runat="server" ErrorMessage="The field 'Attribute' is mandatory." ControlToValidate="AttributeTextBox"></asp:RequiredFieldValidator>
            </InsertItemTemplate>
                <ItemTemplate>
                    <tr style="">
                        <td>
                            <asp:LinkButton CssClass="CommandButton" ID="DeleteButton" runat="server" CommandName="Delete" Text="Delete" CausesValidation="False" />
                            <asp:LinkButton CssClass="CommandButton" ID="EditButton" runat="server" CommandName="Edit" Text="Edit" CausesValidation="False" />
                        </td>

                        <td>
                            <asp:Label ID="AttributeLabel" runat="server" Text='<%# Eval("Attribute") %>' />
                        </td>


                        <td>
                            <asp:Label ID="RemarksLabel" runat="server" Text='<%# Eval("Remarks") %>' />
                        </td>
                    </tr>
                </ItemTemplate>
                <LayoutTemplate>
                    <table id="Table1" runat="server" class="listview">
                        <tr id="Tr1" runat="server">
                            <td id="Td1" runat="server">
                                <table id="itemPlaceholderContainer" runat="server" border="0" style="">
                                    <tr id="Tr2" runat="server">
                                        <th id="Th1" runat="server"></th>
                                        <th id="Th2" runat="server">Attribute</th>
                                        <th id="Th3" runat="server">Remarks</th>
                                    </tr>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr id="Tr3" runat="server">
                            <td id="Td2" runat="server" style=""></td>
                        </tr>
                    </table>
                </LayoutTemplate>
                <SelectedItemTemplate>
                    <tr>
                        <td>
                            <asp:LinkButton CssClass="CommandButton" ID="DeleteButton" runat="server" CommandName="Delete" Text="Delete" CausesValidation="False" />
                            <asp:LinkButton CssClass="CommandButton" ID="EditButton" runat="server" CommandName="Edit" Text="Edit" CausesValidation="False" />
                        </td>

                        <td>
                            <asp:Label ID="AttributeLabel" runat="server" Text='<%# Eval("Attribute") %>' />
                        </td>

                        <td>
                            <asp:Label ID="RemarksLabel" runat="server" Text='<%# Eval("Remarks") %>' />
                        </td>
                    </tr>
                </SelectedItemTemplate>
            </asp:ListView>
            <asp:Label ID="lblStatus" runat="server" Text="" ForeColor="Red"></asp:Label>
        </div>
        <div id="RightColumn">
        </div>
    </div>
</asp:Content>
