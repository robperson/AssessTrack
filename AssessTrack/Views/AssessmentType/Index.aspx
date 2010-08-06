<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<AssessTrack.Models.AssessmentType>>" %>
<%@ Import Namespace="AssessTrack.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Assessment Types
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Assessment Types</h2>

    <table>
        <caption>All Types</caption>
        <tr>
           <th>
                Name
            </th>
            <th>
                Weight
            </th>
            <th>
                Is Test Bank
            </th>
            
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td>
                <strong><%= Html.Encode(item.Name) %></strong>
                <div class="row-actions">
                <%= Html.ActionLink("Edit", "Edit", new { id = item.AssessmentTypeID, siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName() })%> |
                <%= Html.ActionLink("Details", "Details", new { id = item.AssessmentTypeID, siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName() })%>
                </div>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:F}", item.Weight)) %>
            </td>
            <td>
                <%= Html.Encode(item.QuestionBank.ToString()) %>
            </td>
        </tr>
    
    <% } %>

    </table>

    <p>
        <%= Html.ActionLink("Create A New Assessment Type", "Create", new { siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName() })%>
    </p>

</asp:Content>

