using BusinessObject;
using DataAccess;
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
    public partial class frmMemberManagement : Form
    {
        IMemberRepository memberRepository = new MemberRepository();
        BindingSource source;
        private IEnumerable<MemberObject> MemberList;
        public frmMemberManagement()
        {
            InitializeComponent();
        }
        //----------------------------------------
        private void frmMemberManagement_Load(object sender, EventArgs e)
        {
            btnDelete.Enabled = false;
            //Register this event to open the frmMemberDetais form that performs updating
            MemberList = memberRepository.GetMembers();
            dgvMemberList.CellDoubleClick += DvgMemberList_CellDoubleClick;
        }

        //--------------------------------
        private void DvgMemberList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            frmMemberDetails frmMemberDetails = new frmMemberDetails
            {
                Text = "Update member",
                InsertOrUpdate = true,
                MemberInfo = GetMemberObject(),
                MemberRepository = memberRepository,
            };
            if (frmMemberDetails.ShowDialog() == DialogResult.OK)
            {
                LoadMemberList();
                //Set focus member updated
                source.Position = source.Count - 1;
            }
        }
        //Clear txt on TextBoxes
        private void ClearText()
        {
            txtMemberID.Text = string.Empty;
            txtMemberName.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtCity.Text = string.Empty;
            txtCountry.Text = string.Empty;
        }
        //-------------------------------------------
        private MemberObject GetMemberObject()
        {
            MemberObject member = null;
            try
            {
                member = new MemberObject
                {
                    MemberID = int.Parse(txtMemberID.Text),
                    MemberName = txtMemberName.Text,
                    Email = txtEmail.Text,
                    Password = txtPassword.Text,
                    City = txtCity.Text,
                    Country = txtCountry.Text,
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Get member");
            }
            return member;
        }//end GetMemberObject
        //----------------------------------------
        public void LoadMemberList()
        {
            var members = memberRepository.GetMembers();
            //var members = MemberList;
            try
            {
                //The BindingSource component is designed to simplify
                //the process of binding controls to and underlying data source
                source = new BindingSource();
                source.DataSource = members;

                txtMemberID.DataBindings.Clear();
                txtMemberName.DataBindings.Clear();
                txtEmail.DataBindings.Clear();
                txtPassword.DataBindings.Clear();
                txtCity.DataBindings.Clear();
                txtCountry.DataBindings.Clear();

                txtMemberID.DataBindings.Add("Text", source, "MemberID");
                txtMemberName.DataBindings.Add("Text", source, "MemberName");
                txtEmail.DataBindings.Add("Text", source, "Email");
                txtPassword.DataBindings.Add("Text", source, "Password");
                txtCity.DataBindings.Add("Text", source, "City");
                txtCountry.DataBindings.Add("Text", source, "Country");

                dgvMemberList.DataSource = null;
                dgvMemberList.DataSource = source;
                if (members.Count() == 0)
                {
                    ClearText();
                    btnDelete.Enabled = false;
                }
                else
                {
                    btnDelete.Enabled = true;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Load member list");
            }
        }//end LoadMemberList

        //--------------------------------------------------------------
        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadMemberList();
        }//end btnLoad_Click
         //-------------------------------------------------------------
        private void btnNew_Click(object sender, EventArgs e)
        {
            frmMemberDetails frmMemberDetails = new frmMemberDetails
            {
                Text = "Add member",
                InsertOrUpdate = false,
                MemberRepository = memberRepository,
            };
            if (frmMemberDetails.ShowDialog() == DialogResult.OK)
            {
                LoadMemberList();
                //Set focus member inserted
                source.Position = source.Count - 1;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                var member = GetMemberObject();
                memberRepository.DeleteMember(member.MemberID);
                LoadMemberList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Delete a member");
            }
        }//end btnDelete_Click

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
            //frmLogin login = new frmLogin();
           //login.ShowDialog();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                var memberID = int.Parse(txtSearch.Text);
                MemberObject member = memberRepository.SearchByID(memberID);
                if (member != null)
                {
                    source = new BindingSource();
                    source.DataSource = member;

                    txtMemberID.DataBindings.Clear();
                    txtMemberName.DataBindings.Clear();
                    txtEmail.DataBindings.Clear();
                    txtPassword.DataBindings.Clear();
                    txtCity.DataBindings.Clear();
                    txtCountry.DataBindings.Clear();

                    txtMemberID.DataBindings.Add("Text", source, "MemberID");
                    txtMemberName.DataBindings.Add("Text", source, "MemberName");
                    txtEmail.DataBindings.Add("Text", source, "Email");
                    txtPassword.DataBindings.Add("Text", source, "Password");
                    txtCity.DataBindings.Add("Text", source, "City");
                    txtCountry.DataBindings.Add("Text", source, "Country");

                    dgvMemberList.DataSource = null;
                    dgvMemberList.DataSource = source;

                    btnDelete.Enabled = true;

                }
                else
                {
                    ClearText();
                    btnDelete.Enabled = false;
                    MessageBox.Show("This member does not exist!");
                }
            }                      
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Search...");
            }
        }

        private void btnSort_Click(object sender, EventArgs e)
        {
            MemberList = memberRepository.SortingMember();
            LoadMemberList();
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            var country = cbFilter.Text;
            MemberList = memberRepository.FilterByCountry(country);
            if(country != "Choose Country")
            {
                try
                {
                    //The BindingSource component is designed to simplify
                    //the process of binding controls to and underlying data source
                    source = new BindingSource();
                    source.DataSource = MemberList;

                    txtMemberID.DataBindings.Clear();
                    txtMemberName.DataBindings.Clear();
                    txtEmail.DataBindings.Clear();
                    txtPassword.DataBindings.Clear();
                    txtCity.DataBindings.Clear();
                    txtCountry.DataBindings.Clear();

                    txtMemberID.DataBindings.Add("Text", source, "MemberID");
                    txtMemberName.DataBindings.Add("Text", source, "MemberName");
                    txtEmail.DataBindings.Add("Text", source, "Email");
                    txtPassword.DataBindings.Add("Text", source, "Password");
                    txtCity.DataBindings.Add("Text", source, "City");
                    txtCountry.DataBindings.Add("Text", source, "Country");

                    dgvMemberList.DataSource = null;
                    dgvMemberList.DataSource = source;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Filter member list");
                }
            }
            else
            {
                MessageBox.Show("Please Select a Country");
            }
        }
    }

}
