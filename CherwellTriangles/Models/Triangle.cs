using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace CherwellTriangles.Models
{
    /// <summary>
    /// Represents a triangle in a grid.
    /// If row and column are specifed, vertices are populated.
    /// If vertices are specifed, row and column are populated.
    /// 
    /// </summary>
    public class Triangle
    {
        /// <summary>
        /// Calculate vertices if row and column are specified and row and column if vertices are specified
        /// </summary>
        public void DoCalculations()
        {
            Error = false;

            bool bRowColSpecified = false, bVerticesSpecified = false;

            if (!string.IsNullOrEmpty(Row) || Column != 0)
                bRowColSpecified = true;

            if (V1x != -1 || V1y != -1 || V2x != -1 || V2y != -1 || V3x != -1 || V3y != -1)
                bVerticesSpecified = true;

            if (bRowColSpecified && bVerticesSpecified)
            {
                Result = "Both row/column and vertices specified.  Only specify one and the other will be calculated.";
                Error = true;
                return;
            }

            if (!bRowColSpecified && !bVerticesSpecified)
            {
                Result = "Invalid input.";
                Error = true;
                return;
            }

            if (bRowColSpecified)
                CalculateVertices();

            if (bVerticesSpecified)
                GetRowAndColumn();
        }

        void GetRowAndColumn()
        {
            if (V1x < 0 || V1y < 0 || V2x < 0 || V2y < 0 || V3x < 0 || V3y < 0 ||
                V1x > 60 || V1y > 60 || V2x > 60 || V2y > 60 || V3x > 60 || V3y > 60 ||
                V1x % 10 != 0 || V1y % 10 != 0 || V2x % 10 != 0 || V2y % 10 != 0 || V3x % 10 != 0 || V3y % 10 != 0)
            {
                Result = "Invalid input... all vertices need to be between 0 and 60 and multiples of 10.";
                Error = true;
                return;
            }

            int left = Math.Min(V1x, V2x);
            left = Math.Min(left, V3x);

            int right = Math.Max(V1x, V2x);
            right = Math.Max(right, V3x);

            int top = Math.Min(V1y, V2y);
            top = Math.Min(top, V3y);

            int bottom = Math.Max(V1y, V2y);
            bottom = Math.Max(bottom, V3y);

            if (right - left != 10 || bottom - top != 10 ||
                (V1x == V2x && V1y == V2y) ||
                (V3x == V2x && V3y == V2y) ||
                (V1x == V3x && V1y == V3y))
            {
                Result = "Invalid input. Vertices don't create a triangle with correct height and width.";
                Error = true;
                return;
            }

            int rowNumber = top / RowHeight;
            char rowchar = (char)('A' + rowNumber);
            Row = rowchar.ToString();

            int column = right / ColumnWidth;

            Column = column * 2;

            int topcount = 0;
            if (V1y == top)
                topcount++;
            if (V2y == top)
                topcount++;
            if (V3y == top)
                topcount++;

            int rightcount = 0;
            if (V1x == right)
                rightcount++;
            if (V2x == right)
                rightcount++;
            if (V3x == right)
                rightcount++;

            int leftcount = 0;
            if (V1x == left)
                leftcount++;
            if (V2x == left)
                leftcount++;
            if (V3x == left)
                leftcount++;

            if ((rightcount == 2 && topcount == 1) || (leftcount == 2 && topcount == 2))
            {
                Result = "Invalid input. Vertices create a triangle with an upward slope.";
                Error = true;
                return;
            }

            if (topcount == 1) // if triangle has doesn't have 2 points at the top, it's the lower one.  decrement the column 
                Column--;

            Result = "Row:" + Row + " Column:" + Column + " - (" + V1x + "," + V1y + ") (" + V2x + "," + V2y + ") (" + V3x + "," + V3y + ")";
        }

        void CalculateVertices()
        {
            if (Column < 1 || Column > 12 || RowNumber < 0 || RowNumber > 5 || Row.Length > 1)
            {
                Result = "Invalid input. Row must be A-F. Column must be 1-12.";
                Error = true;
                return;
            }

            int ZeroBasedColumn = (Column - 1);
            int ColumnBlock = (int)(ZeroBasedColumn / 2); // truncates decimal

            V2x = ColumnBlock * ColumnWidth;
            V2y = (RowNumber) * RowHeight;

            if (Column % 2 == 1)
            {
                // odd - lower triangle
                V1x = V2x;
                V1y = V2y + RowHeight;
            }
            else
            {
                // even - upper triangle
                V1x = V2x + ColumnWidth;
                V1y = V2y;
            }

            V3x = V2x + ColumnWidth;
            V3y = V2y + RowHeight;

            Result = "Row:" + Row + " Column:" + Column + " - (" + V1x + "," + V1y + ") (" + V2x + "," + V2y + ") (" + V3x + "," + V3y + ")";
        }

        int GridRows { get { return Grid.Rows; } }
        int GridColumns { get { return Grid.Columns; } }

        int RowHeight { get { return Grid.RowHeight; } }
        int ColumnWidth { get { return Grid.ColumnWidth; } }

        /// <summary>
        /// A-F
        /// </summary>
        public string Row { get; set; }
        int RowNumber
        {
            get
            {
                if (string.IsNullOrEmpty(Row))
                    return -1;
                int rownumber = Row[0] - 'A';
                return rownumber;
            }
        }

        /// <summary>
        /// 1-12
        /// </summary>
        public int Column { get; set; } = 0;

        /// <summary>
        /// 0-60, multiple of 10
        /// </summary>
        public int V1x { get; set; } = -1;

        /// <summary>
        /// 0-60, multiple of 10
        /// </summary>
        public int V1y { get; set; } = -1;

        /// <summary>
        /// 0-60, multiple of 10
        /// </summary>
        public int V2x { get; set; } = -1;

        /// <summary>
        /// 0-60, multiple of 10
        /// </summary>
        public int V2y { get; set; } = -1;

        /// <summary>
        /// 0-60, multiple of 10
        /// </summary>
        public int V3x { get; set; } = -1;

        /// <summary>
        /// 0-60, multiple of 10
        /// </summary>
        public int V3y { get; set; } = -1;

        /// <summary>
        /// returns results or error message
        /// </summary>
        public string Result { get; private set; }

        /// <summary>
        /// returns true if validation or operation fails
        /// </summary>
        public bool Error { get; private set; } = false;
    }

}