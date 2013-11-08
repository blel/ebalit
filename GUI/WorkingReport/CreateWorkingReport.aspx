<%@ Page Title="" Language="C#" MasterPageFile="~/GUI/ProtectedSites/ToolsMaster.master" AutoEventWireup="true" CodeBehind="CreateWorkingReport.aspx.cs" Inherits="EbalitWebForms.GUI.WorkingReport.CreateWorkingReport" %>

<%@ Register Assembly="EbalitWebForms" Namespace="EbalitWebForms.Common" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ToolsContent" runat="server">
    <asp:ObjectDataSource ID="odsProject" runat="server" SelectMethod="GetProjects" TypeName="EbalitWebForms.BusinessLogicLayer.WorkingReport.WorkingReportBll"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsTasks" runat="server" SelectMethod="GetTasks" TypeName="EbalitWebForms.BusinessLogicLayer.WorkingReport.WorkingReportBll" OnSelecting="odsTasks_OnSelecting">
        <SelectParameters>
            <asp:Parameter Name="project" Type="Object" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsWorkingReport" runat="server" SelectMethod="GetWorkingReport" TypeName="EbalitWebForms.BusinessLogicLayer.WorkingReport.WorkingReportBll" DataObjectTypeName="EbalitWebForms.DataLayer.ProjectWorkingReport" DeleteMethod="DeleteWorkingReport" InsertMethod="CreateWorkingReport" UpdateMethod="UpdateWorkingReport">
        <SelectParameters>
            <asp:Parameter Name="id" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <cc1:HierarchicalTaskDataSource ID="htsTasks" runat="server" ProjectId="1"></cc1:HierarchicalTaskDataSource>
    <asp:ScriptManager ID="scmAjaxToolkit" runat="server"></asp:ScriptManager>
    <div id="Container">
        <div id="LeftColumn">
        </div>
        <div id="MainColumn">
            <h2>Working Report Details</h2>
            <asp:DetailsView ID="dtvCreateWorkingReport" runat="server" Height="50px" Width="767px" AutoGenerateRows="False" DataSourceID="odsWorkingReport" DataKeyNames="Id">
                <EmptyDataTemplate>
                    No data.
                </EmptyDataTemplate>
                <Fields>
                    <asp:TemplateField HeaderText="Id" SortExpression="Id">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Id") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <InsertItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Id") %>'></asp:TextBox>
                        </InsertItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("Id") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Project" SortExpression="ProjectId">
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlPRoject" runat="server" DataSourceID="odsProject" DataTextField="Name" DataValueField="Id" SelectedValue='<%# Bind("ProjectId") %>'></asp:DropDownList>

                        </EditItemTemplate>
                        <InsertItemTemplate>
                            <asp:DropDownList ID="ddlPRoject" runat="server" DataSourceID="odsProject" DataTextField="Name" DataValueField="Id" SelectedValue='<%# Bind("ProjectId") %>'></asp:DropDownList>

                        </InsertItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("ProjectId") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ResourceId" SortExpression="ResourceId">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("ResourceId") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <InsertItemTemplate>
                            <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("ResourceId") %>'></asp:TextBox>
                        </InsertItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("ResourceId") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="TaskId" SortExpression="TaskId">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtTask" runat="server" Text='<%# Bind("TaskId") %>'></asp:TextBox>
                            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="CommentsPopup" TargetControlID="txtTask"></ajaxToolkit:ModalPopupExtender>
                        </EditItemTemplate>
                        <InsertItemTemplate>
                            <asp:TextBox ID="txtTask" runat="server" Text='<%# Bind("TaskId") %>'></asp:TextBox>
                            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="CommentsPopup" TargetControlID="txtTask"></ajaxToolkit:ModalPopupExtender>
                        </InsertItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label4" runat="server" Text='<%# Bind("TaskId") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="From" SortExpression="From">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("From") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <InsertItemTemplate>
                            <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("From") %>'></asp:TextBox>
                        </InsertItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label5" runat="server" Text='<%# Bind("From") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="To" SortExpression="To">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("To") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <InsertItemTemplate>
                            <asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("To") %>'></asp:TextBox>
                        </InsertItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label6" runat="server" Text='<%# Bind("To") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Notes" SortExpression="Notes">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox7" runat="server" Text='<%# Bind("Notes") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <InsertItemTemplate>
                            <asp:TextBox ID="TextBox7" runat="server" Text='<%# Bind("Notes") %>'></asp:TextBox>
                        </InsertItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label7" runat="server" Text='<%# Bind("Notes") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField ShowEditButton="True" ShowInsertButton="True" />
                </Fields>
            </asp:DetailsView>
            <div id="CommentsPopup" class="Popup">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">

                    <ContentTemplate>
                        <asp:TreeView ID="trvTask" runat="server" DataSourceID="htsTasks" OnSelectedNodeChanged="trvTask_OnSelectedNodeChanged">
                            <DataBindings>
                                <asp:TreeNodeBinding DataMember="Task" TextField="Name" />
                            </DataBindings>
                        </asp:TreeView>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div id="Buttons">
                    <asp:LinkButton ID="lnkClose" runat="server" CssClass="CommandButton" CausesValidation="false">Close</asp:LinkButton>
                </div>
            </div>
        </div>
        <div id="RightColumn">
        </div>
    </div>
</asp:Content>
