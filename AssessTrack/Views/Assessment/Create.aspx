<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/QuizBuilder.Master" Inherits="System.Web.Mvc.ViewPage<AssessTrack.Controllers.AssessmentFormViewModel>" ValidateRequest="false"%>
<%@ Import Namespace="AssessTrack.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Create
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Create</h2>

    <%= Html.ValidationSummary("Create was unsuccessful. Please correct the errors and try again.") %>

    <% Html.RenderPartial("AssessmentForm"); %>

    <div>
        <%=Html.ActionLink("Back to List", "Index", new { siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName() })%>
    </div>

</asp:Content>

