using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace ZhConvert
{
    class Fanhuaji
    {
        public enum ConvertSetting
        {
            Simplified, Traditional, China, Hongkong,
            Taiwan,
            Pinyin, Mars,
            WikiSimplified, WikiTraditional
        }

        class Modules
        {
            enum ModuleValue { enable = 1, disable = 0, auto = -1 }

            //動畫
            ModuleValue Gundam = ModuleValue.disable;
            ModuleValue HunterXHunter = ModuleValue.disable;
            ModuleValue Naruto = ModuleValue.disable;
            ModuleValue OnePiece = ModuleValue.disable;
            ModuleValue Pocketmon = ModuleValue.disable;
            ModuleValue VioletEvergarden = ModuleValue.disable;
            //其他               ModuleValue
            ModuleValue Computer = ModuleValue.auto;
            ModuleValue GanToZuo = ModuleValue.auto;
            ModuleValue InternetSlang = ModuleValue.auto;
            ModuleValue ProperNoun = ModuleValue.auto;
            ModuleValue Punctuation = ModuleValue.auto;
            ModuleValue Repeat = ModuleValue.auto;
            ModuleValue Smooth = ModuleValue.auto;
            ModuleValue TransliterationToTranslation = ModuleValue.auto;
            ModuleValue Typo = ModuleValue.auto;
            ModuleValue Unit = ModuleValue.auto;
            //電視劇           ModuleValue
            ModuleValue Mythbusters = ModuleValue.disable;

            public override string ToString()
            {
                return "{" +
                    "\"Computer\":\"" + (int)Computer + "\"," +
                    "\"GanToZuo\":\"" + (int)GanToZuo + "\"," +
                    "\"Gundam\":\"" + (int)Gundam + "\"," +
                    "\"HunterXHunter\":\"" + (int)HunterXHunter + "\"," +
                    "\"InternetSlang\":\"" + (int)InternetSlang + "\"," +
                    "\"Mythbusters\":\"" + (int)Mythbusters + "\"," +
                    "\"Naruto\":\"" + (int)Naruto + "\"," +
                    "\"OnePiece\":\"" + (int)OnePiece + "\"," +
                    "\"Pocketmon\":\"" + (int)Pocketmon + "\"," +
                    "\"ProperNoun\":\"" + (int)ProperNoun + "\"," +
                    "\"Punctuation\":\"" + (int)Punctuation + "\"," +
                    "\"Repeat\":\"" + (int)Repeat + "\"," +
                    "\"Smooth\":\"" + (int)Smooth + "\"," +
                    "\"TransliterationToTranslation\":\"" + (int)TransliterationToTranslation + "\"," +
                    "\"Typo\":\"" + (int)Typo + "\"," +
                    "\"Unit\":\"" + (int)Unit + "\"," +
                    "\"VioletEvergarden\":\"" + (int)VioletEvergarden + "\"" +
                    "}";
            }
        }

        private static readonly string api_server = "https://api.zhconvert.org/convert";
        private static readonly HttpClient client = new HttpClient();

        private static readonly Regex reg_errorMsg = new Regex("msg\":\"(.*?)\"");
        private static readonly Regex reg_responseText = new Regex("text\":\"(.*?)\"");

        private static async System.Threading.Tasks.Task<string> PostAsync(string text, ConvertSetting convert = ConvertSetting.Taiwan)
        {
            var payload = new Dictionary<string, string>
            {
                { "apiKey", "" },
                { "ignoreTextStyles", "" },  //忽略不被轉換的字幕樣式

                //整理相關
                { "cleanUpText", "0" },  //移除文本無用的內容
                { "trimTrailingWhiteSpaces", "0" },  //移除行末的多餘空格
                { "ensureNewlineAtEof", "0" },  //轉換結果總是以單個空行做為結尾
                { "unifyLeadingHyphen", "0" },  //統一說話人連字號為半形減號
                { "translateTabsToSpaces", "-1" },  //將 Tab 轉換為數個空格

                //差異比較
                { "diffEnable", "0" },  //啟用差異比較
                { "diffCharLevel", "0" },  //啟用精確比較模式
                { "diffIgnoreCase", "0" },  //忽略大小寫的差異
                { "diffIgnoreWhiteSpaces", "0" },  //忽略空格的差異
                { "diffContextLines", "1" },  //前後文行數
                { "diffTemplate", "Inline" },  //輸出樣板
                
                //日文處理策略
                { "jpStyleConversionStrategy", "protect" },
                { "jpTextConversionStrategy", "protectOnlySameOrigin" },
                { "jpTextStyles", "" },

                //自訂取代
                { "userPostReplace", "" },  //轉換後取代
                { "userPreReplace", "" },  //轉換前取代
                { "userProtectReplace", "" },  //保護字詞，不會被轉換
            };

            payload.Add("modules", new Modules().ToString());
            payload.Add("converter", convert.ToString());
            payload.Add("text", text);

            var c = new MultipartFormDataContent();
            foreach (var p in payload)
                c.Add(new StringContent(p.Value), p.Key);
            c.Add(new StringContent(new Modules().ToString()), "modules");
            c.Add(new StringContent(convert.ToString()), "converter");
            c.Add(new StringContent(text), "text");

            //using (var content = new FormUrlEncodedContent(payload))
            using (var response = await client.PostAsync(api_server, c))
            {
                var responseString = await response.Content.ReadAsStringAsync();
                System.Console.WriteLine("Response: " + responseString);

                Match error = reg_errorMsg.Match(responseString);
                if (error.Success && error.Groups[1].Success && !string.IsNullOrEmpty(error.Groups[1].Value))
                {
                    System.Console.WriteLine("Error: " + error.Groups[1].Value);
                }

                Match newText = reg_responseText.Match(responseString);
                return (newText.Success && newText.Groups[1].Success) ? Regex.Unescape(newText.Groups[1].Value) : string.Empty;
            }
        }

        public static async System.Threading.Tasks.Task<string> ToTraditionalAsync(string str)
        {
            return await PostAsync(str);
        }

        public static async System.Threading.Tasks.Task<string> ToSimplifiedAsync(string str)
        {
            return await PostAsync(str, ConvertSetting.Simplified);
        }

        private Fanhuaji() { }
    }
}
