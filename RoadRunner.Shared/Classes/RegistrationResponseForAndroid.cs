using System;

namespace RoadRunner.Shared.Classes
{
    public class RegistrationResponseForAndroid
    {
        public String Result { get; set; }
        public String AlreadyExist { get; set; }
        public String Msg { get; set; }
        public String CustomerId { get; set; }

        public override string ToString()
        {
            return String.Format("{0}Result = {1}{0}AlreadyExist = {2}{0}Msg = {3}{0}Customerid = {4}{0}",
                Environment.NewLine, Result, AlreadyExist, Msg, CustomerId);
        }
    }
}
