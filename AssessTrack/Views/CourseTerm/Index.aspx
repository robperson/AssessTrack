<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<AssessTrack.Models.CourseTerm>>" %>
<%@ Import Namespace="AssessTrack.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Course Offerings</h2>

    <table>
        <tr>
            <th></th>
            <th>Enrolled?</th>
           <th>
                Information
            </th>
            <th>
                Course
            </th>
            <th>
                Term
            </th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td>
                <%= Html.ActionLink(item.Name, "Details", new { courseTermShortName = item.ShortName })%>
            </td>
            <td>
            <%AssessTrack.Models.AssessTrackDataRepository dr = new AssessTrack.Models.AssessTrackDataRepository(); %>
            <%=  (dr.IsCourseTermMember(item,dr.GetLoggedInProfile()))? "Yes" : "No"%>
            </td>
            <td>
                <%= Html.Encode(item.Information) %>
            </td>
            <td>
                <%= Html.Encode(item.Course.Name) %>
            </td>
            <td>
                <%= Html.Encode(item.Term.Name) %>
            </td>
        </tr>
    
    <% } %>

    </table>

    <p>
        <%= Html.ATAuthLink("Create New Course Offering", new { action = "Create" }, AssessTrack.Filters.AuthScope.Site, 5, 10)%>
        <%= Html.ActionLink("Enroll in a Course Offering", "Join") %>
    </p>

</asp:Content>

