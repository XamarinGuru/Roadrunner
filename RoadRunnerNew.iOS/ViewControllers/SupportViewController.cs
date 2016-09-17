using System;
using UIKit;
using Plugin.Messaging;

namespace RoadRunnerNew.iOS
{
	partial class SupportViewController : BaseTitleViewController
	{
		public SupportViewController (IntPtr handle) : base (handle)
		{
		    Title = "SUPPORT";
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad ();

			btnEmailToRR.TouchUpInside += (object sender, EventArgs e) => {
				var emailTask = MessagingPlugin.EmailMessenger;
				if(emailTask.CanSendEmail)
				{
					emailTask.SendEmail("support@rrshuttle.com", "roadrunner support team", "this is email");
				}else{
					var okAlertController = UIAlertController.Create("error", "you can't email on emulator", UIAlertControllerStyle.Alert);
					okAlertController.AddAction(UIAlertAction.Create("ok", UIAlertActionStyle.Default, null));
					PresentViewController(okAlertController, true, null);
				}
			};

			btnCallToRR.TouchUpInside += (object sender, EventArgs e) => {
				var phoneCallTask = MessagingPlugin.PhoneDialer;
				if(phoneCallTask.CanMakePhoneCall){
					phoneCallTask.MakePhoneCall("8053898496", "Roadrunner Support");
				}else{
					var okAlertController = UIAlertController.Create("error", "you can't make phone call on emulator", UIAlertControllerStyle.Alert);
					okAlertController.AddAction(UIAlertAction.Create("ok", UIAlertActionStyle.Default, null));
					PresentViewController(okAlertController, true, null);
				}
			};

			btnCallToRRTollFree.TouchUpInside += (object sender, EventArgs e) => {
				var phoneCallTask = MessagingPlugin.PhoneDialer;
				if(phoneCallTask.CanMakePhoneCall){
					phoneCallTask.MakePhoneCall("8052477919", "Roadrunner Support");
				}else{
					var okAlertController = UIAlertController.Create("error", "you can't make phone call on emulator", UIAlertControllerStyle.Alert);
					okAlertController.AddAction(UIAlertAction.Create("ok", UIAlertActionStyle.Default, null));
					PresentViewController(okAlertController, true, null);
				}
			};
		}
	}
}
