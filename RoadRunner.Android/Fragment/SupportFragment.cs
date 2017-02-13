
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

using Plugin.Messaging;

namespace RoadRunner.Android
{
	public class SupportFragment : Fragment
	{
		//MainActivity mSuperActivity;

		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
		}

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			var view = inflater.Inflate(Resource.Layout.fragment_support, container, false);

			var txtEmail = (TextView)view.FindViewById(Resource.Id.txtSupportEmail);
			txtEmail.Touch += sendEmail;

			var txtPhone = (TextView)view.FindViewById(Resource.Id.txtSupportPhone);
			txtPhone.Touch += callPhone;

			var txtFree = (TextView)view.FindViewById(Resource.Id.txtSupportFree);
			txtFree.Touch += callFree;

			return view;
		}

		private void sendEmail(object sender, View.TouchEventArgs e)
		{
			if (e.Event.Action == MotionEventActions.Down)
			{
				var emailTask = MessagingPlugin.EmailMessenger;
				if (emailTask.CanSendEmail)
				{
					emailTask.SendEmail("support@rrshuttle.com", "roadrunner support team", "this is email");
				}
				else {
					AlertDialog.Builder alert = new AlertDialog.Builder(this.Activity);
					alert.SetTitle("error");
					alert.SetMessage("You can't email on emulator");
					alert.SetPositiveButton("OK", (senderAlert, args) => { });
					alert.Show();
				}
			}
		}
		private void callPhone(object sender, View.TouchEventArgs e)
		{
			if (e.Event.Action == MotionEventActions.Down)
			{
				var phoneCallTask = MessagingPlugin.PhoneDialer;
				if (phoneCallTask.CanMakePhoneCall)
				{
					phoneCallTask.MakePhoneCall("8053898496", "Roadrunner Support");
				}
				else {
					AlertDialog.Builder alert = new AlertDialog.Builder(this.Activity);
					alert.SetTitle("error");
					alert.SetMessage("You can't email on emulator");
					alert.SetPositiveButton("OK", (senderAlert, args) => { });
					alert.Show();
				}
			}
		}
		private void callFree(object sender, View.TouchEventArgs e)
		{
			if (e.Event.Action == MotionEventActions.Down)
			{
				var phoneCallTask = MessagingPlugin.PhoneDialer;
				if (phoneCallTask.CanMakePhoneCall)
				{
					phoneCallTask.MakePhoneCall("8052477919", "Roadrunner Support");
				}
				else {
					AlertDialog.Builder alert = new AlertDialog.Builder(this.Activity);
					alert.SetTitle("error");
					alert.SetMessage("You can't email on emulator");
					alert.SetPositiveButton("OK", (senderAlert, args) => { });
					alert.Show();
				}
			}
		}
	}
}

