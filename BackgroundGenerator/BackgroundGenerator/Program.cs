using System.Runtime.InteropServices;

namespace BackgroundGenerator
{
     class backgroundChanger
     {//Constants used as parameters for the SystemParametersInfo function 
        public const int SPI_SETDESKWALLPAPER = 20; // Used to set wallpaper
        public const int SPIF_UPDATEINFILE = 1; //Indicates that the change should be written to the user profile 
        public const int SPIF_SENDCHANGE = 2; // Broadcasts the WM_SETTINGCHANGE message after updating the uder profile.
        //importing dll
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)] //string marshalling, getting ready for storage, SetLastError ; indicating whether the colleage sets the error.
        public static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni); //using spi to modify certain parts about our user profile.
        static void Main(String[] args)
        {
            Random random = new Random();
            int index = random.Next(0, 5);
            String[] imageNames = { "1.jpg", "2.jpg", "3.jpg", "4.jpg", "5.jpg" };
            String imagePath = @"C:\Users\psala\OneDrive\Pictures\Camera Roll\BackgroundWallPaperResolution\" + imageNames[index];

            //Set wallpaper
            SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, imagePath, SPIF_UPDATEINFILE | SPIF_SENDCHANGE); //using entire function with all parameters and image paths to change background 
        }
     }
}