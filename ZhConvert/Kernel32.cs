using System.Runtime.InteropServices;

namespace ZhConvert
{
    class Kernel32
    {
        private const int LocaleSystemDefault = 0x0800;
        private const int LcmapSimplifiedChinese = 0x02000000;
        private const int LcmapTraditionalChinese = 0x04000000;

        [DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int LCMapString(int locale, int dwMapFlags, string lpSrcStr, int cchSrc,
                                              [Out] string lpDestStr, int cchDest);

        public static string ToTraditional(string str)
        {
            var t = new string(' ', str.Length);
            LCMapString(LocaleSystemDefault, LcmapTraditionalChinese, str, str.Length, t, str.Length);
            return t;
        }

        public static string ToSimplified(string str)
        {
            var t = new string(' ', str.Length);
            LCMapString(LocaleSystemDefault, LcmapSimplifiedChinese, str, str.Length, t, str.Length);
            return t;
        }

        private Kernel32() { }
    }
}
