<%@ Page Title="" Language="C#" MasterPageFile="~/GUI/ProtectedSites/ToolsMaster.master" AutoEventWireup="true" CodeBehind="CreateWorkingReport.aspx.cs" Inherits="EbalitWebForms.GUI.WorkingReport.CreateWorkingReport" %>

<%@ Register Assembly="EbalitWebForms" Namespace="EbalitWebForms.Common" TagPrefix="cc1" %>
<%@ Register Src="~/GUI/WebUserControls/TimeControl.ascx" TagPrefix="uc1" TagName="TimeControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ToolsContent" runat="server">
    <asp:ObjectDataSource ID="odsProject" runat="server" SelectMethod="GetProjects" TypeName="EbalitWebForms.BusinessLogicLayer.WorkingReport.WorkingReportBll" ></asp:ObjectDataSource>
   
    <asp:ObjectDataSource ID="odsWorkingReport" runat="server" SelectMethod="GetWorkingReport" TypeName="EbalitWebForms.BusinessLogicLayer.WorkingReport.WorkingReportBll" DataObjectTypeName="EbalitWebForms.DataLayer.ProjectWorkingReport" DeleteMethod="DeleteWorkingReport" InsertMethod="CreateWorkingReport" UpdateMethod="UpdateWorkingReport" OnSelecting="odsWorkingReport_OnSelecting">
        <SelectParameters>
            <asp:Parameter Name="id" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsResource" runat="server" SelectMethod="GetResources" TypeName="EbalitWebForms.BusinessLogicLayer.WorkingReport.WorkingReportBll" OnSelecting="odsResource_OnSelecting">
        <SelectParameters>
            <asp:Parameter Name="projectId" Type="Int32"></asp:Parameter>
        </SelectParameters>
    </asp:ObjectDataSource>
    <cc1:HierarchicalTaskDataSource ID="htsTasks" runat="server"  OnSelecting="htsTasks_OnSelecting" ></cc1:HierarchicalTaskDataSource>
    <ajaxToolkit:ToolkitScriptManager ID="scmAjaxToolkit" runat="server" EnableScriptGlobalization="true" EnableScriptLocalization="true"></ajaxToolkit:ToolkitScriptManager>
    <div id="Container">
        <div id="LeftColumn">
        </div>
        <div id="MainColumn">
            <h2>Working Report Details</h2>
            <asp:DetailsView ID="dtvCreateWorkingReport" runat="server" CssClass="detailsview" Height="50px" Width="767px" AutoGenerateRows="False" DataSourceID="odsWorkingReport" DataKeyNames="Id" 
                OnItemInserting="dtvCreateWorkingReport_ItemInserting" OnDataBound="dtvCreateWorkingReport_OnDataBound" OnModeChanging="dtvCreateWorkingReport_OnModeChanging">
                <EmptyDataTemplate>
                    No data.
                </EmptyDataTemplate>
                <Fields>

                    <asp:TemplateField HeaderText="Project" SortExpression="ProjectId">
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlPRoject" Width="200" runat="server" DataSourceID="odsProject" DataTextField="Name" DataValueField="Id" SelectedValue='<%# Bind("ProjectId") %>' OnSelectedIndexChanged="ddlPRoject_OnSelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>

                        </EditItemTemplate>
                        <InsertItemTemplate>
                            <asp:DropDownList ID="ddlPRoject" Width="200" runat="server" DataSourceID="odsProject" DataTextField="Name" DataValueField="Id" SelectedValue='<%# Bind("ProjectId") %>' OnSelectedIndexChanged="ddlPRoject_OnSelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>

                        </InsertItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("ProjectId") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Resource" SortExpression="ResourceId">
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlResource" runat="server" SelectedValue='<%# Bind("ResourceId") %>' DataSourceID="odsResource" DataTextField="Name" DataValueField="Id"></asp:DropDownList>
                            
                        </EditItemTemplate>
                        <InsertItemTemplate>
                            <asp:DropDownList ID="ddlResource" runat="server" DataSourceID="odsResource" DataTextField="Name" DataValueField="Id" ></asp:DropDownList>
                        </InsertItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("ResourceId") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Task" SortExpression="TaskId">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtTask" Width="200" runat="server" ></asp:TextBox>
                            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="CommentsPopup" TargetControlID="txtTask"></ajaxToolkit:ModalPopupExtender>
                        </EditItemTemplate>
                        <InsertItemTemplate>
                            <asp:TextBox ID="txtTask" Width="200" runat="server"></asp:TextBox>
                            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="CommentsPopup" TargetControlID="txtTask"></ajaxToolkit:ModalPopupExtender>
                        </InsertItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label4" runat="server" Text='<%# Bind("TaskId") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Date" SortExpression="Date">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtDate" runat="server" Text='<%# Bind("From", "{0:d}") %>'></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDate"></ajaxToolkit:CalendarExtender>
                        </EditItemTemplate>
                        <InsertItemTemplate>
                            <asp:TextBox ID="txtDate" runat="server" Text='<%# Bind("From") %>'></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDate"></ajaxToolkit:CalendarExtender>
                        </InsertItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblDate" runat="server" Text='<%# Bind("From", "{0:d}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="From" SortExpression="From">
                        <EditItemTemplate>
                            <uc1:TimeControl runat="server" ID="FromTime" DisplayTime='<%# Bind("From") %>'/>
                        </EditItemTemplate>
                        <InsertItemTemplate>
                            <uc1:TimeControl runat="server" ID="FromTime" />
                        </InsertItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblFrom" runat="server" Text='<%# Bind("From") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="To" SortExpression="To">
                        <EditItemTemplate>
                            <uc1:TimeControl runat="server" ID="ToTime" DisplayTime='<%# Bind("To") %>' />
                        </EditItemTemplate>
                        <InsertItemTemplate>
                            <uc1:TimeControl runat="server" ID="ToTime"  />
                        </InsertItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label6" runat="server" Text='<%# Bind("To") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Notes" SortExpression="Notes">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox7" Width="200" Height="60" runat="server" TextMode="MultiLine" Text='<%# Bind("Notes") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <InsertItemTemplate>
                            <asp:TextBox ID="TextBox7" Width="200" Height="60" runat="server" TextMode="MultiLine" Text='<%# Bind("Notes") %>'></asp:TextBox>
                        </InsertItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label7" runat="server" Text='<%# Bind("Notes") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField ShowEditButton="True" ShowInsertButton="True" ControlStyle-CssClass="CommandButton" />
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
