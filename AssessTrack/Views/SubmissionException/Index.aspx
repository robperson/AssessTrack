<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<AssessTrack.Models.SubmissionException>>" %>
<%@ Import Namespace="AssessTrack.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Submission Exceptions
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Submission Exceptions</h2>

    <table>
    <caption>All Exceptions</caption>
        <tr>
            <th>
                Assessment
            </th>
            <th>
                Student
            </th>
            <th>
                New Due Date
            </th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
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
        <%= Html.ActionLink("Create A New Submission Exception", "Create", new { siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName() })%>
    </p>

</asp:Content>

