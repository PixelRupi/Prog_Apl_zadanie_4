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

namespace Zadanie_4
{
    public partial class Form1 : Form
    {
        string[] koords_bad;
        double[,] koords;
        string content;
        double max_wartosc_x = 0, max_wartosc_y = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void button_file_open_Click(object sender, EventArgs e)
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;



            //caly ten pier.. od plikow
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;

                    //Read the contents of the file into a stream
                    var fileStream = openFileDialog.OpenFile();


                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        string[] test = null;
                        double koord_x, koord_y;

                        fileContent = reader.ReadToEnd();
                        koords_bad = fileContent.Split('\n');
                        

                        Console.WriteLine(koords_bad.ToString());
                        for (int i = 0; i < (koords_bad.Length - 1); i++)
                        {
                            string temp = koords_bad[i];
                            //Console.WriteLine(temp);
                            //Console.WriteLine("type: temp - " + temp.GetType());
                            test = temp.Split(' ');
                            //Console.WriteLine("x: " + test[0] + " y: " + test[1]);


                            //Console.WriteLine("type: test[0] - " + test[0].GetType());
                            //Console.WriteLine("type: test[1] - " + test[1].GetType());
                            string s_koord_x = test[0];
                            string s_koord_y = test[1];
                            // test[0] oraz test[1] sa teraz stringami, parse na double
                            //zly znak oddzielenia, konwersja kropki na przecinek

                            s_koord_x = s_koord_x.Replace('.', ',');
                            s_koord_y = s_koord_y.Replace('.', ',');

                            chart1.Series["Series1"].Points.AddXY(s_koord_x, s_koord_y);

                            if (double.TryParse(s_koord_x, out koord_x))
                            {
                                Console.WriteLine("Koordynat x: " + koord_x);
                            } else
                            {
                                Console.WriteLine("Koordynat x parse failed");
                            }

                            if (double.TryParse(s_koord_y, out koord_y))
                            {
                                Console.WriteLine("Koordynat y: " + koord_y);
                            }
                            else
                            {
                                Console.WriteLine("Koordnat y parse failed");
                            }


                            if (koord_y >= max_wartosc_y)
                            {
                                max_wartosc_y = koord_y;
                                max_wartosc_x = koord_x;
                            }


                            /**
                            koords[i, 0] = koord_x;
                            koords[i, 1] = koord_y;
                            **/

                        }
                        //Console.WriteLine(test);
                        // koniec loopa

                        Console.WriteLine("Maksymalna wartość to: " + max_wartosc_y + " dla koordynatu x: " + max_wartosc_x);
                        //chart1.Series["max"].Points.AddXY(max_wartosc_x, max_wartosc_y);
                    }
                }

                //MessageBox.Show(fileContent, "File Content at path: " + filePath, MessageBoxButtons.OK);
            }



            //koniec
        }
    }
}
