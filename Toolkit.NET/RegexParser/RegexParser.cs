using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Toolkit.NET.RegexParser
{
    /// <summary>
    /// Class specialized to parse data from big string which can be 
    /// splitted to rows and rows are used to shortly specify where is your information located.
    /// Class is good extension to Regex
    /// </summary>
    public class RegexParser : IRegexParser
    {
        public string[] Rows { get; private set; }
        public string DefaultValue { get; set; }

        /// <summary>
        /// Constructor of RegexParser
        /// </summary>
        /// <param name="paInputText">Text which is need to be splited to rows</param>
        /// <param name="paDefaultValue">If regex parser will not find any match then will be used DefaultValue</param>
        /// <param name="paSeparators">characters which specifies new line in InputText</param>
        public RegexParser(string paInputText, string paDefaultValue, char[] paSeparators)
        {
            Rows = paInputText.Split(paSeparators);
            DefaultValue = paDefaultValue;
        }

        /// <summary>
        /// Constructor of RegexParser
        /// </summary>
        /// <param name="paDefaultValue">If regex parser will not find any match then will be used DefaultValue</param>
        /// <param name="paRows">InputString splitted on rows</param>
        public RegexParser(string paDefaultValue, params string[] paRows)
        {
            Rows = paRows;
            DefaultValue = paDefaultValue;
        }

        /// <summary>
        /// Returns specified row
        /// </summary>
        /// <param name="paRow">index of row</param>
        /// <param name="paRowMustContain">If is defined then row is returned 
        /// only if contain specified string oterwise returns DefaultValue</param>
        /// <returns></returns>
        public string GetRow(int paRow, string paRowMustContain = null)
        {
            try
            {
                return GetRow(new[] { paRow }, RegexDirection.FromStartToEnd, paRowMustContain);
            }
            catch (Exception)
            {
                return DefaultValue;
            }
        }

        /// <summary>
        /// Method returns row from interval which contain selected string
        /// </summary>
        /// <param name="paSearchInRows">Indexes of rows where can be placed your row</param>
        /// <param name="paDirection">Direction of search</param>
        /// <param name="paRowMustContain">String which must contain your line</param>
        /// <returns></returns>
        public string GetRow(int[] paSearchInRows, RegexDirection paDirection, string paRowMustContain)
        {
            try
            {
                var searchInRows = paDirection == RegexDirection.FromStartToEnd ? paSearchInRows : paSearchInRows.Reverse();
                foreach (var row in searchInRows)
                {
                    if (row >= 0 && Rows.Length > 0 && row < Rows.Length)
                    {
                        if (string.IsNullOrEmpty(paRowMustContain) || Rows[row].ToLower().Contains(paRowMustContain.ToLower()))
                        {
                            return Rows[row];
                        }
                    }
                }
                return DefaultValue;
            }
            catch (Exception)
            {
                return DefaultValue;
            }
        }

        /// <summary>
        /// Method returns row from interval which contain selected string and must be approved by your own function
        /// </summary>
        /// <param name="paValidateFunc"></param>
        /// <param name="paSearchInRows">Indexes of rows where can be placed your row</param>
        /// <param name="paDirection">Direction of search</param>
        /// <param name="paRowMustContain">String which must contain your line</param>
        /// <returns></returns>
        public string GetRow(Func<string, bool> paValidateFunc, int[] paSearchInRows, RegexDirection paDirection, string paRowMustContain)
        {
            try
            {
                var searchInRows = paDirection == RegexDirection.FromStartToEnd ? paSearchInRows : paSearchInRows.Reverse();
                foreach (var row in searchInRows)
                {
                    if (row >= 0 && Rows.Length > 0 && row < Rows.Length)
                    {
                        if (string.IsNullOrEmpty(paRowMustContain) || Rows[row].ToLower().Contains(paRowMustContain.ToLower()))
                        {
                            if (paValidateFunc(Rows[row]))
                                return Rows[row];
                        }
                    }
                }
                return DefaultValue;
            }
            catch (Exception)
            {
                return DefaultValue;
            }
        }

        /// <summary>
        /// Method try match your regex pattern in specified row oterwise returns DefaultValue
        /// </summary>
        /// <param name="paRegexPattern"></param>
        /// <param name="paRow"></param>
        /// <returns></returns>
        public string ParseRows(string paRegexPattern, int paRow)
        {
            try
            {
                var inputText = string.Empty;
                if (paRow >= 0 && Rows.Length > 0 && paRow < Rows.Length)
                    inputText = Rows[paRow];
                var match = Regex.Match(inputText, paRegexPattern);
                return match.Success ? match.Value.Trim() : DefaultValue;
            }
            catch (Exception)
            {
                return DefaultValue;
            }
        }

        /// <summary>
        /// Method try match your regex pattern in specified rows oterwise returns DefaultValue
        /// </summary>
        /// <param name="paRegexPattern"></param>
        /// <param name="paSearchInRows"></param>
        /// <param name="paDirection"></param>
        /// <param name="paRowMustContain"></param>
        /// <returns></returns>
        public string ParseRows(string paRegexPattern, int[] paSearchInRows, RegexDirection paDirection, string paRowMustContain = null)
        {
            try
            {
                var searchInRows = paDirection == RegexDirection.FromStartToEnd
                    ? paSearchInRows
                    : paSearchInRows.Reverse();
                foreach (var row in searchInRows)
                {
                    var calcRow = row;
                    if (paDirection == RegexDirection.FromEndToStart)
                        calcRow = Rows.Length - row;
                    if (calcRow >= 0 && Rows.Length > 0 && calcRow < Rows.Length)
                    {
                        if (string.IsNullOrEmpty(paRowMustContain) ||
                            Rows[calcRow].ToLower().Contains(paRowMustContain.ToLower()))
                        {
                            if (string.IsNullOrEmpty(paRegexPattern))
                                return Rows[calcRow];
                            var match = Regex.Match(Rows[calcRow], paRegexPattern);
                            if (match.Success)
                                return match.Value.Trim();
                        }
                    }
                }
                return DefaultValue;
            }
            catch (Exception)
            {
                return DefaultValue;
            }
        }

        /// <summary>
        /// Method will return row which is validated by your ValitadeFunction 
        /// </summary>
        /// <param name="paValidateFunc"></param>
        /// <param name="paSearchInRows"></param>
        /// <param name="paDirection"></param>
        /// <param name="paRowMustContain"></param>
        /// <returns></returns>
        public string ParseRows(Func<string, bool> paValidateFunc, int[] paSearchInRows, RegexDirection paDirection, string paRowMustContain = null)
        {
            try
            {
                var searchInRows = paDirection == RegexDirection.FromStartToEnd ? paSearchInRows : paSearchInRows.Reverse();
                foreach (var row in searchInRows)
                {
                    var calcRow = row;
                    if (paDirection == RegexDirection.FromEndToStart)
                        calcRow = Rows.Length - row;
                    if (calcRow >= 0 && Rows.Length > 0 && calcRow < Rows.Length)
                    {
                        if (string.IsNullOrEmpty(paRowMustContain) || Rows[calcRow].ToLower().Contains(paRowMustContain.ToLower()))
                        {
                            if (paValidateFunc(Rows[calcRow]))
                                return Rows[calcRow];
                        }
                    }
                }
                return DefaultValue;
            }
            catch (Exception)
            {
                return DefaultValue;
            }
        }

        /// <summary>
        /// Method will take your interval of rows and try everyone of them
        /// If row contains sepcified string than is customized by your function and after that is used regex pattern
        /// If your row match regex pattern then returns row oterwise will return DefaultValue
        /// </summary>
        /// <param name="paCustomLineEditFunc"></param>
        /// <param name="paRegexPattern"></param>
        /// <param name="paSearchInRows"></param>
        /// <param name="paDirection"></param>
        /// <param name="paRowMustContain"></param>
        /// <returns></returns>
        public string CustomParse(Func<string, string> paCustomLineEditFunc, string paRegexPattern, int[] paSearchInRows, RegexDirection paDirection, string paRowMustContain = null)
        {
            try
            {
                var searchInRows = paDirection == RegexDirection.FromStartToEnd ? paSearchInRows : paSearchInRows.Reverse();
                foreach (var row in searchInRows)
                {
                    var calcRow = row;
                    if (paDirection == RegexDirection.FromEndToStart)
                        calcRow = Rows.Length - row;
                    if (calcRow < 0 || calcRow >= Rows.Length)
                        continue;
                    var customLine = paCustomLineEditFunc(Rows[calcRow]);
                    if (calcRow >= 0 && Rows.Length > 0 && calcRow < Rows.Length)
                    {
                        if (string.IsNullOrEmpty(paRowMustContain) || Rows[calcRow].ToLower().Contains(paRowMustContain.ToLower()))
                        {
                            if (string.IsNullOrEmpty(paRegexPattern))
                                return customLine;
                            var match = Regex.Match(customLine, paRegexPattern);
                            if (match.Success)
                                return match.Value.Trim();
                        }
                    }
                }
                return DefaultValue;
            }
            catch (Exception)
            {
                return DefaultValue;
            }
        }
    }
}
