using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace BaoMen.Framework.Web.Util
{
    /// <summary>
    /// 验证码
    /// </summary>
    public class Captcha
    {
        private int width = 50;
        /// <summary>
        /// 宽度
        /// </summary>
        public int Width
        {
            //get { return width; }
            set { width = value; }
        }
        private int height = 20;
        /// <summary>
        /// 高度
        /// </summary>
        public int Height
        {
            //get { return height; }
            set { height = value; }
        }
        private int fontSize = 14;
        /// <summary>
        /// 字体大小
        /// </summary>
        public int FontSize
        {
            //get { return fontSize; }
            set { fontSize = value; }
        }

        private const string chars = "23456789abcdefghijklmnopqrstuvwxyz";
        //private const string chars = "23456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        private static readonly Color backColor = Color.White;
        private static readonly Pen borderColor = Pens.DarkGray;
        private static readonly string[] fontItems = new string[] { "Arial", "Helvetica", "Geneva", "sans-serif", "Verdana" };

        private static readonly Brush[] brushItems = new Brush[] { Brushes.OliveDrab, Brushes.ForestGreen, Brushes.DarkCyan, Brushes.LightSlateGray,
                                                                            Brushes.RoyalBlue, Brushes.SlateBlue, Brushes.DarkViolet, Brushes.MediumVioletRed,
                                                                            Brushes.IndianRed, Brushes.Firebrick, Brushes.Chocolate, Brushes.Peru, Brushes.Goldenrod };
        private readonly Random random;


        private Brush GetBrush()
        {
            int brushIndex = random.Next(brushItems.Length);
            return brushItems[brushIndex];
        }

        private Font GetFont()
        {
            int fontIndex = random.Next(fontItems.Length);
            FontStyle fontStyle = GetFontStyle(random.Next(2));
            return new Font(fontItems[fontIndex], fontSize, fontStyle);
        }

        private FontStyle GetFontStyle(int index)
        {
            return index switch
            {
                0 => FontStyle.Bold,
                1 => FontStyle.Italic,
                _ => FontStyle.Regular,
            };
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public Captcha()
        {
            random = new Random(unchecked((int)DateTime.Now.Ticks));
        }

        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <param name="length">长度</param>
        /// <returns></returns>
        public string GetCaptcha(int length)
        {
            List<int> indexes = new List<int>(length);
            string captcha = string.Empty;
            for (int i = 0; i < length; i++)
            {
                int index;
                do
                {
                    index = random.Next(chars.Length);
                }
                while (indexes.Contains(index));
                indexes.Add(index);
                captcha += chars[index];
            }
            return captcha;
        }

        /// <summary>
        /// 绘制验证码
        /// </summary>
        /// <param name="captcha">验证码</param>
        /// <param name="stream">流</param>
        public void Draw(string captcha, global::System.IO.Stream stream)
        {
            Bitmap bitmap = null;
            Graphics graphics = null;
            try
            {
                bitmap = new Bitmap(width, height);
                graphics = Graphics.FromImage(bitmap);
                graphics.Clear(backColor);
                Brush brush = GetBrush();
                //graphics.DrawString(captcha, GetFont(), brush, 1, 1);
                Font font = new Font("Arial", fontSize, FontStyle.Bold);
                SizeF size = GetStringSize(captcha, font);
                float x = (width - size.Width) / 2;
                float y = (height - size.Height) / 2;
                graphics.DrawString(captcha, font, brush, x, y);
                Color color = ((SolidBrush)brush).Color;
                //Color color = Color.Gray;
                for (int n = 0; n < 50; n++)
                {
                    int tempX = random.Next(width);
                    int tempY = random.Next(height);
                    bitmap.SetPixel(tempX, tempY, color);
                }
                graphics.DrawRectangle(borderColor, 0, 0, width - 1, height - 1);
                bitmap.Save(stream, ImageFormat.Png);
            }
            catch { }
            finally
            {
                if (null != bitmap)
                    bitmap.Dispose();
                if (null != graphics)
                    graphics.Dispose();
            }
        }

        /// <summary>
        /// 取得字符串的大小
        /// </summary>
        /// <param name="value">字符串的值</param>
        /// <param name="font">字体信息</param>
        /// <returns></returns>
        public SizeF GetStringSize(string value, Font font)
        {
            Graphics graphics = Graphics.FromImage(new Bitmap(1, 1));
            SizeF sizeF = graphics.MeasureString(value, font);
            graphics.Dispose();
            return sizeF;
        }
    }
}
