//using DataAccessLayer;
//using DataAccessLayer.Models;
//using DataAccessLayer.Repository;
//using System;
//using System.Collections.Generic;
//using System.Globalization;
//using System.Linq;
//using System.Threading.Tasks;
//using Workers.ModelsView;

//namespace Workers
//{
//    public class DTOVacation
//    {
//        CountVacation Countvacation;
//        public DTOVacation()
//        { }
//        public DTOVacation(CountVacation countVacation)
//        {
//            Countvacation = countVacation;
//        }

//        public Guid Id { get; set; }
//        public DateTime FirstDate { get; set; }
//        private DateTime secontDate { get; set; }
//        public int Days { get; set; }
//        public DateTime SecontDate
//        {
//            get { return this.secontDate; }
//            set
//            {
//                this.secontDate = value;
//                this.Days = Countvacation.CountDaysVacation(this.FirstDate, this.SecontDate);
//            }
//        }
//        public Person People { get; set; }
//        public string TeamName { get; set; }
//        public bool IndexDate { get; set; }
//        public Guid Peopleid { get; set; }
//        public bool ConfirmedVacation { get; set; }
//    }
//}
