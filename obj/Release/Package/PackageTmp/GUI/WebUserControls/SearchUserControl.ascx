<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SearchUserControl.ascx.cs" Inherits="EbalitWebForms.GUI.WebUserControls.SearchUserControl" %>
<div id="Search" class="partlet">
                <asp:Table ID="tblSearch" runat="server">
                    <asp:TableHeaderRow>
                        <asp:TableHeaderCell ColumnSpan="2" HorizontalAlign="Left">
                           <h3>Search</h3> 
                        </asp:TableHeaderCell>
                    </asp:TableHeaderRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:TextBox ID="txtSearch" Width="180px" runat="server"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell HorizontalAlign="Right">
                           
                         
                            <asp:LinkButton ID="btnSearch" CausesValidation="false" OnCommand="btnSearch_Command" runat="server" CssClass="CommentButton">Search</asp:LinkButton>

                        </asp:TableCell>
                    </asp:TableRow>

                </asp:Table>

            </div>