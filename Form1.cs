using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace curseach
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            richTextBox2.ReadOnly = true;
            richTextBox3.ReadOnly = true;
            richTextBox4.ReadOnly = true;
        }

        List<string> tokensForSyntax = new List<string>();
        List<string> tokenListWithoutNames = new List<string>();
        bool lexerError;
        bool syntaxError;
        bool semantError;

        public void Lexer()
        {
            string analysisWord = string.Empty;
            string[] codeWithNewLines = richTextBox1.Text.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            string ourCode = string.Join("", codeWithNewLines);

            //int stringCount = 0;
            bool stringSearch = false;

            List<string> listOfTokens = new List<string>();
            //List<string> listOfDeclaredVariables = new List<string>();

            tokensForSyntax.Clear();
            tokenListWithoutNames.Clear();

            foreach (char symbol in ourCode + " ")
            {   
                
                if (stringSearch)
                {
                    if (symbol == ';')
                    {
                        stringSearch = false;
                        listOfTokens.Add(analysisWord + " STRING VALUE");
                        tokenListWithoutNames.Add(analysisWord);
                        tokensForSyntax.Add("STRINGVALUE");
                        analysisWord = string.Empty;
                    }
                    if (symbol == '(' || symbol == ')')
                    {
                        richTextBox3.AppendText("Строка не должна содержать ( и )" + Environment.NewLine);
                        lexerError = true;
                        break;
                    }
                }              

                if (!stringSearch)
                {

                    if (Tokens.VariableTypeIntSearcher(analysisWord) && symbol == ' ')
                    {
                        listOfTokens.Add(analysisWord + " TYPE VARIABLE INT");
                        tokenListWithoutNames.Add(analysisWord);
                        tokensForSyntax.Add("TYPEVARIABLEINT");
                        analysisWord = string.Empty;
                    }
                    else if (Tokens.VariableTypeFloatSearcher(analysisWord) && symbol == ' ')
                    {
                        listOfTokens.Add(analysisWord + " TYPE VARIABLE FLOAT");
                        tokenListWithoutNames.Add(analysisWord);
                        tokensForSyntax.Add("TYPEVARIABLEFLOAT");
                        analysisWord = string.Empty;
                    }
                    else if (Tokens.VariableTypeLongSearcher(analysisWord) && symbol == ' ')
                    {
                        listOfTokens.Add(analysisWord + " TYPE VARIABLE LONG");
                        tokenListWithoutNames.Add(analysisWord);
                        tokensForSyntax.Add("TYPEVARIABLELONG");
                        analysisWord = string.Empty;
                    }
                    else if (Tokens.VariableTypeStringSearcher(analysisWord) && symbol == ' ')
                    {
                        listOfTokens.Add(analysisWord + " TYPE VARIABLE STRING");
                        tokenListWithoutNames.Add(analysisWord);
                        tokensForSyntax.Add("TYPEVARIABLESTRING");
                        analysisWord = string.Empty;
                    }
                    else if (Tokens.StringSearcher(analysisWord))
                    {
                        stringSearch = true;
                    }
                    else if (Tokens.VariablesSearcher(analysisWord) && !Tokens.VariableTypeIntSearcher(analysisWord)
                        && !Tokens.VariableTypeFloatSearcher(analysisWord)
                        && !Tokens.VariableTypeLongSearcher(analysisWord)
                        && !Tokens.VariableTypeStringSearcher(analysisWord)
                    && (symbol == ' ' || symbol == '=' || symbol == '>' || symbol == '<' || symbol == ')' || symbol == ';'))
                    {
                        listOfTokens.Add(analysisWord + " VARIABLE");
                        tokenListWithoutNames.Add(analysisWord);
                        tokensForSyntax.Add("VARIABLE");
                        analysisWord = string.Empty;
                    }
                    else if (Tokens.IncludeFileSearcher(analysisWord) && symbol == ';')
                    {
                        listOfTokens.Add(analysisWord + " INCLUDE FILE");
                        tokenListWithoutNames.Add(analysisWord);
                        tokensForSyntax.Add("INCLUDEFILE");
                        analysisWord = string.Empty;
                    }
                    else if (Tokens.EqualitySearcher(analysisWord) && (Char.IsDigit(symbol) || Char.IsLetter(symbol) || symbol == ' '))
                    {
                        listOfTokens.Add(analysisWord + " EQUAL");
                        tokenListWithoutNames.Add(analysisWord);
                        tokensForSyntax.Add("EQUAL");
                        analysisWord = string.Empty;
                    }
                    else if (Tokens.HeadSearcher(analysisWord) && (symbol == '{' || symbol == ' '))
                    {
                        listOfTokens.Add(analysisWord + " HEAD");
                        tokenListWithoutNames.Add(analysisWord);
                        tokensForSyntax.Add("HEAD");
                        analysisWord = string.Empty;
                    }
                    else if (Tokens.ArithmeticSearcher(analysisWord)
                    && (symbol == ' ' || Char.IsDigit(symbol) || Char.IsLetter(symbol)))
                    {
                        listOfTokens.Add(analysisWord + " OPERATOR");
                        tokenListWithoutNames.Add(analysisWord);
                        tokensForSyntax.Add("OPERATOR");
                        analysisWord = string.Empty;
                    }
                    else if (Tokens.EndlineSearcher(analysisWord))
                    {
                        listOfTokens.Add(analysisWord + " ENDLINE");
                        tokenListWithoutNames.Add(analysisWord);
                        tokensForSyntax.Add("ENDLINE");
                        analysisWord = string.Empty;
                    }
                    else if (Tokens.IfSearcher(analysisWord) && symbol == '(')
                    {
                        listOfTokens.Add(analysisWord + " IF");
                        tokenListWithoutNames.Add(analysisWord);
                        tokensForSyntax.Add("IF");
                        analysisWord = string.Empty;
                    }
                    else if (Tokens.ElseSearcher(analysisWord) && (symbol == ' ' || symbol == '{'))
                    {
                        listOfTokens.Add(analysisWord + " ELSE");
                        tokenListWithoutNames.Add(analysisWord);
                        tokensForSyntax.Add("ELSE");
                        analysisWord = string.Empty;
                    }
                    else if (Tokens.InputSearcher(analysisWord) && symbol == '(')
                    {
                        listOfTokens.Add(analysisWord + " INPUT");
                        tokenListWithoutNames.Add(analysisWord);
                        tokensForSyntax.Add("INPUT");
                        analysisWord = string.Empty;
                    }
                    else if (Tokens.OutputSearcher(analysisWord) && symbol == '(')
                    {
                        listOfTokens.Add(analysisWord + " OUTPUT");
                        tokenListWithoutNames.Add(analysisWord);
                        tokensForSyntax.Add("OUTPUT");
                        analysisWord = string.Empty;
                    }
                    else if (Tokens.WhileSearcher(analysisWord) && symbol == '(')
                    {
                        listOfTokens.Add(analysisWord + " WHILE");
                        tokenListWithoutNames.Add(analysisWord);
                        tokensForSyntax.Add("WHILE");
                        analysisWord = string.Empty;
                    }
                    else if (Tokens.IncludeSearcher(analysisWord) && symbol == ' ')
                    {
                        listOfTokens.Add(analysisWord + " INCLUDE");
                        tokenListWithoutNames.Add(analysisWord);
                        tokensForSyntax.Add("INCLUDE");
                        analysisWord = string.Empty;
                    }
                    else if (Tokens.FunctionSearcher(analysisWord) && symbol == ' ')
                    {
                        listOfTokens.Add(analysisWord + " FUNCTION");
                        tokenListWithoutNames.Add(analysisWord);
                        tokensForSyntax.Add("FUNCTION");
                        analysisWord = string.Empty;
                    }
                    else if (Tokens.FunctionNameSearcher(analysisWord) && symbol == '(' && !Tokens.VariableTypeIntSearcher(analysisWord)
                        && !Tokens.VariableTypeFloatSearcher(analysisWord)
                        && !Tokens.VariableTypeLongSearcher(analysisWord)
                        && !Tokens.VariableTypeStringSearcher(analysisWord))
                    {
                        listOfTokens.Add(analysisWord + " FUNCTION NAME");
                        tokenListWithoutNames.Add(analysisWord);
                        tokensForSyntax.Add("FUNCTIONNAME");
                        analysisWord = string.Empty;
                    }
                    else if (Tokens.IntNumberSearcher(analysisWord)
                    && (symbol == ' ' || symbol == '>' || symbol == '<' || symbol == ';' || symbol == ')'))
                    {
                        listOfTokens.Add(analysisWord + " INT NUMBER");
                        tokenListWithoutNames.Add(analysisWord);
                        tokensForSyntax.Add("INTNUM");
                        analysisWord = string.Empty;
                    }
                    else if (Tokens.LongNumberSearcher(analysisWord)
                    && (symbol == ' ' || symbol == '>' || symbol == '<' || symbol == ';' || symbol == ')'))
                    {
                        listOfTokens.Add(analysisWord + " LONG NUMBER");
                        tokenListWithoutNames.Add(analysisWord);
                        tokensForSyntax.Add("LONGNUM");
                        analysisWord = string.Empty;
                    }
                    else if (Tokens.FloatNumberSearcher(analysisWord)
                    && (symbol == ' ' || symbol == '>' || symbol == '<' || symbol == ';' || symbol == ')'))
                    {
                        listOfTokens.Add(analysisWord + " FLOAT NUMBER");
                        tokenListWithoutNames.Add(analysisWord);
                        tokensForSyntax.Add("FLOATNUM");
                        analysisWord = string.Empty;
                    }
                    else if (Tokens.OpenBracketsSearcher(analysisWord))
                    {
                        listOfTokens.Add(analysisWord + " OPEN BRACKET");
                        tokenListWithoutNames.Add(analysisWord);
                        tokensForSyntax.Add("OPENBRACKET");
                        analysisWord = string.Empty;
                    }
                    else if (Tokens.CloseBracketsSearcher(analysisWord))
                    {
                        listOfTokens.Add(analysisWord + " CLOSE BRACKET");
                        tokenListWithoutNames.Add(analysisWord);
                        tokensForSyntax.Add("CLOSEBRACKET");
                        analysisWord = string.Empty;
                    }
                    else if (Tokens.OpenBlocksSearcher(analysisWord))
                    {
                        listOfTokens.Add(analysisWord + " OPEN BLOCK");
                        tokenListWithoutNames.Add(analysisWord);
                        tokensForSyntax.Add("OPENBLOCK");
                        analysisWord = string.Empty;
                    }
                    else if (Tokens.CloseBlocksSearcher(analysisWord))
                    {
                        listOfTokens.Add(analysisWord + " CLOSE BLOCK");
                        tokenListWithoutNames.Add(analysisWord);
                        tokensForSyntax.Add("CLOSEBLOCK");
                        analysisWord = string.Empty;
                    }
                    else if (Tokens.ReturnSearcher(analysisWord) && symbol == ' ')
                    {
                        listOfTokens.Add(analysisWord + " RETURN");
                        tokenListWithoutNames.Add(analysisWord);
                        tokensForSyntax.Add("RETURN");
                        analysisWord = string.Empty;
                    }
                    else if (analysisWord == " ")
                    {
                        analysisWord = string.Empty;
                    }
                }
                analysisWord += symbol;
            }

            if (analysisWord != String.Empty && analysisWord != " ") { richTextBox3.AppendText("Ошибка в токенах " + analysisWord + Environment.NewLine);  lexerError = true; }

            //richTextBox2.Clear();
            foreach (string token in listOfTokens)
            {
                richTextBox2.AppendText(token + Environment.NewLine);
            }
            /*
            foreach (string token in tokensForSyntax)
            {
                richTextBox2.AppendText(token + Environment.NewLine);
            }
            */
        }

        public void Syntax()
        {
            //richTextBox3.Clear();
            
            int priority = 0; //номер приоритета

            bool INCLUDE = true;
            bool HEAD = false;
            bool FUNC = false;
            bool FUNCNAME = false;
            bool CYCLE = false;
            bool IFELSE = false;
            bool VARS = false;
            bool ENTER = false;
            bool OUTPUT = false;

            bool CLOSEBLOCK = false; //для проверки необходимости }
            bool headON = false; //включаем доступ к хеад
            bool funcON = false; //включаем доступ к функции
            bool newFunc = false;
            bool newToken = false; //включение новых токенов
            bool ifelseMode = false;

            IncludeConditions TestIncludeToken = new IncludeConditions();
            HeadConditions TestHeadToken = new HeadConditions();
            VariablesConditions TestVariableToken = new VariablesConditions();
            WhileConditions TestWhileToken = new WhileConditions();
            EnterConditions TestEnterToken = new EnterConditions();
            OutputConditions TestOutputToken = new OutputConditions();
            IfElseConditions TestIfElseToken = new IfElseConditions();
            FunctionConditions TestFuncToken = new FunctionConditions();
            FunctionNameConditions TestFuncNameToken = new FunctionNameConditions();

            //successfulEndlinesDone endEndlineToken = new successfulEndlinesDone();
            successfulBlocksDone endBlocksToken = new successfulBlocksDone();

            bool lastEndlineToken = false;
            bool lastBlockToken = false;

            List<string> condBrackets = new List<string>(); //включение новых токенов после }
            //bool newToken = false; //включение новых токенов
            //bool ifelseMode = false;
            int countBlocks = 0; //количество фигурных скобок

            foreach (string token in tokensForSyntax)
            {
                if (newToken && token != "CLOSEBLOCK")
                {
                    if (!headON && !funcON)
                    {
                        switch (token)
                        {
                            case "INCLUDE":
                                HEAD = false;
                                break;
                            case "HEAD":
                                HEAD = true;
                                INCLUDE = false;
                                condBrackets.Add(token);
                                break;
                        }
                    }
                    else if (headON && !funcON)
                    {
                        switch (token)
                        {
                            case var tokenstring when new Regex(@"TYPEVARIABLE").IsMatch(tokenstring):
                                VARS = true;
                                FUNC = false;
                                FUNCNAME = false;
                                HEAD = false;
                                CYCLE = false;
                                ENTER = false;
                                OUTPUT = false;
                                IFELSE = false;
                                break;
                            case "WHILE":
                                CYCLE = true;
                                VARS = false;
                                FUNCNAME = false;
                                HEAD = false;
                                FUNC = false;
                                ENTER = false;
                                OUTPUT = false;
                                IFELSE = false;
                                condBrackets.Add(token);
                                break;
                            case "INPUT":
                                ENTER = true;
                                OUTPUT = false;
                                FUNC = false;
                                FUNCNAME = false;
                                CYCLE = false;
                                VARS = false;
                                HEAD = false;
                                IFELSE = false;
                                break;
                            case "OUTPUT":
                                OUTPUT = true;
                                ENTER = false;
                                FUNCNAME = false;
                                FUNC = false;
                                CYCLE = false;
                                VARS = false;
                                HEAD = false;
                                IFELSE = false;
                                break;
                            case "FUNCTIONNAME":
                                FUNCNAME = true;
                                ENTER = false;
                                OUTPUT = false;
                                FUNC = false;
                                CYCLE = false;
                                VARS = false;
                                HEAD = false;
                                IFELSE = false;
                                break;
                            case "IF":
                            case "ELSE":
                                IFELSE = true;
                                FUNCNAME = false;
                                OUTPUT = false;
                                ENTER = false;
                                FUNC = false;
                                CYCLE = false;
                                VARS = false;
                                HEAD = false;
                                condBrackets.Add(token);
                                break;
                        }
                    }
                    else if (funcON && headON)
                    {
                        switch (token)
                        {
                            case "FUNCTION":
                                FUNC = true;
                                IFELSE = false;
                                OUTPUT = false;
                                ENTER = false;
                                FUNCNAME = false;
                                CYCLE = false;
                                VARS = false;
                                HEAD = false;
                                condBrackets.Add(token);
                                headON = false;
                                break;
                            default:
                                richTextBox3.AppendText("После основного блока идет только функция!" + Environment.NewLine);
                                break;
                        }
                    }
                    else if (funcON && !headON)
                    {
                        if (newFunc)
                        {
                            switch (token)
                            {
                                case "FUNCTION":
                                    FUNC = true;
                                    IFELSE = false;
                                    OUTPUT = false;
                                    ENTER = false;
                                    FUNCNAME = false;
                                    CYCLE = false;
                                    VARS = false;
                                    HEAD = false;
                                    condBrackets.Add(token);
                                    newFunc = false;
                                    break;
                                default:
                                    richTextBox3.AppendText("После функции только функция!" + Environment.NewLine);
                                    break;
                            }
                        }
                        else if (!newFunc)
                        {
                            switch (token)
                            {                               
                                case "RETURN":
                                    FUNC = true;
                                    IFELSE = false;
                                    OUTPUT = false;
                                    ENTER = false;
                                    CYCLE = false;
                                    FUNCNAME = false;
                                    VARS = false;
                                    HEAD = false;
                                    break;
                                case var tokenstring when new Regex(@"TYPEVARIABLE").IsMatch(tokenstring):
                                    VARS = true;
                                    HEAD = false;
                                    CYCLE = false;
                                    FUNC = false;
                                    FUNCNAME = false;
                                    ENTER = false;
                                    OUTPUT = false;
                                    IFELSE = false;
                                    break;
                                case "WHILE":
                                    CYCLE = true;
                                    VARS = false;
                                    HEAD = false;
                                    FUNCNAME = false;
                                    FUNC = false;
                                    ENTER = false;
                                    OUTPUT = false;
                                    IFELSE = false;
                                    condBrackets.Add(token);
                                    break;
                                case "INPUT":
                                    ENTER = true;
                                    OUTPUT = false;
                                    CYCLE = false;
                                    VARS = false;
                                    HEAD = false;
                                    FUNCNAME = false;
                                    FUNC = false;
                                    IFELSE = false;
                                    break;
                                case "OUTPUT":
                                    OUTPUT = true;
                                    ENTER = false;
                                    CYCLE = false;
                                    VARS = false;
                                    FUNCNAME = false;
                                    FUNC = false;
                                    HEAD = false;
                                    IFELSE = false;
                                    break;
                                case "IF":
                                case "ELSE":
                                    IFELSE = true;
                                    OUTPUT = false;
                                    ENTER = false;
                                    CYCLE = false;
                                    FUNC = false;
                                    FUNCNAME = false;
                                    VARS = false;
                                    HEAD = false;
                                    condBrackets.Add(token);
                                    break;
                            }
                        }
                    }
                    newToken = false;
                    CLOSEBLOCK = false;
                }
                /*
                 * если видим }, то проверяем к какому токену он относится, закрываем его
                 * открываем доступ к другим токенам
                 */
                if ((CLOSEBLOCK || ifelseMode) && token == "CLOSEBLOCK" && !INCLUDE && condBrackets.Any())
                {
                    if (condBrackets.Last() == "HEAD")
                    {
                        TestHeadToken.StateMachine(token);
                        condBrackets.RemoveAt(condBrackets.Count - 1);
                        //lastBlockToken = endBlocksToken.DoneToken(token);
                        FUNC = true; funcON = true;
                    }
                    else if (condBrackets.Last() == "WHILE")
                    {
                        TestWhileToken.StateMachine(token);
                        condBrackets.RemoveAt(condBrackets.Count - 1);
                        VARS = true; CYCLE = true; ENTER = true; OUTPUT = true; IFELSE = true; 
                        if (!funcON) FUNCNAME = true;
                        //lastBlockToken = endBlocksToken.DoneToken(token);
                    }
                    else if (condBrackets.Last() == "IF")
                    {
                        TestIfElseToken.StateMachine(token);
                        condBrackets.RemoveAt(condBrackets.Count - 1);
                        IFELSE = true; VARS = false; CYCLE = false; ENTER = false; OUTPUT = false; HEAD = false;
                        FUNCNAME = false;
                        ifelseMode = true;
                        //lastBlockToken = endBlocksToken.DoneToken(token);
                        //newToken = false;
                        //CLOSEBLOCK = false;
                        //continue;
                    }
                    else if (condBrackets.Last() == "ELSE")
                    {
                        TestIfElseToken.StateMachine(token);
                        condBrackets.RemoveAt(condBrackets.Count - 1);
                        IFELSE = true; VARS = true; CYCLE = true; ENTER = true; OUTPUT = true; HEAD = false;
                        if (!funcON) FUNCNAME = true;
                        //ifelseMode = false;
                        //lastBlockToken = endBlocksToken.DoneToken(token);
                    }
                    else if (condBrackets.Last() == "FUNCTION")
                    {
                        if (TestFuncToken.ValuesConditions(token)) 
                        {
                            TestFuncToken.StateMachine(token);
                            condBrackets.RemoveAt(condBrackets.Count - 1);
                            FUNC = true; VARS = false; CYCLE = false; ENTER = false; OUTPUT = false; HEAD = false; IFELSE = false;
                            FUNCNAME = false; newFunc = true;
                            //lastBlockToken = endBlocksToken.DoneToken(token);
                        }
                        else
                        {
                            richTextBox3.AppendText("У функции отсутствует возврат" + Environment.NewLine);
                            break;
                        }
                    }
                    CLOSEBLOCK = false;
                }
                else if (FUNC)
                {
                    if (!TestFuncToken.ValuesConditions(token))
                    {
                        richTextBox3.AppendText("Ошибка в токене " + token + Environment.NewLine);
                        priority = -1;
                        break;
                    }
                    else { TestFuncToken.StateMachine(token);  }
                }
                else if (FUNCNAME)
                {
                    if (!TestFuncNameToken.ValuesConditions(token))
                    {
                        richTextBox3.AppendText("Ошибка в токене " + token + Environment.NewLine);
                        priority = -1;
                        break;
                    }
                    else TestFuncNameToken.StateMachine(token); 
                        //lastEndlineToken = endEndlineToken.DoneToken(token); }
                }
                else if (ENTER)
                {
                    if (!TestEnterToken.ValuesConditions(token))
                    {
                        richTextBox3.AppendText("Ошибка в токене " + token + Environment.NewLine);
                        priority = -1;
                        break;
                    }
                    else TestEnterToken.StateMachine(token); //lastEndlineToken = endEndlineToken.DoneToken(token); }
                }
                else if (OUTPUT)
                {
                    if (!TestOutputToken.ValuesConditions(token))
                    {
                        richTextBox3.AppendText("Ошибка в токене " + token + Environment.NewLine);
                        priority = -1;
                        break;
                    }
                    else { TestOutputToken.StateMachine(token); }
                }
                else if (IFELSE)
                {
                    if (!TestIfElseToken.ValuesConditions(token))
                    {
                        richTextBox3.AppendText("Ошибка в токене " + token + Environment.NewLine);
                        priority = -1;
                        break;
                    }
                    else { TestIfElseToken.StateMachine(token);  }
                }
                else if (VARS)
                {
                    if (!TestVariableToken.ValuesConditions(token))
                    {
                        richTextBox3.AppendText("Ошибка в токене " + token + Environment.NewLine);
                        priority = -1;
                        break;
                    }
                    else TestVariableToken.StateMachine(token); //lastEndlineToken = endEndlineToken.DoneToken(token); }
                    }
                else if (CYCLE)
                {
                    if (!TestWhileToken.ValuesConditions(token))
                    {
                        richTextBox3.AppendText("Ошибка в токене " + token + Environment.NewLine);
                        priority = -1;
                        break;
                    }
                    else { TestWhileToken.StateMachine(token);  }
                }
                else if (HEAD)
                {
                    if (!TestHeadToken.ValuesConditions(token))
                    {
                        richTextBox3.AppendText("Ошибка в токене " + token + Environment.NewLine);
                        priority = -1;
                        break;
                    }
                    else { TestHeadToken.StateMachine(token); headON = true;  }
                }               
                else if (INCLUDE)
                {
                    if (!TestIncludeToken.ValuesConditions(token))
                    {
                        richTextBox3.AppendText("Ошибка в токене " + token + Environment.NewLine);
                        priority = -1;
                        break;
                    }
                    else  TestIncludeToken.StateMachine(token); //lastEndlineToken = endEndlineToken.DoneToken(token); } 
                }
                else priority = -1;
                //richTextBox3.AppendText(IFELSE.ToString() + " " + ENTER.ToString() + " " + OUTPUT.ToString() + " " +
                //    HEAD.ToString() + " " + CYCLE.ToString() + " " + VARS.ToString() +  " " + FUNC.ToString() + Environment.NewLine);

                countBlocks = endBlocksToken.BlocksCount(token); //счет количества фигурных скобок
                if (countBlocks == -1) break; //первой должна идти {

                if (token == "OPENBLOCK") ifelseMode = false;
                if ((token == "OPENBLOCK" || token == "ENDLINE" || token == "CLOSEBLOCK") && !ifelseMode) { newToken = true; CLOSEBLOCK = true; }

                //if (condBrackets.Any()) richTextBox3.AppendText(condBrackets.Last() + Environment.NewLine);
            }

            //richTextBox3.Clear();
            /*
            if (priority != -1 && lastEndlineToken && lastBlockToken && countBlocks == 0)
            {
                richTextBox3.AppendText("Успешно" + Environment.NewLine);              
            }
            */
            if (priority == -1)
            {
                richTextBox3.AppendText("Ожидание другого токена" + Environment.NewLine);
                syntaxError = true;
            }
            /*
            else if (!lastBlockToken)
            {
                richTextBox3.AppendText("Отсутствует закрывающая }" + Environment.NewLine);
                syntaxError = true;
            }
            */
            else if (countBlocks != 0)
            {
                richTextBox3.AppendText("Нарушен порядок расставления { и }" + Environment.NewLine);
                syntaxError = true;
            }
            //richTextBox3.AppendText(lastBlockToken.ToString() + " " + lastEndlineToken.ToString() + " " + countBlocks.ToString() + Environment.NewLine);

        }

        public void Semant()
        {
            List<string> declaredVariables = new List<string>();
            //List<string> declaredFunction = new List<string>();
            List<string> existsFunction = new List<string>();
            //List<int> tokenNumber = new List<int>();
            //List<string> declaredVariablesForTest = new List<string>();

            Dictionary<string, string> Variables = new Dictionary<string, string>();
            Dictionary<string, string> functionVariables = new Dictionary<string, string>();
            Dictionary<string, string> declaredVariablesForTest = new Dictionary<string, string>();
            Dictionary<string, string> functionType = new Dictionary<string, string>();
            Dictionary<string, string> fType = new Dictionary<string, string>();
            bool testTypes = true;
            bool funcTest = true;
            bool repeatVar = true;
            bool funcFind = false;
            bool nameFunc = true;

            //отдельный поиск для переменных
            for (int i = 0; i < tokenListWithoutNames.Count; i++)
            {
                //в функции только переменные, объявленные в функции
                //if (Tokens.FunctionSearcher(tokenListWithoutNames[i])) { declaredVariables.Clear(); continue; }
                if (i > 0 && (tokenListWithoutNames[i - 1] == "инт" || tokenListWithoutNames[i - 1] == "флт" || tokenListWithoutNames[i - 1] == "стр"))
                {
                    foreach (string x in declaredVariables)
                    {
                        if (x == tokenListWithoutNames[i])
                        {
                            richTextBox3.AppendText("Переменная с названием " + tokenListWithoutNames[i] + " уже существует" + Environment.NewLine);
                            semantError = true;
                            testTypes = false;
                            repeatVar = false;
                            break;
                        }
                    }
                    if (!repeatVar) break;
                    declaredVariables.Add(tokenListWithoutNames[i]);
                    Variables.Add(tokenListWithoutNames[i], tokenListWithoutNames[i - 1]);
                    //declaredVariablesForTest.Add(tokenListWithoutNames[i], tokenListWithoutNames[i - 1]);
                    //tokenNumber.Add(i);
                }
                else if (i > 0 && (tokenListWithoutNames[i - 1] == "(" || tokenListWithoutNames[i - 1] == "<" || tokenListWithoutNames[i - 1] == ">"
                    || tokenListWithoutNames[i - 1] == "<=" || tokenListWithoutNames[i - 1] == "=>" || tokenListWithoutNames[i - 1] == "<>"
                    || tokenListWithoutNames[i - 1] == "==" || Tokens.ReturnSearcher(tokenListWithoutNames[i - 1])) && !Tokens.StringSearcher(tokenListWithoutNames[i])
                    && !Tokens.IntNumberSearcher(tokenListWithoutNames[i]) && !Tokens.FloatNumberSearcher(tokenListWithoutNames[i]) && !Tokens.LongNumberSearcher(tokenListWithoutNames[i])
                    && !Tokens.VariableTypeIntSearcher(tokenListWithoutNames[i]) && !Tokens.VariableTypeFloatSearcher(tokenListWithoutNames[i]) && !Tokens.VariableTypeLongSearcher(tokenListWithoutNames[i])
                    && !Tokens.VariableTypeStringSearcher(tokenListWithoutNames[i]))
                {
                    int variableFind = declaredVariables.IndexOf(tokenListWithoutNames[i]);
                    if (variableFind == -1)
                    {
                        //richTextBox3.Clear();
                        richTextBox3.AppendText("Такой переменной не существует " + tokenListWithoutNames[i] + Environment.NewLine);
                        semantError = true;
                        testTypes = false;
                        //break;
                    }
                }
            }
            //отдельный поиск для функции
            for (int i = tokenListWithoutNames.Count - 1; i >= 0; i--)
            {
                if (i > 0 && Tokens.FunctionNameSearcher(tokenListWithoutNames[i]) && Tokens.OpenBracketsSearcher(tokenListWithoutNames[i + 1]))
                {
                    foreach (string x in declaredVariables)
                    {
                        if (tokenListWithoutNames[i] == x) { richTextBox3.AppendText("Имя функции уже совпадает с именем переменной" + Environment.NewLine); semantError = true; nameFunc = false; break; }
                    }
                    if (!nameFunc) break;
                    if (i > 0 && Tokens.FunctionSearcher(tokenListWithoutNames[i - 1]))
                    {
                        bool eF = false;
                        foreach (string x in existsFunction)
                        {
                            if (tokenListWithoutNames[i] == x) { eF = true; break; }
                        }
                        if (eF)
                        {
                            richTextBox3.AppendText("Функция уже существует: " + tokenListWithoutNames[i] + Environment.NewLine);
                            testTypes = false;
                            semantError = true;
                        }
                        else { existsFunction.Add(tokenListWithoutNames[i]); }
                    }
                    else
                    {
                        int functionFind = existsFunction.IndexOf(tokenListWithoutNames[i]);
                        if (functionFind == -1)
                        {
                            //richTextBox3.Clear();
                            richTextBox3.AppendText("Такой функции не существует: " + tokenListWithoutNames[i] + Environment.NewLine);
                            testTypes = false;
                            semantError = true;
                            //break;
                        }
                    }
                }
            }
            //уместность сравнения
            if (testTypes)
            {
                string type = string.Empty;
                string varName = string.Empty;
                for (int i = 0; i < tokenListWithoutNames.Count; i++)
                {
                    if (i > 0 && Tokens.OpenBracketsSearcher(tokenListWithoutNames[i - 1]) && Tokens.ArithmeticSearcher(tokenListWithoutNames[i + 1]))
                    {
                        if (Variables.Any()) type = Variables[tokenListWithoutNames[i]];
                        if (type == "стр")
                        {
                            richTextBox3.AppendText("Строковые нельзя" + Environment.NewLine);
                            funcTest = false;
                            semantError = true;
                            break;
                        }
                        else varName = tokenListWithoutNames[i];
                    }
                    else if (i > 0 && Tokens.ArithmeticSearcher(tokenListWithoutNames[i - 1]) && Tokens.CloseBracketsSearcher(tokenListWithoutNames[i + 1]))
                    {
                        string twoType = string.Empty;
                        if (varName == tokenListWithoutNames[i]) { richTextBox3.AppendText("Объявление самой себя" + Environment.NewLine); semantError = true; funcTest = false;  break; }
                        if (Variables.Any() && !Tokens.IntNumberSearcher(tokenListWithoutNames[i]) && !Tokens.FloatNumberSearcher(tokenListWithoutNames[i]) && !Tokens.LongNumberSearcher(tokenListWithoutNames[i])) twoType = Variables[tokenListWithoutNames[i]];
                        else twoType = tokenListWithoutNames[i];
                        if (type != twoType)
                        {
                            // string twoType = Variables[tokenListWithoutNames[i]];
                            if (twoType == "стр")
                            {
                                richTextBox3.AppendText("Строковые нельзя" + Environment.NewLine);
                                funcTest = false;
                                semantError = true;
                                break;
                            }
                            else if ((type == "флт" || type == "инт" || type == "лнг") && (twoType == "флт" || twoType == "инт" || twoType == "лнг"))
                            {
                                //richTextBox3.AppendText("С объявлением все в порядке");
                            }
                            else if ((Tokens.IntNumberSearcher(twoType) || Tokens.FloatNumberSearcher(twoType) || Tokens.LongNumberSearcher(twoType))
                                && (type == "флт" || type == "инт" || type == "лнг"))
                            {
                                //richTextBox3.AppendText("С объявлением все в порядке");
                            }
                        }
                        else { richTextBox3.AppendText("Ошибка с типом переменных" + Environment.NewLine); semantError = true; funcTest = false; }
                        }
                }
            }
            //проверка совместимости типов функций, наличия правильных директив
            if (funcTest && testTypes)
            {
                for (int i = 0; i < tokenListWithoutNames.Count; i++)
                {
                    if (i > 0 && Tokens.FunctionNameSearcher(tokenListWithoutNames[i]) && Tokens.OpenBracketsSearcher(tokenListWithoutNames[i + 1]))
                    {
                        if (i > 0 && Tokens.FunctionSearcher(tokenListWithoutNames[i - 1]))
                        {
                            fType.Clear();
                            functionVariables.Clear();
                            fType.Add(tokenListWithoutNames[i], tokenListWithoutNames[i + 2]); //функция и ее тип
                            functionType.Add(tokenListWithoutNames[i], tokenListWithoutNames[i + 2]); //для анализа в дальнейшем цикле
                            funcFind = true;
                        }
                    }
                    else if (funcFind && (tokenListWithoutNames[i] == "инт" || tokenListWithoutNames[i] == "флт" || tokenListWithoutNames[i] == "стр"))
                    {
                        functionVariables.Add(tokenListWithoutNames[i + 1], tokenListWithoutNames[i]); //переменные в теле функции
                    }
                    else if (Tokens.ReturnSearcher(tokenListWithoutNames[i]))
                    {
                        //returnType.Add(tokenListWithoutNames[i + 1], functionVariables.Values.Last());
                        //richTextBox4.AppendText(tokenListWithoutNames[i + 1].ToString() + functionType.Values.Last().ToString() + Environment.NewLine);
                        string typeFunc = String.Empty;
                        string typeVar = String.Empty;
                        if (fType.Any() && functionVariables.Any())
                        {
                            typeFunc = fType.Values.Last().ToString();
                            if (functionVariables.ContainsKey(tokenListWithoutNames[i + 1])) typeVar = functionVariables[tokenListWithoutNames[i + 1]];
                            if (typeFunc != typeVar) { semantError = true; richTextBox3.AppendText("Возврат функции не того типа" + Environment.NewLine); break; }
                        }
                    }
                }
                for (int i = 0; i < tokenListWithoutNames.Count; i++)
                {
                    if (Tokens.FunctionSearcher(tokenListWithoutNames[i])) { break; }
                    else if (tokenListWithoutNames[i] == "инт" || tokenListWithoutNames[i] == "флт" || tokenListWithoutNames[i] == "стр")
                    {
                        declaredVariablesForTest.Add(tokenListWithoutNames[i + 1], tokenListWithoutNames[i]);
                    }
                    else if (i > 0 && Tokens.FunctionNameSearcher(tokenListWithoutNames[i]) && Tokens.OpenBracketsSearcher(tokenListWithoutNames[i + 1]))
                    {
                        if (functionType.Any() && declaredVariablesForTest.Any())
                        {
                            string typeFunc = string.Empty;
                            string typeVar = string.Empty;
                            if (functionType.ContainsKey(tokenListWithoutNames[i])) typeFunc = functionType[tokenListWithoutNames[i]];
                            if (declaredVariablesForTest.ContainsKey(tokenListWithoutNames[i + 2])) typeVar = declaredVariablesForTest[tokenListWithoutNames[i + 2]];
                            if (typeFunc != typeVar) { semantError = true; richTextBox3.AppendText("Передача в функцию не того типа" + Environment.NewLine); break; }
                        }
                    }
                }

                if (!tokenListWithoutNames.Contains("#старт"))
                {
                    richTextBox3.AppendText("Основная библиотека не подключена" + Environment.NewLine);
                    semantError = true;
                }
                if (tokenListWithoutNames.Contains("стр") && !tokenListWithoutNames.Contains("#строка"))
                {
                    richTextBox3.AppendText("Отсутствует строковая библиотека" + Environment.NewLine);
                    semantError = true;
                }
            }
            /*
            if (!funcFind && (tokenListWithoutNames[i] == "инт" || tokenListWithoutNames[i] == "флт" || tokenListWithoutNames[i] == "стр"))
            {
                declaredVariablesForTest.Add(tokenListWithoutNames[i + 1], tokenListWithoutNames[i]);
            }
            if (i > 0 && Tokens.FunctionNameSearcher(tokenListWithoutNames[i]) && Tokens.OpenBracketsSearcher(tokenListWithoutNames[i + 1]))
            {
                if (i > 0 && Tokens.FunctionSearcher(tokenListWithoutNames[i - 1]))
                {
                    funcFind = true;
                    functionType.Add(tokenListWithoutNames[i], tokenListWithoutNames[i + 2]);
                }
                else
                {
                    if (functionType.Any() && declaredVariablesForTest.Any())
                    {
                        string typeFunc = functionType[tokenListWithoutNames[i]];
                        string typeVar = declaredVariablesForTest[tokenListWithoutNames[i + 2]];
                        if (typeFunc != typeVar) { richTextBox3.AppendText("Передача в функцию не того типа"); break; }
                    }
                }
            }
            */
        }
        /* этот код выглядит максимально мерзко
         * я даже спорить не буду
         */
        public void Translate()
        {
            var enterList = new List<string>();
            var outputsList = new List<string>();
            var functionsList = new List<string>();
            var functionNameList = new List<string>();
            var stringList = new List<string>();

            string doneCode = richTextBox1.Text;
            string[] codeArray = doneCode.Split(new char[] { '\r', '\n' });
            //string enterVariable = string.Empty;
            foreach (string str in codeArray) 
            {
                if (!str.Contains("/") && str.Contains("#") && str.Any()) 
                {
                    string changeStr;
                    changeStr = str.Replace("#старт;", "<iostream>").Replace("#строка;", "<string>").Replace("!вкл", "#include");
                    doneCode = doneCode.Replace(str, changeStr);
                }
                else if (!str.Contains("/") && str.Any())
                {
                    string changeStr;
                    changeStr = str.Replace("инт", "int").Replace("флт", "float").Replace("лнг", "long").Replace("стр", "string").Replace(",", ".").Replace("!основа", "using namespace std;\nvoid main()").Replace("!если", "if").Replace("!иначе", "else").Replace("!пока", "while").Replace("!вернуть", "return").Replace("<>", "!=").Replace("=>", ">=");
                    doneCode = doneCode.Replace(str, changeStr);
                }
                else if (str.Contains("/") && str.Contains("стр") && (str.IndexOf("стр") < str.IndexOf("/")))
                {
                    string changeStr;
                    changeStr = str.Replace("стр", "string");
                    doneCode = doneCode.Replace(str, changeStr);
                }
            }
            
            Regex enterRegex = new Regex(@"!вывод\(\s*[a-zA-Z]+[0-9]*\s*\)\s*\;");
            Regex outRegex = new Regex(@"!ввод\(\s*[a-zA-Z]+[0-9]*\s*\)\s*\;");
            Regex functionRegex = new Regex(@"!функция\s+.*\)");
            Regex functionNameRegex = new Regex(@"[a-zA-Z]+[0-9]*\(\s*[a-zA-Z]+[0-9]*\s*\)\;");
            Regex stringRegex = new Regex(@"\/(.+)\;");
            MatchCollection findEnters = enterRegex.Matches(doneCode);
            MatchCollection findOutputs = outRegex.Matches(doneCode);
            MatchCollection findFunctions = functionRegex.Matches(doneCode);
            MatchCollection findFunctionsNames = functionNameRegex.Matches(doneCode);
            MatchCollection findString = stringRegex.Matches(doneCode);

            foreach (Match find in findEnters)
            {
                enterList.Add(find.Value);
            }
            foreach (string enters in enterList)
            {
                Match enterVariable = Regex.Match(enters, @"(?<=\s+|\()([a-zA-Z]+[0-9]*)(?=\s+|\))");
                string pattern = @"!вывод\(\s*" + enterVariable.Value + @"\s*\)\;";
                string replacement = @"cout<<" + enterVariable.Value + @"<<endl;";
                doneCode = Regex.Replace(doneCode, pattern, replacement);
            }
            foreach (Match find in findOutputs)
            {
                outputsList.Add(find.Value);
            }
            foreach (string outputs in outputsList)
            {
                Match outputsVariable = Regex.Match(outputs, @"(?<=\s+|\()([a-zA-Z]+[0-9]*)(?=\s+|\))");
                string pattern = @"!ввод\(\s*" + outputsVariable.Value + @"\s*\)\;";
                string replacement = @"cin>>" + outputsVariable.Value + @";";
                doneCode = Regex.Replace(doneCode, pattern, replacement);
            }
            foreach (Match find in findFunctions)
            {
                functionsList.Add(find.Value);
            }
            foreach (string funcs in functionsList)
            {
                if (funcs.Contains("int")) { string funcInt = funcs.Replace("!функция", "int"); doneCode = doneCode.Replace(funcs, funcInt); doneCode = doneCode.Replace("using namespace std;\n", "using namespace std;\n" + funcInt + ";\n"); }
                else if (funcs.Contains("float")) { string funcFlt = funcs.Replace("!функция", "float"); doneCode = doneCode.Replace(funcs, funcFlt); doneCode = doneCode.Replace("using namespace std;\n", "using namespace std;\n" + funcFlt + ";\n"); }
                else if (funcs.Contains("string")) { string funcStr = funcs.Replace("!функция", "string"); doneCode = doneCode.Replace(funcs, funcStr); doneCode = doneCode.Replace("using namespace std;\n", "using namespace std;\n" + funcStr + ";\n"); }
                else if (funcs.Contains("long")) { string funcLng = funcs.Replace("!функция", "long"); doneCode = doneCode.Replace(funcs, funcLng); doneCode = doneCode.Replace("using namespace std;\n", "using namespace std;\n" + funcLng + ";\n"); }
            }
            foreach (Match find in findFunctionsNames)
            {
                functionNameList.Add(find.Value);
            }
            foreach (string funcnames in functionNameList)
            {
                if (funcnames.Contains("int") || funcnames.Contains("float") || funcnames.Contains("long") || funcnames.Contains("string")) continue;

                Match variableFunction = Regex.Match(funcnames, @"(?<=\s+|\()([a-zA-Z]+[0-9]*)(?=\s+|\))");
                string changeStr = variableFunction + " = " + funcnames;
                doneCode = doneCode.Replace(funcnames, changeStr);
            }
            foreach (Match find in findString)
            {
                stringList.Add(find.Value);
            }
            foreach (string str in stringList)
            {
                Match stringValue = Regex.Match(str, @"(?<=\/)(.+)(?=\;)");
                string pattern = @"\/\s*" + stringValue.Value + @"\s*\;";
                string replacement = @"""" + stringValue.Value + @""";";
                doneCode = Regex.Replace(doneCode, pattern, replacement);
            }

            richTextBox4.AppendText(doneCode);

        }

        private void button1_Click(object sender, EventArgs e)
        {   
            richTextBox2.Clear();
            richTextBox3.Clear();
            richTextBox4.Clear();
            lexerError = false;
            syntaxError = false;
            semantError = false;
            Lexer();   
            if (!lexerError) Syntax();
            if (!lexerError && !syntaxError) Semant();
            if (!semantError && !lexerError && !syntaxError) Translate();
        }
    }
}
