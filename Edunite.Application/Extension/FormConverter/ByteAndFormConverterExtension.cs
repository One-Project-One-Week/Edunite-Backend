using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Http;

namespace Edunite.Application.Extension.FormConverter
{
    public class ByteAndFormConverterExtension : IByteAndFormConverterExtension
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
