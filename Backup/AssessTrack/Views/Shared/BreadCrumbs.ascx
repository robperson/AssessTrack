<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="AssessTrack.Helpers" %>
<p class="breadcrumbs">
<%= Html.CurrentSiteLink("","") %>
<%= Html.CurrentCourseTermLink(" &raquo; ","") %>
</p>