namespace NW_DB_Report
{
    public class ElementProperties
    {
        public string FileName { get; set; }

        public string Level { get; set; }

        public string Type { get; set; }

        public string ElementId { get; set; }

        public ElementProperties(string fileName, string level, string type, string elementId)
        {
            this.FileName = fileName ?? "<File_Null>";
            this.Level = level ?? "<_Level_Null>";
            this.Type = type ?? "<Type_Null>";
            this.ElementId = elementId ?? "-1";
        }
    }
}
