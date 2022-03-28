using _03M_SolarFarmAssessment.Core.DTO;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using _03M_SolarFarmAssessment.Core.Interface;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _03M_SolarFarmAssessment.DAL;

namespace _03M_SolarFarmAssessment.Test
{
    public class SolarPanelRepositoryTests
    {
        ISolarPanelRepository testRepository;
        Dictionary<string, SolarPanel> solarPanels;
        SolarPanel testPanel, existingPanel;

        [SetUp]
        public void Setup()
        {
            testRepository = new SolarPanelRepository();
            solarPanels = new Dictionary<string, SolarPanel>();
            testPanel = new SolarPanel();
            int row = 55;
            int column = 66;
            int material = 0;
            int yearInstalled = 2000;

            existingPanel = new SolarPanel();
            existingPanel.Section = "SPTest";
            existingPanel.Row = 55;
            existingPanel.Column = 66;
            existingPanel.ID = $"{existingPanel.Section}-{existingPanel.Row}-{existingPanel.Column}";
            existingPanel.Material = 0;
            existingPanel.YearInstalled = 2000;
            existingPanel.IsTracking = true;


            for (int index = 0; index < 5; index++)
            {
                testPanel = new SolarPanel();
                testPanel.Section = "SPTest";
                testPanel.Row = row;
                testPanel.Column = column;
                testPanel.ID = $"{testPanel.Section}-{testPanel.Row}-{testPanel.Column}";
                testPanel.Material = (MaterialType)material;
                testPanel.YearInstalled = yearInstalled;
                testPanel.IsTracking = true;

                testRepository.Add(testPanel);
                solarPanels.Add(testPanel.ID, testPanel);

                row += 1;
                column += 1;
                material += 1;
                yearInstalled += 1;
            }
        }

        [Test]
        public void TestAddPanel()
        {
            bool actual = false;
            bool expected = true;
            testRepository.Add(testPanel);
            Dictionary<string, SolarPanel> actualSolarPanels = testRepository.GetAll().Data;
            foreach (KeyValuePair<string, SolarPanel> panel in actualSolarPanels)
            {
                if (panel.Key == testPanel.ID)
                {
                    actual = true;
                }
            }
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestAddDuplicatePanel()
        {
            Result<SolarPanel> result = testRepository.Add(existingPanel);
            bool actual = result.Success;
            bool expected = false;
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestEditSameID()
        {
            bool actual = false, expected = true;
            SolarPanel newPanel = new SolarPanel();
            newPanel = new SolarPanel();
            newPanel.Section = "SPTest";
            newPanel.Row = testPanel.Row;
            newPanel.Column = testPanel.Column;
            newPanel.ID = $"{testPanel.Section}-{testPanel.Row}-{testPanel.Column}";
            newPanel.Material = (MaterialType)4;
            newPanel.YearInstalled = 0001;
            newPanel.IsTracking = false;

            testRepository.Add(testPanel);
            Result<SolarPanel> result = testRepository.Edit(testPanel.ID, newPanel);
            if (result.Data.ID == newPanel.ID &&
               result.Data.Section == newPanel.Section &&
               result.Data.Row == newPanel.Row &&
               result.Data.Column == newPanel.Column &&
               result.Data.Material == newPanel.Material &&
               result.Data.YearInstalled == newPanel.YearInstalled &&
               result.Data.IsTracking == newPanel.IsTracking)
            {
                actual = true;
            }

            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestEditChangeID()      //Does not function properly if run more than once
        {
            bool actual = false, expected = true;
            SolarPanel newPanel = new SolarPanel();
            newPanel = new SolarPanel();
            newPanel.Section = "SPTest";
            newPanel.Row = 111;
            newPanel.Column = 111;
            newPanel.ID = $"{testPanel.Section}-{testPanel.Row}-{testPanel.Column}";
            newPanel.Material = (MaterialType)3;
            newPanel.YearInstalled = 1111;
            newPanel.IsTracking = false;

            testRepository.Add(testPanel);
            Result<SolarPanel> result = testRepository.Edit(testPanel.ID, newPanel);
            if (result.Data.ID == newPanel.ID &&
               result.Data.Section == newPanel.Section &&
               result.Data.Row == newPanel.Row &&
               result.Data.Column == newPanel.Column &&
               result.Data.Material == newPanel.Material &&
               result.Data.YearInstalled == newPanel.YearInstalled &&
               result.Data.IsTracking == newPanel.IsTracking)
            {
                actual = true;
            }

            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestRemove()
        {
            bool actualRemoveSuccess = true;
            bool expectedRemoveSuccess = true;

            testRepository.Add(testPanel);
            testRepository.Remove(testPanel.ID);

            Dictionary<string, SolarPanel> actualSolarPanels = testRepository.GetAll().Data;
            foreach (KeyValuePair<string, SolarPanel> panel in actualSolarPanels)
            {
                if (panel.Key == testPanel.ID)
                {
                    actualRemoveSuccess = false;
                }
            }
            Assert.AreEqual(expectedRemoveSuccess, actualRemoveSuccess);
        }
    }
}
