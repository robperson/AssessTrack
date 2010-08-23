<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Code Download Link
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Code Download Link</h2>
    <p>
    <% if (ViewData["CodeDownloadLink"] != null)
       { %>
    <a href="<%= ViewData["CodeDownloadLink"] %>">Click here to download the code</a>. The file will be deleted soon.
    <%} else { %>
    This page is no longer valid. You'll have to generate the download link again.
    <%} %>
    </p>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ExtraContent" runat="server">
</asp:Content>
