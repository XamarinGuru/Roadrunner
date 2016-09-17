using System;
using UIKit;
using System.Collections.Generic;
using Foundation;

namespace RoadRunnerNew.iOS
{
	public class MyPickerModel : UIPickerViewModel
	{
		private List<KeyValuePair<object, string>> _dic;
		public event EventHandler<MyPickerChangedEventArgs> PickerChanged;

		public MyPickerModel(List<KeyValuePair<object, string>> dic)
		{
			_dic = dic;
		}

		public override nint GetComponentCount (UIPickerView pickerView)
		{
			return 1;
		}

		public override nint GetRowsInComponent (UIPickerView pickerView, nint component)
		{
			return _dic.Count;
		}

		public override string GetTitle (UIPickerView pickerView, nint row, nint component)
		{
			return _dic[(int) row].Value;
		}

		public override UIView GetView (UIPickerView pickerView, nint row, nint component, UIView view)
		{
			var label = new UILabel ();

			label.Text = GetTitle (pickerView, row, component);
			label.TextAlignment = UITextAlignment.Center;
			label.Font = UIFont.FromName ("Raleway", 25f);
			label.TextColor = UIColor.White;
			label.Lines = 5;
			return label;
		}
		public override nfloat GetRowHeight (UIPickerView pickerView, nint component)
		{
			return 40f;
		}

		public override void Selected (UIPickerView pickerView, nint row, nint component)
		{
			if (PickerChanged != null && _dic.Count > 0)
			{
				var args = new MyPickerChangedEventArgs
				{
					SelectedKey = _dic[(int) row].Key,
					SelectedValue = _dic[(int) row].Value
				};
				PickerChanged(this, args);
			}
		}
	}
}

