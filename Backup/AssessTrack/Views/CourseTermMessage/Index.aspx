<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<AssessTrack.Models.CourseTermMessage>>" %>
<%@ Import Namespace="AssessTrack.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Messages
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Messages</h2>

    <table>
        <caption>All Messages</caption>
        <tr>
           <th>
                Subject
            </th>
            <th>
                Body
            </th>
            
            
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td>
                <strong><%= Html.ActionLink(item.Subject, "Details", new { id = item.MessageID, siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName() })%></strong>
                <div class="row-actions">
                <%= Html.ATAuthLink("Edit", new { action = "Edit", controller = "CourseTermMessage", id = item.MessageID, siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName() },AssessTrack.Filters.AuthScope.CourseTerm,5,10)%>
                
                </div>
            </td>
            <td>
                <%= (item.Body.Length > 100)? item.Body.Substring(0,100) + "..." : item.Body %>
            </td>
            
        </tr>
    
    <% } %>

    </table>
    
    <p>
        <%= Html.ATAuthLink("Create a new message", new { action = "Create", controller = "CourseTermMessage", siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName() },AssessTrack.Filters.AuthScope.CourseTerm,5,10)%>
    </p>

</asp:Content>

