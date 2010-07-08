<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<AssessTrack.Controllers.AssessmentListViewModel>" %>
<%@ Import Namespace="AssessTrack.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Assessments
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Current Assessments</h2>

    <table>
        <tr>
            <th></th>
            <th>
                Name
            </th>
            <th>
                DueDate
            </th>
            <th>
                Is Extra Credit
            </th>
            <th>
                Assessment Type
            </th>
            <th>
                CreatedDate
            </th>
            <th>
                Course
            </th>
            <th>
                Is Visible
            </th>
            <th>
                Is Open
            </th>
            <th>
                Is Gradable
            </th>
            <th>
                Weight
            </th>
            <th>
                Allow Multiple Submissions
            </th>
        </tr>

    <% foreach (var item in Model.CurrentAssessments) { %>
    
        <tr>
            <td>
                <ul>
                    <li><%= Html.ATAuthLink("Details", new { action = "Details", id = item.AssessmentID ,siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName() }, AssessTrack.Filters.AuthScope.CourseTerm, 4, 10)%> </li>
                    <li><%= Html.ATAuthLink("Submit", new { action="Submit", id = item.AssessmentID, siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName() }, AssessTrack.Filters.AuthScope.CourseTerm, 1, 1)%></li>
                    <li><%= Html.ATAuthLink("Edit", new { action="Edit", id = item.AssessmentID, siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName() }, AssessTrack.Filters.AuthScope.CourseTerm, 4, 10)%></li>
                </ul>
            </td>
            <td>
                <%= Html.Encode(item.Name) %>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:g}", item.DueDate)) %>
            </td>
            <td>
                <%= Html.Encode(item.IsExtraCredit) %>
            </td>
            <td>
                <%= Html.Encode(item.AssessmentType.Name) %>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:g}", item.CreatedDate.ToShortDateString())) %>
            </td>
            <td>
                <%= Html.Encode(item.CourseTerm.Name) %>
            </td>
            <td>
                <%= Html.Encode(item.IsVisible) %>
            </td>
            <td>
                <%= Html.Encode(item.IsOpen) %>
            </td>
            <td>
                <%= Html.Encode(item.IsGradable) %>
            </td>
            <td>
                <%= Html.Encode(item.Weight) %>
            </td>
            <td>
                <%= Html.Encode(item.AllowMultipleSubmissions) %>
            </td>
        </tr>
    
    <% } %>

    </table>
    
    <h2>Past Due Assessments</h2>

    <table>
        <tr>
            <th></th>
            <th>
                Name
            </th>
            <th>
                DueDate
            </th>
            <th>
                Is Extra Credit
            </th>
            <th>
                Assessment Type
            </th>
            <th>
                CreatedDate
            </th>
            <th>
                Course
            </th>
            <th>
                Is Visible
            </th>
            <th>
                Is Open
            </th>
            <th>
                Is Gradable
            </th>
            <th>
                Weight
            </th>
            <th>
                Allow Multiple Submissions
            </th>
        </tr>

    <% foreach (var item in Model.PastAssessments) { %>
    
        <tr>
            <td>
                <ul>
                    <li><%= Html.ATAuthLink("Details", new { action = "Details", id = item.AssessmentID ,siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName() }, AssessTrack.Filters.AuthScope.CourseTerm, 4, 10)%> </li>
                    <li><%= Html.ATAuthLink("Edit", new { action="Edit", id = item.AssessmentID, siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName() }, AssessTrack.Filters.AuthScope.CourseTerm, 4, 10)%></li>
                </ul>    
            </td>
            <td>
                <%= Html.Encode(item.Name) %>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:g}", item.DueDate)) %>
            </td>
            <td>
                <%= Html.Encode(item.IsExtraCredit) %>
            </td>
            <td>
                <%= Html.Encode(item.AssessmentType.Name) %>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:g}", item.CreatedDate.ToShortDateString())) %>
            </td>
            <td>
                <%= Html.Encode(item.CourseTerm.Name) %>
            </td>
            <td>
                <%= Html.Encode(item.IsVisible) %>
            </td>
            <td>
                <%= Html.Encode(item.IsOpen) %>
            </td>
            <td>
                <%= Html.Encode(item.IsGradable) %>
            </td>
            <td>
                <%= Html.Encode(item.Weight) %>
            </td>
            <td>
                <%= Html.Encode(item.AllowMultipleSubmissions) %>
            </td>
        </tr>
    
    <% } %>

    </table>

    <p>
        <%= Html.ATAuthLink("Create New", new { Action = "Create", siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName() }, AssessTrack.Filters.AuthScope.Site, 5, 10)%>
    </p>

</asp:Content>

