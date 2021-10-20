using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace QLSV
{
    public partial class Form1 : Form
    {
        string flag; // cờ để phân biệt thêm hay sửa
       
        public Form1()
        {
            InitializeComponent();
        }
        #region khoa nut
        //tạo khóa để khóa các nút khi ko dùng 
        public void KhoaControl()
        {
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnThoat.Enabled = true;
            btnIn.Enabled = true;
            btnLuu.Enabled = false;
            btnKLuu.Enabled = false;

            txtMaSV.ReadOnly = true;
            txtHoTen.ReadOnly = true;
            txtHocBong.ReadOnly = true;
            btnThem.Focus();

        }
        #endregion
        #region mo nut
        //Mo khoa khi cần dùng
        public void MoControl()
        {
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnThoat.Enabled = false;
            btnIn.Enabled = false;
            btnLuu.Enabled = true;
            btnKLuu.Enabled = true;

            txtMaSV.ReadOnly = false;
            txtHoTen.ReadOnly = false;
            txtHocBong.ReadOnly = false;
            txtMaSV.Focus();
        }
        #endregion
        string chuoiketnoi = @"Data Source=DESKTOP-1MA3JT2;Initial Catalog=QLSV;Integrated Security=True";
        string sql;
        SqlConnection ketnoi;
        SqlCommand thuchien;
        SqlDataReader docdulieu;
        int i = 0;
        private void Form1_Load(object sender, EventArgs e)
        {
            
            KhoaControl();
            listView1.View = View.Details;
            listView1.GridLines = true; // hiển thị dạng mạng lưới
            listView1.FullRowSelect = true; // chọn nguyên 1 dòng

            rbtnNam.Checked = true; // mặc định đánh nam
            ketnoi = new SqlConnection(chuoiketnoi);
            hienthi();

        }
        //tao hien thi cho listview
        public void hienthi()
        {
            listView1.Items.Clear();
            ketnoi.Open();
            sql = @"  select Sinhvien.MaSV, Sinhvien.HotenSV,Sinhvien.Ngaysinh,Sinhvien.Gioitinh,Sinhvien.Hocbong,Lop.Tenlop from Sinhvien ,Lop where Sinhvien.Malop = Lop.Malop";
            thuchien = new SqlCommand(sql, ketnoi);
            docdulieu = thuchien.ExecuteReader();
            i = 0;
            while(docdulieu.Read())
            {
                listView1.Items.Add(docdulieu[0].ToString());
                listView1.Items[i].SubItems.Add(docdulieu[1].ToString());
                listView1.Items[i].SubItems.Add(docdulieu[2].ToString());
                listView1.Items[i].SubItems.Add(docdulieu[3].ToString());
                listView1.Items[i].SubItems.Add(docdulieu[4].ToString());
                listView1.Items[i].SubItems.Add(docdulieu[5].ToString());
                i++;
            }
            ketnoi.Close();
        }
        #region Thoát
        private void button7_Click(object sender, EventArgs e)
        {
            DialogResult h = MessageBox.Show("Bạn có chắc muốn thoát không?", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (h == DialogResult.OK)
                Application.Exit();
        }
        #endregion

        #region Thêm
        private void btnThem_Click(object sender, EventArgs e)
        {
            MoControl();
            flag = "Them";

        }
        #endregion

        private void btnSua_Click(object sender, EventArgs e)
        {
            MoControl();
            flag = "Sua";
            if (listView1.SelectedItems.Count > 0)
            {
                MessageBox.Show("Phải chọn dòng để thực hiện sửa");
            }


        }
        //combobox Lớp
        private void cbLopHoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            ketnoi.Open();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(@"select Tenlop from Lop", ketnoi);
                da.Fill(dt);
                ketnoi.Close();
            }
            catch(Exception ex)
            {
                throw new Exception();
            }
            {
                cbLopHoc.DisplayMember = "Tiếng anh 5";
                cbLopHoc.ValueMember = "Malop";
                cbLopHoc.DataSource = dt;
            }
        }
        //đảo dữ liệu lên trên
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                txtMaSV.Text = listView1.SelectedItems[0].SubItems[0].Text;
                txtHoTen.Text = listView1.SelectedItems[0].SubItems[1].Text;
                dtpNgaySinh.Text = listView1.SelectedItems[0].SubItems[2].Text;             
                if (listView1.SelectedItems[0].SubItems[3].Text == "Nam")
                {
                        rbtnNam.Checked = true;
                }else 
                {
                    rbtnNu.Checked = true;
                }
                txtHocBong.Text = listView1.SelectedItems[0].SubItems[4].Text;
                cbLopHoc.Text = listView1.SelectedItems[0].SubItems[5].Text;

            }

        }
        public bool ktTrung(string MaSV)
        {
            foreach (ListViewItem item in listView1.Items)
            {
                if (item.SubItems[0].Text == MaSV)
                {
                    return true;
                }
            }
            return false;
        }
        #region nut luu
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (flag == "Them")
            {
                if (checkData())
                {
                    DialogResult h = MessageBox.Show("Bạn có chắc muốn lưu không?", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    if (h == DialogResult.OK)
                    {
                        if (ktTrung(txtMaSV.Text) == false)
                        {
                            ListViewItem item = listView1.Items.Add(txtMaSV.Text);
                            item.SubItems.Add(txtHoTen.Text);
                            item.SubItems.Add(dtpNgaySinh.Value.ToShortDateString());
                            string Gioitinh = "Nam";

                            if (rbtnNu.Checked)

                                Gioitinh = "Nữ";
                            item.SubItems.Add(Gioitinh);
                            item.SubItems.Add(txtHocBong.Text);
                            item.SubItems.Add(cbLopHoc.SelectedItem.ToString());
                        }
                        else
                        {
                            MessageBox.Show("Không được nhập trùng mã số sinh viên nhé !!");
                        } 
                            

                    }

                }
            }
            else if (flag == "Sua")
            {
                if (checkData())
                {
                    listView1.SelectedItems[0].SubItems[0].Text = txtMaSV.Text;
                    listView1.SelectedItems[0].SubItems[1].Text= txtHoTen.Text;
                    listView1.SelectedItems[0].SubItems[2].Text =dtpNgaySinh.Text;
                    if (rbtnNam.Checked == true)
                    {
                        listView1.SelectedItems[0].SubItems[3].Text = "Nam";
                    }
                    else
                    {
                        listView1.SelectedItems[0].SubItems[3].Text = "Nữ";
                    }

                    listView1.SelectedItems[0].SubItems[4].Text = txtHocBong.Text;
                    listView1.SelectedItems[0].SubItems[5].Text = cbLopHoc.Text;
                    listView1.Refresh();
                    KhoaControl(); //Sư xong quay lại khóa ô
                }
            }
            // Tẩy trắng thông tin trong các ô 
            txtMaSV.Text = "";
            txtHoTen.Text = "";
            dtpNgaySinh.Value = DateTime.Now;
            rbtnNam.Checked = true;
            txtHocBong.Text = "";
            cbLopHoc.Text = "";
            KhoaControl(); //thêm xong quay lại khóa ô
        }
        #endregion

        #region check xem nhap du chua
        //check data xem người dùng nhập đủ chưa
        public bool checkData()
        {
            if(string.IsNullOrWhiteSpace(txtMaSV.Text))
            {
                MessageBox.Show("Bạn chưa nhập mã sinh viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaSV.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Bạn chưa nhập tên sinh viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtHoTen.Focus();
                return false;
            }
 
            if (string.IsNullOrWhiteSpace(cbLopHoc.Text))
            {
                MessageBox.Show("Bạn chưa chọn lớp ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbLopHoc.Focus();
                return false;
            }
            return true;
        }
        #endregion
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void txtMaSV_TextChanged(object sender, EventArgs e)
        {

        }
        #region nút xóa
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                if (MessageBox.Show("Bạn có muốn xoá!", "Thông báo", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    listView1.Items.RemoveAt(listView1.SelectedIndices[0]);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn mục cần xoá!");
            }
        }
        #endregion
        private void dtpNgaySinh_ValueChanged(object sender, EventArgs e)
        {

        }
        #region huy luu
        private void btnKLuu_Click(object sender, EventArgs e)
        {
            DialogResult h = MessageBox.Show("Bạn có chắc muốn hủy không?", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (h == DialogResult.OK)
                {
                    // Tẩy trắng thông tin trong các ô 
                    txtMaSV.Text = "";
                    txtHoTen.Text = "";
                    dtpNgaySinh.Value = DateTime.Now;
                    rbtnNam.Checked = true;
                    txtHocBong.Text = "";
                    cbLopHoc.Text = "";
                    KhoaControl();
                }
        }
        #endregion

        private void btnIn_Click(object sender, EventArgs e)
        {
            Form2 frm = new Form2();
            frm.Show();
        }

        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {

        }

        private void rbtnNam_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
