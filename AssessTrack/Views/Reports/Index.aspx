<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="AssessTrack.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Reports
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Reports</h2>
    <ul>
        <li><%= Html.ATAuthLink("Final Grades", new { controller = "Reports", action = "FinalGrades", siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName() }, AssessTrack.Filters.AuthScope.CourseTerm, 5, 10)%></li>
        <li><%= Html.ATAuthLink("My Grades", new { controller = "Reports", action = "StudentPerformance", siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName(), id = UserHelpers.GetCurrentUserID() }, AssessTrack.Filters.AuthScope.CourseTerm, 1, 1)%></li>
        <li><%= Html.ATAuthLink("Student Performance", new { action = "Students", controller = "Reports", siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName() }, AssessTrack.Filters.AuthScope.CourseTerm, 5, 10)%></li>
    </ul>
</asp:Content>
