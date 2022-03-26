using _03M_SolarFarmAssessment.BLL;
using System;

namespace _03M_SolarFarmAssessment.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleIO ui = new ConsoleIO();
            MenuController menu = new MenuController(ui);
            menu.Service = SolarPanelServiceFactory.GetSolarPanelService();
            menu.Run();
        }
    }
}
