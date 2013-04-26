<%@ Page Title="" Language="C#" MasterPageFile="Master.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="EbalitWebForms.GUI.WebForm1" %>

<%@ Register Src="~/GUI/WebUserControls/Archive.ascx" TagPrefix="uc1" TagName="Archive" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <div id="Title">
        >>  Home
    </div>
    <div id="Container">
        <div id="LeftColumn">
        </div>
        <div id="MainColumn">
            <div id="BlogContent">
                <asp:ObjectDataSource ID="odsBlogEntry" runat="server" SelectMethod="GetBlogEntry" TypeName="EbalitWebForms.BusinessLogicLayer.BlogEntryDAL" OnSelecting="odsBlogEntry_Selecting">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="0" Name="Id" Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <asp:DetailsView ID="dvwEntry" runat="server" AutoGenerateRows="False" DataSourceID="odsBlogEntry" BorderStyle="None">
                    <Fields>
                        <asp:TemplateField SortExpression="Subject" ItemStyle-BorderStyle="None" ShowHeader="False">
                            <ItemTemplate>
                                <h2>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("Subject") %>'></asp:Label>
                                </h2>
                            </ItemTemplate>
                            <ItemStyle BorderStyle="None"></ItemStyle>
                        </asp:TemplateField>

                        <asp:TemplateField SortExpression="Content" ItemStyle-BorderStyle="None" ItemStyle-VerticalAlign="Top" ShowHeader="False">
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("Content") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle BorderStyle="None"></ItemStyle>
                        </asp:TemplateField>
                    </Fields>
                    <HeaderStyle BorderStyle="None" />
                </asp:DetailsView>
            </div>
        </div>
        <div id="RightColumn">
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
                                <asp:LinkButton ID="lnkButton" CommandArgument='<%#Eval("Id") %>' OnCommand="lnkButton_Command" runat="server"><%# Eval("Subject") %></asp:LinkButton>
                                <i>(<asp:Label ID="ContentLabel" runat="server" Text='<%# Eval("BlogCategory.BlogTopic.Topic") %>' />)</i>
                            </li>
                        </ItemTemplate>
                    </asp:DataList>
                </ul>
            </div>
            <div id="History" class="partlet">
                
                <uc1:Archive runat="server" id="Archive" ItemsBusinessLayerObject="EbalitWebForms.BusinessLogicLayer.BlogEntryDAL"
                    ItemType="EbalitWebForms.DataLayer.BlogEntry" SelectMethod="GetBlogEntriesGroupedByMonths"  OnLinkButtonPressed="Archive_LinkButtonPressed" DisplayField="Subject"/>
            </div>
        </div>
    </div>
</asp:Content>
