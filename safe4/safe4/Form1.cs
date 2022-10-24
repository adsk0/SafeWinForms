using safe4.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using Image = System.Drawing.Image;


namespace safe4
{
    public partial class Form1 : Form
    {
        public int[,] arrMatrix = Matrix.createArr();   // first matrix creating (constant)
        public int[,] addMatrix = new int[5, 5];         // duplicate matrix creating
        public Button[,] arrButtons = new Button[5, 5];   // creating buttons array
        public int difficaltyLevel = 3;                   // default field is level hard

        public Form1()
        {
            for (int i = 0; i < 5; i++)   // filling duplicate matrix
            {
                for (int j = 0; j < 5; j++)
                {
                    int tmp;
                    if (int.TryParse(arrMatrix[i, j].ToString(), out tmp))
                    {
                        addMatrix[i, j] = tmp;
                    }
                }
            }
            InitializeComponent();
            labelOfWin.Visible = false;
            arrButtons = new Button[,]
            {
                {button1, button2, button3, button4, button5},
                {button6, button7, button8, button9, button10},
                {button11, button12, button13, button14, button15},
                {button16, button17, button18, button19, button20},
                {button21, button22, button23, button24, button25},
            };
            int counter = 0;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    counter++;
                    arrButtons[i, j].Click += ButtonAction_OnClick;
                    arrButtons[i, j].Tag = counter;
                }
            }
            changeField(arrButtons, addMatrix);    // shows field for start
        }

        public static int[,] change_columns(int[,] addMatrix, int col)  // change columns in matrix. called in func "changeMatrix"
        {
            for (int i = col - 1; i < col; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (addMatrix[i, j] == 0)
                    {
                        addMatrix[i, j] = 1;
                    }
                    else
                    {
                        addMatrix[i, j] = 0;
                    }
                }
            }
            return addMatrix;
        }
        public static int[,] change_rows(int[,] addMatrix, int col, int row)     // change rows in matrix. called in func "changeMatrix"
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = row - 1; j < row; j++)
                {
                    if (addMatrix[i, j] == 0)
                    {
                        addMatrix[i, j] = 1;
                    }
                    else
                    {
                        addMatrix[i, j] = 0;
                    }
                }
            }
            if (addMatrix[col - 1, row - 1] == 0)
            {
                addMatrix[col - 1, row - 1] = 1;
            }
            else
            {
                addMatrix[col - 1, row - 1] = 0;
            }
            return addMatrix;
        }

        public static int[,] changeMatrix(int number, int[,] addMatrix) //  gets number of cell and changes matrix
        {
            int count = 0;         // counter for convert number om button to double arrays index (int xy = int[x, y])
            int col = new int();
            int row = new int();
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    count++;
                    if (count == number)
                    {
                        col = i + 1;
                        row = j + 1;
                        change_columns(addMatrix, col);
                        change_rows(addMatrix, col, row);
                    }
                }
            }
            return addMatrix;
        }

        public static void changeField(Button[,] arrButtons, int[,] addMatrix)   // shows all new changes on the gaming field
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    arrButtons[i, j].Image = GetPositionInfo(addMatrix[i, j]);
                    arrButtons[i, j].BackColor = GetColorInfo(addMatrix[i, j]);
                }
            }
        }
        public static Bitmap GetPositionInfo(int number)  // tells to button about what image it has
        {
            if (number == 0)
            {
                return Resources.boneSmallHorizontal;
            }
            else
            {
                return Resources.boneSmallVertical;
            }
        }

        public static Color GetColorInfo(int number)   // tells to button about what color it should be
        {
            if (number == 0)
            {
                return Color.Red;
            }
            else
            {
                return Color.Blue;
            }
        }

        public void ButtonAction_OnClick(object sender, EventArgs e)  // catches  clics on buttons on gaming field
        {
            Button clickedButton = sender as Button;
            int index = Convert.ToInt32((sender as Button).Tag);
            changeMatrix(index, addMatrix);
            changeField(arrButtons, addMatrix);
            winLabel(addMatrix, difficaltyLevel);   // checks win
            labelOfWin.Visible = winLabel(addMatrix, difficaltyLevel);  // shows label 
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            /// ???
        }

        private void buttonMedium_Click(object sender, EventArgs e)
        {
            difficaltyLevel = 2;
            shakeMatrix(addMatrix);
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (i > 3 || j > 3)
                    {
                        arrButtons[i, j].Visible = false;
                    }
                    else
                    {
                        arrButtons[i, j].Visible = true;
                    }
                    changeField(arrButtons, addMatrix);
                }
            }
        }

        private void buttonEasy_Click(object sender, EventArgs e)
        {
            difficaltyLevel = 1;
            shakeMatrix(addMatrix);
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (i > 2 || j > 2)
                    {
                        arrButtons[i, j].Visible = false;
                    }
                    else
                    {
                        arrButtons[i, j].Visible = true;
                    }
                    changeField(arrButtons, addMatrix);
                }
            }
        }

        private void buttonHard_Click(object sender, EventArgs e)
        {
            difficaltyLevel = 3;
            shakeMatrix(addMatrix);
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    {
                        arrButtons[i, j].Visible = true;
                        changeField(arrButtons, addMatrix);
                    }
                }
            }
        }

        public static void shakeMatrix(int[,] addMatrix)  // shakes matrix
        {
            Random rnd = new Random();
            int[] expect = new int[13] { 1, 2, 3, 6, 7, 8, 11, 12, 13, 19, 20, 24, 25 };  // for shakes only 3x3 field (level easy)
            for (int i = 0; i < rnd.Next(77, 777); i++)
            {
                changeMatrix(expect[rnd.Next(1, 13)], addMatrix);
            }
            int countSame = 4;    // for protect against creating autowin field
            while (countSame == 4 || countSame == 5 || countSame == 9 || countSame == 0)
            {
                countSame = 0;
                changeMatrix(expect[rnd.Next(1, 8)], addMatrix);
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (addMatrix[i, j] == 0)
                        {
                            countSame++;
                        }
                    }
                }
            }
        }

        private void buttonShake_Click(object sender, EventArgs e)
        {
            shakeMatrix(addMatrix);
            changeField(arrButtons, addMatrix);
            labelOfWin.Visible = false;
        }


        public static bool winLabel(int[,] addMatrix, int difficaltyLevel)  // opens finish-game label when win
        {
            int winEasy = 0, winMedium = 0, winHard = 0;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (addMatrix[0, 0] == addMatrix[i, j])
                    {
                        if (difficaltyLevel == 1 && i < 3 && j < 3)
                        {
                            winEasy++;
                        }
                        if (difficaltyLevel == 2 && i < 4 && j < 4)
                        {
                            winMedium++;
                        }
                        if (difficaltyLevel == 3)
                        {
                            winHard++;
                        }
                    }
                }
            }
            if (difficaltyLevel == 1 && winEasy == 9)
            {
                return true;
            }
            if (difficaltyLevel == 2 && winMedium == 16)
            {
                return true;            
            }
            if (difficaltyLevel == 3 && winHard == 25)
            {
                return true;          
            }
            return false;
        }
    }
}
