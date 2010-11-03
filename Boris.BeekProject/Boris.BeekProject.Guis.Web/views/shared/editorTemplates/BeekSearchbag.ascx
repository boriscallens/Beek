<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Boris.BeekProject.Services.Search.SearchBags.BeekSearchbag>" %>
 <table>
    <tr>
      <td><label for="isbn">Isbn: </label></td>
      <td><input type="text" maxlength="17" id="isbn" name="isbnContains" value="<%=Html.Encode(Model.IsbnContains) %>"/></td>
    </tr>
    <tr>
      <td><label for="title">Title: </label></td>
      <td><input type="text" id="title" name="beekTitleContains" value="<%=Html.Encode(Model.BeekTitleContains) %>"/></td>
    </tr>
    <tr>
      <td><label for="author">Author: </label></td>
      <td><input type="text" id="author" name="AuthorNameContains" value="<%=Html.Encode(Model.AuthorNameContains) %>"/></td>
    </tr>
    <tr>
      <td colspan="2">
        <input type="submit" value="Search" />
      </td>
    </tr>
</table>