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
using WpfApp_Product.Static_Classes;

namespace WpfApp_Product
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool _isCurrentProductChanges = false;
        private Image _image = new Image();
        private TextBlock _textBlock = new TextBlock();
        private readonly Uri _firstBlankImagePath;

        public List<Product> Products { get; set; } = Configuration.GetProducts();
        public MainWindow()
        {
            InitializeComponent();
            Products.ForEach(p => LstBxProduct.Items.Add(p));

            _firstBlankImagePath = new Uri(ImgProduct.Source.ToString());
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            const int firstChildrenIndex = 0;

            Button btn = sender as Button;
            Grid grid = btn.Parent as Grid;

            ScrollViewer scl = grid.Children[firstChildrenIndex] as ScrollViewer;

            _textBlock = scl.Content as TextBlock;

            Border bore = grid.Parent as Border;

            Grid newGrid = bore.Parent as Grid;

            Border border = newGrid.Children[firstChildrenIndex] as Border;

            _image = border.Child as Image;


            ImgProduct.Source = _image.Source;

            TxtBlckFilePath.Text = _image.Source.ToString();
            TxtBxInfoAboutProduct.Text = _textBlock.Text;

            _isCurrentProductChanges = true;

            TxtBxInfoAboutProduct.Focus();
            TxtBxInfoAboutProduct.SelectionStart = TxtBxInfoAboutProduct.Text.Length;
        }

        private void BtnUpload_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                DefaultExt = ".jpg",
                Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png"
            };

            Nullable<bool> result = openFileDialog.ShowDialog();

            if (result == true)
            {
                ImgProduct.Source = new BitmapImage(new Uri(openFileDialog.FileName));
                TxtBlckFilePath.Text = openFileDialog.FileName;
            }

        }


        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            Grid secondRowBaseGrid = button.Parent as Grid;

            TextBox secondRowBaseGridSecondChildren = (secondRowBaseGrid.Children[0] as ScrollViewer).Content as TextBox;
            Grid baseGrid = secondRowBaseGrid.Parent as Grid;

            Grid firstRowBaseGrid = baseGrid.Children[0] as Grid;
            Image firstRowBaseGridFirstChildren = firstRowBaseGrid.Children[0] as Image;

            TextBlock firstRowSecondChildren = ((firstRowBaseGrid.Children[1] as Grid).Children[0] as ScrollViewer).Content as TextBlock;

            #region IfYouWantCheckOut

            //Button insideBaseGridThirdGridSecondButton = sender as Button;

            //Grid insideBaseGridThirdGrid = button.Parent as Grid;

            //TextBox insideThirdGridInsideScrollViewerTextBox = (insideBaseGridThirdGrid.Children[0] as ScrollViewer).Content as TextBox;

            //Grid BaseGrid = insideBaseGridThirdGrid.Parent as Grid;

            //Grid insideBaseGridFirstGrid = BaseGrid.Children[0] as Grid;
            //Image insideFirstGridImage = insideBaseGridFirstGrid.Children[0] as Image;

            //TextBlock insideFirstGridFirstGridinsideScrollViewerinsideTextbox = ((insideBaseGridFirstGrid.Children[1] as Grid).Children[0] as ScrollViewer).Content as TextBlock;

            #endregion


            if (ImgProduct.Source.ToString() == _firstBlankImagePath.ToString() || 
                string.IsNullOrWhiteSpace(secondRowBaseGridSecondChildren.Text))
            {
                MessageBox.Show("You must upload image and add description to add this product.", "Products in co..");
                return;
            }



            if (_isCurrentProductChanges == true)
            {
                _textBlock.Text = secondRowBaseGridSecondChildren.Text;
                _image.Source = firstRowBaseGridFirstChildren.Source;               
                _isCurrentProductChanges = false;
            }
            else
            {
                Product product = new Product
                {
                    Name = secondRowBaseGridSecondChildren.Text,
                    ImagePath = firstRowBaseGridFirstChildren.Source.ToString()
                };

                LstBxProduct.Items.Add(product);

                LstBxProduct.ScrollIntoView(LstBxProduct.Items[LstBxProduct.Items.Count - 1]);

            }


            firstRowBaseGridFirstChildren.Source = new BitmapImage(_firstBlankImagePath);
            secondRowBaseGridSecondChildren.Text = default;
            firstRowSecondChildren.Text = "Upload your image by pressing upload.";
            MessageBox.Show("Saved Successfully.", "Products in co..");

        }

        
    }

}