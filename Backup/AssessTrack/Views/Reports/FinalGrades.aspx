<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<AssessTrack.Models.CourseTermMember>>" %>
<%@ Import Namespace="AssessTrack.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Final Grades
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Final Grades</h2>

    <table>
        <tr>
            <th>
                Student Name
            </th>
            <th>
                Final Grade
            </th>
            <th>
                Final Grade (w/o Extra Credit)
            </th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td>
                <%= Html.RouteLink(item.Profile.FirstName + " " + item.Profile.LastName, new { controller = "Reports", id = item.MembershipID, action = "StudentPerformance", siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName() }, new { title = "Click to view student's Performance Report" })%>
            </td>
            <td>
                <%= Html.Encode(item.GetFinalLetterGrade()) %>
                (<%= Html.Encode(string.Format("{0:0.00}%",item.GetFinalGrade())) %>)
            </td>
            <td>
                <%= Html.Encode(item.GetFinalLetterGrade(false)) %>
                (<%= Html.Encode(string.Format("{0:0.00}%",item.GetFinalGrade(false))) %>)
            </td>
        </tr>
    
    <% } %>

    </table>

</asp:Content>

