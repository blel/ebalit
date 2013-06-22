<%@ Page Title="" Language="C#" MasterPageFile="~/GUI/ProtectedSites/ToolsMaster.master" AutoEventWireup="true" CodeBehind="ManageWines.aspx.cs" Inherits="EbalitWebForms.GUI.WineDatabase.ManageWines" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ToolsContent" runat="server">
    <asp:ObjectDataSource ID="odsWines" TypeName="EbalitWebForms.BusinessLogicLayer.Repository`1[[EbalitWebForms.DataLayer.Wine, EbalitWebForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], EbalitWebForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" runat="server" DataObjectTypeName="EbalitWebForms.DataLayer.Wine" DeleteMethod="DeleteItem" InsertMethod="CreateItem" OldValuesParameterFormatString="original_{0}" SelectMethod="GetItems" UpdateMethod="UpdateItem" OnDeleted="odsWines_Deleted"></asp:ObjectDataSource>
    <div id="Container">
        <div id="LeftColumn"></div>
        <div id="MainColumn">
            <h2>Wines</h2>
            <div id="Filter">
            </div>
            <asp:ListView ID="lvwWines" runat="server" DataSourceID="odsWines" DataKeyNames="Id">
                <LayoutTemplate>
                    <table id="Table2" runat="server" style="width: 100%;">
                        <tr id="Tr1" runat="server">
                            <td id="Td1" runat="server">
                                <table id="itemPlaceholderContainer" runat="server" border="1" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px; font-family: Arial; width: 100%;">
                                    <tr id="Tr2" runat="server" style="background-color: #DCDCDC; color: #000000;">
                                        <th id="Th1" runat="server" style="width:120px;"></th>
                                        <th id="Th2" runat="server">&nbsp;Label&nbsp;</th>
                                        <th id="Th3" runat="server">&nbsp;Year&nbsp;</th>
                                        <th id="Th4" runat="server">&nbsp;Grape&nbsp;</th>
                                        <th id="Th5" runat="server">&nbsp;Origin&nbsp;</th>
                                        <th id="Th6" runat="server">&nbsp;Bought On&nbsp;</th>
                                        <th id="Th7" runat="server">&nbsp;Store&nbsp;</th>

                                    </tr>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr id="Tr3" runat="server">
                            <td id="Td2" runat="server" style="text-align: center; background-color: #CCCCCC; font-family: Verdana, Arial, Helvetica, sans-serif; color: #000000;">
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
                <EmptyDataTemplate>
                    <table id="Table1" runat="server" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px;">
                        <tr>
                            <td>No data was returned.
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <ItemTemplate>
                    <tr style="background-color: #DCDCDC;" id="WineListRow" runat="server">
                        <td>
                            <asp:LinkButton ID="DeleteButton" runat="server" CommandName="Delete" Text="Delete" CssClass="CommandButton" />
                            <asp:LinkButton ID="btnDetails" runat="server" CssClass="CommandButton" OnCommand="btnDetails_Command" CommandArgument='<%# Eval("Id") %>'>Details</asp:LinkButton>
                        </td>

                        <td>
                            <asp:Label ID="lblLabel" runat="server" Text='<%# Eval("Label") %>' />
                        </td>

                        <td>
                            <asp:Label ID="lblYear" runat="server" Text='<%# Eval("Year") %>' />
                        </td>
                        <td>
                            <asp:Label ID="lblGrape" runat="server" Text='<%# Eval("Grape") %>' />
                        </td>
                        <td>
                            <asp:Label ID="lblOrigin" runat="server" Text='<%# Eval("Origin") %>' />
                        </td>
                        <td>
                            <asp:Label ID="lblBoughtOn" runat="server" Text='<%# Eval("BoughtOn","{0:d}") %>' />
                        </td>
                        <td>
                            <asp:Label ID="lblMagazine" runat="server" Text='<%# Eval("Magazine") %>' />
                        </td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr style="background-color: #FFF8DC;" id="TaskListRow" runat="server">
                        <td>
                            <asp:LinkButton ID="DeleteButton" runat="server" CommandName="Delete" Text="Delete" CssClass="CommandButton" />
                            <asp:LinkButton ID="btnDetails" runat="server" CssClass="CommandButton" OnCommand="btnDetails_Command" CommandArgument='<%# Eval("Id") %>'>Details</asp:LinkButton>
                        </td>

                        <td>
                            <asp:Label ID="lblLabel" runat="server" Text='<%# Eval("Label") %>' />
                        </td>

                        <td>
                            <asp:Label ID="lblYear" runat="server" Text='<%# Eval("Year") %>' />
                        </td>
                        <td>
                            <asp:Label ID="lblGrape" runat="server" Text='<%# Eval("Grape") %>' />
                        </td>
                        <td>
                            <asp:Label ID="lblOrigin" runat="server" Text='<%# Eval("Origin") %>' />
                        </td>
                        <td>
                            <asp:Label ID="lblBoughtOn" runat="server" Text='<%# Eval("BoughtOn","{0:d}") %>' />
                        </td>
                        <td>
                            <asp:Label ID="lblMagazine" runat="server" Text='<%# Eval("Magazine") %>' />
                        </td>
                    </tr>
                </AlternatingItemTemplate>
            </asp:ListView>
            <asp:Label ID="lblStatus" runat="server" Text="" ForeColor="Red"></asp:Label>
        </div>
        <div id="RightColumn"></div>
    </div>
</asp:Content>
