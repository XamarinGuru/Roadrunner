using System;

namespace RoadRunner.Shared.Classes
{
    public class DeleteCreditCardNewResponse
    {
        public String Result { get; set; }
        public String Message { get; set; }

        public override string ToString()
        {
            return String.Format("{0}Result = {1}{0}Message = {2}{0}", Environment.NewLine, Result, Message);
        }
    }
}
