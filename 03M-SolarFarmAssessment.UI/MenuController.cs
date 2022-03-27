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
            _UI.Display("");
            DisplayMenu();
            return _UI.GetIntRecquired("Select [0-4]");
        }
        public void FindPanelsBySection()
        {
            _UI.Display("\nFind Panels by Section\n======================\n");
            string section = _UI.GetStringRecquired("Section Name");
            Result<List<SolarPanel>> result = Service.LoadSection(section);
            if (result.Success)
            {
                _UI.Display($"\nPanels in {section}");
                _UI.Display($"Row Col Year Material Tracking");
                for (int index = 0; index < result.Data.Count; index++)
                {
                    _UI.Display($"{result.Data[index].Row,3} {result.Data[index].Column,3} {result.Data[index].YearInstalled,4} {result.Data[index].Material,8} {result.Data[index].IsTrackingAsString,8}");
                }
            }
            else
            {
                _UI.Error(result.Message);
            }
        }
        public void AddPanel()
        {
            SolarPanel panel = new SolarPanel();
            string tempString;
            bool valid = false;

            _UI.Display("\nAdd a Panel\n===========\n");
            panel.Section = _UI.GetStringRecquired("Section");
            panel.Row = _UI.GetIntRecquired("Row");
            panel.Column = _UI.GetIntRecquired("Column");

            while (!valid) //maybe turn into mthod
            {
                tempString = _UI.GetStringRecquired("Material (PolySi, MonoSi, ASi, CdTe, CIGS)");
                
                if (Enum.TryParse<MaterialType>(tempString, true, out MaterialType tempMaterial))   //true = ignorescase
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

            panel.YearInstalled = _UI.GetIntRecquired("Installation Year");

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
            else
            {
                _UI.Success(result.Message);
            }
        }
        public void UpdatePanel()
        {
            _UI.Display("\nUpdate a Panel\n==============\n");
            string section = _UI.GetStringRecquired("Section");
            int row = _UI.GetIntRecquired("Row");
            int column = _UI.GetIntRecquired("Column");

            Result<SolarPanel> result = Service.Get($"{section}-{row}-{column}");
            if (!result.Success)
            {
                _UI.Error(result.Message);
                return;
            }

            SolarPanel input = new SolarPanel();
            string targetKey = result.Data.ID;
            _UI.Display($"\nEditing {section}-{row}-{column}");
            _UI.Display($"Press [Enter] to keep original value.\n");

            input.Section = _UI.GetStringOptional($"Section ({result.Data.Section})");
            input.Row = _UI.GetIntOptional($"Row ({result.Data.Row})");
            input.Column = _UI.GetIntOptional($"Column ({result.Data.Column})");

            string tempString;      //maybe turn into mthod
            bool valid = false;     //
            while (!valid) 
            {
                tempString = _UI.GetStringOptional($"Material (PolySi, MonoSi, ASi, CdTe, CIGS) ({result.Data.Material})");

                if (Enum.TryParse<MaterialType>(tempString, true, out MaterialType tempMaterial))   //true = ignorescase
                {
                    valid = true;
                    if (int.TryParse(tempString, out int throwaway))
                    {
                        valid = false;
                    }
                    else
                    {
                        input.Material = tempMaterial;              //new line
                        continue;
                    }
                }
                if(tempString == "")    //this is different than the add validation
                {
                    valid = true;
                    input.Material = result.Data.Material;          //new line
                    continue;
                }

                _UI.Error("Invalid Material Type!");

            }       //
            input.YearInstalled = _UI.GetIntOptional($"Year Installed ({result.Data.YearInstalled})");

            valid = false;
            while (!valid)
            {
                switch (_UI.GetStringOptional($"Tracked [y/n] ({result.Data.IsTrackingAsString})").ToLower())
                {
                    case "y":
                        input.IsTracking = true;
                        valid = true;
                        break;
                    case "n":
                        input.IsTracking = false;
                        valid = true;
                        break;
                    case "":
                        input.IsTracking = result.Data.IsTracking;
                        valid = true;
                        break;
                    default:
                        _UI.Error("Invalid Input!");
                        break;
                }
            }

            result = Service.Edit(targetKey,input);
            if (!result.Success)
            {
                _UI.Error(result.Message);
            }
            else
            {
                _UI.Success(result.Message);
            }
        }
        public void RemovePanel()
        {
            _UI.Display("\nRemove a Panel\n==============\n");
            string section = _UI.GetStringRecquired("Section");
            int row = _UI.GetIntRecquired("Row");
            int column = _UI.GetIntRecquired("Column");

            Result<SolarPanel> result = Service.Get($"{section}-{row}-{column}");

            if (!result.Success)
            {
                _UI.Error(result.Message);
                return;
            }
            else
            {
                result = Service.Remove(result.Data.ID);
                _UI.Success(result.Message);
            }
        }

    }
}
