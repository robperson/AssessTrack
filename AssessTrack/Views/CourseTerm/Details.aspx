<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<AssessTrack.Models.CourseTerm>" %>
<%@ Import Namespace="AssessTrack.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%= Model.Name %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%= Model.Name %></h2>
    <h3>General Information</h3>
    <p><%= Model.Course.Description %></p>
    <h3>Information for this semester</h3>
    <div class="courseterm-information">
        <%= Model.Information %>
    </div>
    
    <p>
        <%= Html.ActionLink("Click here to enroll in this course.", "Join", new { siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Model.ShortName }) %>
    </p>

</asp:Content>


