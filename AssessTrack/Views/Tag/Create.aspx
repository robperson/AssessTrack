<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<AssessTrack.Models.ViewModels.TagEditViewModel>" %>
<%@ Import Namespace="AssessTrack.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Create A New Tag
</asp:Content>
<asp:Content ID="Head" ContentPlaceHolderID="HeadContent" runat="server">
<script src="/Scripts/jquery.autogrowtextarea.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Create A New Tag</h2>

    <%= Html.ValidationSummary("Create was unsuccessful. Please correct the errors and try again.") %>

    <% Html.RenderPartial("TagForm"); %>

    <div>
        <%=Html.ActionLink("Back to List", "Index", new { siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName() })%>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ExtraContent" runat="server">
<script type="text/javascript">
    $('textarea').autogrow();
</script>
</asp:Content>