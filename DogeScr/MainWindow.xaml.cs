﻿using DogeScr.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
            //currently only support one mornitor
            InitWindowSize();
            Init();
        }

        private Configuration configuration;
        public TileGenerator generator;
        public readonly string StartupDir = AppDomain.CurrentDomain.BaseDirectory + "\\";
        public const string configFileName = "config.xml";

        private void InitWindowSize()
        {
            // fullscreen
            this.Left = 0;
            this.Top = 0;
            this.Width = SystemParameters.VirtualScreenWidth;
            this.Height = SystemParameters.VirtualScreenHeight;
        }


        private void Init()
        {
            try
            {
                configuration = Configuration.Load<Configuration>(StartupDir + configFileName);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                configuration = new Configuration();
                configuration.CreateDefault();
            }


            try
            {
                configuration.Save<Configuration>(StartupDir + configFileName);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            generator = new TileGenerator(configuration, (int)this.Width, (int)this.Height);
            generator.WorkerEvent += generator_WorkerEvent;
            generator.Start();
        }

#if DEBUG
        //get current process
        System.Diagnostics.Process proc = System.Diagnostics.Process.GetCurrentProcess();
        long maxMemory = 0;
#endif
        void generator_WorkerEvent(object sender, TileGenerator.WorkerEventArgs e)
        {

            TileStoryboard tileStoryboard = new TileStoryboard(e.element, 100, 100, 800);
            //add an tile element and auto remove when completed
            AddElement(e.element.Uid, e.element, MainGrid);
            tileStoryboard.Completed += delegate
            {
                RemoveElement(e.element.Uid, MainGrid);
            };
            //begin animation
            tileStoryboard.Begin();

#if DEBUG
            long usedMemory = proc.PrivateMemorySize64 / 1024 / 1024;
            if (usedMemory > maxMemory) maxMemory = usedMemory;//I don't care the thread lock
            label1.Content = (DateTime.Now - proc.StartTime) + "\n" +
                usedMemory.ToString() + " MB\nMax: " + maxMemory.ToString();
#endif
        }




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
            Console.Out.WriteLine("create " + uid);
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
                Console.Out.WriteLine("remove " + uid);
            }
        }

        private void MainGrid_MouseMove(object sender, MouseEventArgs e)
        {

            //Environment.Exit(0);

        }
    }
}




// by celeron533