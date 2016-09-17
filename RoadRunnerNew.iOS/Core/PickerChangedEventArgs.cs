using System;

namespace RoadRunnerNew.iOS
{
	public class PickerChangedEventArgs : EventArgs
	{
		public string SelectedValue {get; set;}
		public string SelectedValueID { get; set;}
		public string SelectedValueDiff { get; set;}
	}
}

