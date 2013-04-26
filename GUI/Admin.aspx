<%@ Page Title="" Language="C#" MasterPageFile="Master.Master" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="EbalitWebForms.GUI.WebForm6" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <div id="TitleContainer">
    <div id="Title" class="pageHeader">
        >> Admin
    </div>
        </div>
    <div id="Container">



        <div id="LeftColumn">
      
        </div>

        <div id="MainColumn" >
            <asp:Login ID="ctlLogin" runat="server" EnableTheming="False" BorderColor="#666666" BorderStyle="Solid" BorderWidth="1px" Height="160px" Width="400px" OnAuthenticate="ctlLogin_Authenticate" DestinationPageUrl="~/ProtectedSites/AdminIndex.aspx">
                <CheckBoxStyle BorderColor="#666666" />
                <LabelStyle HorizontalAlign="Left" />
                <LoginButtonStyle BorderColor="#666666" BorderStyle="Solid" />
                <TextBoxStyle Width="200px" BorderColor="#666666" BorderStyle="Solid" />
                <TitleTextStyle Font-Bold="True" HorizontalAlign="Left" VerticalAlign="Top" />
            </asp:Login>
        </div>
        <div id="RightColumn">
         
        </div>
    </div>

</asp:Content>
