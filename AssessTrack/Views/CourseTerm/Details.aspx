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
        <%= Html.ATAuthLink("Manage Tags", new { controller = "Tag", action = "Index" }, AssessTrack.Filters.AuthScope.Site, 5, 10)%><br />
        <%= Html.ATAuthLink("Manage Assessment Types", new { controller = "AssessmentType", action = "Index" }, AssessTrack.Filters.AuthScope.Site, 5, 10)%><br />
        <%= Html.RouteLink("Manage Assessments", new { controller = "Assessment" , action = "Index"}) %><br />
        <%= Html.ATAuthLink("Manage Submissions", new { controller = "SubmissionRecord", action = "Index" }, AssessTrack.Filters.AuthScope.Site, 5, 10)%>
        <%= Html.RouteLink("Students", "CourseTermDetails", new { action = "Students", controller = "CourseTerm" })%>
    </p>
    
    <p>

        <%=Html.ActionLink("Back to List", "Index") %>
    </p>

</asp:Content>

