using Microsoft.AspNetCore.Http;

namespace Edunite.Extension.ByteAndFormConverter
{
    public interface IFormConverterExtension 
    {
        IFormFile ConvertByteArrayToIFormFile(byte[] fileBytes, string fileName, string contentType);
        Task<byte[]> ConvertIFormFileToByteArray(IFormFile file);
    }
}
