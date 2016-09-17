using System;
using System.Collections.Generic;
using RoadRunner.Shared;
using UIKit;

namespace RoadRunnerNew.iOS
{
	public class PickerModel : UIPickerViewModel
	{
		private readonly IList<string> values;

		public event EventHandler<PickerChangedEventArgs> PickerChanged;

		public PickerModel(IList<string> values)
		{
			this.values = values;
		}

		public override nint GetComponentCount (UIPickerView pickerView)
		{
			return 1;
		}

		public override nint GetRowsInComponent (UIPickerView pickerView, nint component)
		{
			return values.Count;
		}

		public override string GetTitle (UIPickerView pickerView, nint row, nint component)
		{
			return values[(int)row];
		}

		public override nfloat GetRowHeight (UIPickerView pickerView, nint component)
		{
			return 40f;
		}

		public override void Selected (UIPickerView pickerView, nint row, nint component)
		{
			if (this.PickerChanged != null)
			{
				this.PickerChanged(this, new PickerChangedEventArgs{SelectedValue = values[(int)row]});
			}
		}
	}


	public class PickerModel1 : UIPickerViewModel
	{
		private readonly IList<GetAirlineResponseItem> values;


		public event EventHandler<PickerChangedEventArgs> PickerChanged;


		public PickerModel1(IList<GetAirlineResponseItem> values)
		{
			this.values = values;
		}

		public override nint GetComponentCount (UIPickerView pickerView)
		{
			return 1;
		}

		public override nint GetRowsInComponent (UIPickerView pickerView, nint component)
		{
			return values.Count;
		}

		public override string GetTitle (UIPickerView pickerView, nint row, nint component)
		{
			return values[(int)row].Airline;
		}

		public override nfloat GetRowHeight (UIPickerView pickerView, nint component)
		{
			return 40f;
		}

		public override void Selected (UIPickerView pickerView, nint row, nint component)
		{
			if (this.PickerChanged != null)
			{
				this.PickerChanged(this, new PickerChangedEventArgs{SelectedValue = values[(int)row].Airline, SelectedValueID = values[(int)row].id, SelectedValueDiff = values[(int)row].diff});
			}
		}
	}
}

