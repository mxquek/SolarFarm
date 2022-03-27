using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using _03M_SolarFarmAssessment.Core.Interface;
using _03M_SolarFarmAssessment.Core.DTO;

namespace _03M_SolarFarmAssessment.DAL
{
    public class SolarPanelRepository : ISolarPanelRepository
    {
        private Dictionary<string,SolarPanel> _SolarPanels;
        private string _path;

        public SolarPanelRepository()
        {
            _SolarPanels = new Dictionary<string, SolarPanel>();
            _path = @"C:\Users\Mabel\training\03M-SolarFarmAssessmentData\solarFarm.csv";
            SolarPanelCSVFormatter csv = new SolarPanelCSVFormatter();

            if (!File.Exists(_path))
            {
                //referenced stack overflow
                using (FileStream fs = File.Create(_path)) { }
            }

            else
            {
                using (StreamReader sr = new StreamReader(_path))
                {
                    string currentLine = sr.ReadLine();

                    if (currentLine != null)
                    {
                        currentLine = sr.ReadLine();
                    }
                    while (currentLine != null)
                    {
                        SolarPanel record = csv.Deserialize(currentLine.Trim());
                        _SolarPanels.Add(record.ID,record);
                        currentLine = sr.ReadLine();
                    }
                }
            }
        }
        public Result<SolarPanel> Add(SolarPanel record)
        {
            Result<SolarPanel> result = new Result<SolarPanel>();
            record.ID = record.Section + "-" + record.Row + "-" + record.Column;

            foreach (KeyValuePair<string, SolarPanel> panel in _SolarPanels)
            {
                if (panel.Key == record.ID)
                {
                    result.Success = false;
                    result.Message = "Panel already exists! Panels must have a unique section, row, and column combination";
                    return result;
                }
            }

            _SolarPanels.Add(record.ID, record);
            result.Success = true;
            result.Message = "New Solar Panel Added";
            result.Data = record;

            WriteToFile();
            return result;
        }

        public Result<SolarPanel> Edit(string targetKey, SolarPanel input)
        {
            Result<SolarPanel> result = new Result<SolarPanel>();
            if(input.Section == "")
            {
                input.Section = _SolarPanels[targetKey].Section;
            }
            if (input.Row == null)
            {
                input.Row = _SolarPanels[targetKey].Row;
            }
            if (input.Column == null)
            {
                input.Column = _SolarPanels[targetKey].Column;
            }
            if (input.YearInstalled == null)
            {
                input.YearInstalled = _SolarPanels[targetKey].YearInstalled;
            }
            input.ID = input.Section + "-" + input.Row + "-" + input.Column;

            if (input.ID != targetKey)
            {
                Remove(targetKey);
                result = Add(input);
                if (!result.Success)
                {
                    return result;
                }
                else
                {
                    result.Message = $"{targetKey} has been changed to {input.ID} with updated information";
                }
            }
            else
            {
                _SolarPanels[targetKey].Material = input.Material;
                _SolarPanels[targetKey].YearInstalled = input.YearInstalled;
                _SolarPanels[targetKey].IsTracking = input.IsTracking;
                result.Message = $"{targetKey} has been updated";
            }
            result.Success = true;
            WriteToFile();
            return result;
        }

        public Result<Dictionary<string,SolarPanel>> GetAll()
        {
            Result<Dictionary<string, SolarPanel>> result = new Result<Dictionary<string, SolarPanel>>();
            result.Success = true;
            result.Message = "All Solar Panels retrieved.";
            result.Data = new Dictionary<string, SolarPanel>(_SolarPanels);
            return result;
        }

        public Result<SolarPanel> Remove(string key)
        {
            Result<SolarPanel> result = new Result<SolarPanel>();
            _SolarPanels.Remove(key);
            result.Success = true;
            result.Message = $"Solar Panel ({key}) was removed.";
            WriteToFile();
            return result;
        }

        public void WriteToFile()
        {
            SolarPanelCSVFormatter csv = new SolarPanelCSVFormatter();
            File.WriteAllText(_path, "ID,Section,Row,Column,YearInstalled,Material,IsTracking");

            bool appendMode = true;
            using (StreamWriter sw = new StreamWriter(_path, appendMode))
            {
                foreach (KeyValuePair<string,SolarPanel> panel in _SolarPanels)
                {
                    sw.Write($"\n{csv.Serialize(panel)}");
                }
            }
        }
    }
}
