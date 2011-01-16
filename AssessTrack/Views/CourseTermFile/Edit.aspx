<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<AssessTrack.Models.File>" %>
<%@ Import Namespace="AssessTrack.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Edit</h2>
    <%= Html.ValidationSummary("Upload was unsuccessful. Please correct the errors and try again.") %>

    <% using (Html.BeginForm("Edit","CourseTermFile", FormMethod.Post, new { enctype = "multipart/form-data" }))
       {%>

        <fieldset>
            <p>
                <label for="Title">Title (Optional)</label>
                <%= Html.TextBox("Title",Model.Title) %>
            </p>
            <p>
                <label for="Description">Description (Optional)</label>
                <%= Html.TextBox("Description",Model.Description) %>
                
            </p>
            <p>
                <label for="FileUpload">Select File</label>
                <input type="file" name="FileUpload" />
            </p>
            <p>
                <input type="submit" value="Update" />
            </p>
        </fieldset>

    <% } %>

    <div>
        <%= Html.ATAuthLink("Back to list", new { controller = "CourseTermFile", action = "Index", siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName() }, AssessTrack.Filters.AuthScope.CourseTerm, 1, 10)%>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ExtraContent" runat="server">
</asp:Content>
