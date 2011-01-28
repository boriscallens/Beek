<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<BeekViewData>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Css" runat="server">
  <%=Html.PrintStyleSheet("beek")%>
</asp:Content>

<asp:Content ContentPlaceHolderID="Main" runat="server"> 
  <% using (Html.BeginForm<BeekController>(a=>a.Edit(null), FormMethod.Post, new{id="adBeekForm"})) {%>
      <%=Html.ValidationSummary()%>
      <%=Html.EditorFor(m=>Model.Beek) %>
      <p>
        <button type="submit" value="Create">Add</button>
      </p>
  <% } %>
</asp:Content>