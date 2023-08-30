using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MagicClock
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		System.Windows.Threading.DispatcherTimer Timer = new System.Windows.Threading.DispatcherTimer();
		public string Time { get; set; }

		public MainWindow()
		{
			InitializeComponent();


			Timer.Tick += new EventHandler(Timer_Click);
			Timer.Interval = new TimeSpan(0, 0, 1);
			Timer.Start();

		}

		private void Timer_Click(object sender, EventArgs e)
		{
			DateTime d;
			d = DateTime.Now;
			label1.Content = d.Hour + " : " + d.Minute;

			OutlinedTextLabel(label1);
		}

		protected override void OnRender(DrawingContext drawingContext)
		{
			var location = label1.PointToScreen(new Point(625, -15));

			Window window = GetWindow(label1);

			window.Left = location.X;
			window.Top = location.Y - window.Height;


			base.OnRender(drawingContext);
		}


		void OutlinedTextLabel(Label lbl)
		{
			FormattedText formattedText = new FormattedText(lbl.Content.ToString(),
				System.Globalization.CultureInfo.GetCultureInfo("ko-kr"),
				FlowDirection.LeftToRight,
				new Typeface(lbl.FontFamily, lbl.FontStyle, lbl.FontWeight, lbl.FontStretch),
				lbl.FontSize, Brushes.Black);

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
