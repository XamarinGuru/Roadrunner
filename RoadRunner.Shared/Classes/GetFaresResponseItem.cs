using System;

namespace RoadRunner.Shared.Classes
{
	public class GetFaresResponseItem : FareContainer
    {
        public String isLA { get; set; }
        public String url { get; set; }
        public String discAmt { get; set; }
        public String gratuity { get; set; }

        public override string ToString()
        {
            return String.Format("{0}id = {1} {0}NAME = {2} {0}capacity = {3} {0}amount = {4} {0}discountAmt = {5} {0}totalAmt = {6} {0}serviceImage = {7} {0}IMAGE = {8} {0}serviceid = {9} {0}isLA = {10} {0}url = {11} {0}discAmt = {12} {0}gratuity = {13}{0} ", Environment.NewLine,
                id, NAME, capacity, amount, discountAmt, totalAmt, serviceImage, IMAGE, serviceid, isLA, url, discAmt, gratuity);
        }
    }
}
