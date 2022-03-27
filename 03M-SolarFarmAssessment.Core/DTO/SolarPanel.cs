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
        public int? Row { get; set; }
        public int? Column { get; set; }
        public int? YearInstalled { get; set; }
        public MaterialType Material { get; set; }
        public bool IsTracking { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"ID: {ID}");
            sb.AppendLine($"Section: {Section}");
            sb.AppendLine($"Row: {Row}");
            sb.AppendLine($"Column: {Column}");
            sb.AppendLine($"Year Installed: {YearInstalled}");
            sb.AppendLine($"Material Type: {Material.ToString()}");
            if (IsTracking)
            {
                sb.AppendLine($"Tracking: yes");
            }
            else
            {
                sb.AppendLine($"Tracking: no");
            }
            
            sb.AppendLine("---------------------");

            return sb.ToString();
        }

        public string IsTrackingAsString    //referenced stack overflow
        {
            get
            {
                if(IsTracking == true)
                {
                    return "Yes";
                }
                else
                {
                    return "No";
                }
                
            }
        }
    }
}
