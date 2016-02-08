using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace DogeScr.Core
{
    public class TileGenerator
    {
        public delegate void WorkerEventHandler(object sender, WorkerEventArgs e);
        public event WorkerEventHandler WorkerEvent;

        public List<string> phraseList = new List<string>();

        Random random = new Random(System.DateTime.Now.Millisecond);
        DispatcherTimer timer = new DispatcherTimer();

        const string dogeImage = "[dogeImage]";

        /// <summary>
        /// Generate qualified labels
        /// </summary>
        /// <param name="interval"></param>
        public TileGenerator(List<TileBase> tileList, int interval,
            int screenWidth, int screenHeight)
        {
            phraseList.Add("-10%");
            phraseList.Add("-20%");
            phraseList.Add("-25%");
            phraseList.Add("-33%");
            phraseList.Add("-50%");
            phraseList.Add("-75%");
            phraseList.Add("-85%");
            phraseList.Add("-66%");
            phraseList.Add(dogeImage);
            //
            ImageSource src = new BitmapImage(new Uri(@"pack://application:,,,/"
                             + Assembly.GetExecutingAssembly().GetName().Name
                             + ";component/"
                             + "Resources/Gabe.png", UriKind.Absolute));

            timer.Interval = new TimeSpan(interval * 1000 * 10);
            timer.Tick += delegate
            {
                //random
                string nextPharse = phraseList[random.Next(phraseList.Count)];
                int positionX = random.Next(screenWidth);
                int positionY = random.Next(screenHeight);
                Color randomColor = Color.FromScRgb(1, (float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble());
                randomColor = Color.FromRgb(255, 255, 255);//

                Label myLabel = new Label();
                myLabel.Uid = "a_" + System.Guid.NewGuid().ToString().Replace("-", "");

                //image or text
                if (string.Compare(nextPharse, dogeImage) == 0)
                {
                    myLabel.Background = new ImageBrush(src);
                    myLabel.Width = src.Width / 1;
                    myLabel.Height = src.Height / 1;
                    myLabel.Opacity = 0.8;
                }
                else
                {
                    myLabel.Content = nextPharse;
                    myLabel.FontSize = 36;
                    myLabel.Foreground = new SolidColorBrush(randomColor);
                    myLabel.Background = new SolidColorBrush(Color.FromRgb(75, 107, 32));
                }

                myLabel.VerticalAlignment = VerticalAlignment.Top;
                myLabel.HorizontalAlignment = HorizontalAlignment.Left;
                myLabel.Margin = new Thickness(positionX, positionY, 0, 0);

                WorkerEvent(this, new WorkerEventArgs() { element = myLabel });
                //GC.Collect(GC.MaxGeneration);
            };



        }

        public void Start()
        {
            timer.Start();
        }


        public class WorkerEventArgs : EventArgs
        {
            public UIElement element;
        }

    }
}
