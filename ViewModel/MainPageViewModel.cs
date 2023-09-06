using CommunityToolkit.Mvvm;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using QRCoder;
using ZXing.QrCode.Internal;

namespace AppGerentes.ViewModel
{
    public partial class MainPageViewModel: ObservableObject
    {
        [ObservableProperty]
        ImageSource qrCodeSource;

        [ObservableProperty]
        string hours = "00";

        [ObservableProperty]
        string minutes = "00";

        [ObservableProperty]
        string seconds = "00";

        [ObservableProperty]
        string qRValue;

        [RelayCommand]
        public void OneHour()
        {
            Hours = "01";
            Minutes = "00";
            Seconds = "00";

            GenerateQRCode();
        }

        [RelayCommand]
        public void TwoHours()
        {
            Hours = "02";
            Minutes = "00";
            Seconds = "00";

            GenerateQRCode();
        }

        [RelayCommand]
        public void ThreeHours()
        {
            Hours = "03";
            Minutes = "00";
            Seconds = "00";

            GenerateQRCode();
        }

        [RelayCommand]
        public void GenerateQRCode()
        {
            //Verify if no one of the numbers are higher than espected

            int intHours = int.Parse(Hours);
            int intMinutes = int.Parse(Minutes);
            int intSeconds = int.Parse(Seconds);

            if (intHours >= 60)
            {
                Hours = "59";
            }

            if (intMinutes >= 60)
            {
                Minutes = "59";
            }

            if (intSeconds >= 60)
            {
                Seconds = "59";
            }

            //Set the value of the QR
            string qrCodeValue = $"{Hours}:{Minutes}:{Seconds}";

            Console.WriteLine(qrCodeValue);

            //Generate the QR
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrCodeValue, QRCodeGenerator.ECCLevel.L);
            PngByteQRCode qRCode = new PngByteQRCode(qrCodeData);
            byte[] qrCodeBytes = qRCode.GetGraphic(20);
            QrCodeSource = ImageSource.FromStream(() => new MemoryStream(qrCodeBytes));
        }
    }
}
