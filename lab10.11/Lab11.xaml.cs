using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
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

namespace lab10._11
{
    [Serializable]
    struct Product
    {
        public string Name;
        public decimal Price;
        public int Quantity;

        public decimal TotalCost => Price * Quantity;
    }

    public partial class Lab11 : Page
    {
        private List<Product> products;

        public Lab11()
        {
            InitializeComponent();
            products = new List<Product>();
        }

        private void Button_AddProduct_Click(object sender, RoutedEventArgs e)
        {
            Product product = new Product
            {
                Name = ProductName.Text,
                Price = decimal.Parse(ProductPrice.Text),
                Quantity = int.Parse(ProductQuantity.Text)
            };
            products.Add(product);
            UpdateUI();
        }

        public void UpdateUI()
        {
            ProductName.Text = "";
            ProductPrice.Text = "";
            ProductQuantity.Text = "";

            ProductsPanel.Children.Clear();
            foreach (Product product in products)
            {
                ProductsPanel.Children.Add(new TextBlock { Text = $"{product.Name}: {product.Quantity} x {product.Price} = {product.TotalCost}" });
            }

            decimal totalCost = 0;
            decimal totalPrice = 0;
            int totalQuantity = 0;

            foreach (Product product in products)
            {
                totalCost += product.TotalCost;
                totalPrice += product.Price;
                totalQuantity += product.Quantity;
            }

            decimal averagePrice = products.Count > 0 ? totalPrice / products.Count : 0;

            TotalCostLabel.Content = $"Total Cost: {totalCost}";
            AveragePriceLabel.Content = $"Average Price: {averagePrice}";
        }

        private void Button_Save_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Бинарные файлы(*.dat)|*.dat";

            if (sfd.ShowDialog() == true)
            {
                using (BinaryWriter writer = new BinaryWriter(File.Open(sfd.FileName, FileMode.Create)))
                {
                    string json = JsonSerializer.Serialize(products);
                    writer.Write(json);
                }
            }
        }

        private void Button_Load_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Бинарные файлы(*.dat)|*.dat";
            if (ofd.ShowDialog() == true)
            {
                using (BinaryReader reader = new BinaryReader(File.Open(ofd.FileName, FileMode.Open)))
                {
                    string json = reader.ReadString();
                    List<Product> loadedProducts = JsonSerializer.Deserialize<List<Product>>(json)!;
                    products.Clear();
                    products.AddRange(loadedProducts);
                    UpdateUI();
                }
            }
        }
    }
}
