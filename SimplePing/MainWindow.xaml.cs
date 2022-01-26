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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.NetworkInformation;
namespace SimplePing
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void Start_button_Click(object sender, RoutedEventArgs e)
		{
			Status_text.Text = "Testing...";
			string[] domains = { 
				"www.google.com",
				"www.microsoft.com"
			};
			bool isconnected = false;
			long[] times = new long[] {0,0};
			
			foreach (var current_domain in domains)
			{
				if (IsConnectedToInternet(current_domain))
				{
					isconnected = true;
					if(current_domain == "www.google.com")
					{
						times[0] = pingtime(current_domain);
					}
					if (current_domain == "www.microsoft.com")
					{
						times[1] = pingtime(current_domain);
					}
				}
				else
				{
					
				}
			}
			long avragetime = times[0] + times[1];
			avragetime = avragetime / 2;
			if (!isconnected)
			{
				Status_text.Text = "Not connected to Internet!";
				BitmapImage bitmap = new BitmapImage();
				bitmap.BeginInit();
				bitmap.UriSource = new Uri(Directory.GetCurrentDirectory() + "/wi-fi-disconnected.png");
				bitmap.EndInit();
				Status.Source = bitmap;
				ping_time.Text = "N/A";
				Start_button.Content = "Try again";
			} else
			{
				Status_text.Text = "Connected to Internet!";
				BitmapImage bitmap = new BitmapImage();
				bitmap.BeginInit();
				bitmap.UriSource = new Uri(Directory.GetCurrentDirectory() + "/wi-fi-connected.png");
				bitmap.EndInit();
				Status.Source = bitmap;
				ping_time.Text = avragetime.ToString()+"MS";
				Start_button.Content = "Refresh";
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
			catch { 
				
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
	}
}
