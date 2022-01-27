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
			}
			if (IsConnectedToInternet(Input_domain_or_ip.Text))
			{
				long pingspeed = pingtime(Input_domain_or_ip.Text);
				ping_time_text.Text = pingspeed.ToString()+"ms";
				if(pingspeed > 50)
				{
					Fastorslow.Text = "Slow";
				}
				if(pingspeed < 50)
				{
					Fastorslow.Text = "OK";
				}
				if (pingspeed < 10)
				{
					Fastorslow.Text = "Fast";
				}

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
