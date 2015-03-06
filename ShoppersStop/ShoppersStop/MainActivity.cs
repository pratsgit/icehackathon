using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace ShoppersStop
{
	[Activity (Label = "ShoppersStop", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity, GestureDetector.IOnGestureListener
	{
		int count = 1;

		private GestureDetector _gestureDetector;
		private TextView _textView;


		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button> (Resource.Id.myButton);
			_textView = FindViewById<TextView>(Resource.Id.velocity_text_view);
			_textView.Text = "Fling Velocity: ";
			_gestureDetector = new GestureDetector (this);
			button.Click += delegate {
				button.Text = string.Format ("{0} clicks!", count++);
			};


		}

		public bool OnDown (MotionEvent e)
		{
			return false;
		}

		public bool OnFling (MotionEvent e1, MotionEvent e2, float velocityX, float velocityY)
		{
			_textView.Text = String.Format("Fling velocity: {0} x {1}", velocityX, velocityY);
			return true;
		}

		public void OnLongPress (MotionEvent e)
		{

		}

		public bool OnScroll (MotionEvent e1, MotionEvent e2, float distanceX, float distanceY)
		{
			return false;
		}

		public void OnShowPress (MotionEvent e)
		{

		}

		public bool OnSingleTapUp (MotionEvent e)
		{
			return false;
		}

		public override bool OnTouchEvent(MotionEvent e)
		{
			_gestureDetector.OnTouchEvent(e);
			return false;
		}
	}
}


