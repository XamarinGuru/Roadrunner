using System;

namespace RoadRunner.Shared.Classes
{
    public class CheckLoginForAndroidResponse
    {
        public String Result { get; set; }
        public String Message { get; set; }

        public String CustomerType { get; set; }
        public String Customerid { get; set; }
        public String FName { get; set; }
        public String LName { get; set; }
        public String PH { get; set; }
        public String Email { get; set; }

        public override string ToString()
        {
            return String.Format("{0}Result = {1}{0}Message = {2}{0}Type = {3}{0}Customerid = {4}{0}FName = {5}{0}LName = {6}{0}PH = {7}{0}Email = {8}{0}",
                Environment.NewLine, Result, Message, CustomerType, Customerid, FName, LName, PH, Email);
        }
    }
}
