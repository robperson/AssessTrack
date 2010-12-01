<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<AssessTrack.Models.ReportsAndTools.CourseOutcomeDetailsModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Course Outcome Details
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Course Outcome Details</h2>
    <h3><%= (string.IsNullOrEmpty(Model.CourseOutcome.DescriptiveName))? Model.CourseOutcome.Name : Model.CourseOutcome.DescriptiveName %> - <%= Model.CourseOutcome.Description %></h3>
    <% Html.RenderPartial("GridReportView", Model.Report); %>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ExtraContent" runat="server">
</asp:Content>
