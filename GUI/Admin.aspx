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
           <div id="login" style="display:block; margin-left:auto;margin-right:auto; width:400px;">
            <asp:Login ID="ctlLogin" runat="server" EnableTheming="False"
                 BorderColor="#CCCC99" BorderStyle="Solid" BorderWidth="1px" 
                Height="160px" Width="400px" OnAuthenticate="ctlLogin_Authenticate" 
                DestinationPageUrl="/GUI/ProtectedSites/AdminIndex.aspx" BackColor="#F7F7DE" 
                Font-Names="Verdana" Font-Size="10pt">
                <CheckBoxStyle BorderColor="#666666" />
                <LabelStyle HorizontalAlign="Left" />
                <TextBoxStyle Width="200px" BorderColor="#666666" BorderStyle="Solid" />
                <TitleTextStyle Font-Bold="True" HorizontalAlign="Left" VerticalAlign="Top" BackColor="#6B696B" ForeColor="#FFFFFF" />
            </asp:Login>
          </div>
        </div>
        <div id="RightColumn">
         
        </div>
    </div>
</asp:Content>
