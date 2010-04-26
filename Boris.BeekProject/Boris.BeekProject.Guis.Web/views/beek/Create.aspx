<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<BeekViewModel>" %>

<asp:Content ContentPlaceHolderID="Main" runat="server">
    <h2>Add Beek</h2>
    
    <% using (Html.BeginForm()) {%>
        <%= Html.ValidationSummary(true.ToString())%>

        <fieldset>
            <legend></legend>
            <%=Html.EditorFor(m=>Model.Beek) %>
            <p>
              <input type="submit" value="Create" />
            </p>
        </fieldset>

    <% } %>
</asp:Content>