using System;

namespace RoadRunner.Shared.Classes
{
    public class GetRecentPickUpAddressResponseItem
    {
        public String PickUpAddress { get; set; }
        public String RawInput { get; set; }

        public String Street { get; set; }
        public String City { get; set; }
        public String Zip { get; set; }
        public String Comment { get; set; }

        public override string ToString()
        {
            return String.Format("{0}PickUpAddress = {1}{0}{0}Street = {2} {0}City = {3} {0}Zip = {4} {0}Comment = {5} {0}{0}{0}RawInput = {6} {0}", Environment.NewLine,
                PickUpAddress, Street, City, Zip, Comment, RawInput);
        }
    }
}
