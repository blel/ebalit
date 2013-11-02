<%@ Page Title="" Language="C#" MasterPageFile="~/GUI/ProtectedSites/ToolsMaster.master" AutoEventWireup="true" CodeBehind="CreateWorkingReport.aspx.cs" Inherits="EbalitWebForms.GUI.WorkingHours.CreateWorkingReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ToolsContent" runat="server">
    <asp:ObjectDataSource ID="odsWorkingHours" runat="server" DataObjectTypeName="MPSServiceLibrary.DataContracts.WorkingHours" DeleteMethod="DeleteWorkingHours" InsertMethod="AddWorkingHours" SelectMethod="GetWorkingHours" TypeName="WorkingHoursServiceClient" UpdateMethod="UpdateWorkingHours">
        <SelectParameters>
            <asp:Parameter DefaultValue="0" Name="id" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsTasks" runat="server" SelectMethod="GetTasks" TypeName="WorkingHoursServiceClient">
        <SelectParameters>
            <asp:Parameter DefaultValue="" Name="project" Type="Object" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsProjects" runat="server" SelectMethod="GetProjects" TypeName="ProjectSyncServiceClient"></asp:ObjectDataSource>
    <div id="Container">
        <div id="LeftColumn"></div>
        <div id="MainColumn">
            <asp:DetailsView ID="dtvWorkingReport" runat="server" Height="231px" Width="745px" AutoGenerateRows="False" CssClass="detailsview" DataSourceID="odsWorkingHours" DataKeyNames="Id">
                <EmptyDataTemplate>
                    No data.
                </EmptyDataTemplate>
                <Fields>
                    <asp:CommandField ShowEditButton="True" ShowInsertButton="True" ControlStyle-CssClass="CommandButton">
                        <ControlStyle CssClass="CommandButton"></ControlStyle>
                    </asp:CommandField>
                    <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" Visible="False" />
                    <asp:TemplateField HeaderText="Select Project">
                        <InsertItemTemplate>
                            <asp:DropDownList ID="ddlProjects" AppendDataBoundItems="true" runat="server" DataSourceID="odsProjects" DataTextField="Name" DataValueField="UniqueId" >
                                <asp:ListItem Text="--select an item--" Value=""></asp:ListItem>
                            </asp:DropDownList>

                        </InsertItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlProjects" AppendDataBoundItems="true" runat="server" DataSourceID="odsProjects" DataTextField="Name" DataValueField="UniqueId" >
                                <asp:ListItem Text="--select an item--" Value=""></asp:ListItem>
                            </asp:DropDownList>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Tasks">
                        <InsertItemTemplate>
                            <asp:DropDownList ID="ddlTasks" AppendDataBoundItems="true" runat="server" DataSourceID="odsTasks" DataTextField="Value" DataValueField="Key" SelectedValue='<%# Bind("Name")%>'>
                                <asp:ListItem Text="--select an item--" Value=""></asp:ListItem>
                            </asp:DropDownList>

                        </InsertItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlTasks" AppendDataBoundItems="true" runat="server" DataSourceID="odsTasks" DataTextField="Value" DataValueField="Key" SelectedValue='<%# Bind("Name")%>'>
                                <asp:ListItem Text="--select an item--" Value=""></asp:ListItem>
                            </asp:DropDownList>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="From">
                        <InsertItemTemplate>
                            <asp:TextBox ID="lblFrom" runat="server" Text='<%#Bind("From") %>'></asp:TextBox>
                        </InsertItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="lblFrom" runat="server" Text='<%#Bind("From") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="To">
                        <InsertItemTemplate>
                            <asp:TextBox ID="lblTo" runat="server" Text='<%#Bind("To") %>'></asp:TextBox>
                        </InsertItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="lblTo" runat="server" Text='<%#Bind("To") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Notes">
                        <InsertItemTemplate>
                            <asp:TextBox ID="lblNotes" runat="server" Text='<%#Bind("Notes") %>'></asp:TextBox>
                        </InsertItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="lblNotes" runat="server" Text='<%#Bind("Notes") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                </Fields>
            </asp:DetailsView>
        </div>
        <div id="RightColumn"></div>
    </div>
</asp:Content>
