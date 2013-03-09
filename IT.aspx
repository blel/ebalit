<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="IT.aspx.cs" Inherits="EbalitWebForms.WebForm3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <h2>IT
    </h2>
    <asp:ScriptManager ID="smgAccordion" runat="server">
    </asp:ScriptManager>
    <asp:ObjectDataSource ID="odsBlogEntry" runat="server" SelectMethod="GetBlogEntry" TypeName="EbalitWebForms.BlogEntryDAL" OnSelecting="odsBlogEntry_Selecting" OnSelected="odsBlogEntry_Selected">
        <SelectParameters>
            <asp:Parameter DefaultValue="0" Name="Id" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <div id="Menu" style="float:left; width:300px">
        <ajaxToolkit:Accordion ID="Accordion" runat="server">
        </ajaxToolkit:Accordion>
    </div>
    <asp:HiddenField ID="hdfSelectedPane" runat="server" />
    <div id="MainContent">
        <asp:DetailsView ID="dvwEntry" runat="server" Height="50px" Width="125px" AutoGenerateRows="False" DataSourceID="odsBlogEntry">
            <Fields>
                <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" />
                <asp:BoundField DataField="Subject" HeaderText="Subject" SortExpression="Subject" HtmlEncode="False" />
                <asp:BoundField DataField="Content" HeaderText="Content" SortExpression="Content" HtmlEncode="False" />
                <asp:BoundField DataField="PublishedOn" HeaderText="PublishedOn" SortExpression="PublishedOn" />
                <asp:BoundField DataField="Category" HeaderText="Category" SortExpression="Category" />
            </Fields>
        </asp:DetailsView>
    </div>

    <!-- 
    honestly, I don't like stuff like the script below.
    what id does: it searches the Accordion,and adds a selected Index chaned event handler.
     
    When the selected index is changed, the new index is saved in a hidden field.
    When a postback occurs, the value of the hidden field is retrieved and the correct
    pane is activated again...       
        -->
    <script type="text/javascript">

        function pageLoad() {
            var accordion = $find("Content_Accordion_AccordionExtender");
            accordion.add_selectedIndexChanged(selectedIndexChanged);
        }

        function selectedIndexChanged(sender, args) {
            var hiddenField = document.getElementById("<%=hdfSelectedPane.ClientID%>");
            hiddenField.value = args.get_selectedIndex();
        }
    </script>
</asp:Content>
