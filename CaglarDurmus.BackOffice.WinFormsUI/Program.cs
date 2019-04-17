using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CaglarDurmus.BackOffice.WebFormsUI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using (var login = new Login())
            {
                if (login.ShowDialog() != DialogResult.OK)
                    return;
                Application.Run(new ProductsForm());
            }
        }
    }
}
