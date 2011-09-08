<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<AssessTrack.Models.ReportsAndTools.GradeDistribution>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Class Grade Distribution
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Class Grade Distribution</h2>
    <% Html.RenderPartial("GradeDistributionTable", Model); %>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ExtraContent" runat="server">
</asp:Content>
