<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebFormsTPL._Default" Async="true" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">

</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h3>How many things do you want?</h3>
    <asp:TextBox ID="txtNumberOfThings" runat="server"></asp:TextBox>
    <asp:Button ID="btnSubmit" runat="server" Text="Async Submit" OnClick="btnSubmit_Click" />
    <br/>
    Loading took: <asp:Label ID="lblRunTime" runat="server" Text="Label"></asp:Label> (seconds)
    <br/>
    Loading details: <asp:Label ID="lblLoading" runat="server" Text="Label"></asp:Label>
    <br/>
    Thread details: <asp:Label ID="lblThreadDetails" runat="server" Text="Label"></asp:Label>
    <asp:GridView ID="gvResults" runat="server"></asp:GridView>
   
</asp:Content>
