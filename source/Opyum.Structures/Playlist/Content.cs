using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opyum.Structures.Playlist
{
    public class FileContent
    {
        /// <summary>
        /// File <see cref="System.IO.Stream"/> loaded into memory
        /// </summary>
        public IFileFromMemoryStream FileMemoryStream { get; set; }

        /// <summary>
        /// The file path
        /// </summary>
        public string Path { get => FileMemoryStream.FilePath; }


    }
}
