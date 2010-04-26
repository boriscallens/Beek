<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<BeekViewModel>" %>

<asp:Content ContentPlaceHolderID="Main" runat="server">
    <h2>Details</h2>
    
    <%=Html.DisplayFor(m=>m.Beek) %>
</asp:Content>