<%@ Page Title="" Language="C#" MasterPageFile="~/GUI/ProtectedSites/ToolsMaster.master" AutoEventWireup="true" CodeBehind="ManageConsumations.aspx.cs" Inherits="EbalitWebForms.GUI.WineDatabase.ManageConsumations" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ToolsContent" runat="server">
    <asp:ObjectDataSource ID="odsConsumation" TypeName="EbalitWebForms.BusinessLogicLayer.Repository`1[[EbalitWebForms.DataLayer.WineConsumation, EbalitWebForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], EbalitWebForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetItems" DataObjectTypeName="EbalitWebForms.DataLayer.WineConsumation" DeleteMethod="DeleteItem" InsertMethod="CreateItem" UpdateMethod="UpdateItem" OnDeleted="odsConsumation_Deleted">
        <SelectParameters>
            <asp:Parameter DefaultValue="Wine" Name="include" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:XmlDataSource ID="xdsWineRating" runat="server" DataFile="~/Resources/MasterData.xml" XPath="/MasterData/DataDictionary[@Name='WineRating']/DictionaryItem"></asp:XmlDataSource>

    <div id="Container">
        <div id="LeftColumn"></div>
        <div id="MainColumn">
            <h2>Consumations</h2>
            <div id="Filter">
            </div>
            <asp:ListView ID="lvwConsumations" runat="server" DataSourceID="odsConsumation" DataKeyNames="Id">
                <LayoutTemplate>
                    <table id="Table2" runat="server" style="width: 100%;">
                        <tr id="Tr1" runat="server">
                            <td id="Td1" runat="server">
                                <table id="itemPlaceholderContainer" runat="server" border="1" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px; font-family: Arial; width: 100%;">
                                    <tr id="Tr2" runat="server" style="background-color: #DCDCDC; color: #000000;">
                                        <th id="Th1" runat="server"></th>
                                        <th id="Th2" runat="server">&nbsp;Taster&nbsp;</th>
                                        <th id="Th3" runat="server">&nbsp;Wine&nbsp;</th>
                                        <th id="Th4" runat="server">&nbsp;Tasted On&nbsp;</th>
                                        <th id="Th5" runat="server">&nbsp;Rating&nbsp;</th>
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
                            <asp:Label ID="lblTaster" runat="server" Text='<%# Eval("Taster") %>' />
                        </td>

                        <td>
                            <asp:Label ID="lblWine" runat="server" Text='<%# Eval("Wine.Label") %>' />
                        </td>
                        <td>
                            <asp:Label ID="lblTastedOn" runat="server" Text='<%# Eval("TastedOn","{0:d}") %>' />
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlRating" AppendDataBoundItems="true" Enabled="false" DataSourceID="xdsWineRating" DataTextField="Value" DataValueField="Key" SelectedValue='<%#Bind("Rating") %>' runat="server">
                                <asp:ListItem Text="No rating" Value=""></asp:ListItem>
                            </asp:DropDownList>
                            
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
                            <asp:Label ID="lblTaster" runat="server" Text='<%# Eval("Taster") %>' />
                        </td>

                        <td>
                            <asp:Label ID="lblWine" runat="server" Text='<%# Eval("Wine.Label") %>' />
                        </td>
                        <td>
                            <asp:Label ID="lblTastedOn" runat="server" Text='<%# Eval("TastedOn","{0:d}") %>' />
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlRating" Enabled="false" AppendDataBoundItems="true" DataSourceID="xdsWineRating" DataTextField="Value" DataValueField="Key" SelectedValue='<%#Bind("Rating") %>' runat="server">
                                <asp:ListItem Text="No rating" Value=""></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </AlternatingItemTemplate>
            </asp:ListView>
            <asp:Label ID="lblStatus" runat="server" Text="" ForeColor="Red"></asp:Label>
        </div>
        <div id="RightColumn"></div>
    </div>
</asp:Content>
