using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;

namespace quanlysinhvien
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnKetnoi_Click(object sender, RoutedEventArgs e)
        {
            try

            {

                using (SqlConnection connection =

                    new SqlConnection(@"Server=DESKTOP-9I76QM9;Database=QuanLySinhVien; Integrated Security=SSPI"))

                {

                    connection.Open();

                }

                MessageBox.Show("Mo va dong co so du lieu thanh cong.");

            }

            catch (Exception ex)

            {

                MessageBox.Show("Loi khi mo  ket noi: " + ex.Message);

            }
        }

        private void btnDulieu_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<SinhVien> DanhSachSinhVien = new List<SinhVien>();
                using (SqlConnection connection =
                    new SqlConnection(@"Server=DESKTOP-9I76QM9;Database=QuanLySinhVien; Integrated Security=SSPI"))
                using (SqlCommand command =
                    new SqlCommand("SELECT MaSV, TenSV, Email, MaKH FROM SinhVien;", connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var sv = new SinhVien();
                            sv.MaSV = reader.GetString(0);
                            sv.TenSV = reader.GetString(1);
                            sv.Email = reader.GetString(2);
                            sv.MaKH = reader.GetString(3);
                            DanhSachSinhVien.Add(sv);
                        }
                    }
                }
                MessageBox.Show("Mo va dong co so du lieu thanh cong.");
                dulieu.ItemsSource = DanhSachSinhVien;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi khi mo  ket noi: " + ex.Message);
            }
        }

        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            SinhVien sv = new SinhVien();
            sv.MaSV = "SV07";
            sv.TenSV = "Khiem";
            sv.Email = "khiem@gmail.com";
            sv.MaKH = "KH02";
            if (Them_sinh_vien(sv) > 0)
                MessageBox.Show("Du lieu duoc them thanh cong!");
        }

        private int Them_sinh_vien(SinhVien sinhvien)
        {
            try
            {
                using (SqlConnection connection =
                    new SqlConnection(@"Server=DESKTOP-9I76QM9;Database=QuanLySinhVien;
        Integrated Security=SSPI"))
                using (SqlCommand command = new SqlCommand("INSERT INTO SinhVien(MaSV,TenSV,Email,MaKH)" + "VALUES(@MaSV,@TenSV,@Email,@MaKH);", connection))
                {
                    command.Parameters.Add("MaSV", SqlDbType.NChar, 10).Value =
                        sinhvien.MaSV;
                    object dbTenSV = sinhvien.TenSV;
                    if (dbTenSV == null)
                    {
                        dbTenSV = DBNull.Value;
                    }
                    command.Parameters.Add("TenSV", SqlDbType.VarChar, 20).Value = dbTenSV;
                    object dbEmail = sinhvien.Email;
                    if (dbEmail == null)
                    {
                        dbEmail = DBNull.Value;
                    }
                    command.Parameters.Add("Email", SqlDbType.NVarChar, 50).Value =
                        dbEmail;
                    command.Parameters.Add("MaKH", SqlDbType.NChar, 10).Value =
                        sinhvien.MaKH;
                    connection.Open();
                    return command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi khi mo  ket noi:" + ex.Message);
                return -1;
            }
        }

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            SinhVien sv = new SinhVien();
            sv.MaSV = txtMaSV.Text;
            if (Xoa_sinh_vien(sv) > 0)
                MessageBox.Show("Du lieu duoc xoa thanh cong!");
        }


        private int Xoa_sinh_vien(SinhVien sinhvien)
        {

            try
            {
                using (SqlConnection connection =
                    new SqlConnection(@"Server=DESKTOP-9I76QM9;Database=QuanLySinhVien;Integrated Security=SSPI"))
                using (SqlCommand command = new SqlCommand("DELETE FROM SinhVien " + "WHERE MaSV = @MaSV", connection))
                {
                    command.Parameters.Add("MaSV", SqlDbType.NChar, 10).Value = sinhvien.MaSV;
                    connection.Open();
                    return command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi khi mo  ket noi:" + ex.Message);
                return -1;
            }







        }

        private void btnCapnhat_Click(object sender, RoutedEventArgs e)
        {
            SinhVien sv = new SinhVien();
            sv.MaSV = txtMaSV.Text;
            sv.TenSV = txtTenSV.Text;
            sv.Email = txtEmail.Text;
            sv.MaKH = txtMaKH.Text;
            if (Cap_nhat_sinh_vien(sv) > 0)
                MessageBox.Show("Du lieu duoc cap nhat thanh cong!");

        }




        private int Cap_nhat_sinh_vien(SinhVien sinhvien)
        {
            try
            {
                using (SqlConnection connection =
                new SqlConnection(@"Server=DESKTOP-9I76QM9;Database=QuanLySinhVien; Integrated Security=SSPI"))
                using (SqlCommand command = new SqlCommand("UPDATE SinhVien " + "SET TenSV = @TenSV, Email = @Email, MaKH = @MaKH " + "WHERE MaSV = @MaSV", connection))
                {
                    command.Parameters.Add("MaSV", SqlDbType.NChar, 10).Value = sinhvien.MaSV; command.Parameters.Add("TenSV", SqlDbType.VarChar, 20).Value = sinhvien.TenSV; command.Parameters.Add("Email", SqlDbType.NVarChar, 50).Value = sinhvien.Email; command.Parameters.Add("MaKH", SqlDbType.NChar, 10).Value = sinhvien.MaKH; connection.Open();
                    return command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi khi mo  ket noi:" + ex.Message);
                return -1;
            }
        }

    }
}



