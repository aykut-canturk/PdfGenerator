using PdfSharpCore.Drawing;
using PdfSharpCore.Fonts;
using PdfSharpCore.Pdf;
using PdfSharpCore.Utils;
using System;
using System.Collections.Generic;

namespace PdfSharpDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            // Initialize font resolver
            GlobalFontSettings.FontResolver = new FontResolver();

            // Create and generate PDF
            var pdfGenerator = new PdfGenerator();
            pdfGenerator.GeneratePdf("helloworld.pdf");
        }
    }

    class PdfGenerator
    {
        public void GeneratePdf(string outputPath)
        {
            var document = new PdfDocument();
            var page = document.AddPage();

            var gfx = XGraphics.FromPdfPage(page);
            var font = new XFont("Arial", 20, XFontStyle.Bold);

            // Add logo
            AddLogo(gfx, page);

            var textColor = XBrushes.Black;

            // Add Lorem Ipsum text
            string loremIpsum = GetLoremIpsum();

            var loremFont = new XFont("Arial", 10, XFontStyle.Regular);
            double leftMargin = 50;
            double topMargin = 120;
            double rightMargin = 50;
            double width = page.Width - leftMargin - rightMargin;

            // Draw wrapped text
            DrawWrappedText(gfx, loremIpsum, loremFont, textColor, leftMargin, topMargin, width);

            document.Save(outputPath);
        }

        private void AddLogo(XGraphics gfx, PdfPage page)
        {
            var logo = XImage.FromFile("logo.png");
            double logoWidth = 100; // Adjust based on your logo's desired width
            double logoHeight = logo.PixelHeight * (logoWidth / logo.PixelWidth); // Maintain aspect ratio
            double xPosition = page.Width - logoWidth - 50; // 50 units margin from right
            double yPosition = 20; // 20 units margin from top
            gfx.DrawImage(logo, xPosition, yPosition, logoWidth, logoHeight);
        }

        private string GetLoremIpsum()
        {
            return @"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce vel pulvinar ex, a euismod eros. Sed tortor odio, vulputate eget nulla sed, cursus porttitor ex. Nulla ligula elit, mollis facilisis blandit non, bibendum a justo. Etiam porttitor dolor non dui placerat, quis accumsan justo tempus. Sed tortor eros, commodo eget leo non, pharetra pretium purus. Morbi sapien leo, viverra ut augue vitae, fermentum vulputate ligula. Nulla id rhoncus lacus, sed sollicitudin turpis. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Sed eleifend pulvinar arcu quis posuere. Nulla nulla arcu, vehicula vel lacus vitae, interdum efficitur orci. Pellentesque molestie interdum faucibus.
Ut eleifend euismod massa non sodales. Suspendisse ac laoreet nisi. Mauris sit amet eros ac tortor faucibus condimentum eget eu odio. Phasellus vel elit sed magna feugiat ornare nec et mi. Interdum et malesuada fames ac ante ipsum primis in faucibus. Ut egestas tortor ut metus porttitor pretium. Morbi odio leo, varius non vehicula in, blandit et velit.
Proin dictum, erat in lacinia auctor, quam erat congue tortor, quis consequat elit nulla vel nisl. Donec ipsum libero, suscipit eu ex vel, vulputate accumsan urna. Nunc in blandit est, eu gravida dolor. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Pellentesque elementum quis dui id interdum. Etiam vel velit ut lectus tincidunt interdum. Maecenas mattis hendrerit magna non volutpat. Sed non erat congue, suscipit ligula a, imperdiet risus. Donec consectetur orci at vehicula posuere. Pellentesque bibendum nisi orci, ut tempus augue commodo non. Nunc ut nunc ante. Nulla tristique metus non egestas porttitor.
Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Aliquam pulvinar lorem elit, nec pulvinar mi cursus at. Sed a imperdiet enim. Morbi non arcu quis tortor pretium ullamcorper euismod eget ipsum. Pellentesque bibendum egestas augue sed luctus. Aenean fermentum venenatis urna, non consectetur massa tempor ut. Nullam non fermentum augue. Nam convallis aliquam tortor eget suscipit.
Suspendisse nec consectetur tellus, dictum varius dolor. Nulla vitae risus gravida, lobortis eros in, commodo metus. Nam aliquet nisi at sapien aliquet ullamcorper. Duis luctus fringilla sem at elementum. Maecenas vestibulum urna eu purus malesuada, vel vulputate risus convallis. Donec egestas nunc eget tempor finibus. Quisque bibendum lectus ut libero ullamcorper malesuada. Integer sollicitudin eleifend malesuada. Etiam vel libero dapibus, efficitur magna et, consectetur dolor. Nullam nec eros et nibh ultrices imperdiet laoreet ac mi. Proin id mi finibus, laoreet magna eu, interdum erat.";
        }

        private void DrawWrappedText(XGraphics graphics, string text, XFont font, XBrush brush, double x, double y, double maxWidth)
        {
            // Clean the text by replacing any problematic characters
            text = text.Replace("\r", "").Trim();
            string[] paragraphs = text.Split('\n');
            double lineHeight = font.GetHeight() * 1.2;
            double currentY = y;

            foreach (var paragraph in paragraphs)
            {
                // Clean each paragraph to remove any special characters that might cause squares
                string cleanParagraph = paragraph.Trim();
                if (string.IsNullOrWhiteSpace(cleanParagraph))
                {
                    currentY += lineHeight;
                    continue;
                }

                string[] words = cleanParagraph.Split(' ');
                string line = "";

                foreach (var word in words)
                {
                    string testLine = line.Length > 0 ? line + " " + word : word;
                    double lineWidth = graphics.MeasureString(testLine, font).Width;

                    if (lineWidth > maxWidth)
                    {
                        graphics.DrawString(line, font, brush, x, currentY);
                        line = word;
                        currentY += lineHeight;
                    }
                    else
                    {
                        line = testLine;
                    }
                }

                if (line.Length > 0)
                {
                    graphics.DrawString(line, font, brush, x, currentY);
                    currentY += lineHeight * 1.5; // Extra space after paragraph
                }
            }
        }
    }
}