using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CherwellTriangles.Models;

namespace CherwellTriangles.Controllers
{
    /// <summary>
    /// Represents a triangle in a grid.
    /// 
    /// </summary>
    public class TriangleController : ApiController
    {
        /// <summary>
        /// Triangle data is passed via POST request body.
        /// If row and column are specifed, vertices are populated.
        /// If vertices are specifed, row and column are populated.
        /// Only one or the other should be specified.
        /// </summary>
        /// 
        /// <param name="triangle">Row and Column or Verticies V1x,V1y V2x,V2y, V3x, V3y</param>
        /// <returns>populated triangle</returns>
        public Triangle Post([FromBody]Triangle triangle)
        {
            triangle.DoCalculations();
            return triangle;
        }
    }
}
