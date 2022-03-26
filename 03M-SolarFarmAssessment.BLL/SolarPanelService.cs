using System;
using System.Collections.Generic;
using _03M_SolarFarmAssessment.Core.DTO;
using _03M_SolarFarmAssessment.Core.Interface;


namespace _03M_SolarFarmAssessment.BLL
{
    public class SolarPanelService : ISolarPanelService
    {
        private ISolarPanelRepository _SolarPanelRepository;

        public SolarPanelService(ISolarPanelRepository repo)
        {
            _SolarPanelRepository = repo;
        }
        public Result<SolarPanel> Add(SolarPanel panel)
        {
            Result<SolarPanel> result = new Result<SolarPanel>();
            if (panel.Row <= 0 || panel.Row > 250)
            {
                result.Success = false;
                result.Message = "Row is out of bounds.";
            }
            if (panel.Column <= 0 || panel.Column > 250)
            {
                result.Success = false;
                result.Message = "Column is out of bounds.";
            }
            if (panel.YearInstalled > DateTime.Now.Year)
            {
                result.Success = false;
                result.Message = "The year cannot be in the future.";
            }

            if (!result.Success)
            {
                return result;
            }
            return _SolarPanelRepository.Add(panel);
        }

        public Result<SolarPanel> Edit(SolarPanel panel)
        {
            throw new NotImplementedException();
        }

        public Result<SolarPanel> Get(string key)
        {
            throw new NotImplementedException();
        }

        public Result<List<SolarPanel>> LoadSection(string section)
        {
            throw new NotImplementedException();
        }

        public Result<SolarPanel> Remove(string key)
        {
            throw new NotImplementedException();
        }
    }
}
