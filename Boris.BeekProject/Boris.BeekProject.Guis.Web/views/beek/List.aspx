<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<BeekViewData>" %>

<h2>Last added Beek</h2>

<ul>
  <%foreach(ViewBeek beek in Model.Beeks){
    var beek1 = beek;%>
    <li><%=Html.DisplayFor(m => beek1) %></li>
  <%}%>
</ul>