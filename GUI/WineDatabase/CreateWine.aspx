<%@ Page Title="" Language="C#" MasterPageFile="~/GUI/ProtectedSites/ToolsMaster.master" AutoEventWireup="true" CodeBehind="CreateWine.aspx.cs" Inherits="EbalitWebForms.GUI.WineDatabase.CreateWine" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ToolsContent" runat="server">
    <!-- Globalization and localization must be enabled at script manager -->
    <asp:ScriptManager ID="ScriptManager" runat="server" EnableScriptGlobalization="true" EnableScriptLocalization="true"></asp:ScriptManager>
    <asp:ObjectDataSource ID="odsWines" TypeName="EbalitWebForms.BusinessLogicLayer.Repository`1[[EbalitWebForms.DataLayer.Wine, EbalitWebForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], EbalitWebForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" runat="server" DataObjectTypeName="EbalitWebForms.DataLayer.Wine" DeleteMethod="DeleteItem" InsertMethod="CreateItem" OldValuesParameterFormatString="original_{0}" SelectMethod="GetItemById" UpdateMethod="UpdateItem" OnSelecting="odsWines_Selecting" OnInserted="odsWines_Inserted">
        <SelectParameters>
            <asp:Parameter DefaultValue="-1" Name="id" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
     <asp:ObjectDataSource ID="odsWineAttributes" TypeName="EbalitWebForms.BusinessLogicLayer.Repository`1[[EbalitWebForms.DataLayer.WineAttribute, EbalitWebForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], EbalitWebForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetItems">

    </asp:ObjectDataSource>
    <div id="Container">
        <div id="LeftColumn">
        </div>
        <div id="MainColumn">
            <h2>Wine Details</h2>
            <asp:DetailsView ID="dvwWine" runat="server" Height="50px" Width="762px" CssClass="detailsview" DataSourceID="odsWines" AutoGenerateRows="False" DataKeyNames="Id" OnItemInserting="dvwWine_ItemInserting" OnModeChanging="dvwWine_ModeChanging" OnItemInserted="dvwWine_ItemInserted" OnItemUpdated="dvwWine_ItemUpdated" OnItemUpdating="dvwWine_ItemUpdating" OnDataBound="dvwWine_DataBound">
                <Fields>
                    <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" Visible="False" />
                    <asp:TemplateField HeaderText="Label">
                        <InsertItemTemplate>
                            <asp:TextBox ID="txtLabel" Width="500" runat="server" Text='<%# Bind("Label") %>' BackColor="LightYellow"></asp:TextBox>
                            <br />
                            <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator1" runat="server" ErrorMessage="required" ControlToValidate="txtLabel" ForeColor="Red"></asp:RequiredFieldValidator>
                        </InsertItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtLabel" Width="500" runat="server" Text='<%# Bind("Label") %>' BackColor="LightYellow"></asp:TextBox>
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" runat="server" ErrorMessage="required" ControlToValidate="txtLabel" ForeColor="Red"></asp:RequiredFieldValidator>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Year">
                        <InsertItemTemplate>
                            <asp:TextBox ID="txtYear" Width="200" runat="server" Text='<%# Bind("Year") %>' BackColor="LightYellow"></asp:TextBox>
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="Dynamic" runat="server" ErrorMessage="required" ControlToValidate="txtYear" ForeColor="Red"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="CompareValidator1" Display="Dynamic" Operator="DataTypeCheck" Type="Integer" ControlToValidate="txtYear" runat="server" ErrorMessage="Please enter a year in format YYYY." ForeColor="Red"></asp:CompareValidator>
                            <asp:RangeValidator ID="RangeValidator1" Display="Dynamic" runat="server" ErrorMessage="Please enter a year between 1900 and 2100." ControlToValidate="txtYear" MinimumValue="1900" MaximumValue="2100" ForeColor="Red"></asp:RangeValidator>

                        </InsertItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtYear" Width="200" runat="server" Text='<%# Bind("Year") %>' BackColor="LightYellow"></asp:TextBox>
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="Dynamic" runat="server" ErrorMessage="required" ControlToValidate="txtYear" ForeColor="Red"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="CompareValidator1" Display="Dynamic" Operator="DataTypeCheck" Type="Integer" ControlToValidate="txtYear" runat="server" ErrorMessage="Please enter a year in format YYYY." ForeColor="Red"></asp:CompareValidator>
                            <asp:RangeValidator ID="RangeValidator1" Display="Dynamic" runat="server" ErrorMessage="Please enter a year between 1900 and 2100." ControlToValidate="txtYear" MinimumValue="1900" MaximumValue="2100" ForeColor="Red"></asp:RangeValidator>

                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Grape">
                        <InsertItemTemplate>
                            <asp:TextBox ID="txtGrape" Width="500" runat="server" Text='<%# Bind("Grape") %>'></asp:TextBox>

                        </InsertItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtGrape" Width="500" runat="server" Text='<%# Bind("Grape") %>'></asp:TextBox>

                        </EditItemTemplate>

                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Origin">
                        <InsertItemTemplate>
                            <asp:TextBox ID="txtOrigin" Width="500" runat="server" Text='<%# Bind("Origin") %>'></asp:TextBox>
                        </InsertItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtOrigin" Width="500" runat="server" Text='<%# Bind("Origin") %>'></asp:TextBox>
                        </EditItemTemplate>

                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Bought On">
                        <InsertItemTemplate>
                            <asp:TextBox ID="txtBoughtOn" Width="200" runat="server" Text='<%# Bind("BoughtOn", "{0:d}") %>'></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtBoughtOn"></ajaxToolkit:CalendarExtender>
                            <br />
                            <asp:CompareValidator ID="CompareValidator2" Display="Dynamic" Operator="DataTypeCheck" Type="Date" ControlToValidate="txtBoughtOn" runat="server" ErrorMessage="Please enter a date." ForeColor="Red"></asp:CompareValidator>

                        </InsertItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtBoughtOn" Width="200" runat="server" Text='<%# Bind("BoughtOn",  "{0:d}") %>'></asp:TextBox>
                            <br />
                            <asp:CompareValidator ID="CompareValidator2" Display="Dynamic" Operator="DataTypeCheck" Type="Date" ControlToValidate="txtBoughtOn" runat="server" ErrorMessage="Please enter a date." ForeColor="Red"></asp:CompareValidator>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtBoughtOn"></ajaxToolkit:CalendarExtender>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Store">
                        <InsertItemTemplate>
                            <asp:TextBox ID="txtStore" Width="500" runat="server" Text='<%# Bind("Magazine") %>'></asp:TextBox>

                        </InsertItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtStore" Width="500" runat="server" Text='<%# Bind("Magazine") %>'></asp:TextBox>

                        </EditItemTemplate>

                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Wine Attributes">
                        <InsertItemTemplate>
                            <asp:CheckBoxList ID="cblAttributes" RepeatColumns="3" RepeatDirection="Horizontal" DataSourceID="odsWineAttributes" DataTextField="Attribute" DataValueField="Id" runat="server"></asp:CheckBoxList>
                        </InsertItemTemplate>
                        <EditItemTemplate>
                            <asp:CheckBoxList ID="cblAttributes" RepeatColumns="3" RepeatDirection="Horizontal" DataSourceID="odsWineAttributes" DataTextField="Attribute" DataValueField="Id" runat="server"></asp:CheckBoxList>

                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Remarks">
                        <InsertItemTemplate>
                            <asp:TextBox ID="txtRemarks" TextMode="MultiLine" Width="500" Height="300" runat="server" Text='<%# Bind("Remarks") %>'></asp:TextBox>

                        </InsertItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtRemarks" TextMode="MultiLine" Width="500" Height="300" runat="server" Text='<%# Bind("Remarks") %>'></asp:TextBox>

                        </EditItemTemplate>

                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Created By">

                        <EditItemTemplate>
                            <asp:Label ID="lblCreatedBy" runat="server" Text='<%# Bind("CreatedBy") %>'></asp:Label>
                        </EditItemTemplate>

                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Created On">

                        <EditItemTemplate>
                            <asp:Label ID="lblCreatedOn" runat="server" Text='<%# Bind("CreatedOn") %>'></asp:Label>
                        </EditItemTemplate>

                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Changed By">

                        <EditItemTemplate>
                            <asp:Label ID="lblChangedBy" runat="server" Text='<%# Bind("ChangedBy") %>'></asp:Label>
                        </EditItemTemplate>

                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Changed On">

                        <EditItemTemplate>
                            <asp:Label ID="lblChangedOn"  runat="server" Text='<%# Bind("ChangedOn") %>'></asp:Label>
                        </EditItemTemplate>

                    </asp:TemplateField>

                    <asp:CommandField ShowEditButton="True" ShowInsertButton="True" ControlStyle-CssClass="CommandButton" />
                </Fields>
            </asp:DetailsView>
        </div>
        <div id="RightColumn">
        </div>
    </div>
</asp:Content>
