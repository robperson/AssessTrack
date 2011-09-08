<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Reset Password
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Reset Password</h2>
    <p>Enter your username so that we may email you your new password. Once you log in with
    your new password you can change it to whatever you like.</p>
    <%= Html.ValidationSummary() %>
    <% using(Html.BeginForm()){ %>
        <p>
        <label for="email">Email Address: </label>
        <%= Html.TextBox("email") %>
        </p>
        <p>
            <input type="submit" value="Send My Password!" />
        </p>
    <%} %>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ExtraContent" runat="server">
</asp:Content>
