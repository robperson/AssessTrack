<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<AssessTrack.Models.Invitation>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Accept Invite
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Accept Invite</h2>

    <p>
        Use the form below to create a new account. When you create your account you will have access to
        <strong>"<%= Model.Site.Title %>
        <% if (Model.CourseTerm != null)
           { %>
           - <%= Model.CourseTerm.Name %>"</strong>.
        <% } %>
    </p>
    <p>
        Passwords are required to be a minimum of <%=Html.Encode(ViewData["PasswordLength"])%> characters in length.
    </p>
    <%= Html.ValidationSummary("Account creation was unsuccessful. Please correct the errors and try again.") %>

    <% using (Html.BeginForm()) { %>
        <div>
            <fieldset>
                <legend>Account Information</legend>
                <p>
                    <label for="username">Username:</label>
                    <%= Html.TextBox("username") %>
                    <%= Html.ValidationMessage("username") %>
                </p>
                <p>
                    <label for="email">Email:</label>
                    <span><%= Model.Email %></span>
                </p>
                <p>
                    <label for="password">Password:</label>
                    <%= Html.Password("password") %>
                    <%= Html.ValidationMessage("password") %>
                </p>
                <p>
                    <label for="confirmPassword">Confirm password:</label>
                    <%= Html.Password("confirmPassword") %>
                    <%= Html.ValidationMessage("confirmPassword") %>
                </p>
                <p>
                    <label for="FirstName">First Name:</label>
                    <%= Html.TextBox("FirstName") %>
                    <%= Html.ValidationMessage("FirstName") %>
                </p>
                <p>
                    <label for="LastName">Last Name:</label>
                    <%= Html.TextBox("LastName") %>
                    <%= Html.ValidationMessage("LastName") %>
                </p>
                <p>
                    <label for="SchoolStudentID">Student ID Number:</label>
                    <%= Html.TextBox("SchoolStudentID") %>
                    <%= Html.ValidationMessage("SchoolStudentID") %>
                </p>
                <p>
                    <label for="Major">Major:</label>
                    <%= Html.TextBox("Major") %>
                    <%= Html.ValidationMessage("Major") %>
                </p>
                <p>
                    <input type="submit" value="Register" />
                </p>
            </fieldset>
        </div>
    <% } %>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ExtraContent" runat="server">
</asp:Content>
