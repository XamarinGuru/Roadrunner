using System;
using System.Collections.Generic;
using System.Text;

namespace RoadRunner.Shared.Classes
{
    public class GetMyBookedReservationsResponse
    {
        public String Title { get; set; }
        public String Message { get; set; }
        public List<GetMyBookedReservationsResponseReservation> MyReservations { get; set; }
        
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine(Environment.NewLine);
            sb.AppendLine(String.Format("Title = {1}{0}Message = {2}", Environment.NewLine, Title, Message));
            sb.AppendLine(Environment.NewLine);
            int counter = 1;
            foreach (var reservation in MyReservations)
            {
                sb.AppendLine(Environment.NewLine);
                sb.AppendLine(counter.ToString());
                sb.AppendLine(Environment.NewLine);
                sb.AppendLine(reservation.ToString());
                sb.AppendLine(Environment.NewLine);
                counter++;
            }
            sb.AppendLine(Environment.NewLine);
            sb.AppendLine(Environment.NewLine);
            return sb.ToString();
        }
    }

    //<add>
    //<complex>b</complex>
    //<stNum>123</stNum>
    //<street>hihih</street>
    //<unit>v</unit>
    //<city>Ventura</city>
    //<zip>93001</zip>
    //</add>

    public class GetMyBookedReservationsResponseReservationLocation
    {
        public String stNum { get; set; }
        public String street { get; set; }
        public String complex { get; set; }
        public String unit { get; set; }
        public String city { get; set; }
        public String zip { get; set; }
        
        public String raw { get; set; }

        public bool IsMatchRegex { get; set; }

        public override string ToString()
        {
            return IsMatchRegex
                ? String.Format("{0}\tstNum = {1}{0}\tstreet = {2}{0}\tcomplex = {3}{0}\tunit = {4}{0}\tcity = {5}{0}\tzip = {6}{0}",
                    Environment.NewLine, stNum, street, complex, unit, city, zip) //raw = {7}{0}
                : raw;
        }
    }
}
