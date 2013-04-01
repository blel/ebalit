<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="IT.aspx.cs" Inherits="EbalitWebForms.WebForm3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <h2>IT
    </h2>
    <asp:ScriptManager ID="smgAccordion" runat="server">
    </asp:ScriptManager>
    <asp:ObjectDataSource ID="odsBlogEntry" runat="server" SelectMethod="GetBlogEntry" TypeName="EbalitWebForms.BlogEntryDAL" OnSelecting="odsBlogEntry_Selecting">
        <SelectParameters>
            <asp:Parameter DefaultValue="0" Name="Id" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:HiddenField ID="hdfSelectedPane" runat="server" />
    <div id="Menu">
        <ajaxToolkit:Accordion ID="Accordion" runat="server">
        </ajaxToolkit:Accordion>
    </div>

    <div id="MainContent">
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
                        <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto" Width="800px" Height="400px">

                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("Content") %>'></asp:Label>

                        </asp:Panel>
                    </ItemTemplate>

                    <ItemStyle BorderStyle="None"></ItemStyle>
                </asp:TemplateField>


            </Fields>
            <HeaderStyle BorderStyle="None" />
        </asp:DetailsView>
    </div>
    <asp:ObjectDataSource ID="odsBlogComment" runat="server" DataObjectTypeName="EbalitWebForms.BlogComment" InsertMethod="CreateBlogComment" SelectMethod="GetBlogComment" TypeName="EbalitWebForms.BlogCommentBLL" >
        <SelectParameters>
            <asp:Parameter DefaultValue="0" Name="blogCommentId" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <div id="Popup" >
        <b runat="server">Comments</b>
        <asp:DetailsView ID="dvwBlogComment" runat="server" Height="50px" Width="600px" AutoGenerateRows="False" DataSourceID="odsBlogComment" BorderStyle="None" EmptyDataText="Thank you for your comment." GridLines="None"  OnItemInserting="dvwBlogComment_ItemInserting">
            <Fields>
                <asp:TemplateField HeaderText="Subject" SortExpression="Subject" ControlStyle-BorderStyle="None" ItemStyle-BorderStyle="None" FooterStyle-BorderStyle="None" HeaderStyle-BorderStyle="None">
                    <InsertItemTemplate>
                        <asp:TextBox ID="txtSubject" runat="server" Text='<%# Bind("Subject") %>' Width="300"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSubject" ErrorMessage="Please enter a subject."></asp:RequiredFieldValidator>
                    </InsertItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Content" SortExpression="Content" ItemStyle-BorderStyle="None" HeaderStyle-BorderStyle="None" FooterStyle-BorderStyle="None" ControlStyle-BorderStyle="None">
                    <InsertItemTemplate>
                        <asp:TextBox ID="txtContent" runat="server" Text='<%# Bind("Content") %>' Width="300" TextMode="MultiLine" Rows="10"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtContent" ErrorMessage="Please enter a content."></asp:RequiredFieldValidator>
                    </InsertItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Your Name" SortExpression="UserName" ItemStyle-BorderStyle="None" ControlStyle-BorderStyle="None" FooterStyle-BorderStyle="None" HeaderStyle-BorderStyle="None">
                    <InsertItemTemplate>
                        <asp:TextBox ID="txtUser" runat="server" Text='<%# Bind("UserName") %>' Width="300"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtUser" ErrorMessage="Please insert your user name."></asp:RequiredFieldValidator>
                    </InsertItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Your E-Mail" SortExpression="eMail" ItemStyle-BorderStyle="None" HeaderStyle-BorderStyle="None" FooterStyle-BorderStyle="None" ControlStyle-BorderStyle="None">
                    <InsertItemTemplate>
                        <asp:TextBox ID="txtEmail" runat="server" Text='<%# Bind("eMail") %>' Width="300"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtEmail" ErrorMessage="Please enter an e-Mail Address"></asp:RequiredFieldValidator>
                    </InsertItemTemplate>
                </asp:TemplateField>
            </Fields>
            <FooterStyle BorderStyle="None" />
            <FooterTemplate>
                <asp:LinkButton  ID="lnkSend" runat="server" OnCommand="lnkSend_Command">Send</asp:LinkButton>
                <asp:LinkButton ID="lnkClose" runat="server" CausesValidation="false">Close</asp:LinkButton>
            </FooterTemplate>
        </asp:DetailsView>
    </div>
    <div id="CommentArea">
    <asp:Label ID="lblShowPopup" runat="server" Text="Comment" CssClass="Button"></asp:Label>
        </div>
    <ajaxToolkit:PopupControlExtender ID="PopupControlExtender1" runat="server" TargetControlID="lblShowPopup" PopupControlID="Popup" OffsetX="200" OffsetY="-500"></ajaxToolkit:PopupControlExtender>
    <div id="RightMenu">

        <asp:ObjectDataSource ID="odsBlogComments" runat="server" SelectMethod="GetBlogComments" TypeName="EbalitWebForms.BlogCommentBLL" OnSelecting="odsBlogComments_Selecting">
            <SelectParameters>
                <asp:Parameter Name="blogEntryId" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:Panel ID="Panel2" runat="server" ScrollBars="Auto" Height="600px">
        <asp:DataList ID="dtlComments" runat="server" DataSourceID="odsBlogComments">
            <ItemTemplate>
                Comment from <b runat="server"> <asp:Label ID="Label4" runat="server" Text='<%# Eval("UserName") %>' /> </b> posted on <i runat="server"> <asp:Label ID="Label5" runat="server" Text='<%# Eval("PostedOn","{0:d}") %>' /></i>:
                <br />
                Subject: <asp:Label ID="ContentLabel" runat="server" Text='<%# Eval("Subject") %>' />               
                <br />
                <asp:Label ID="Label6" runat="server" Text='<%# Eval("Content") %>' />

                <br />
            </ItemTemplate>
        </asp:DataList>
            </asp:Panel>
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
