using System;
using Application.Common.Enums;

namespace Application.Common.Helper
{
    public static class FileFormatEnumHelper
    {
        public static string GetFormat(this FileFormats fileFormat)
        {
            switch (fileFormat)
            {

                case FileFormats.Pdf:

                    return ".pdf";

                case FileFormats.Word:

                    return ".docx";

                case FileFormats.Excel:

                    return ".xlsx";

                case FileFormats.PowerPoint:

                    return ".pptx";

                case FileFormats.Image:

                    return ".jpeg";

                default:
                    throw new ArgumentOutOfRangeException(nameof(fileFormat), fileFormat, null);
            }
        }
    }
}
