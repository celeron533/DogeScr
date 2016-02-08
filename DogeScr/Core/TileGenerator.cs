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

        Random random = new Random(System.DateTime.Now.Millisecond);
        DispatcherTimer timer = new DispatcherTimer();

        /// <summary>
        /// Generate qualified labels
        /// </summary>
        /// <param name="interval"></param>
        public TileGenerator(List<TileBase> tileList, int interval,
            int screenWidth, int screenHeight)
        {

            //
            //ImageSource src = new BitmapImage(new Uri(@"pack://application:,,,/"
            //                 + Assembly.GetExecutingAssembly().GetName().Name
            //                 + ";component/"
            //                 + "Resources/Gabe.png", UriKind.Absolute));

            timer.Interval = new TimeSpan(interval * 1000 * 10);
            timer.Tick += delegate
            {
                //random
                TileBase nextTile = tileList[random.Next(tileList.Count)];
                int positionX = random.Next(screenWidth);
                int positionY = random.Next(screenHeight);
                Color randomColor = Color.FromScRgb(1, (float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble());
                randomColor = Color.FromRgb(255, 255, 255);//

                Label tileLabel = new Label();
                tileLabel.Uid = "a_" + Guid.NewGuid().ToString().Replace("-", "");
                tileLabel.VerticalAlignment = VerticalAlignment.Top;
                tileLabel.HorizontalAlignment = HorizontalAlignment.Left;
                tileLabel.Margin = new Thickness(positionX, positionY, 0, 0);

                //image or text
                switch (nextTile.tileType)
                {
                    case TileType.Image:
                        ImageTile imageTile = (ImageTile)nextTile;
                        tileLabel.Background = new ImageBrush( new BitmapImage(new Uri(imageTile.imagePath, UriKind.Absolute)));
                        tileLabel.Width = imageTile.imageSize.Width;
                        tileLabel.Height = imageTile.imageSize.Height;
                        tileLabel.Opacity = imageTile.opacity;
                        break;
                    case TileType.Text:
                        TextTile textTile = (TextTile)nextTile;
                        tileLabel.Content = textTile.text;
                        tileLabel.Background = new SolidColorBrush(textTile.background);
                        tileLabel.Foreground = new SolidColorBrush(textTile.forground);
                        tileLabel.FontSize = textTile.fontSize;
                        break;
                    default://undefined TileType
                        return;
                }

                //if (string.Compare(nextPharse, dogeImage) == 0)
                //{
                //    tileLabel.Background = new ImageBrush(src);
                //    tileLabel.Width = src.Width / 1;
                //    tileLabel.Height = src.Height / 1;
                //    tileLabel.Opacity = 0.8;
                //}
                //else
                //{
                //    tileLabel.Content = nextPharse;
                //    tileLabel.FontSize = 36;
                //    tileLabel.Foreground = new SolidColorBrush(randomColor);
                //    tileLabel.Background = new SolidColorBrush(Color.FromRgb(75, 107, 32));
                //}

                WorkerEvent(this, new WorkerEventArgs() { element = tileLabel });
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
