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
            //Console.WriteLine(_path);     //testing
            if (!File.Exists(_path))
            {
                using(FileStream fs = File.Create(_path)) { }
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
            record.ID = record.Section + "-" + record.Row + "-" + record.Column;
            _SolarPanels.Add(record.ID, record);

            Result<SolarPanel> result = new Result<SolarPanel>();
            result.Success = true;
            result.Message = "New Solar Panel Added";
            result.Data = record;

            //Console.WriteLine(_SolarPanels[record.ID]);       TEST
            WriteToFile();
            return result;
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
