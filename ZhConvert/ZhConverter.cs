namespace ZhConvert
{
    public class ZhConverter
    {
        public enum Method { Kernel32, Fanhuaji }

        private ZhConverter() { }

        public static async System.Threading.Tasks.Task<string> ToTraditional(string str, Method method = Method.Kernel32)
        {
            switch (method)
            {
                case Method.Kernel32:
                    return Kernel32.ToTraditional(str);
                case Method.Fanhuaji:
                    return await Fanhuaji.ToTraditionalAsync(str);
                default:
                    return str;
            }
        }

        public static async System.Threading.Tasks.Task<string> ToSimplified(string str, Method method = Method.Kernel32)
        {
            switch (method)
            {
                case Method.Kernel32:
                    return Kernel32.ToSimplified(str);
                case Method.Fanhuaji:
                    return await Fanhuaji.ToSimplifiedAsync(str);
                default:
                    return str;
            }
        }
    }
}