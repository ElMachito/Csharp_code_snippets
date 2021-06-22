//Snippet for screen capture in .BMP then converting it to .JPG



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;//be sure to add the system.drawing and windows forms refrence
using System.Drawing.Imaging;
using System.Windows.Forms;


namespace CaptureScreenshot
{
    class CaptureScreenShot
    {
        static void Main(string[] args)
        {
            //THis code snippet takes a screen shot in BMP then converts it to a JPG before saving in the user's desktop folder
            int screenWidth = Screen.GetBounds(new Point(0, 0)).Width;
            int screenHeight = Screen.GetBounds(new Point(0, 0)).Height;
            Bitmap bmpScreenShot = new Bitmap(screenWidth, screenHeight);
            Graphics gfx = Graphics.FromImage((Image)bmpScreenShot);
            gfx.CopyFromScreen(0, 0, 0, 0, new Size(screenWidth, screenHeight));
            bmpScreenShot.Save(@"C:\SkyRoom.jpg", ImageFormat.Jpeg);//added refrence to the skyroom folder name
        }
    }
