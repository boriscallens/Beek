<%@ Page Title="" Language="C#" MasterPageFile="~/views/shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<AccountViewModel>" %>

<asp:Content ContentPlaceHolderID="Main" runat="server">
  <h2>My Beek</h2>
  <%if(Model.Beek.Any()){%>
    <ul>
      <%foreach (var beek in Model.Beek){%>
        <li><%=beek.Title%></li>
      <%}%>
    </ul>
  <%}else{%>
    There are currently no beek in your collection   
  <%}%>
  
  <p>
    <%= Html.ActionLink<SearchController>(a => a.Index(), "Add beek to your collection")%>
  </p>
  
  <p>
    <%= Html.ActionLink<BeekController>(a=> a.Create(), "Add new Beek") %>
  </p>
</asp:Content>
