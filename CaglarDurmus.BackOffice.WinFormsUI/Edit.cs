using CaglarDurmus.BackOffice.Business.Abstract;
using CaglarDurmus.BackOffice.Business.DependencyResolvers.Ninject;
using CaglarDurmus.BackOffice.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CaglarDurmus.BackOffice.WebFormsUI
{
    public partial class Edit : Form
    {
        public Edit(ProductsForm productForm)
        {
            this._productForm = productForm;
            this.InitializeComponent();
            this._productService = InstanceFactory.GetInstance<IProductService>();
            this._categoryService = InstanceFactory.GetInstance<ICategoryService>();
        }

        public Edit(ProductsForm productForm, Product product)
        {
            this._productForm = productForm;
            this.InitializeComponent();
            this._productService = InstanceFactory.GetInstance<IProductService>();
            this._categoryService = InstanceFactory.GetInstance<ICategoryService>();
            this._product = product;
            this.tbxProductNameAdd.Text = product.ProductName;
            this.tbxQuantityPerUnit.Text = product.QuantityPerUnit;
            this.tbxStock.Text = product.UnitsInStock.ToString();
            this.tbxUnitPrice.Text = product.UnitPrice.ToString();
        }

        private void LoadCatagories()
        {
            cbxAddCategory.DataSource = _categoryService.GetAll();
            cbxAddCategory.DisplayMember = "CategoryName";
            cbxAddCategory.ValueMember = "CategoryId";
        }

        private void Edit_Load(object sender, EventArgs e)
        {
            this.LoadCatagories();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (this._product == null)
                {
                    this._productService.Add(new Product
                    {
                        CategoryId = Convert.ToInt32(cbxAddCategory.SelectedValue),
                        ProductName = tbxProductNameAdd.Text,
                        QuantityPerUnit = tbxQuantityPerUnit.Text,
                        UnitPrice = Convert.ToDecimal(tbxUnitPrice.Text),
                        UnitsInStock = Convert.ToInt16(tbxStock.Text)

                    });
                    MessageBox.Show("Ürün Eklendi!");
                    this._productForm.LoadProducts();
                    this.Close();

                }
                else
                {
                    this._productService.Update(new Product
                    {
                        ProductId = _product.ProductId,
                        CategoryId = Convert.ToInt32(cbxAddCategory.SelectedValue),
                        ProductName = tbxProductNameAdd.Text,
                        QuantityPerUnit = tbxQuantityPerUnit.Text,
                        UnitPrice = Convert.ToDecimal(tbxUnitPrice.Text),
                        UnitsInStock = Convert.ToInt16(tbxStock.Text)
                    });

                    MessageBox.Show("Ürün Güncellendi!");
                    this._productForm.LoadProducts();
                    this.Close();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private ProductsForm _productForm;
        private IProductService _productService;
        private ICategoryService _categoryService;
        private Product _product;
    }
}
