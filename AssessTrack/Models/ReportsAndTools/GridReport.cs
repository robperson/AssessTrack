using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AssessTrack.Helpers;

namespace AssessTrack.Models.ReportsAndTools
{
    public class GridReport<XType,YType>
    {
        public List<XType> XItems;
        public List<YType> YItems;

        public bool ShowColumnTotals;
        public bool ShowColumnAverages;
        public bool ShowColumnPfmes;
        public bool ShowColumnGrades;


        private double[] ColumnTotals;
        private double detailTotal;

        private double[,] cellValues;

        public GridReport(List<XType> x, List<YType> y, 
            Func<YType, string> ylabel,
            Func<YType, string> ydetail,
            Func<YType, double> yval,
            Func<XType,string> xlabel,
            Func<XType,YType,double> cellval,
            bool showcoltotals, bool showcolavgs, bool showcolpfmes, bool showcolgrade)
        {
            if (x == null)
                throw new ArgumentNullException("x");
            if (y == null)
                throw new ArgumentNullException("y");
            if (ylabel == null)
                throw new ArgumentNullException("ylabel");
            if (xlabel == null)
                throw new ArgumentNullException("xlabel");
            if (cellval == null)
                throw new ArgumentNullException("cellval");

            XItems = x;
            YItems = y;
            
            ShowColumnAverages = showcolavgs;
            ShowColumnGrades = showcolgrade;
            ShowColumnPfmes = showcolpfmes;
            ShowColumnTotals = showcoltotals;

            PrintXLabel = xlabel;
            PrintYDetail = ydetail;
            PrintYLabel = ylabel;
            GetCellValue = cellval;
            GetYValue = yval;

            cellValues = new double[x.Count, y.Count];
            ColumnTotals = new double[x.Count];

            int x_idx;
            int y_idx;

            double total;

            foreach (var xvalue in x)
            {
                total = 0.0;
                x_idx = x.IndexOf(xvalue);
                foreach (var yvalue in y)
                {
                    
                    y_idx = y.IndexOf(yvalue);

                    cellValues[x_idx, y_idx] = GetCellValue(xvalue, yvalue);
                    total += cellValues[x_idx, y_idx];
                }
                ColumnTotals[x_idx] = total;
            }

            detailTotal = 0.0;
            if (GetYValue != null)
            {
                foreach (var yitem in YItems)
                {
                    detailTotal += GetYValue(yitem);
                }
            }
        }

        public Func<YType, string> PrintYLabel;
        public Func<YType, string> PrintYDetail;
        private Func<YType, double> GetYValue;

        public Func<XType, string> PrintXLabel;

        private Func<XType, YType, double> GetCellValue;

        public string PrintCellValue(XType xitem, YType yitem)
        {
            int x_idx;
            int y_idx;

            x_idx = XItems.IndexOf(xitem);
            y_idx = YItems.IndexOf(yitem);

            return cellValues[x_idx, y_idx].ToString();
        }

        public string PrintDetailColumnTotal()
        {
            return detailTotal.ToString();
        }

        public string PrintColumnTotal(XType xitem)
        {
            int idx = XItems.IndexOf(xitem);
            return ColumnTotals[idx].ToString();
        }

        private double getColumnAvg(XType item)
        {
            int idx = XItems.IndexOf(item);
            double total = ColumnTotals[idx];
            double avg = total / YItems.Count;
            return avg;
        }

        public string PrintColumnAverage(XType xitem)
        {
            double avg = getColumnAvg(xitem);
            return avg.ToString("{0:0.00}");
        }

        public string PrintColumnGrade(XType xitem)
        {
            double avg = getColumnAvg(xitem);
            return GradeHelpers.GetFinalLetterGrade(avg);
        }

        public string PrintColumnPfme(XType xitem)
        {
            double avg = getColumnAvg(xitem);
            return GradeHelpers.GetPfme(avg);
        }
    }
}
