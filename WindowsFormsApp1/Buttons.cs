using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
  public partial class Buttons : Form
  {
    public Buttons()
    {
      InitializeComponent();
    }

    private void Buttons_Load(object sender, EventArgs e)
    {
      makebuttons();
    }
    private void makebuttons()
    {
      for (int i = 0; i < 20; i++)
      {
        var b = new Button();
        b.Text = "Knop " + i;
        b.Left = (i % 5) * 120 + 20;
        b.Top= (i / 5) * 50 + 10;
        b.Tag = "" + i;
        b.Size = new Size(70, 30);
        this.Controls.Add(b);
        b.Click += HandleClick;
      }
    }
    private void HandleClick(object sender, EventArgs e)
    {
      Button b = sender as Button;
      MessageBox.Show($"You clicked button # {b.Tag}");
    }
  }
}
