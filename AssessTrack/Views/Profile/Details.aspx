<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<AssessTrack.Models.Profile>" %>
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
    </fieldset>
    <p>

        <%=Html.ActionLink("Edit", "Edit", new { id=Model.MembershipID }) %> |
        <%=Html.ActionLink("Back to List", "Index")%>
    </p>

</asp:Content>
