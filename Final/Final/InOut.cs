using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Final.Controller;

namespace Final
{
    public partial class InOut : Form
    {
        AccountDAO aDAO = new AccountDAO();
        OriginalDAO oDAO = new OriginalDAO();
        CategoryDAO cDAO = new CategoryDAO();
        ProductDAO pDAO = new ProductDAO();
        BillDAO bDAO = new BillDAO();
        BillDetailDAO bdDAO = new BillDetailDAO();
        public Boolean isImport = false;
        public Boolean isExport = false;
        string[] searchBox = { "Xuất xứ", "Loại sản phẩm", "Tên sản phẩm", "Ngày Nhập Xuất" };
        int lastBillId;
        public InOut()
        {
            InitializeComponent();
            List<string> listSearchBox = searchBox.ToList();
            if (Setting.acc.Role == 1)
                listSearchBox.Add("Nhân Viên");
            comboBox3.DataSource = listSearchBox;
            loadData();
        }

        public void loadData()
        {
            cbOrigin.DataSource = oDAO.getAllOriginalName();
            cbncc.DataSource = oDAO.getAllOriginalName();
            cbNvien.DataSource = aDAO.getAllUserName();
        }


        private void smHome_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btth_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn có muốn nhập sản phẩm ko?", "Import Albert", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                if (isImport == false)
                {
                    string username = Setting.acc.Username;
                    bDAO.createNewBill(username, 1);
                    lastBillId = bDAO.getLastBillId();
                    isImport = true;
                    isExport = false;
                }
                int originId = oDAO.getIdByOriginalName(cbOrigin.Text);
                int cateId = cDAO.getIdByCateNameAndOriginalId(cbCate.Text, originId);
                string pName = cbProName.Text;
                int quan = Convert.ToInt32(nrInQua.Text);
                int tax = Convert.ToInt32(nrTaxIn.Text);
                pDAO.updateProductInfo(cateId, pName, quan);
                int proId = pDAO.getIdByNameAndCateId(cateId, pName);
                bdDAO.createNewBillDetail(proId, lastBillId, quan, tax);
                MessageBox.Show("Nhập hàng thành công", "Import Albert");
                Setting.home.loadData();
                loadData();
            }

            
        }

        private void cbOrigin_SelectedIndexChanged(object sender, EventArgs e)
        {
            string origin = cbOrigin.Text;
            cbCate.DataSource = cDAO.getAllCatenameByOriginalID(oDAO.getIdByOriginalName(origin));
        }

        private void cbCate_SelectedIndexChanged(object sender, EventArgs e)
        {
            string origin = cbOrigin.Text;
            string cate = cbCate.Text;
            cbProName.DataSource = pDAO.getAllProductNameByCateID(cDAO.getIdByCateNameAndOriginalId(cate, (oDAO.getIdByOriginalName(origin))));
        }

        private void cbncc_SelectedIndexChanged(object sender, EventArgs e)
        {
            string origin = cbncc.Text;
            cbCateOut.DataSource = cDAO.getAllCatenameByOriginalID(oDAO.getIdByOriginalName(origin));
        }

        private void cbCateOut_SelectedIndexChanged(object sender, EventArgs e)
        {
            string origin = cbncc.Text;
            string cate = cbCateOut.Text;
            int originId = oDAO.getIdByOriginalName(origin);
            int cateId = cDAO.getIdByCateNameAndOriginalId(cate, originId);
            cbsp.DataSource = pDAO.getAllProductNameByCateID(cDAO.getIdByCateNameAndOriginalId(cate, (oDAO.getIdByOriginalName(origin))));
            nrQuaOut.Maximum = pDAO.getQuantityByNameAndCateId(cateId, cbsp.Text);
            nrQuaOut.Value = pDAO.getQuantityByNameAndCateId(cateId, cbsp.Text);
        }

        private void btxh_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn có muốn xuất sản phẩm ko?", "Export ALbert", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                if (nrQuaOut.Value == 0)
                {
                    MessageBox.Show("Khong co hang hoa");
                }
                else
                {
                    if (isExport == false)
                    {                       
                        string username = Setting.acc.Username;
                        bDAO.createNewBill(username, 0);
                        lastBillId = bDAO.getLastBillId();
                        isExport = true;
                        isImport = false;
                    }
                    int originId = oDAO.getIdByOriginalName(cbncc.Text);
                    int cateId = cDAO.getIdByCateNameAndOriginalId(cbCateOut.Text, originId);
                    string pName = cbsp.Text;
                    int quan = Convert.ToInt32(nrQuaOut.Text);
                    int tax = Convert.ToInt32(nrTaxOut.Text);
                    pDAO.updateProductInfo(cateId, pName, -quan);
                    int proId = pDAO.getIdByNameAndCateId(cateId, pName);
                    bdDAO.createNewBillDetail(proId, lastBillId, quan, tax);
                    MessageBox.Show("Xuất hàng thành công", "Export ALbert");
                    Setting.home.loadData();
                    loadData();
                }
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbSearch.Visible = false;
            dtSearch.Visible = false;
            cbNvien.Visible = false;
            import.Visible = false;
            export.Visible = false;
            panel2.Visible = false;
            if (comboBox3.SelectedIndex == 3)
            {
                dtSearch.Visible = true;
                panel2.Visible = true;
                import.Visible = true;
                export.Visible = true;
            }
            else if (comboBox3.SelectedIndex == 4)
            {
                panel2.Visible = true;
                cbNvien.Visible = true;
                import.Visible = true;
                export.Visible = true;
            }
            else tbSearch.Visible = true;

        }

        private void btSearch_Click(object sender, EventArgs e)
        {
            switch (comboBox3.SelectedIndex)
            {
                case 0:
                    dataGridView1.DataSource = pDAO.getAllProductbyOrignalName(tbSearch.Text)
                        .Select(x => new
                        {
                            x.Cate.Original.OriginalName,
                            x.Cate.CateName,
                            x.ProductName,
                            x.QuantityInStock,
                        })
                        .ToList();
                    ;
                    break;
                case 1:
                    dataGridView1.DataSource = pDAO.getAllProductbyCateName(tbSearch.Text)
                        .Select(x => new
                        {
                            x.Cate.Original.OriginalName,
                            x.Cate.CateName,
                            x.ProductName,
                            x.QuantityInStock,
                        })
                        .ToList();
                    ;
                    break;
                case 2:
                    dataGridView1.DataSource = pDAO.getAllProductbyProductName(tbSearch.Text)
                        .Select(x => new
                        {
                            x.Cate.Original.OriginalName,
                            x.Cate.CateName,
                            x.ProductName,
                            x.QuantityInStock,
                        })
                        .ToList();
                    ;
                    break;
                case 3:
                    if (import.Checked)
                        dataGridView1.DataSource = bdDAO.getAllBillInOut(1, dtSearch.Value).Select(x => new
                        {
                            x.BillId,
                            x.Product.Cate.Original.OriginalName,
                            x.Product.Cate.CateName,
                            x.Product.ProductName,
                            x.QuantityUnit,
                            x.Bill.Date,
                            x.Tax
                        })
                        .ToList();
                    else
                        dataGridView1.DataSource = bdDAO.getAllBillInOut(0, dtSearch.Value).Select(x => new
                        {
                            x.BillId,
                            x.Product.Cate.Original.OriginalName,
                            x.Product.Cate.CateName,
                            x.Product.ProductName,
                            x.QuantityUnit,
                            x.Bill.Date,
                            x.Tax
                        })
                        .ToList();
                    break;
                case 4:
                    if (import.Checked)
                        dataGridView1.DataSource = bdDAO.getAllBillInOut(cbNvien.Text, 1).Select(x => new
                        {
                            x.BillId,
                            x.Product.Cate.Original.OriginalName,
                            x.Product.Cate.CateName,
                            x.Product.ProductName,
                            x.QuantityUnit,
                            x.Bill.Date,
                            x.Tax
                        })
                            .ToList();
                    else
                        dataGridView1.DataSource = bdDAO.getAllBillInOut(cbNvien.Text, 0).Select(x => new
                        {
                            x.BillId,
                            x.Product.Cate.Original.OriginalName,
                            x.Product.Cate.CateName,
                            x.Product.ProductName,
                            x.QuantityUnit,
                            x.Bill.Date,
                            x.Tax
                        }
                ).ToList();
                    break;
            }
        }
    }
}
