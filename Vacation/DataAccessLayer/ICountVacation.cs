using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer
{
    public interface ICountVacation
    {
       int CountWeekend(DateTime StartDay, DateTime EndDay, string TeamName);
        bool CheckWeekend(DateTime StartDay, DateTime EndDay, DateTime StartDaySecond, DateTime EndDaySecond, string TeamNameOne, string TeamNameTwo);
        bool AuditDate(DateTime date);
        int ResultCountAddDay(DateTime CountDate, Person person);
        bool CheckonBusy(Person person, DateTime FirstDate, DateTime SecondDate);
        int CountDaysVacation(DateTime startDate, DateTime FinishDate);
        int GetNumbervacation();
    }
}
