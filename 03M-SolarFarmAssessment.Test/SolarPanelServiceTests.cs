using _03M_SolarFarmAssessment.Core.DTO;
using _03M_SolarFarmAssessment.BLL;
using NUnit.Framework;
using _03M_SolarFarmAssessment.Core.Interface;
using System;

namespace _03M_SolarFarmAssessment.Test
{
    public class SolarPanelServiceTests
    {
        SolarPanel invalidTestPanel,testPanel;
        ISolarPanelService testService;
        [SetUp]
        public void Setup()
        {
            invalidTestPanel = new SolarPanel();
            testPanel = new SolarPanel();
            testService = SolarPanelServiceFactory.GetSolarPanelService();

            testPanel.Section = "Main";
            testPanel.Row = 55;
            testPanel.Column = 66;
            testPanel.Material = Enum.Parse<MaterialType>("CdTe");
            testPanel.YearInstalled = 2022;
            testPanel.IsTracking = true;
            testService.Add(testPanel);
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
            string key = "Main-22-11";
            Result<SolarPanel> result = testService.Get(key);

            bool actual = result.Success;
            bool expected = false;
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestGetValidKey()
        {
            testService.Add(testPanel);
            string key = "Main-55-66";
            Result<SolarPanel> result = testService.Get(key);

            bool actual = result.Success;
            bool expected = true;

            Assert.AreEqual(expected, actual);
        }
    }
}