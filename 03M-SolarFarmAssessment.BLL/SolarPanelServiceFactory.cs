using _03M_SolarFarmAssessment.Core.Interface;
using _03M_SolarFarmAssessment.DAL;

namespace _03M_SolarFarmAssessment.BLL
{
    public class SolarPanelServiceFactory
    {
        public static ISolarPanelService GetSolarPanelService()
        {
            return new SolarPanelService(new SolarPanelRepository());
        }
    }
}
