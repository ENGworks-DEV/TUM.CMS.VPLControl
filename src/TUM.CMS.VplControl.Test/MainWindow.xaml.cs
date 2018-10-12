using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
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

                //Zoom out
                ScaleT.ScaleX /= 1.1;
                ScaleT.ScaleY /= 1.1;
                VplControl.Height *= 1.1;
                VplControl.Width *= 1.1;
                actualzoom /= 1.1;
                ScaleT.CenterX = VplControl.Width / 2;
                ScaleT.CenterY = VplControl.Height / 2;
 
                
                //foreach (Node item in VplControl.NodeCollection)
                //{
                   
                //    Point point = e.GetPosition(item);
                    
                //    if (item.Border.MinHeight > 0 && item.Border.Width > 0)
                //    {
                //        //Zoom out

                //        ////Center of item
                //        //var ItemY = (item.Top);
                //        //var ItemX = (item.Left);

                        
                //        //item.Top = ItemY-((ItemY - point.Y) /2);
                //        //item.Left = ItemX-((ItemX - point.X)/2);

                //        ////Center of item
                //        var ItemY = (item.RenderTransformOrigin.Y);
                //        var ItemX = (item.RenderTransformOrigin.X);

                //        var middlePoint = new Point(((ItemX -  point.X) *300), ((ItemY - point.Y) / 2));
                //        var transform = item.RenderTransform;
                //        TranslateTransform translateTransform = new TranslateTransform();
                //        translateTransform.X = (ItemX - point.X) / 2;
                //        translateTransform.Y = (ItemY - point.Y) / 2;

                //        transform = translateTransform;
                //        item.SetValue(Node.RenderTransformProperty, translateTransform);
                //    }
                //}
                ScaleT.ScaleX = 1;
                ScaleT.ScaleY = 1;
            }

         
            else if (e.Delta < 0 && actualzoom < 1.3)
            {

                //Zoom in
                ScaleT.ScaleX *= 1.1;
                ScaleT.ScaleY *= 1.1;
                VplControl.Height /= 1.1;
                VplControl.Width /=  1.1;
                actualzoom *= 1.1;
                ScaleT.CenterX = VplControl.Width / 10;
                ScaleT.CenterY = VplControl.Height / 10;

               
                //foreach (Node item in VplControl.NodeCollection)
                //{
                   
                //    Point point = e.GetPosition(item);
                    
                //    if (item.Border.MinHeight > 0 && item.Border.Width >0)
                //    {
                //        ////Center of item
                //        //var ItemY = (item.Top);
                //        //var ItemX = (item.Left);

                //        ////Zoom in
                //        //item.Top = (2 * ItemY + point.Y) / 10;
                //        //item.Left = (2* ItemX + point.X) / 10;

                //        //Center of item
                //        var ItemY = (item.RenderTransformOrigin.Y);
                //        var ItemX = (item.RenderTransformOrigin.X);

                //        var middlePoint = new Point(((ItemX + point.X) / 2), ((ItemY + point.Y) / 2));
                //        var transform = item.RenderTransform;

                //        TranslateTransform translateTransform = new TranslateTransform();
                //        translateTransform.X = (ItemX - point.X) / 2;
                //        translateTransform.Y = (ItemY - point.Y) / 2;

                //        transform = translateTransform;
                //        item.SetValue(Node.RenderTransformOriginProperty, middlePoint);
                        
                //    }


                //}
                ScaleT.ScaleX = 1;
                ScaleT.ScaleY = 1;

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