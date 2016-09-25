using System;

namespace Toolkit.NET.RegexParser
{
    public interface IRegexParser
    {
        string GetRow(int paRow, string paRowMustContain = null);

        string GetRow(int[] paSearchInRows, RegexDirection paDirection, string paRowMustContain);

        string GetRow(Func<string, bool> paValidateFunc, int[] paSearchInRows, RegexDirection paDirection, string paRowMustContain);

        string ParseRows(string paRegexPattern, int paRow);

        string ParseRows(string paRegexPattern, int[] paSearchInRows, RegexDirection paDirection, string paRowMustContain = null);

        string ParseRows(Func<string, bool> paValidateFunc, int[] paSearchInRows, RegexDirection paDirection, string paRowMustContain = null);

        string CustomParse(Func<string, string> paCustomLineEditFunc, string paRegexPattern, int[] paSearchInRows,
            RegexDirection paDirection, string paRowMustContain = null);
    }
}
