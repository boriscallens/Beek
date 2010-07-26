<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Boris.BeekProject.Guis.Shared.ViewData.SearchViewData>" %>

<asp:Content ContentPlaceHolderID="Main" runat="server">
    <h2>Search Beek</h2>
    <form id="searchForm" action="/Search/Beek">
      <table>
        <tr>
          <td><label for="isbn">Isbn: </label></td>
          <td><input type="text" maxlength="17" id="isbn" name="isbn" /></td>
        </tr>    
        <tr>
          <td colspan="2">
            <input type="button" value="submit" />
          </td>
        </tr>
      </table>
    </form>
    
    
    <div id="resultSet"></div>
</asp:Content>

<asp:Content ContentPlaceHolderID="Title" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="Css" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="Js" runat="server">
</asp:Content>
