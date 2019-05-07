using System.IO;

namespace Opyum.Structures
{
    public interface IFileFromMemoryStream
    {
        bool BufferEmpty { get; }
        BufferingStatus BufferingState { get; }
        FileInfo FileInformation { get; set; }
        string FilePath { get; }
        long Length { get; }
        double PositionPercentage { get; set; }
        long Position { get; set; }

        void Dispose();
        byte[] GetBuffer();
        int Read(byte[] buffer, int offset, int count);
    }
}