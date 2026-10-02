using System.Collections.ObjectModel;
using System.Windows;

namespace InventoryManagementApp
{
    public partial class MainWindow : Window
    {
        ObservableCollection<Product> products =
            new ObservableCollection<Product>();

        public MainWindow()
        {
            InitializeComponent();

            ProductDataGrid.ItemsSource = products;
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ProductNameTextBox.Text))
            {
                MessageBox.Show("Please enter product name.");
                return;
            }

            if (!decimal.TryParse(PriceTextBox.Text, out decimal price))
            {
                MessageBox.Show("Please enter a valid price.");
                return;
            }

            if (!int.TryParse(QuantityTextBox.Text, out int quantity))
            {
                MessageBox.Show("Please enter a valid quantity.");
                return;
            }

            Product product = new Product
            {
                Name = ProductNameTextBox.Text,
                Price = price,
                Quantity = quantity
            };

            products.Add(product);

            ClearFields();

            MessageBox.Show("Product added successfully.");
        }

        private void UpdateProduct_Click(object sender, RoutedEventArgs e)
        {
            if (ProductDataGrid.SelectedItem is Product selectedProduct)
            {
                if (!decimal.TryParse(PriceTextBox.Text, out decimal price))
                {
                    MessageBox.Show("Please enter a valid price.");
                    return;
                }

                if (!int.TryParse(QuantityTextBox.Text, out int quantity))
                {
                    MessageBox.Show("Please enter a valid quantity.");
                    return;
                }

                selectedProduct.Name = ProductNameTextBox.Text;
                selectedProduct.Price = price;
                selectedProduct.Quantity = quantity;

                ProductDataGrid.Items.Refresh();

                ClearFields();

                MessageBox.Show("Product updated successfully.");
            }
            else
            {
                MessageBox.Show("Please select a product.");
            }
        }

        private void DeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            if (ProductDataGrid.SelectedItem is Product selectedProduct)
            {
                products.Remove(selectedProduct);

                ClearFields();

                MessageBox.Show("Product deleted successfully.");
            }
            else
            {
                MessageBox.Show("Please select a product.");
            }
        }

        private void ProductDataGrid_SelectionChanged(
            object sender,
            System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (ProductDataGrid.SelectedItem is Product selectedProduct)
            {
                ProductNameTextBox.Text = selectedProduct.Name;
                PriceTextBox.Text = selectedProduct.Price.ToString();
                QuantityTextBox.Text = selectedProduct.Quantity.ToString();
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            ClearFields();
        }

        private void ClearFields()
        {
            ProductNameTextBox.Clear();
            PriceTextBox.Clear();
            QuantityTextBox.Clear();

            ProductDataGrid.UnselectAll();
        }
    }
}