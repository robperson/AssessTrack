<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="AssessTrack.Helpers" %>
<ul>
        <%= Html.ATAuthLink("Final Grades", "<li>", "</li>", new { controller = "Reports", action = "FinalGrades", siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName() }, AssessTrack.Filters.AuthScope.CourseTerm, 5, 10)%>
        <%= Html.ATAuthLink("Struggling Students", "<li>", "</li>", new { controller = "Reports", action = "StrugglingStudents", siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName() }, AssessTrack.Filters.AuthScope.CourseTerm, 5, 10)%>
        <%= Html.ATAuthLink("My Grades", "<li>", "</li>", new { controller = "Reports", action = "StudentPerformance", siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName(), id = UserHelpers.GetCurrentUserID() }, AssessTrack.Filters.AuthScope.CourseTerm, 1, 1)%>
        <%= Html.ATAuthLink("Student Performance", "<li>", "</li>", new { action = "Students", controller = "Reports", siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName() }, AssessTrack.Filters.AuthScope.CourseTerm, 5, 10)%>
        <%= Html.ATAuthLink("Class Grade Distribution", "<li>", "</li>", new { action = "ClassGradeDistribution", controller = "Reports", siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName() }, AssessTrack.Filters.AuthScope.CourseTerm, 5, 10)%>
        <%= Html.ATAuthLink("Grade Sheet Overview", "<li>", "</li>", new { action = "GradeSheetOverview", controller = "Reports", siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName() }, AssessTrack.Filters.AuthScope.CourseTerm, 5, 10)%>
        <%= Html.ATAuthLink("Course Outcome Summary", "<li>", "</li>", new { action = "CourseOutcomeSummary", controller = "Reports", siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName() }, AssessTrack.Filters.AuthScope.CourseTerm, 5, 10)%>
        <%= Html.ATAuthLink("Program Outcome Summary", "<li>", "</li>", new { action = "ProgramOutcomeSummary", controller = "Reports", siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName() }, AssessTrack.Filters.AuthScope.CourseTerm, 5, 10)%>
        <%= Html.ATAuthLink("Tag Performance Summary", "<li>", "</li>", new { action = "TagPerformanceSummary", controller = "Reports", siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName() }, AssessTrack.Filters.AuthScope.CourseTerm, 5, 10)%>
    </ul>