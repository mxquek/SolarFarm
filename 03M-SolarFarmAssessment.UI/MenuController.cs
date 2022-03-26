using _03M_SolarFarmAssessment.BLL;
using _03M_SolarFarmAssessment.Core.DTO;
using _03M_SolarFarmAssessment.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03M_SolarFarmAssessment.UI
{
    class MenuController
    {
        private ConsoleIO _UI;
        public ISolarPanelService Service;

        public MenuController(ConsoleIO ui)
        {
            _UI = ui;
        }

        public void Run()
        {

            _UI.Display("Welcome to Solar Farm");

            bool running = true;
            while (running)
            {
                switch (GetMenuChoice())
                {
                    case 0:
                        running = false;
                        _UI.Display("\n\nExiting Program...");
                        break;
                    case 1:
                        FindPanelsBySection();
                        break;
                    case 2:
                        AddPanel();
                        break;
                    case 3:
                        UpdatePanel();
                        break;
                    case 4:
                        RemovePanel();
                        break;
                    default:
                        _UI.Error("Invalid Menu Option\n");
                        break;
                }
            }
        }

        public void DisplayMenu()
        {
            _UI.Display("Main Menu");
            _UI.Display("=========");
            _UI.Display("0. Quit");
            _UI.Display("1. Find Panels by Section");
            _UI.Display("2. Add a Panel");
            _UI.Display("3. Update a Panel");
            _UI.Display("4. Remove a Panel");
        }

        public int GetMenuChoice()
        {
            DisplayMenu();
            return _UI.GetInt("Select [0-4]");
        }

        public void FindPanelsBySection()
        {
            _UI.Display("\nFind Panels by Section\n======================\n");
        }
        public void AddPanel()
        {
            SolarPanel panel = new SolarPanel();
            string tempString;
            bool valid = false;

            _UI.Display("\nAdd a Panel\n===========\n");
            panel.Section = _UI.GetStringRecquired("Section");
            panel.Row = _UI.GetInt("Row");
            panel.Column = _UI.GetInt("Column");

            while (!valid)
            {
                tempString = _UI.GetStringRecquired("Material (PolySi, MonoSi, ASi, CdTe, CIGS)");
                
                if (Enum.TryParse<MaterialType>(tempString, true, out MaterialType tempMaterial))
                {
                    valid = true;
                    if (int.TryParse(tempString, out int throwaway))
                    {
                        valid = false;
                    }
                    else
                    {
                        continue;
                    }
                }

                _UI.Error("Invalid Material Type!");

            }

            panel.YearInstalled = _UI.GetInt("Installation Year");

            valid = false;
            while (!valid)
            {
                switch (_UI.GetStringRecquired("Tracked [y/n]").ToLower())
                {
                    case "y":
                        panel.IsTracking = true;
                        valid = true;
                        break;
                    case "n":
                        panel.IsTracking = false;
                        valid = true;
                        break;
                    default:
                        _UI.Error("Invalid Input!");
                        break;
                }
            }


            Result<SolarPanel> result = Service.Add(panel);

            if (!result.Success)
            {
                _UI.Error(result.Message);
            }
        }
        public void UpdatePanel()
        {
            _UI.Display("\nUpdate a Panel\n==============\n");
        }
        public void RemovePanel()
        {
            _UI.Display("\nRemove a Panel\n==============\n");

        }

    }
}
