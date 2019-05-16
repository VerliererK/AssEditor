using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AssEditor.Subtitle
{
    /// <summary>
    /// 中研院平衡語料庫詞類標記集 http://ckipsvr.iis.sinica.edu.tw/papers/category_list.pdf
    /// </summary>
    public enum PartOfSpeech
    {
        A,  /*非謂形容詞*/
        Caa,  /*對等連接詞，如：和、跟*/
        Cab,  /*連接詞，如：等等*/
        Cba,  /*連接詞，如：的話*/
        Cbb,  /*關聯連接詞*/
        Da,  /*數量副詞*/
        Dfa,  /*動詞前程度副詞*/
        Dfb,  /*動詞後程度副詞*/
        Di,  /*時態標記*/
        Dk,  /*句副詞*/
        D,  /*副詞*/
        Na,  /*普通名詞*/
        Nb,  /*專有名稱*/
        Nc,  /*地方詞*/
        Ncd,  /*位置詞*/
        Nd,  /*時間詞*/
        Neu,  /*數詞定詞*/
        Nes,  /*特指定詞*/
        Nep,  /*指代定詞*/
        Neqa,  /*數量定詞*/
        Neqb,  /*後置數量定詞*/
        Nf,  /*量詞*/
        Ng,  /*後置詞*/
        Nh,  /*代名詞*/
        Nv,  /*名物化動詞*/
        I,  /*感嘆詞*/
        P,  /*介詞*/
        T,  /*語助詞*/
        VA,  /*動作不及物動詞*/
        VAC,  /*動作使動動詞*/
        VB,  /*動作類及物動詞*/
        VC,  /*動作及物動詞*/
        VCL,  /*動作接地方賓語動詞*/
        VD,  /*雙賓動詞*/
        VE,  /*動作句賓動詞*/
        VF,  /*動作謂賓動詞*/
        VG,  /*分類動詞*/
        VH,  /*狀態不及物動詞*/
        VHC,  /*狀態使動動詞*/
        VI,  /*狀態類及物動詞*/
        VJ,  /*狀態及物動詞*/
        VK,  /*狀態句賓動詞*/
        VL,  /*狀態謂賓動詞*/
        V_2,  /*有*/
        Unknown
    }

    public class Segment
    {
        public string Word;
        public PartOfSpeech POS;

        public Segment(string word, PartOfSpeech pos)
        {
            Word = word;
            POS = pos;
        }
    }

    public class Segmentor
    {
        private static readonly string api_server = "https://120.127.233.228/Segmentor/Func/getSegResult/";
        private static readonly HttpClient client = new HttpClient();
        private static readonly Regex regex_result = new Regex("\\[\\\"(.*?)\",\\\"(.*?)\"\\]");

        public static async Task<List<Segment>> SegmentTextAsync(string text)
        {
            try
            {
                System.Net.ServicePointManager.ServerCertificateValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;

                var content = new StringContent("{\"RawText\":\"" + text + "\"}", System.Text.Encoding.UTF8, "application/json");
                using (var response = await client.PostAsync(api_server, content))
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    if (!response.IsSuccessStatusCode) return null;
                    var matches = regex_result.Matches(Regex.Unescape(responseString).Replace(" ", "").Replace("\n", ""));
                    List<Segment> results = new List<Segment>();
                    foreach (Match m in matches)
                    {
                        if (m.Groups.Count != 3) continue;
                        string word = m.Groups[1].Value;
                        string pos = m.Groups[2].Value;
                        PartOfSpeech POS = PartOfSpeech.Unknown;
                        System.Enum.TryParse(pos, out POS);
                        results.Add(new Segment(word, POS));
                    }
                    return results;
                }
            }
            catch
            {
                return null;
            }
        }

        public static bool IsNoun(PartOfSpeech pos)
        {
            return pos == PartOfSpeech.Na || pos == PartOfSpeech.Nb || pos == PartOfSpeech.Nc || pos == PartOfSpeech.Ncd || pos == PartOfSpeech.Nd;
        }
    }
}
