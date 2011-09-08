<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Home
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Welcome!</h2>
    <p>Welcome to AssessTrack.com. AssessTrack is a Learning Management System that focuses on easy 
    assessment taking and powerful reporting tools. It is an excellent way to enhance in-class learning or to provide 
    online-only learning.</p>
    <p>
        Do you already have an account? <%= Html.ActionLink("Click here to log on", "LogOn", "Account")%>. If not,
        <%= Html.ActionLink("Click here to register", "Register", "Account") %>.
    </p>
    <h2>Features</h2>
    <p>Some of AssessTrack's ground-breaking features include:</p>
    <ul>
        <li>Powerful tagging features</li>
        <li>Generate reports to measure performance based on pre-defined Program Outcomes and Learning Objectives</li>
        <li>Create assessments with a drag-and-drop editor</li>
        <li>Completely online assessment taking</li>
        <li>Reminders to keep students and instructors informed of important information</li>
        <li>And more... !</li>
    </ul>
</asp:Content>
