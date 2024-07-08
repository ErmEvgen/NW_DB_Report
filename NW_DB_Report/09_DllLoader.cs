using System;
using System.IO;
using System.Reflection;

namespace NW_DB_Report
{
    internal class DllLoader
    {
        string FolderPath;
        public DllLoader(string folderPath= "")
        {
            FolderPath = folderPath;
            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.AssemblyResolve += new ResolveEventHandler(MyResolveEventHandler);
        }

        private Assembly MyResolveEventHandler(object sender, ResolveEventArgs args)
        {
            Assembly MyAssembly, objExecutingAssembly;
            string strTempAssmbPath = "";

            objExecutingAssembly = Assembly.GetExecutingAssembly();
            AssemblyName[] arrReferencedAssmbNames = objExecutingAssembly.GetReferencedAssemblies();

            if (!Directory.Exists(FolderPath))
                FolderPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            foreach (AssemblyName strAssmbName in arrReferencedAssmbNames)
            {
                if (strAssmbName.FullName.Substring(0, strAssmbName.FullName.IndexOf(",")) == args.Name.Substring(0, args.Name.IndexOf(",")))
                {
                    var strTempAssmbFile = args.Name.Substring(0, args.Name.IndexOf(",")) + ".dll";
                    strTempAssmbPath = Path.Combine(FolderPath, strTempAssmbFile);
                    break;
                }
            }
            MyAssembly = Assembly.LoadFrom(strTempAssmbPath);
            return MyAssembly;
        }
    }
}
