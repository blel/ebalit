<%@ Page Title="" Language="C#" MasterPageFile="~/GUI/ProtectedSites/ToolsMaster.master" AutoEventWireup="true" CodeBehind="CreateProject.aspx.cs" Inherits="EbalitWebForms.GUI.WorkingReport.CreateProject" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ToolsContent" runat="server">
    <asp:ObjectDataSource ID="odsProjects" runat="server" 
        TypeName="EbalitWebForms.BusinessLogicLayer.Repository`1[[EbalitWebForms.DataLayer.ProjectProject, EbalitWebForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], EbalitWebForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" 
        OldValuesParameterFormatString="original_{0}" SelectMethod="GetItemById" 
        DataObjectTypeName="EbalitWebForms.DataLayer.ProjectProject" DeleteMethod="DeleteItem" InsertMethod="CreateItem" UpdateMethod="UpdateItem"
        OnInserting="odsProjects_OnInserting" OnSelecting="odsProjects_OnSelecting" OnInserted="odsProjects_OnInserted" OnUpdated="odsProjects_OnUpdated">
        <SelectParameters>
            <asp:Parameter Name="id" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <div id="Container">
        <div id="LeftColumn">
        </div>
        <div id="MainColumn">
            <h2>Project Details</h2>
            <asp:DetailsView ID="dtvProject" CssClass="detailsview" runat="server" Height="50px" Width="769px" 
                AutoGenerateRows="False" DataSourceID="odsProjects" DataKeyNames="Id" OnModeChanging="dtvProject_OnModeChanging">
                <Fields>
                    <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" Visible="False" />
                    <asp:TemplateField HeaderText="Name">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtName" runat="server" Width="300" Text='<%# Bind("Name") %>'></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtName" ErrorMessage="Name is required."></asp:RequiredFieldValidator>
                        </EditItemTemplate>
                        <InsertItemTemplate>
                            <asp:TextBox ID="txtName" runat="server" Width="300" Text='<%# Bind("Name") %>'></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtName" ErrorMessage="Name is required."></asp:RequiredFieldValidator>
                        </InsertItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField ShowEditButton="True" ShowInsertButton="True" ControlStyle-CssClass="CommandButton" />
                </Fields>
            </asp:DetailsView>
        </div>
        <div id="RightColumn"></div>
    </div>
</asp:Content>

