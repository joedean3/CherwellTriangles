using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CherwellTriangles.Models
{
    /// <summary>
    /// // if we implement drawing the grid, this class would contain a collection of Triangles
    /// </summary>
    public class Grid
    {
        static public int Rows { get; set; } = 6;
        static public int Columns { get; set; } = 12;

        static public int RowHeight { get; set; } = 10;
        static public int ColumnWidth { get; set; } = 10;

    }
}