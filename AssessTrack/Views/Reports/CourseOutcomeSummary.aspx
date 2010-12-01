<%@ Import Namespace="AssessTrack.Models" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<AssessTrack.Models.ReportsAndTools.IGridReport>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Course Outcome Summary
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Course Outcome Summary</h2>
    <% Html.RenderPartial("GridReportView") ; %>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ExtraContent" runat="server">
</asp:Content>
