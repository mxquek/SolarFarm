using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _03M_SolarFarmAssessment.Core.Interface;
using _03M_SolarFarmAssessment.Core.DTO;

namespace _03M_SolarFarmAssessment.DAL
{
    public class SolarPanelRepository : ISolarPanelRepository
    {
        public Result<SolarPanel> Add(SolarPanel record)
        {
            throw new NotImplementedException();
        }

        public Result<SolarPanel> Edit(SolarPanel record)
        {
            throw new NotImplementedException();
        }

        public Result<List<SolarPanel>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Result<SolarPanel> Remove(string key)
        {
            throw new NotImplementedException();
        }
    }
}
