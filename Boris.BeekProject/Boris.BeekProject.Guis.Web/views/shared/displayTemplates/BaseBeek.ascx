<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Boris.BeekProject.Model.Beek.BaseBeek>" %>
<%@ Import Namespace="Boris.BeekProject.Model.Accounts" %>

<table class="beek">
  <tr>
    <td>title:</td>
    <td><%=Html.Encode(Model.Title)%></td>
    <td>author:</td>
    <td><%=Html.Encode(Model.GetInvolvedUsersForContribution(Contributions.Writer).First())%></td>
  </tr>
</table>