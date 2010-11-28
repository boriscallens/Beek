<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<SearchViewData>" %>
<%@ Import Namespace="Boris.BeekProject.Model.Beek" %>

<asp:Content ContentPlaceHolderID="Css" runat="server">
  <%=Html.PrintStyleSheet("search")%>
  <%=Html.PrintStyleSheet("beek")%>
</asp:Content>

<asp:Content ContentPlaceHolderID="Main" runat="server">
  <form action="/Search/ProcessBeek" method="post" id="searchBeekForm">
    <%=Html.EditorFor(m => m.UsedBeekSearchBag) %>
    <p>
      <input type="submit" name="submit" value="Search" />
    </p>
  </form>
  <div id="resultSet">
    <%foreach (BaseBeek beek in Model.FoundBeek){%>
      <%Html.RenderAction<BeekController>(c => c.Thumb(beek.Id));%>
    <%}%>
    </div>
</asp:Content>