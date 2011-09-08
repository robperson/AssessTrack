<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<AssessTrack.Controllers.CourseTermMemberViewModel>" %>
<%@ Import Namespace="AssessTrack.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%= Model.CourseTerm.Name %>'s Members
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%= Model.CourseTerm.Name %>'s Members</h2>
    
     <% foreach (var table in Model.Tables) 
       {
           if (table.ShowDetails)
           {
               Html.RenderPartial("DetailedMemberTable", table);
           }
           else
           {
               Html.RenderPartial("MemberTable", table);
           }
       } %>
   
</asp:Content>

