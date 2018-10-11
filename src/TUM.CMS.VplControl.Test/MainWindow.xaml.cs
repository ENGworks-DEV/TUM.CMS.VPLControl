using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Data;
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
            
            VplControl.Height = this.Height;
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

            var element = sender as UIElement;
            var position = e.GetPosition(this.VplControl);

            VplControl.WorkspaceElements.Add( VplControl.NodeCollection);

            var transform = VplControl.TransformToDescendant(VplControl.NodeCollection[0]);
            var matrix = trans.Matrix;
            var scale = e.Delta >= 0 ? 1.1 : (1.0 / 1.1); // choose appropriate scaling factor

            matrix.ScaleAtPrepend(scale, scale, position.X, position.Y);
            matrix.TranslatePrepend(position.X, position.Y);
            transform.Matrix = matrix;
            
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