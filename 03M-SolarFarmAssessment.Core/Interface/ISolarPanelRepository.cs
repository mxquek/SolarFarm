using _03M_SolarFarmAssessment.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03M_SolarFarmAssessment.Core.Interface
{
    public interface ISolarPanelRepository
    {
        public Result<Dictionary<string, SolarPanel>> GetAll();     // Retrieves all panels
        Result<SolarPanel> Add(SolarPanel record);                  // Adds a panel
        Result<SolarPanel> Remove(string key);                      // Removes panel
        Result<SolarPanel> Edit(string targetKey, SolarPanel record);                 // Replaces a panel
    }
}
