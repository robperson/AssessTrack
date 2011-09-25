<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<AssessTrack.Models.CourseTermViewModels.CourseTermInstructorDetailsViewModel>" %>

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
    <p>Current enrollment password: <%= Model.CourseTerm.Password %></p>
    <h3>Ungraded Assessments</h3>
    <% Html.RenderPartial("UngradedAssessmentList", Model.UngradedAssessments); %>
    <h3>Class Grade Distribution</h3>
    <% Html.RenderPartial("~/Views/Reports/GradeDistributionTable.ascx", Model.GradeDistribution); %>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ExtraContent" runat="server">
</asp:Content>
