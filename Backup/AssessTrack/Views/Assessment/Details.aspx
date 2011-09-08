<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<AssessTrack.Models.Assessment>" %>
<%@ Import Namespace="AssessTrack.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Assessment Details
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Assessment Details</h2>

    <h3><%= Html.Encode(Model.Name) %></h3>
            
        <p>
            Due Date:
            <%= Html.Encode(String.Format("{0:g}", Model.DueDate)) %>
        </p>
        <p>
            Is Extra Credit:
            <%= Html.Encode(Model.IsExtraCredit) %>
        </p>
        <p>
            Assessment Type:
            <%= Html.Encode(Model.AssessmentType.Name) %>
        </p>
        <p>
            Created Date:
            <%= Html.Encode(String.Format("{0:g}", Model.CreatedDate.ToShortDateString())) %>
        </p>
        <p>
            Is Visible:
            <%= Html.Encode(Model.IsVisible) %>
        </p>
        <p>
            Is Open:
            <%= Html.Encode(Model.IsOpen) %>
        </p>
        <p>
            Is Gradable:
            <%= Html.Encode(Model.IsGradable) %>
        </p>
        
    
    <p>

        <%=Html.ActionLink("Back to Assessment list", "Index", new { siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName() })%>
    </p>

</asp:Content>

