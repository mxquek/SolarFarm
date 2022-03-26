using _03M_SolarFarmAssessment.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03M_SolarFarmAssessment.Core.Interface
{
    public interface ISolarSolarPanelService
    {
        Result<List<SolarPanel>> LoadSection(string section);     // Retrieves only SolarPanels in section
        Result<SolarPanel> Get(string key);                       // Get only the SolarPanel with the specified key
        Result<SolarPanel> Add(SolarPanel SolarPanel);            // Adds a SolarPanel
        Result<SolarPanel> Remove(string key);                    // Removes SolarPanel by key
        Result<SolarPanel> Edit(SolarPanel SolarPanel);           // edits a SolarPanel with the same key
    }
}
