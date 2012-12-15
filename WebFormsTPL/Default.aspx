<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebFormsTPL._Default" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">

</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h3>How many things do you want?</h3>
    <asp:TextBox ID="txtNumberOfThings" runat="server"></asp:TextBox>
    <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
    <br/>
    Loading took: <asp:Label ID="lblRunTime" runat="server" Text="Label"></asp:Label> (seconds)
    <asp:GridView ID="gvResults" runat="server"></asp:GridView>
   
</asp:Content>
