<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="AssessTrack.Helpers" %>
<ul>
    <li><%= Html.RouteLink("View Assessments", new { controller = "Assessment", action = "Index", siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName() })%></li>
    <%= Html.ATAuthLink("View Assessment Types", "<li>", "</li>", new { controller = "AssessmentType", action = "Index", siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName() }, AssessTrack.Filters.AuthScope.CourseTerm, 5, 10)%>
    <%= Html.ATAuthLink("View Members", "<li>", "</li>", new { controller = "CourseTermMember", action = "Index", siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName() }, AssessTrack.Filters.AuthScope.CourseTerm, 5, 10)%>
    <%= Html.ATAuthLink("View Messages", "<li>", "</li>", new { controller = "CourseTermMessage", action = "Index", siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName() }, AssessTrack.Filters.AuthScope.CourseTerm, 1, 10)%>
    <li>
        <%= Html.ATAuthLink("View Reports", new { controller = "Reports", action = "Index", siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName() }, AssessTrack.Filters.AuthScope.CourseTerm, 1, 10)%>
        <% Html.RenderPartial("ReportsMenu"); %>
    </li>
    
    <%= Html.ATAuthLink("View Submission Exceptions", "<li>", "</li>", new { controller = "SubmissionException", action = "Index", siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName() }, AssessTrack.Filters.AuthScope.CourseTerm, 5, 10)%>
    <%= Html.ATAuthLink("View Submissions", "<li>", "</li>", new { controller = "SubmissionRecord", action = "Index", siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName() }, AssessTrack.Filters.AuthScope.CourseTerm, 5, 10)%>
    <%= Html.ATAuthLink("View Tags", "<li>", "</li>", new { controller = "Tag", action = "Index", siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName() }, AssessTrack.Filters.AuthScope.CourseTerm, 5, 10)%>
    <li>
        <%= Html.ATAuthLink("View Tools", new { controller = "CourseTermTools", action = "Index", siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName() }, AssessTrack.Filters.AuthScope.CourseTerm, 5, 10)%>
        <% Html.RenderPartial("CourseTermToolsMenu"); %>
    </li>
</ul>        