using PdfSharp.Drawing;
using PdfSharp.Pdf;
using Pipeline.AddInViews;
using System;
using System.AddIn;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace ExportPlugin
{
    [AddIn("Export")]
    public class ExportPlugin : IExportPluginView
    {
        private readonly List<string> supportedExtensions = new List<string> { ".txt" };
        private readonly string tempPath = Path.Combine(Path.GetTempPath(), "DemoExportPlugin");

        private IPluginApi api;
        private PanelUIModel model;

        public string DisplayName => "Export";

        public void Initialize(IPluginApi pluginApi)
        {
            api = pluginApi ?? throw new ArgumentNullException(nameof(api));
        }

        public bool CanExport(string file)
        {
            return !string.IsNullOrEmpty(file) && supportedExtensions.Contains(Path.GetExtension(file));
        }

        public void Export(string file)
        {
            model.FileNameText = $"Export file: {Path.GetFileName(file)}";

            model.StatusText = "Converting...";

            try
            {
                PdfDocument pdf = new PdfDocument();
                pdf.Info.Title = $"Converted {Path.GetFileName(file)}";

                PdfPage page = pdf.AddPage();
                XGraphics gfx = XGraphics.FromPdfPage(page);
                XFont font = new XFont("Verdana", 14, XFontStyle.BoldItalic);

                foreach (var line in File.ReadAllLines(file))
                {
                    gfx.DrawString(line, font, XBrushes.Black, new XRect(0, 0, page.Width, page.Height), XStringFormats.Center);
                }


                if (!Directory.Exists(tempPath))
                    Directory.CreateDirectory(tempPath);

                var convertedFileName = Path.Combine(tempPath, $"{Path.GetFileNameWithoutExtension(file)}.pdf");

                pdf.Save(convertedFileName);

                model.StatusText = "Converting is finished";
                model.ConvertedFilePath = convertedFileName;
            }
            catch (Exception)
            {
                model.StatusText = "Error on converting";
            }

        }

        public FrameworkElement GetPanelUI()
        {
            model = new PanelUIModel { FileNameText = "No files selected to export" };
            return new PanelUI { DataContext = model };
        }
    }
}
