<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<List<AssessTrack.Models.Term>>" %>
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
                StartDate
            </th>
            <th>
                EndDate
            </th>
            <th>
                Site
            </th>
            <th>
                Name
            </th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td>
                <%= Html.ActionLink("Edit", "Edit", new { id = item.TermID, siteShortName = Html.CurrentSiteShortName()})%> |
                <%= Html.ActionLink("Details", "Details", new { id = item.TermID, siteShortName = Html.CurrentSiteShortName() })%>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:g}", item.StartDate.ToShortDateString())) %>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:g}", item.EndDate.ToShortDateString())) %>
            </td>
            <td>
                <%= Html.Encode(item.Site.Title) %>
            </td>
            <td>
                <%= Html.Encode(item.Name) %>
            </td>
        </tr>
    
    <% } %>

    </table>

    <p>
        <%= Html.ActionLink("Create New", "Create", new { siteShortName = Html.CurrentSiteShortName() })%>
    </p>

</asp:Content>

