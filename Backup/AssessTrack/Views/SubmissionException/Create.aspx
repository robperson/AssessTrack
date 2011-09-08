<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<AssessTrack.Controllers.SubmissionExceptionFormModel>" %>
<%@ Import Namespace="AssessTrack.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Create A Submission Exception
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Create A Submission Exception</h2>
    <%= Html.ValidationSummary("Create was unsuccessful. Please correct the errors and try again.") %>
    <% using (Html.BeginForm())
       { %>
        <p>
                <label for="AssessmentID">Assessment:</label>
                <%= Html.DropDownList("AssessmentID", Model.AssessmentsList)%>
            </p>
            <p>
                <label for="StudentID">Student:</label>
                <%= Html.DropDownList("StudentID", Model.StudentsList)%>
            </p>
            <p>
                <label for="DueDate">New Due Date:</label>
                <%= Html.TextBox("DueDate", Model.DueDate) %>
                <%= Html.ValidationMessage("DueDate","*") %>
            </p>
            <p>
                <input type="submit" value="Create!" />
            </p>
       
    <% } %>
    <p>
        
        <%=Html.ActionLink("Back to List", "Index", new { siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName() }) %>
    </p>

</asp:Content>

