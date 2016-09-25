using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Toolkit.NET.RegexParser
{
    public class RegexParser : IRegexParser
    {
        public string[] Rows { get; private set; }
        public string DefaultValue { get; set; }

        public RegexParser(string paInputText, string paDefaultValue, char[] paSeparators)
        {
            Rows = paInputText.Split(paSeparators);
            DefaultValue = paDefaultValue;
        }

        public RegexParser(string paDefaultValue, params string[] paRows)
        {
            Rows = paRows;
            DefaultValue = paDefaultValue;
        }

        public string GetRow(int paLine, string paRowMustContain = null)
        {
            try
            {
                return GetRow(new[] { paLine }, RegexDirection.FromStartToEnd, paRowMustContain);
            }
            catch (Exception)
            {
                return DefaultValue;
            }
        }

        public string GetRow(int[] paSearchInRows, RegexDirection paDirection, string paLineMustContent)
        {
            try
            {
                var searchInRows = paDirection == RegexDirection.FromStartToEnd ? paSearchInRows : paSearchInRows.Reverse();
                foreach (var line in searchInRows)
                {
                    if (line >= 0 && Rows.Length > 0 && line < Rows.Length)
                    {
                        if (string.IsNullOrEmpty(paLineMustContent) || Rows[line].ToLower().Contains(paLineMustContent.ToLower()))
                        {
                            return Rows[line];
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

        public string GetRow(Func<string, bool> paValidateFunc, int[] paSearchInRows, RegexDirection paDirection, string paLineMustContent)
        {
            try
            {
                var searchInRows = paDirection == RegexDirection.FromStartToEnd ? paSearchInRows : paSearchInRows.Reverse();
                foreach (var line in searchInRows)
                {
                    if (line >= 0 && Rows.Length > 0 && line < Rows.Length)
                    {
                        if (string.IsNullOrEmpty(paLineMustContent) || Rows[line].ToLower().Contains(paLineMustContent.ToLower()))
                        {
                            if (paValidateFunc(Rows[line]))
                                return Rows[line];
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

        public string ParseRows(string paRegexPattern, int[] paSearchInRows, RegexDirection paDirection, string paLineMustContent = null)
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
                        if (string.IsNullOrEmpty(paLineMustContent) ||
                            Rows[calcRow].ToLower().Contains(paLineMustContent.ToLower()))
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

        public string ParseRows(Func<string, bool> paValidateFunc, int[] paSearchInRows, RegexDirection paDirection, string paLineMustContent = null)
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
                        if (string.IsNullOrEmpty(paLineMustContent) || Rows[calcRow].ToLower().Contains(paLineMustContent.ToLower()))
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

        public string CustomParse(Func<string, string> paCustomLineEditFunc, string paRegexPattern, int[] paSearchInRows, RegexDirection paDirection, string paLineMustContent = null)
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
                        if (string.IsNullOrEmpty(paLineMustContent) || Rows[calcRow].ToLower().Contains(paLineMustContent.ToLower()))
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
