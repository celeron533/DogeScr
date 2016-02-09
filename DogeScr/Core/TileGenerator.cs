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

            timer.Interval = new TimeSpan(interval * 1000 * 10);
            timer.Tick += delegate
            {
                //random
                TileBase nextTile = tileList[random.Next(tileList.Count)];
                int positionX = random.Next(screenWidth);
                int positionY = random.Next(screenHeight);

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
                        tileLabel.Background = new ImageBrush(new BitmapImage(new Uri
                            (new Uri(AppDomain.CurrentDomain.BaseDirectory), imageTile.imagePath)
                            ));
                        tileLabel.Width = imageTile.imageSize.Width;
                        tileLabel.Height = imageTile.imageSize.Height;
                        tileLabel.Opacity = imageTile.opacity;
                        break;
                    case TileType.Text:
                        TextTile textTile = (TextTile)nextTile;
                        tileLabel.Content = textTile.text;
                        tileLabel.FontSize = textTile.fontSize;

                        tileLabel.Background = new SolidColorBrush(textTile.background);
                        if (textTile.randomBackground)
                            tileLabel.Background = new SolidColorBrush(
                                Color.FromScRgb(1, (float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble())
                                );

                        tileLabel.Foreground = new SolidColorBrush(textTile.foreground);
                        if (textTile.randomForeground)
                            tileLabel.Foreground = new SolidColorBrush(
                                Color.FromScRgb(1, (float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble())
                                );

                        break;
                    default://undefined TileType
                        return;
                }

                WorkerEvent(this, new WorkerEventArgs() { element = tileLabel });

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
