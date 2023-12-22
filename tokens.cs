using System;
using System.Text.RegularExpressions;

public class Tokens
{
    //типы переменных
    public static bool VariableTypeIntSearcher(string text)
    {
        return (text == "инт");
    }
    public static bool VariableTypeFloatSearcher(string text)
    {
        return (text == "флт");
    }
    public static bool VariableTypeLongSearcher(string text)
    {
        return (text == "лнг");
    }
    public static bool VariableTypeStringSearcher(string text)
    {
        return (text == "стр");
    }
    //сами переменные
    public static bool VariablesSearcher(string text)
    {
        bool maybe = true;

        if (text.Length == 0 || !Char.IsLetter(text[0]))
            maybe = false;
        else
            foreach (char symbol in text)
            {
                if (!Char.IsDigit(symbol) && !Char.IsLetter(symbol))
                    maybe = false;
            }
        return maybe;
    }
    public static bool IncludeFileSearcher(string text)
    {
        /*
        bool maybe = true;

        if (text.Length == 0 || text[0] != '#')
            maybe = false;
        else
            for (int i = 1; i < text.Length; i++)
            {
                if (!Char.IsDigit(text[i]) && !Char.IsLetter(text[i]))
                    maybe = false;
            }
        return maybe;
        */
        return (text == "#старт" || text == "#строка");
    }
    //арифметические операторы
    public static bool ArithmeticSearcher(string text)
    {
        return (text == "<" || text == ">" || text == "<=" || text == "=>" || text == "<>" || text == "==");
    }
    //поиск равно
    public static bool EqualitySearcher(string text)
    {
        return (text == "=");
    }
    //конец строки
    public static bool EndlineSearcher(string text)
    {
        return (text == ";");
    }
    //условный оператор
    public static bool IfSearcher(string text)
    {
        return (text == "!если");
    }
    public static bool ElseSearcher(string text)
    {
        return (text == "!иначе");
    }
    //ввод
    public static bool InputSearcher(string text)
    {
        return (text == "!ввод");
    }
    public static bool BreakSearcher(string text)
    {
        return (text == "!пропустить");
    }
    public static bool ContinueSearcher(string text)
    {
        return (text == "!продолжить");
    }
    //вывод
    public static bool OutputSearcher(string text)
    {
        return (text == "!вывод");
    }
    //функция
    public static bool FunctionSearcher(string text)
    {
        return (text == "!функция");
    }
    //название функции
    public static bool FunctionNameSearcher(string text)
    {
        bool maybe = true;

        if (text.Length == 0 || !Char.IsLetter(text[0]))
            maybe = false;
        else
            foreach (char symbol in text)
            {
                if (!Char.IsDigit(symbol) && !Char.IsLetter(symbol))
                    maybe = false;
            }
        return maybe;
    }
    //включение через директивы
    public static bool IncludeSearcher(string text)
    {
        return (text == "!вкл");
    }
    //цикл с предусловием
    public static bool WhileSearcher(string text)
    {
        return (text == "!пока");
    }
    //поиск числа
    public static bool IntNumberSearcher(string text)
    {
        int number;
        return int.TryParse(text, out number);
    }
    public static bool FloatNumberSearcher(string text)
    {
        float number;
        return float.TryParse(text, out number);
    }
    public static bool LongNumberSearcher(string text)
    {
        long number;
        return long.TryParse(text, out number);
    }
    //скобки
    public static bool OpenBracketsSearcher(string text)
    {
        return (text == "(");
    }
    public static bool CloseBracketsSearcher(string text)
    {
        return (text == ")");
    }
    //блоки
    public static bool OpenBlocksSearcher(string text)
    {
        return (text == "{");
    }
    public static bool CloseBlocksSearcher(string text)
    {
        return (text == "}");
    }
    //возврат
    public static bool ReturnSearcher(string text)
    {
        return (text == "!вернуть");
    }
    //головной файл
    public static bool HeadSearcher(string text)
    {
        return (text == "!основа");
    }
    //строка
    public static bool StringSearcher(string text)
    {
        return (text == "/");
    }

}
