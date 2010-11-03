<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<%=Html.DropDownList(string.Empty, Html.SelectListItemsFromEnum<BeekTypes>(0)) %>