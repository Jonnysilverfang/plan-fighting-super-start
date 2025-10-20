using System;
using System.Windows.Forms;

namespace Kien
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Chạy Form1 trước, không chạy Form2
            Application.Run(new Form1());
        }
    }
}
