using System;
using Xunit;

using Moq;
using System.Globalization;
using DataAccessLayer;

namespace XUnitTestWorkers
{
    public class UnitTestCountVacation
    {
        const string format = "M/d/yyyy";
        
        [Fact]
        public void TestCountVacationHolyDays()
        {
            DateTime StartDate = DateTime.ParseExact("10/28/2019", format, CultureInfo.InvariantCulture);
            DateTime EndDate = DateTime.ParseExact("10/31/2019",format, CultureInfo.InvariantCulture);
            TestingClassCount testingClassCount = new TestingClassCount();
            var result = testingClassCount.CountDaysVacation(StartDate,EndDate);
            Assert.Equal(0, result);
        }
        [Fact]
        public void TestCountVacation()
        {
            DateTime StartDate = DateTime.ParseExact("10/10/2019", format, CultureInfo.InvariantCulture);
            DateTime EndDate = DateTime.ParseExact("10/14/2019", format, CultureInfo.InvariantCulture);
          
            TestingClassCount testingClassCount = new TestingClassCount();
            var result = testingClassCount.CountDaysVacation(StartDate, EndDate);
            Assert.Equal(3, result);
        }
        [Fact]
        public void TestCountVacationSunAndSat()
        {
            DateTime StartDate = DateTime.ParseExact("11/26/2019", format, CultureInfo.InvariantCulture);
            DateTime EndDate = DateTime.ParseExact("11/30/2019", format, CultureInfo.InvariantCulture);

            TestingClassCount testingClassCount = new TestingClassCount();
            var result = testingClassCount.CountDaysVacation(StartDate, EndDate);
            Assert.Equal(4, result);
        }
     
    } 
}
