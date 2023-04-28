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

        private void button1_Click(object sender, EventArgs e)
        {
            Graphics g = Graphics.FromImage(pictureBox1.Image);
            Pen pen = new Pen(penСolorDialog.Color);
            Brush brush = new SolidBrush(brushСolorDialog.Color);
            g.FillEllipse(brush, 30, 30, 20, 10);
            g.DrawEllipse(pen, 30, 30, 20, 10);
            brush = null;
            pen = null;
            g = null;
            pictureBox1.Invalidate();

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
        static Brush brush = null;

//нажата кнопка мышки - запоминаем координаты
        private void pictureBox1_MouseDown(object  sender, MouseEventArgs  e)
        {
            startX = prevX = e.X;
            startY = prevY = e.Y;
            g = Graphics.FromImage(pictureBox1.Image);
            //создаём обычный карандаш
            pen = new Pen(penСolorDialog.Color);
            //создаём очищающий карандаш
            Brush  brush = new TextureBrush(pictureBox1.Image);
            erasePen = new Pen(brush);
            //режим рисования - замена старого изображения
            g.CompositingMode =CompositingMode.SourceCopy;
        }



        //перемещение мышки - перерисовываем фигуру
        private void pictureBox1_MouseMove(object  sender, MouseEventArgs e)
        {
            if (g == null) return;
            //стираем нарисованное до этого
            g.DrawEllipse(erasePen, startX, startY, prevX - startX, prevY - startY);
            //и рисуем заново
            g.DrawEllipse(pen, startX, startY, e.X - startX, e.Y - startY);
            prevX = e.X; 
            prevY = e.Y;
            pictureBox1.Invalidate(); //вызываем перерисовку 
        }


        //отпускание мышки - освобождаем ресурсы
        private void pictureBox1_MouseUp(object  sender, MouseEventArgs  e)
        {
            pen=null; 
            erasePen.Brush=null;
            erasePen= null;
            g= null;
        }

    }
}
