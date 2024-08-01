using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities
{
    public class CaptchaService
    {
        public static Tuple<string, byte[]> GenerateCaptcha(int width = 300, int height = 150)
        {
            string captchaText = GenerateRandomText();
            byte[] captchaImage = GenerateImage(captchaText, width, height);


            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string filePath = Path.Combine(desktopPath, "xvg82hgtica.png");
            File.WriteAllBytes(filePath, captchaImage);

            return Tuple.Create(captchaText, captchaImage);
        }

        private static string GenerateRandomText()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();

            return new string(Enumerable.Repeat(chars, 6)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private static byte[] GenerateImage(string text, int width, int height)
        {
            using (Bitmap bitmap = new Bitmap(width, height))
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
                graphics.Clear(Color.White);

                // Daha güzel bir font ve renk seçimi
                Font font = new Font("Segoe UI", 30, FontStyle.Bold);
                SolidBrush textBrush = new SolidBrush(Color.DarkSlateGray);

                // Captcha metnini yan yana ve yamuk bir şekilde yerleştirme
                Random random = new Random();
                float x = 10;

                foreach (char character in text)
                {

                    float angle = random.Next(-30, 30);

                    Matrix matrix = new Matrix();
                    matrix.RotateAt(angle, new PointF(x, height / 2));

                    // Karakteri transformasyon matrisi ile çizme
                    graphics.Transform = matrix;
                    graphics.DrawString(character.ToString(), font, textBrush, x, height / 2);
                    graphics.ResetTransform();

                    // Sonraki karakter için x konumunu güncelleme
                    x += graphics.MeasureString(character.ToString(), font).Width + 5;
                }


                AddRandomLines(graphics, width, height, 9);


                using (MemoryStream ms = new MemoryStream())
                {
                    bitmap.Save(ms, ImageFormat.Png);
                    return ms.ToArray();
                }
            }
        }

        private static void AddRandomLines(Graphics graphics, int width, int height, int lineCount)
        {
            Random random = new Random();
            using (Pen linePen = new Pen(Color.Gray, 2))
            {
                for (int i = 0; i < lineCount; i++)
                {
                    float x1 = random.Next(width);
                    float y1 = random.Next(height);
                    float x2 = random.Next(width);
                    float y2 = random.Next(height);


                    x2 = x1 + (x2 - x1) * 2.5f;
                    y2 = y1 + (y2 - y1) * 2.5f;

                    graphics.DrawLine(linePen, x1, y1, x2, y2);
                }
            }
        }

    }

}
