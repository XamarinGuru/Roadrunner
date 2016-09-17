﻿using System;

namespace RoadRunner.Shared
{
	public class CancelReservationForAndroidResponse

	{
		public String Result { get; set; }
		public String Message { get; set; }

		public override string ToString()
		{
			return String.Format("{0}Result = {1}{0}Message = {2}{0}", Environment.NewLine, Result, Message);
		}
	}

}

