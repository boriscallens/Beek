<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<BeekViewData>" %>

<asp:Content ContentPlaceHolderID="Main" runat="server">
  <h2>Add Beek</h2>
   
  <% using (Html.BeginForm<BeekController>(a=>a.Create(null), FormMethod.Post, new{id="adBeekForm"})) {%>
      <%=Html.ValidationSummary()%>
      <%=Html.EditorFor(m=>Model.Beek) %>
      <p>
        <button type="submit" value="Create">Add</button>
      </p>
  <% } %>
</asp:Content>