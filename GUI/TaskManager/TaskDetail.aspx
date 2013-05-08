<%@ Page Title="" Language="C#" MasterPageFile="~/GUI/ProtectedSites/ToolsMaster.Master" AutoEventWireup="true" CodeBehind="TaskDetail.aspx.cs" Inherits="EbalitWebForms.GUI.TaskManager.TaskDetail" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ToolsContent" runat="server">
    <asp:ScriptManager ID="ScriptManager" runat="server" EnableScriptGlobalization="true" EnableScriptLocalization="true"></asp:ScriptManager>
    <asp:ObjectDataSource ID="odsTasks" runat="server" DataObjectTypeName="EbalitWebForms.DataLayer.Task" DeleteMethod="DeleteTask" InsertMethod="CreateTask" SelectMethod="GetTaskById" TypeName="EbalitWebForms.BusinessLogicLayer.TaskBLL" UpdateMethod="UpdateTask" OnSelecting="odsTasks_Selecting">
        <SelectParameters>
            <asp:Parameter Name="id" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:XmlDataSource ID="xdsTaskStatus" runat="server" DataFile="~/Resources/MasterData.xml" XPath="/MasterData/DataDictionary[@Name='TaskStatus']/DictionaryItem"></asp:XmlDataSource>
    <asp:XmlDataSource ID="xdsTaskPriority" runat="server" DataFile="~/Resources/MasterData.xml" XPath="/MasterData/DataDictionary[@Name='TaskPriority']/DictionaryItem"></asp:XmlDataSource>
    <asp:XmlDataSource ID="xdsTaskClosingType" runat="server" DataFile="~/Resources/MasterData.xml" XPath="/MasterData/DataDictionary[@Name='ClosingType']/DictionaryItem"></asp:XmlDataSource>
    <asp:ObjectDataSource ID="odsTaskCategories" runat="server" DataObjectTypeName="EbalitWebForms.DataLayer.TaskCategory" DeleteMethod="Delete" InsertMethod="Create" SelectMethod="GetTaskCategories" TypeName="EbalitWebForms.BusinessLogicLayer.TaskCategoryBLL" UpdateMethod="Update"></asp:ObjectDataSource>


    <div id="Container">
        <div id="LeftColumn">
        </div>
        <div id="MainColumn">

            <asp:DetailsView ID="dtvTask" runat="server" AutoGenerateRows="False" DataSourceID="odsTasks" Height="50px" Width="761px" CssClass="detailsview" DataKeyNames="Id" OnItemInserting="dtvTask_ItemInserting" OnItemUpdating="dtvTask_ItemUpdating" OnDataBinding="dtvTask_DataBinding" OnItemInserted="dtvTask_ItemInserted" OnItemUpdated="dtvTask_ItemUpdated" OnModeChanging="dtvTask_ModeChanging">
                <Fields>
                    <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" Visible="False" />
                    <asp:TemplateField HeaderText="Subject" SortExpression="Subject">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtSubject" Width="500" runat="server" Text='<%# Bind("Subject") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <InsertItemTemplate>
                            <asp:TextBox ID="txtSubject" Width="500" runat="server" Text='<%# Bind("Subject") %>'></asp:TextBox>
                        </InsertItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblSubject" runat="server" Text='<%# Bind("Subject") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Content" SortExpression="Content">
                        <EditItemTemplate>
                             <asp:TextBox ID="txtContent" TextMode="MultiLine" Width="500" Height="300" runat="server" Text='<%# Bind("Content") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <InsertItemTemplate>
                            <asp:TextBox ID="txtContent" TextMode="MultiLine" Width="500" Height="300" runat="server" Text='<%# Bind("Content") %>'></asp:TextBox>
                        </InsertItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblContent" runat="server" Text='<%# Bind("Content") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="DueDate" SortExpression="DueDate">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtDueDate" Width="200" runat="server" Text='<%# Bind("DueDate", "{0:d}") %>'></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDueDate"></ajaxToolkit:CalendarExtender>
                        </EditItemTemplate>
                        <InsertItemTemplate>
                            <asp:TextBox ID="txtDueDate" Width="200"  runat="server" Text='<%# Bind("DueDate") %>'></asp:TextBox>
                             <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDueDate"></ajaxToolkit:CalendarExtender>
                        </InsertItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblDueDate" runat="server" Text='<%# Bind("DueDate") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Task Category" SortExpression="FK_TaskCategory">
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlTaskCategory" Width="200"  runat="server" DataSourceID="odsTaskCategories" DataTextField="TaskCategory1" DataValueField="Id" SelectedValue='<%#Bind("FK_TaskCategory") %>' AppendDataBoundItems="true">
                                <asp:ListItem Text="--select an item--" Value=""></asp:ListItem>
                            </asp:DropDownList>
                        </EditItemTemplate>
                        <InsertItemTemplate>
                            <asp:DropDownList ID="ddlTaskCategory" Width="200"  runat="server" DataSourceID="odsTaskCategories" DataTextField="TaskCategory1" DataValueField="Id" SelectedValue='<%#Bind("FK_TaskCategory") %>' AppendDataBoundItems="true">
                                <asp:ListItem Text="--select an item--" Value=""></asp:ListItem>
                            </asp:DropDownList>
                        </InsertItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblTaskCategory" runat="server" Text='<%# Eval("TaskCategory.TaskCategory1") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="State" SortExpression="State">
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlState" Width="200px"  runat="server" DataSourceID="xdsTaskStatus" DataTextField="Value" DataValueField="Value" SelectedValue='<%# Bind("State") %>'>
                            </asp:DropDownList>
                        </EditItemTemplate>
                        <InsertItemTemplate>
                            <asp:DropDownList ID="ddlState" Width="200px"  runat="server" DataSourceID="xdsTaskStatus" DataTextField="Value" DataValueField="Value" SelectedValue='<%# Bind("State") %>'>
                            </asp:DropDownList>
                        </InsertItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblState" runat="server" Text='<%# Bind("State") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Priority" SortExpression="Priority">
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlPriority" Width="200px"  runat="server" DataSourceID="xdsTaskPriority" DataTextField="Value" DataValueField="Value" SelectedValue='<%# Bind("Priority") %>'>
                            </asp:DropDownList>
                        </EditItemTemplate>
                        <InsertItemTemplate>
                            <asp:DropDownList ID="ddlPriority" Width="200px"  runat="server" DataSourceID="xdsTaskPriority" DataTextField="Value" DataValueField="Value" SelectedValue='<%# Bind("Priority") %>' >
                            </asp:DropDownList>
                        </InsertItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblPriority" runat="server" Text='<%# Bind("Priority") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Closing Type" SortExpression="ClosingType">
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlClosingType" Width="200"  runat="server" SelectedValue='<%# Bind("ClosingType") %>'
                                 DataSourceID="xdsTaskClosingType" DataTextField="Value" DataValueField="Value">
                            </asp:DropDownList>
                        </EditItemTemplate>
                        <InsertItemTemplate>
                            <asp:DropDownList ID="ddlClosingType" Width="200px"  runat="server" 
                                DataSourceID="xdsTaskClosingType" DataTextField="Value" DataValueField="Value" SelectedValue='<%# Bind("ClosingType") %>'>
                            </asp:DropDownList>
                        </InsertItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblClosingType" runat="server" Text='<%# Bind("ClosingType") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:CommandField ShowInsertButton="True" ShowDeleteButton="True" ShowEditButton="True" ControlStyle-CssClass="CommandButton" />

                </Fields>
            </asp:DetailsView>

        </div>
        <div id="RightColumn">
        </div>
    </div>
</asp:Content>


