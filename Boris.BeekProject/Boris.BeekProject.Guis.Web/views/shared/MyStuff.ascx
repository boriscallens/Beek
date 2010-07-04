<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<BaseBeekViewData>" %>
<%=Html.ActionLink<AccountController>(a=>a.MyBeek(), "My Beek", new{@class="navSubLink"})%>
<div class="navSubLink">My other stuff</div>
<%=Html.ActionLink<AccountController>(a=>a.LogOut(), "Log Out", new{@class="navSubLink"})%>