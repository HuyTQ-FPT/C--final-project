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
    public partial class Home : Form
    {
        ProductDAO pDAO = new ProductDAO();
        BillDetailDAO bdDAO = new BillDetailDAO();

        public Home()
        {
            InitializeComponent();
            loadData();
        }


        private void smQLK_Click(object sender, EventArgs e)
        {
            if(Setting.io!=null)
                Setting.io.Close();
            Setting.io = new InOut();
            Setting.io.Show();
            this.Show();
        }

        private void btrefresh_Click(object sender, EventArgs e)
        {
            loadData();
        }
        public void loadData()
        {

            dataGridView1.DataSource = pDAO.GetAll().
                Select(x => new
                {
                    x.ProductId,
                    x.Cate.CateName,
                    x.ProductName,
                    x.Cate.Original.OriginalName,
                    x.QuantityInStock
                }
                ).ToList();

            dataGridView2.DataSource = bdDAO.getAllBillInOut(1)
                .Select(x => new
                {
                    x.BillId,
                    x.Product.Cate.Original.OriginalName,
                    x.Product.Cate.CateName,
                    x.Product.ProductName,
                    x.QuantityUnit,
                    x.Bill.Date,
                    x.Tax,
                    x.Bill.UsernameNavigation.Name,
                }
                ).ToList();
            dataGridView3.DataSource = bdDAO.getAllBillInOut(0)
                .Select(x => new
                {
                    x.BillId,
                    x.Product.Cate.Original.OriginalName,
                    x.Product.Cate.CateName,
                    x.Product.ProductName,
                    x.QuantityUnit,
                    x.Bill.Date,
                    x.Tax,
                    x.Bill.UsernameNavigation.Name,
                }
                ).ToList();
        }
    }
}
