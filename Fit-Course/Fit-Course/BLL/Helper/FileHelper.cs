using DAL.Enum.fileType;


namespace BLL.Helper
{
    public class FileHelper
    {
        public static FileType? GetFileTypeFromPath(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                return null;

            string extension = Path.GetExtension(filePath)?.ToLower();

            return extension switch
            {
                ".pdf" => FileType.pdf,
                ".pptx" => FileType.pptx,
                ".doc" => FileType.doc,
                ".docx" => FileType.docx,
                ".txt" => FileType.txt,
                ".rtf" => FileType.rtf,
                ".odt" => FileType.odt,
                ".xlsx" => FileType.xlsx,
                ".csv" => FileType.csv,
                _ => null
            };
        }
    }
}
