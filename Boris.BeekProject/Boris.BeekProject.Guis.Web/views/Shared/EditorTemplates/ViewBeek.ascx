<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl< Boris.BeekProject.Guis.Shared.ViewModels.ViewBeek>" %>
<%=Html.HiddenFor(m=>m.Id) %>
<table>
  <tr>
    <td><%=Html.LabelFor(m=>m.Title)%>:</td>    
    <td><%=Html.EditorFor(m=>m.Title)%></td>
  </tr>
  <tr>
    <td><%=Html.LabelFor(m=>m.Author)%>:</td>
    <td><%=Html.EditorFor(m=>m.Author) %></td>
  </tr>
</table>