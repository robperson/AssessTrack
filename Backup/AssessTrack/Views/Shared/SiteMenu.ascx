<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="AssessTrack.Helpers" %>
<ul>
<%= Html.ATAuthLink("Courses", "<li>", "</li>", new { controller = "Course", siteShortName = Html.CurrentSiteShortName(), action = "" }, AssessTrack.Filters.AuthScope.Site, 3, 10)%>
<%= Html.ATAuthLink("Terms(Semesters)", "<li>", "</li>", new { controller = "Term", siteShortName = Html.CurrentSiteShortName(), action = "" }, AssessTrack.Filters.AuthScope.Site, 3, 10)%>
<%= Html.ATAuthLink("Course Offerings", "<li>", "</li>", new { controller = "CourseTerm", siteShortName = Html.CurrentSiteShortName(), action = "Index" }, AssessTrack.Filters.AuthScope.Site, 0, 10)%>
<%= Html.ATAuthLink("Program Outcomes", "<li>", "</li>", new { controller = "ProgramOutcome", siteShortName = Html.CurrentSiteShortName(), action = "Index" }, AssessTrack.Filters.AuthScope.Site, 5, 10)%>
<%= Html.ATAuthLink("Members", "<li>", "</li>", new { controller = "SiteMember", siteShortName = Html.CurrentSiteShortName(), action = "Index" }, AssessTrack.Filters.AuthScope.Site, 5, 10)%>
<%= Html.ATAuthLink("Invitations", "<li>", "</li>", new { controller = "Invite", siteShortName = Html.CurrentSiteShortName(), action = "Index" }, AssessTrack.Filters.AuthScope.Site, 5, 10)%>
</ul>