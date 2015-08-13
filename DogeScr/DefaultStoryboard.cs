using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Animation;
using System.Windows;

namespace DogeScr
{
    public class DefaultStoryboard: Storyboard
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <param name="fadeInDuration"></param>
        /// <param name="fadeOutDuration"></param>
        /// <param name="stayDuration"></param>
        public DefaultStoryboard(UIElement element,long fadeInDuration, long fadeOutDuration, long stayDuration)
        {
            this.RepeatBehavior = new RepeatBehavior(1);
            FadeAnimation fadeIn = new FadeAnimation(FadeAnimation.FadeType.In, fadeInDuration, element);
            this.AddChild(fadeIn);

            FadeAnimation fadeOut = new FadeAnimation(FadeAnimation.FadeType.Out, fadeOutDuration, element);
            fadeOut.BeginTime = new TimeSpan((fadeInDuration+stayDuration) * 1000 * 10);
            this.AddChild(fadeOut);
        }

        public class FadeAnimation : DoubleAnimation
        {
            public enum FadeType
            { 
                In, Out
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="fadeType"></param>
            /// <param name="duration">millisecond</param>
            /// <param name="element"></param>
            public FadeAnimation(FadeType fadeType, long duration, UIElement element)
            {
                this.From = (fadeType == FadeType.In ? 0 : 1);
                this.To = (fadeType == FadeType.In ? 1 : 0);
                this.Duration = new Duration(new TimeSpan(duration * 1000 * 10));
                Storyboard.SetTarget(this, element);
                Storyboard.SetTargetProperty(this, new PropertyPath("(UIElement.Opacity)"));
            }
        }


    }
}
