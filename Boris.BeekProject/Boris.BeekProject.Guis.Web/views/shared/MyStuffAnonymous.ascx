<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<BaseBeekViewData>" %>
  <%using (Html.BeginForm<AccountController>(a => a.LogIn(string.Empty, string.Empty, string.Empty), FormMethod.Post, new { id = "navLoginForm", @class="navSubLink"}))
    {%>
    <div id="navLoginInputs">
        <input type="text" name="username" id="navLoginUsername" value="Username" tabindex="998" id="navLoginUsername"/>
        <input type="password" name="password" id="password" value="" tabindex="999" />
    </div>
    <div>      
	    <input type="image" value="Log In" src="/content/pics/icons/key.png"/>
    </div>
  <%}%>

<%=Html.ActionLink<AccountController>(a=>a.Register(Model.User.Id), "Register", new{@class="navSubLink"})%>