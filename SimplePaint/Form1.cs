using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace SimplePaint
{
    public partial class Form1 : Form
    {
        private string shapeName = "line";
        private bool solidStyle = true;
        private bool drawMood = true;
        private int fontSize = 7;
        private bool isClicked = false;
        private Point location1; // starting location
        private Point location2; // ending location
        Rectangle rec = new Rectangle(); // object of rectangle
        private Shape currentShape = null;
        private List <Shape> shapes = new List<Shape> { };
        Pen pen;
        Graphics g;


        public class Shape
        {
            public Point location1, location2;
            public string shapeName;
            public Pen pen;


        }
        public Form1()
        {

            InitializeComponent();
            this.g = this.CreateGraphics();
            this.pen = new Pen(Color.Black,this.fontSize);

        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.shapeName = "rectangle";

        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.shapeName = "line";
        }

        private void label4_Click(object sender, EventArgs e)
        {
            this.shapeName = "circle";
        }

        private void label7_Click(object sender, EventArgs e)
        {
            if (this.solidStyle)
            {
                this.pen.DashStyle = DashStyle.DashDot;
                this.solidStyle = !this.solidStyle; //dotted
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {
            if (!this.solidStyle)
            {
                this.pen.DashStyle = DashStyle.Solid;
                this.solidStyle = !this.solidStyle; //solid
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            this.fontSize = Convert.ToInt32(Math.Round(numericUpDown1.Value, 0)); //get the size
            this.pen.Width = this.fontSize;
        }

        private void label5_Click(object sender, EventArgs e)
        {
            if (!this.drawMood) this.drawMood = !this.drawMood; //select mood
        }

        private void label6_Click(object sender, EventArgs e)
        {
            if (this.drawMood) this.drawMood = !this.drawMood; //draw mood
        }

        private void label10_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            this.pen.Color = colorDialog1.Color; //choose color for the pen

        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.drawMood)
            {
                this.isClicked = !this.isClicked; //true
                this.location1 = e.Location; // get starting location
                this.currentShape = new Shape();
                this.currentShape.location1 = this.location1;
            }
            switch (this.shapeName)
            {
                case "line":
                    this.currentShape.shapeName = this.shapeName;
                    break;
                case "rectangle":
                    this.currentShape.shapeName = this.shapeName;
                    break;
                case "circle":
                    this.currentShape.shapeName = this.shapeName;
                    break;
            }
            

        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            if (this.isClicked && this.drawMood )
            {
                this.location2 = e.Location; // get the ending location of x and y 
                this.isClicked = !this.isClicked; //make it false and wait for new point entry
                this.currentShape.location2 = this.location2;
            }
            this.currentShape.pen = this.pen;
            this.shapes.Add(this.currentShape);
            Refresh();
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            /*if (this.isClicked && this.drawMood)
            {
                this.location2 = e.Location; //get the current location 
            }
            Refresh(); // refresh the form
*/

        }

        private Rectangle GetRect(Point location1, Point location2)
        {
            this.rec = new Rectangle();
            this.rec.X = Math.Min(location1.X, location2.X); // x-value
            this.rec.Y = Math.Min(location1.Y, location2.Y); // y-value
            this.rec.Width = Math.Abs(location1.X - location2.X);
            this.rec.Height = Math.Abs(location1.Y - location2.Y);
            return this.rec;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            foreach(var shape in shapes)
            {
                switch (shape.shapeName)
                {
                    case "line":
                        //
                        break;
                    case "rectangle":
                        e.Graphics.DrawRectangle(shape.pen, GetRect(shape.location1,shape.location2));
                        break;
                    case "circle":
                        e.Graphics.DrawEllipse(shape.pen, GetRect(shape.location1, shape.location2));
                        break;
                }
            }
          
               
        }

        private void label10_Click_1(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            this.pen.Color = colorDialog1.Color; //choose color for the pen
        }
    }
}


/*
 
{
  public class Line
  {
    public Point start;
    public Point end;
  }
  public partial class Form1 : Form
  {
    private int positoinOffset = 0;
    private int x1 =0, y1=0;
    private int x2 =0, y2=0;
    private bool isClicked = false;
    private List<Line> lines;
    private Line currentLine = null;
    public Form1()
    {
      InitializeComponent();
      this.lines = new List<Line>();
    }
    private void Form1_MouseClick(object sender, MouseEventArgs e)
    {
      if (!this.isClicked)
      {
        this.currentLine = new Line();
        currentLine.start = new Point(e.X, e.Y);
      }
      else
      {
        currentLine.end = new Point(e.X, e.Y);
        this.lines.Add(currentLine);
        this.Invalidate();
      }
      this.isClicked = !isClicked;
    }
    private void Form1_Paint(object sender, PaintEventArgs e)
    {
      Graphics g = e.Graphics;
      Pen p = new Pen(Brushes.Black, 4);
      p.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDot;
      foreach (var line in this.lines)
        g.DrawLine(p, 
        line.start.X,
        line.start.Y,
        line.end.X,
        line.end.Y);
    }
    private void button1_Click(object sender, EventArgs e)
    {
      this.positoinOffset += 32;
      this.Invalidate();
    }
  }
} 
*/