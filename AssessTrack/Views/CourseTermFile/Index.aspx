<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<AssessTrack.Models.File>>" %>
<%@ Import Namespace="AssessTrack.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Files
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Files</h2>
<p>
        <%= Html.ATAuthLink("Upload a file", 
                        new 
                        { 
                            action="Upload", 
                            siteShortName = Html.CurrentSiteShortName(), 
                            courseTermShortName = Html.CurrentCourseTermShortName() 
                        },
                         AssessTrack.Filters.AuthScope.CourseTerm, 
                         5, 
                         10)
                %>
    </p>
    
    <% if (Model.Count() < 1)
       { %>
    <p>No files uploaded.</p>
    <%} %>
    <table>
        <tr>
            
            <th>
                Name
            </th>
            <th>
                Description
            </th>
            <th></th>
        </tr>
 <% if (Model.Count() < 1)
       { %>
    <tr><td colspan="3">No files uploaded.</td></tr>
    <%} %>
    <% foreach (var item in Model) { %>
    
        <tr>
            
            <td>
                <%= Html.Encode(item.Title ?? item.Name) %>
            </td>
            <td>
                <%= Html.Encode(item.Description) %>
            </td>
            <td>
                <%= Html.ATAuthLink("Edit", ""," | ",
                        new 
                        { 
                            action="Edit", 
                            id = item.FileID, 
                            siteShortName = Html.CurrentSiteShortName(), 
                            courseTermShortName = Html.CurrentCourseTermShortName() 
                        },
                         AssessTrack.Filters.AuthScope.CourseTerm, 
                         5, 
                         10)
                %>
                <%= Html.ATAuthLink("Delete", ""," | ",
                        new 
                        { 
                            action="Delete", 
                            id = item.FileID, 
                            siteShortName = Html.CurrentSiteShortName(), 
                            courseTermShortName = Html.CurrentCourseTermShortName() 
                        },
                         AssessTrack.Filters.AuthScope.CourseTerm, 
                         5, 
                         10)
                %>
                <%= Html.ATAuthLink("Download " + item.Name, 
                        new 
                        { 
                            action="Download",
                            controller="File", 
                            id = item.FileID
                        },
                         AssessTrack.Filters.AuthScope.CourseTerm, 
                         1, 
                         10)
                %>
            </td>
        </tr>
    
    <% } %>

    </table>

    <p>
        <%= Html.ATAuthLink("Upload a file", 
                        new 
                        { 
                            action="Upload", 
                            siteShortName = Html.CurrentSiteShortName(), 
                            courseTermShortName = Html.CurrentCourseTermShortName() 
                        },
                         AssessTrack.Filters.AuthScope.CourseTerm, 
                         5, 
                         10)
                %>
    </p>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ExtraContent" runat="server">
</asp:Content>

