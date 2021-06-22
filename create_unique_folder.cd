//Snippet for creating unique named folder for the logon users desktop



using System;
using System.IO; //must have this

class DirectoryCreation
{
    public static void Main()
    {


        string SkyRoomFolder = string.Format("{0:yyyyMMdd_HHmmss}", DateTime.Now);
        //The GetFolder Path method can be used to find any system or special folder.  
        string directoryString = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + (@"\SkyRoomSnapshot" + SkyRoomFolder);

        Directory.CreateDirectory(directoryString);



    }
}
