<%@ Page Title="" Language="C#" MasterPageFile="AdminMaster.master" AutoEventWireup="true" CodeBehind="BlogEntries.aspx.cs" Inherits="EbalitWebForms.GUI.ProtectedSites.WebForm3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
    <asp:ObjectDataSource ID="odsBlogEntries" runat="server" DataObjectTypeName="EbalitWebForms.DataLayer.BlogEntry" DeleteMethod="DeleteBlogEntry" InsertMethod="CreateBlogEntry" SelectMethod="GetBlogEntries" TypeName="EbalitWebForms.BusinessLogicLayer.BlogEntryDAL" UpdateMethod="UpdateBlogEntry" OnDeleting="odsBlogEntries_Deleting" OnSelected="odsBlogEntries_Selected">
        <DeleteParameters>
            <asp:Parameter Name="Id" Type="Int32" />
        </DeleteParameters>
        <UpdateParameters>
            <asp:Parameter Name="ID" Type="Int32" />
        </UpdateParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsTopics" runat="server" DataObjectTypeName="EbalitWebForms.DataLayer.BlogTopic" DeleteMethod="DeleteBlogTopic" InsertMethod="CreateBlogTopic" SelectMethod="ReadBlogTopic" TypeName="EbalitWebForms.BusinessLogicLayer.BlogTopicDAL" UpdateMethod="UpdateBlogTopic">
        <DeleteParameters>
            <asp:Parameter Name="BlogTopicID" Type="Int32" />
        </DeleteParameters>
        <UpdateParameters>
            <asp:Parameter Name="BlogTopicID" Type="Int32" />
        </UpdateParameters>
    </asp:ObjectDataSource>
    <div id="Container">
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataSourceID="odsBlogEntries" OnDataBinding="GridView1_DataBinding" DataKeyNames="Id" OnRowEditing="GridView1_RowEditing" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnSelectedIndexChanging="GridView1_SelectedIndexChanging" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical" Width="1300px">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:CommandField ShowDeleteButton="True" ShowSelectButton="True" />
                <asp:BoundField DataField="BlogCategory.Category" HeaderText="Category" SortExpression="Category" />
                <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" Visible="False" />
                <asp:BoundField DataField="Subject" HeaderText="Subject" SortExpression="Subject" />

                <asp:BoundField DataField="PublishedOn" HeaderText="PublishedOn" SortExpression="PublishedOn" DataFormatString="{0:dd.MM.yyyy}" />
            </Columns>
            <EmptyDataTemplate>
                No Data.
            </EmptyDataTemplate>
            <FooterStyle BackColor="#CCCC99" />
            <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
            <RowStyle BackColor="#F7F7DE" />
            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#FBFBF2" />
            <SortedAscendingHeaderStyle BackColor="#848384" />
            <SortedDescendingCellStyle BackColor="#EAEAD3" />
            <SortedDescendingHeaderStyle BackColor="#575357" />
        </asp:GridView>
    </div>
</asp:Content>
