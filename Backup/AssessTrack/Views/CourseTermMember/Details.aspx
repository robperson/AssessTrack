<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<AssessTrack.Models.CourseTermMember>" %>
<%@ Import Namespace="AssessTrack.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Details
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Details</h2>

    <fieldset>
        <legend>Fields</legend>
        <p>
            Member's Name:
            <%= Html.Encode(UserHelpers.GetFullNameForID(Model.MembershipID)) %>
        </p>
        <p>
            AccessLevel:
            <%= Html.Encode(Model.AccessLevel) %>
        </p>
        <p>
            AccessCode:
            <%= Html.Encode(Model.AccessCode) %>
        </p>
    </fieldset>
    <p>

        <%=Html.ActionLink("Edit", "Edit", new { id=Model.CourseTermMemberID, siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName() }) %> |
        <%=Html.ActionLink("Back to List", "Index", new { siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName() })%>
    </p>

</asp:Content>

