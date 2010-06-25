<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<AssessTrack.Models.SubmissionException>>" %>
<%@ Import Namespace="AssessTrack.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Index</h2>

    <table>
        <tr>
            <th></th>
            <th>
                Assessment
            </th>
            <th>
                Student
            </th>
            <th>
                Due Date
            </th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td>
                <%-- <%= Html.ActionLink("Edit", "Edit", new { id=item.SubmissionExceptionID }) %> |
                <%= Html.ActionLink("Details", "Details", new { id=item.SubmissionExceptionID })%> --%>
            </td>
            <td>
                <%= Html.Encode(item.Assessment.Name) %>
            </td>
            <td>
                <%= Html.Encode(AssessTrack.Helpers.UserHelpers.GetFullNameForID(item.StudentID)) %>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:g}", item.DueDate)) %>
            </td>
        </tr>
    
    <% } %>

    </table>

    <p>
        <%= Html.ActionLink("Create New", "Create", new { siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName() })%>
    </p>

</asp:Content>

