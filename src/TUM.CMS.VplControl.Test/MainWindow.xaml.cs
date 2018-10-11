using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
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
            //if (ScaleT.ScaleX < 1.1)
            //{
            Point point = new Point(ScaleT.CenterX - e.GetPosition(this).X, ScaleT.CenterY - e.GetPosition(this).Y);
                if (e.Delta > 0 && (ScaleT.ScaleX* ScaleRate) < 1)
                {
                    
            
                    //ScaleT.CenterX = e.GetPosition(this).X;
                    //ScaleT.CenterY = e.GetPosition(this).Y;

                    ScaleT.ScaleX *= ScaleRate;
                    ScaleT.ScaleY *= ScaleRate;
                    
                    VplControl.Width = VplControl.ActualWidth / ScaleRate;
                    VplControl.Height = VplControl.ActualHeight / ScaleRate;
                    //VplControl.TranslateTransform.X = point.X;
                    //VplControl.TranslateTransform.Y = point.Y;

            }
            //if (e.Delta > 0 && ScaleT.ScaleX > 0.9)
            //{

            //    ScaleT.CenterX = e.GetPosition(this).X;
            //    ScaleT.CenterY = e.GetPosition(this).Y;
            //    ScaleT.ScaleX *= ScaleRate;
            //    ScaleT.ScaleY *= ScaleRate;

            //}
            else if(e.Delta < 0 )
                {
                VplControl.TranslateTransform.X = e.GetPosition(this).X;
                VplControl.TranslateTransform.Y = e.GetPosition(this).Y;
                ScaleT.ScaleX /= ScaleRate;
                ScaleT.ScaleY /= ScaleRate;
                VplControl.Width = (VplControl.RenderSize.Width * ScaleRate);
                    VplControl.Height = (VplControl.RenderSize.Height * ScaleRate);

                //VplControl.TranslateTransform.X = 0;
                //VplControl.TranslateTransform.Y = 0;
            }
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