<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<AssessTrack.Models.ViewModels.InviteModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Create Invite
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Create Invite</h2>

    <%= Html.ValidationSummary("Create was unsuccessful. Please correct the errors and try again.") %>

    <% using (Html.BeginForm()) {%>

        <fieldset>
            <legend>Invite Details</legend>
            <p>
                <label for="SiteAccessLevel">Site Access Level</label>
                <%= Html.TextBox("SiteAccessLevel", Model.SiteAccessLevel) %>
            </p>
            <p>
                <label for="CourseTermID">Course Offering</label>
                <%= Html.DropDownList("CourseTermID",Model.CourseTermList) %>
            </p>
            <p>
                <label for="CourseTermAccessLevel">Course Offering Access Level:</label>
                <%= Html.TextBox("CourseTermAccessLevel", Model.CourseTermAccessLevel) %>
            </p>
            <p>
                <input type="submit" value="Create" />
            </p>
        </fieldset>

    <% } %>

    

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ExtraContent" runat="server">
</asp:Content>

