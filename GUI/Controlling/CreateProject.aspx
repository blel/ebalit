<%@ Page Title="" Language="C#" MasterPageFile="~/GUI/ProtectedSites/ToolsMaster.master" AutoEventWireup="true" CodeBehind="CreateProject.aspx.cs" Inherits="EbalitWebForms.GUI.Controlling.CreateProject" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ToolsContent" runat="server">
    <asp:EntityDataSource ID="edsControllingDataSource" runat="server" OnContextCreating="edsControllingDataSource_ContextCreating"
        EntitySetName="Projects" EnableInsert="true"></asp:EntityDataSource>
        <div id="Container">
        <div id="LeftColumn">

            Create Project</div>
        <div id="MainColumn">
            <asp:DetailsView ID="dvwProject" runat="server" Height="50px" Width="768px" AutoGenerateRows="False" DataSourceID="edsControllingDataSource">
                <Fields>
                    <asp:BoundField DataField="Name" HeaderText="Project Name" />
                    <asp:BoundField DataField="Code" HeaderText="Project Code" />
                    <asp:CommandField ShowInsertButton="True" />
                </Fields>
            </asp:DetailsView>
        </div>
        <div id="RightColumn">

        </div>
    </div>
</asp:Content>
