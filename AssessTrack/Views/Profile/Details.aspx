<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<AssessTrack.Models.ViewModels.ProfileDetailsViewModel>" %>
<%@ Import Namespace="AssessTrack.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Details
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Details</h2>

    <fieldset>
        <legend>Fields</legend>
        <p>
            Member's Name:
            <%= Html.Encode(UserHelpers.GetFullNameForID(Model.member.MembershipID)) %>
        </p>
        <p>
            AccessLevel:
            <%= Html.Encode(Model.member.AccessLevel) %>
        </p>
    </fieldset>

    <p>

        <%=Html.ActionLink("Edit", "Edit", new { id=Model.member.MembershipID }) %> |
        <%=Html.ActionLink("Back to List", "Index")%>
    </p>

    <h2>User's Course Enrollments</h2>

    <table>
        <tr>
            <th>
                Course Name
            </th>
            <th>
                Site
            </th>
        </tr>

    <% foreach (var item in Model.CourseTerms) { %>
    
        <tr>
            <td>
                <%= item.Name %>
            </td>
            <td>
                <%= item.Site.Title %>
            </td>
        </tr>
    
    <% } %>

    </table>

</asp:Content>

