using Microsoft.AspNetCore.Http;
namespace Edunite.Application.Extension.FormConverter
{
    public interface IByteAndFormConverterExtension
    {
        IFormFile ConvertByteArrayToIFormFile(byte[] fileBytes, string fileName, string contentType);
        Task<byte[]> ConvertIFormFileToByteArray(IFormFile file);
    }
}
