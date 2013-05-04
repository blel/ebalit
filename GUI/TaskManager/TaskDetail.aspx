﻿<%@ Page Title="" Language="C#" MasterPageFile="~/GUI/Master.Master" AutoEventWireup="true" CodeBehind="TaskDetail.aspx.cs" Inherits="EbalitWebForms.GUI.TaskManager.TaskDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <asp:ObjectDataSource ID="odsTasks" runat="server" DataObjectTypeName="EbalitWebForms.DataLayer.Task" DeleteMethod="DeleteTask" InsertMethod="CreateTask" SelectMethod="GetTasks" TypeName="EbalitWebForms.BusinessLogicLayer.TaskBLL" UpdateMethod="UpdateTask"></asp:ObjectDataSource>
    <asp:XmlDataSource ID="xdsTaskStatus" runat="server" DataFile="~/Resources/MasterData.xml" XPath="/MasterData/DataDictionary[@Name='TaskStatus']/DictionaryItem" OnDataBinding="xdsTaskStatus_DataBinding"></asp:XmlDataSource>
    <asp:XmlDataSource ID="xdsTaskPriority" runat="server" DataFile="~/Resources/MasterData.xml" XPath="/MasterData/DataDictionary[@Name='TaskPriority']/DictionaryItem"></asp:XmlDataSource>
    <asp:XmlDataSource ID="xdsTaskClosingType" runat="server" DataFile="~/Resources/MasterData.xml" XPath="/MasterData/DataDictionary[@Name='ClosingType']/DictionaryItem"></asp:XmlDataSource>
    <asp:ObjectDataSource ID="odsTaskCategories" runat="server" DataObjectTypeName="EbalitWebForms.DataLayer.TaskCategory" DeleteMethod="Delete" InsertMethod="Create" SelectMethod="GetTaskCategories" TypeName="EbalitWebForms.BusinessLogicLayer.TaskCategoryBLL" UpdateMethod="Update" OnSelected="odsTaskCategories_Selected"></asp:ObjectDataSource>
    <div id="TitleContainer">
        <div id="Title">
            >> Task Details
        </div>
    </div>

    <div id="Container">
        <div id="LeftColumn">
        </div>
        <div id="MainColumn">

            <asp:DetailsView ID="dtvTask" runat="server" AutoGenerateRows="False" DataSourceID="odsTasks" Height="50px" Width="761px" CssClass="detailsview" DataKeyNames="Id">
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
                            <asp:TextBox ID="txtContent" Width="500" Height="300" runat="server" Text='<%# Bind("Content") %>'></asp:TextBox>
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
                            <asp:TextBox ID="txtDueDate" Width="200" runat="server" Text='<%# Bind("DueDate") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <InsertItemTemplate>
                            <asp:TextBox ID="txtDueDate" Width="200"  runat="server" Text='<%# Bind("DueDate") %>'></asp:TextBox>
                        </InsertItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblDueDate" runat="server" Text='<%# Bind("DueDate") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Task Category" SortExpression="FK_TaskCategory">
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlTaskCategory" Width="200"  runat="server" DataSourceID="odsTaskCategories" DataTextField="TaskCategory1" DataValueField="Id" SelectedValue='<%#Bind("FK_TaskCategory") %>'>
                            </asp:DropDownList>
                        </EditItemTemplate>
                        <InsertItemTemplate>
                            <asp:DropDownList ID="ddlTaskCategory" Width="200"  runat="server" DataSourceID="odsTaskCategories" DataTextField="TaskCategory1" DataValueField="Id" SelectedValue='<%#Bind("FK_TaskCategory") %>'>
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

                    <asp:CommandField ShowInsertButton="True" ShowDeleteButton="True" ShowEditButton="True" />

                </Fields>
            </asp:DetailsView>

        </div>
        <div id="RightColumn">
        </div>
    </div>
</asp:Content>


