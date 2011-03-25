<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<AssessTrack.Controllers.SiteViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Join
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Join a Site</h2>
    <p>
        <%= Html.ValidationSummary() %>
    </p>
    <% foreach (var site in Model.Sites)
       {%>
       <div class="enroll">
            <h3><%=site.Title %></h3>
            <p>
            <%= site.Description %>
            </p>
            <% using (Html.BeginForm())
               { %>
                <%= Html.Hidden("id",site.SiteID) %>
            
                <% if (!string.IsNullOrEmpty(site.Password))
                   { %>
                    <p>
                        <label for="Password">Password:</label>
                        <%= Html.TextBox("Password")%>
                    </p>
                <% } %>
                <p>                   
                    <input type="submit" value="Join  <%= site.Title %>" />
                </p>    
            <% } %>
        </div>
    <% } %>

</asp:Content>
