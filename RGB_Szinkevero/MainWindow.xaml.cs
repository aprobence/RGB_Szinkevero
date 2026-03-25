using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RGB_Szinkevero
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            FrissitesSzinek();
        }

        private void ColorChange(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (ColorPreview != null)
            {
                FrissitesSzinek();
            }
        }

        private void FrissitesSzinek()
        {
            byte r = (byte)RedSlider.Value;
            byte g = (byte)GreenSlider.Value;
            byte b = (byte)BlueSlider.Value;
            if (r == 255 && g == 255 && b == 255)
            {
                txtPreviewLabel.Foreground = new SolidColorBrush(Colors.Black);
            } else
            {
                txtPreviewLabel.Foreground = new SolidColorBrush(Colors.White);
            }
            Color aktualisSzin = Color.FromRgb(r, g, b);
            ColorPreview.Fill = new SolidColorBrush(aktualisSzin);
            txtRGB.Text = $"RGB: ({r}, {g}, {b})";
            txtHex.Text = $"HEX: #{r:X2}{g:X2}{b:X2}";
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            RedSlider.Value = 0;
            GreenSlider.Value = 0;
            BlueSlider.Value = 0;
        }

        private void btnRandom_Click(object sender, RoutedEventArgs e)
        {
            Random rnd = new Random();
            if (RedCheckBox.IsChecked == false) RedSlider.Value = rnd.Next(256);
            if (GreenCheckBox.IsChecked == false) GreenSlider.Value = rnd.Next(256);
            if (BlueCheckBox.IsChecked == false) BlueSlider.Value = rnd.Next(256);
        }

        private void btnCanvasDelete_Click(object sender, RoutedEventArgs e)
        {
            DrawingCanvas.Children.Clear();
        }

        private void DrawingCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Drawing(e.GetPosition(DrawingCanvas));
            }
        }

        private void DrawingCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Drawing(e.GetPosition(DrawingCanvas));
            }
        }

        private void Drawing(Point pos)
        {
            Shape shape;
            if (rbCircle.IsChecked == false)
            {
                shape = new Rectangle();
            }
            else
            {
                shape = new Ellipse();
            }
            double size = BrushSizeSlider.Value;
            shape.Width = size;
            shape.Height = size;
            shape.Fill = ColorPreview.Fill;

            Canvas.SetLeft(shape, pos.X - size / 2);
            Canvas.SetTop(shape, pos.Y - size / 2);
            DrawingCanvas.Children.Add(shape);
        }
    }
}