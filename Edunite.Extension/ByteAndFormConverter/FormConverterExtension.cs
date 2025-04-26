using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;

namespace Edunite.Extension.ByteAndFormConverter
{
    public class FormConverterExtension
    {
        public IFormFile ConvertByteArrayToIFormFile(byte[] fileBytes, string fileName, string contentType)
        {
            var stream = new MemoryStream(fileBytes);
            return new FormFile(stream, 0, fileBytes.Length, "file", fileName)
            {
                Headers = new HeaderDictionary(),
                ContentType = contentType
            };
        }
        public async Task<byte[]> ConvertIFormFileToByteArray(IFormFile file)
        {
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}
