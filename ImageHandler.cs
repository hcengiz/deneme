using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Drawing;
using System.Net;


public class ImageHandler : IHttpHandler
{
    // the max size of the Thumbnail
    const int MaxDim = 120;
    public string path = "";
    public int width, height = 0;

    public void ProcessRequest(HttpContext ctx)
    {
        // let's cache this for 1 day//no cache ŞY
        ctx.Response.ContentType = "image/jpeg";
        //ctx.Response.Cache.SetCacheability(HttpCacheability.Public);
        //ctx.Response.Cache.SetExpires(DateTime.Now.AddDays(1));

        // find the directory where we're storing the images
        //string imageDir = ConfigurationSettings.AppSettings["imageDir"];
        //imageDir = Path.Combine(
        //    ctx.Request.PhysicalApplicationPath, imageDir);

        // find the image that was requested
        //string file = ctx.Request.PhysicalApplicationPath + path;
        string file = path;
        //     file = Path.Combine(imageDir, file);
        // load it up
        using (System.Drawing.Image img = new Bitmap(file))
        {
            // do some math to resize the image
            // note: i know very little about image resizing,
            // but this just seemed to work under v1.1. I think
            // under v2.0 the images look incorrect.
            // *note to self* fix this for v2.0
            //int h = img.Height;
            //int w = img.Width;
            //int b = h > w ? h : w;
            //double per = (b > MaxDim) ? (MaxDim * 1.0) / b : 1.0;
            //h = 136;//(int)(h * per);
            //w = 184;//(int)(w * per);

            // create the thumbnail image
            using (System.Drawing.Image img2 =
                      img.GetThumbnailImage(width, height,
                      new System.Drawing.Image.GetThumbnailImageAbort(Abort),
                      IntPtr.Zero))
            {
                // emit it to the response strea,
                img2.Save(ctx.Response.OutputStream,
                    System.Drawing.Imaging.ImageFormat.Jpeg);
            }
        }
    }

    public bool IsReusable
    {
        get { return true; }
    }

    private bool Abort()
    {
        return false;
    }

    public static void ResizeAndSaveImage(string filename, string extension, int width, int height, int NewWidth, int NewHeight, string KeyWord)
    {
        
        System.Drawing.Bitmap DestImage = new System.Drawing.Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
        System.Drawing.Graphics.FromImage(DestImage).DrawImage(System.Drawing.Image.FromStream(new System.Net.WebClient().OpenRead(filename + extension)),
            new System.Drawing.Rectangle(0, 0, width, height),
            new System.Drawing.Rectangle(0, 0, width, height),
            System.Drawing.GraphicsUnit.Pixel);
        System.Drawing.Bitmap imgOutput = new System.Drawing.Bitmap(DestImage, NewWidth, NewHeight);
        imgOutput.Save(filename + KeyWord + extension, System.Drawing.Imaging.ImageFormat.Jpeg);
    }

    //public static void ResizeAndSaveImage(string filename, string extension, int width, int height, int NewWidth, int NewHeight, string KeyWord)
    //{
    //    System.Drawing.Bitmap DestImage = new System.Drawing.Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
    //    System.Drawing.Graphics.FromImage(DestImage).DrawImage(System.Drawing.Image.FromStream(new System.Net.WebClient().OpenRead(filename + extension)),
    //        new System.Drawing.Rectangle(0, 0, width, height),
    //        new System.Drawing.Rectangle(0, 0, width, height),
    //        System.Drawing.GraphicsUnit.Pixel);
    //    System.Drawing.Bitmap imgOutput = new System.Drawing.Bitmap(DestImage, NewWidth, NewHeight);
    //    imgOutput.Save(filename + KeyWord + extension, System.Drawing.Imaging.ImageFormat.Jpeg);
    //}

    public static void ResizeAndSaveImageForFTP(string filename, string extension, int width, int height, int NewWidth, int NewHeight, string KeyWord)
    {//string fileFullName,
        System.Drawing.Bitmap DestImage = new System.Drawing.Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
        string ftpAdres = "ftp://10.254.1.113/test";
        FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpAdres + "/" + filename + extension);
        request.Method = WebRequestMethods.Ftp.DownloadFile;
        request.Credentials = new NetworkCredential("Syildiz", "s009");
        using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
        {
            Stream stream = response.GetResponseStream();
            System.Drawing.Graphics.FromImage(DestImage).DrawImage(System.Drawing.Image.FromStream(stream),
                new System.Drawing.Rectangle(0, 0, width, height),
                new System.Drawing.Rectangle(0, 0, width, height),
                System.Drawing.GraphicsUnit.Pixel);
            System.Drawing.Bitmap imgOutput = new System.Drawing.Bitmap(DestImage, NewWidth, NewHeight);
            // imgOutput.Save(System.Web.HttpContext.Current.Request.PhysicalApplicationPath+"temp"+filename.Replace("/","\\") + KeyWord + extension, System.Drawing.Imaging.ImageFormat.Jpeg);
            //  Stream bitmapStream = new 
            imgOutput.Save(ftpAdres + filename + KeyWord + extension, System.Drawing.Imaging.ImageFormat.Jpeg);
            //CopyImage(ftpAdres, filename + extension, System.Web.HttpContext.Current.Request.PhysicalApplicationPath + "/temp/" + filename + KeyWord + extension);
        }

    }

    private static void CopyImage(string ftpAdres, string fileFullName, string sourcefile)
    {
        FtpWebRequest request = (FtpWebRequest)WebRequest.Create(fileFullName);
        request.Method = WebRequestMethods.Ftp.UploadFile;
        request.Credentials = new NetworkCredential("Syildiz", "s009");
        using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
        {

            Stream stream = request.GetRequestStream();
            FileStream file = File.OpenRead(sourcefile);

            int length = 1024;
            byte[] buffer = new byte[length];
            int bytesRead = 0;

            do
            {
                bytesRead = file.Read(buffer, 0, length);
                stream.Write(buffer, 0, bytesRead);
            }
            while (bytesRead != 0);

            file.Close();
            stream.Close();
        }
    }

    #region IHttpHandler Members

    bool IHttpHandler.IsReusable
    {
        get { throw new Exception("The method or operation is not implemented."); }
    }

    void IHttpHandler.ProcessRequest(HttpContext context)
    {
        throw new Exception("The method or operation is not implemented.");
    }

    #endregion
}
