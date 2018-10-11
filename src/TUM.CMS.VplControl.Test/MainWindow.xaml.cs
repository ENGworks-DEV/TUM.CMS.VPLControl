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
            if (e.Delta > 0)
            {
                ScaleT.ScaleX *= ScaleRate;
                ScaleT.ScaleY *= ScaleRate;
            }
            else
            {
                ScaleT.ScaleX /= ScaleRate;
                ScaleT.ScaleY /= ScaleRate;
            }
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