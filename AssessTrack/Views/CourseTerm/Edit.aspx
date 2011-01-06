<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<AssessTrack.Controllers.CourseTermViewModel>" %>
<%@ Import Namespace="AssessTrack.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Edit</h2>
<%= Html.ValidationSummary("Edit was unsuccessful. Please correct the errors and try again.") %>

    <% using (Html.BeginForm("Edit", "CourseTerm", FormMethod.Post, new { enctype = "multipart/form-data" }))
   {%>

        <fieldset>
            <legend>Fields</legend>
            <p>
                <label for="Name">Name:</label>
                <%= Html.TextBox("Name",Model.CourseTerm.Name) %>
                <%= Html.ValidationMessage("Name", "*") %>
            </p>
            <p>
                <label for="ShortName">ShortName:</label>
                <%= Html.TextBox("ShortName", Model.CourseTerm.ShortName) %>
                <%= Html.ValidationMessage("ShortName", "*") %>
            </p>
            <p>
                <label for="Information">Information:</label>
                <%= Html.TextArea("Information",Model.CourseTerm.Information) %>
                <%= Html.ValidationMessage("Information", "*") %>
            </p>
            <p>
                <label for="CourseID">Course:</label>
                <%= Html.DropDownList("CourseID",Model.CourseList) %>
            </p>
            <p>
                <label for="TermID">Term:</label>
                <%= Html.DropDownList("TermID", Model.TermList) %>
            </p>
            <p>
                <label for="Password">Password:</label>
                <%= Html.TextBox("Password", Model.CourseTerm.Password) %>
            </p>
            <p>
                <label for="Syllabus">Upload Syllabus</label>
                <input type="file" name="Syllabus" />
            </p>
            <p>
                <input type="submit" value="Save" />
            </p>
        </fieldset>

    <% } %>

    <div>
        <%=Html.ActionLink("Back to List", "Index", new {siteShortName = Html.CurrentSiteShortName() })%>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ExtraContent" runat="server">
</asp:Content>
