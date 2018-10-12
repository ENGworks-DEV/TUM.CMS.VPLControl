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

        //protected override void OnContentRendered(EventArgs e)
        //{
        //    this.VplControl.Width = ViewBox.ActualWidth;
        //    this.VplControl.Height = ViewBox.ActualHeight;
        //    var positionX = ViewBox.ActualWidth / 2;
        //    var positionY = ViewBox.ActualHeight / 2;

        //    var transform = VplControl.RenderTransform as MatrixTransform;
        //    var matrix = transform.Matrix;
        //    matrix.ScaleAtPrepend(1, 1, positionX, positionY);
        //    transform.Matrix = matrix;
        //    // Your code here.
        //}
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
                var element = sender as UIElement;
                
                var transform = VplControl.RenderTransform as MatrixTransform;
                var matrix = transform.Matrix;
                var scale = e.Delta >= 0 ? 1.1 : (1.0 / 1.1); // choose appropriate scaling factor
                var position = e.GetPosition(Vbox);

            {
                    //Limit scale to 1.2 - 0.5
                    scale = (( actualzoom > 0.5 || scale == 1.1)&& (actualzoom < 1.2 || scale <1 )) ? scale: 1;
                    matrix.ScaleAt(scale, scale, position.X, position.Y);
                    transform.Matrix = matrix;
                    //VplControl.Width /= scale;
                    //VplControl.Height /= scale;
                    actualzoom *= scale;



            }

            VplControl.UpdateLayout();
            //foreach (Border item in VplControl.Children.OfType<Border>())
            //{

            //    var obj = item.Child;
            //    var position = e.GetPosition(VplControl);

            //    Point Center  =new Point( VplControl.Width /2, VplControl.Height/2) ;
                
            //    var type = obj.GetType();
            //    if (obj.GetType().BaseType == typeof(Node))
            //    {
            //        //if(actualzoom > 0)
            //        //{
            //            var node = obj as Node;
            //            Point nCurrentL = new Point(node.Top, node.Left);
            //            Point nodeNewLocation = new Point((nCurrentL.X + Center.X)/2, (nCurrentL.Y + Center.Y) / 2);
            //            node.Top = nodeNewLocation.Y;
            //            node.Left = nodeNewLocation.X;
                        
            //        //}

            //    }

            //}
        }


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