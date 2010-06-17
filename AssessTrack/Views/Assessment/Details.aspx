<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<AssessTrack.Models.Assessment>" %>
<%@ Import Namespace="AssessTrack.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Details
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Details</h2>

    <fieldset>
        <legend>Fields</legend>
        <p>
            Name:
            <%= Html.Encode(Model.Name) %>
        </p>
        <p>
            DueDate:
            <%= Html.Encode(String.Format("{0:g}", Model.DueDate)) %>
        </p>
        <p>
            IsExtraCredit:
            <%= Html.Encode(Model.IsExtraCredit) %>
        </p>
        <p>
            Assessment Type:
            <%= Html.Encode(Model.AssessmentType.Name) %>
        </p>
        <div>
            Data:
            <pre><%= Html.Encode(Model.Data) %></pre>
        </div>
        <p>
            CreatedDate:
            <%= Html.Encode(String.Format("{0:g}", Model.CreatedDate.ToShortDateString())) %>
        </p>
        <p>
            Course:
            <%= Html.Encode(Model.CourseTerm.Name) %>
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
        
    </fieldset>
    <p>

        <%=Html.ActionLink("Back to List", "Index", new { siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName() })%>
    </p>

</asp:Content>

