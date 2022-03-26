using _03M_SolarFarmAssessment.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03M_SolarFarmAssessment.Core.Interface
{
    public interface ISolarPanelFormatter
    {
        SolarPanel Deserialize(string data);
        string Serialize(SolarPanel record);
    }
}
