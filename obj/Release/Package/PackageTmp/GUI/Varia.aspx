<%@ Page Title="" Language="C#" MasterPageFile="Master.Master" AutoEventWireup="true" CodeBehind="Varia.aspx.cs" Inherits="EbalitWebForms.GUI.WebForm5" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
        <asp:ScriptManager ID="smgAccordion" runat="server">
    </asp:ScriptManager>
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
    <asp:HiddenField ID="hdfSelectedPane" runat="server" />
        <div id="Title">
        >>  Varia
    </div>
    <div id="Container">
        <div id="LeftColumn">
            <h2>Categories</h2>
            <ajaxToolkit:Accordion ID="Accordion" runat="server">
            </ajaxToolkit:Accordion>
        </div>
        <div id="MainColumn">
            <div id="ButtonZone">
                <asp:Label ID="lblShowPopup" runat="server" Text="Comment" CssClass="CommentButton"></asp:Label>
            </div>
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

            <!-- comments -->
            <div id="Comments">
                <asp:ObjectDataSource ID="odsBlogComments" runat="server" SelectMethod="GetBlogComments" TypeName="EbalitWebForms.BusinessLogicLayer.BlogCommentBLL" OnSelecting="odsBlogComments_Selecting">
                    <SelectParameters>
                        <asp:Parameter Name="blogEntryId" Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <asp:DataList ID="dtlComments" runat="server" DataSourceID="odsBlogComments" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical" Width="773px" Style="margin-right: 0px">
                    <AlternatingItemStyle BackColor="White" />
                    <FooterStyle BackColor="#CCCC99" />
                    <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                    <ItemStyle BackColor="#F7F7DE" />
                    <ItemTemplate>
                        Comment from <b id="B2" runat="server">
                            <asp:Label ID="Label4" runat="server" Text='<%# Eval("UserName") %>' />
                        </b>posted on <i id="I1" runat="server">
                            <asp:Label ID="Label5" runat="server" Text='<%# Eval("PostedOn","{0:d}") %>' /></i>:
                <br />
                        Subject:
                <asp:Label ID="ContentLabel" runat="server" Text='<%# Eval("Subject") %>' />
                        <br />
                        <asp:Label ID="Label6" runat="server" Text='<%# Eval("Content") %>' />

                        <br />
                    </ItemTemplate>
                    <SelectedItemStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                </asp:DataList>
            </div>
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
                           <asp:TextBox ID="txtSearch" Width="160px" runat="server"></asp:TextBox>
                       </asp:TableCell>
                       <asp:TableCell>
                           <asp:Button ID="btnSearch" CssClass="Button" runat="server" Text="Search" CausesValidation="false" OnClick="btnSearch_Click"/>
                       </asp:TableCell>
                   </asp:TableRow>
                    
                </asp:Table>
                
            </div>
        </div>
    <ajaxToolkit:PopupControlExtender ID="PopupControlExtender1" runat="server" TargetControlID="lblShowPopup" PopupControlID="Popup" OffsetX="-600" OffsetY="100"></ajaxToolkit:PopupControlExtender>
    <div id="Popup">
        <asp:ObjectDataSource ID="odsBlogComment" runat="server" DataObjectTypeName="EbalitWebForms.DataLayer.BlogComment" InsertMethod="CreateBlogComment" SelectMethod="GetBlogComment" TypeName="EbalitWebForms.BusinessLogicLayer.BlogCommentBLL">
            <SelectParameters>
                <asp:Parameter DefaultValue="0" Name="blogCommentId" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <h2 id="H1" runat="server">Comments</h2>
        <asp:DetailsView ID="dvwBlogComment" runat="server" Height="50px" Width="600px" AutoGenerateRows="False" DataSourceID="odsBlogComment" BorderStyle="None" EmptyDataText="Thank you for your comment." GridLines="None" OnItemInserting="dvwBlogComment_ItemInserting">
            <Fields>
                <asp:TemplateField HeaderText="Subject" SortExpression="Subject" ControlStyle-BorderStyle="NotSet" ItemStyle-BorderStyle="None" FooterStyle-BorderStyle="None" HeaderStyle-BorderStyle="None" HeaderStyle-CssClass="label">
                    <InsertItemTemplate>
                        <asp:TextBox ID="txtSubject" runat="server" Text='<%# Bind("Subject") %>' Width="300"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSubject" ErrorMessage="Please enter a subject."></asp:RequiredFieldValidator>
                    </InsertItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Content" SortExpression="Content" ItemStyle-BorderStyle="None" HeaderStyle-BorderStyle="None" FooterStyle-BorderStyle="None" ControlStyle-BorderStyle="NotSet">
                    <InsertItemTemplate>
                        <asp:TextBox ID="txtContent" runat="server" Text='<%# Bind("Content") %>' Width="300" TextMode="MultiLine" Rows="10"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtContent" ErrorMessage="Please enter a content."></asp:RequiredFieldValidator>
                    </InsertItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Your Name" SortExpression="UserName" ItemStyle-BorderStyle="None" ControlStyle-BorderStyle="NotSet" FooterStyle-BorderStyle="None" HeaderStyle-BorderStyle="None">
                    <InsertItemTemplate>
                        <asp:TextBox ID="txtUser" runat="server" Text='<%# Bind("UserName") %>' Width="300"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtUser" ErrorMessage="Please insert your user name."></asp:RequiredFieldValidator>
                    </InsertItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Your E-Mail" SortExpression="eMail" ItemStyle-BorderStyle="None" HeaderStyle-BorderStyle="None" FooterStyle-BorderStyle="None" ControlStyle-BorderStyle="NotSet">
                    <InsertItemTemplate>
                        <asp:TextBox ID="txtEmail" runat="server" Text='<%# Bind("eMail") %>' Width="300"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtEmail" ErrorMessage="Please enter an e-Mail Address"></asp:RequiredFieldValidator>
                    </InsertItemTemplate>
                </asp:TemplateField>
            </Fields>
            <FooterStyle BorderStyle="None" />
            <FooterTemplate>
                <asp:LinkButton CssClass="Button" ID="lnkSend" runat="server" OnCommand="lnkSend_Command">Send</asp:LinkButton>
                <asp:LinkButton CssClass="Button" ID="lnkClose" runat="server" CausesValidation="false">Close</asp:LinkButton>
            </FooterTemplate>
        </asp:DetailsView>
    </div>
    </div>
</asp:Content>
