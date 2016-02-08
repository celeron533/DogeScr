using DogeScr.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DogeScr
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitWindowSize();
            Init();
        }

        private Configuration configuration;
        private DoubleAnimation animationTemplate = new DoubleAnimation();
        public TileGenerator generator;

        private void InitWindowSize()
        {
            // fullscreen
            this.Left = 0;
            this.Top = 0;
            this.Width= SystemParameters.VirtualScreenWidth;
            this.Height= SystemParameters.VirtualScreenHeight;
        }


        private void Init()
        {
            //configuration = Configuration.Load<Configuration>("C:\\a.xml");
            //configuration.Save<Configuration>("C:\\a.xml");
            generator = new TileGenerator(null, 10, (int)this.Width, (int)this.Height);
            generator.WorkerEvent += generator_WorkerEvent;
            generator.Start();
        }

        //get current process
        System.Diagnostics.Process proc = System.Diagnostics.Process.GetCurrentProcess();
        void generator_WorkerEvent(object sender, TileGenerator.WorkerEventArgs e)
        {

            TileStoryboard defaultStoryboard = new TileStoryboard(e.element, 100, 100, 800);
                AddElement(e.element.Uid, e.element, MainGrid);
                defaultStoryboard.Completed += delegate { 
                    RemoveElement(e.element.Uid, MainGrid);
                };

            defaultStoryboard.Begin();

            long usedMemory = proc.PrivateMemorySize64 / 1024 / 1024  ;
            if (usedMemory > max) max = usedMemory;
            label1.Content = (DateTime.Now - proc.StartTime) + "\n"+
                usedMemory.ToString()+" MB\nMax: "+max.ToString();
        }
        long max = 0;
        


        /// <summary>
        /// Add and register an element to the parent panel
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="element"></param>
        /// <param name="panel"></param>
        public void AddElement(string uid, UIElement element, Panel panel)
        {
            panel.Children.Add(element);
            panel.RegisterName(uid, element);
            Console.Out.WriteLine("create "+uid);
        }

        /// <summary>
        /// Remove and deregister an element from parent panel
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="panel"></param>
        public void RemoveElement(string uid, Panel panel)
        {
            UIElement element = panel.FindName(uid) as UIElement;
            if (element != null)
            {
                panel.Children.Remove(element);
                panel.UnregisterName(uid);
                Console.Out.WriteLine("remove "+uid);
            }
        }

        private void MainGrid_MouseMove(object sender, MouseEventArgs e)
        {

                //Environment.Exit(0);

        }
    }
}




// by celeron533