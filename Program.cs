<<<<<<< HEAD
namespace plan_fighting_super_start
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.

            ApplicationConfiguration.Initialize();
            Application.Run(new Login());
        }
    }
}
=======
﻿using System;
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
>>>>>>> 88a3da28403503078ef20e92d9801821b2664c55
