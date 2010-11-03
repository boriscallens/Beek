<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<SearchViewData>" %>
<%@ Import Namespace="Boris.BeekProject.Model.Beek" %>

<asp:Content ContentPlaceHolderID="Main" runat="server">
    <h2>Search Beek</h2>
    <form action="/Search/ProcessBeek" method="post">
      <%=Html.EditorFor(m => m.UsedBeekSearchBag) %>
    </form>
 
    <div id="resultSet">
      <%foreach (BaseBeek beek in Model.FoundBeek){%>
        <%Html.RenderAction<BeekController>(c => c.Thumb(beek.Id));%>
      <%}%>
    </div>
</asp:Content>