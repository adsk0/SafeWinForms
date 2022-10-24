using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace safe4
{

    public class Matrix
    {
        public static int[,] createArr()
        {
            int[,] arrMatrix = new int[5, 5];
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (i == j)
                    {
                        arrMatrix[i, j] = 0;
                    }
                    else
                    {
                        arrMatrix[i, j] = 1;
                    }
                }
            }
            Form1.shakeMatrix(arrMatrix);
            return arrMatrix;
        }
    }

    internal static class Program
    {

        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

    }
}
