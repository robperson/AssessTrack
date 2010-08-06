<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="AssessTrack.Helpers" %>
<ul>
        <%= Html.ATAuthLink("Final Grades", "<li>", "</li>", new { controller = "Reports", action = "FinalGrades", siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName() }, AssessTrack.Filters.AuthScope.CourseTerm, 5, 10)%>
        <%= Html.ATAuthLink("My Grades", "<li>", "</li>", new { controller = "Reports", action = "StudentPerformance", siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName(), id = UserHelpers.GetCurrentUserID() }, AssessTrack.Filters.AuthScope.CourseTerm, 1, 1)%>
        <%= Html.ATAuthLink("Student Performance", "<li>", "</li>", new { action = "Students", controller = "Reports", siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName() }, AssessTrack.Filters.AuthScope.CourseTerm, 5, 10)%>
    </ul>