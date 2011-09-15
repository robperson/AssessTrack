<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<AssessTrack.Models.Site>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Join
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Join <%= Model.Title %></h2>
    <p>
        <%= Html.ValidationSummary() %>
    </p>
    <p>
        <%= Model.Description %>
    </p>
    <% using (Html.BeginForm())
        { %>
        <%= Html.Hidden("id",Model.SiteID) %>
            
        <% if (!string.IsNullOrEmpty(Model.Password))
            { %>
            <p>
                <label for="Password">Password:</label>
                <%= Html.TextBox("Password")%>
            </p>
        <% } %>
        <p>                   
            <input type="submit" value="Join  <%= Model.Title %>" />
        </p>    
    <% } %>
       
   

</asp:Content>
