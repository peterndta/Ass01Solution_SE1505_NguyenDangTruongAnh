using BusinessObject;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyStoreWinApp
{
    public partial class frmMemberDetails : Form
    {
        public frmMemberDetails()
        {
            InitializeComponent();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var member = new MemberObject
                {
                    MemberID = int.Parse(txtMemberID.Text),
                    MemberName = txtMemberName.Text,
                    Email = txtEmail.Text,
                    Password = txtPassword.Text,
                    City = txtCity.Text,
                    Country = txtCountry.Text,

                };
                if (InsertOrUpdate == false)
                {
                    MemberRepository.InsertMember(member);
                    MessageBox.Show("Add Success!");
                }
                else
                {
                    MemberRepository.UpdateMember(member);
                    MessageBox.Show("Update Success!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, InsertOrUpdate == false ? "Add a new member" : "Update a member");
            }
        }//end btnSave_Click

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
        //-----------------------------------------------------------
        public IMemberRepository MemberRepository { get; set; }
        public bool InsertOrUpdate { get; set; } //False : Insert, True : Update
        public MemberObject MemberInfo { get; set; }
        //---------------------------------------------------------
        private void frmMemberDetails_Load(object sender, EventArgs e)
        {
            txtMemberID.Enabled = !InsertOrUpdate;
            if (InsertOrUpdate == true) //Update mode
            {
                //Show member to perform updating
                txtMemberID.Text = MemberInfo.MemberID.ToString();
                txtMemberName.Text = MemberInfo.MemberName;
                txtEmail.Text = MemberInfo.Email.ToString();
                txtPassword.Text = MemberInfo.Password.ToString();
                txtCity.Text = MemberInfo.City.ToString();
                txtCountry.Text = MemberInfo.Country.ToString();

            }
        }//end frmMemberDetails_Load
        //---------------------------------------------------------
    }
}
