<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Archive.ascx.cs" Inherits="EbalitWebForms.GUI.WebUserControls.Archive" %>
<asp:ScriptManager ID="smgAccordion" runat="server">
</asp:ScriptManager>

<script type="text/javascript">

    function pageLoad() {
        var accordion = $find("Content_Archive_Accordion_AccordionExtender");
        accordion.add_selectedIndexChanged(selectedIndexChanged);
    }

    function selectedIndexChanged(sender, args) {
        var hiddenField = document.getElementById("<%=hdfSelectedPane.ClientID%>");
            hiddenField.value = args.get_selectedIndex();
        }


   

    function StopBubblingAndDoPostBack(e) {
        var test = "test";
        if (!e) var e = window.event;
        e.cancelBubble = true;
        if (e.stopPropagation) e.stopPropagation();
        __doPostBack();
    }

   

</script>

<asp:HiddenField ID="hdfSelectedPane" runat="server" />
<h3>Archive</h3>
<ajaxToolkit:Accordion ID="Accordion" runat="server">

</ajaxToolkit:Accordion>


