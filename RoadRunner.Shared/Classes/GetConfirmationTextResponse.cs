using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadRunner.Shared.Classes
{
    public class GetConfirmationTextResponse
    {
        public List<GetConfirmationTextResponseItem> ConfList { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine(Environment.NewLine);

            foreach (var card in ConfList)
            {
                sb.AppendLine(card.ToString());
            }

            return sb.ToString();
        }
    }
    public class GetConfirmationTextResponseItem
    {
        public String Header { get; set; }
        public String Body { get; set; }

        public override string ToString()
        {
            return String.Format("{0}Header = {1}{0}Body = {2}{0}", Environment.NewLine, Header, Body);
        }
    }
}
