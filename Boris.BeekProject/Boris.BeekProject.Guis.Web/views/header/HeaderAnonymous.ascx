<%@ Control Language="C#" Inherits="ViewUserControl<HeaderViewData>" %>
<div id="header">
  <a href="#" id="logo"></a>
  <div id="navigation" class="<%=ViewData.Model.CurrentNavBlock%>IsActive">
    <a href="/" id="homeNavBlock" class="navBlock">          
      <span class="navTitle">home</span>    
    </a>
    <div id="beekNavblock" class="navBlock">
      <span class="navTitle">beek</span>
      <%=Html.ActionLink<SearchController>(a=>a.Beek(), "Search", new{@class="navSubLink"}) %>
    </div>
    <div id="myStuffNavBlock" class="navBlock">
      <%using (Html.BeginForm<AccountController>(a => a.LogIn(string.Empty, string.Empty, string.Empty), FormMethod.Post, new { id = "navLoginForm", @class="navSubLink"})){%>
        <table id="navLoginInputs">
          <tr>
            <td>
              <input type="text" name="username" value="Username" tabindex="998" id="navLoginUsername"/>
            </td>
            <td rowspan="2">
              <input type="image" value="Log In" src="/content/pics/icons/key.png"/>
            </td>
          </tr>
          <tr>
            <td>
              <input type="password" name="password" id="loginpassword" value="" tabindex="999" />  
            </td>
          </tr>
        </table>
      <%}%>
      <%=Html.ActionLink<AccountController>(a=>a.Register(Model.User.Id), "Register", new{@class="navSubLink"})%>
    </div>
  </div>
</div>

