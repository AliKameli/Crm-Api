using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Noor.Text
{
    /// <summary>
    /// 
    /// </summary>
    public static class StringExtension
    {
        /// <inheritdoc />
        public enum NormalizeGroup
        {
            Digits = 0,
            EnglishAlpha = 1,
            EditingCharacters = 2,
            Erabs = 3,//ترتیب قرار گرفتن این مورد نباید بالا تر از کاراکترهای ویرایشی باشد
            HalfSpace = 4,
            MultiModeCharacter = 5,
            Space = 6
        }

        public class NormalizeOption
        {
            public NormalizeGroup NormalizeGroup { get; set; }
            public string ReplacementString { get; set; } = null;
            public bool TobeReplaced { get; set; }
        }
        #region Sample

        /// <summary>
        /// تابع نرمال سازی که بخش متن کاوی برای اعمال بر روی ریشه ها پیشنهاد داده است
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string SampleTextMining(this string text)
        {
            text = text.Replace("ؤ", "ء").Replace("إ", "ء").Replace("ّ", "")
                .Replace("أ", "ء").Replace("ا", "ء")
                .Replace("ئ", "ء").Replace("ة", "ت")
                .Replace("ی", "ي").Replace("ى", "ي")
                .Replace("آ", "ء").Replace("ک", "ك").Trim();
            return text;
        }

        /// <summary>
        /// تابعی برای جایگزینی گروه کاراکترها با کاراکتر پیشنهادی
        /// </summary>
        /// <param name="inputValue">رشته ورودی</param>
        /// <param name="normalizeOptionList"></param>
        /// <returns></returns>
        public static string Sample(this string inputValue, List<NormalizeOption> normalizeOptionList)
        {
            var outPutValue = SpecialCharacters(inputValue);
            //حتما باید انیوم ها به ترتیب اعمال شوند.
            var normalizeGroupList = Enum.GetValues(typeof(NormalizeGroup));
            foreach (var normalizeGroup in normalizeGroupList)
            {
                var normalizeOption = normalizeOptionList.SingleOrDefault(a => (int)a.NormalizeGroup == (int)normalizeGroup);
                switch (normalizeGroup)
                {
                    case NormalizeGroup.Digits:
                        if (normalizeOption != null)
                        {
                            if (normalizeOption.TobeReplaced)
                            {
                                string replacementString = normalizeOption.ReplacementString ?? "";
                                outPutValue = RemoveDigits(outPutValue, replacementString);
                            }
                        }
                        else//Defult
                        {
                            outPutValue = RemoveDigits(outPutValue);
                        }
                        break;
                    case NormalizeGroup.EditingCharacters:
                        if (normalizeOption != null)
                        {
                            if (normalizeOption.TobeReplaced)
                            {
                                string replacementString = normalizeOption.ReplacementString ?? "";
                                outPutValue = RemoveEditingCharacters(outPutValue, replacementString);
                            }
                        }
                        else//Defult
                        {
                            outPutValue = RemoveEditingCharacters(outPutValue);
                        }
                        break;
                    case NormalizeGroup.EnglishAlpha:
                        if (normalizeOption != null)
                        {
                            if (normalizeOption.TobeReplaced)
                            {
                                string replacementString = normalizeOption.ReplacementString ?? "";
                                outPutValue = RemoveEnglishAlpha(outPutValue, replacementString);
                            }
                        }
                        else//Defult
                        {
                            outPutValue = RemoveEnglishAlpha(outPutValue);
                        }
                        break;
                    case NormalizeGroup.Erabs:
                        if (normalizeOption != null)
                        {
                            if (normalizeOption.TobeReplaced)
                            {
                                string replacementString = normalizeOption.ReplacementString ?? string.Empty;
                                outPutValue = RemoveErabs(outPutValue, replacementString);
                            }
                        }
                        else//Defult
                        {
                            outPutValue = RemoveErabs(outPutValue, string.Empty);
                        }
                        break;
                    case NormalizeGroup.HalfSpace:
                        if (normalizeOption != null)
                        {
                            if (normalizeOption.TobeReplaced)
                            {
                                string replacementString = normalizeOption.ReplacementString ?? string.Empty;
                                outPutValue = RemoveHalfSpace(outPutValue, replacementString);
                            }
                        }
                        else//Defult
                        {
                            outPutValue = RemoveHalfSpace(outPutValue, string.Empty);
                        }
                        break;
                    case NormalizeGroup.MultiModeCharacter:
                        if (normalizeOption != null)
                        {
                            if (normalizeOption.TobeReplaced)
                            {
                                outPutValue = UniformMultiModeCharacter(outPutValue);
                            }
                        }
                        else//Defult
                        {
                            outPutValue = UniformMultiModeCharacter(outPutValue);
                        }
                        break;
                    case NormalizeGroup.Space:
                        if (normalizeOption != null)
                        {
                            if (normalizeOption.TobeReplaced)
                            {
                                string replacementString = normalizeOption.ReplacementString ?? string.Empty;
                                outPutValue = outPutValue.Replace(((char)32).ToString(), replacementString);
                            }
                        }
                        else//Defult
                        {
                            outPutValue = outPutValue.Replace(((char)32).ToString(), string.Empty);
                        }
                        break;
                }
            }
            return outPutValue;
        }

        /// <summary>
        /// تابعی برای حذف کاراکترهای خاص
        /// </summary>
        /// <param name="inputValue">رشته ورودی</param>
        /// <param name="removeSpace">فاصله حذف شوند یا خیر؟ 0 حذف شوند 1 باقی بمانند</param>
        /// <param name="removeDigits">اعداد حذف شوند یا خیر؟ 0 حذف شوند 1باقی بمانند</param>
        /// <param name="removeEnglishAlpha">حروف انگلیسی حذف شوند یا خیر؟ 0 حذف شوند 1 باقی بمانند</param>
        /// <param name="removeEditingCharacters">کاراکترهای ویرایشی حذف شوند یا خیر؟ 0  حذف شوند 1 باقی بمانند</param>
        /// <param name="removeErabs">اعراب حذف شوند یا خیر ؟0 حذف شوند 1 باقی بماند</param>
        /// <param name="sampleCharacterForSearch">تبدیل بعضی کاراکترها به کاراکترهای یکسان برای جستجو</param>
        /// <param name="sampleCharacterForSort">تبدیل کاراکترهای عربی به فارسی برای مرتب سازی</param>
        /// <returns></returns>
        public static string Sample(this string inputValue, bool removeSpace = true, bool removeDigits = true, bool removeEnglishAlpha = true, bool removeEditingCharacters = true, bool removeErabs = true, bool sampleCharacterForSearch = true, bool sampleCharacterForSort = false)
        {
            var outPutValue = SpecialCharacters(inputValue);
            outPutValue = RemoveHalfSpace(outPutValue);

            if (sampleCharacterForSearch)
            {
                outPutValue = UniformMultiModeCharacter(outPutValue);
            }
            else
            if (sampleCharacterForSort)
            {
                outPutValue = SampleCharacterForSort(outPutValue);
            }

            if (removeErabs)
            {
                outPutValue = RemoveErabs(outPutValue);
            }

            if (removeDigits)
            {
                outPutValue = RemoveDigits(outPutValue);
            }

            if (removeEnglishAlpha)
            {
                outPutValue = RemoveEnglishAlpha(outPutValue);
            }

            if (removeEditingCharacters)
            {
                outPutValue = RemoveEditingCharacters(outPutValue);
            }

            outPutValue = Regex.Replace(outPutValue, @"\s+", " "); //outPutValue.Replace(((char)8205).ToString(), " ");

            if (removeSpace)
            {
                outPutValue = outPutValue.Replace(((char)32).ToString(), string.Empty);
            }
            return outPutValue;
        }

        private static string SpecialCharacters(string outPutValue)
        {
            outPutValue = outPutValue.Replace(((char)9618).ToString(), " "); //▒ کاراکتر 177 نور2
            outPutValue = outPutValue.Replace(((char)9611).ToString(), " "); //█ کاراکتر 219 نور2
            outPutValue = outPutValue.Replace(((char)3338).ToString(), " "); // Modifier Letter Dot Vertical Bar
            // حذف کاراکتر ندید
            outPutValue = outPutValue.Replace(((char)8206).ToString(), " "); //Right-To-Let Mark
            outPutValue = outPutValue.Replace(((char)8207).ToString(), " "); //Right-To-Let Mark
            //نرمالسازی تشدید
            outPutValue = outPutValue.Replace("َّ", "َّ").Replace("ُّ", "ُّ").Replace("ِّ", "ِّ");
            outPutValue = ReplaceUnormalDigit(outPutValue);

            //outPutValue = outPutValue.Replace(((char)1606).ToString(), "6");
            return outPutValue;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="outPutValue"></param>
        /// <returns></returns>
        public static string ReplaceUnormalDigit(string outPutValue)
        {
            outPutValue = outPutValue.Replace(((char)1632).ToString(), "0");
            outPutValue = outPutValue.Replace(((char)1633).ToString(), "1");
            outPutValue = outPutValue.Replace(((char)1634).ToString(), "2");
            outPutValue = outPutValue.Replace(((char)1635).ToString(), "3");
            outPutValue = outPutValue.Replace(((char)1636).ToString(), "4");
            outPutValue = outPutValue.Replace(((char)1637).ToString(), "5");
            outPutValue = outPutValue.Replace(((char)1638).ToString(), "6");
            outPutValue = outPutValue.Replace(((char)1639).ToString(), "7");
            outPutValue = outPutValue.Replace(((char)1640).ToString(), "8");
            outPutValue = outPutValue.Replace(((char)1641).ToString(), "9");

            outPutValue = outPutValue.Replace(((char)1776).ToString(), "0");
            outPutValue = outPutValue.Replace(((char)1777).ToString(), "1");
            outPutValue = outPutValue.Replace(((char)1778).ToString(), "2");
            outPutValue = outPutValue.Replace(((char)1779).ToString(), "3");
            outPutValue = outPutValue.Replace(((char)1780).ToString(), "4");
            outPutValue = outPutValue.Replace(((char)1781).ToString(), "5");
            outPutValue = outPutValue.Replace(((char)1782).ToString(), "6");
            outPutValue = outPutValue.Replace(((char)1783).ToString(), "7");
            outPutValue = outPutValue.Replace(((char)1784).ToString(), "8");
            outPutValue = outPutValue.Replace(((char)1785).ToString(), "9");

            return outPutValue;
        }
        //همسان کردن کاراکترهای چند شکلی
        private static string UniformMultiModeCharacter(string outPutValue)
        {
            outPutValue = outPutValue.Replace("ﻵ", "ﻠﺎ");
            outPutValue = outPutValue.Replace("ﻶ", "ﻠﺎ");
            outPutValue = outPutValue.Replace("ﻼ", "ﻠﺎ");
            outPutValue = outPutValue.Replace("ﻷ", "ﻠﺎ");
            outPutValue = outPutValue.Replace("ﻸ", "ﻠﺎ");
            outPutValue = outPutValue.Replace("ﻹ", "ﻠﺎ");
            outPutValue = outPutValue.Replace("ﻺ", "ﻠﺎ");
            outPutValue = outPutValue.Replace("ﻻ", "ﻠﺎ");

            outPutValue = outPutValue.Replace((char)1574, (char)1740); //Replace 'ئ' to  'ی'
            outPutValue = outPutValue.Replace((char)65161, (char)1740); //Replace 'ﺉ' to  'ی'
            outPutValue = outPutValue.Replace((char)65162, (char)1740); //Replace 'ﺊ' to  'ی'
            outPutValue = outPutValue.Replace((char)65163, (char)1740); //Replace 'ﺋ' to  'ی'
            outPutValue = outPutValue.Replace((char)1609, (char)1740); //Replace 'ى' to 'ی'
            outPutValue = outPutValue.Replace((char)1610, (char)1740); //Replace 'ي' to 'ی'

            outPutValue = outPutValue.Replace((char)1572, (char)1608); //Replace 'ؤ' to 'و'

            outPutValue = outPutValue.Replace((char)1577, (char)1607); //Replace 'ة' to 'ه'
            outPutValue = outPutValue.Replace((char)1729, (char)1607); //Replace 'ہ'  to 'ه')
            outPutValue = outPutValue.Replace((char)1728, (char)1607); //Replace 'ۀ'  to 'ه')

            outPutValue = outPutValue.Replace((char)1571, (char)1575); //Replace('أ' to 'ا')
            outPutValue = outPutValue.Replace((char)65155, (char)1575); //Replace('ﺃ' to 'ﺍ') or 65165
            outPutValue = outPutValue.Replace((char)65156, (char)1575); //Replace('ﺄ' to 'ﺍ')

            outPutValue = outPutValue.Replace((char)1573, (char)1575); //Replace('إ' to 'ا')
            outPutValue = outPutValue.Replace((char)65159, (char)1575); //Replace('ﺇ' to 'ﺍ')
            outPutValue = outPutValue.Replace((char)65160, (char)1575); //Replace('ﺈ' to 'ﺍ')

            outPutValue = outPutValue.Replace((char)1570, (char)1575); //Replace('آ' to 'ﺍ')
            outPutValue = outPutValue.Replace((char)65153, (char)1575); //Replace('ﺁ' to 'ﺍ')
            outPutValue = outPutValue.Replace((char)65154, (char)1575); //Replace('ﺂ' to 'ﺍ')
            outPutValue = outPutValue.Replace((char)1649, (char)1575); //Replace('ٱ' to 'ﺍ')

            outPutValue = SampleCharacterForSort(outPutValue);

            return outPutValue;
        }

        private static string SampleCharacterForSort(string outPutValue)
        {
            outPutValue = outPutValue.Replace("ﻻ", "ﻠﺎ");
            outPutValue = outPutValue.Replace((char)1603, (char)1705); //replce 'ك' to 'ک'
            outPutValue = outPutValue.Replace((char)1609, (char)1740); //Replace 'ى' to 'ی'
            outPutValue = outPutValue.Replace((char)1610, (char)1740); //Replace 'ي' to 'ی'
            outPutValue = outPutValue.Replace((char)1729, (char)1607); //Replace 'ہ'  to 'ه')
            return outPutValue;
        }

        private static string RemoveEditingCharacters(string outPutValue, string replacementValue = "")
        {
            outPutValue = outPutValue.Replace(((char)33).ToString(), replacementValue); //!
            outPutValue = outPutValue.Replace(((char)34).ToString(), replacementValue); //"
            outPutValue = outPutValue.Replace(((char)35).ToString(), replacementValue); //#
            outPutValue = outPutValue.Replace(((char)36).ToString(), replacementValue); //$
            outPutValue = outPutValue.Replace(((char)37).ToString(), replacementValue); //%
            outPutValue = outPutValue.Replace(((char)38).ToString(), replacementValue); //&
            outPutValue = outPutValue.Replace(((char)39).ToString(), replacementValue); //'
            outPutValue = outPutValue.Replace(((char)40).ToString(), replacementValue); //(
            outPutValue = outPutValue.Replace(((char)41).ToString(), replacementValue); //)
            outPutValue = outPutValue.Replace(((char)42).ToString(), replacementValue); //*
            outPutValue = outPutValue.Replace(((char)43).ToString(), replacementValue); //+
            outPutValue = outPutValue.Replace(((char)44).ToString(), replacementValue); //,
            outPutValue = outPutValue.Replace(((char)45).ToString(), replacementValue); //-
            outPutValue = outPutValue.Replace(((char)46).ToString(), replacementValue); //.
            outPutValue = outPutValue.Replace(((char)47).ToString(), replacementValue); // /

            outPutValue = outPutValue.Replace(((char)58).ToString(), replacementValue); //:
            outPutValue = outPutValue.Replace(((char)59).ToString(), replacementValue); //;
            outPutValue = outPutValue.Replace(((char)60).ToString(), replacementValue); //<
            outPutValue = outPutValue.Replace(((char)61).ToString(), replacementValue); //=
            outPutValue = outPutValue.Replace(((char)62).ToString(), replacementValue); //>
            outPutValue = outPutValue.Replace(((char)63).ToString(), replacementValue); //?
            outPutValue = outPutValue.Replace("؟", replacementValue); //؟
            outPutValue = outPutValue.Replace(((char)64).ToString(), replacementValue); //@

            outPutValue = outPutValue.Replace(((char)91).ToString(), replacementValue); //[
            outPutValue = outPutValue.Replace(((char)92).ToString(), replacementValue); // \
            outPutValue = outPutValue.Replace(((char)93).ToString(), replacementValue); // ]
            outPutValue = outPutValue.Replace(((char)94).ToString(), replacementValue); //^
            outPutValue = outPutValue.Replace(((char)95).ToString(), replacementValue); //_
            outPutValue = outPutValue.Replace(((char)96).ToString(), replacementValue); //`

            outPutValue = outPutValue.Replace(((char)123).ToString(), replacementValue); //{
            outPutValue = outPutValue.Replace(((char)124).ToString(), replacementValue); //|
            outPutValue = outPutValue.Replace(((char)125).ToString(), replacementValue); //}

            outPutValue = outPutValue.Replace(((char)1548).ToString(), replacementValue); //،

            outPutValue = outPutValue.Replace("»", replacementValue); //»
            outPutValue = outPutValue.Replace("«", replacementValue); //«
            outPutValue = outPutValue.Replace("؛", replacementValue); //؛
            return outPutValue;
        }

        private static string RemoveErabs(string outPutValue, string replacementValue = "")
        {
            // حذف تشدید و اعراب
            outPutValue = outPutValue.Replace(((char)64606).ToString(), replacementValue);//ﱞ
            outPutValue = outPutValue.Replace(((char)64607).ToString(), replacementValue);//ﱟ
            outPutValue = outPutValue.Replace(((char)64608).ToString(), replacementValue);//ﱠ
            outPutValue = outPutValue.Replace(((char)64609).ToString(), replacementValue);//ﱡ
            outPutValue = outPutValue.Replace(((char)64610).ToString(), replacementValue);//ﱢ
            // حذف اعراب
            outPutValue = outPutValue.Replace(((char)1611).ToString(), replacementValue);
            outPutValue = outPutValue.Replace(((char)1612).ToString(), replacementValue);
            outPutValue = outPutValue.Replace(((char)1613).ToString(), replacementValue);
            outPutValue = outPutValue.Replace(((char)1614).ToString(), replacementValue);
            outPutValue = outPutValue.Replace(((char)1615).ToString(), replacementValue);
            outPutValue = outPutValue.Replace(((char)1616).ToString(), replacementValue);
            outPutValue = outPutValue.Replace(((char)1617).ToString(), replacementValue);
            outPutValue = outPutValue.Replace(((char)1618).ToString(), replacementValue);
            outPutValue = outPutValue.Replace(((char)1619).ToString(), replacementValue);
            outPutValue = outPutValue.Replace(((char)1620).ToString(), replacementValue);
            outPutValue = outPutValue.Replace(((char)1621).ToString(), replacementValue);
            //SubscriptAlef الف خنجري پايين
            outPutValue = outPutValue.Replace(((char)1622).ToString(), replacementValue);
            outPutValue = outPutValue.Replace(((char)1623).ToString(), replacementValue);
            outPutValue = outPutValue.Replace(((char)1624).ToString(), replacementValue);
            outPutValue = outPutValue.Replace(((char)1625).ToString(), replacementValue);
            outPutValue = outPutValue.Replace(((char)1626).ToString(), replacementValue);
            outPutValue = outPutValue.Replace(((char)1627).ToString(), replacementValue);
            outPutValue = outPutValue.Replace(((char)1628).ToString(), replacementValue);
            outPutValue = outPutValue.Replace(((char)1629).ToString(), replacementValue);
            outPutValue = outPutValue.Replace(((char)1630).ToString(), replacementValue);
            outPutValue = outPutValue.Replace(((char)1631).ToString(), replacementValue);
            outPutValue = outPutValue.Replace(((char)1761).ToString(), replacementValue);//ۡ

            //SuperscriptAlef الف خنجري بالا
            outPutValue = outPutValue.Replace(((char)1648).ToString(), replacementValue);
            return outPutValue;
        }
        private static string RemoveHalfSpace(string outPutValue, string replacementValue = "")
        {
            outPutValue = outPutValue.Replace(((char)8204).ToString(), replacementValue); //Alt+0157 sample ه‍ق Zero With Non-Joiner
            outPutValue = outPutValue.Replace(((char)8205).ToString(), replacementValue); //Alt+0158 sample می‌شود Zero With Joiner
            return outPutValue;
        }
        private static string RemoveDigits(string outPutValue, string replacementValue = "")
        {
            //0-9
            outPutValue = outPutValue.Replace(((char)48).ToString(), replacementValue);
            outPutValue = outPutValue.Replace(((char)49).ToString(), replacementValue);
            outPutValue = outPutValue.Replace(((char)50).ToString(), replacementValue);
            outPutValue = outPutValue.Replace(((char)51).ToString(), replacementValue);
            outPutValue = outPutValue.Replace(((char)52).ToString(), replacementValue);
            outPutValue = outPutValue.Replace(((char)53).ToString(), replacementValue);
            outPutValue = outPutValue.Replace(((char)54).ToString(), replacementValue);
            outPutValue = outPutValue.Replace(((char)55).ToString(), replacementValue);
            outPutValue = outPutValue.Replace(((char)56).ToString(), replacementValue);
            outPutValue = outPutValue.Replace(((char)57).ToString(), replacementValue);
            //اعداد فارسی

            return outPutValue;
        }
        private static string RemoveEnglishAlpha(string outPutValue, string replacementValue = "")
        {
            // A-Z
            outPutValue = outPutValue.Replace(((char)65).ToString(), replacementValue);
            outPutValue = outPutValue.Replace(((char)66).ToString(), replacementValue);
            outPutValue = outPutValue.Replace(((char)67).ToString(), replacementValue);
            outPutValue = outPutValue.Replace(((char)68).ToString(), replacementValue);
            outPutValue = outPutValue.Replace(((char)69).ToString(), replacementValue);
            outPutValue = outPutValue.Replace(((char)70).ToString(), replacementValue);
            outPutValue = outPutValue.Replace(((char)71).ToString(), replacementValue);
            outPutValue = outPutValue.Replace(((char)72).ToString(), replacementValue);
            outPutValue = outPutValue.Replace(((char)73).ToString(), replacementValue);
            outPutValue = outPutValue.Replace(((char)74).ToString(), replacementValue);
            outPutValue = outPutValue.Replace(((char)75).ToString(), replacementValue);
            outPutValue = outPutValue.Replace(((char)76).ToString(), replacementValue);
            outPutValue = outPutValue.Replace(((char)77).ToString(), replacementValue);
            outPutValue = outPutValue.Replace(((char)78).ToString(), replacementValue);
            outPutValue = outPutValue.Replace(((char)79).ToString(), replacementValue);
            outPutValue = outPutValue.Replace(((char)80).ToString(), replacementValue);
            outPutValue = outPutValue.Replace(((char)81).ToString(), replacementValue);
            outPutValue = outPutValue.Replace(((char)82).ToString(), replacementValue);
            outPutValue = outPutValue.Replace(((char)83).ToString(), replacementValue);
            outPutValue = outPutValue.Replace(((char)84).ToString(), replacementValue);
            outPutValue = outPutValue.Replace(((char)85).ToString(), replacementValue);
            outPutValue = outPutValue.Replace(((char)86).ToString(), replacementValue);
            outPutValue = outPutValue.Replace(((char)87).ToString(), replacementValue);
            outPutValue = outPutValue.Replace(((char)88).ToString(), replacementValue);
            outPutValue = outPutValue.Replace(((char)89).ToString(), replacementValue);
            outPutValue = outPutValue.Replace(((char)90).ToString(), replacementValue);

            // a-z
            outPutValue = outPutValue.Replace(((char)97).ToString(), replacementValue);
            outPutValue = outPutValue.Replace(((char)98).ToString(), replacementValue);
            outPutValue = outPutValue.Replace(((char)99).ToString(), replacementValue);
            outPutValue = outPutValue.Replace(((char)100).ToString(), replacementValue);
            outPutValue = outPutValue.Replace(((char)101).ToString(), replacementValue);
            outPutValue = outPutValue.Replace(((char)102).ToString(), replacementValue);
            outPutValue = outPutValue.Replace(((char)103).ToString(), replacementValue);
            outPutValue = outPutValue.Replace(((char)104).ToString(), replacementValue);
            outPutValue = outPutValue.Replace(((char)105).ToString(), replacementValue);
            outPutValue = outPutValue.Replace(((char)106).ToString(), replacementValue);
            outPutValue = outPutValue.Replace(((char)107).ToString(), replacementValue);
            outPutValue = outPutValue.Replace(((char)108).ToString(), replacementValue);
            outPutValue = outPutValue.Replace(((char)109).ToString(), replacementValue);
            outPutValue = outPutValue.Replace(((char)110).ToString(), replacementValue);
            outPutValue = outPutValue.Replace(((char)111).ToString(), replacementValue);
            outPutValue = outPutValue.Replace(((char)112).ToString(), replacementValue);
            outPutValue = outPutValue.Replace(((char)113).ToString(), replacementValue);
            outPutValue = outPutValue.Replace(((char)114).ToString(), replacementValue);
            outPutValue = outPutValue.Replace(((char)115).ToString(), replacementValue);
            outPutValue = outPutValue.Replace(((char)116).ToString(), replacementValue);
            outPutValue = outPutValue.Replace(((char)117).ToString(), replacementValue);
            outPutValue = outPutValue.Replace(((char)118).ToString(), replacementValue);
            outPutValue = outPutValue.Replace(((char)119).ToString(), replacementValue);
            outPutValue = outPutValue.Replace(((char)120).ToString(), replacementValue);
            outPutValue = outPutValue.Replace(((char)121).ToString(), replacementValue);
            outPutValue = outPutValue.Replace(((char)122).ToString(), replacementValue);

            // phonetic
            outPutValue = outPutValue.Replace(((char)256).ToString(), replacementValue);
            outPutValue = outPutValue.Replace(((char)257).ToString(), replacementValue);
            outPutValue = outPutValue.Replace(((char)298).ToString(), replacementValue);
            outPutValue = outPutValue.Replace(((char)299).ToString(), replacementValue);
            outPutValue = outPutValue.Replace(((char)362).ToString(), replacementValue);
            outPutValue = outPutValue.Replace(((char)363).ToString(), replacementValue);
            outPutValue = outPutValue.Replace(((char)7692).ToString(), replacementValue);
            outPutValue = outPutValue.Replace(((char)7693).ToString(), replacementValue);
            outPutValue = outPutValue.Replace(((char)7716).ToString(), replacementValue);
            outPutValue = outPutValue.Replace(((char)7717).ToString(), replacementValue);
            outPutValue = outPutValue.Replace(((char)7778).ToString(), replacementValue);
            outPutValue = outPutValue.Replace(((char)7779).ToString(), replacementValue);
            outPutValue = outPutValue.Replace(((char)7788).ToString(), replacementValue);
            outPutValue = outPutValue.Replace(((char)7789).ToString(), replacementValue);
            outPutValue = outPutValue.Replace(((char)7826).ToString(), replacementValue);
            outPutValue = outPutValue.Replace(((char)7827).ToString(), replacementValue);
            return outPutValue;
        }

        #endregion Sample

        #region RegularExpression

        #region Common
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pattern"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        public static MatchCollection ExcuteRegex(string pattern, string str)
        {
            var rgx = new Regex(pattern);
            var matchCollection = rgx.Matches(str);
            return matchCollection;
        }
        #endregion Common

        #region NGeneralRegex

        #region Number Methods

        public static bool IsNumber(this string character)
        {
            Regex PureNumberRegex = new Regex(@"^-?\d+$");
            Match match = PureNumberRegex.Match(character);
            if (match.Success)
                return true;
            return false;
        }

        public static bool HasNumber(this string character)
        {
            Regex NumberRegex = new Regex(@"\d+");
            Match match = NumberRegex.Match(character);
            if (match.Success)
                return true;
            return false;
        }

        #endregion

        #region Digit Methods

        /// <summary>
        /// Get digit from text when lenght is between minLenght, maxLenght
        /// </summary>
        /// <param name="str"></param>
        /// <param name="minLenght">Minimum Lenght</param>
        /// <param name="maxLenght">Maximum Lenght</param>
        /// <returns>MatchCollection</returns>
        /// <Creator>Amir Niazi</Creator>
        /// <date>2015/09/23</date>
        public static MatchCollection GetDigit(this string str, int minLenght = 1, int maxLenght = 1)
        {
            var pattern = string.Format(@"\d{{{0},{1}}}", minLenght, maxLenght);
            return ExcuteRegex(pattern, str);
        }

        /// <summary>
        /// Get digit from text when lenght is at least 1
        /// </summary>
        /// <param name="str"></param>
        /// <param name="minLenght">Minimum Lenght</param>
        /// <returns>MatchCollection</returns>
        /// <creator>Amir Niazi</creator>
        /// <date>2015/09/23</date>
        public static MatchCollection GetDigit(this string str, int minLenght = 1)
        {
            var pattern = string.Format(@"\d{{{0},}}", minLenght);
            return ExcuteRegex(pattern, str);
        }

        #endregion

        #region HtmlTag Methods

        /// <summary>
        /// Get all html tag
        /// <para> GroupNames{TagTitle} </para>
        /// </summary>
        /// <param name="str"></param>
        /// <returns>MatchCollection</returns>
        /// <creator>Amir Niazi</creator>
        /// <date>2015/09/23</date>
        public static MatchCollection GetAllHtmlTag(this string str)
        {
            return ExcuteRegex(@"(\<(/?(?<TagTitle>[^\>]+))\>)", str);
        }

        /// <summary>
        /// Get specific html tag
        /// </summary>
        /// <param name="str">tag Title</param>
        /// <param name="tagTitle"></param>
        /// <returns>MatchCollection</returns>
        /// <creator>Amir Niazi</creator>
        /// <date>2015/09/26</date>
        public static MatchCollection GetHtmlTag(this string str, string tagTitle)
        {
            var pattern = string.Format(@"<{0}>((?!(<{0}>|</{0}>))(.|\r\n))*?</{0}>", tagTitle);
            return ExcuteRegex(pattern, str);
        }

        /// <summary>
        /// Get open html tag
        /// </summary>
        /// <param name="str"></param>
        /// <returns>MatchCollection</returns>
        /// <creator>Amir Niazi</creator>
        /// <date>2015/09/23</date>
        public static MatchCollection GetOpenHtmlTag(this string str)
        {
            return ExcuteRegex(@"(\<(?=[^\/])([^\>]+)\>)", str);
        }

        /// <summary>
        /// Get close html tag
        /// </summary>
        /// <param name="str"></param>
        /// <returns>MatchCollection</returns>
        /// <creator>Amir Niazi</creator>
        /// <date>2015/09/23</date>
        public static MatchCollection GetCloseHtmlTag(this string str)
        {
            return ExcuteRegex(@"(\<(/[^\>]+)\>)", str);
        }

        #endregion

        #region Persian Method
        /// <summary>
        /// Get Persian character
        /// </summary>
        /// <param name="str"></param>
        /// <returns>MatchCollection</returns>
        /// <creator>Amir Niazi</creator>
        /// <date>2015/09/26</date>
        public static MatchCollection GetPersianChar(this string str)
        {
            return ExcuteRegex(@"[\u0600-\u06FF]", str);
        }

        /// <summary>
        /// Get Persian Word
        /// </summary>
        /// <param name="str"></param>
        /// <returns>MatchCollection</returns>
        /// <creator>Amir Niazi</creator>
        /// <date>2015/09/26</date>
        public static MatchCollection GetPersianWord(this string str)
        {
            return ExcuteRegex(@"[\u0600-\u060b\u060d-\u06FF\u200C-\u200D]+", str);
        }
        #endregion

        #region English Method
        /// <summary>
        /// Get English character
        /// </summary>
        /// <param name="str"></param>
        /// <returns>MatchCollection</returns>
        /// <creator>Amir Niazi</creator>
        /// <date>2015/09/26</date>
        public static MatchCollection GetEnglishChar(this string str)
        {
            return ExcuteRegex(@"[A-Za-z]", str);
        }

        /// <summary>
        /// Get English Word
        /// </summary>
        /// <param name="str"></param>
        /// <returns>MatchCollection</returns>
        /// <creator>Amir Niazi</creator>
        /// <date>2015/09/26</date>
        public static MatchCollection GetEnglishWord(this string str)
        {
            return ExcuteRegex(@"[A-Za-z]+", str);
        }
        #endregion

        #region Arabic Method
        /// <summary>
        /// Get Arabic Char
        /// </summary>
        /// <param name="str"></param>
        /// <returns>MatchCollection</returns>
        /// <creator>Amir Niazi</creator>
        /// <date>2015/10/03</date>
        public static MatchCollection GetArabicChar(this string str)
        {
            return ExcuteRegex(@"[\u0610-\u0615\u0618-\u061A\u0621-\u065F\u2000-\u200F\uFC5E-\uFC63\u0670\uFE70-\uFEFC\uFB50-\uFBB1\u06CC\u06AF\u067E\u0698\u0686\u06A9\u06C0-\u06C3\u0674]", str);
        }
        /// <summary>
        /// Get Arabic Word
        /// </summary>
        /// <param name="str"></param>
        /// <returns>MatchCollection</returns>
        /// <creator>Amir Niazi</creator>
        /// <date>2015/10/03</date>
        public static MatchCollection GetArabicWord(this string str)
        {
            return ExcuteRegex(@"[\u0610-\u0615\u0618-\u061A\u0621-\u065F\u2000-\u200F\uFC5E-\uFC63\u0670\uFE70-\uFEFC\uFB50-\uFBB1\u06CC\u06AF\u067E\u0698\u0686\u06A9\u06C0-\u06C3\u0674\u200C-\u200D]+", str);
        }
        #endregion

        #region Editing Character Method
        /// <summary>
        /// Get all editing character from txt
        /// </summary>
        /// <param name="str"></param>
        /// <returns>MatchCollection</returns>
        /// <creator>Amir Niazi</creator>
        /// <date>2015/09/29</date>
        public static MatchCollection GetEditingCharacter(this string str)
        {
            return ExcuteRegex(@"[^\P{P}]", str);
        }

        #endregion

        #region Email Method
        /// <summary>
        /// Get emails list from text
        /// </summary>
        /// <param name="str"></param>
        /// <returns>MatchCollection</returns>
        /// <creator>Amir Niazi</creator>
        /// <date>2015/09/29</date>
        public static MatchCollection GetEmail(this string str)
        {
            return ExcuteRegex(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", str);
        }
        /// <summary>
        /// check email address is valid or not valid
        /// </summary>
        /// <param name="str"></param>
        /// <returns>bool</returns>
        /// <creator>Amir Niazi</creator>
        /// <date>2015/09/29</date>
        public static bool IsValidEmail(this string str)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(str);
                return addr.Address == str;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region Word Method
        /// <summary>
        /// Get Word When End With Expressions
        /// </summary>
        /// <param name="str"></param>
        /// <returns>MatchCollection</returns>
        /// <creator>Amir Niazi</creator>
        /// <date>2015/10/07</date>
        public static MatchCollection GetWordEndWithExp(this string str, string exp)
        {
            return ExcuteRegex(string.Format(@"\w+{0}(?=[^\P{P}]|\W|^)", exp), str);
        }
        /// <summary>
        /// Get Start of Word When End With Expressions
        /// </summary>
        /// <param name="str"></param>
        /// <returns>MatchCollection</returns>
        /// <creator>Amir Niazi</creator>
        /// <date>2015/10/07</date>
        public static MatchCollection GetStartOFWordEndWithExp(this string str, string exp)
        {
            return ExcuteRegex(string.Format(@"\w+(?={0}([^\P{P}]|\W|^))", exp), str);
        }
        /// <summary>
        /// Get Word When Start With Expressions
        /// </summary>
        /// <param name="str"></param>
        /// <returns>MatchCollection</returns>
        /// <creator>Amir Niazi</creator>
        /// <date>2015/10/07</date>
        public static MatchCollection GetWordStartWithExp(this string str, string exp)
        {
            return ExcuteRegex(string.Format(@"(?<=([^\P{P}]|\W|^)){0}\w+", exp), str);
        }
        /// <summary>
        /// Get End of Word When Start With Expressions
        /// </summary>
        /// <param name="str"></param>
        /// <returns>MatchCollection</returns>
        /// <creator>Amir Niazi</creator>
        /// <date>2015/10/07</date>
        public static MatchCollection GetEndOFWordStartWithExp(this string str, string exp)
        {
            return ExcuteRegex(string.Format(@"(?<=([^\P{P}]|\W|^){0})\w+", exp), str);
        }
        #endregion

        #region Noor2SpecificRegex
        /// <summary>
        /// Get page number and page text from text
        ///<para> GroupNames{pageNumber,pageText} </para>
        /// </summary>
        /// <param name="str"></param>
        /// <returns>MatchCollection</returns>
        /// <Creator>Amir Niazi</Creator>
        /// <date>2015/09/30</date>
        public static MatchCollection GetPageNumberWithText(this string str)
        {
            string pattern = @"\}\$(?<pageNumber>\d+)\$\{(?<pageText>((?!\}\$).)*)";
            return ExcuteRegex(pattern, str);
        }

        /// <summary>
        /// Get page format from text
        /// GroupNames: {pageNumber}
        /// </summary>
        /// <param name="str"></param>
        /// <returns>MatchCollection</returns>
        /// <Creator>Amir Niazi</Creator>
        /// <date>2015/09/30</date>
        public static MatchCollection GetPageFormat(this string str)
        {
            const string pattern = @"\}\$(?<pageNumber>\d+)\$\{";
            return ExcuteRegex(pattern, str);
        }

        /// <summary>
        /// Get Revayat Format from text
        /// GroupNames: {MasomNo,RevayatID,RevayatText}
        /// </summary>
        /// <param name="str"></param>
        /// <returns>MatchCollection</returns>
        /// <Creator>Amir Niazi</Creator>
        /// <date>2015/10/04</date>
        public static MatchCollection GetRevayatFormat(this string str)
        {
            return ExcuteRegex(@"}R((?<MasomNo>(\d+[\,\-])+))?(}IR(?<RevayatID>\d+)IR{)?(?<RevayatText>((?!\}R|R\{).)*)R{", str);
        }

        /// <summary>
        /// Get Heading Format from text
        /// GroupNames: {HeadingTitle}
        /// </summary>
        /// <param name="str"></param>
        /// <returns>MatchCollection</returns>
        /// <Creator>Amir Niazi</Creator>
        /// <date>2015/10/05</date>
        public static MatchCollection GetHeadingFormat(this string str)
        {
            return ExcuteRegex(@"}J(?![a-zA-Z])(?<HeadingTitle>((?!J{).)*)(?<![a-zA-Z])J{", str);
        }
        #endregion Noor2SpecificRegex

        #endregion NGeneralRegex

        #endregion RegularExpression

        #region Reverse
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string Reverse(this string input)
        {
            var outPut = "";
            while (input.Length > 0)
            {
                outPut += input.Substring(input.Length - 1);
                input = input.Substring(0, input.Length - 1);
            }
            return outPut;
        }
        #endregion

    }
}

