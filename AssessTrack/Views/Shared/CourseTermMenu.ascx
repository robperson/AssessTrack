<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="AssessTrack.Helpers" %>
<ul>
    <li><%= Html.RouteLink("Assessments", new { controller = "Assessment", action = "Index", siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName() })%></li>
    <%= Html.ATAuthLink("Assessment Types", "<li>", "</li>", new { controller = "AssessmentType", action = "Index", siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName() }, AssessTrack.Filters.AuthScope.CourseTerm, 5, 10)%>
    <%= Html.ATAuthLink("Exceptions", "<li>", "</li>", new { controller = "SubmissionException", action = "Index", siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName() }, AssessTrack.Filters.AuthScope.CourseTerm, 5, 10)%>
    <%= Html.ATAuthLink("Files", "<li>", "</li>", new { controller = "CourseTermFile", action = "Index", siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName() }, AssessTrack.Filters.AuthScope.CourseTerm, 1, 10)%>
    <%= Html.ATAuthLink("Members", "<li>", "</li>", new { controller = "CourseTermMember", action = "Index", siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName() }, AssessTrack.Filters.AuthScope.CourseTerm, 5, 10)%>
    <%= Html.ATAuthLink("Messages", "<li>", "</li>", new { controller = "CourseTermMessage", action = "Index", siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName() }, AssessTrack.Filters.AuthScope.CourseTerm, 1, 10)%>
    
    <li>
        <%= Html.ATAuthLink("Reports", new { controller = "Reports", action = "Index", siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName() }, AssessTrack.Filters.AuthScope.CourseTerm, 1, 10)%>
        <% Html.RenderPartial("ReportsMenu"); %>
    </li>
    
    
    <%= Html.ATAuthLink("Submissions", "<li>", "</li>", new { controller = "SubmissionRecord", action = "Index", siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName() }, AssessTrack.Filters.AuthScope.CourseTerm, 5, 10)%>
    <%= Html.ATAuthLink("Tags", "<li>", "</li>", new { controller = "Tag", action = "Index", siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName() }, AssessTrack.Filters.AuthScope.CourseTerm, 5, 10)%>
    <li>
        <%= Html.ATAuthLink("Tools", new { controller = "CourseTermTools", action = "Index", siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName() }, AssessTrack.Filters.AuthScope.CourseTerm, 5, 10)%>
        <% Html.RenderPartial("CourseTermToolsMenu"); %>
    </li>
</ul>        