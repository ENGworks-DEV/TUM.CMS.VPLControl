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

        protected override void OnContentRendered(EventArgs e)
        {
            this.VplControl.Width = ViewBox.ActualWidth;
            this.VplControl.Height = ViewBox.ActualHeight;
            ScaleT.CenterX = ViewBox.ActualWidth / 2;
            ScaleT.CenterY = ViewBox.ActualHeight / 2;
            ScaleT.ScaleX = 1;
            ScaleT.ScaleY = 1;
            // Your code here.
        }
        private void onstart(object sender, DependencyPropertyChangedEventArgs e)
        {
            //this.VplControl.Width = ViewBox.ActualWidth;
            //this.VplControl.Height = ViewBox.ActualHeight;
            //ScaleT.CenterX = 0;
            //ScaleT.CenterY = 0;
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
        private double actualzoom { get; set; } = 1;
        private void Canvas_MouseWheel(object sender, MouseWheelEventArgs e)
        {

            //ScaleT.CenterX = ViewBox.ActualWidth / 2;
            //ScaleT.CenterY = ViewBox.ActualHeight / 2;
            if (e.Delta > 0 && actualzoom > 0.6)
            {
                ScaleT.ScaleX /= 1.1;
                ScaleT.ScaleY /= 1.1;
                VplControl.Height *= 1.1;
                VplControl.Width *= 1.1;
                actualzoom /= 1.1;
                ScaleT.CenterX = VplControl.Width / 2;
                ScaleT.CenterY = VplControl.Height / 2;
                ScaleT.ScaleX =1;
                ScaleT.ScaleY = 1;
                
                foreach (VplElement item in VplControl.NodeCollection)
                {
                    var t = VplControl.TransformToDescendant(item);
                    Point point = Mouse.GetPosition(item);
                    var p = t.Transform(point);
                    if (item.Border.MinHeight > 0 && item.Border.Width > 0)
                    {
                        //Center of item
                        var ItemY = (item.Top + item.Border.MinHeight / 2);
                        var ItemX = (item.Left + item.Border.Width / 2);

                        var middlePoint = new Point(((ItemX - p.X) / 2), ((ItemY - p.Y) / 2));
                        var renderT = item.RenderTransformOrigin = middlePoint;
                    }
                }
            }

         
            else if (e.Delta < 0 && actualzoom < 1.3)
            {
                ScaleT.ScaleX *= 1.1;
                ScaleT.ScaleY *= 1.1;
                VplControl.Height /= 1.1;
                VplControl.Width /=  1.1;
                actualzoom *= 1.1;
                ScaleT.CenterX = VplControl.Width / 2;
                ScaleT.CenterY = VplControl.Height / 2;
                ScaleT.ScaleX = 1;
                ScaleT.ScaleY = 1;

               
                foreach (VplElement item in VplControl.NodeCollection)
                {
                    var t = VplControl.TransformToDescendant(item);
                    Point point = Mouse.GetPosition(item);
                    var p = t.Transform(point);
                    if (item.Border.MinHeight > 0 && item.Border.Width >0)
                    {
                        //Center of item
                        var ItemY = (item.Top + item.Border.MinHeight / 2);
                        var ItemX = (item.Left + item.Border.Width / 2);

                        var middlePoint = new Point((ItemX + p.X) / 2, (ItemY + p.Y) / 2);
                        var renderT = item.RenderTransformOrigin = middlePoint;
                    }


                }
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