<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%
    if (Request.IsAuthenticated) {
        AssessTrack.Models.AssessTrackDataRepository data = new AssessTrack.Models.AssessTrackDataRepository();
        AssessTrack.Models.Profile profile = data.GetLoggedInProfile();
%>
        Welcome <b><%= Html.Encode(profile.FirstName) %></b>!
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
