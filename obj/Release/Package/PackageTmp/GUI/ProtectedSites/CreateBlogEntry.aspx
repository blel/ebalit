<%@ Page Title="" Language="C#" MasterPageFile="AdminMaster.master" AutoEventWireup="true" CodeBehind="CreateBlogEntry.aspx.cs" Inherits="EbalitWebForms.GUI.ProtectedSites.WebForm1" %>

<asp:Content runat="server" ContentPlaceHolderID="AdminContent" ID="ThisContent">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="true" EnableScriptLocalization="true"></asp:ScriptManager>
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
    <asp:HiddenField ID="hdfTextSelection" runat="server" />
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
                        <asp:DropDownList ID="ddlTopic" runat="server" DataSourceID="odsBlogTopic" SelectedValue='<%#Eval("BlogCategory.BlogTopic.Id") %>' DataTextField="Topic" DataValueField="Id" Width="150px" OnTextChanged="ddlTopic_TextChanged" AutoPostBack="True"></asp:DropDownList>
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
                        <input id="btnHtmlEncode" type="button" value="Html Encode" onclick="AssignSelectedTextToHiddenField()" /><br />
                        <asp:TextBox ID="txtContent" runat="server" Text='<%#Bind("Content") %>' Width="1000" Height="400" TextMode="MultiLine"></asp:TextBox>
                    </InsertItemTemplate>
                    <EditItemTemplate>
                        <input id="btnHtmlEncode" type="button" value="Html Encode" onclick="AssignSelectedTextToHiddenField()" /><br />
                        <asp:TextBox ID="txtContent" runat="server" Text='<%#Bind("Content") %>' Width="1000" Height="400" TextMode="MultiLine"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblContent" runat="server" Text='<%#Bind("Content") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="PublishedOn" SortExpression="PublishedOn">
                    <EditItemTemplate>

                        <asp:TextBox ID="txtPublishedOn" runat="server" Text='<%# Bind("PublishedOn", "{0:dd.MM.yyyy}") %>'></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtPublishedOn"></ajaxToolkit:CalendarExtender>
                    </EditItemTemplate>
                    <InsertItemTemplate>
                        <asp:TextBox ID="txtPublishedOn" runat="server" Text='<%# Bind("PublishedOn", "{0:dd.MM.yyyy}") %>'></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtPublishedOn"></ajaxToolkit:CalendarExtender>

                    </InsertItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("PublishedOn", "{0:dd.MM.yyyy}") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField ShowInsertButton="True" ShowDeleteButton="True" ShowEditButton="True" />
            </Fields>
            <HeaderTemplate>
            </HeaderTemplate>
        </asp:DetailsView>
    </div>
    <script type="text/javascript">
        /*copied from http://stackoverflow.com/questions/7186586/how-to-get-the-selected-text-in-textarea-using-jquery-in-internet-explorer-7*/

        function getInputSelection(el) {
            var start = 0, end = 0, normalizedValue, range,
                textInputRange, len, endRange;

            if (typeof el.selectionStart == "number" && typeof el.selectionEnd == "number") {
                start = el.selectionStart;
                end = el.selectionEnd;
            } else {
                range = document.selection.createRange();

                if (range && range.parentElement() == el) {
                    len = el.value.length;
                    normalizedValue = el.value.replace(/\r\n/g, "\n");

                    // Create a working TextRange that lives only in the input
                    textInputRange = el.createTextRange();
                    textInputRange.moveToBookmark(range.getBookmark());

                    // Check if the start and end of the selection are at the very end
                    // of the input, since moveStart/moveEnd doesn't return what we want
                    // in those cases
                    endRange = el.createTextRange();
                    endRange.collapse(false);

                    if (textInputRange.compareEndPoints("StartToEnd", endRange) > -1) {
                        start = end = len;
                    } else {
                        start = -textInputRange.moveStart("character", -len);
                        start += normalizedValue.slice(0, start).split("\n").length - 1;

                        if (textInputRange.compareEndPoints("EndToEnd", endRange) > -1) {
                            end = len;
                        } else {
                            end = -textInputRange.moveEnd("character", -len);
                            end += normalizedValue.slice(0, end).split("\n").length - 1;
                        }
                    }
                }
            }

            return {
                start: start,
                end: end
            };

        }

        //Html encodes the selected text
        function AssignSelectedTextToHiddenField() {
            var txtContent = document.getElementById("<%= DetailsView1.FindControl("txtContent")!=null?DetailsView1.FindControl("txtContent").ClientID:null%>");
            var selectedText = getInputSelection(txtContent);

            var theText = txtContent.value.substring(selectedText.start, selectedText.end);

            theText = htmlEncode(theText);
            txtContent.value = txtContent.value.substring(0, selectedText.start) + theText + txtContent.value.substring(selectedText.end, txtContent.value.length);
        }

        /*http://stackoverflow.com/questions/1219860/javascript-jquery-html-encoding*/
        function htmlEncode(value) {
            //create a in-memory div, set it's inner text(which jQuery automatically encodes)
            //then grab the encoded contents back out.  The div never exists on the page.
            return $('<div/>').text(value).html();
        }

        function htmlDecode(value) {
            return $('<div/>').html(value).text();
        }

    </script>
</asp:Content>
