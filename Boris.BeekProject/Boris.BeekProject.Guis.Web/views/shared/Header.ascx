<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<BaseBeekViewModel>" %>
    <div id="header">
      <a href="#" id="logo"></a>
      <div id="navBlocks">
        <a href="#" id="homeNavBlock" class="navBlock activeNavBlock">          
          <span class="navTitle">home</span>    
        </a>
        <a href="#" id="searchNavBlock" class="navBlock">
          <span class="navTitle">search</span>
        </a>
        <div id="myStuffNavBlock" class="navBlock">
          <a href="#" class="navTitle">my stuff</a>
          <%if (Model.User.IsAnonymous){
            Html.RenderPartial("MyStuffAnonymous", ViewData);
          }else{
            Html.RenderPartial("myStuff", ViewData);
          }%>        
        </div>
        
      </div>
    </div>
