using System;
using System.Linq;
using System.Xml.Linq;

namespace RoadRunner.Shared
{
	public class SignUpResponse
	{
        public string result { get; set; }
        public string message { get; set; }
        public string customerId { get; set; }

		public static SignUpResponse Parse(string xmlData)
		{
            throw new NotImplementedException();
			XDocument doc = XDocument.Parse (xmlData);
			XNamespace ns = doc.Root.GetDefaultNamespace ();

			var response = new SignUpResponse ();

			response.message = doc.Root.Descendants (ns + "Msg").First ().Value;
			response.result = doc.Root.Descendants (ns + "Result").First ().Value;

			var customerIDElements = doc.Root.Descendants (ns + "CustomerId").ToList ();
			if (customerIDElements.Count > 0)
				response.customerId = customerIDElements.First ().Value;

			return response;
		}
	}
}

