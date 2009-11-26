<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="BeekProject.Web.Views.Beek.List" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    List!
    <%=ViewData.Model.Beeks.Count().ToString()%>
</asp:Content>
