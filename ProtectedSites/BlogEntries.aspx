<%@ Page Title="" Language="C#" MasterPageFile="~/ProtectedSites/AdminMaster.master" AutoEventWireup="true" CodeBehind="BlogEntries.aspx.cs" Inherits="EbalitWebForms.ProtectedSites.WebForm3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
    <asp:ObjectDataSource ID="odsBlogEntries" runat="server" DataObjectTypeName="EbalitWebForms.BlogEntry" DeleteMethod="DeleteBlogEntry" InsertMethod="CreateBlogEntry" SelectMethod="GetBlogEntries" TypeName="EbalitWebForms.BlogEntryDAL" UpdateMethod="UpdateBlogEntry" OnDeleting="odsBlogEntries_Deleting" OnSelected="odsBlogEntries_Selected">
        <DeleteParameters>
            <asp:Parameter Name="Id" Type="Int32" />
        </DeleteParameters>
        <UpdateParameters>
            <asp:Parameter Name="ID" Type="Int32" />
        </UpdateParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsTopics" runat="server" DataObjectTypeName="EbalitWebForms.BlogTopic" DeleteMethod="DeleteBlogTopic" InsertMethod="CreateBlogTopic" SelectMethod="ReadBlogTopic" TypeName="EbalitWebForms.BlogTopicDAL" UpdateMethod="UpdateBlogTopic">
        <DeleteParameters>
            <asp:Parameter Name="BlogTopicID" Type="Int32" />
        </DeleteParameters>
        <UpdateParameters>
            <asp:Parameter Name="BlogTopicID" Type="Int32" />
        </UpdateParameters>
    </asp:ObjectDataSource>
    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataSourceID="odsBlogEntries" OnDataBinding="GridView1_DataBinding" DataKeyNames="Id" OnRowEditing="GridView1_RowEditing" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnSelectedIndexChanging="GridView1_SelectedIndexChanging">
        <Columns>
            <asp:CommandField ShowDeleteButton="True" ShowSelectButton="True" />
            <asp:BoundField DataField="BlogCategory.Category" HeaderText="Category" SortExpression="Category" />
            <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" Visible="False" />
            <asp:BoundField DataField="Subject" HeaderText="Subject" SortExpression="Subject" />
            <asp:BoundField DataField="Content" HeaderText="Content" SortExpression="Content" />
            <asp:BoundField DataField="PublishedOn" HeaderText="PublishedOn" SortExpression="PublishedOn" DataFormatString="{0:dd.MM.yyyy}" />
        </Columns>
        <EmptyDataTemplate>
            No Data.
        </EmptyDataTemplate>
    </asp:GridView>
</asp:Content>
