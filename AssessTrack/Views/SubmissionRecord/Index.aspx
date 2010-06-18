<%@ Import Namespace="AssessTrack.Helpers" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<AssessTrack.Controllers.SubmissionRecordViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Index</h2>
    <div>
    <% using (Html.BeginForm()) { %>
       <%=Html.DropDownList("id", Model.AssessmentList)%>
       <input type="submit" value="Get Submission Records"/>
    <% } %>
    
    </div>
    <table>
        <tr>
            <th></th>
            <th>
                Student
            </th>
            <th>
                SubmissionDate
            </th>
            <th>
                Grade
            </th>
            <th>
                GradedOn
            </th>
            <th>
                GradedBy
            </th>
            <th>
                Comments
            </th>
        </tr>

    <% foreach (var item in Model.Submissions) { %>
    
        <tr>
            <td>
                <%= Html.ActionLink("Grade", "Grade", new { id = item.SubmissionRecordID, siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName() })%>
                |
                <%= Html.ActionLink("View", "View", new { id = item.SubmissionRecordID, siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName() })%>
            </td>
            <td>
                <%= Html.Encode(UserHelpers.GetFullNameForID(item.StudentID))%>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:g}", item.SubmissionDate)) %>
            </td>
            <td>
                <%= Html.Encode(item.Score) %>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:g}", item.GradedOn)) %>
            </td>
            <td>
                <%= Html.Encode(UserHelpers.GetFullNameForID(item.GradedBy))%>
            </td>
            <td>
                <%= Html.Encode(item.Comments) %>
            </td>
        </tr>
    
    <% } %>

    </table>

</asp:Content>

