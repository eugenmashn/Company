using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccessLayer.Models
{
    public class Vacation

    {
        [NotMapped]
        CountVacation Countvacation;
        public Vacation()
        { }
        public  Vacation(CountVacation countVacation)
        {
            Countvacation = countVacation;
        }
     
        public Guid Id { get; set; }
        public DateTime FirstDate { get; set; }
        public int Days { get; set; }
        public DateTime SecontDate { get; set; }
        public Person People { get; set; }
        public string TeamName { get; set; }
        public bool IndexDate { get; set; }
        public Guid Peopleid { get; set; }
        public bool ConfirmedVacation { get; set; }
    }
}
