
using System;
using System.Collections.Generic;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;

using RoadRunner.Shared;
using RoadRunner.Shared.Classes;

using System.Threading.Tasks;

namespace RoadRunner.Android
{
	[Activity (Label = "ThankYouActivity")]			
	public class ThankYouActivity : BaseActivity
	{
		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			SetContentView(Resource.Layout.ThankYou);

			GetConfirmText();

			var btnShareFacebook = (ImageView)FindViewById(Resource.Id.btnShareFacebook);
			btnShareFacebook.Click += (object sender, EventArgs e) =>
			{
				var uri = global::Android.Net.Uri.Parse("https://www.facebook.com/sharer/sharer.php?u=https%3A//www.rrshuttle.com/index.html");
				var intent = new Intent(Intent.ActionView, uri);
				StartActivity(intent);
			};

			var btnShareLinkedin = (ImageView)FindViewById(Resource.Id.btnShareLinkedin);
			btnShareLinkedin.Click += (object sender, EventArgs e) =>
			{
				var uri = global::Android.Net.Uri.Parse("https://www.linkedin.com/shareArticle?mini=true&url=https%3A//www.rrshuttle.com&title=Roadrunner%20Shuttle%20and%20Limousine%20Service&summary=Check%20out%20my%20ride!&source=");
				var intent = new Intent(Intent.ActionView, uri);
				StartActivity(intent);
			};

			var btnShareGoogle = (ImageView)FindViewById(Resource.Id.btnShareGoogle);
			btnShareGoogle.Click += (object sender, EventArgs e) =>
			{
				var uri = global::Android.Net.Uri.Parse("https://plus.google.com/share?url=https%3A//www.rrshuttle.com");
				var intent = new Intent(Intent.ActionView, uri);
				StartActivity(intent);
			};

			var btnDone = (Button)FindViewById (Resource.Id.btn_Done);
			btnDone.Click += delegate(object sender , EventArgs e )
			{
				Facade.Instance.CurrentRide.ResetVM();
				StartActivity(new Intent(this,typeof(MainActivity)));
			};

			var btnReturn = (Button)FindViewById (Resource.Id.btn_Return);
			btnReturn.Click += delegate(object sender , EventArgs e )
			{
				ReverseTripInfo();
				StartActivity(new Intent(this,typeof(RideInformationActivity)));
				OverridePendingTransition (Resource.Animation.fromRight, Resource.Animation.toLeft);
			};
		}

