using _03M_SolarFarmAssessment.Core.DTO;
using _03M_SolarFarmAssessment.BLL;
using NUnit.Framework;
using _03M_SolarFarmAssessment.Core.Interface;
using System;
using System.Collections.Generic;

namespace _03M_SolarFarmAssessment.Test
{
    public class SolarPanelServiceTests
    {
        SolarPanel invalidTestPanel,testPanel;
        ISolarPanelService testService;
        List<SolarPanel> solarPanels;
        [SetUp]
        public void Setup()
        {
            invalidTestPanel = new SolarPanel();
            testPanel = new SolarPanel();
            solarPanels = new List<SolarPanel>();
            testService = SolarPanelServiceFactory.GetSolarPanelService();
            int row = 55, column = 66, material = 0, yearInstalled = 2000;

            for (int index = 0; index < 5; index++)
            {
                testPanel = new SolarPanel();
                testPanel.Section = "SPTest";
                testPanel.Row = row;
                testPanel.Column = column;
                testPanel.Material = (MaterialType)material;
                testPanel.YearInstalled = yearInstalled;
                testPanel.IsTracking = true;
                solarPanels.Add(testPanel);
                testService.Add(testPanel);

                row += 1;
                column += 1;
                material += 1;
                yearInstalled += 1;
            } 
        }

        [Test]
        public void TestAddPanelInvalidRow1()
        {
            invalidTestPanel.Row = 0;
            Result<SolarPanel> result = testService.Add(invalidTestPanel);
            bool actual = result.Success;
            bool expected = false;
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestAddPanelInvalidRow2()
        {
            invalidTestPanel.Row = 251;
            Result<SolarPanel> result = testService.Add(invalidTestPanel);
            bool actual = result.Success;
            bool expected = false;
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestAddPanelInvalidColumn1()
        {
            invalidTestPanel.Row = 55;
            invalidTestPanel.Column = 0;
            Result<SolarPanel> result = testService.Add(invalidTestPanel);
            bool actual = result.Success;
            bool expected = false;
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestAddPanelInvalidColumn2()
        {
            invalidTestPanel.Row = 55;
            invalidTestPanel.Column = 251;
            Result<SolarPanel> result = testService.Add(invalidTestPanel);
            bool actual = result.Success;
            bool expected = false;
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestAddPanelInvalidYear()
        {
            invalidTestPanel.Row = 55;
            invalidTestPanel.Column = 55;
            invalidTestPanel.YearInstalled = 2023;
            Result<SolarPanel> result = testService.Add(invalidTestPanel);
            bool actual = result.Success;
            bool expected = false;
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestGetInvalidKey()
        {
            string key = "SPTest-100-100";
            Result<SolarPanel> result = testService.Get(key);

            bool actual = result.Success;
            bool expected = false;
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestGetValidKey()
        {
            testService.Add(testPanel);
            string key = "SPTest-55-66";
            Result<SolarPanel> result = testService.Get(key);

            bool actual = result.Success;
            bool expected = true;

            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestLoadInvalidSection()
        {
            Result<List<SolarPanel>> result = testService.LoadSection("Random1234");
            List<SolarPanel> actual = result.Data;
            List<SolarPanel> expected = new List<SolarPanel>();

            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestLoadValidSection()
        {
            Result<List<SolarPanel>> result = testService.LoadSection(testPanel.Section);

            List<SolarPanel> actualList = result.Data;
            List<SolarPanel> expectedList = solarPanels;
            bool actual = false;

            for (int index = 0; index < actualList.Count; index++)
            {
                if(actualList[index].ID == expectedList[index].ID)
                {
                    actual = true;
                }
                else
                {
                    actual = false;
                }
            }

            bool expected = true;

            Assert.AreEqual(expected, actual);
        }
    }
}