<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="Boris.BeekProject.Guis.Shared.ViewModels.DTO" %>

<%=Html.DropDownList(string.Empty, Html.SelectListItemsFromEnum<BeekTypes>(0)) %>