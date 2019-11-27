using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace ComPortTest
{
    public class CultureInfoCollection : System.Collections.CollectionBase
    {
        private CultureInfoClass[] _cultureInfoClass;
        
        public CultureInfoCollection()
        {
            _cultureInfoClass = new CultureInfoClass[0];

            InsertKnownCultures();
        }

        ~CultureInfoCollection()
        {
            Clear();
        }

        public CultureInfoClass this[int index]
        {
            get { return (CultureInfoClass)_cultureInfoClass[index]; }
        }

        public int Add(object value)
        {
            Array.Resize(ref _cultureInfoClass, _cultureInfoClass.Length + 1);
            _cultureInfoClass[_cultureInfoClass.Length - 1] = (CultureInfoClass)value;

            return Count() -1;
        }

        public int Add(CultureInfoClass culture)
        {
            return Add((object)culture);
        }

        public int Add(string name, int identifier, string culture)
        {
            CultureInfoClass c = new CultureInfoClass();

            c.Index = Count();
            c.Name = name;
            c.Identifier = identifier;
            c.Culture = culture;
            return Add(c);
        }

        public bool Contains(object value)
        {
            return FindByCultureInfoClass ((CultureInfoClass)value) != null;
        }

        public int IndexOf(object value)
        {
            CultureInfoClass c = FindByCultureInfoClass((CultureInfoClass)value);

            if (c != null)
            {
                return c.Index;
            } else 
            {
                return -1;
            }
        }

        public void Remove(object value)
        {
            RemoveAt(IndexOf(value));
        }

        public new void RemoveAt(int index)
        {
            if ((index >= 0) && (index < Count()))
            {
                Array.Clear(_cultureInfoClass, index, 1);
            }
        }
        
        public void Remove(CultureInfoClass culture)
        {
            CultureInfoClass c = FindByCultureInfoClass(culture);

            if (c != null)
            {
                Array.Clear(_cultureInfoClass, c.Index, 1);
            }
        }

        public bool IsFixedSize
        {
            get { return true; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public void Insert(int index, object value)
        {
            throw new Exception("The method INSERT is not implemented.");
        }

        public void CopyTo(Array array, int index)
        {
            throw new Exception("The method COPYTU is not implemented.");
        }

        public bool IsSynchronized
        {
            get { return false; }
        }

        public object SyncRoot
        {
            get { return this; }
        }

        public new void Clear()
        {
            Array.Clear(_cultureInfoClass, 0, _cultureInfoClass.Length);
        }

        public new int Count()
        {
            return _cultureInfoClass.Length;
        }

        public CultureInfoClass FindByCultureInfoClass(CultureInfoClass culture)
        {
            CultureInfoClass c;

            for (int i = 0; i < _cultureInfoClass.Length; i++)
            {
                c = _cultureInfoClass[i];

                if (c.Name.ToLower().Trim().Equals(culture.Name.ToLower().Trim()) &&
                    c.Identifier == culture.Identifier &&
                    c.Culture.ToLower().Trim().Equals(culture.Culture.ToLower().Trim()))
                {
                    return c;
                }
            }

            return null;
        }

        public CultureInfoClass FindByName(string name)
        {
            foreach (CultureInfoClass c in _cultureInfoClass)
            {
                if (name.ToLower().Trim().Equals(c.Name.ToLower().Trim()))
                {
                    return c;
                }
            }

            return null;
        }

        public CultureInfoClass FindByIdentifier(int identifier)
        {
            foreach (CultureInfoClass c in _cultureInfoClass)
            {
                if (identifier == c.Identifier)
                {
                    return c;
                }
            }

            return null;
        }

        public CultureInfoClass FindByCulture(string culture)
        {
            foreach (CultureInfoClass c in _cultureInfoClass)
            {
                if (culture.ToLower().Trim().Equals(c.Culture.ToLower().Trim()))
                {
                    return c;
                }
            }

            return null;
        }

        public CultureInfoClass FindByIndex(int index)
        {
            foreach (CultureInfoClass c in _cultureInfoClass)
            {
                if (index == c.Index)
                {
                    return c;
                }
            }

            return null;
        }

        private void InsertKnownCultures()
        {
            Add("", 0x007F, "Invariant culture");
            Add("af", 0x0036, "Afrikaans");
            Add("af-ZA", 0x0436, "Afrikaans (South Africa)");
            Add("sq", 0x001C, "Albanian");
            Add("sq-AL", 0x041C, "Albanian (Albania)");
            Add("ar", 0x0001, "Arabic");
            Add("ar-DZ", 0x1401, "Arabic (Algeria)");
            Add("ar-BH", 0x3C01, "Arabic (Bahrain)");
            Add("ar-EG", 0x0C01, "Arabic (Egypt)");
            Add("ar-IQ", 0x0801, "Arabic (Iraq)");
            Add("ar-JO", 0x2C01, "Arabic (Jordan)");
            Add("ar-KW", 0x3401, "Arabic (Kuwait)");
            Add("ar-LB", 0x3001, "Arabic (Lebanon)");
            Add("ar-LY", 0x1001, "Arabic (Libya)");
            Add("ar-MA", 0x1801, "Arabic (Morocco)");
            Add("ar-OM", 0x2001, "Arabic (Oman)");
            Add("ar-QA", 0x4001, "Arabic (Qatar)");
            Add("ar-SA", 0x0401, "Arabic (Saudi Arabia)");
            Add("ar-SY", 0x2801, "Arabic (Syria)");
            Add("ar-TN", 0x1C01, "Arabic (Tunisia)");
            Add("ar-AE", 0x3801, "Arabic (U.A.E.)");
            Add("ar-YE", 0x2401, "Arabic (Yemen)");
            Add("hy", 0x002B, "Armenian");
            Add("hy-AM", 0x042B, "Armenian (Armenia)");
            Add("az", 0x002C, "Azeri");
            Add("az-Cyrl-AZ", 0x082C, "Azeri (Azerbaijan, Cyrillic)");
            Add("az-Latn-AZ", 0x042C, "Azeri (Azerbaijan, Latin)");
            Add("eu", 0x002D, "Basque");
            Add("eu-ES", 0x042D, "Basque (Basque)");
            Add("be", 0x0023, "Belarusian");
            Add("be-BY", 0x0423, "Belarusian (Belarus)");
            Add("bg", 0x0002, "Bulgarian");
            Add("bg-BG", 0x0402, "Bulgarian (Bulgaria)");
            Add("ca", 0x0003, "Catalan");
            Add("ca-ES", 0x0403, "Catalan (Catalan)");
            Add("zh-HK", 0x0C04, "Chinese (Hong Kong SAR, PRC)");
            Add("zh-MO", 0x1404, "Chinese (Macao SAR)");
            Add("zh-CN", 0x0804, "Chinese (PRC)");
            Add("zh-Hans", 0x0004, "Chinese (Simplified)");
            Add("zh-SG", 0x1004, "Chinese (Singapore)");
            Add("zh-TW", 0x0404, "Chinese (Taiwan)");
            Add("zh-Hant", 0x7C04, "Chinese (Traditional)");
            Add("hr", 0x001A, "Croatian");
            Add("hr-HR", 0x041A, "Croatian (Croatia)");
            Add("cs", 0x0005, "Czech");
            Add("cs-CZ", 0x0405, "Czech (Czech Republic)");
            Add("da", 0x0006, "Danish");
            Add("da-DK", 0x0406, "Danish (Denmark)");
            Add("dv", 0x0065, "Divehi");
            Add("dv-MV", 0x0465, "Divehi (Maldives)");
            Add("nl", 0x0013, "Dutch");
            Add("nl-BE", 0x0813, "Dutch (Belgium)");
            Add("nl-NL", 0x0413, "Dutch (Netherlands)");
            Add("en", 0x0009, "English");
            Add("en-AU", 0x0C09, "English (Australia)");
            Add("en-BZ", 0x2809, "English (Belize)");
            Add("en-CA", 0x1009, "English (Canada)");
            Add("en-029", 0x2409, "English (Caribbean)");
            Add("en-IE", 0x1809, "English (Ireland)");
            Add("en-JM", 0x2009, "English (Jamaica)");
            Add("en-NZ", 0x1409, "English (New Zealand)");
            Add("en-PH", 0x3409, "English (Philippines)");
            Add("en-ZA", 0x1C09, "English (South Africa");
            Add("en-TT", 0x2C09, "English (Trinidad and Tobago)");
            Add("en-GB", 0x0809, "English (United Kingdom)");
            Add("en-US", 0x0409, "English (United States)");
            Add("en-ZW", 0x3009, "English (Zimbabwe)");
            Add("et", 0x0025, "Estonian");
            Add("et-EE", 0x0425, "Estonian (Estonia)");
            Add("fo", 0x0038, "Faroese");
            Add("fo-FO", 0x0438, "Faroese (Faroe Islands)");
            Add("fi", 0x000B, "Finnish");
            Add("fi-FI", 0x040B, "Finnish (Finland)");
            Add("fr", 0x000C, "French");
            Add("fr-BE", 0x080C, "French (Belgium)");
            Add("fr-CA", 0x0C0C, "French (Canada)");
            Add("fr-FR", 0x040C, "French (France)");
            Add("fr-LU", 0x140C, "French (Luxembourg)");
            Add("fr-MC", 0x180C, "French (Monaco)");
            Add("fr-CH", 0x100C, "French (Switzerland)");
            Add("gl", 0x0056, "Galician");
            Add("gl-ES", 0x0456, "Galician (Spain)");
            Add("ka", 0x0037, "Georgian");
            Add("ka-GE", 0x0437, "Georgian (Georgia)");
            Add("de", 0x0007, "German");
            Add("de-AT", 0x0C07, "German (Austria)");
            Add("de-DE", 0x0407, "German (Germany)");
            Add("de-LI", 0x1407, "German (Liechtenstein)");
            Add("de-LU", 0x1007, "German (Luxembourg)");
            Add("de-CH", 0x0807, "German (Switzerland)");
            Add("el", 0x0008, "Greek");
            Add("el-GR", 0x0408, "Greek (Greece)");
            Add("gu", 0x0047, "Gujarati");
            Add("gu-IN", 0x0447, "Gujarati (India)");
            Add("he", 0x000D, "Hebrew");
            Add("he-IL", 0x040D, "Hebrew (Israel)");
            Add("hi", 0x0039, "Hindi");
            Add("hi-IN", 0x0439, "Hindi (India)");
            Add("hu", 0x000E, "Hungarian");
            Add("hu-HU", 0x040E, "Hungarian (Hungary)");
            Add("is", 0x000F, "Icelandic");
            Add("is-IS", 0x040F, "Icelandic (Iceland)");
            Add("id", 0x0021, "Indonesian");
            Add("id-ID", 0x0421, "Indonesian (Indonesia)");
            Add("it", 0x0010, "Italian");
            Add("it-IT", 0x0410, "Italian (Italy)");
            Add("it-CH", 0x0810, "Italian (Switzerland)");
            Add("ja", 0x0011, "Japanese");
            Add("ja-JP", 0x0411, "Japanese (Japan)");
            Add("kn", 0x004B, "Kannada");
            Add("kn-IN", 0x044B, "Kannada (India)");
            Add("kk", 0x003F, "Kazakh");
            Add("kk-KZ", 0x043F, "Kazakh (Kazakhstan)");
            Add("kok", 0x0057, "Konkani");
            Add("kok-IN", 0x0457, "Konkani (India)");
            Add("ko", 0x0012, "Korean");
            Add("ko-KR", 0x0412, "Korean (Korea)");
            Add("ky", 0x0040, "Kyrgyz");
            Add("ky-KG", 0x0440, "Kyrgyz (Kyrgyzstan)");
            Add("lv", 0x0026, "Latvian");
            Add("lv-LV", 0x0426, "Latvian (Latvia)");
            Add("lt", 0x0027, "Lithuanian");
            Add("lt-LT", 0x0427, "Lithuanian (Lithuania)");
            Add("mk", 0x002F, "Macedonian");
            Add("mk-MK", 0x042F, "Macedonian (Macedonia, FYROM)");
            Add("ms", 0x003E, "Malay");
            Add("ms-BN", 0x083E, "Malay (Brunei Darussalam)");
            Add("ms-MY", 0x043E, "Malay (Malaysia)");
            Add("mr", 0x004E, "Marathi");
            Add("mr-IN", 0x044E, "Marathi (India)");
            Add("mn", 0x0050, "Mongolian");
            Add("mn-MN", 0x0450, "Mongolian (Mongolia)");
            Add("no", 0x0014, "Norwegian");
            Add("nb-NO", 0x0414, "Norwegian (Bokmål, Norway)");
            Add("nn-NO", 0x0814, "Norwegian (Nynorsk, Norway)");
            Add("fa", 0x0029, "Persian");
            Add("fa-IR", 0x0429, "Persian (Iran)");
            Add("pl", 0x0015, "Polish");
            Add("pl-PL", 0x0415, "Polish (Poland)");
            Add("pt", 0x0016, "Portuguese");
            Add("pt-BR", 0x0416, "Portuguese (Brazil)");
            Add("pt-PT", 0x0816, "Portuguese (Portugal)");
            Add("pa", 0x0046, "Punjabi");
            Add("pa-IN", 0x0446, "Punjabi (India)");
            Add("ro", 0x0018, "Romanian");
            Add("ro-RO", 0x0418, "Romanian (Romania)");
            Add("ru", 0x0019, "Russian");
            Add("ru-RU", 0x0419, "Russian (Russia)");
            Add("sa", 0x004F, "Sanskrit");
            Add("sa-IN", 0x044F, "Sanskrit (India)");
            Add("sr-Cyrl-CS", 0x0C1A, "Serbian (Serbia, Cyrillic)");
            Add("sr-Latn-CS", 0x081A, "Serbian (Serbia, Latin)");
            Add("sk", 0x001B, "Slovak");
            Add("sk-SK", 0x041B, "Slovak (Slovakia)");
            Add("sl", 0x0024, "Slovenian");
            Add("sl-SI", 0x0424, "Slovenian (Slovenia)");
            Add("es", 0x000A, "Spanish");
            Add("es-AR", 0x2C0A, "Spanish (Argentina)");
            Add("es-BO", 0x400A, "Spanish (Bolivia)");
            Add("es-CL", 0x340A, "Spanish (Chile)");
            Add("es-CO", 0x240A, "Spanish (Colombia)");
            Add("es-CR", 0x140A, "Spanish (Costa Rica)");
            Add("es-DO", 0x1C0A, "Spanish (Dominican Republic)");
            Add("es-EC", 0x300A, "Spanish (Ecuador)");
            Add("es-SV", 0x440A, "Spanish (El Salvador)");
            Add("es-GT", 0x100A, "Spanish (Guatemala)");
            Add("es-HN", 0x480A, "Spanish (Honduras)");
            Add("es-MX", 0x080A, "Spanish (Mexico)");
            Add("es-NI", 0x4C0A, "Spanish (Nicaragua)");
            Add("es-PA", 0x180A, "Spanish (Panama)");
            Add("es-PY", 0x3C0A, "Spanish (Paraguay)");
            Add("es-PE", 0x280A, "Spanish (Peru)");
            Add("es-PR", 0x500A, "Spanish (Puerto Rico)");
            Add("es-ES", 0x0C0A, "Spanish (Spain)");
            Add("es-ES_tradnl", 0x040A, "Spanish (Spain, Traditional Sort)");
            Add("es-UY", 0x380A, "Spanish (Uruguay)");
            Add("es-VE", 0x200A, "Spanish (Venezuela)");
            Add("sw", 0x0041, "Swahili");
            Add("sw-KE", 0x0441, "Swahili (Kenya)");
            Add("sv", 0x001D, "Swedish");
            Add("sv-FI", 0x081D, "Swedish (Finland)");
            Add("sv-SE", 0x041D, "Swedish (Sweden)");
            Add("syr", 0x005A, "Syriac");
            Add("syr-SY", 0x045A, "Syriac (Syria)");
            Add("ta", 0x0049, "Tamil");
            Add("ta-IN", 0x0449, "Tamil (India)");
            Add("tt", 0x0044, "Tatar");
            Add("tt-RU", 0x0444, "Tatar (Russia)");
            Add("te", 0x004A, "Telugu");
            Add("te-IN", 0x044A, "Telugu (India)");
            Add("th", 0x001E, "Thai");
            Add("th-TH", 0x041E, "Thai (Thailand)");
            Add("tr", 0x001F, "Turkish");
            Add("tr-TR", 0x041F, "Turkish (Turkey)");
            Add("uk", 0x0022, "Ukrainian");
            Add("uk-UA", 0x0422, "Ukrainian (Ukraine)");
            Add("ur", 0x0020, "Urdu");
            Add("ur-PK", 0x0420, "Urdu (Pakistan)");
            Add("uz", 0x0043, "Uzbek");
            Add("uz-Cyrl-UZ", 0x0843, "Uzbek (Uzbekistan, Cyrillic)");
            Add("uz-Latn-UZ", 0x0443, "Uzbek (Uzbekistan, Latin)");
            Add("vi", 0x002A, "Vietnamese");
            Add("vi-VN", 0x042A, "Vietnamese (Vietnam)");
        }
    }
}