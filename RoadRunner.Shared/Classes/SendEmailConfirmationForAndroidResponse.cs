using System;

namespace RoadRunner.Shared
{
	public class SendEmailConfirmationForAndroidResponse
	{
		public String Result { get; set; }

		public String Message { get; set; }

		public String EmailSentOn { get; set; }

		public override string ToString ()
		{
			return String.Format ("{0}Result = {1}{0}Message = {2}{0}EmailSentOn = {3}{0}", Environment.NewLine, Result, Message, EmailSentOn);
		}
	}
}