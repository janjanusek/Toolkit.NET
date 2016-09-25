using System;

namespace Toolkit.NET.RegexParser
{
    public interface IRegexParser
    {
        string GetRow(int paLine, string paRowMustContain = null);

        string GetRow(int[] paSearchInRows, RegexDirection paDirection, string paLineMustContent);

        string GetRow(Func<string, bool> paValidateFunc, int[] paSearchInRows, RegexDirection paDirection, string paLineMustContent);

        string ParseRows(string paRegexPattern, int paRow);

        string ParseRows(string paRegexPattern, int[] paSearchInRows, RegexDirection paDirection, string paLineMustContent = null);

        string ParseRows(Func<string, bool> paValidateFunc, int[] paSearchInRows, RegexDirection paDirection, string paLineMustContent = null);

        string CustomParse(Func<string, string> paCustomLineEditFunc, string paRegexPattern, int[] paSearchInRows,
            RegexDirection paDirection, string paLineMustContent = null);
    }
}
