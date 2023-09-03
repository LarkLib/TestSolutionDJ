using System.Drawing.Printing;
using ZXing.Common;
using ZXing;
using ZXing.Windows.Compatibility;
using System.Text.Unicode;
using ZXing.QrCode;
using System.Text;
using ThoughtWorks.QRCode.Codec;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        private readonly PrintDocument _printDoc = new PrintDocument();
        private readonly PrintController _controller = new StandardPrintController();
        private readonly Pen _penCross = new Pen(Color.Black, 0.4f);
        private readonly Font _infoFont = new Font("Arial Black", 8, FontStyle.Bold);
        private string _barcode = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            var len = _barcode.Length;
            btnPrint.Text = $"print {len}";
            var bitmap = CreateBarcode(_barcode, 600, 600);
            //var bitmap = CreateQRcode(_barcode);
            bitmap.Save("barcode.bmp");
            pictureBox1.Image = bitmap;
            //_printDoc.Print();
        }

        private void PrintDocMaterial_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            g.PageUnit = GraphicsUnit.Millimeter;

            //g.DrawRectangle(_penCross, 0, 0, 60, 25);

            g.DrawImage(CreateBarcode(_barcode, 200, 100), 10, 2, 38, 18);
            g.DrawString(_barcode, _infoFont, Brushes.Black, new PointF(20, 20));
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //777
            _barcode = @"在线二维码识别解码工具，可以识别解码二维码图片中包含的文本内容，支持截图粘贴快速识别二维码在线识也称二维码在线扫描或二维码在线解析等是指将图像二维码解码并读取转换成文本内容当想知道一个二维码中存储的是在线二维码识别解码工具，可以识别解码二维码图片中包含的文本内容，支持截图粘贴快速识别二维码在线识也称二维码在线扫描或二维码在线解析等是指将图像二维码解码并读取转换成文本内容当想知道一个二维码中存储的是在线二维码识别解码工具，可以识别解码二维码图片中包含的文本内容，支持截图粘贴快速识别二维码在线识也称二维码在线扫描或二维码在线解析等是指将图像二维码解码并读取转换成文本内容当想知道一个二维码中存储的是在线二维码识别解码工具，可以识别解码二维码图片中包含的文本内容，支持截图粘贴快速识别二维码在线识也称二维码在线扫描或二维码在线解析等是指将图像二维码解码并读取转换成文本内容当想知道一个二维码中存储的是在线二维码识别解码工具，可以识别解码二维码图片中包含的文本内容，支持截图粘贴快速识别二维码在线识也称二维码在线扫描或二维码在线解析等是指将图像二维码解码并读取转换成文本内容当想知道一个二维码中存储的是在线二维码识别解码工具，可以识别解码二维码图片中包含的文本内容，支持截图粘贴快速识别二维码在线识也称二维码在线扫描或二维码在线解析等是指将图像二维码解码并读取转换成文本内容当想知道一个二维码中存储的是在线二维码识别解码工具，可以识别解码二维码图片中包含的文本内容，支持截图粘贴快速识别二维码在线识也称二维码在线扫描或二维码在线解析等是指将图像二维码解码并读取转换成文本内容当想知道一个二维码中存储的是码在线扫描或二维码在线解析等是指将图像二维码解码并读取转换成文本内容当想知道一个二维码中存储的是的道码在线扫描或二维码在线解析等是指将图像二维工了有地一上";
            //2331
            /*
            _barcode = @"
abcdefghigklmnopqrstuvwxyzabcdefghigklmnopqrstuvwxyzabcdefghigklmnopqrstuvwxyzabcdefghigklmnopqrstuv
abcdefghigklmnopqrstuvwxyzabcdefghigklmnopqrstuvwxyzabcdefghigklmnopqrstuvwxyzabcdefghigklmnopqrstuv
abcdefghigklmnopqrstuvwxyzabcdefghigklmnopqrstuvwxyzabcdefghigklmnopqrstuvwxyzabcdefghigklmnopqrstuv
abcdefghigklmnopqrstuvwxyzabcdefghigklmnopqrstuvwxyzabcdefghigklmnopqrstuvwxyzabcdefghigklmnopqrstuv
abcdefghigklmnopqrstuvwxyzabcdefghigklmnopqrstuvwxyzabcdefghigklmnopqrstuvwxyzabcdefghigklmnopqrstuv
abcdefghigklmnopqrstuvwxyzabcdefghigklmnopqrstuvwxyzabcdefghigklmnopqrstuvwxyzabcdefghigklmnopqrstuv
abcdefghigklmnopqrstuvwxyzabcdefghigklmnopqrstuvwxyzabcdefghigklmnopqrstuvwxyzabcdefghigklmnopqrstuv
abcdefghigklmnopqrstuvwxyzabcdefghigklmnopqrstuvwxyzabcdefghigklmnopqrstuvwxyzabcdefghigklmnopqrstuv
abcdefghigklmnopqrstuvwxyzabcdefghigklmnopqrstuvwxyzabcdefghigklmnopqrstuvwxyzabcdefghigklmnopqrstuv
abcdefghigklmnopqrstuvwxyzabcdefghigklmnopqrstuvwxyzabcdefghigklmnopqrstuvwxyzabcdefghigklmnopqrstuv
abcdefghigklmnopqrstuvwxyzabcdefghigklmnopqrstuvwxyzabcdefghigklmnopqrstuvwxyzabcdefghigklmnopqrstuv
abcdefghigklmnopqrstuvwxyzabcdefghigklmnopqrstuvwxyzabcdefghigklmnopqrstuvwxyzabcdefghigklmnopqrstuv
abcdefghigklmnopqrstuvwxyzabcdefghigklmnopqrstuvwxyzabcdefghigklmnopqrstuvwxyzabcdefghigklmnopqrstuv
abcdefghigklmnopqrstuvwxyzabcdefghigklmnopqrstuvwxyzabcdefghigklmnopqrstuvwxyzabcdefghigklmnopqrstuv
abcdefghigklmnopqrstuvwxyzabcdefghigklmnopqrstuvwxyzabcdefghigklmnopqrstuvwxyzabcdefghigklmnopqrstuv
abcdefghigklmnopqrstuvwxyzabcdefghigklmnopqrstuvwxyzabcdefghigklmnopqrstuvwxyzabcdefghigklmnopqrstuv
abcdefghigklmnopqrstuvwxyzabcdefghigklmnopqrstuvwxyzabcdefghigklmnopqrstuvwxyzabcdefghigklmnopqrstuv
abcdefghigklmnopqrstuvwxyzabcdefghigklmnopqrstuvwxyzabcdefghigklmnopqrstuvwxyzabcdefghigklmnopqrstuv
abcdefghigklmnopqrstuvwxyzabcdefghigklmnopqrstuvwxyzabcdefghigklmnopqrstuvwxyzabcdefghigklmnopqrstuv
abcdefghigklmnopqrstuvwxyzabcdefghigklmnopqrstuvwxyzabcdefghigklmnopqrstuvwxyzabcdefghigklmnopqrstuv
abcdefghigklmnopqrstuvwxyzabcdefghigklmnopqrstuvwxyzabcdefghigklmnopqrstuvwxyzabcdefghigklmnopqrstuv
abcdefghigklmnopqrstuvwxyzabcdefghigklmnopqrstuvwxyzabcdefghigklmnopqrstuvwxyzabcdefghigklmnopqrstuv
xyzabcdefghigklmnopqrstuvwxyzabcdefghigklmnopqrstuvxyzabcdefghiaaaaaaaa
            ";//*/
            _printDoc.PrintPage += PrintDocMaterial_PrintPage;
            _printDoc.PrintController = _controller;
        }

        public static Bitmap CreateBarcode(string barcode, int width, int height)
        {
            EncodingOptions options = null;
            options = new QrCodeEncodingOptions
            {
                PureBarcode = true,
                Width = width,
                Height = height,
                CharacterSet = "UTF-8"
            };

            BarcodeWriter writer = new BarcodeWriter
            {
                Format = BarcodeFormat.QR_CODE,
                Options = options
            };
            return writer.Write(barcode);
        }

        public Bitmap CreateQRcode(string code)
        {
            QRCodeEncoder encoder = new QRCodeEncoder
            {
                QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE,
                QRCodeScale = 2,
                QRCodeVersion = 0,
                QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M
            };
            return encoder.Encode(code, Encoding.GetEncoding("UTF-8"));
        }
    }
}