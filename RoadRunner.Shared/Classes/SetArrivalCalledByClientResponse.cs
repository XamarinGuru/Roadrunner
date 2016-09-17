using System;

namespace RoadRunner.Shared.Classes
{
    public class SetArrivalCalledByClientResponse
    {
        public String Result { get; set; }
        public String eventStatus { get; set; }
        public String Wait { get; set; }
        public String APInst { get; set; }
        public String EndInst { get; set; }

        public override string ToString()
        {
            return String.Format("{0}Result = {1}{0}" +
                                 "eventStatus = {2}{0}" +
                                 "Wait = {3}{0}" +
                                 "APInst = {4}{0}" +
                                 "EndInst = {5}{0}",
                Environment.NewLine, Result, eventStatus, Wait, APInst, EndInst);
        }
    }
}
