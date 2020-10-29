using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Application.Common.Models
{

    public class FileUploadModel
    {
        [JsonIgnore]
        public IFormFile FormFile { get; set; }
        public int RequiredDocumentId { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public bool Mandatory { get; set; }
        public string FileDescription { get; set; }
        public string UniqueFileName { get; set; }
        public string ContentType { get; set; }

    }
}
