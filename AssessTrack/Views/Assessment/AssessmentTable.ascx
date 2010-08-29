<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<AssessTrack.Controllers.AssessmentTableModel>" %>
<%@ Import Namespace="AssessTrack.Helpers" %>
<table>
        <caption><%= Html.Encode(Model.Caption) %></caption>
        <tr>
            <th>
                Name
            </th>
            <th>
                Due Date
            </th>
            <th>
                Assessment Type
            </th>
            <th>
                Weight
            </th>
        </tr>
        
        <% foreach (var item in Model.Assessments) { %>
    
        <tr>
            <td>
                <strong><%= Html.ATAuthLink(item.Name, new { action = "Details", id = item.AssessmentID ,siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName() }, AssessTrack.Filters.AuthScope.CourseTerm, 1, 10)%> </strong>
                <div class="row-actions">
                    <%= Html.ATAuthLink("Submit", new { action="Submit", id = item.AssessmentID, siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName() }, AssessTrack.Filters.AuthScope.CourseTerm, 1, 1)%>
                    <%= Html.ATAuthLink("Edit", "", " | ", new { action="Edit", id = item.AssessmentID, siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName() }, AssessTrack.Filters.AuthScope.CourseTerm, 4, 10)%>
                    <%= Html.ATAuthLink("Submissions", "", " | ", new { controller = "SubmissionRecord", action="Index", id = item.AssessmentID, siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName() }, AssessTrack.Filters.AuthScope.CourseTerm, 2, 10)%>
                    <%= Html.ATAuthLink("Preview", "", "", new { controller = "Assessment", action="Preview", id = item.AssessmentID, siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName() }, AssessTrack.Filters.AuthScope.CourseTerm, 5, 10)%>
                </div>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:g}", item.DueDate)) %>
            </td>
            <td>
                <%= Html.Encode(item.AssessmentType.Name) %>
            </td>
            <td>
                <%= Html.Encode(item.Weight) %>
            </td>
        </tr>
    
    <% } %>
        

    </table>
