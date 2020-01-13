using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
  [Serializable]
  public class Tree : IComparer, IComparable
  {
    public delegate void HeightChangedEventHandler(object sender, HeightChangedEventArgs e);
    public event HeightChangedEventHandler HeightChanged;
    public Tree(string name, int height, DateTime dateOfSeed)
    {
      this.Name = name;
      this.Height = height;
      this.DateOfSeed = dateOfSeed;
    }
    public Tree(string name, int height)
      : this(name, height, DateTime.Today)
    {
    }
    public string Name { get; set; }
    private int m_Height;
    public int Height
    {
      get { return m_Height; }
      set
      {
        if (value <= 0)
          throw new IllegalHeightException(value);
        m_Height = value;
        HeightChanged?.Invoke(this, new HeightChangedEventArgs(value));
      }
    }

    public void Grow()
    {
      this.Height++;
    }
    public void Grow(int increment)
    {
      this.Height += increment;
    }
    private DateTime m_DateOfSeed;
    private static bool CheckSeedDate(DateTime seedDate)
    {
      return seedDate.Year >= 1900 && seedDate.Month != 11;
    }
    public override string ToString()
    {
      return $"{this.Name} height: {this.Height} date{this.DateOfSeed.ToShortDateString()}";
    }
    public int Compare(object x, object y)
    {
      Tree a = (Tree)x, b = (Tree)y;
      if (a.Height < b.Height)
        return -1;
      else if (a.Height > b.Height)
        return 1;
      else
        return 0;
    }

    public int CompareTo(object obj)
    {
      Tree b = (Tree)obj;
      if (this.Height < b.Height)
        return -1;
      else if (this.Height > b.Height)
        return 1;
      else
        return 0;
    }

    public DateTime DateOfSeed
    {
      get { return m_DateOfSeed; }
      set
      {
        if (value.Date > DateTime.Today.Date || !CheckSeedDate(value))
          throw new ApplicationException("Illegal Date of seeding");
        m_DateOfSeed = value;
      }
    }
    public int Age
    {
      get
      {
        int y = DateTime.Now.Year - this.DateOfSeed.Year;
        if (this.DateOfSeed.AddYears(y).Date > DateTime.Today)
          y--;
        return y;
      }
    }
  }
  public class HeightChangedEventArgs : EventArgs
  {
    public int NewHeight { get; set; }
    public HeightChangedEventArgs(int newHeight)
    {
      this.NewHeight = newHeight;
    }
  }

  [Serializable]
  public class IllegalHeightException : Exception
  {
    public IllegalHeightException() { }
    public IllegalHeightException(int height) : this($"Illegal Height: {height}") { }
    public IllegalHeightException(string message) : base(message) { }
    public IllegalHeightException(string message, Exception inner) : base(message, inner) { }
    protected IllegalHeightException(
    System.Runtime.Serialization.SerializationInfo info,
    System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
  }
  public class Forest:IEnumerable<Tree>
  {
    private List<Tree> trees;
    public Forest()
    {
      trees = new List<Tree>();
    }
    public void Add(Tree t)
    {
      trees.Add(t);
    }

    public IEnumerator<Tree> GetEnumerator()
    {
      return trees.GetEnumerator();
    }
    public IEnumerable<Tree> TreesByName
    {
      get
      {
        foreach( var tree in trees.OrderBy(t => t.Name))
        {
          yield return tree;
        }
      }
    }
    IEnumerator IEnumerable.GetEnumerator()
    {
      return this.GetEnumerator();
    }
  }
}
