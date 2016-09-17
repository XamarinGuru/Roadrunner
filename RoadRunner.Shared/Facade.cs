using System;
using System.Threading.Tasks;
using RoadRunner.Shared.ViewModels;
using System.Collections.Generic;

namespace RoadRunner.Shared
{
	public sealed class Facade
	{
		private static readonly Lazy<Facade> lazy = new Lazy<Facade>(() => new Facade());

		public static Facade Instance { get { return lazy.Value; } }

		private Facade()
		{
			
		}

		private List<object> _UIViewControllerList = new List<object> ();
		public List<object> UIViewControllerList {
			get{
				return _UIViewControllerList;
			}
		}

		private ScheduleARideVM _currentRide = new ScheduleARideVM(); 
		public ScheduleARideVM CurrentRide{
			get{
				return _currentRide;
			}
		}

		public void ResetCurrentRide(){
			_currentRide.ResetVM ();
		}
	}
}

