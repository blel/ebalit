<%@ Page Title="" Language="C#" MasterPageFile="~/GUI/Master.Master" AutoEventWireup="true" CodeBehind="AllSearchResult.aspx.cs" Inherits="EbalitWebForms.GUI.WebForm10" %>

<%@ Register Src="~/GUI/WebUserControls/SearchUserControl.ascx" TagPrefix="uc1" TagName="SearchUserControl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <asp:ObjectDataSource ID="odsAllSearchResults" runat="server" SelectMethod="FindBlogEntries" TypeName="EbalitWebForms.BusinessLogicLayer.BlogEntryDAL">
        <SelectParameters>
            <asp:QueryStringParameter Name="searchText" QueryStringField="searchText" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <div id="TitleContainer">
        <div id="Title" class="pageHeader">
            >> Search Results
        </div>
    </div>
    
    <div id="Container">
        <div id="LeftColumn"></div>
        <div id="MainColumn">
            <asp:DataList ID="dtlAllSearchResults" runat="server" DataSourceID="odsAllSearchResults" Width="775px" CellPadding="4" ForeColor="#333333">
                <AlternatingItemStyle BackColor="White" />
                <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                <ItemStyle BackColor="#FFFBD6" ForeColor="#333333" />
                <ItemTemplate>
                    <i>
                        <asp:LinkButton ID="lnkSearchResult" CommandArgument='<%#Eval("BlogCategory.BlogTopic.PageName").ToString() + "?BlogEntryID=" +  Eval("Id") %>' runat="server" OnCommand="lnkSearchResult_Command"><%# Eval("Subject")%>, published on <%# Eval("PublishedOn","{0:d}")%></asp:LinkButton>
                        (<asp:Label ID="lblTopic" runat="server" Text='<%# Eval("BlogCategory.BlogTopic.Topic") %>' />)
                    </i>
                    
                   
                    <br />
                    <asp:Label ID="ContentLabel" runat="server" Text='<%# Eval("Content") %>' />
                    <br />
                </ItemTemplate>
                
                <SelectedItemStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
            </asp:DataList>
        </div>
        <div id="RightColumn">
            <uc1:SearchUserControl runat="server" ID="SearchUserControl" OnSearchButtonClick="SearchUserControl_SearchButtonClick"/>
        </div>
    </div>
</asp:Content>
