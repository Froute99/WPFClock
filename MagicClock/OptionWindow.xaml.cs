using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Shapes;

namespace MagicClock
{
	/// <summary>
	/// Interaction logic for OptionWindow.xaml
	/// </summary>
	public partial class OptionWindow : Window
	{
		public OptionWindow()
		{
			InitializeComponent();





			//DataContext = new MonitorComboBox();
		}


		private void OnOKButtonClicked(object sender, RoutedEventArgs e)
		{
			String strX = InputBox_X.Text;
			String strY = InputBox_Y.Text;

			int inputX = Int32.Parse(strX);
			int inputY = Int32.Parse(strY);



			MainWindow clock = Window.GetWindow(App.Current.MainWindow) as MainWindow;
			clock.ChangeOffset(inputX, inputY);


		}


		//private void OnMonitorSelected(object sender, SelectionChangedEventArgs e)
		//{
		//	ComboBox cmb = sender as ComboBox;


		//	MainWindow clock = Window.GetWindow(App.Current.MainWindow) as MainWindow;
		//	Trace.WriteLine(monitorComboBox.SelectedIndex);
		//	clock.ChangeMonitor(monitorComboBox.SelectedIndex);

		//}

		//class MonitorComboBox
		//{
		//	public List<string> MonitorList { get; set; } = new List<string>()
		//	{
		//		"1",
		//		"2"
		//	};
		//}

		//private void monitorComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		//{

		//}
	}
}
