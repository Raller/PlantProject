using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace PlantApp.Utils
{
    public class Camera
    {

        public static async Task<string> TakePhotoAsync()
        {

            try
            {
                var photo = await MediaPicker.CapturePhotoAsync();
                return await LoadPhotoAsync(photo);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"CapturePhotoAsync THREW: {ex.Message}");
                return null;
            }
        }

        private async static Task<string> LoadPhotoAsync(FileResult photo)
        {
            // canceled
            if (photo == null)
            {
                return null;
            }
            // save the file into local storage
            var newFile = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
            using (var stream = await photo.OpenReadAsync())
            using (var newStream = File.OpenWrite(newFile))
                await stream.CopyToAsync(newStream);

            return newFile;
        }

    }
}
