<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<p class="logon">
<%
    if (Request.IsAuthenticated) {
        
%>
        Welcome, <b><%= Html.Encode(ViewData["UserFullName"]) %></b>!
        [ <%= Html.ActionLink("Log Off", "LogOff", "Account") %> ]
<%
    }
    else {
%> 
        [ <%= Html.ActionLink("Log On", "LogOn", "Account") %> ] |
        [ <%= Html.ActionLink("Register", "Register", "Account") %> ]
<%
    }
%>
</p>