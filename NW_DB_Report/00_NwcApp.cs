using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Autodesk.Navisworks.Api;
using Autodesk.Navisworks.Api.Clash;
using Autodesk.Navisworks.Api.Plugins;


namespace NW_DB_Report 
{

    // plugin constructor - custom tab attributes 
    [Plugin("CustomTabSample222", "TwentyTwo222", DisplayName = "CustomTabSample2222")]
    // xaml file - layout of custom ribbon (panel & buttons) 
    [RibbonLayout("CustomTabSample2.xaml")]
    // ribbon tab ID from xaml file
    [RibbonTab("TabTest")]
    // ribbon button ID & icon files
    [Command("Button_One", ToolTip = "Выгрузка отчетов в Excel", ExtendedToolTip = "Пока не фига не работает", Icon = "1_16.png", LargeIcon = "cat.png")]

    [Command("Button_Two", ToolTip = "Создание поисковых наборов и проверок", ExtendedToolTip = "Данные берутся из GoogleSheets Горпроекта", Icon = "2_16.png", LargeIcon = "dog.png")]


    public class MainClass : CommandHandlerPlugin
    {
        public override int ExecuteCommand(string name, params string[] parameters)
        {

            new DllLoader();

            switch (name)
            {
                case "Button_One":
                    {
                        new StartClashUnload();
                        break;
                    }

                case "Button_Two":
                    {
                    MessageBox.Show("Если Чак Норрис шутит про жену Уилла Смита, Уилл встаёт и даёт ей пощечину.",
                        "Sample - Button Two");
                        break;
                    }
                case "Button_Three":
                    MessageBox.Show("In three words I can sum up everything I've learned" +
                        " about life: it goes on.\n-Robert Frost",
                        "Sample - Button Three");
                    break;
                case "Button_Four":
                    MessageBox.Show("Four things for success: work and pray, " +
                        "think and believe.\n-Norman Vincent Peale", "Sample - Button Four");
                    break;
            }
            return 0;
        }
    }


    [Plugin("NWC_Unload", "ERM", DisplayName = "GSUnload")] 
    public class NwcApp : AddInPlugin
    {
        public override int Execute(params string[] parameters)
        {
            new StartClashUnload();
            return 0;
        }
    }

}
