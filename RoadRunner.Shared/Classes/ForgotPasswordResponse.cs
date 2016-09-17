using System;

namespace RoadRunner.Shared.Classes
{
    public class ForgotPasswordResponse
    {
        public String status { get; set; }
        public String MSG { get; set; }
        public String Result { get; set; }

        public override string ToString()
        {
            return String.Format("{0}status = {1}{0}message = {2}{0}result = {3}{0}", Environment.NewLine, status, MSG, Result);
        }
    }
}