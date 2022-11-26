using iTextSharp.text;
using iTextSharp.text.pdf;
using PdfSharpCore.Pdf.IO;

//SplitPagesITextSharp();
//MergePagesITextSharp();
//AddQrCode();


void MergePagesITextSharp()
{
    Document document = new Document();

    //Step 2: we create a writer that listens to the document
    PdfWriter writer = PdfWriter.GetInstance(document, new FileStream("Aaa.pdf", FileMode.Create));

    //Step 3: Open the document
    document.Open();

    PdfContentByte cb = writer.DirectContent;

    // we create a reader for the document
    iTextSharp.text.pdf.PdfReader reader1 = new iTextSharp.text.pdf.PdfReader("X.pdf");
    iTextSharp.text.pdf.PdfReader reader2 = new iTextSharp.text.pdf.PdfReader("X.pdf");

    document.SetPageSize(new iTextSharp.text.Rectangle(0, 0, 1191, 842));
    document.NewPage();

    var page1 = writer.GetImportedPage(reader1, 1);

    var page2 = writer.GetImportedPage(reader2, 2);

    cb.AddTemplate(page1, 0, 0);
    //play around to find the exact location for the next pdf
    cb.AddTemplate(page2, 1191 / 2, 0);

    document.Close();
}

void SplitPagesITextSharp()
{
    Document document = new Document(PageSize.A4);
    PdfWriter writer = PdfWriter.GetInstance(document, new FileStream("proba.pdf", FileMode.Create));

    document.Open();

    PdfContentByte cb = writer.DirectContent;

    iTextSharp.text.pdf.PdfReader reader = new iTextSharp.text.pdf.PdfReader("Aaa.pdf");
    iTextSharp.text.pdf.PdfImportedPage page = writer.GetImportedPage(reader, 1);

    document.NewPage();
    cb.AddTemplate(page, 0, 0);

    document.NewPage();
    cb.AddTemplate(page, -PageSize.A4.Width, 0);

    document.Close();
}

void AddQrCode()
{
    string originalPdf = @"XMinus1.pdf";
    System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

    PdfSharpCore.Pdf.PdfDocument PDFDoc = PdfSharpCore.Pdf.IO.PdfReader.Open(originalPdf, PdfDocumentOpenMode.Import);
    PdfSharpCore.Pdf.PdfDocument PDFNewDoc = new PdfSharpCore.Pdf.PdfDocument();

    for (int Pg = 0; Pg < PDFDoc.Pages.Count; Pg++)
    {
        PdfSharpCore.Pdf.PdfPage pp = PDFNewDoc.AddPage(PDFDoc.Pages[Pg]);
        PdfSharpCore.Drawing.XGraphics gfx = PdfSharpCore.Drawing.XGraphics.FromPdfPage(pp);
        PdfSharpCore.Drawing.XImage xImage = PdfSharpCore.Drawing.XImage.FromFile("qr.png");
        gfx.DrawImage(xImage, 0, 0, 30, 30);
        // gfx.DrawString("Hello", font, XBrushes.Black, new XRect(0, 0, pp.Width, pp.Height), XStringFormats.BottomRight);
    }

    PDFNewDoc.Save(@"XMinus1QrCodeAdded.pdf");
}


//PDFSHARP MERGE
/*
void TwoPagesOnOnePage()
{
    Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

    string filename = "X.pdf";
    PdfDocument outputDocument = new PdfDocument();

    outputDocument.PageLayout = PdfPageLayout.SinglePage;

    XStringFormat format = new XStringFormat();
    format.Alignment = XStringAlignment.Center;
    format.LineAlignment = XLineAlignment.Far;

    XPdfForm form = XPdfForm.FromFile(filename);
    form.PageNumber = 1;
    PdfPage page = outputDocument.AddPage(form.Page);
    page.Size = PageSize.A3;
    page.Orientation = PageOrientation.Landscape;
    form.PageNumber = form.PageCount;
    var gfx = XGraphics.FromPdfPage(page);
    
    var box = new XRect(page.Width / 2, 0, page.Width / 2, page.Height);
    gfx.DrawImage(form, box);
    *//*XImage xImage = XImage.FromFile("Slika.jpg");
    gfx.DrawImage(xImage, page.Width / 4, 0, page.Width / 4, page.Height);*//*

    for (int idx = 2; idx <= form.PageCount - 1; idx++)
    {
        form.PageNumber = idx;

        PdfPage xpage = outputDocument.AddPage(form.Page);
    }

    outputDocument.Save("XMinus1.pdf");
}

*/

