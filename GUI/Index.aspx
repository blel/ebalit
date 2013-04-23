<%@ Page Title="" Language="C#" MasterPageFile="Master.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="EbalitWebForms.GUI.WebForm1" %>
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
                        <asp:TemplateField SortExpression="PublishedOn" ItemStyle-Height="20px" ItemStyle-BorderStyle="None" ShowHeader="False">
                            <ItemTemplate>
                                <b>
                                    <asp:Label ID="header" runat="server" Text="Published On: "></asp:Label>
                                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("PublishedOn","{0:d}") %>'></asp:Label>
                                </b>
                            </ItemTemplate>
                            <ItemStyle BorderStyle="None" Height="20px"></ItemStyle>
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

        </div>
    </div>
</asp:Content>
