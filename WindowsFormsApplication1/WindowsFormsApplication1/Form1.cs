using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        Licz licz1;
        Strings strings;
        Graphics g;
        Bitmap DrawArea;
        Pen winner, mypen, redpen, bluepen, greenpen;

        /**
         * centra
         */
        float x1, x2, x3;
        float y1, y2, y3;
        float z1, z2, z3;
        float a1, a2, a3;
        
        /**
         * liczniki są tak jakby współczynnikami zbliżenia centrum
         * do kolejnych danych z danych uczących
         */
        int counter1 = 1;
        int counter2 = 1;
        int counter3 = 1;

        /**
         * współczynniki szerokości f. radialnej - szerokość zwiększa się
         * wraz ze wzrostem liczby wprowadzonych danych uczących 
         * (im więcej przykładów ma sieć, tym bardziej mogą być rozrzucone
         * w stosunku do centrum)
         */
        float b1 = 1;
        float b2 = 1;
        float b3 = 1;

        /**
         * odpowiada za randomowe wartości początkowe centrów
         * przyjąłem wartości z przedziału <0,10>, ponieważ
         * wielkości z danych uczących mieszczą się w tym zakresie
         * (dla <0,100> potrzebowałem około 5x więcej przykładów
         * uczących, by identyfikować bez pomyłki)
         */
        float[] floatValues;

        private void writeToTextBoxes()
        {
            textBox1.Text = x1.ToString();
            textBox2.Text = x2.ToString();
            textBox3.Text = x3.ToString();
            textBox4.Text = y1.ToString();
            textBox5.Text = y2.ToString();
            textBox6.Text = y3.ToString();
            textBox7.Text = z1.ToString();
            textBox8.Text = z2.ToString();
            textBox9.Text = z3.ToString();
            textBox10.Text = a1.ToString();
            textBox11.Text = a2.ToString();
            textBox12.Text = a3.ToString();
        }

        private void disableAll()
        {
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            textBox4.Enabled = false;
            textBox5.Enabled = false;
            textBox6.Enabled = false;
            textBox7.Enabled = false;
            textBox8.Enabled = false;
            textBox9.Enabled = false;
            textBox10.Enabled = false;
            textBox11.Enabled = false;
            textBox12.Enabled = false;
            richTextBox2.Enabled = false;
        }

        public Form1()
        {
            InitializeComponent();
            licz1 = new Licz();
            strings = new Strings();
            DrawArea = new Bitmap(pictureBox1.Size.Width, pictureBox1.Size.Height);

            pictureBox1.Image = DrawArea;
            disableAll();

            progressBar1.Minimum = 0;
            progressBar2.Minimum = 0;
            progressBar3.Minimum = 0;
            progressBar1.Maximum = 100;
            progressBar2.Maximum = 100;
            progressBar3.Maximum = 100;

            winner = new Pen(Color.HotPink);
            mypen = new Pen(Color.Black);
            redpen = new Pen(Color.Red);
            bluepen = new Pen(Color.Blue);
            greenpen = new Pen(Color.Green);

            mypen.Width = 2;
            redpen.Width = 2;
            bluepen.Width = 2;
            greenpen.Width = 2;
            winner.Width = 4;

            g = Graphics.FromImage(DrawArea);
            g.Clear(SystemColors.Control);
            createGrid();
            pictureBox1.Image = DrawArea;
            g.Dispose();
        }

        /**
         * przydziela wartość do odpowiedniej klasy i 
         * na jej podstawie zbliża konkretne centra do
         * wartości ze stringa
         */
        private void przydziel(float[] floatValues, string[] values)
        {
            //identyfikuje klasę i zapisuje jako 1,2 lub 3
            switch (values[4])
            {
                case "Iris-setosa": floatValues[4] = 1; break;
                case "Iris-versicolor": floatValues[4] = 2; break;
                case "Iris-virginica": floatValues[4] = 3; break;
                default:
                    MessageBox.Show("Nieprawidłowa klasa!");
                    return;
            }

            // zmienia centrum i szerokość funkcji dla iris-setosa
            if (floatValues[4] == 1)
                licz1.licz(ref counter1, ref x1, ref y1, ref z1, ref a1, floatValues, ref b1);

            // zmienia centrum i szerokość funkcji dla iris-versicolor
            if (floatValues[4] == 2)
                licz1.licz(ref counter2, ref x2, ref y2, ref z2, ref a2, floatValues, ref b2);

            // zmienia centrum i szerokość funkcji dla iris-virginica
            if (floatValues[4] == 3)
                licz1.licz(ref counter3, ref x3, ref y3, ref z3, ref a3, floatValues, ref b3);

            textBox1.Text = x1.ToString();
            textBox2.Text = x2.ToString();
            textBox3.Text = x3.ToString();
            textBox4.Text = y1.ToString();
            textBox5.Text = y2.ToString();
            textBox6.Text = y3.ToString();
            textBox7.Text = z1.ToString();
            textBox8.Text = z2.ToString();
            textBox9.Text = z3.ToString();
            textBox10.Text = a1.ToString();
            textBox11.Text = a2.ToString();
            textBox12.Text = a3.ToString();
        }

        /** RAND! button */
        private void button1_Click(object sender, EventArgs e)
        {
            licz1.randomize(ref x1, ref x2, ref x3, ref y1, ref y2, ref y3, ref z1, ref z2, ref z3, ref a1, ref a2, ref a3);
            writeToTextBoxes();
            grid(false);
        }

        /** LEARN button */
        private void button2_Click(object sender, EventArgs e)
        {
            // jeśli nie wygenerowano losowych wartości centrów, zostaną wygenerowane teraz
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" ||
                textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == "" ||
                textBox7.Text == "" || textBox8.Text == "" || textBox9.Text == "" || 
                textBox10.Text == "" || textBox11.Text == "" || textBox12.Text == "")
            {
                licz1.randomize(ref x1, ref x2, ref x3, ref y1, ref y2, ref y3, ref z1, ref z2, ref z3, ref a1, ref a2, ref a3);
                writeToTextBoxes();
                MessageBox.Show("Automatycznie wpisane randomowe wartości początkowe");
                return;
            }

            //dzieli napis w oknie na tablicę stringów
            string[] tempArray = richTextBox1.Lines;

            for (int i = 0; i < richTextBox1.Lines.Count(); i++)
            {
                try
                {
                    string temp = tempArray[i];

                    //pomija puste linijki
                    if (temp == "") continue;

                    //dzieki każdego stringa na podstawie przecinków
                    char delimiter = ',';
                    string[] values = temp.Split(delimiter);
                    float[] floatValues = new float[5];
                    
                    try
                    {
                        for (int j = 0; j < 4; j++)
                            floatValues[j] = float.Parse(values[j], 
                                System.Globalization.CultureInfo.InvariantCulture);
                    }

                    catch (IndexOutOfRangeException)
                    {
                        MessageBox.Show("Error!");
                        return;
                    }

                    //przydziela do odpowiedniej klasy
                    przydziel(floatValues, values);
                }

                catch (FormatException)
                {
                    MessageBox.Show("Podaj właściwą wartość uczącą!");
                    return;
                }
            }

            //usuwa napis z okna
            richTextBox1.Text = "";

            grid(false);
        }

        /** RESET button */
        private void button3_Click(object sender, EventArgs e)
        {
            counter1 = 1;
            counter2 = 1;
            counter3 = 1;

            textBox1.Text = textBox2.Text = textBox3.Text = textBox4.Text = textBox5.Text = textBox6.Text = textBox7.Text = textBox8.Text = textBox9.Text = textBox10.Text = textBox11.Text = textBox12.Text = richTextBox1.Text = check.Text = richTextBox2.Text = "";

            progressBar1.Value = 0;
            progressBar2.Value = 0;
            progressBar3.Value = 0;

            label16.Text = "Iris-setosa:";
            label17.Text = "Iris-versicolor:";
            label18.Text = "Iris-virginica:";

            g = Graphics.FromImage(DrawArea);
            g.Clear(SystemColors.Control);
            createGrid();
            pictureBox1.Image = DrawArea;
            g.Dispose();
        }

        /**
         * CHECK MY KNOWLEDGE button
         */
        private void button4_Click(object sender, EventArgs e)
        {
            int win = 0;
            float przyn1 = 0;
            float przyn2 = 0;
            float przyn3 = 0;
            floatValues = new float[5];
            string[] values = check.Text.Split(',');

            progressBar1.Value = 0;
            progressBar2.Value = 0;
            progressBar3.Value = 0;

            //dzieli dane dzięki przecinkom
            try
            {
                for (int j = 0; j < 4; j++)
                    floatValues[j] = float.Parse(values[j],
                        System.Globalization.CultureInfo.InvariantCulture);
            }

            catch (IndexOutOfRangeException)
            {
                MessageBox.Show("Złe dane.");
                return;
            }

            catch (FormatException)
            {
                MessageBox.Show("Wpisz cokolwiek, co mógłbym zidentyfikować.");
                return;
            }

            /**
             * przypisuje klasę jako float 1,2,3 lub 4 - w przypadku,
             * gdy nie chcemy podawać klasy, dajemy "?"
             */
            switch (values[4])
            {
                case "Iris-setosa": floatValues[4] = 1; break;
                case "Iris-versicolor": floatValues[4] = 2; break;
                case "Iris-virginica": floatValues[4] = 3; break;
                case "?": floatValues[4] = 0; break;
                default:
                    MessageBox.Show("Nieprawidłowa klasa!");
                    return;
            }

            //liczy przynależność do klas 1,2,3 (suma)
            przyn1 += licz1.f(b1, x1, floatValues[0]) + licz1.f(b1, y1, floatValues[1]) + licz1.f(b1, z1, floatValues[2]) + licz1.f(b1, a1, floatValues[3]);
            przyn2 += licz1.f(b2, x2, floatValues[0]) + licz1.f(b2, y2, floatValues[1]) + licz1.f(b2, z2, floatValues[2]) + licz1.f(b2, a2, floatValues[3]);
            przyn3 += licz1.f(b3, x3, floatValues[0]) + licz1.f(b3, y3, floatValues[1]) + licz1.f(b3, z3, floatValues[2]) + licz1.f(b3, a3, floatValues[3]);

            //sprawdza, która przynależność jest największa
            if (przyn1 > przyn2)
                if (przyn1 > przyn3) win = 1;
                else win = 3;

            else if (przyn2 > przyn3) win = 2;
                else win = 3;

            progressBar1.Value = (int)Math.Round(100 * przyn1 / 4, 1);
            progressBar2.Value = (int)Math.Round(100 * przyn2 / 4, 1);
            progressBar3.Value = (int)Math.Round(100 * przyn3 / 4, 1);

            label16.Text = "Iris-setosa: " + Math.Round(100 * przyn1 / 4, 1).ToString() + "%";
            label17.Text = "Iris-versicolor: " + Math.Round(100 * przyn2 / 4, 1).ToString() + "%";
            label18.Text = "Iris-virginica: " + Math.Round(100 * przyn3 / 4, 1).ToString() + "%";

            string przynaleznosci = "Iris-setosa: " + Math.Round(100*przyn1/4, 1).ToString() + "%\nIris-versicolor: " + Math.Round(100*przyn2/4, 1).ToString() + "%\nIris-virginica: " + Math.Round(100*przyn3/4, 1).ToString() + "%";

            richTextBox2.Text = "Uważam, że jest to \t" + strings.chooseWinner(win) +"\nPowinno być \t" + values[4];
            grid(true);
        }

        /**
         * przykładowe dane
         */
        private void button5_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = strings.daneUczace();
        }

        /**
         * tworzy wykresy i osie
         */
        private void createGrid()
        {
            g = Graphics.FromImage(DrawArea);
            g.Clear(SystemColors.Control);

            /** osie */
            g.DrawLine(mypen, 0, 80, pictureBox1.Width, 80);
            g.DrawLine(mypen, 0, 160, pictureBox1.Width, 160);
            g.DrawLine(mypen, 0, 240, pictureBox1.Width, 240);
            g.DrawLine(mypen, 0, 320, pictureBox1.Width, 320);

            /** znaczniki na osiach */
            for (int i = 30; i < pictureBox1.Width; i += 30)
            {
                g.DrawLine(mypen, (int)(i / 0.75), 75, (int)(i / 0.75), 85);                
                g.DrawLine(mypen, (int)(i / 0.75), 155, (int)(i / 0.75), 165);
                g.DrawLine(mypen, (int)(i / 0.75), 235, (int)(i / 0.75), 245);
                g.DrawLine(mypen, (int)(i / 0.75), 315, (int)(i / 0.75), 325);

                g.DrawString((i / 30).ToString(), new Font("Arial", 8), new SolidBrush(Color.Black), new PointF((float)(i / 0.75), 80));
                g.DrawString((i / 30).ToString(), new Font("Arial", 8), new SolidBrush(Color.Black), new PointF((float)(i / 0.75), 160));
                g.DrawString((i / 30).ToString(), new Font("Arial", 8), new SolidBrush(Color.Black), new PointF((float)(i / 0.75), 240));
                g.DrawString((i / 30).ToString(), new Font("Arial", 8), new SolidBrush(Color.Black), new PointF((float)(i / 0.75), 320));
            }
        }

        /**
         * rysuje wykresy funkcji przynależności
         */
        private void grid(bool check)
        {
            createGrid();

            Point[] point = new Point[pictureBox1.Width];
            Point[] point2 = new Point[pictureBox1.Width];
            Point[] point3 = new Point[pictureBox1.Width];
            Point[] point4 = new Point[pictureBox1.Width];
            Point[] point5 = new Point[pictureBox1.Width];
            Point[] point6 = new Point[pictureBox1.Width];
            Point[] point7 = new Point[pictureBox1.Width];
            Point[] point8 = new Point[pictureBox1.Width];
            Point[] point9 = new Point[pictureBox1.Width];
            Point[] point10 = new Point[pictureBox1.Width];
            Point[] point11 = new Point[pictureBox1.Width];
            Point[] point12 = new Point[pictureBox1.Width];

            for (int i = 0; i < pictureBox1.Width; i++)
            {
                point[i].X = i * 40;
                point[i].Y = -(int)(70 * licz1.f(b1, x1, i)) + 80;
                point2[i].X = i * 40;
                point2[i].Y = -(int)(70 * licz1.f((float)b2, x2, i)) + 80;
                point3[i].X = i * 40;
                point3[i].Y = -(int)(70 * licz1.f((float)b3, x3, i)) + 80;
                point4[i].X = i * 40;
                point4[i].Y = -(int)(70 * licz1.f(b1, y1, i)) + 160;
                point5[i].X = i * 40;
                point5[i].Y = -(int)(70 * licz1.f((float)b2, y2, i)) + 160;
                point6[i].X = i * 40;
                point6[i].Y = -(int)(70 * licz1.f((float)b3, y3, i)) + 160;
                point7[i].X = i * 40;
                point7[i].Y = -(int)(70 * licz1.f(b1, z1, i)) + 240;
                point8[i].X = i * 40;
                point8[i].Y = -(int)(70 * licz1.f((float)b2, z2, i)) + 240;
                point9[i].X = i * 40;
                point9[i].Y = -(int)(70 * licz1.f((float)b3, z3, i)) + 240;
                point10[i].X = i * 40;
                point10[i].Y = -(int)(70 * licz1.f(b1, a1, i)) + 320;
                point11[i].X = i * 40;
                point11[i].Y = -(int)(70 * licz1.f((float)b2, a2, i)) + 320;
                point12[i].X = i * 40;
                point12[i].Y = -(int)(70 * licz1.f((float)b3, a3, i)) + 320;
            }

            g.DrawCurve(bluepen, point);
            g.DrawCurve(redpen, point2);
            g.DrawCurve(greenpen, point3); 
            g.DrawCurve(bluepen, point4);
            g.DrawCurve(redpen, point5);
            g.DrawCurve(greenpen, point6); 
            g.DrawCurve(bluepen, point7);
            g.DrawCurve(redpen, point8);
            g.DrawCurve(greenpen, point9);
            g.DrawCurve(bluepen, point10);
            g.DrawCurve(redpen, point11);
            g.DrawCurve(greenpen, point12);

            g.DrawLine(mypen, 0, 80, pictureBox1.Width, 80);
            g.DrawLine(mypen, 0, 160, pictureBox1.Width, 160);
            g.DrawLine(mypen, 0, 240, pictureBox1.Width, 240);
            g.DrawLine(mypen, 0, 320, pictureBox1.Width, 320);

            if (check)
            {
                check = false;
                g.DrawLine(winner, 40 * floatValues[0], 80, 40 * floatValues[0], 10);
                g.DrawLine(winner, 40 * floatValues[1], 160, 40 * floatValues[1], 90);
                g.DrawLine(winner, 40 * floatValues[2], 240, 40 * floatValues[2], 170);
                g.DrawLine(winner, 40 * floatValues[3], 320, 40 * floatValues[3], 250);
            }

            pictureBox1.Image = DrawArea;
            g.Dispose();
        }
    }
}
