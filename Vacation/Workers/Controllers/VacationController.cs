using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.Models;
using Workers.ModelsView;
using DataAccessLayer.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using DataAccessLayer;
using Microsoft.AspNetCore.Authorization;
using Workers.Models_View;
using System.Drawing;
using DataAccessLayer;
namespace Workers.Controllers
{
    [Authorize]
    [Route("[controller]/[action]/{personId?}")]
    
    public class VacationController : Controller
    {
        private static readonly Random rand = new Random();
        public IEFGenericRepository<Team> Teamrepository { get; set; }

        public IEFGenericRepository<Person> Personrepository { get; set; }
        public IEFGenericRepository<Weekend> Weekendrepository { get; set; }

        public IEFGenericRepository<Vacation> Vacationrepository { get; set; }
        public CountVacation Countvacation { get; set; }
        public VacationServices VacationServices { get; set; }
        public VacationController(IEFGenericRepository<Person> personrepository, IEFGenericRepository<Team> teamrepository, IEFGenericRepository<Weekend> weekendpository, IEFGenericRepository<Vacation> vacationrepository,CountVacation countVacation,VacationServices vacationServices)
        {
            Personrepository = personrepository;
            Teamrepository = teamrepository;
            Weekendrepository = weekendpository;
            Vacationrepository = vacationrepository;
            Countvacation = countVacation;
            VacationServices = vacationServices;
        }
        /*      [Route("/ShowVacation/{personId}") ]*/
        public IActionResult ShowVacation(Guid personId)
        {
            ViewBag.PersonId = personId;
            ViewBag.person = Personrepository.FindById(personId);
            IEnumerable<Vacation> vacations = Vacationrepository.IncludeGet(t => t.People).Where(i => i.Peopleid == personId);
            return View(vacations);
        }
        /*      [Route("/AddnewVacation/{personId}")]*/
        [Authorize(Roles = "admin, employee")]
        [HttpGet]
        public IActionResult AddnewVacation(Guid personId)
        {
            IEnumerable<Weekend>  weekends = Weekendrepository.Get(p => p.startDate> DateTime.Now);
            Person person = Personrepository.FindById(personId);
            ViewBag.AddDays = person.Days;
            ViewBag.weekends = weekends;
            ViewBag.PersonId = personId;
            return View();
        }
        /*        [Route("/CreateNewVacation/{personId}")]*/
        [Authorize(Roles = "admin,employee")]
        [HttpPost]
        public IActionResult AddnewVacation(Guid personId,VacationView vacation)
        {
            Person person = Personrepository.IncludeGet(p => p.Team).FirstOrDefault(x => x.Id == personId);
            if (!VacationServices.CreateVacation(personId, vacation)) {
                ModelState.AddModelError("EndDay", "Please change date");
                ViewBag.AddDays = person.Days;
                List<Weekend> weekends = Weekendrepository.Get(p => p.startDate > DateTime.Now).ToList();
                ViewBag.weekends = weekends;
                ViewBag.PersonId = personId;
                return View(vacation);
            }
            return Redirect("/Home/Workers/Home/Workers");
        }
           [Route("/Delete/{vacationId}/{personId}")]
        [Authorize(Roles = "admin, employee")]
        public IActionResult DeleteVacation(Guid vacationId,Guid personId)
        {
            Vacation vacation = Vacationrepository.FindById(vacationId);
            Person person = Personrepository.FindById(personId);
            person.Days += vacation.Days;
            Vacationrepository.Remove(vacation);
            Personrepository.Update(person);
            return Redirect("/Home/Workers/Home/Workers");
        }

        public IActionResult ShowCalendar(Guid PersonId)
        {
            return View();
        }
        public JsonResult GetEvents(Guid PersonId)
        {
            IEnumerable<Vacation> vacations = Vacationrepository.Get(p => p.Peopleid == PersonId);
            DateTime start;
            DateTime end;
            var viewModel = new CalendarEventy();
            var events = new List<CalendarEventy>();
            int numColors = 10;
            var colors = new List<string>();
            for (int j = 0; j < numColors; j++)
            {
                var random = new Random(); // Don't put this here!
                colors.Add(String.Format("#{0:X6}", random.Next(0x1000000)));
            }
            int i = 1;
            foreach(Vacation vacation in vacations)
            {
             start = vacation.FirstDate;
             end =vacation.SecontDate;
                events.Add(new CalendarEventy()
                {
                    Id= Guid.NewGuid(),
                    title = "Vacation"+i,
                    start = start.ToString("yyyy-MM-dd"),
                    end = end.ToString("yyyy-MM-dd"),
                    allDay = false,
                    backgroundColor=colors[i]
                });
                i++;
              
            }


            return Json(events.ToArray());
        }
        public IActionResult Confirm(Guid vacationId)
        {
            Vacation vacation = Vacationrepository.FindById(vacationId);
            vacation.ConfirmedVacation = true;
            Vacationrepository.Update(vacation);
            return Redirect("/Home/NewVacation");
        }
    }
}