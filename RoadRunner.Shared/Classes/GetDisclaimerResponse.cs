using System;
using System.Collections.Generic;
using System.Text;

namespace RoadRunner.Shared.Classes
{
    public class GetDisclaimerResponse
    {
        public List<GetDisclaimerResponseItem> DisclaimerList { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine(Environment.NewLine);

            foreach (var card in DisclaimerList)
            {
                sb.AppendLine(card.ToString());
            }

            return sb.ToString();
        }
    }

    public class GetDisclaimerResponseItem
    {
        public String Header { get; set; }
        public String Body { get; set; }

        public override string ToString()
        {
            return String.Format("{0}Header = {1}{0}Body = {2}{0}", Environment.NewLine, Header, Body);
        }
    }
}
