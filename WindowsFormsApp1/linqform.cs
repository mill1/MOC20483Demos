using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace WindowsFormsApp1
{
  public partial class linqform : Form
  {
    public linqform()
    {
      InitializeComponent();
    }

    private void button1_Click(object sender, EventArgs e)
    {
      var di = new DirectoryInfo(System.Environment.SystemDirectory);
      //var q = from f in di.GetFiles()
      //        where f.Length > 500000
      //        orderby f.Length descending
      //        select new  { f.Name, size=f.Length, f.LastWriteTime};
      var q = di.GetFiles()
             .Where(f => f.Length > 500000)
             .OrderByDescending(f => f.Length)
             .Select(f => new { f.Name, size = f.Length, f.LastWriteTime });
      this.dataGridView1.DataSource = q.ToList();
    }

    private void button2_Click(object sender, EventArgs e)
    {
      var di = new DirectoryInfo(System.Environment.SystemDirectory);
      var xml = new XElement("Files",
           from f in di.GetFiles()
           where f.Length >500000
           orderby f.Length descending
           select new XElement("File",
             new XElement("Name", f.Name),
             new XElement("Size", f.Length)
        ));
      xml.Save(@"c:\users\cursist\files.xml");
    }

    private void button3_Click(object sender, EventArgs e)
    {
      var xml = XDocument.Load(@"c:\users\cursist\files.xml");
      var q = from f in xml.Element("Files").Elements("File")
              select new
              {
                Name = (string)f.Element("Name"),
                Size = (long)f.Element("Size"),
              };
      this.dataGridView1.DataSource = q.ToList();
    }
  }
}
