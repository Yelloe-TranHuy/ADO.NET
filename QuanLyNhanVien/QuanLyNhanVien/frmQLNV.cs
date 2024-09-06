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

namespace QuanLyNhanVien
{
    public partial class frmQLNV : Form
    {
        public frmQLNV()
        {
            InitializeComponent();
        }
        DataSet ds = new DataSet("dsQLNV");
        SqlDataAdapter daChucVu;
        SqlDataAdapter daNhanVien;
        private void frmQLNV_Load(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = @"Data Source = YELLOE\TRANVANHUY_SQL; Initial Catalog = QLNV; Integrated Security = True; Encrypt = True; TrustServerCertificate = True";
            // Dữ liệu combobox Chức vụ
            string sQueryChucVu = @"select * from chucvu";
            daChucVu = new SqlDataAdapter(sQueryChucVu, conn);
            daChucVu.Fill(ds, "tblChucVu");
            cboChucVu.DataSource = ds.Tables["tblChucVu"];
            cboChucVu.DisplayMember = "tencv";
            cboChucVu.ValueMember = "macv";
            // Dữ liệu datagrid Danh sách nhân viên
            string sQueryNhanVien = @"select n.*, c.tencv from nhanvien n, chucvu c where
n.macv=c.macv";
            daNhanVien = new SqlDataAdapter(sQueryNhanVien, conn);
            daNhanVien.Fill(ds, "tblDSNhanVien");
            dgDSNhanVien.DataSource = ds.Tables["tblDSNhanVien"];
            dgDSNhanVien.Columns["manv"].HeaderText = "Mã số";
            dgDSNhanVien.Columns["manv"].Width = 60;
            // … đặt tiêu đề tiếng Việt, định độ rộng cho các trường còn lại
            dgDSNhanVien.Columns["macv"].Visible = false;
            // Command Thêm nhân viên
            string sThemNV = @"insert into nhanvien values(@MaNV, @HoLot, @TenNV, @Phai,
@NgaySinh, @MaCV)";
            SqlCommand cmThemNV = new SqlCommand(sThemNV, conn);
            cmThemNV.Parameters.Add("@MaNV", SqlDbType.NVarChar, 5, "manv");
            cmThemNV.Parameters.Add("@HoLot", SqlDbType.NVarChar, 50, "holot");
            cmThemNV.Parameters.Add("@TenNV", SqlDbType.NVarChar, 10, "ten");
            cmThemNV.Parameters.Add("@Phai", SqlDbType.NVarChar, 3, "phai");
            cmThemNV.Parameters.Add("@NgaySinh", SqlDbType.SmallDateTime, 10,
            "ngaysinh");
            cmThemNV.Parameters.Add("@MaCV", SqlDbType.NVarChar, 5, "macv");
            daNhanVien.InsertCommand = cmThemNV;
        }

        private void txtMaSo_Enter(object sender, EventArgs e)
        {
            if (txtMaNV.Text == "NV001")
            {
                txtMaNV.Text = "";
                txtMaNV.ForeColor = Color.White;
            }
        }

        private void txtMaSo_Leave(object sender, EventArgs e)
        {
            if (txtMaNV.Text == "")
            {
                txtMaNV.Text = "NV001";
                txtMaNV.ForeColor = Color.DimGray;
            }
        }

        private void txtHoLot_Enter(object sender, EventArgs e)
        {
            if (txtHoLot.Text == "Họ")
            {
                txtHoLot.Text = "";
                txtHoLot.ForeColor = Color.White;
            }
        }

        private void txtHoLot_Leave(object sender, EventArgs e)
        {
            if (txtHoLot.Text == "")
            {
                txtHoLot.Text = "Họ";
                txtHoLot.ForeColor = Color.DimGray;
            }
        }

        private void txtTen_Enter(object sender, EventArgs e)
        {
            if (txtTen.Text == "Tên")
            {
                txtTen.Text = "";
                txtTen.ForeColor = Color.White;
            }
        }

        private void txtTen_Leave(object sender, EventArgs e)
        {
            if (txtTen.Text == "")
            {
                txtTen.Text = "Tên";
                txtTen.ForeColor = Color.DimGray;
            }
        }

        private void cboChucVu_Enter(object sender, EventArgs e)
        {
            if (cboChucVu.Text == "Chọn chức vụ")
            {
                cboChucVu.Text = "";
                cboChucVu.ForeColor = Color.White;
            }
        }

        private void cboChucVu_Leave(object sender, EventArgs e)
        {
            if (cboChucVu.Text == "")
            {
                cboChucVu.Text = "Chọn chức vụ";
                cboChucVu.ForeColor = Color.DimGray;
            }
        }

        private void Exit()
        {
            DialogResult exit;
            exit = MessageBox.Show("Are you sure you want to exit frmQLNV?", "Exit the form!", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (exit == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Exit();
        }

        private void mitThoat_Click(object sender, EventArgs e)
        {
            Exit();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            DataRow row = ds.Tables["tblDSNhanVien"].NewRow();
            row["manv"] = txtMaNV.Text;
            row["holot"] = txtHoLot.Text;
            row["ten"] = txtTen.Text;
            if (radNu.Checked == true)
            {
                row["phai"] = "Nữ";
            }
            else
            {
                row["phai"] = "Nam";
            }
            row["ngaysinh"] = dtpNgaySinh.Text;
            row["macv"] = cboChucVu.SelectedValue;
            ds.Tables["tblDSNhanVien"].Rows.Add(row);
        }

        private void txtMaNV_TextChanged(object sender, EventArgs e)
        {

        }

        private void dtpNgaySinh_ValueChanged(object sender, EventArgs e)
        {

        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgDSNhanVien.SelectedRows.Count > 0)
            {
                DataGridViewRow dr = dgDSNhanVien.SelectedRows[0];
                dr.Cells["manv"].Value = txtMaNV.Text;
                dr.Cells["holot"].Value = txtHoLot.Text;
                dr.Cells["ten"].Value = txtTen.Text;
                dr.Cells["phai"].Value = radNam.Checked ? "Nam" : "Nu";
                dr.Cells["ngaysinh"].Value = dtpNgaySinh.Value;
                dr.Cells["macv"].Value = cboChucVu.SelectedValue;
            }
            else
            {
                MessageBox.Show("Chọn dòng để sửa.");
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dgDSNhanVien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow dr = dgDSNhanVien.SelectedRows[0];
            txtMaNV.Text = dr.Cells["manv"].Value.ToString();
            txtHoLot.Text = dr.Cells["holot"].Value.ToString();
            txtTen.Text = dr.Cells["ten"].Value.ToString();
            if (dr.Cells["phai"].Value.ToString() == "Nam")
            {
                radNam.Checked = true;
            }
            else
            {
                radNu.Checked = true;
            }
            dtpNgaySinh.Text = dr.Cells["ngaysinh"].Value.ToString();
            cboChucVu.SelectedValue = dr.Cells["macv"].Value.ToString();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgDSNhanVien.SelectedRows.Count > 0)
            {
                DataGridViewRow dr = dgDSNhanVien.SelectedRows[0];
                ds.Tables["tblDSNhanVien"].Rows[dr.Index].Delete();
            }
            else
            {
                MessageBox.Show("Chọn dòng để xóa");
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommandBuilder cb = new SqlCommandBuilder(daNhanVien);
                daNhanVien.Update(ds, "tblDSNhanVien");
                MessageBox.Show("Đã lưu.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lưu dữ liệu: " + ex.Message);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            ds.Tables["tblDSNhanVien"].RejectChanges();
            MessageBox.Show("Đã hủy");
        }
    }
}
