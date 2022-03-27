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
                return result;
            }
            if (panel.Column <= 0 || panel.Column > 250)
            {
                result.Success = false;
                result.Message = "Column is out of bounds.";
                return result;
            }
            if (panel.YearInstalled > DateTime.Now.Year)
            {
                result.Success = false;
                result.Message = "The year cannot be in the future.";
                return result;
            }

            return _SolarPanelRepository.Add(panel);
        }

        public Result<SolarPanel> Edit(string targetKey, SolarPanel panel)
        {
            return _SolarPanelRepository.Edit(targetKey,panel);
        }

        public Result<SolarPanel> Get(string key)
        {
            Result<SolarPanel> result = new Result<SolarPanel>();
            Dictionary<string, SolarPanel> solarPanels = _SolarPanelRepository.GetAll().Data;

            foreach (KeyValuePair<string, SolarPanel> panel in solarPanels)
            {
                if(panel.Key == key)
                {
                    result.Success = true;
                    result.Message = $"Solar Panel {key} was found.";
                    result.Data = panel.Value;
                    return result;
                }
            }
            result.Success = false;
            result.Message = $"No solar panel with the requested key ({key}) was found.";
            return result;
        }

        public Result<List<SolarPanel>> LoadSection(string section)
        {
            Result<List<SolarPanel>> result = new Result<List<SolarPanel>>();
            result.Data = new List<SolarPanel>();
            Dictionary<string, SolarPanel> solarPanels = _SolarPanelRepository.GetAll().Data;
            result.Success = false;
            result.Message = $"No solar panels in {section} were found.";

            foreach (KeyValuePair<string, SolarPanel> panel in solarPanels)
            {
                if (panel.Value.Section == section)
                {
                    result.Success = true;
                    result.Message = $"Solar Panel in {section} was found.";
                    result.Data.Add(panel.Value);
                }
            }

            return result;
        }

        public Result<SolarPanel> Remove(string key)
        {
            return _SolarPanelRepository.Remove(key);
        }
    }
}
