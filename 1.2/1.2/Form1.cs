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

namespace Hamiltonian_path__ind_1._2_
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
        public bool[] usedVertex;
        public int matrixSize;
        public List<int> path;

        public bool HamiltonPathExistence(int vertex)
        {
            path.Add(vertex);

            if (path.Count == matrixSize)
            {
                return true;
            }
            usedVertex[vertex] = true;
            for (int i = 0; i < matrixSize; i++)
            {
                if (!usedVertex[i] && vertexMatrix[vertex,i] == 1)
                {
                    if (HamiltonPathExistence(i))
                    {
                        return true;
                    }
                }
            }
            path.RemoveAt(path.Count - 1);
            usedVertex[vertex] = false;
            return false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int t = 0;
            if (richTextBox1.Text.Equals(""))
            {
                StreamReader ifstream = new StreamReader("matrix.txt", System.Text.Encoding.Default);
                richTextBox1.Text += ifstream.ReadToEnd();
                ifstream.Close();
            }

            while (!richTextBox1.Text[t++].Equals('\n'))
            {
                matrixSize++;
            }

            vertexMatrix = new int[matrixSize, matrixSize];
            usedVertex = new bool[matrixSize];
            path = new List<int>(matrixSize);
            t = 0;
        
            for (int i = 0; i < matrixSize; i++)
            {
                for (int j = 0; j < matrixSize; j++)
                {
                    vertexMatrix[i, j] = (int)Char.GetNumericValue(richTextBox1.Text[t++]);
                }
                t++;
            }

            StreamWriter ffstream = new StreamWriter("result.txt", false, System.Text.Encoding.Default);
            bool flag = false;
            for (int i = 0; i < matrixSize && !flag; i++)
            {
                if (HamiltonPathExistence(i))
                {
                    foreach (int item in path)
                    {
                        ffstream.Write(item);
                        label3.Text += Convert.ToString(item);

                        if (!item.Equals(path.Last()))
                        {
                            ffstream.Write("-");
                            label3.Text += "-";
                        }
                    }
                    flag = true;
                }
            }

            if (!flag)
            {
                label3.Text += "Гамильтонова пути не существует.";
                ffstream.Write("Гамильтонова пути не существует.");
            }
            ffstream.Close();
        }
    }
}
