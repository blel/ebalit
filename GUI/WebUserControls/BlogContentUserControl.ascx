<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BlogContentUserControl.ascx.cs" Inherits="EbalitWebForms.GUI.WebUserControls.BlogContentUserControl" %>

<asp:ObjectDataSource ID="odsBlogEntry" runat="server" SelectMethod="GetBlogEntry" TypeName="EbalitWebForms.BusinessLogicLayer.BlogEntryDAL" OnSelecting="odsBlogEntry_Selecting">
    <SelectParameters>
        <asp:Parameter DefaultValue="0" Name="Id" Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>
<asp:LinkButton ID="btnEdit" runat="server" CssClass="CommentButton" OnClick="btnEdit_Click" CausesValidation="false">Edit</asp:LinkButton>

<div id="BlogContent">

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
<div id="ButtonZone" runat="server" class="ButtonZone">
    <asp:Label ID="lblShowPopup" runat="server" Text="Comment" CssClass="CommentButton" ></asp:Label>
</div>
<!-- comments -->
<div id="Comments">
    <asp:ObjectDataSource ID="odsBlogComments" runat="server" SelectMethod="GetBlogComments" TypeName="EbalitWebForms.BusinessLogicLayer.BlogCommentBLL" OnSelecting="odsBlogComments_Selecting">
        <SelectParameters>
            <asp:Parameter Name="blogEntryId" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:DataList ID="dtlComments" runat="server" DataSourceID="odsBlogComments" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical" Width="764px" Style="margin-right: 0px">
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
<ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="lblShowPopup" PopupControlID="Popup"></ajaxToolkit:ModalPopupExtender>

<div id="Popup" runat="server" class="Popup">
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
        <FooterStyle BorderStyle="None"  Height="40"/>
        <FooterTemplate>
            <asp:LinkButton CssClass="CommentButton" ID="lnkSend" runat="server" OnCommand="lnkSend_Command">Send</asp:LinkButton>
            <asp:LinkButton CssClass="CommentButton" ID="lnkClose" runat="server" CausesValidation="false">Close</asp:LinkButton>
        </FooterTemplate>
    </asp:DetailsView>
</div>
