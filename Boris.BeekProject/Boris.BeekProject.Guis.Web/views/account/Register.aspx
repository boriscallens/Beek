<%@ Page Language="C#" MasterPageFile="~/views/shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<AccountViewData>" %>

<asp:Content ContentPlaceHolderID="Title" runat="server">
  - Account - Register
</asp:Content>
<asp:Content ContentPlaceHolderID="Css" runat="server">
  <%=Html.PrintStyleSheet("register")%>
</asp:Content>
<asp:Content ContentPlaceHolderID="Main" runat="server">
  <%if (Model.Messages.ContainsKey(MessageKeys.UserNameNotFound)){%>
    <span><%=Model.Messages[MessageKeys.UserNameNotFound]%></span>
  <%}%>
  <%using (Html.BeginForm<AccountController>(a=>a.Register(null), FormMethod.Post, new{id="registerForm"})){%>
    <div>
      <%=Html.HiddenFor(i=>Model.User.Id)%>
    </div>
    <div>
      <%=Html.LabelFor(n => Model.User.Name)%>
      <%=Html.TextBoxFor(n => Model.User.Name, new {tabindex = "1"})%>      
    </div>
    <div>
      <label for="password">Password</label>
      <input type="password" name="password" id="password" value="" tabindex="2" />
    </div>
    <div>
      <%=Html.LabelFor(n => Model.User.Email)%>
      <%=Html.TextBoxFor(n => Model.User.Email, new {tabindex = "3"})%>      
    </div>
    <div>      
	    <input type="submit" value="Register"/>
    </div>
  <%}%>
<%--
    <div>
         <h4>Radio Button Choice</h4>

         <label for="radio-choice-1">Choice 1</label>
         <input type="radio" name="radio-choice-1" id="radio-choice-1" tabindex="2" value="choice-1" />

		 <label for="radio-choice-2">Choice 2</label>
         <input type="radio" name="radio-choice-2" id="radio-choice-2" tabindex="3" value="choice-2" />
    </div>

	<div>
		<label for="select-choice">Select Dropdown Choice:</label>
		<select name="select-choice" id="select-choice">
			<option value="Choice 1">Choice 1</option>
			<option value="Choice 2">Choice 2</option>
			<option value="Choice 3">Choice 3</option>
		</select>
	</div>
	
	<div>
		<label for="textarea">Textarea:</label>
		<textarea cols="40" rows="8" name="textarea" id="textarea"></textarea>
	</div>
	
	<div>
	    <label for="checkbox">Checkbox:</label>
		<input type="checkbox" name="checkbox" id="checkbox" />
    </div>

	<div>
	    <input type="submit" value="Submit" />
    </div>
</form>
--%></asp:Content>
