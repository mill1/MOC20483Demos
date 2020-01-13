using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Threading;
using System.Data.OleDb;
using System.Security.Cryptography;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using xl=Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace WindowsFormsApp1
{
  enum TShirtSize
  {
    Small = 10,
    Medium = 20,
    Large = 30,
    XLarge = 35,
    XXLarge = 40,
  }
  struct Person
  {
    public string Name, Phone;
    public DateTime DateOfBirth;
    public override string ToString()
    {
      return $"Name = {Name}, Phone = {Phone}," +
             $"Date Of Birth = {DateOfBirth.ToString("dd-MM-yyyy")}";
    }
  }

  public partial class Form1 : Form
  {
    public Form1()
    {
      InitializeComponent();
    }

    private void button1_Click(object sender, EventArgs e)
    {
      const int million = 1_000_000;
      var a = "januari 15 2017";
      DateTime b = Convert.ToDateTime(a, new CultureInfo("nl-NL"));
      System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("pt-PT");
      MessageBox.Show(b.ToLongDateString());
      Trace.WriteLine($"Datum in het Portugees:{b.ToLongDateString()}");
    }

    private void button2_Click(object sender, EventArgs e)
    {
      string s = "";
      Stopwatch sw = new Stopwatch();
      var sb = new StringBuilder();
      sw.Start();
      for (int i = 0; i < 400000; i++)
      {
        sb.Append("a");
      }
      s = sb.ToString();
      sw.Stop();
      MessageBox.Show(sw.ElapsedMilliseconds.ToString());
    }

    private void button3_Click(object sender, EventArgs e)
    {
      int a = 10;
      strange(ref a);
      Console.WriteLine(a);
      Console.WriteLine(triple(5));
      Console.WriteLine(triple(5.0));
      Console.WriteLine(multiply(a: 3, b: 4, d: 5));
      FillLabel(label1, "hello", Color.CornflowerBlue);
      a = trymyparse(null);
      Trace.WriteLine("Function button clicked");
    }
    void strange(ref int x)
    {
      x = 20;
    }
    int triple(int x)
    {
      return x * 3;
    }
    double triple(double x)
    {
      return x * 3;
    }
    double multiply(double a, double b, double c = 1, double d = 1)
    {
      return a * b * c * d;
    }

    void FillLabel(Label lbl, string s)
    {
      FillLabel(lbl, s, Color.Red);
    }
    void FillLabel(Label lbl, string s, Color col)
    {
      lbl.Text = s;
      lbl.ForeColor = col;
    }
    int trymyparse(string s)
    {
      try
      {
        int x = int.Parse(s);
        return x;
      }
      catch (FormatException ex)
      {
        Console.WriteLine(ex.Message);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
      }
      finally
      {
        Console.WriteLine("quitting function...");
      }
      return 0;
    }
    PerformanceCounter pfc;
    private void Form1_Load(object sender, EventArgs e)
    {
      var x = new TextWriterTraceListener(@"C:\Users\e.nijhuis\source\repos\TrainingC#_demos\tracestuff.txt");
      Trace.Listeners.Add(x);
      Trace.AutoFlush = true;
      try
      {
        //pfc = new PerformanceCounter("democsharp", "hitcount", false);
      }
      catch(Exception ex)
      {
        MessageBox.Show(ex.Message);
        //PerformanceCounterCategory.Create("democsharp", "x", PerformanceCounterCategoryType.MultiInstance, "hitcount", "y");
        //pfc = new PerformanceCounter("aabbcc", "hitcount", false);
    }
    }

    private void timer1_Tick(object sender, EventArgs e)
    {
      var r = new Random();
      var max = r.Next(6);
      for (int i = 0; i < max; i++)
      {
        pfc.Increment();
      }
    }

    private void button4_Click(object sender, EventArgs e)
    {
      TShirtSize size = TShirtSize.XLarge;
      Console.WriteLine((int)size);
      int x = 30;
      var sz = (TShirtSize)x;
      Console.WriteLine(sz);
      string s = "Medium";
      sz = (TShirtSize)Enum.Parse(typeof(TShirtSize), s);
      Console.WriteLine(sz);
    }

    private void button5_Click(object sender, EventArgs e)
    {
      Person p;
      p.Name = "Jan";
      p.Phone = "0612345678";
      p.DateOfBirth = new DateTime(1990, 7, 5);
      Console.WriteLine(p);
    }

    private void button6_Click(object sender, EventArgs e)
    {
      var f = new linqform();
      f.ShowDialog();
    }

    private void button7_Click(object sender, EventArgs e)
    {
      int[] a = { 3, 5, 7, 9, 12, 6, 8, 20, 2 };
      var q = (from i in a where i < 9 select i).ToList();
      //a[0] = 100;
      Console.WriteLine(q.Count());
      Console.WriteLine(a.Sum(x => x * x));
      Console.WriteLine(a.Any(f => f > 15));
      Console.WriteLine(a.All(f => f > 0));
      Console.WriteLine(a.FirstOrDefault(f => f > 10));
    }

    private void button8_Click(object sender, EventArgs e)
    {
      float x = 10f;
      object o = x; // boxing
      int a = (int)(float)o;  // unboxing
      Console.WriteLine(a);
    }

    Tree t;
    private void button9_Click(object sender, EventArgs e)
    {
      try
      {
        t = new Tree("Eik", 20, new DateTime(2000, 10, 30));
        t.HeightChanged += this.T_HeightChanged;
        t.Grow(7);
        MessageBox.Show($"{t.Name} {t.Height} {t.Age}");
      }
      catch (IllegalHeightException ex)
      {
        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void T_HeightChanged(object sender, HeightChangedEventArgs e)
    {
      label1.Text = $"New height = {e.NewHeight}";
    }

    private void button10_Click(object sender, EventArgs e)
    {
      t.Grow(3);
    }

    private void button11_Click(object sender, EventArgs e)
    {
      var f = new Buttons();
      f.ShowDialog();
    }
    public void ChangePerson(Person1 p)
    {
      p.name = "john";
    }
    public void ChangePerson(ref Person2 p)
    {
      p.name = "john";
    }

    private void button12_Click(object sender, EventArgs e)
    {
      Person1 p1 = new Person1();
      p1.name = "Ann";
      Person2 p2;
      p2.name = "Ann";
      p2.address = "123";
      ChangePerson(p1);
      ChangePerson(ref p2);
      Console.WriteLine(p1.name);
      Console.WriteLine(p2.name);
      test123.doiets();
    }

    private void button13_Click(object sender, EventArgs e)
    {
      Tree[] Trees = new Tree[5];
      Trees[0] = new Tree("Eik", 7);
      Trees[1] = new Tree("Beuk", 17);
      Trees[2] = new Tree("Hazelaar", 3);
      Trees[3] = new Tree("Jeneverbes", 11);
      Trees[4] = new Tree("Plataan", 9);
      for (int i = 0; i < Trees.Length; i++)
      {
        Console.WriteLine(Trees[i]);

      }
      Console.WriteLine("sorting...");
      Array.Sort(Trees);
      for (int i = 0; i < Trees.Length; i++)
      {
        Console.WriteLine(Trees[i]);
      }
    }

    private void button14_Click(object sender, EventArgs e)
    {
      Forest f = new Forest();
      f.Add(new Tree("Eik", 7));
      f.Add(new Tree("Beuk", 17));
      f.Add(new Tree("Hazelaar", 3));
      f.Add(new Tree("Jeneverbes", 11));
      f.Add(new Tree("Plataan", 9));
      foreach (var tree in f)
      {
        Console.WriteLine(tree);
      }
      foreach (var tree in f.TreesByName)
      {
        Console.WriteLine(tree);
      }
    }

    private void button15_Click(object sender, EventArgs e)
    {
      Animal a = new Dog();
      a.Eat();
      TestAnimal(a);
      TestAnimal(new Cat());
    }
    public void TestAnimal(Animal a)
    {
      a.Eat();
      if (a is Dog)
        ((Dog)a).Bark();
      Cat c = a as Cat;
      if (c != null)
        c.Miaow();
    }

    private void button16_Click(object sender, EventArgs e)
    {
      double d = 20;
      MessageBox.Show(d.Treble().ToString());
      MessageBox.Show(5.0.Treble().ToString());
    }

    private void button17_Click(object sender, EventArgs e)
    {
      MessageBox.Show(File.ReadAllText(@"c:\users\cursist\tracestuff.txt"));
    }

    private void button18_Click(object sender, EventArgs e)
    {
      Tree t = new Tree("beuk", 42, new DateTime(2002, 7, 23));
      Console.WriteLine(t);
      var formatter = new BinaryFormatter();
      var stream = File.Create(@"c:\users\cursist\tree.dat");
      formatter.Serialize(stream, t);
      stream.Close();
    }

    private void button19_Click(object sender, EventArgs e)
    {
      var formatter = new BinaryFormatter();
      var stream = File.OpenRead(@"c:\users\cursist\tree.dat");
      var t = (Tree)formatter.Deserialize(stream);
      Console.WriteLine(t);

    }

    private void button20_Click(object sender, EventArgs e)
    {
      int total = 0;
      using (var sr = new StreamReader(@"c:\users\cursist\numbers.txt"))
      {
        for (; ; )
        {
          string s = sr.ReadLine();
          if (s == null)
            break;
          if (s.Trim().Length > 0 && int.TryParse(s, out int number))
          {
            total += number;
          }
        }
      }
      Console.WriteLine(total);
    }


    private void button22_Click(object sender, EventArgs e)
    {
      Stopwatch sw = new Stopwatch();
      sw.Start();
      var cnn = new OleDbConnection("Provider=SQLNCLI11.1;Integrated Security=SSPI;Persist Security Info=False;User ID=\"\";Initial Catalog=AdventureWorks2017;Data Source=\"\";Initial File Name=\"\";Server SPN=\"\"");
      cnn.Open();
      var sql = "Select name, listprice from production.product where listprice > 20";
      var cmd = new OleDbCommand(sql, cnn);
      var reader = cmd.ExecuteReader();
      while (reader.Read())
      {
        var name = reader.GetString(0);
        var price = reader.GetDecimal(1);
        Console.WriteLine($"{name} price: {price}");
      }
      reader.Close();
      cnn.Close();
      sw.Stop();
      MessageBox.Show(sw.ElapsedMilliseconds.ToString());
    }

    private void button23_Click(object sender, EventArgs e)
    {
      webform f = new webform();
      f.ShowDialog();
    }
    int count;
    Mutex mx = null;
    private void button24_Click(object sender, EventArgs e)
    {
      count = 0;
      mx = new Mutex();
      for (int i = 0; i < 10; i++)
      {
        var t = new Thread(TestThread);
        t.Start();
      }
    }
    void TestThread()
    {
      for (int i = 0; i < 10; i++)
      {
        mx.WaitOne();
        int temp = count;
        Thread.Sleep(2);
        count = temp + 1;
        mx.ReleaseMutex();
      }
    }

    private void button25_Click(object sender, EventArgs e)
    {
      Console.WriteLine($"count = {count}");
    }
    double CalculateReciprocals(long max)
    {
      double total = 0;
      for (int i = 1; i <= max; i++)
      {
        total += 1.0 / i;
      }
      return total;
    }
    Task<double> CalculateReciprocalsAsync(long max) {
      var t = Task.Run(() => CalculateReciprocals(max));
      return t;
    }
    async void DoAsyncStuff()
    {
      Console.WriteLine("starting calculation");
      var d1 = await CalculateReciprocalsAsync(100000000);
      Console.WriteLine($"d1 = {d1}");
      var d2 = await CalculateReciprocalsAsync(200000000);
      Console.WriteLine($"d2 = {d2}");
    }
    private void button26_Click(object sender, EventArgs e)
    {
      DoAsyncStuff();
      Console.WriteLine("all finished");
    }

    private void button27_Click(object sender, EventArgs e)
    {
      var t = new Thread(CalculatePi);
      t.Start();
    }
    private void CalculatePi()
    {
      double pi = 16 * calcarctan(1.0 / 5) - 4 * calcarctan(1.0 / 239);
      // this crashes from a background thread! Use invoke with lambda, cast to Action
      this.Invoke((Action)(() => {
        this.label1.Text = $"Pi={pi}";
      }));
    }

    double calcarctan(double z)
    {
      double tan = 0;
      double pow = z, count = 1, sign = 1;
      do
      {
        tan += sign * pow / count;
        count += 2;
        pow *= z * z;
        sign = -sign;
      } while (pow > 1e-20);
      return tan;
    }

    private void button28_Click(object sender, EventArgs e)
    {
      dynamic b = button1;
      MessageBox.Show(b.Left.ToString());
      b = 5;
      MessageBox.Show(b.ToString());
    }

    private void button29_Click(object sender, EventArgs e)
    {
      const string filename = @"c:\users\cursist\demo2.xlsx";
      if (File.Exists(filename))
        File.Delete(filename);
      var excel = new xl.Application();
      var wb = excel.Workbooks.Add();
      xl.Worksheet ws = wb.Worksheets.Item[1];
      ws.Cells[4, 2] = 1000;
      wb.SaveAs(filename);
      wb.Close();
      excel.Quit();
      System.Runtime.InteropServices.Marshal.ReleaseComObject(excel);
    }

    private void button30_Click(object sender, EventArgs e)
    {
      var tree = new Tree("oak", 42);
      Type t = tree.GetType();
      var members = t.GetMembers();
      foreach (var member in members)
      {
        Console.WriteLine($"Name:{member.Name}, Type={member.MemberType.ToString()}");
      }
    }

    private void button31_Click(object sender, EventArgs e)
    {
      var t = typeof(SpecialFuncs);
      for (int i = 1; i <=6; i++)
      {
        var m = t.GetMethod($"Func{i}");
        if (m != null)
        {
          var b = new Button();
          b.Width = 50;
          b.Height = 30;
          b.Left = i * 60 -40;
          b.Top = 15;
          b.Tag = m.Name;
          b.Text = $"{i}";
          this.groupBox1.Controls.Add(b);
          b.Click += CallFunc;
          var attrs = m.GetCustomAttributes(typeof (SpecialAttribute));
          foreach (SpecialAttribute attr in attrs)
          {
            if (attr.Finished)
              b.BackColor = Color.LightGreen;
          }
        }
      }
      
    }
    private void CallFunc(object sender, EventArgs e)
    {
      var t = typeof(SpecialFuncs);
      var m = t.GetMethod( (sender as Button).Tag.ToString());
      m.Invoke(t,null);

    }
    byte[] secretmessage;
    private void button32_Click(object sender, EventArgs e)
    {
      var message = textBox1.Text;
      var pw = "abcdefghz";
      var salt = "xyzzy";
      var rgb = new Rfc2898DeriveBytes(pw, Encoding.Unicode.GetBytes( salt));
      var aes = new AesManaged();
      var rgbkey = rgb.GetBytes(aes.KeySize / 8);
      var rgbiv = rgb.GetBytes(aes.BlockSize / 8);
      var stream = new MemoryStream();
      var enc = aes.CreateEncryptor(rgbkey, rgbiv);
      var crypt = new CryptoStream(stream, enc, CryptoStreamMode.Write);
      var bytes = Encoding.Unicode.GetBytes(message);
      crypt.Write(bytes, 0, bytes.Length);
      crypt.FlushFinalBlock();
      secretmessage = stream.ToArray();
      crypt.Close();
      stream.Close();
      textBox1.Text = Encoding.ASCII.GetString(secretmessage);
    }

    private void button33_Click(object sender, EventArgs e)
    {
      var pw = "abcdefghz";
      var salt = "xyzzy";
      var rgb = new Rfc2898DeriveBytes(pw, Encoding.Unicode.GetBytes(salt));
      var aes = new AesManaged();
      var rgbkey = rgb.GetBytes(aes.KeySize / 8);
      var rgbiv = rgb.GetBytes(aes.BlockSize / 8);
      var stream = new MemoryStream();
      var enc = aes.CreateDecryptor(rgbkey, rgbiv);
      var crypt = new CryptoStream(stream, enc, CryptoStreamMode.Write);
      var secret = Encoding.Unicode.GetBytes(textBox1.Text);
      crypt.Write(secretmessage, 0, secretmessage.Length);
      crypt.FlushFinalBlock();
      var message = Encoding.Unicode.GetString( stream.ToArray());
      crypt.Close();
      stream.Close();
      textBox1.Text=message;
    }

    private void button34_Click(object sender, EventArgs e)
    {
      Func<int,int> Treble  = (x => x * x * x);
      var a = Treble(7);
      Console.WriteLine(a);

      TestMethod(10, 20, x => x + x);

      int[] arr = { 3, 5, 6, 8, 11, 13, 17, 23, 40 };

      var newarr = Filter(arr, x => x % 2 == 0);
      foreach (int item in newarr)
      {
        Console.WriteLine(item);
      }
    }
    private List<int> Filter(int[] numbers, Func<int, bool> myFilter)
    {
      return numbers.Where(myFilter).ToList();
    }

    private void TestMethod(int start, int end, Func<int, int> myFunc)
    {
      for (int i = start; i <=end ; i++)
      {
        Console.WriteLine(myFunc(i));
      }
    }
  }
  public class Person1
  {
    public string name, address;
  }
  public struct Person2
  {
    public string name, address;
  }
  public class test123
  {
    public static void doiets()
    {
      MessageBox.Show("it works!");
    }
  }
  public interface IFillable
  {
    double Capacity { get; set; }
    void Fill();
    double FillGrade { get; set; }
  }
  class Tanker : IFillable
  {
    public double Capacity { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public double FillGrade { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public void Fill()
    {
      throw new NotImplementedException();
    }
  }
  public abstract class Animal
  {
    public string Name { get; set; }
    public virtual void Eat()
    {
      Console.WriteLine("Animal Eat");
    }
  }
  public class Cat : Animal
  {
    public override void Eat()
    {
      Console.WriteLine("Cat Eat");
    }
    public void Miaow()
    {
      Console.WriteLine("Miaowwwwwwwwww!");
    }
  }
  public class Dog : Animal
  {
    public override void Eat()
    {
      Console.WriteLine("Dog Eat");
    }
    public void Bark()
    {
      Console.WriteLine("Arf! Arf! Arf!");
    }
  }
  public static class MyExtensions {
    public static double Treble(this double d)
    {
      return d * 3;
    }
  } 
  public static class SpecialFuncs
  {
    public static void Func1()
    {
      MessageBox.Show("First!");
    }
    [Special(true)]
    public static void Func2()
    {
      MessageBox.Show("Silver...");
    }
    public static void Func4()
    {
      MessageBox.Show("This is function 4");
    }
    [Special(finished:true)]
    public static void Func6()
    {
      MessageBox.Show("I am #6");
    }
  }
  class SpecialAttribute:Attribute
  {
    public bool Finished { get; set; }
    public SpecialAttribute(bool finished)
    {
      this.Finished = finished;
    }
  }
}
