using System.IO;
using System.Text;

namespace AssEditor.Subtitle
{
    public static class EncodingHelper
    {
        /// <summary>
        /// https://en.wikipedia.org/wiki/Byte_order_mark
        /// </summary>
        /// UTF8: EF BB BF
        /// UTF7: 2B 2F 76
        /// UTF16 BE: FE FF
        /// UTF16 LE: FF FE
        /// UTF32 BE: 00 00 FE FF
        /// UTF32 LE: FF FE 00 00
        /// <param name="path"></param>
        /// <returns></returns>
        public static Encoding GetEncoding(string path)
        {
            byte[] buffer = new byte[4];
            using (var file = new FileStream(path, FileMode.Open))
                file.Read(buffer, 0, 4);

            //Detect Encoding By BOM
            if (buffer[0] == 0xEF && buffer[1] == 0xBB && buffer[2] == 0xBF)
                return Encoding.UTF8;  // UTF-8 With BOM
            else if (buffer[0] == 0x2B && buffer[1] == 0x2F && buffer[2] == 0x76)
                return Encoding.UTF7;  // UTF-7 With BOM
            else if (buffer[0] == 0xFF && buffer[1] == 0xFE && buffer[2] == 0x00 && buffer[3] == 0x00)
                return Encoding.UTF32;  //12000 utf-32 Unicode UTF-32, little endian
            else if (buffer[0] == 0xFF && buffer[1] == 0xFE)
                return Encoding.Unicode;  // 1200 utf-16 Unicode UTF-16, little endian
            else if (buffer[0] == 0xFE && buffer[1] == 0xFF)
                return Encoding.BigEndianUnicode;  //1201 unicodeFFFE Unicode UTF-16, big endian
            else if (buffer[0] == 0x00 && buffer[1] == 0x00 && buffer[2] == 0xFE && buffer[3] == 0xFF)
                return Encoding.GetEncoding(12001);  //12001 utf-32BE Unicode UTF-32, big endian
            else
                return GetEncodingWithoutBOM(path);
        }

        /// <summary>
        /// Detect Unicode � ( REPLACEMENT CHARACTER )
        /// Not 100% correct
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private static Encoding GetEncodingWithoutBOM(string path)
        {
            char unknown = (char)65533; //REPLACEMENT CHARACTER
            bool isUTF8 = true;

            using (var sr = new StreamReader(path, Encoding.UTF8))
            {
                string line;
                while ((line = sr.ReadLine()) != null && !isUTF8)
                    foreach (char c in line)
                        if (c == unknown)
                        {
                            isUTF8 = false;
                            break;
                        }
            }

            if (isUTF8) return Encoding.UTF8;
            else if (IsEncoding(path, Encoding.GetEncoding(950))) return Encoding.GetEncoding(950);
            else if (IsEncoding(path, Encoding.GetEncoding(936))) return Encoding.GetEncoding(936);
            else return null;
        }

        //check byte[] encoding by checking length of readByte and GetString
        public static bool IsEncoding(byte[] bytes, Encoding encoding)
        {
            //convert byte[] to string and convert return
            return bytes.Length ==
                encoding.GetByteCount(encoding.GetString(bytes));
        }
        //check file encoding by checking length of readByte and GetString
        public static bool IsEncoding(string file, Encoding encoding)
        {
            if (!File.Exists(file)) return false;
            return IsEncoding(File.ReadAllBytes(file), encoding);
        }

    }
}
