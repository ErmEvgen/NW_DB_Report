using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using Autodesk.Navisworks.Api;
using Autodesk.Navisworks.Api.Clash;
using Autodesk.Navisworks.Api.Plugins;
using Autodesk.Windows;
using App = Autodesk.Navisworks.Api.Application;



namespace NW_DB_Report
{
    public class StartClashUnload
    {
        Document ActiveDoc;
        DocumentClash DocClash;
        SavedItemCollection Tests;
        Dictionary<string, SavedViewpoint> CheckPoints = new Dictionary<string, SavedViewpoint>();
        List<NavisTest> NavisTests = new List<NavisTest>();

        public StartClashUnload()
        {

            string MainVPName = "All";

            ActiveDoc = App.ActiveDocument;
            DocClash = DocumentExtensions.GetClash(ActiveDoc);
            Tests = DocClash.TestsData.Tests;

            foreach (SavedItem oSVP in ActiveDoc.SavedViewpoints.Value)

            {
                if (!oSVP.IsGroup)
                {
                    SavedViewpoint oThisSVP = oSVP as SavedViewpoint;
                    CheckPoints[oSVP.DisplayName] = oThisSVP;
                }
            }

            if (CheckPoints.ContainsKey(MainVPName))
            {
                ActiveDoc.SavedViewpoints.CurrentSavedViewpoint = CheckPoints[MainVPName];


                foreach (var test in Tests)
                {
                    DocClash.TestsData.TestsClearResults((ClashTest)test);
                }
                DocClash.TestsData.TestsRunAllTests();


                NavisTests = GetNavisTests(DocClash);


            }
            else
            {
                MessageBox.Show("Нет настроенной видовой точки All", "Создайте ViewPoint \"All\"");
            }
        }

        private List<NavisTest> GetNavisTests(DocumentClash doc)
        {
            List<NavisTest> navisTestList = new List<NavisTest>();
            foreach (var test in Tests)
            {
                var clashTest = (ClashTest)test;
                List<TestResult> results = new List<TestResult>();
                foreach (ClashResult child in ((GroupItem)clashTest).Children)
                {
                    Guid instanceGuid1 = child.CompositeItem1.InstanceGuid;
                    Guid instanceGuid2 = child.CompositeItem2.InstanceGuid;
                    Guid tempGuid = new Guid();
                    string elemId1;
                    string type1;
                    GetIdAndType(child.CompositeItem1, ref instanceGuid1, ref tempGuid, out elemId1, out type1);
                    ElementProperties element1 = new ElementProperties(child.CompositeItem1.PropertyCategories.FindPropertyByDisplayName("Элемент", "Файл источника")?.Value.ToDisplayString(), child.CompositeItem1.PropertyCategories.FindPropertyByDisplayName("Элемент", "Слой")?.Value.ToDisplayString(), type1, elemId1);
                    string elemId2;
                    string type2;
                    GetIdAndType(child.CompositeItem2, ref instanceGuid2, ref tempGuid, out elemId2, out type2);
                    ElementProperties element2 = new ElementProperties(child.CompositeItem2.PropertyCategories.FindPropertyByDisplayName("Элемент", "Файл источника")?.Value.ToDisplayString(), child.CompositeItem2.PropertyCategories.FindPropertyByDisplayName("Элемент", "Слой")?.Value.ToDisplayString(), type2, elemId2);
                    Point3D center = child.Center;
                    results.Add(new TestResult(element1, element2, center)
                    {
                        KonfliktInNavis = child.DisplayName
                    });
                }
                navisTestList.Add(new NavisTest(results, ((SavedItem)clashTest).DisplayName, clashTest.TestType, clashTest.Tolerance));
            }
            return navisTestList;
        }
        private static void GetIdAndType(ModelItem compositeItem, ref Guid instanceGuid, ref Guid tempGuid, out string elemId, out string type)
        {
            var fff  = compositeItem.PropertyCategories;
            elemId = compositeItem.PropertyCategories.FindPropertyByName("LcRevitId", "LcOaNat64AttributeValue")?.Value.ToDisplayString();
            type = compositeItem.PropertyCategories.FindPropertyByDisplayName("Элемент", "Тип")?.Value.ToDisplayString();
            if (!tempGuid.Equals(instanceGuid))
                return;
            elemId = compositeItem.Parent.PropertyCategories.FindPropertyByName("LcRevitId", "LcOaNat64AttributeValue")?.Value.ToDisplayString();
            type = compositeItem.Parent.PropertyCategories.FindPropertyByDisplayName("Элемент", "Тип")?.Value.ToDisplayString();
            instanceGuid = compositeItem.Parent.InstanceGuid;
            if (!tempGuid.Equals(instanceGuid))
                return;
            elemId = compositeItem.Parent.Parent.PropertyCategories.FindPropertyByName("LcRevitId", "LcOaNat64AttributeValue")?.Value.ToDisplayString();
            type = compositeItem.Parent.Parent.PropertyCategories.FindPropertyByDisplayName("Элемент", "Тип")?.Value.ToDisplayString();
            instanceGuid = compositeItem.Parent.Parent.InstanceGuid;
            if (!tempGuid.Equals(instanceGuid))
                return;
            elemId = compositeItem.Parent.Parent.Parent.PropertyCategories.FindPropertyByName("LcRevitId", "LcOaNat64AttributeValue")?.Value.ToDisplayString();
            type = compositeItem.Parent.Parent.Parent.PropertyCategories.FindPropertyByDisplayName("Элемент", "Тип")?.Value.ToDisplayString();
        }

    }
}