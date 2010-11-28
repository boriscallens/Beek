<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<BeekViewData>" %>

<asp:Content ContentPlaceHolderID="Css" runat="server">
  <%=Html.PrintStyleSheet("beek")%>
</asp:Content>

<asp:Content ContentPlaceHolderID="Main" runat="server">
    <h2>Details</h2>
    <div id="beekDetail">
      <%=Html.DisplayFor(m=>m.Beek) %>
    </div>
</asp:Content>