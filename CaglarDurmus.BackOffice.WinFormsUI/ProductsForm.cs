using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CaglarDurmus.BackOffice.Business;
using CaglarDurmus.BackOffice.Business.Abstract;
using CaglarDurmus.BackOffice.Business.DependencyResolvers.Ninject;
using CaglarDurmus.BackOffice.Entities.Concrete;

namespace CaglarDurmus.BackOffice.WebFormsUI
{
    public partial class ProductsForm : Form
    {
        public ProductsForm()
        {
            this.MinimumSize = new Size(683, 499);
            InitializeComponent();
            _productService = InstanceFactory.GetInstance<IProductService>();
            _categoryService = InstanceFactory.GetInstance<ICategoryService>();
            this.dgwProduct.DefaultCellStyle.Font = new Font("", 12);
        }

        private IProductService _productService;
        private ICategoryService _categoryService;
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadProducts();
            LoadCatagories();
        }

        private void LoadCatagories()
        {
            cbxCategory.DataSource = _categoryService.GetAll();
            cbxCategory.DisplayMember = "CategoryName";
            cbxCategory.ValueMember = "CategoryId";
        }

        public void LoadProducts()
        {
            var products = _productService.GetAll();
            dgwProduct.DataSource = products;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Edit edit = new Edit(this);
            edit.ShowDialog();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgwProduct.CurrentRow != null)
                {
                    _productService.Delete(new Product
                    {
                        ProductId = Convert.ToInt32(dgwProduct.CurrentRow.Cells[0].Value)
                    });
                    MessageBox.Show("Ürün Silindi!");
                    LoadProducts();
                }

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            var row = dgwProduct.CurrentRow;
            var product = _productService.GetProduct((int)row.Cells[0].Value);
            Edit edit = new Edit(this, product);
            edit.ShowDialog();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadProducts();
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            try
            {
                dgwProduct.DataSource = _productService.GetByFilter(
                    Convert.ToInt32(cbxCategory.SelectedValue),
                    tbxProductName.Text,
                    !string.IsNullOrWhiteSpace(tbxMinStock.Text) ? Convert.ToInt32(tbxMinStock.Text) : (int?)null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}

