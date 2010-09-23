<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<AssessTrack.Models.CourseTermMessage>" %>
<%@ Import Namespace="AssessTrack.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	View Message - <%= Model.Subject %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%= Model.Subject %></h2>
    <p>Created by <%= UserHelpers.GetFullNameForID(Model.CreatedBy) %> on <%= Model.CreatedDate.ToString() %></p>
    <div>
            
            <%= Model.Body %>
        </div>
       
    <p>

        <%=Html.ActionLink("Back to List", "Index", new { siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName() })%>
    </p>

</asp:Content>

