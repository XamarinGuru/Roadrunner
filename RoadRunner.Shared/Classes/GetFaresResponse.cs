using System;
using System.Collections.Generic;
using System.Text;

namespace RoadRunner.Shared.Classes
{
    public class GetFaresResponse
    {
        public String TITLE { get; set; }
        public String MSG { get; set; }
		public List<FareContainer> Fares { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine(Environment.NewLine);

            sb.AppendLine(String.Format("Result = {0}", TITLE));
            sb.AppendLine(String.Format("Msg = {0}", MSG));

            foreach (var item in Fares)
            {
                sb.AppendLine(item.ToString());
            }

            return sb.ToString();
        }
    }
}
