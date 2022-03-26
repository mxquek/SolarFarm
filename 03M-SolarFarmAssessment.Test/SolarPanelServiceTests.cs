using _03M_SolarFarmAssessment.Core.DTO;
using _03M_SolarFarmAssessment.BLL;
using NUnit.Framework;
using _03M_SolarFarmAssessment.Core.Interface;

namespace _03M_SolarFarmAssessment.Test
{
    public class SolarPanelServiceTests
    {
        SolarPanel invalidTestPanel,validTestPanel;
        ISolarPanelService testService;
        [SetUp]
        public void Setup()
        {
            invalidTestPanel = new SolarPanel();
            validTestPanel = new SolarPanel();
            testService = SolarPanelServiceFactory.GetSolarPanelService();
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
    }
}