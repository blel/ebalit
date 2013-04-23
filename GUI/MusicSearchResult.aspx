<%@ Page Title="" Language="C#" MasterPageFile="~/GUI/Master.Master" AutoEventWireup="true" CodeBehind="MusicSearchResult.aspx.cs" Inherits="EbalitWebForms.GUI.WebForm7" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <asp:ObjectDataSource ID="odsMusicSearchResults" runat="server" SelectMethod="FindBlogEntries" TypeName="EbalitWebForms.BusinessLogicLayer.BlogEntryDAL">
        <SelectParameters>
            <asp:QueryStringParameter Name="blogTopic" QueryStringField="blogTopic" Type="String" />
            <asp:QueryStringParameter Name="searchText" QueryStringField="searchText" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <div id="Title">
        >> Music Search Results
    </div>
    <div id="Container">
        <div id="LeftColumn">
        </div>
        <div id="MainColumn">
            <asp:DataList ID="dtlMusicSearchResults" runat="server" DataSourceID="odsMusicSearchResults" Width="775px" CellPadding="4" ForeColor="#333333">
                <AlternatingItemStyle BackColor="White" />
                <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                <ItemStyle BackColor="#FFFBD6" ForeColor="#333333" />
                <ItemTemplate>

                    <i>
                        <asp:LinkButton ID="lnkSearchResult" CommandArgument='<%# Eval("Id") %>' runat="server" OnCommand="lnkSearchResult_Command"><%# Eval("Subject")%>, published on <%# Eval("PublishedOn","{0:d}")%></asp:LinkButton>

                    </i>
                    <br />
                    <asp:Label ID="ContentLabel" runat="server" Text='<%# Eval("Content") %>' />
                    <br />

                </ItemTemplate>
                <SelectedItemStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
            </asp:DataList>
        </div>
        <div id="RightColumn">
            <div id="Search" class="partlet">
                <asp:Table ID="tblSearch" runat="server">
                    <asp:TableHeaderRow>
                        <asp:TableHeaderCell ColumnSpan="2" HorizontalAlign ="Left">
                            Search
                        </asp:TableHeaderCell>
                    </asp:TableHeaderRow>
                   <asp:TableRow>
                       <asp:TableCell>
                           <asp:TextBox ID="txtSearch" Width="160px"  runat="server"></asp:TextBox>
                       </asp:TableCell>
                       <asp:TableCell>
                           <asp:Button ID="btnSearch" CssClass="Button" runat="server" Text="Search" CausesValidation="false" OnClick="btnSearch_Click"/>
                       </asp:TableCell>
                   </asp:TableRow>
                    
                </asp:Table>
                
            </div>
        </div>
    </div>
</asp:Content>
