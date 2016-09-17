
using System.Collections.Generic;

using Android.Content;
using Android.Views;
using Android.Widget;

namespace RoadRunner.Android
{
	class MenuListAdapter : BaseAdapter {

		Context mContext;
		List<Menu_Item> mNavItems;

		public MenuListAdapter(Context context, List<Menu_Item> navItems) {
			mContext = context;
			mNavItems = navItems;
		}

		public override int Count {
			get {
				return mNavItems.Count;
			}
		}

		public override Java.Lang.Object GetItem (int position)
		{
			return null;
		}

		public Menu_Item GetNavItem(int position)
		{
			return mNavItems [position];
		}

		override public long GetItemId(int position) {
			return position;
		}

		public override View GetView (int position, View convertView, ViewGroup parent)
		{
			if (convertView == null) 
			{
				convertView = LayoutInflater.From (mContext).Inflate (Resource.Layout.item_menu, null);
			}
			var titleView = (TextView)convertView.FindViewById (Resource.Id.title);
			var iconView = (ImageView)convertView.FindViewById (Resource.Id.icon);

			var navItem = (Menu_Item) GetNavItem(position);
			titleView.Text = navItem.mTitle;
			iconView.SetImageResource (navItem.mIcon);
			return convertView;
		}
	}
}