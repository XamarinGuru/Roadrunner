using System;
using System.Collections.Generic;
using System.Text;

namespace RoadRunner.Shared.Classes
{
    public class GetCreditCardDetailsNewForPhoneResponse
    {
        public String Result { get; set; }
        public String Msg { get; set; }
        public List<GetCreditCardDetailsNewForPhoneResponseItem> CardList { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine(Environment.NewLine);

            sb.AppendLine(String.Format("Result = {0}", Result));
            sb.AppendLine(String.Format("Msg = {0}", Msg));

            foreach (var card in CardList)
            {
                sb.AppendLine(card.ToString());
            }

            return sb.ToString();
        }
    }
}
