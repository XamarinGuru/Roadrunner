using System;

namespace RoadRunner.Shared.Classes
{
	public class ResetPasswordResponse
	{
		public string Result { get; set; }
		public string Msg { get; set; }

		public override string ToString()
		{
			return String.Format("{0}Result = {1}{0}Msg = {2}{0}", Environment.NewLine, Result, Msg);
		}
	}
}

