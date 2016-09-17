using System;
using System.Collections.Generic;
using System.Text;

namespace RoadRunner.Shared.Classes
{
    public class GetRecentPickUpAddressResponse
    {
        public List<GetRecentPickUpAddressResponseItem> Items { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine(Environment.NewLine);

            foreach (var item in Items)
            {
                sb.AppendLine(item.ToString());
            }

            return sb.ToString();
        }
    }
}
