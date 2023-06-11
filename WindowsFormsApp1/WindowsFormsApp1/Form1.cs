using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            pictureBox1.Image = new Bitmap(pictureBox1.Size.Width, pictureBox1.Size.Height);
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (pictureBox1.Image != null)
                {
                    pictureBox1.Image = null;
                }
                pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
                Text = openFileDialog1.FileName;
            }
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image.Save(saveFileDialog1.FileName);
                Text = saveFileDialog1.FileName;
            }
        }

        private void цветToolStripMenuItem_Click(object sender, EventArgs e)
        {
            penСolorDialog.ShowDialog();
        }
        private void цветToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            brushСolorDialog.ShowDialog();
        }

        static int pic;
        private void button1_Click(object sender, EventArgs e)
        {
            Graphics g = Graphics.FromImage(pictureBox1.Image);
            Pen pen = new Pen(penСolorDialog.Color);
            Brush brush = new SolidBrush(brushСolorDialog.Color);
            pictureBox1.Invalidate();
            pic = 1;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Graphics g = Graphics.FromImage(pictureBox1.Image);
            Pen pen = new Pen(penСolorDialog.Color);
            Brush brush = new SolidBrush(brushСolorDialog.Color);
            pictureBox1.Invalidate();
            pic = 2;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Graphics g = Graphics.FromImage(pictureBox1.Image);
            Pen pen = new Pen(penСolorDialog.Color);
            Brush brush = new SolidBrush(brushСolorDialog.Color);
            pictureBox1.Invalidate();
            pic = 3;
        }

        private void создатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = new Bitmap(pictureBox1.Size.Width, pictureBox1.Size.Height);
        }
        static int startX, startY;
        //координаты мышки при предыдущем перемещении
        static int prevX, prevY;
        //ссылки на некоторые объекты,чтобы всё время не пересоздавать
        static Graphics g = null;
        static Pen pen = null;
        static Pen erasePen = null;
        static Brush brush2 = null;

        //нажата кнопка мышки - запоминаем координаты
        private void pictureBox1_MouseDown(object  sender, MouseEventArgs  e)
        {
            startX = prevX = e.X;
            startY = prevY = e.Y;
            g = Graphics.FromImage(pictureBox1.Image);
            //создаём обычный карандаш
            pen = new Pen(penСolorDialog.Color);
            //создаём очищающий карандаш
            Brush  brush1 = new TextureBrush(pictureBox1.Image);
            erasePen = new Pen(brush1);
            brush2 = new SolidBrush(brushСolorDialog.Color); 
            //режим рисования - замена старого изображения
            g.CompositingMode =CompositingMode.SourceCopy;
        }

        //перемещение мышки - перерисовываем фигуру
        private void pictureBox1_MouseMove(object  sender, MouseEventArgs e)
        {
            if (g == null) return;
            //стираем нарисованное до этого
            if (pic == 1)
            {
                g.DrawEllipse(erasePen, startX, startY, prevX - startX, prevY - startY);
                //и рисуем заново
                g.DrawEllipse(pen, startX, startY, e.X - startX, e.Y - startY);
                if (checkBox1.Checked)
                {
                    g.FillEllipse(brush2, startX, startY, e.X - startX, e.Y - startY);
                }
            }
            else if (pic == 2)
            {
                erasePen.Width = 5;
                g.DrawLine(erasePen, startX, startY, prevX, prevY);
                //и рисуем заново
                g.DrawLine(pen, startX, startY, e.X, e.Y);
            }
            else if (pic == 3)
            {
                g.DrawRectangle(erasePen, startX, startY, prevX - startX, prevY - startY);
                //и рисуем заново
                g.DrawRectangle(pen, startX, startY, e.X - startX, e.Y - startY);
                if (checkBox1.Checked)
                {
                    g.FillRectangle(brush2, startX, startY, e.X - startX, e.Y - startY);
                }
            }
            prevX = e.X; 
            prevY = e.Y;
            pictureBox1.Invalidate(); //вызываем перерисовку 
        }


        //отпускание мышки - освобождаем ресурсы
        private void pictureBox1_MouseUp(object  sender, MouseEventArgs  e)
        {
            pen=null; 
            erasePen= null;
            g= null;
            brush2 = null;
        }

    }
}
