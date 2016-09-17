using System;
using System.Collections.Generic;
using System.Text;

namespace RoadRunner.Shared
{
	public class GetSpecialServicesResponse
	{
		public String Title { get; set; }
		public String Message { get; set; }
		public List<GetSpecialServicesResponseItem> SpecialServices { get; set; }

		public bool IsExtraBaggageAvailable
		{
			get
			{
				return IsProductFound (Constant.EXTRA_BAGGAGE_PRODUCT_ID);
			}
		}

		public bool IsMeetAndGreetAvailable
		{
			get
			{
				return IsProductFound (Constant.MEET_AND_GREET_PRODUCT_ID);
			}
		}

		public double AdditionalBaggageCost 
		{
			get 
			{
				return GetProductCost (Constant.EXTRA_BAGGAGE_PRODUCT_ID);
			}
		}

		public string AdditionalBaggageProductID
		{
			get 
			{
				return Constant.EXTRA_BAGGAGE_PRODUCT_ID;
			}
		}

		public double MeetAndGreetCost
		{
			get
			{
				return GetProductCost (Constant.MEET_AND_GREET_PRODUCT_ID);
			}
		}

		public string MeetAndGreetProductID
		{
			get
			{
				return Constant.MEET_AND_GREET_PRODUCT_ID;
			}
		}

		private bool IsProductFound(string productID)
		{
			if (this.SpecialServices == null || this.SpecialServices.Count == 0)
				return false;

			bool isFound = false;
			foreach(var service in this.SpecialServices)
			{
				if (service.ProductID == productID)
				{
					isFound = true;
					break;
				}
			}

			return isFound;
		}

		private double GetProductCost(string productID)
		{
			if (this.SpecialServices == null || this.SpecialServices.Count == 0)
				return 0;

			double cost = 0;
			foreach(var service in this.SpecialServices)
			{
				if (service.ProductID == productID)
				{
					cost = double.Parse (service.Price);
					break;
				}
			}

			return cost;
		}

		public override string ToString()
		{
			var sb = new StringBuilder();
			sb.AppendLine(Environment.NewLine);

			sb.AppendLine(String.Format("Title = {0}", Title));
			sb.AppendLine(String.Format("Message = {0}", Message));

			foreach (var item in SpecialServices)
			{
				sb.AppendLine(item.ToString());
			}

			return sb.ToString();
		}
	}

	public class GetSpecialServicesResponseItem
	{
		public String ProductID { get; set; }
		public String Product { get; set; }
		public String Price { get; set; }
		public String Quantity { get; set; }
		public String Type { get; set; }

		public override string ToString()
		{
			return String.Format("{0}ProductID = {1}{0}Product = {2}{0}Price = {3}{0}Quantity = {4}{0}Type = {5}{0}",
				Environment.NewLine, ProductID, Product, Price, Quantity, Type);
		}
	}

}

