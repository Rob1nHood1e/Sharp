using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Number_of_spanning_trees__ind_1._1_
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.MaximumSize = new System.Drawing.Size(550, 550);
            this.WindowState = FormWindowState.Maximized;
        }
        public int[,] vertexMatrix;
        public int[,] kirchhoffMatrix;
        public int matrixSize;

        public void CreateKirchhoffMatrix() //создание матрицы Кирхгофа
        {
            kirchhoffMatrix = new int[matrixSize, matrixSize];
            for (int i = 0; i < matrixSize; i++)
            {
                int temp = 0;
                for (int j = 0; j < matrixSize; j++)
                {
                    if (vertexMatrix[i, j] == 1)
                    {
                        kirchhoffMatrix[i, j] = -1;
                        temp++;
                    }
                }
                kirchhoffMatrix[i, i] = temp;
            }
        }
        public void TransformMatrix(int[,] kirMatrix, int[,] minor, int row, int column, int size) //строит новую матрицу, удаляя i строку и j столбец исходной
        {
            int difi = 0, difj = 0;
            for (int i = 0; i < size - 1; i++)
            {
                if (i == row)
                {
                    difi = 1;
                }
                difj = 0;
                for (int j = 0; j < size - 1; j++)
                {
                    if (j == column)
                    {
                        difj = 1;
                    }
                    minor[i, j] = kirMatrix[i + difi, j + difj];
                }
            }
        }
        public int FindDeterminant(int[,] kirMatrix, int matrixSize) //поиск определителя матрицы Кирхгофа без i строки и j столбца
        {
            int sum = 0, a = 1;
            int[,] minor = new int[matrixSize, matrixSize];
            if (matrixSize > 0)
            {
                if (matrixSize == 1)
                {
                    return kirMatrix[0, 0];
                }
                if (matrixSize == 2)
                {
                    return kirMatrix[0, 0] * kirMatrix[1, 1] - kirMatrix[0, 1] * kirMatrix[1, 0];
                }
                if (matrixSize >= 2)
                {
                    for (int i = 0; i < matrixSize; i++)
                    {
                        TransformMatrix(kirMatrix, minor, i, 0, matrixSize);
                        sum += a * kirMatrix[i, 0] * FindDeterminant(minor, matrixSize - 1);
                        a = -a;
                    }
                }
            }
            else return 0;
            return sum;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int t = 0;
            if (richTextBox1.Text.Equals(""))
            {
                StreamReader ifstream = new StreamReader("matrix.txt", System.Text.Encoding.Default);
                richTextBox1.Text += ifstream.ReadToEnd();
                ifstream.Close();
            }//если текстбокс пуст, заполняем его данными из файла

            while (!richTextBox1.Text[t++].Equals('\n'))
            {
                matrixSize++;
            }//определение размера матрицы
            vertexMatrix = new int[matrixSize, matrixSize];
            t = 0;

            for (int i = 0; i < matrixSize; i++)
            {
                for (int j = 0; j < matrixSize; j++)
                {
                    vertexMatrix[i, j] = (int)Char.GetNumericValue(richTextBox1.Text[t++]);
                }
                t++;
            }

            CreateKirchhoffMatrix();
            int det = FindDeterminant(kirchhoffMatrix, matrixSize-1);
            StreamWriter ffstream = new StreamWriter("result.txt", false, System.Text.Encoding.Default);
            ffstream.Write(det);//запись результата в файл
            ffstream.Close();
            label3.Text = Convert.ToString(det);
        }
    }
}
