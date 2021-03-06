using System;
using System.Collections.Generic;
using System.Text;
using fyiReporting.RDL;
using System.DrawingCore;
using System.ComponentModel;
using System.Xml;
using ZXing.QrCode;
using ZXing.Rendering;
using System.IO;

namespace fyiReporting.CRI
{
    public class BarCodeEAN8 : ICustomReportItem
    {
        static public readonly float OptimalHeight = 35.91f;          // Optimal height at magnification 1    
        static public readonly float OptimalWidth = 65.91f;            // Optimal width at mag 1
        private string _codeEan8 = "";

        #region ICustomReportItem Members

        public bool IsDataRegion()
        {
            return false;
        }

        public void DrawImage(ref Bitmap bm)
        {
            DrawImage(ref bm, _codeEan8);
        }


        /// <summary>
        /// Design time: Draw a hard coded BarCode for design time;  Parameters can't be
        /// relied on since they aren't available.
        /// </summary>
        /// <param name="bm"></param>
        public void DrawDesignerImage(ref Bitmap bm)
        {
            DrawImage(ref bm, "12345678");
        }

        internal void DrawImage(ref System.DrawingCore.Bitmap bm, string qrcode)
        {

            Graphics g = null;
            g = Graphics.FromImage(bm);
            float mag = PixelConversions.GetMagnification(g, bm.Width, bm.Height, OptimalHeight, OptimalWidth);

            int barHeight = PixelConversions.PixelXFromMm(g, OptimalHeight * mag);
            int barWidth = PixelConversions.PixelYFromMm(g, OptimalWidth * mag);
            var writer = new ZXing.BarcodeWriterPixelData
            {
                Format = ZXing.BarcodeFormat.EAN_8,
                Options = new QrCodeEncodingOptions
                {
                    Height = barHeight,
                    Width = barWidth,
                    Margin = 0
                }
            };

            writer.Format = ZXing.BarcodeFormat.QR_CODE;
            writer.Options.Height = barHeight;
            writer.Options.Width = barWidth;

            bm = ConvertPixelDataToBitmap(writer.Write(qrcode));

        }

        private Bitmap ConvertPixelDataToBitmap(PixelData pixelData)
        {

            using (var bitmap = new System.DrawingCore.Bitmap(pixelData.Width, pixelData.Height, System.DrawingCore.Imaging.PixelFormat.Format32bppRgb))
            using (var ms = new MemoryStream())
            {
                var bitmapData = bitmap.LockBits(new System.DrawingCore.Rectangle(0, 0, pixelData.Width, pixelData.Height), System.DrawingCore.Imaging.ImageLockMode.WriteOnly, System.DrawingCore.Imaging.PixelFormat.Format32bppRgb);
                try
                {
                    // we assume that the row stride of the bitmap is aligned to 4 byte multiplied by the width of the image   
                    System.Runtime.InteropServices.Marshal.Copy(pixelData.Pixels, 0, bitmapData.Scan0, pixelData.Pixels.Length);
                }
                finally
                {
                    bitmap.UnlockBits(bitmapData);
                }
                // save to stream as PNG   
                bitmap.Save(ms, System.DrawingCore.Imaging.ImageFormat.Jpeg);

                return bitmap;
            }

        }


        public void SetProperties(IDictionary<string, object> props)
        {
            try
            {
                _codeEan8 = props["Code"].ToString();
            }
            catch (KeyNotFoundException)
            {
                throw new Exception("Code property must be specified");
            }
        }

        public object GetPropertiesInstance(XmlNode iNode)
        {
            BarCodeProperties bcp = new BarCodeProperties(this, iNode);
            foreach (XmlNode n in iNode.ChildNodes)
            {
                if (n.Name != "CustomProperty")
                    continue;
                string pname = XmlHelpers.GetNamedElementValue(n, "Name", "");
                switch (pname)
                {
                    case "Code":
                        bcp.SetBarCode(XmlHelpers.GetNamedElementValue(n, "Value", ""));
                        break;
                    default:
                        break;
                }
            }

            return bcp;
        }

