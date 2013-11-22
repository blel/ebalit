<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CategoryBrowser.ascx.cs" Inherits="EbalitWebForms.GUI.WebUserControls.CategoryBrowser" %>
<div id="CategoryBrowser" class="partlet">
    <h3>Categories</h3>
    <asp:ScriptManager ID="smgAccordion" runat="server">
    </asp:ScriptManager>

    <asp:HiddenField ID="hdfSelectedPane" runat="server" />
    <ajaxToolkit:Accordion ID="Accordion" runat="server">
    </ajaxToolkit:Accordion>
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
        var accordion = $find("Content_CategoryBrowser_Accordion_AccordionExtender");
        accordion.add_selectedIndexChanged(selectedIndexChanged);
    }

    function selectedIndexChanged(sender, args) {
        var hiddenField = document.getElementById("<%=hdfSelectedPane.ClientID%>");
        hiddenField.value = args.get_selectedIndex();
    }
</script>
