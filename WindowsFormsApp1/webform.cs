using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
  public partial class webform : Form
  {
    public webform()
    {
      InitializeComponent();
    }

    private void button1_Click(object sender, EventArgs e)
    {
      var url = "https://www.wikipedia.org/";
      var request = (HttpWebRequest)WebRequest.Create(url);
      var response = (HttpWebResponse)request.GetResponse();

      //var stream = new StreamReader( );
      //var s = stream.ReadToEnd();
      this.webBrowser1.DocumentStream = response.GetResponseStream();
      
    }
  }
}
