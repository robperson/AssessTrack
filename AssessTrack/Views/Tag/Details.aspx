<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<AssessTrack.Controllers.TagDetailsModel>" %>
<%@ Import Namespace="AssessTrack.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Details
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Details</h2>

    <fieldset>
        <legend>Fields</legend>
        <p>
            TagID:
            <%= Html.Encode(Model.Tag.TagID) %>
        </p>
        <p>
            Name:
            <%= Html.Encode(Model.Tag.Name) %>
        </p>
        <p>
            Description:
            <%= Html.Encode(Model.Tag.Description) %>
        </p>
        
    </fieldset>
    <h3>Tagged Assessments</h3>
    <table>
    <tr>
        <th>Name</th>
        <th>Weight</th>
    </tr>
    <% foreach (var assessment in Model.Assessments) { %>
        <tr>
            <td>
                <%= Html.Encode(assessment.Name) %>
            </td>
            <td>
                <%= Html.Encode(assessment.Weight) %>
            </td>
        </tr>
    <% } %>
    </table>
    <h3>Tagged Questions</h3>
    <table>
    <tr>
        <th>Name</th>
        <th>Weight</th>
    </tr>
    <% foreach (var question in Model.Questions) { %>
        <tr>
            <td>
                <%= Html.Encode(question.Assessment.Name) %>,
                Question #<%= Html.Encode(question.Number) %>
            </td>
            <td>
                <%= Html.Encode(question.Weight) %>
            </td>
        </tr>
    <% } %>
    </table>
    <h3>Tagged Answers</h3>
    <table>
    <tr>
        <th>Name</th>
        <th>Weight</th>
    </tr>
    <% foreach (var answer in Model.Answers) { %>
        <tr>
            <td>
                <%= Html.Encode(answer.Assessment.Name) %>,
                Question #<%= Html.Encode(answer.Question.Number) %>,
                Answer #<%= Html.Encode(answer.Number) %>
            </td>
            <td>
                <%= Html.Encode(answer.Weight) %>
            </td>
        </tr>
    <% } %>
    </table>
    <p>

        <%=Html.ActionLink("Edit", "Edit", new { id = Model.Tag.TagID, siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName() })%> |
        <%=Html.ActionLink("Back to List", "Index", new { siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName() })%>
    </p>

</asp:Content>

