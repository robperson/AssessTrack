<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<AssessTrack.Models.AssessmentType>>" %>
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
                Name
            </th>
            <th>
                Weight
            </th>
            <th>
                Is Extra Credit
            </th>
            <th>
                Course
            </th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td>
                <%= Html.ActionLink("Edit", "Edit", new { id = item.AssessmentTypeID, siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName() })%> |
                <%= Html.ActionLink("Details", "Details", new { id = item.AssessmentTypeID, siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName() })%>
            </td>
            <td>
                <%= Html.Encode(item.Name) %>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:F}", item.Weight)) %>
            </td>
            <td>
                <%= Html.Encode(item.QuestionBank.ToString()) %>
            </td>
            <td>
                <%= Html.Encode(item.CourseTerm.Name) %>
            </td>
        </tr>
    
    <% } %>

    </table>

    <p>
        <%= Html.ActionLink("Create New", "Create", new { siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName() })%>
    </p>

</asp:Content>

