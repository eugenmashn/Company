using DataAccessLayer;
using DataAccessLayer.Models;
using DataAccessLayer.Repository;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Workers.ModelsView;

namespace Workers
{
    public class VacationServices
    {
        private static readonly Random rand = new Random();
        public IEFGenericRepository<Team> Teamrepository { get; set; }

        public IEFGenericRepository<Person> Personrepository { get; set; }
        public IEFGenericRepository<Weekend> Weekendrepository { get; set; }

        public IEFGenericRepository<Vacation> Vacationrepository { get; set; }
        public CountVacation Countvacation { get; set; }
        public VacationServices(IEFGenericRepository<Person> personrepository, IEFGenericRepository<Team> teamrepository, IEFGenericRepository<Weekend> weekendpository, IEFGenericRepository<Vacation> vacationrepository, CountVacation countVacation)
        {
            Personrepository = personrepository;
            Teamrepository = teamrepository;
            Weekendrepository = weekendpository;
            Vacationrepository = vacationrepository;
            Countvacation = countVacation;
        }
        public bool CreateVacation(Guid personId, VacationView vacation)
        {
            const string format = "M/d/yyyy";
            // Person person = Personrepository.FindById(personId);
            Person person = Personrepository.IncludeGet(p => p.Team).FirstOrDefault(x => x.Id == personId);
        

            Vacation NewVacation = new Vacation();
            NewVacation.Id = Guid.NewGuid();
      
            NewVacation.FirstDate = DateTime.ParseExact(vacation.startDay, format, CultureInfo.InvariantCulture);
            NewVacation.SecontDate = DateTime.ParseExact(vacation.EndDay, format, CultureInfo.InvariantCulture);
            NewVacation.Days = Countvacation.CountDaysVacation(NewVacation.FirstDate, NewVacation.SecontDate);
            NewVacation.People = person;
            if (!Countvacation.CheckonBusy(person, NewVacation.FirstDate, NewVacation.SecontDate))
            {
                return false;
            }
            person.Days -= Countvacation.CountDaysVacation(NewVacation.FirstDate, NewVacation.SecontDate);
            Vacationrepository.Create(NewVacation);
            return true;
        }
    }
}
