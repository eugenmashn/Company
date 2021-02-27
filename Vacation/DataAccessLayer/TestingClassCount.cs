using DataAccessLayer.Models;
using DataAccessLayer.Repository;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace DataAccessLayer
{
    public class TestingClassCount
    {

        public bool AuditDate(DateTime date)
        {
            List<Weekend> listweekend = new List<Weekend>();
            Weekend weekend = new Weekend();
            weekend.Id = Guid.NewGuid();
            string startDate = "10/28/2019";
            weekend.startDate = DateTime.ParseExact(startDate, "M/d/yyyy", CultureInfo.InvariantCulture);
            weekend.EndDate = weekend.startDate.AddDays(4);
            weekend.Name = "Test";
            listweekend.Add(weekend);
            foreach (var i in listweekend)
            {
                if ((date.Date >= i.startDate.Date) && date.Date <= i.EndDate.Date)
                {
                    return true;
                }
            }
            return false;
        }
        //public int ResultCountAddDay(DateTime CountDate, Person person)
        //{
        //    List<Person> IndexDayTwo = Personrepository.Get().ToList();
        //    int IndexDay = person.Days;
        //    for (int i = 0; i <= IndexDay; i++)
        //    {
        //        if (CountDate.DayOfWeek == DayOfWeek.Sunday || CountDate.DayOfWeek == DayOfWeek.Saturday || AuditDate(CountDate))
        //        {
        //            IndexDay++;
        //        }
        //        CountDate = CountDate.AddDays(1);
        //    }
        //    return IndexDay;
        //}
        //public bool CheckonBusy(Person person, DateTime FirstDate, DateTime SecondDate)
        //{
        //    if (CountTeam(person.Team.TeamName) - CountWeekend(FirstDate, SecondDate, person.Team.TeamName) <= person.Team.MinNumberWorkers)
        //    {
        //        return false;
        //    }
        //    return true;
        //}
        //private int CountTeam(string TeamName)
        //{

        //    List<Person> list = Personrepository.IncludeGet(p => p.Team).Where(i => i.Team.TeamName == TeamName).ToList();
        //    return list.Count;
        //}
        public int CountDaysVacation(DateTime startDate, DateTime FinishDate)
            {
                int CountDaysHolyDays = 0;
                int IndexDay = 0;
                for (DateTime i = startDate; i <= FinishDate;)
                {
                    if (i.DayOfWeek == DayOfWeek.Sunday || i.DayOfWeek == DayOfWeek.Saturday || AuditDate(i))
                    {
                        IndexDay++;
                        CountDaysHolyDays--;
                    }
                    CountDaysHolyDays++;
                    i = i.AddDays(1);
                }
                return CountDaysHolyDays;
            }
    
    }
    
}
