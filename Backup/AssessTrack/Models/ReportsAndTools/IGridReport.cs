using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AssessTrack.Models.ReportsAndTools
{
    public interface IGridReport
    {
        List<object> XItems { get; set; }
        List<object> YItems { get; set; }

        bool ShowColumnTotals { get; set; }
        bool ShowColumnAverages { get; set; }
        bool ShowColumnPfmes { get; set; }
        bool ShowColumnGrades { get; set; }
        bool ShowRowAverages { get; set; }


        string PrintYLabel(object item);
        string PrintYDetail(object yitem);
        
        

        string PrintXLabel(object item);

        string PrintCellValue(object xitem, object yitem);

        string PrintDetailColumnTotal();

        string PrintColumnTotal(object xitem);



        string PrintColumnAverage(object xitem);

        string PrintColumnPercentage(object xitem);
        string PrintColumnGrade(object xitem);

        string PrintColumnPfme(object xitem);

        string PrintRowAverage(object yitem);
        string PrintTotalAverage();
    }
}
