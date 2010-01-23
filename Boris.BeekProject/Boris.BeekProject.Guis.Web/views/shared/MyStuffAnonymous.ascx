<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<BaseBeekViewModel>" %>
<%=Html.ActionLink<AccountController>(a=>a.LogIn(), "Log In", new{@class="navSubLink"})%>
<%=Html.ActionLink<AccountController>(a=>a.Register(Model.User.Id), "Register", new{@class="navSubLink"})%>