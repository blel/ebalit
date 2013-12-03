<%@ Page Title="" Language="C#" MasterPageFile="Master.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="EbalitWebForms.GUI.WebForm1" %>

<%@ Register Src="~/GUI/WebUserControls/Archive.ascx" TagPrefix="uc1" TagName="Archive" %>
<%@ Register Src="~/GUI/WebUserControls/CategoryBrowser.ascx" TagPrefix="uc1" TagName="CategoryBrowser" %>
<%@ Register Src="~/GUI/WebUserControls/BlogContentUserControl.ascx" TagPrefix="uc1" TagName="BlogContentUserControl" %>
<%@ Register Src="~/GUI/WebUserControls/SearchUserControl.ascx" TagPrefix="uc1" TagName="SearchUserControl" %>
<%@ Register Src="~/GUI/WebUserControls/TimeControl.ascx" TagPrefix="uc1" TagName="TimeControl" %>





<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <div id="TitleContainer">
        <div id="Title" class="pageHeader">
            >> Home
        </div>
    </div>
    <div id="Container">

        <div id="LeftColumn">
            <img runat="server" src="/WebResources/cutHead.gif"  style="margin-left:auto;margin-right:auto; display:block;"/>
            
            <div id="RecentBlogEntries" class="partlet">
                <h3>Recent posts</h3>
                <asp:ObjectDataSource ID="odsRecentEntries" runat="server" SelectMethod="GetRecentBlogEntries" TypeName="EbalitWebForms.BusinessLogicLayer.BlogEntryDAL">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="10" Name="count" Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <ul>
                    <asp:DataList ID="dtlRecentBlogEntries" runat="server" DataSourceID="odsRecentEntries">
                        <ItemTemplate>
                            <li>
                                <asp:LinkButton CssClass="MenuButton" ID="lnkButton" CommandArgument='<%#Eval("Id") %>' OnCommand="lnkButton_Command" runat="server"><%# Eval("Subject") %></asp:LinkButton>
                                <i>(<asp:Label ID="ContentLabel" runat="server" Text='<%# Eval("BlogCategory.BlogTopic.Topic") %>' />)</i>
                            </li>
                        </ItemTemplate>
                    </asp:DataList>
                </ul>
            </div>
        </div>
        <div id="MainColumn">
            <uc1:BlogContentUserControl runat="server" ID="BlogContentUserControl" BlogTopic="Home" />
        </div>
        <div id="RightColumn">
            <uc1:SearchUserControl runat="server" ID="SearchUserControl" OnSearchButtonClick="SearchUserControl_SearchButtonClick" />
            <uc1:Archive runat="server" ID="Archive" ItemsBusinessLayerObject="EbalitWebForms.BusinessLogicLayer.BlogEntryDAL"
                ItemType="EbalitWebForms.DataLayer.BlogEntry" SelectMethod="GetBlogEntriesGroupedByMonths" OnLinkButtonPressed="Archive_LinkButtonPressed" DisplayField="Subject" />
        </div>
    </div>
</asp:Content>
