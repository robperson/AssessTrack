<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<AssessTrack.Models.Site>" %>
<%@ Import Namespace="AssessTrack.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Details
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%= Html.Encode(Model.Title) %></h2>

    
        <p>
            Description:
            <%= Html.Encode(Model.Description) %>
        </p>
       
    <h3>Links</h3>
    <% Html.RenderPartial("SiteMenu"); %>
    <p>

        <%=Html.ATAuthLink("Edit", new { siteShortName = Model.ShortName, action = "Edit" }, AssessTrack.Filters.AuthScope.Site, 5, 10)%> <br />
        <%=Html.ActionLink("Back to List", "Index") %>
    </p>

</asp:Content>

