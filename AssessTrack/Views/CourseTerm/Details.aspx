<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<AssessTrack.Models.CourseTerm>" %>
<%@ Import Namespace="AssessTrack.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Details
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%= Html.Encode(Model.Name) %></h2>

    <fieldset>
        <legend>About this course</legend>
        <p>
            Information:
            <%= Html.Encode(Model.Information) %>
        </p>
        <p>
            Course:
            <%= Html.Encode(Model.Course.Name) %>
        </p>
        <p>
            Term:
            <%= Html.Encode(Model.Term.Name) %>
        </p>
        <% if (Model.File != null)
           { %>
        <p>
            Download Syllabus:
            <%= Html.ActionLink(Model.File.Name, "Download", new {controller="File", id = Model.Syllabus.Value}) %>
        </p>
        <%}
           else
           { %>
        <p>
            No Syllabus!
        </p>
        <% } %>
    </fieldset>
    <h3>Links</h3>
    <% Html.RenderPartial("CourseTermMenu"); %>
    
    <p>
        <%=Html.ActionLink("Back to List", "Index", new { siteShortName = Html.CurrentSiteShortName() })%>
    </p>

</asp:Content>

