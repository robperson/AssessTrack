<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<AssessTrack.Controllers.PerformanceReportModel>" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Student Performance History
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">--%>

    <h2><%= Html.Encode(Model.Profile.FirstName) %> <%= Html.Encode(Model.Profile.LastName) %>'s Performance Over Time</h2>
    <% 
        /*String labels = "";
        String labelPos = "";
        int count = 0;
        String tempDate;
        foreach (var date in Model.gradeDist.dates)
        {
            tempDate = date.ToShortDateString();
            tempDate = tempDate.Substring(0, tempDate.LastIndexOf("/"));
            labels += tempDate + "|";
            labelPos += count + ",";
            count++;
        }
        labelPos = labelPos.Substring(0, labelPos.Length-1);
        labels = labels.Substring(0, labels.Length - 1);
        String grades = "";
        foreach (var grade in Model.gradeDist.grades)
        {
            grades += grade + ",";
        }
        grades = grades.Substring(0, grades.Length-1);
        String chartUrl = "http://chart.apis.google.com/chart";
           chartUrl += "?chxl=1:|" + labels;
           chartUrl += "&chxp=1," + labelPos;
           chartUrl += "&chxr=0,0,100|1,0," + (count-1);
           chartUrl += "&chxs=1,676767,11.5,0,lt,676767";
           chartUrl += "&chxt=y,x";
           chartUrl += "&chs=600x400";
           chartUrl += "&cht=lc";
           chartUrl += "&chco=C2D6EB";
           //chartUrl += "&chd=s:tjcfc";
           chartUrl += "&chd=t:" + grades;
           chartUrl += "&chdl=Grade";
           chartUrl += "&chdlp=t";
           chartUrl += "&chg=14.3,-1,1,1";
           chartUrl += "&chls=4,4,0";
           chartUrl += "&chm=B,C5D4B5BB,0,0,0";
           chartUrl += "&chtt=Grade+Summary";*/
        %>
    <div id="gradeChart">
        <img src="<%= Model.chartUrl %>" width="600" height="300" alt="Grade Summary" />
    </div>
    
    <%--<table>
        <tr>
            <% foreach(var date in Model.gradeDist.dates) 
              {%>
              <td><%= date.ToString() %></td>
              <%} %>
        </tr>
        <tr>
            <%foreach(var grade in Model.gradeDist.grades) 
              {%>
              <td><%= grade %></td>
              <%} %>
        </tr>
      </table>--%>
<%--</asp:Content>
--%>