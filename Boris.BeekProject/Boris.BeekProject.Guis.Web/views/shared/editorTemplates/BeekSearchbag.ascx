<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Boris.BeekProject.Services.Search.SearchBags.BeekSearchbag>" %>
<table>
  <tr>
    <td><label for="BeekTitleContains" class="form-first">Title:</label></td>
    <td><input type="text" id="BeekTitleContains" name="BeekTitleContains" value="<%=Html.Encode(Model.BeekTitleContains) %>"/></td>
  </tr>
  <tr>
    <td><label for="AuthorNameContains" class="form">Author: </label></td>
    <td><input type="text" id="AuthorNameContains" name="AuthorNameContains" value="<%=Html.Encode(Model.AuthorNameContains) %>"/></td>
  </tr>
</table>