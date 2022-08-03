using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace AssignmentBD.Common
{
    public class CommonUtitlity
    {
        private IDictionary<int, string> EntryPoints = new Dictionary<int, string>();
        private IDictionary<int, string> NationalHolidays = new Dictionary<int, string>();

        public IDictionary<int, string> LoadEntryPoints()
        {
            EntryPoints.Add(0, "Zero Point".ToUpper());
            EntryPoints.Add(5, "NS Interchange".ToUpper());
            EntryPoints.Add(10, "Ph4 Interchange".ToUpper());
            EntryPoints.Add(17, "Ferozpur Interchange".ToUpper());
            EntryPoints.Add(24, "Lake City Interchange".ToUpper());
            EntryPoints.Add(29, "Raiwand Interchange".ToUpper());
            EntryPoints.Add(34, "Bahria Interchange".ToUpper());
            return EntryPoints;
        }

        public bool IsNationalHolidayDiscount(string month,int day)
        {
            NationalHolidays.Clear();
            NationalHolidays.Add(23, "MARCH");
            NationalHolidays.Add(14, "AUGUST");
            NationalHolidays.Add(25, "DECEMBER");

           return NationalHolidays.Where(x => x.Key == day && x.Value.Equals(month.ToUpper())).Any();
        }

        public string EvenOrOdd(string vNumber)
        {
            var num =Convert.ToInt16(vNumber.Split('-')[1]);
            return (num%2==0)?"even":"odd";
        }
       
    }
    public abstract class CommonProps
    {
        public HttpStatusCode HttpStatus { get; set; }
        public string Message { get; set; }
    }
}