using CaglarDurmus.BackOffice.Business.Abstract;
using CaglarDurmus.BackOffice.Business.DependencyResolvers.Ninject;
using CaglarDurmus.BackOffice.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CaglarDurmus.BackOffice.WebFormsUI
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            _userService = InstanceFactory.GetInstance<IUserService>();
        }

        private IUserService _userService;
        private void btnLogin_Click(object sender, EventArgs e)
        {
            var user = _userService.GetUser(this.txtUserName.Text);
            if (user == null)
            {
                MessageBox.Show("Kullanıcı Bulunamadı");
            }
            else
            {
                if (user != null && user.Password == txtPassword.Text)
                {
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show("Şifre Hatalı");
                }
            }

        }

    }
}
