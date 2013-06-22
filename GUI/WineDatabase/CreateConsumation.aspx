<%@ Page Title="" Language="C#" MasterPageFile="~/GUI/ProtectedSites/ToolsMaster.master" AutoEventWireup="true" CodeBehind="CreateConsumation.aspx.cs" Inherits="EbalitWebForms.GUI.WineDatabase.CreateConsumation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ToolsContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="true" EnableScriptLocalization="true"></asp:ScriptManager>
    <asp:ObjectDataSource ID="odsWines" TypeName="EbalitWebForms.BusinessLogicLayer.WineBLL" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetWineAsString"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsConsumation" TypeName="EbalitWebForms.BusinessLogicLayer.Repository`1[[EbalitWebForms.DataLayer.WineConsumation, EbalitWebForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], EbalitWebForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetItemById" OnSelecting="odsConsumation_Selecting" DataObjectTypeName="EbalitWebForms.DataLayer.WineConsumation" DeleteMethod="DeleteItem" InsertMethod="CreateItem" UpdateMethod="UpdateItem" OnSelected="odsConsumation_Selected">
        <SelectParameters>
            <asp:Parameter DefaultValue="-1" Name="id" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:XmlDataSource ID="xdsWineRating" runat="server" DataFile="~/Resources/MasterData.xml" XPath="/MasterData/DataDictionary[@Name='WineRating']/DictionaryItem"></asp:XmlDataSource>
    <div id="Container">
        <div id="LeftColumn"></div>
        <div id="MainColumn">
            <h2>Consumation Details</h2>
            <asp:DetailsView ID="dvwConsumation" AutoGenerateRows="False" CssClass="detailsview" runat="server" Height="50px" Width="762px" DataSourceID="odsConsumation" DataKeyNames="Id" OnItemInserted="dvwConsumation_ItemInserted" OnItemInserting="dvwConsumation_ItemInserting" OnItemUpdated="dvwConsumation_ItemUpdated" OnItemUpdating="dvwConsumation_ItemUpdating" OnModeChanging="dvwConsumation_ModeChanging">
                <Fields>
                    <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" Visible="False" />
                    <asp:TemplateField HeaderText="Taster">
                        <InsertItemTemplate>
                            <asp:Label ID="lblTaster" runat="server" Text='<%#Bind("Taster") %>'></asp:Label>
                        </InsertItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="lblTaster" runat="server" Text='<%#Bind("Taster") %>'></asp:Label>
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Wine">
                        <InsertItemTemplate>
                            <asp:DropDownList ID="ddlWine" AppendDataBoundItems="true" Width="500" runat="server" DataSourceID="odsWines" DataTextField="Value" DataValueField="Key" SelectedValue='<%#Bind("FK_Wine") %>' BackColor="LightYellow">
                                <asp:ListItem Text="--select an item--" Value=""></asp:ListItem>
                            </asp:DropDownList>
                            </br>
                            <asp:RequiredFieldValidator Display="Dynamic" ForeColor="Red" ControlToValidate="ddlWine" ID="RequiredFieldValidator1" runat="server" ErrorMessage="Select an item"></asp:RequiredFieldValidator>

                        </InsertItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlWine" AppendDataBoundItems="true" Width="500" runat="server" DataSourceID="odsWines" DataTextField="Value" DataValueField="Key" SelectedValue='<%#Bind("FK_Wine") %>' BackColor="LightYellow">
                                <asp:ListItem Text="--select an item--" Value=""></asp:ListItem>
                            </asp:DropDownList>
                            </br>
                            <asp:RequiredFieldValidator Display="Dynamic" ControlToValidate="ddlWine" ID="RequiredFieldValidator1" runat="server" ErrorMessage="Select an item"></asp:RequiredFieldValidator>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Tasted On">
                        <InsertItemTemplate>
                            <asp:TextBox runat="server" ID="txtTastedOn" Text='<%#Bind("TastedOn","{0:d}") %>'></asp:TextBox>
                            <br />
                            <asp:CompareValidator ID="CompareValidator2" Display="Dynamic" Operator="DataTypeCheck" Type="Date" ControlToValidate="txtTastedOn" runat="server" ErrorMessage="Please enter a date." ForeColor="Red"></asp:CompareValidator>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtTastedOn"></ajaxToolkit:CalendarExtender>
                        </InsertItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox runat="server" ID="txtTastedOn" Text='<%#Bind("TastedOn","{0:d}") %>'></asp:TextBox>
                            <br />
                            <asp:CompareValidator ID="CompareValidator2" Display="Dynamic" Operator="DataTypeCheck" Type="Date" ControlToValidate="txtTastedOn" runat="server" ErrorMessage="Please enter a date." ForeColor="Red"></asp:CompareValidator>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtTastedOn"></ajaxToolkit:CalendarExtender>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Meal">
                        <InsertItemTemplate>
                            <asp:TextBox runat="server" ID="txtMeal" Width="500" Text='<%#Bind("Meal") %>'></asp:TextBox>
                        </InsertItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox runat="server" ID="txtMeal" Width="500" Text='<%#Bind("Meal") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Weather">
                        <InsertItemTemplate>
                            <asp:TextBox runat="server" ID="txtWeather" Width="500" Text='<%#Bind("WeatherConditions") %>'></asp:TextBox>
                        </InsertItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox runat="server" ID="txtWeather" Width="500" Text='<%#Bind("WeatherConditions") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="My Rating">
                        <InsertItemTemplate>
                            <asp:DropDownList ID="ddlRating" AppendDataBoundItems="true" runat="server" DataSourceID="xdsWineRating" DataTextField="Value" DataValueField="Key" SelectedValue='<%# Bind("Rating")%>'>
                                <asp:ListItem Text="--select an item--" Value=""></asp:ListItem>
                            </asp:DropDownList>

                        </InsertItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlRating" AppendDataBoundItems="true" runat="server" DataSourceID="xdsWineRating" DataTextField="Value" DataValueField="Key" SelectedValue='<%# Bind("Rating")%>'>
                                <asp:ListItem Text="--select an item--" Value=""></asp:ListItem>
                            </asp:DropDownList>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Remarks">
                        <InsertItemTemplate>
                            <asp:TextBox runat="server" ID="txtRemarks" TextMode="MultiLine" Width="500" Height="300" Text='<%#Bind("Remarks") %>'></asp:TextBox>
                        </InsertItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox runat="server" ID="txtRemarks" TextMode="MultiLine" Width="500" Height="300" Text='<%#Bind("Remarks") %>'></asp:TextBox>
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
                            <asp:Label ID="lblChangedOn" runat="server" Text='<%# Bind("ChangedOn") %>'></asp:Label>
                        </EditItemTemplate>

                    </asp:TemplateField>
                    <asp:CommandField ShowEditButton="True" ShowInsertButton="True" ControlStyle-CssClass="CommandButton" />
                </Fields>
            </asp:DetailsView>
        </div>
        <div id="RightColumn"></div>
    </div>


</asp:Content>
