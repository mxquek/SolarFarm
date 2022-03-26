using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03M_SolarFarmAssessment.Core.DTO
{
    public class SolarPanel
    {
        public string ID { get; set; }
        public string Section { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public int YearInstalled { get; set; }
        public MaterialType Material { get; set; }
        public bool IsTracking { get; set; }
    }
}
