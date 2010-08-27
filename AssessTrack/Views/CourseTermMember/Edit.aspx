<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<AssessTrack.Models.CourseTermMember>" %>
<%@ Import Namespace="AssessTrack.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Edit</h2>

    <%= Html.ValidationSummary("Edit was unsuccessful. Please correct the errors and try again.") %>

    <% using (Html.BeginForm()) {%>

        <fieldset>
            <legend>Fields</legend>
            <p>
                User's Name: <%= Html.Encode(UserHelpers.GetFullNameForID(Model.MembershipID)) %>
            </p>
            <p>
                <label for="AccessLevel">AccessLevel:</label>
                <%= Html.TextBox("AccessLevel", Model.AccessLevel) %>
                <%= Html.ValidationMessage("AccessLevel", "*") %>
            </p>
            <p>
                <label for="AccessCode">Access Code:</label>
                <%= Html.TextBox("AccessCode", Model.AccessCode ?? "") %>
                <%= Html.ValidationMessage("AccessCode", "*") %>
            </p>
            <p>
                <input type="submit" value="Save" />
            </p>
        </fieldset>

    <% } %>

    <div>
        <%=Html.ActionLink("Back to List", "Index", new { siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName() }) %>
    </div>

</asp:Content>

