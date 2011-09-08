<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="AssessTrack.Helpers" %>
<ul>
    <%= Html.ATAuthLink("Download Code For Answer", "<li>", "</li>", new { controller = "CourseTermTools", action = "DownloadAnswerCode", siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName()},AssessTrack.Filters.AuthScope.CourseTerm,5,10) %>
</ul>