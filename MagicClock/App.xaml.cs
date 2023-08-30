using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Drawing;
using Winforms = System.Windows.Forms;
using System.IO;
using System.Windows.Media.Imaging;

namespace MagicClock
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		private Winforms.NotifyIcon tray;

		public App()
		{
			tray = new Winforms.NotifyIcon();
		}

		protected override void OnStartup(StartupEventArgs e)
		{
			SetTray();

			base.OnStartup(e);
		}

		private void SetTray()
		{
			BitmapImage mainIcon = new BitmapImage(new Uri("pack://application:,,,/Resources/hmm.bmp", UriKind.RelativeOrAbsolute));
			tray.Icon = Icon.FromHandle(BitmapImage2Bitmap(mainIcon).GetHicon());
			tray.Visible = true;


			BitmapImage exitIcon = new BitmapImage(new Uri("pack://application:,,,/Resources/power.bmp", UriKind.RelativeOrAbsolute));
			
			tray.ContextMenuStrip = new Winforms.ContextMenuStrip();
			tray.ContextMenuStrip.Items.Add("Exit", Image.FromHbitmap(BitmapImage2Bitmap(exitIcon).GetHbitmap()), OnExitClicked);
		}

		private void OnExitClicked(object sender, EventArgs e)
		{
			Shutdown();
		}

		protected override void OnExit(ExitEventArgs e)
		{
			tray.Dispose();

			base.OnExit(e);
		}


		private Bitmap BitmapImage2Bitmap(BitmapImage bitmapImage)
		{
			using (MemoryStream outStream = new MemoryStream())
			{
				BitmapEncoder enc = new BmpBitmapEncoder();
				enc.Frames.Add(BitmapFrame.Create(bitmapImage));
				enc.Save(outStream);
				System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(outStream);

				return new Bitmap(bitmap);
			}
		}

	}
}
