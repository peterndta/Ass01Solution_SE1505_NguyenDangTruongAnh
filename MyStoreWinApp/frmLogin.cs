using BusinessObject;
using DataAccess.Repository;
using System;
using System.Windows.Forms;

namespace MyStoreWinApp
{
    public partial class frmLogin : Form
    {
        IMemberRepository memeberRepository = new MemberRepository();

        public frmLogin()
        {
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            frmMemberManagement frmMemberManagement = new frmMemberManagement();
            var Email = txtEmail.Text;
            var Password = txtPassword.Text;

            MemberObject loginInfo = memeberRepository.Login(Email, Password);
            if (loginInfo != null)
            {
                frmMemberManagement.ShowDialog();
            }
            else
            {
                MessageBox.Show("Your account does not exist!");
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
