using BusinessObject;
using DataAccess.Repository;
using System;
using System.Windows.Forms;
using Microsoft.Extensions.Configuration;
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

            //Get Admin account from json
            var adminDefaultSettings = Program.Configuration.GetSection("AdminAccount").Get<MemberObject>();
            var email = adminDefaultSettings.Email; //admin email
            var password = adminDefaultSettings.Password; //admin password
            MemberObject loginInfo = memeberRepository.Login(Email, Password);
            
            if (loginInfo != null) //check if member
            {
                frmMemberDetails frmMemberDetails = new frmMemberDetails
                {
                    Text = "My Info",
                    InsertOrUpdate = true,
                    MemberInfo = loginInfo,
                    MemberRepository = memeberRepository,
                };
                frmMemberDetails.ShowDialog();
            }
            else if (Email == email && Password == password) //check if admin
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
