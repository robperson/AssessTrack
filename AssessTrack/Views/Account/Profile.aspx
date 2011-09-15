<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<AssessTrack.Models.Profile>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Profile
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Edit Profile</h2>

    <p>Click here to <%= Html.ActionLink("change your password","ChangePassword") %>.</p>
    <p>Edit your profile below.</p>
    <%= Html.ValidationSummary("Edit was unsuccessful. Please correct the errors and try again.") %>

    <% using (Html.BeginForm()) {%>

        <p>
            <label for="FirstName">First Name:</label>
            <%= Html.TextBox("FirstName", Model.FirstName) %>
            <%= Html.ValidationMessage("FirstName", "*") %>
        </p>
        <p>
            <label for="LastName">Last Name:</label>
            <%= Html.TextBox("LastName", Model.LastName) %>
            <%= Html.ValidationMessage("LastName", "*") %>
        </p>
        <p>
            <label for="SchoolIDNumber">School ID Number:</label>
            <%= Html.TextBox("SchoolIDNumber", Model.SchoolIDNumber) %>
            <%= Html.ValidationMessage("SchoolIDNumber", "*") %>
        </p>
        <p>
            <label for="Major">Major:</label>
            <%= Html.TextBox("Major", Model.Major) %>
            <%= Html.ValidationMessage("Major", "*") %>
        </p>
        <p>
            <label for="EmailAddress">EmailAddress:</label>
            <%= Html.TextBox("EmailAddress", Model.EmailAddress)%>
            <%= Html.ValidationMessage("EmailAddress", "*")%>
        </p>
        
        <p>
            <input type="submit" value="Save Changes" />
        </p>
        

    <% } %>

</asp:Content>
