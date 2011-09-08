<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/QuizBuilder.Master" Inherits="System.Web.Mvc.ViewPage<AssessTrack.Controllers.AssessmentFormViewModel>" %>
<%@ Import Namespace="AssessTrack.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit Assessment
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Edit Assessment</h2>
    <%= Html.ValidationSummary("Edit was unsuccessful. Please correct the errors and try again.") %>

    <% Html.RenderPartial("AssessmentForm"); %>

    <div>
        <%=Html.ActionLink("Back to Assessment list", "Index", new { siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName() })%>
    </div>
</asp:Content>
