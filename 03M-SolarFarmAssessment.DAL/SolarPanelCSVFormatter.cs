using System;
using _03M_SolarFarmAssessment.Core.Interface;
using _03M_SolarFarmAssessment.Core.DTO;
using System.Collections.Generic;

namespace _03M_SolarFarmAssessment.DAL
{
    public class SolarPanelCSVFormatter : ISolarPanelFormatter
    {
        public SolarPanel Deserialize(string data)
        {
            SolarPanel result = new SolarPanel();

            string[] fields = data.Split(",");
            result.ID = fields[0];
            result.Section = fields[1];
            result.Row = int.Parse(fields[2]);
            result.Column = int.Parse(fields[3]);
            result.YearInstalled = int.Parse(fields[4]);
            result.Material = Enum.Parse<MaterialType>(fields[5]);
            result.IsTracking = bool.Parse(fields[6]);

            return result;
        }

        public string Serialize(KeyValuePair<string, SolarPanel> panel)
        {
            return $"{panel.Value.ID},{panel.Value.Section},{panel.Value.Row},{panel.Value.Column},{panel.Value.YearInstalled},{panel.Value.Material},{panel.Value.IsTracking}";
        }
    }
}
