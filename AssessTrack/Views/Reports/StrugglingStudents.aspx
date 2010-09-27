<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<AssessTrack.Models.CourseTermMember>>" %>
<%@ Import Namespace="AssessTrack.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Struggling Students
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Struggling Students</h2>

    <table>
        <tr>
            <th>
                Student Name
            </th>
            <th>
                Final Grade
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
        </tr>
    
    <% } %>

    </table>

</asp:Content>

