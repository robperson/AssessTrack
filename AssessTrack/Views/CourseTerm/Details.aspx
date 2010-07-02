<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<AssessTrack.Models.CourseTerm>" %>
<%@ Import Namespace="AssessTrack.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Details
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%= Html.Encode(Model.Name) %></h2>

    <fieldset>
        <legend>About this course</legend>
        <p>
            Information:
            <%= Html.Encode(Model.Information) %>
        </p>
        <p>
            Course:
            <%= Html.Encode(Model.Course.Name) %>
        </p>
        <p>
            Term:
            <%= Html.Encode(Model.Term.Name) %>
        </p>
    </fieldset>
    <h3>Options</h3>
    <p>
        <%= Html.ATAuthLink("View Tags", new { controller = "Tag", action = "Index", siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName() }, AssessTrack.Filters.AuthScope.CourseTerm, 5, 10)%><br />
        <%= Html.ATAuthLink("View Assessment Types", new { controller = "AssessmentType", action = "Index", siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName() }, AssessTrack.Filters.AuthScope.CourseTerm, 5, 10)%><br />
        <%= Html.RouteLink("View Assessments", new { controller = "Assessment", action = "Index", siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName() })%><br />
        <%= Html.ATAuthLink("View Submissions", new { controller = "SubmissionRecord", action = "Index", siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName() }, AssessTrack.Filters.AuthScope.CourseTerm, 5, 10)%><br />
        <%= Html.ATAuthLink("View Members", new { controller = "CourseTermMember", action = "Index", siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName() }, AssessTrack.Filters.AuthScope.CourseTerm, 5, 10)%><br />
        <%= Html.ATAuthLink("View Reports", new { controller = "Reports", action = "Index", siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName() }, AssessTrack.Filters.AuthScope.CourseTerm, 1, 10)%><br />
        <%= Html.ATAuthLink("View Submission Exceptions", new { controller = "SubmissionException", action = "Index", siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName() }, AssessTrack.Filters.AuthScope.CourseTerm, 5, 10)%>
        
        
    </p>
    
    <p>

        <%=Html.ActionLink("Back to List", "Index", new { siteShortName = Html.CurrentSiteShortName() })%>
    </p>

</asp:Content>

