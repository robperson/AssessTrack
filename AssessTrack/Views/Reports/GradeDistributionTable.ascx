<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<AssessTrack.Models.ReportsAndTools.GradeDistribution>" %>
<div class="barchart">
    <img src="http://chart.apis.google.com/chart?chxl=0:|A|B|C|D|F|Ungraded&chxr=1,-5,100&chxt=x,y&chbh=a&chs=300x225&cht=bvg&chco=C2D6EB&chd=t:<%= Model.ACount + "," + Model.BCount + "," + Model.CCount + "," + Model.DCount + "," + Model.FCount + "," + Model.UngradedCount%>&chtt=Grade+Distribution" width="300" height="225" alt="Grade Distribution" />

</div>
<table>
    <tr>
        <th>Grade</th>
        <th>Count</th>
    </tr>
    <tr>
        <td>A</td>
        <td><%= Model.ACount %></td>
    </tr>
    <tr>
        <td>B</td>
        <td><%= Model.BCount %></td>
    </tr>
    <tr>
        <td>C</td>
        <td><%= Model.CCount %></td>
    </tr>
    <tr>
        <td>D</td>
        <td><%= Model.DCount %></td>
    </tr>
    <tr>
        <td>F</td>
        <td><%= Model.FCount %></td>
    </tr>
    <tr>
        <td>Ungraded</td>
        <td><%= Model.UngradedCount %></td>
    </tr>
</table>
<p>Total Students: <%= Model.TotalCount %></p>
