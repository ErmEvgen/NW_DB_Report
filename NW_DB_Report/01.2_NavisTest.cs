using System;
using System.Collections.Generic;
using Autodesk.Navisworks.Api.Clash;

namespace NW_DB_Report
{
    public class NavisTest
    {
        private string clashTestTypeToDisplay;
        private double tolerance;

        public List<TestResult> Results { get; set; }

        public string TestName { get; set; }

        public ClashTestType ClashTestType { get; set; }

        public string ClashTestTypeToDisplay
        {
            get
            {
                switch ((int)this.ClashTestType)
                {
                    case 0:
                        this.clashTestTypeToDisplay = "По пересечени";
                        break;
                    case 1:
                        this.clashTestTypeToDisplay = "По пересечени (консервативно)";
                        break;
                    case 2:
                        this.clashTestTypeToDisplay = "Просвет";
                        break;
                    case 3:
                        this.clashTestTypeToDisplay = "Дублирование";
                        break;
                    case 4:
                        this.clashTestTypeToDisplay = "Пользовательский тип проверки";
                        break;
                    default:
                        this.clashTestTypeToDisplay = "Тип не определен";
                        break;
                }
                return this.clashTestTypeToDisplay;
            }
        }

        public double Tolerance
        {
            get => Math.Round(this.tolerance * 304.8);
            set => this.tolerance = value;
        }

        public NavisTest(
          List<TestResult> results,
          string testName,
          ClashTestType clashTestType,
          double tolerance)
        {
            this.Results = results;
            this.TestName = testName ?? throw new ArgumentNullException(nameof(testName));
            this.ClashTestType = clashTestType;
            this.Tolerance = tolerance;
        }
    }
}
