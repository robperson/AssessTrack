<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="AssessTrack.Helpers" %>
<ul>
<li><%= Html.ATAuthLink("Courses", new { controller = "Course", siteShortName = Html.CurrentSiteShortName(), action = "" }, AssessTrack.Filters.AuthScope.Site, 3, 10)%></li>
<li><%= Html.ATAuthLink("Terms(Semesters)", new { controller = "Term", siteShortName = Html.CurrentSiteShortName(), action = "" }, AssessTrack.Filters.AuthScope.Site, 3, 10)%></li>
<li><%= Html.ATAuthLink("Course Offerings", new { controller = "CourseTerm", siteShortName = Html.CurrentSiteShortName(), action = "Index" }, AssessTrack.Filters.AuthScope.Site, 0, 10)%></li>
</ul>