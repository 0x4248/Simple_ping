using System;
using System.IO;
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
using System.Windows.Shapes;
using System.Net.NetworkInformation;
namespace SimplePing
{
	/// <summary>
	/// Interaction logic for Custom_ping.xaml
	/// </summary>
	public partial class Custom_ping : Window
	{
		public Custom_ping()
		{
			InitializeComponent();
		}


		private void Start_ping_button_Click(object sender, RoutedEventArgs e)
		{
			if (!IsConnectedToInternet(Input_domain_or_ip.Text))
			{
				ping_time_text.Text = "Cant Connect";
				Fastorslow.Text = "";
				BitmapImage bitmap = new BitmapImage();
				bitmap.BeginInit();
				bitmap.UriSource = new Uri(Directory.GetCurrentDirectory() + "/cloud-cross.png");
				bitmap.EndInit();
				Status.Source = bitmap;
			}
			if (IsConnectedToInternet(Input_domain_or_ip.Text))
			{
				BitmapImage bitmap = new BitmapImage();
				bitmap.BeginInit();
				long pingspeed = pingtime(Input_domain_or_ip.Text);
				ping_time_text.Text = pingspeed.ToString()+"ms";
				if(pingspeed > 50)
				{
					Fastorslow.Text = "Slow";
					var bc = new BrushConverter();

					Fastorslow.Foreground = (Brush)bc.ConvertFrom("#FFDA0000");
					
					bitmap.UriSource = new Uri(Directory.GetCurrentDirectory() + "/error-cloud.png");
					
					Status.Source = bitmap;
				}
				if(pingspeed < 50)
				{
					Fastorslow.Text = "OK";
					var bc = new BrushConverter();

					Fastorslow.Foreground = (Brush)bc.ConvertFrom("#FF000000");
					bitmap.UriSource = new Uri(Directory.GetCurrentDirectory() + "/cloud-tick.png");
					Status.Source = bitmap;
				}
				if (pingspeed < 10)
				{
					Fastorslow.Text = "Fast";
					var bc = new BrushConverter();

					Fastorslow.Foreground = (Brush)bc.ConvertFrom("#FF46C700");
					bitmap.UriSource = new Uri(Directory.GetCurrentDirectory() + "/cloud-tick.png");
					Status.Source = bitmap;
				}
				bitmap.EndInit();
			}
		}
		public bool IsConnectedToInternet(string url)
		{
			bool result = false;
			Ping p = new Ping();
			try
			{
				PingReply reply = p.Send(url, 10000);

				if (reply.Status == IPStatus.Success)
					return true;
			}
			catch
			{

			}
			return result;
		}
		public long pingtime(string url)
		{
			Ping p = new Ping();
			try
			{
				PingReply reply = p.Send(url, 10000);

				if (reply.Status == IPStatus.Success)
					return reply.RoundtripTime;
			}
			catch
			{

			}
			return 0;
		}

		private void Input_domain_or_ip_GotFocus(object sender, RoutedEventArgs e)
		{
			Input_domain_or_ip.Text = "";
		}
	}
}
