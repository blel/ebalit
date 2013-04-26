<%@ Page Title="" Language="C#" MasterPageFile="AdminMaster.master" AutoEventWireup="true" CodeBehind="CreateBlogEntry.aspx.cs" Inherits="EbalitWebForms.GUI.ProtectedSites.WebForm1" %>

<asp:Content runat="server" ContentPlaceHolderID="AdminContent" ID="ThisContent">

    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DataObjectTypeName="EbalitWebForms.DataLayer.BlogEntry" DeleteMethod="DeleteBlogEntry" InsertMethod="CreateBlogEntry" SelectMethod="GetBlogEntry" TypeName="EbalitWebForms.BusinessLogicLayer.BlogEntryDAL" UpdateMethod="UpdateBlogEntry" OnInserted="ObjectDataSource1_Inserted" OnSelecting="ObjectDataSource1_Selecting">
        <SelectParameters>
            <asp:Parameter DefaultValue="0" Name="Id" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsBlogTopic" runat="server" SelectMethod="ReadBlogTopic" TypeName="EbalitWebForms.BusinessLogicLayer.BlogTopicDAL" DataObjectTypeName="EbalitWebForms.DataLayer.BlogTopic" DeleteMethod="DeleteBlogTopic" InsertMethod="CreateBlogTopic" UpdateMethod="UpdateBlogTopic">
        <DeleteParameters>
            <asp:Parameter Name="BlogTopicID" Type="Int32" />
        </DeleteParameters>
        <UpdateParameters>
            <asp:Parameter Name="BlogTopicID" Type="Int32" />
        </UpdateParameters>
    </asp:ObjectDataSource>
    <div id="Container">
        <asp:DetailsView ID="DetailsView1" runat="server" Height="50px" Width="1395px" AutoGenerateRows="False" DataSourceID="ObjectDataSource1" GridLines="None" DataKeyNames="Id" OnItemInserting="DetailsView1_ItemInserting1" OnItemUpdating="DetailsView1_ItemUpdating">
            <EmptyDataTemplate>
                No data found.
            <br />
                <asp:Button ID="btnNew" runat="server" Text="New" CommandName="New" />
            </EmptyDataTemplate>
            <Fields>
                <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" Visible="False" />
                <asp:TemplateField HeaderText="Topic">
                    <InsertItemTemplate>
                        <asp:DropDownList ID="ddlTopic" runat="server" DataSourceID="odsBlogTopic" DataTextField="Topic" DataValueField="Id" Width="150px" OnTextChanged="ddlTopic_TextChanged" AutoPostBack="True"></asp:DropDownList>
                    </InsertItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblTopic" runat="server" Text='<%#Bind("BlogCategory.BlogTopic.Topic") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlTopic" runat="server"  DataSourceID="odsBlogTopic" SelectedValue='<%#Eval("BlogCategory.BlogTopic.Id") %>' DataTextField="Topic" DataValueField="Id" Width="150px" OnTextChanged="ddlTopic_TextChanged" AutoPostBack="True"></asp:DropDownList>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Category">
                    <InsertItemTemplate>
                        <asp:DropDownList ID="ddlCategory" runat="server" DataTextField="Category" DataValueField="Id" SelectedValue='<%# Bind("Category") %>' Width="150px" OnDataBinding="ddlCategory_DataBinding"></asp:DropDownList>
                    </InsertItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblCategory" runat="server" Text='<%#Bind("BlogCategory.Category") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlCategory" runat="server" DataTextField="Category" DataValueField="Id" SelectedValue='<%# Bind("Category") %>' Width="150px" OnDataBinding="ddlCategory_DataBinding"></asp:DropDownList>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Subject" HeaderText="Subject" SortExpression="Subject" ControlStyle-Width="1000px">
                    <ControlStyle Width="1000px"></ControlStyle>
                </asp:BoundField>
                <asp:TemplateField HeaderText="Content">
                    <InsertItemTemplate>
                        <asp:TextBox ID="txtContent" runat="server" Text='<%#Bind("Content") %>' Width="1000" Height="400" TextMode="MultiLine"></asp:TextBox>
                    </InsertItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtContent" runat="server" Text='<%#Bind("Content") %>' Width="1000" Height="400" TextMode="MultiLine"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblContent" runat="server" Text='<%#Bind("Content") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="PublishedOn" SortExpression="PublishedOn">
                    <EditItemTemplate>

                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("PublishedOn", "{0:dd.MM.yyyy}") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <InsertItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("PublishedOn", "{0:dd.MM.yyyy}") %>'></asp:TextBox>
                    </InsertItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("PublishedOn", "{0:dd.MM.yyyy}") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField ShowInsertButton="True" ShowDeleteButton="True" ShowEditButton="True" />
            </Fields>
        </asp:DetailsView>
    </div>
</asp:Content>
