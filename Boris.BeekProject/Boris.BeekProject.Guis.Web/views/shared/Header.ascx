<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<BaseBeekViewData>" %>
    <div id="header">
      <a href="#" id="logo"></a>
      <div id="navBlocks" <%=Model.CurrentNavBlock == NavBlocks.MyStuff?"class='lastNavBlockSelected'":""%>>          
        <a href="/" id="homeNavBlock" class="navBlock <%=Model.CurrentNavBlock == NavBlocks.Home?"activeNavBlock":""%>">          
          <span class="navTitle">home</span>    
        </a>
        <div id="beek" class="navBlock <%=Model.CurrentNavBlock == NavBlocks.Beek?"activeNavBlock":""%>">
          <span class="navTitle">beek</span>
          <%if (!Model.User.IsAnonymous){%>
            <%=Html.ActionLink<BeekController>(a=>a.Create(), "Add", new{@class="navSubLink"})%>
          <%}%>
        </div>
        <div id="myStuffNavBlock" class="navBlock <%=Model.CurrentNavBlock == NavBlocks.MyStuff?"activeNavBlock":""%>">
          <a href="#" class="navTitle">my stuff</a>
          <%if (Model.User.IsAnonymous){
            Html.RenderPartial("MyStuffAnonymous", ViewData);
          }else{
            Html.RenderPartial("myStuff", ViewData);
          }%>        
        </div>
      </div>
    </div>
