<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AsyncWebForm._Default" Async="true" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h3>Async in your face</h3>
     <h3>How many things do you want?</h3>
    <asp:TextBox ID="txtNumberOfThings" runat="server"></asp:TextBox>
    <asp:Button ID="btnSubmit" runat="server" Text="Submit Async" OnClick="btnSubmit_Click" />
    <br/>
    Loading details: <asp:Label ID="lblLoading" runat="server" Text="Label"></asp:Label>
    <br/>
    Thread details: <asp:Label ID="lblThreadDetails" runat="server" Text="Label"></asp:Label>
</asp:Content>
