<%@ Page Title="" Language="C#" MasterPageFile="~/GUI/ProtectedSites/ToolsMaster.Master" AutoEventWireup="true" CodeBehind="TaskList.aspx.cs" Inherits="EbalitWebForms.GUI.TaskManager.TaskList" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ToolsContent" runat="server">
    <asp:ScriptManager ID="ScriptManager" runat="server" EnableScriptGlobalization="true" EnableScriptLocalization="true"></asp:ScriptManager>
    <asp:ObjectDataSource ID="odsTasks" runat="server" DataObjectTypeName="EbalitWebForms.DataLayer.Task" DeleteMethod="DeleteTask" InsertMethod="CreateTask" SelectMethod="GetFilteredTasks" TypeName="EbalitWebForms.BusinessLogicLayer.TaskBLL" UpdateMethod="UpdateTask" OnSelecting="odsTasks_Selecting" OnDeleted="odsTasks_Deleted1">
        <SelectParameters>
            <asp:Parameter Name="filter" Type="Object" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsTaskCategories" runat="server" DataObjectTypeName="EbalitWebForms.DataLayer.TaskCategory" DeleteMethod="Delete" InsertMethod="Create" SelectMethod="GetTaskCategories" TypeName="EbalitWebForms.BusinessLogicLayer.TaskCategoryBLL" UpdateMethod="Update"></asp:ObjectDataSource>
    <asp:XmlDataSource ID="xdsTaskStatus" runat="server" DataFile="~/Resources/MasterData.xml" XPath="/MasterData/DataDictionary[@Name='TaskStatus']/DictionaryItem"></asp:XmlDataSource>
    <asp:XmlDataSource ID="xdsTaskPriority" runat="server" DataFile="~/Resources/MasterData.xml" XPath="/MasterData/DataDictionary[@Name='TaskPriority']/DictionaryItem"></asp:XmlDataSource>
    <asp:XmlDataSource ID="xdsTaskClosingType" runat="server" DataFile="~/Resources/MasterData.xml" XPath="/MasterData/DataDictionary[@Name='ClosingType']/DictionaryItem"></asp:XmlDataSource>
    <asp:ObjectDataSource ID="odsTaskComments" runat="server" DataObjectTypeName="EbalitWebForms.DataLayer.TaskComment" DeleteMethod="DeleteTaskComment" InsertMethod="CreateTaskComment" SelectMethod="GetTaskComments" TypeName="EbalitWebForms.BusinessLogicLayer.TaskComments" UpdateMethod="UpdateTaskComment" >
        <SelectParameters>
            <asp:ControlParameter ControlID="hdfSelectedTaskId" Name="taskID" PropertyName="Value" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:HiddenField ID="hdfSelectedTaskId" runat="server" />

    <div id="Container">
        
        <div id="Filter" >
            <asp:Table ID="Table1" runat="server">
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:Label ID="Label1" runat="server" Text="Text"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell ColumnSpan="3">
                        <asp:TextBox ID="txtFreeText" Width="285" runat="server"></asp:TextBox>
                    </asp:TableCell>

                    <asp:TableCell>
                        <asp:Label ID="Label2" runat="server" Text="Due Date from"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell>

                        <asp:TextBox ID="txtDateFrom" runat="server"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDateFrom"></ajaxToolkit:CalendarExtender>

                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:Label ID="Label4" runat="server" Text="To"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell>

                        <asp:TextBox ID="txtDateTo" runat="server"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDateTo"></ajaxToolkit:CalendarExtender>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:Label ID="Label5" runat="server" Text="Task Category"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:ListBox ID="ddlTaskCategory" SelectionMode="Multiple" Width="100" runat="server"  DataSourceID="odsTaskCategories" DataTextField="TaskCategory1" DataValueField="Id" AppendDataBoundItems="true">
                            
                        </asp:ListBox>
     
                    </asp:TableCell>

                    <asp:TableCell>
                        <asp:Label ID="Label6" runat="server" Text="Task Status"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:ListBox ID="ddlTaskStatus"  SelectionMode="Multiple" Width="100" runat="server" DataSourceID="xdsTaskStatus" DataTextField="Value" DataValueField="Value"></asp:ListBox>
                      
                    </asp:TableCell>

                    <asp:TableCell>
                        <asp:Label ID="Label7" runat="server" Text="Task Priority"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:ListBox ID="ddlPriority" SelectionMode="Multiple" runat="server" DataSourceID="xdsTaskPriority" DataTextField="Value" DataValueField="Value"></asp:ListBox>
                        
                    </asp:TableCell>

                    <asp:TableCell>
                        <asp:Label ID="Label3" runat="server" Text="Closing Type"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:ListBox ID="ddlClosingType" SelectionMode="Multiple" runat="server" DataSourceID="xdsTaskClosingType" DataTextField="Value" DataValueField="Value"></asp:ListBox>
                       
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableFooterRow>
                    <asp:TableCell>
                        <asp:LinkButton ID="lnkFind" runat="server" CausesValidation="false" OnCommand="lnkFind_Command" CssClass="CommandButton">Find</asp:LinkButton>&nbsp;
                        <asp:LinkButton ID="lnkClear" runat="server" CausesValidation="false" OnCommand="lnkClear_Command" CssClass="CommandButton">Clear</asp:LinkButton>&nbsp;
                        <asp:LinkButton ID="lnkCreate" runat="server" CausesValidation="false" OnCommand="lnkCreate_Command" CssClass="CommandButton">Create</asp:LinkButton>&nbsp;
                        <asp:LinkButton ID="lnkExport" runat="server" CausesValidation="false" OnCommand="lnkExport_Command" CssClass="CommandButton">Export</asp:LinkButton>
                    </asp:TableCell>
                </asp:TableFooterRow>
            </asp:Table>
        </div>
        <asp:ListView ID="lvwTasks" runat="server" DataSourceID="odsTasks" OnItemUpdating="lvwTasks_ItemUpdating" DataKeyNames="Id" OnItemDataBound="lvwTasks_ItemDataBound" >
            <AlternatingItemTemplate>
                <tr style="background-color: #FFF8DC;" id="TaskListRow" runat="server">
                    <td>
                        <asp:LinkButton ID="DeleteButton" runat="server" CommandName="Delete" Text="Delete" CssClass="CommandButton" />
                        <asp:LinkButton ID="EditButton" runat="server" CommandName="Edit" Text="Edit" CssClass="CommandButton" />
                        <asp:LinkButton ID="btnDetails" runat="server" OnCommand="btnDetails_Command" CommandArgument='<%# Eval("Id") %>' CssClass="CommandButton">Details</asp:LinkButton>
                        <asp:LinkButton ID="btnComments" runat="server" CssClass="CommandButton" CommandArgument='<%# Eval("Id") %>' OnCommand="btnComments_Command">Comments</asp:LinkButton>
                        <asp:Button ID="btnDummyBtn" runat="server" Text="Button" CssClass="hidden" />
                        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="CommentsPopup" TargetControlID="btnDummyBtn"></ajaxToolkit:ModalPopupExtender>


                    </td>
                    <td>
                        <asp:Label ID="SubjectLabel" runat="server" Text='<%# Eval("Subject") %>' />
                    </td>

                    <td>
                        <asp:Label ID="DueDateLabel" runat="server" Text='<%# Eval("DueDate","{0:d}") %>' />
                    </td>
                    <td>
                        <asp:Label ID="FK_TaskCategoryLabel" runat="server" Text='<%# Eval("TaskCategory.TaskCategory1") %>' />
                    </td>
                    <td>
                        <asp:Label ID="StateLabel" runat="server" Text='<%# Eval("State") %>' />
                    </td>
                    <td>
                        <asp:Label ID="PriorityLabel" runat="server" Text='<%# Eval("Priority") %>' />
                    </td>
                    <td>
                        <asp:Label ID="ClosingTypeLabel" runat="server" Text='<%# Eval("ClosingType") %>' />
                    </td>
                </tr>
            </AlternatingItemTemplate>
            <EditItemTemplate>
                <tr style="background-color: #003528; color: #FFFFFF;">
                    <td>
                        <asp:LinkButton ID="UpdateButton" runat="server" CommandName="Update" Text="Update" CssClass="CommandButton"/>
                        <asp:LinkButton ID="CancelButton" runat="server" CommandName="Cancel" Text="Cancel" CssClass="CommandButton"/>&nbsp;

                    </td>

                    <td>
                        <asp:TextBox Width="400px" ID="SubjectTextBox" runat="server" Text='<%# Bind("Subject") %>' />
                    </td>

                    <td>
                        <asp:TextBox ID="DueDateTextBox" runat="server" Text='<%# Bind("DueDate", "{0:d}") %>' />
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="DueDateTextBox"></ajaxToolkit:CalendarExtender>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlTaskCategory" DataSourceID="odsTaskCategories" DataTextField="TaskCategory1" DataValueField="Id" runat="server" AppendDataBoundItems="true" SelectedValue='<%# Bind("FK_TaskCategory") %>'>
                            <asp:ListItem Text="" Value=""></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlTaskStatus" DataSourceID="xdsTaskStatus" DataTextField="Value" DataValueField="Value" runat="server" AppendDataBoundItems="true" SelectedValue='<%# Bind("State") %>'>
                            <asp:ListItem Text="" Value=""></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlPriority" DataSourceID="xdsTaskPriority" DataTextField="Value" DataValueField="Value" runat="server" AppendDataBoundItems="true" SelectedValue='<%# Bind("Priority") %>'>
                            <asp:ListItem Text="" Value=""></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:DropDownList ID="DropDownList1" DataSourceID="xdsTaskClosingType" DataTextField="Value" DataValueField="Value" runat="server" AppendDataBoundItems="true" SelectedValue='<%# Bind("ClosingType") %>'>
                            <asp:ListItem Text="" Value=""></asp:ListItem>
                        </asp:DropDownList>
                        <asp:TextBox ID="txtContent" Visible="false" Text='<%# Bind("Content") %>' runat="server" />
                        <asp:TextBox ID="txtCreatedOn" Visible="false" Text='<%# Bind("CreatedOn") %>' runat="server" />
                        <asp:TextBox ID="txtCreatedBy" Visible="false" Text='<%# Bind("CreatedBy") %>' runat="server" />
                    </td>

                </tr>
            </EditItemTemplate>
            <EmptyDataTemplate>
                <table runat="server" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px;">
                    <tr>
                        <td>No data was returned.
                                                    <asp:Button ID="btnDummyBtn" runat="server" Text="Button" CssClass="hidden" />
                        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="CommentsPopup" TargetControlID="btnDummyBtn"></ajaxToolkit:ModalPopupExtender>
                        </td>
                    </tr>
                </table>
            </EmptyDataTemplate>
            <ItemTemplate>
                <tr style="background-color: #DCDCDC;" id="TaskListRow" runat="server">
                    <td>
                        <asp:LinkButton ID="DeleteButton" runat="server" CommandName="Delete" Text="Delete" CssClass="CommandButton"/>
                        <asp:LinkButton ID="EditButton" runat="server" CommandName="Edit" Text="Edit" CssClass="CommandButton" />
                        <asp:LinkButton ID="btnDetails" runat="server" OnCommand="btnDetails_Command" CssClass="CommandButton" CommandArgument='<%# Eval("Id") %>'>Details</asp:LinkButton>
                        <asp:LinkButton ID="btnComments" runat="server" CssClass="CommandButton" CommandArgument='<%# Eval("Id") %>' OnCommand="btnComments_Command">Comments</asp:LinkButton>&nbsp;
                        <asp:Button ID="btnDummyBtn" runat="server" Text="Button" CssClass="hidden" />
                        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="CommentsPopup" TargetControlID="btnDummyBtn"></ajaxToolkit:ModalPopupExtender>

                    </td>

                    <td>
                        <asp:Label ID="SubjectLabel" runat="server" Text='<%# Eval("Subject") %>' />
                    </td>

                    <td>
                        <asp:Label ID="DueDateLabel" runat="server" Text='<%# Eval("DueDate","{0:d}") %>' />
                    </td>
                    <td>
                        <asp:Label ID="FK_TaskCategoryLabel" runat="server" Text='<%# Eval("TaskCategory.TaskCategory1") %>' />
                    </td>
                    <td>
                        <asp:Label ID="StateLabel" runat="server" Text='<%# Eval("State") %>' />
                    </td>
                    <td>
                        <asp:Label ID="PriorityLabel" runat="server" Text='<%# Eval("Priority") %>' />
                    </td>
                    <td>
                        <asp:Label ID="ClosingTypeLabel" runat="server" Text='<%# Eval("ClosingType") %>' />
                    </td>
                </tr>
            </ItemTemplate>
            <LayoutTemplate>
                <table id="Table2" runat="server" style="width:100%;" >
                    <tr id="Tr1" runat="server">
                        <td id="Td1" runat="server">
                            <table id="itemPlaceholderContainer" runat="server" border="1" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px; font-family: Arial;width:100%; ">
                                <tr id="Tr2" runat="server" style="background-color: #DCDCDC; color: #000000; ">
                                    <th id="Th1" runat="server"></th>
                                    <th id="Th2" runat="server" style="width:400px">&nbsp;Subject&nbsp;</th>
                                    <th id="Th3" runat="server">&nbsp;DueDate&nbsp;</th>
                                    <th id="Th4" runat="server">&nbsp;Task Category&nbsp;</th>
                                    <th id="Th5" runat="server">&nbsp;State&nbsp;</th>
                                    <th id="Th6" runat="server">&nbsp;Priority&nbsp;</th>
                                    <th id="Th7" runat="server">&nbsp;ClosingType&nbsp;</th>

                                </tr>
                                <tr id="itemPlaceholder" runat="server">
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr id="Tr3" runat="server">
                        <td id="Td2" runat="server" style="text-align: center; background-color: #CCCCCC; font-family: Verdana, Arial, Helvetica, sans-serif; color: #000000;">
                            <asp:DataPager ID="DataPager1" runat="server">
                                <Fields>
                                    <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False" />
                                    <asp:NumericPagerField />
                                    <asp:NextPreviousPagerField ButtonType="Button" ShowLastPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False" />
                                </Fields>
                            </asp:DataPager>
                        </td>
                    </tr>
                </table>
            </LayoutTemplate>
            <SelectedItemTemplate>
                <tr style="background-color: #008A8C; font-weight: bold; color: #FFFFFF;">
                    <td>
                        <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" Text="Delete" />
                        <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="Edit" />&nbsp;
                    </td>
                    <td>
                        <asp:Label ID="SubjectLabel" runat="server" Text='<%# Eval("Subject") %>' />
                    </td>

                    <td>
                        <asp:Label ID="DueDateLabel" runat="server" Text='<%# Eval("DueDate","{0:d}") %>' />
                    </td>
                    <td>
                        <asp:Label ID="FK_TaskCategoryLabel" runat="server" Text='<%# Eval("TaskCategory.TaskCategory1") %>' />
                    </td>
                    <td>
                        <asp:Label ID="StateLabel" runat="server" Text='<%# Eval("State") %>' />
                    </td>
                    <td>
                        <asp:Label ID="PriorityLabel" runat="server" Text='<%# Eval("Priority") %>' />
                    </td>
                    <td>
                        <asp:Label ID="ClosingTypeLabel" runat="server" Text='<%# Eval("ClosingType") %>' />
                    </td>
                </tr>
            </SelectedItemTemplate>
        </asp:ListView>
        <asp:Label ID="lblStatus" runat="server" Text="" ForeColor="Red"></asp:Label>
    </div>
    <div id="CommentsPopup" class="Popup">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:ListView ID="lvwTaskComments" runat="server" DataSourceID="odsTaskComments"  InsertItemPosition="LastItem" DataKeyNames="Id" OnItemInserting="lvwTaskComments_ItemInserting" OnItemUpdating="lvwTaskComments_ItemUpdating" >
                    <AlternatingItemTemplate>
                        <span ><span class="description"> Comment by <%# Eval("CreatedBy") %> posted on <%# Eval("CreatedOn") %></span>
                            <br>
                            </br>
                            <asp:TextBox ID="txtComment" Width="500" Height="100" TextMode="MultiLine" runat="server" Enabled="false" Text='<%# Eval("Comment") %>'></asp:TextBox>
           
                            <br>
                            </br>
                            <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="Edit" />
                            <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" Text="Delete" />&nbsp;
                            <br />
                            <br />
                        </span>
                    </AlternatingItemTemplate>
                    <EditItemTemplate>
                        <span style="">Comment
                            <br />
                            <asp:TextBox ID="CommentTextBox" Width="500" Height="100" TextMode="MultiLine" runat="server" Text='<%# Bind("Comment") %>' />
                            <br />
                            <asp:Button ID="UpdateButton" runat="server" CommandName="Update" Text="Update" />
                            <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Cancel" />
                            <br />
                            <br />
                        </span>
                    </EditItemTemplate>
                    <EmptyDataTemplate>
                        <span>No data was returned.</span>
                    </EmptyDataTemplate>
                    <InsertItemTemplate>
                        <span style="">Comment
                            <br />
                            <asp:TextBox ID="CommentTextBox" Width="500" Height="100" TextMode="MultiLine" runat="server" Text='<%# Bind("Comment") %>' />
                            <br />
                            <asp:Button ID="InsertButton" runat="server" CommandName="Insert" Text="Insert" />
                            <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Clear" />&nbsp;
                            <br />
                            <br />
                        </span>
                    </InsertItemTemplate>
                    <ItemTemplate>
                        <span ><span class="description"> Comment by <%# Eval("CreatedBy") %> posted on <%# Eval("CreatedOn") %></span>
                            <br>
                            </br>
                <asp:TextBox ID="txtComment" Width="500" Height="100" TextMode="MultiLine" runat="server" Enabled="false" Text='<%# Eval("Comment") %>'></asp:TextBox>
                            <br />
                            <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="Edit" />
                            <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" Text="Delete" />&nbsp;
                            <br />
                            <br />
                        </span>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <div id="itemPlaceholderContainer" runat="server" style="width: 597px">
                            <span runat="server" id="itemPlaceholder" />
                        </div>
                        <div style="">
                            <asp:DataPager ID="DataPager1" runat="server" PageSize="2">
                                <Fields>
                                    <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowLastPageButton="True" />
                                </Fields>
                            </asp:DataPager>
                        </div>
                    </LayoutTemplate>
                    <SelectedItemTemplate>
                        <span style="">Comment:
                <asp:Label ID="CommentLabel" runat="server" Text='<%# Eval("Comment") %>' />
                            <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="Edit" />
                            <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" Text="Delete" />&nbsp;
                            <br />
                            <br />
                        </span>
                    </SelectedItemTemplate>
                </asp:ListView>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div id="Buttons">
            <asp:LinkButton ID="lnkClose" runat="server" CssClass="CommandButton" CausesValidation="false">Close</asp:LinkButton>
        </div>
    </div>

    <script type="text/javascript">
        function UpdateCommentsID(id) {

            var hiddenfield = document.getElementById("<%=hdfSelectedTaskId.ClientID %>");
            if (hiddenfield != null) {
                hiddenfield.Value = id;
            }
        }

    </script>
    
</asp:Content>