        public void SetPropertiesInstance(XmlNode node, object inst)
        {
            node.RemoveAll();       // Get rid of all properties

            BarCodeProperties bcp = inst as BarCodeProperties;
            if (bcp == null)
                return;


            XmlHelpers.CreateChild(node, "Code", bcp.Code);
        }


        /// <summary>
        /// Design time call: return string with <CustomReportItem> ... </CustomReportItem> syntax for 
        /// the insert.  The string contains a variable {0} which will be substituted with the
        /// configuration name.  This allows the name to be completely controlled by
        /// the configuration file.
        /// </summary>
        /// <returns></returns>
        public string GetCustomReportItemXml()
        {
            return "<CustomReportItem><Type>{0}</Type>" +
                string.Format("<Height>{0}mm</Height><Width>{1}mm</Width>", OptimalHeight, OptimalWidth) +
                "<CustomProperties>" +
                "<CustomProperty>" +
                "<Name>Code</Name>" +
                "<Value>00123456</Value>" +
                "</CustomProperty>" +
                "</CustomProperties>" +
                "</CustomReportItem>";
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            return;
        }

        public void DrawImage(ref MemoryStream ms, int height, int weight)
        {            
            //see qr
            DrawImage(ref ms, _codeEan8, height, weight);
            
        }

        internal void DrawImage(ref MemoryStream ms, string qrcode, int height, int weight)
        {
            Bitmap bm = new Bitmap(weight, height);
            Graphics g = null;
            g = Graphics.FromImage(bm);
            float mag = PixelConversions.GetMagnification(g, bm.Width, bm.Height, OptimalHeight, OptimalWidth);

            int barHeight = PixelConversions.PixelXFromMm(g, OptimalHeight * mag);
            int barWidth = PixelConversions.PixelYFromMm(g, OptimalWidth * mag);
            var writer = new ZXing.BarcodeWriterPixelData
            {
                Format = ZXing.BarcodeFormat.EAN_8,
                Options = new QrCodeEncodingOptions
                {
                    Height = barHeight,
                    Width = barWidth,
                    Margin = 0
                }
            };

            writer.Format = ZXing.BarcodeFormat.QR_CODE;
            writer.Options.Height = barHeight;
            writer.Options.Width = barWidth;

            //bm = ConvertPixelDataToBitmap(writer.Write(qrcode));
            ms = ConvertPixelDataToMemoryStream(writer.Write(qrcode));

        }

        private MemoryStream ConvertPixelDataToMemoryStream(PixelData pixelData)
        {

            using (var bitmap = new System.DrawingCore.Bitmap(pixelData.Width, pixelData.Height, System.DrawingCore.Imaging.PixelFormat.Format32bppRgb))
            using (var ms = new MemoryStream())
            {
                var bitmapData = bitmap.LockBits(new System.DrawingCore.Rectangle(0, 0, pixelData.Width, pixelData.Height), System.DrawingCore.Imaging.ImageLockMode.WriteOnly, System.DrawingCore.Imaging.PixelFormat.Format32bppRgb);
                try
                {
                    // we assume that the row stride of the bitmap is aligned to 4 byte multiplied by the width of the image   
                    System.Runtime.InteropServices.Marshal.Copy(pixelData.Pixels, 0, bitmapData.Scan0, pixelData.Pixels.Length);
                }
                finally
                {
                    bitmap.UnlockBits(bitmapData);
                }
                // save to stream as PNG   
                bitmap.Save(ms, System.DrawingCore.Imaging.ImageFormat.Jpeg);

                return ms;
            }

        }

        #endregion

        /// <summary>
        /// BarCodeProperties- All properties are type string to allow for definition of
        /// a runtime expression.
        /// </summary>
        public class BarCodeProperties
        {
            string _codeEan8;
            BarCodeEAN8 _bc;
            XmlNode _node;

            internal BarCodeProperties(BarCodeEAN8 bc, XmlNode node)
            {
                _bc = bc;
                _node = node;
            }

            internal void SetBarCode(string ns)
            {
                _codeEan8 = ns;
            }
            [Category("Code"),
               Description("The text string to be encoded as a BarCodeEAN8 Code.")]
            public string Code
            {
                get { return _codeEan8; }
                set { _codeEan8 = value; _bc.SetPropertiesInstance(_node, this); }
            }


        }
    }
}
