using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace threadForms
{
    public partial class Form1 : Form
    {
        private static object lockObject = new object();
        public static ArrayList generalList = new ArrayList();
        public static ArrayList list1 = new ArrayList();
        public static ArrayList list2 = new ArrayList();
        public static ArrayList list3 = new ArrayList();
        public static ArrayList list4 = new ArrayList();
        public static ArrayList evenNumberList = new ArrayList();
        public static ArrayList oddNumberList = new ArrayList();
        public static ArrayList primeNumberList = new ArrayList();
        public static int generalListNumberPiece = 100000;

        public Form1()
        {
            InitializeComponent();
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            for (int i = 1; i <= generalListNumberPiece; i++)
            {
                generalList.Add(i);
            }

            for (int i = 1; i <= generalListNumberPiece / 4; i++)
            {
                list1.Add(i);
            }

            for (int i = generalListNumberPiece / 4 + 1; i <= generalListNumberPiece / 2; i++)
            {
                list2.Add(i);
            }

            for (int i = (generalListNumberPiece / 2) + 1; i <= generalListNumberPiece * 3 / 4; i++)
            {
                list3.Add(i);
            }

            for (int i = (generalListNumberPiece *3 / 4) + 1; i <= generalListNumberPiece; i++)
            {
                list4.Add(i);
            }

            Thread thread1 = new Thread(() => ThreadRun(list1));
            Thread thread2 = new Thread(() => ThreadRun(list2));
            Thread thread3 = new Thread(() => ThreadRun(list3));
            Thread thread4 = new Thread(() => ThreadRun(list4));

            thread1.Start();
            thread2.Start();
            thread3.Start();
            thread4.Start();

            thread1.Join();
            thread2.Join();
            thread3.Join();
            thread4.Join();


            richTextBox1.Text = ListToString(evenNumberList);
            richTextBox2.Text = ListToString(oddNumberList);
            richTextBox3.Text = ListToString(primeNumberList);

        }

        private void ThreadRun(ArrayList list)
        {
            foreach (int i in list)
            {
                lock (lockObject)
                {
                    if (EvenNumberCheck(i))
                    {
                        evenNumberList.Add(i);
                    }
                    if (OddNumberCheck(i))
                    {
                        oddNumberList.Add(i);
                    }
                    if (PrimeNumberCheck(i))
                    {
                        primeNumberList.Add(i);
                    }
                }
            }
        }

        private bool EvenNumberCheck(int number)
        {
            bool result = false;
            if(number %2 == 0)
            {
                result = true;
            }
            return result;
        }

        private bool OddNumberCheck(int number)
        {
            bool result = false;
            if (number % 2 == 1)
            {
                result = true;
            }
            return result;
        }

        private bool PrimeNumberCheck(int number)
        {
            if (number <= 1)
            {
                return false;
            }

            for (int i = 2; i <= number / 2; i++)
            {
                if (number % i == 0)
                {
                    return false;
                }
            }
            return true;
        }

        private string ListToString(ArrayList list)
        {
            string result = "";
            foreach (int i in list)
            {
                result += i + "  ";
            }
            return result;
        }
    }
}
