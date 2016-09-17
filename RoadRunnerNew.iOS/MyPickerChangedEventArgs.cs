using System;

namespace RoadRunnerNew.iOS
{
	public class MyPickerChangedEventArgs : EventArgs
	{
		public object SelectedKey {get; set;}
		public string SelectedValue { get; set;}
	}
}

