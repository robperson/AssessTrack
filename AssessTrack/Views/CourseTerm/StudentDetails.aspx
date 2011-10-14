<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<AssessTrack.Models.CourseTermViewModels.CourseTermStudentDetailsViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%= Model.CourseTerm.Name %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%= Model.CourseTerm.Name %></h2>
    <h3>General Information</h3>
    <p><%= Model.CourseTerm.Course.Description %></p>
    <h3>Information for this semester</h3>
    <div class="courseterm-information">
        <%= Model.CourseTerm.Information %>
    </div>
    <% if (Model.CourseTerm.File != null)
        { %>
    <p>
        Download Syllabus:
        <%= Html.ActionLink(Model.CourseTerm.File.Name, "Download", new {controller="File", id = Model.CourseTerm.Syllabus.Value}) %>
    </p>
    <%}
        else
        { %>
    <p>
        No Syllabus Uploaded.
    </p>
    <h3>Your current grade is: <%= Model.CurrentGrade %></h3>
    <% Html.RenderPartial("RecentMessagesList", Model.RecentMessages); %>
    <% Html.RenderPartial("UpcomingAssessmentList", Model.UpcomingAssessments); %>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ExtraContent" runat="server">
</asp:Content>
