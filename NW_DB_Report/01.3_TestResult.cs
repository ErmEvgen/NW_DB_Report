using System;
using Autodesk.Navisworks.Api;

namespace NW_DB_Report
{
    public class TestResult
    {
        public ElementProperties Element1 { get; set; }

        public ElementProperties Element2 { get; set; }

        public Point3D Center { get; set; }

        public string KonfliktInNavis { get; set; }

        public TestResult(ElementProperties element1, ElementProperties element2, Point3D center)
        {
            this.Element1 = element1 ?? throw new ArgumentNullException(nameof(element1));
            this.Element2 = element2 ?? throw new ArgumentNullException(nameof(element2));
            this.Center = center;
        }
    }
}