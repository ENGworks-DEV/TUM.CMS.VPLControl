using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using TUM.CMS.VplControl.Core;

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

            this.VplControl.SizableParent = Tab;
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
        private double actualzoom { get; set; } = 1;

        private void Canvas_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            //Performing Matrix scale
            var element = sender as UIElement;

            var transform = VplControl.RenderTransform as MatrixTransform;
            var matrix = transform.Matrix;
            var scale = e.Delta >= 0 ? 1.1 : (1.0 / 1.1);
            var position = e.GetPosition(Vbox);

            {
                //Limit scale to 1.2 - 0.5
               // scale = ((actualzoom > 0.5 || scale == 1.1) && (actualzoom < 1.2 || scale < 1)) ? scale : 1;
                matrix.ScaleAt(scale, scale, position.X, position.Y);
                transform.Matrix = matrix;

                actualzoom *= scale;

            }

            VplControl.UpdateLayout();

        }


        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            var filePath = @"../testdata/test.vplxml";
            if (File.Exists(filePath))
            {
                VplControl.OpenFile(filePath);
                
            }
        }



        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }
    }
}