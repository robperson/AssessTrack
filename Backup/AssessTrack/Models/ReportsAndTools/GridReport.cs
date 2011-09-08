using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AssessTrack.Helpers;

namespace AssessTrack.Models.ReportsAndTools
{
    public class GridReport<XType,YType>: IGridReport
    {
        private List<XType> xItems;
        private List<YType> yItems;



        public bool ShowColumnTotals { get; set; }
        public bool ShowColumnAverages { get; set; }
        public bool ShowColumnPfmes { get; set; }
        public bool ShowColumnGrades { get; set; }
        public bool ShowRowAverages { get; set; }


        private double[] ColumnTotals;
        private double[] RowTotals;
        private double[] RowAverages;
        private double detailTotal;
        private double totalAverage;

        private double[,] cellValues;

        public GridReport(List<XType> x, List<YType> y, 
            Func<YType, string> ylabel,
            Func<YType, string> ydetail,
            Func<YType, double> yval,
            Func<XType,string> xlabel,
            Func<XType,YType,double> cellval,
            bool showcoltotals, bool showcolavgs, bool showcolpfmes, bool showcolgrade, bool showrowavgs)
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

            xItems = x;
            yItems = y;

            XItems = xItems.ConvertAll(item => (object)item);
            YItems = yItems.ConvertAll(item => (object)item);
            
            ShowColumnAverages = showcolavgs;
            ShowColumnGrades = showcolgrade;
            ShowColumnPfmes = showcolpfmes;
            ShowColumnTotals = showcoltotals;
            ShowRowAverages = showrowavgs;

            printXLabel = xlabel;
            printYDetail = ydetail;
            printYLabel = ylabel;
            GetCellValue = cellval;
            GetYValue = yval;

            cellValues = new double[x.Count, y.Count];
            ColumnTotals = new double[x.Count];
            RowTotals = new double[y.Count];
            RowAverages = new double[y.Count];

            for (int i = 0; i < y.Count; i++)
            {
                RowTotals[i] = 0.0;
            }

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
                    RowTotals[y_idx] += cellValues[x_idx, y_idx];
                }
                ColumnTotals[x_idx] = total;
            }


            detailTotal = 0.0;
            if (GetYValue != null)
            {
                foreach (var yitem in yItems)
                {
                    detailTotal += GetYValue(yitem);
                }
            }

            
            if (ShowRowAverages)
            {
                for (int i = 0; i < y.Count; i++)
                {
                    RowAverages[i] = RowTotals[i] / (double)x.Count;
                }

                if (ShowColumnTotals)
                {
                    totalAverage = ColumnTotals.Sum() / detailTotal;
                }
            }
        }

        private Func<YType, string> printYLabel;
        private Func<YType, string> printYDetail;
        
        private Func<YType, double> GetYValue;

        private Func<XType, string> printXLabel;

        private Func<XType, YType, double> GetCellValue;

        private double getColumnAvg(XType item)
        {
            int idx = xItems.IndexOf(item);
            double total = ColumnTotals[idx];
            double avg = total / yItems.Count;
            return avg;
        }

        private double getColumnPct(XType item)
        {
            int idx = xItems.IndexOf(item);
            double total = ColumnTotals[idx];
            double pct = total / detailTotal * 100;
            return pct;
        }

        #region IGridReport Members


        public string PrintYLabel(object item)
        {
            return printYLabel((YType)item);
        }

        public string PrintYDetail(object yitem)
        {
            if (printYDetail != null)
            {
                return printYDetail((YType)yitem);
            }
            else
            {
                return "";
            }
        }

        public string PrintXLabel(object item)
        {
            return printXLabel((XType)item);
        }

        public string PrintCellValue(object xitem, object yitem)
        {
            int x_idx;
            int y_idx;

            x_idx = xItems.IndexOf((XType)xitem);
            y_idx = yItems.IndexOf((YType)yitem);

            return cellValues[x_idx, y_idx].ToString("0.00");
        }

        public string PrintDetailColumnTotal()
        {
            return detailTotal.ToString("0.00");
        }

        public string PrintColumnTotal(object xitem)
        {
            int idx = xItems.IndexOf((XType)xitem);
            return ColumnTotals[idx].ToString("0.00");
        }

        public string PrintColumnAverage(object xitem)
        {
            double avg = getColumnAvg((XType)xitem);
            return avg.ToString("0.00");        
        }

        public string PrintColumnPercentage(object xitem)
        {
            if (!ShowColumnTotals)
                return "N/A";

            double pct = getColumnPct((XType)xitem);

            return string.Format("{0:0.00}", pct);
        }
        public string PrintColumnGrade(object xitem)
        {
            double avg = getColumnPct((XType)xitem);
            return GradeHelpers.GetFinalLetterGrade(avg);
        }

        public string PrintColumnPfme(object xitem)
        {
            double avg = getColumnPct((XType)xitem);
            return GradeHelpers.PrintPfme(avg);
        }

        public string PrintRowAverage(object yitem)
        {
            int idx = yItems.IndexOf((YType)yitem);
            return RowAverages[idx].ToString("0.00");
        }

        public string PrintTotalAverage()
        {
            return totalAverage.ToString("0.00");
        }

        #endregion

        #region IGridReport Members

        public List<object> XItems
        {
            get;
            set;
        }

        public List<object> YItems
        {
            get;
            set;
        }

        #endregion
    }
}
