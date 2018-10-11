using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using TUM.CMS.VplControl.Core;
using TUM.CMS.VplControl.Utilities;

namespace TUM.CMS.VplControl.Test
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += OnLoaded;

            VplGroupControl.MainVplControl.ExternalNodeTypes.AddRange(
                Utilities.Utilities.GetTypesInNamespace(Assembly.GetExecutingAssembly(), "TUM.CMS.VplControl.Test.Nodes")
                    .ToList());
           this.MouseWheel += Canvas_MouseWheel;
          
            VplGroupControl.MainVplControl.NodeTypeMode = NodeTypeModes.All;
        }

        private void btnCopy_Click(object sender, RoutedEventArgs e)
        {
            VplControl.VplControlCopy();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            VplControl.VplControlDelete();
        }

        private void BtnAll_Click(object sender, RoutedEventArgs e)
        {
            VplControl.VplControlSelectAll();
        }

        private void btnUnselect_Click(object sender, RoutedEventArgs e)
        {
            VplControl.VplControlUnselectAll();
        }

        private void BtnGroup_Click(object sender, RoutedEventArgs e)
        {
            VplControl.VplControlGroup();
        }


        private void BtnPaste_Click(object sender, RoutedEventArgs e)
        {
            VplControl.VplControlPaste();
        }

        const double ScaleRate = 1.1;
        private void Canvas_MouseWheel(object sender, MouseWheelEventArgs e)
        {
        
            if (e.Delta > 0 && (ScaleT.ScaleX * ScaleRate) < 1)
            {
                Point position = e.GetPosition(this);
                ScaleT.CenterX += e.GetPosition(VplControl).X;
                ScaleT.CenterY += e.GetPosition(VplControl).Y;
                ScaleT.ScaleX *= ScaleRate;
                ScaleT.ScaleY *= ScaleRate;

                VplControl.Width /= ScaleRate;
                VplControl.Height /=ScaleRate;
                Point cursorpos = Mouse.GetPosition(this);

                double discrepancyX = cursorpos.X - position.X;
                double discrepancyY = cursorpos.Y - position.Y;

                var panTransform=  VplControl.TranslateTransform;
                panTransform.X += discrepancyX;
                panTransform.Y += discrepancyY;
            }

            //}
            else if (e.Delta < 0)
            {
                Point position = e.GetPosition(this);

                ScaleT.CenterX = e.GetPosition(this).X;
                ScaleT.CenterY = e.GetPosition(this).Y;
                ScaleT.ScaleX /= ScaleRate;
                ScaleT.ScaleY /= ScaleRate;
                VplControl.Width *= ScaleRate;
                VplControl.Height *= ScaleRate;


                Point cursorpos = Mouse.GetPosition(this);

                double discrepancyX = cursorpos.X - position.X;
                double discrepancyY = cursorpos.Y - position.Y;

                var panTransform = VplControl.TranslateTransform;
                panTransform.X += discrepancyX;
                panTransform.Y += discrepancyY;

            }
            VplControl.UpdateLayout();

        }

        //private void Canvas_MouseWheel(object sender, MouseWheelEventArgs e)
        //{
        //    var element = sender as UIElement;
        //    var position = e.GetPosition(this.VplControl);
        //    var transform = VplControl.RenderTransform as MatrixTransform;
        //    var matrix = transform.Matrix;
        //    var scale = e.Delta >= 0 ? 1.1 : (1.0 / 1.1); // choose appropriate scaling factor

        //    matrix.ScaleAtPrepend(scale, scale, position.X, position.Y);
        //    transform.Matrix = matrix;
        //    VplControl.Width *= scale;
        //    VplControl.Height *= scale;
        //}
        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            var filePath = @"../testdata/test.vplxml";
            if (File.Exists(filePath))
            {
                VplControl.OpenFile(filePath);
                //VplGroupControl.MainVplControl.OpenFile(filePath);
            }
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }
    }
}