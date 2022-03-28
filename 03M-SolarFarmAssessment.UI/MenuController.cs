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
        private MaterialType? GetMaterialType(bool nullable, string? previousMaterialAsString)
        {
            bool valid = false;
            string input;
            MaterialType material = new MaterialType();
            while (!valid)
            {
                if (nullable == true)
                {
                    input = _UI.GetStringOptional($"Material ({previousMaterialAsString}) [PolySi, MonoSi, ASi, CdTe, CIGS]");
                    if (input == "")
                    {
                        valid = true;
                        return null;
                    }
                }
                else
                {
                    input = _UI.GetStringOptional($"Material [PolySi, MonoSi, ASi, CdTe, CIGS]");
                }

                if (Enum.TryParse<MaterialType>(input, true, out material))   //true = ignorescase
                {
                    if (int.TryParse(input, out int throwaway))
                    {
                        valid = false;
                        _UI.Error("Invalid Material Type!");
                    }
                    else
                    {
                        valid = true;
                    }
                }
                else
                {
                    _UI.Error("Invalid Material Type!");
                }

            }
            return material;
        }
        private bool? GetIsTracking(bool nullable, string? previousIsTrackingAsString)
        {
            bool valid = false;
            bool? result = null;
            string input;
            while (!valid)
            {
                if (nullable == true)
                {
                    input = _UI.GetStringOptional($"Tracked ({previousIsTrackingAsString}) [y/n]").ToLower();
                    if (input == "")
                    {
                        valid = true;
                        result = null;
                    }
                }
                else
                {
                    input = _UI.GetStringOptional($"Tracked [y/n]").ToLower();
                }

                if (input == "y")
                {
                    valid = true;
                    result = true;
                }
                else if (input == "n")
                {
                    valid = true;
                    result = false;
                }
                else
                {
                    _UI.Error("Invalid Input!");
                }
            }
            return result;
        }
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
            return _UI.GetIntRequired("Select [0-4]");
        }
        public void FindPanelsBySection()
        {
            _UI.Display("\nFind Panels by Section\n======================\n");
            string section = _UI.GetStringRequired("Section Name");
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

            _UI.Display("\nAdd a Panel\n===========\n");
            panel.Section = _UI.GetStringRequired("Section");
            panel.Row = _UI.GetIntRequired("Row");
            panel.Column = _UI.GetIntRequired("Column");
            panel.Material = GetMaterialTypeRequired();
            panel.YearInstalled = _UI.GetIntRequired("Installation Year");
            panel.IsTracking = GetIsTrackingRequired();

            Result<SolarPanel> result = Service.Add(panel);

            if (!result.Success)
            {
                _UI.Error(result.Message);
                _UI.Error("Panel was not added.");
            }
            else
            {
                _UI.Success(result.Message);
            }
        }
        public void UpdatePanel()
        {
            SolarPanel input = new SolarPanel();
            _UI.Display("\nUpdate a Panel\n==============\n");
            input.Section = _UI.GetStringRequired("Section");
            input.Row = _UI.GetIntRequired("Row");
            input.Column = _UI.GetIntRequired("Column");

            Result<SolarPanel> result = Service.Get($"{input.Section}-{input.Row}-{input.Column}");
            if (!result.Success)
            {
                _UI.Error(result.Message);
                return;
            }

            
            string targetKey = result.Data.ID;
            _UI.Display($"\nEditing {input.Section}-{input.Row}-{input.Column}");
            _UI.Display($"Press [Enter] to keep original value.\n");

            input.Section = _UI.GetStringOptional($"Section ({result.Data.Section})");
            input.Row = _UI.GetIntOptional($"Row ({result.Data.Row})");
            input.Column = _UI.GetIntOptional($"Column ({result.Data.Column})");
            input.Material = GetMaterialTypeOptional(result.Data.Material);
            input.YearInstalled = _UI.GetIntOptional($"Year Installed ({result.Data.YearInstalled})");
            input.IsTracking = GetIsTrackingOptional(result.Data.IsTracking, result.Data.IsTrackingAsString);

            result = Service.Edit(targetKey,input);

            if (!result.Success)
            {
                _UI.Error(result.Message);
                _UI.Error("Panel was not updated");
            }
            else
            {
                _UI.Success(result.Message);
            }
        }
        public void RemovePanel()
        {
            _UI.Display("\nRemove a Panel\n==============\n");
            string section = _UI.GetStringRequired("Section");
            int row = _UI.GetIntRequired("Row");
            int column = _UI.GetIntRequired("Column");

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
        
        public MaterialType GetMaterialTypeRequired()
        {
            return (MaterialType)GetMaterialType(false,null);
        }
        public MaterialType GetMaterialTypeOptional(MaterialType previousMaterial)
        {
            MaterialType? result = GetMaterialType(true,previousMaterial.ToString());
            if (result == null)
            {
                result = previousMaterial;
            }
            return (MaterialType)result;
        }
        
        public bool GetIsTrackingRequired()
        {
            return (bool)GetIsTracking(false,null);
        }
        public bool GetIsTrackingOptional(bool previousIsTracking, string previousIsTrackingAsString)
        {
            bool? result = GetIsTracking(true, previousIsTrackingAsString);
            if(result == null)
            {
                result = previousIsTracking;
            }
            return (bool)result;
        }
    }
}
