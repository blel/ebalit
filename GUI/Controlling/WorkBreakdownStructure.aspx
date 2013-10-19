<%@ Page Title="" Language="C#" MasterPageFile="~/GUI/ProtectedSites/ToolsMaster.master" AutoEventWireup="true" CodeBehind="WorkBreakdownStructure.aspx.cs" Inherits="EbalitWebForms.GUI.Controlling.WorkBreakdownStructure" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ToolsContent" runat="server">
    <div id="Container">
        <div id="LeftColumn">

        </div>
        <div id="MainColumn">

            <asp:TreeView ID="trvProjectView" runat="server"  OnSelectedNodeChanged="trvProjectView_SelectedNodeChanged"  >
                <DataBindings>
                    <asp:TreeNodeBinding  TextField="Name" />
                </DataBindings>
                                <DataBindings>
                    <asp:TreeNodeBinding   TextField="Display" />
                </DataBindings>
            </asp:TreeView>

        </div>
        <div id="RightColumn">

        </div>
    </div>
</asp:Content>
