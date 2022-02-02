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
using System.Net;

namespace SimplePing
{
	/// <summary>
	/// Interaction logic for Network_Info.xaml
	/// </summary>
	public partial class Network_Info : Window
	{
		public Network_Info()
		{
			InitializeComponent();
			string hostName = Dns.GetHostName(); // Retrive the Name of HOST
			Computer_Name_Text.Text = hostName;
			// Get the IP
			string myIP = Dns.GetHostByName(hostName).AddressList[0].ToString();
			Local_IP_Text.Text =  myIP;
			
		}

	}
}