		private void ReverseTripInfo()
		{
			var PickUpLocation = Facade.Instance.CurrentRide.PickUpLocation;
			var PickUpLocationName = Facade.Instance.CurrentRide.PickUpLocationName;
			var PickUpLocationId = Facade.Instance.CurrentRide.PickUpLocationId;
			var PickUpLocationLatitude = Facade.Instance.CurrentRide.PickUpLocationLatitude;
			var PickUpLocationLongitude = Facade.Instance.CurrentRide.PickUpLocationLongitude;
			var PickUpAirlines = Facade.Instance.CurrentRide.PickUpAirlines;
			var PickUpAirlinesId = Facade.Instance.CurrentRide.PickUpAirlinesId;
			var PickUpFlightNumber = Facade.Instance.CurrentRide.PickUpFlightNumber;
			var PickUpFlightTime = Facade.Instance.CurrentRide.PickUpFlightTime;
			var PickUpFlightTypeIsDomestic = Facade.Instance.CurrentRide.PickUpFlightTypeIsDomestic;
			var PickUpLocation3CharacterAirportCode = Facade.Instance.CurrentRide.PickUpLocation3CharacterAirportCode;
			var PickUpLocationZip = Facade.Instance.CurrentRide.PickUpLocationZip;
			var IsPickUpLocationAirport = Facade.Instance.CurrentRide.IsPickUpLocationAirport;

			Facade.Instance.CurrentRide.PickUpLocation = Facade.Instance.CurrentRide.DropOffLocation;
			Facade.Instance.CurrentRide.PickUpLocationName = Facade.Instance.CurrentRide.DropOffLocationName;
			Facade.Instance.CurrentRide.PickUpLocationId = Facade.Instance.CurrentRide.DropOffLocationId;
			Facade.Instance.CurrentRide.PickUpLocationLatitude = Facade.Instance.CurrentRide.DropOffLocationLatitude;
			Facade.Instance.CurrentRide.PickUpLocationLongitude = Facade.Instance.CurrentRide.DropOffLocationLongitude;
			Facade.Instance.CurrentRide.PickUpAirlines = Facade.Instance.CurrentRide.DropOffAirlines;
			Facade.Instance.CurrentRide.PickUpAirlinesId = Facade.Instance.CurrentRide.DropOffAirlinesId;
			Facade.Instance.CurrentRide.PickUpFlightNumber = Facade.Instance.CurrentRide.DropOffFlightNumber;
			Facade.Instance.CurrentRide.PickUpFlightTime = Facade.Instance.CurrentRide.DropOffFlightTime;
			Facade.Instance.CurrentRide.PickUpFlightTypeIsDomestic = Facade.Instance.CurrentRide.DropOffFlightTypeIsDomestic;
			Facade.Instance.CurrentRide.PickUpLocation3CharacterAirportCode = Facade.Instance.CurrentRide.DropOffLocation3CharacterAirportCode;
			Facade.Instance.CurrentRide.PickUpLocationZip = Facade.Instance.CurrentRide.DropOffLocationZip;
			Facade.Instance.CurrentRide.IsPickUpLocationAirport = Facade.Instance.CurrentRide.IsDropOffLocationAirport;

			Facade.Instance.CurrentRide.DropOffLocation = PickUpLocation;
			Facade.Instance.CurrentRide.DropOffLocationName = PickUpLocationName;
			Facade.Instance.CurrentRide.DropOffLocationId = PickUpLocationId;
			Facade.Instance.CurrentRide.DropOffLocationLatitude = PickUpLocationLatitude;
			Facade.Instance.CurrentRide.DropOffLocationLongitude = PickUpLocationLongitude;
			Facade.Instance.CurrentRide.DropOffAirlines = PickUpAirlines;
			Facade.Instance.CurrentRide.DropOffAirlinesId = PickUpAirlinesId;
			Facade.Instance.CurrentRide.DropOffFlightNumber = PickUpFlightNumber;
			Facade.Instance.CurrentRide.DropOffFlightTime = PickUpFlightTime;
			Facade.Instance.CurrentRide.DropOffFlightTypeIsDomestic = PickUpFlightTypeIsDomestic;
			Facade.Instance.CurrentRide.DropOffLocation3CharacterAirportCode = PickUpLocation3CharacterAirportCode;
			Facade.Instance.CurrentRide.DropOffLocationZip = PickUpLocationZip;
			Facade.Instance.CurrentRide.IsDropOffLocationAirport = IsPickUpLocationAirport;
		}

		private void GetConfirmText()
		{
			ShowLoadingView("Loading data...");

			String result = String.Empty;

			Task runSync = Task.Factory.StartNew(async () =>
			{
				var dic = new Dictionary<String, String>
				{
					{Constant.GETCONFIRMATIONTEXT_RESID, String.IsNullOrEmpty(Facade.Instance.CurrentRide.ReservationID) ? "1536724" : Facade.Instance.CurrentRide.ReservationID}
				};

				result = await AppData.ApiCall(Constant.GETCONFIRMATIONTEXT, dic);

				var tt = (GetConfirmationTextResponse)AppData.ParseResponse(Constant.GETCONFIRMATIONTEXT, result);

				RunOnUiThread(() =>
				{
					var aaa = tt.ConfList;
				});

				HideLoadingView();
			}).Unwrap();
		}
	}
}

