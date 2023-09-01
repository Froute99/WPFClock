using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MagicClock
{
	public partial class MainWindow : Window
	{
		public int monitorId = 1;
		public int offsetX = 525;
		public int offsetY = 45;

		System.Windows.Threading.DispatcherTimer Timer = new System.Windows.Threading.DispatcherTimer();


		public MainWindow()
		{
			InitializeComponent();

			label1.Content = "00:00";

			Timer.Tick += new EventHandler(Timer_Click);
			Timer.Interval = new TimeSpan(0, 0, 1);
			Timer.Start();

		}

		public void Test()
		{
			Window window = GetWindow(label1);

			window.Left = 0;
			window.Top = 0;

		}

		private void Timer_Click(object sender, EventArgs e)
		{
			DateTime d;
			d = DateTime.Now;
			string hourString = d.Hour.ToString();
			string minuteString = d.Minute.ToString();

			if (d.Hour < 10)   hourString = "0" + hourString;
			if (d.Minute < 10) minuteString = "0" + minuteString;

			label1.Content = hourString + " : " + minuteString;

			OutlineTextLabel(label1);
		}


		protected override void OnRender(DrawingContext drawingContext)
		{
			ChangeLocation();

			base.OnRender(drawingContext);
		}



///
/// HelperFunctions
///

		public void ChangeMonitor(int newMonitor)
		{
			monitorId = newMonitor;
		}

		public void ChangeOffset(int x, int y)
		{
			offsetX = x;
			offsetY = y;

			ChangeLocation();
		}

		void ChangeLocation()
		{
			int MonitorWidth = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;

			Screen[] screens = Screen.AllScreens;
			//int screenId = monitorId;


			Window window = GetWindow(label1);

			var screen = screens[0];
			var area = screen.WorkingArea;

			window.Left = area.Left;
			window.Top = area.Top;


			float ResolutionPortion = (float)MonitorWidth / area.Width;

			float desiredX = area.Left + (area.Width - offsetX) * ResolutionPortion;
			float desiredY = area.Top + offsetY * ResolutionPortion;
			var location = label1.PointToScreen(new Point(desiredX, desiredY));

			Trace.WriteLine(desiredX);
			Trace.WriteLine(desiredY);

			window.Left = location.X;
			window.Top = location.Y;
		}


		void OutlineTextLabel(System.Windows.Controls.Label lbl)
		{
			FormattedText formattedText = new FormattedText(lbl.Content.ToString(),
											  System.Globalization.CultureInfo.GetCultureInfo("ko-kr"),
											  System.Windows.FlowDirection.LeftToRight,
											  new Typeface(lbl.FontFamily, lbl.FontStyle, lbl.FontWeight, lbl.FontStretch),
											  lbl.FontSize,
											  Brushes.Black);

			formattedText.TextAlignment = TextAlignment.Center;

			Geometry geometry = formattedText.BuildGeometry(new Point(0, 0));

			PathGeometry pathGeometry = geometry.GetFlattenedPathGeometry();

			Canvas cvs = new Canvas();
			lbl.Content = cvs;

			Path pt = new Path();
			pt.Stroke = Brushes.White;
			pt.StrokeThickness = 1;
			pt.Fill = lbl.Foreground;
			pt.Stretch = Stretch.Fill;

			cvs.Children.Add(pt);

			Canvas.SetTop(pt, (cvs.ActualHeight - formattedText.Extent) / 100);
			Canvas.SetLeft(pt, (cvs.ActualWidth - formattedText.Width) / 100);

			pt.Data = pathGeometry;
		}

	}
}
