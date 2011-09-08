<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<AssessTrack.Models.ViewModels.TagViewModel>>" %>
<%@ Import Namespace="AssessTrack.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Tutoring
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Tutoring</h2>
    <h3>Topics you need to review:</h3>
    
    <ul>
    <% foreach (var tag in Model)
       {%>
       <li>
       <p><strong><%= tag.Tag.DescriptiveName ?? tag.Tag.Name %></strong> - <%= tag.Tag.Description %></p>
       <p>You scored <%= tag.Score %>% on questions about <%= tag.Tag.DescriptiveName ?? tag.Tag.Name %>.
       <%= Html.RouteLink("Click here to view a tutorial.",new { id = tag.Tag.TagID, siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName(), controller = "Tag", action = "Tutorial" })%>    </p>
       <p><%= Html.RouteLink("Click here for a review.",new { id = tag.Tag.TagID, siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName(), controller = "Tutor", action = "TagReview" })%>    </p>
       </li>
       
    <% } %>
    </ul>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ExtraContent" runat="server">
</asp:Content>
