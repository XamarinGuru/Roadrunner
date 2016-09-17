using System;

namespace RoadRunner.Shared.Classes
{
	public class GetFareForReservationCharterResponseItem : FareContainer
    {
        public String HourlyRate { get; set; }
        
        public override string ToString()
        {
            return String.Format("{0}id = {1} {0}NAME = {2} {0}capacity = {3} {0}amount = {4} {0}discountAmt = {5} {0}totalAmt = {6} {0}serviceImage = {7} {0}IMAGE = {8} {0}serviceid = {9} {0}HourlyRate = {10} {0} ", Environment.NewLine,
                id, NAME, capacity, amount, discountAmt, totalAmt, serviceImage, IMAGE, serviceid, HourlyRate);
        }
    }
}
