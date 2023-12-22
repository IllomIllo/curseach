using System;
using System.Threading.Tasks;

public class VariablesConditions
{
    bool tvintState = true;
    bool tvfloatState = true;
    bool tvlongState = true;
    bool tvstringState = true;
    bool vState = false;
    bool eqState = false;
    bool inState = false;
    bool flState = false;
    bool loState = false;
    bool strState = false;
    bool endState = false;
    
    public bool ValuesConditions(string token)
    {
        if (token == "TYPEVARIABLEINT") return tvintState;
        if (token == "TYPEVARIABLEFLOAT") return tvfloatState;
        if (token == "TYPEVARIABLELONG") return tvlongState;
        if (token == "TYPEVARIABLESTRING") return tvstringState;
        if (token == "VARIABLE") return vState;
        if (token == "EQUAL") return eqState;
        if (token == "INTNUM") return inState;
        if (token == "LONGNUM") return loState;
        if (token == "FLOATNUM") return flState;
        if (token == "STRINGVALUE") return strState;
        if (token == "ENDLINE") return endState;
        else return false;
    }

    string type = string.Empty;

    public void StateMachine(string token)
    {
        switch (token)
        {
            case "TYPEVARIABLEINT":
                tvintState = false;
                tvlongState = false;
                tvfloatState = false;
                tvstringState = false;
                vState = true;
                type = "int";
                break;
            case "TYPEVARIABLELONG":
                tvintState = false;
                tvlongState = false;
                tvfloatState = false;
                tvstringState = false;
                vState = true;
                type = "lng";
                break;
            case "TYPEVARIABLEFLOAT":
                tvintState = false;
                tvlongState = false;
                tvfloatState = false;
                tvstringState = false;
                vState = true;
                type = "flt";
                break;
            case "TYPEVARIABLESTRING":
                tvintState = false;
                tvlongState = false;
                tvfloatState = false;
                tvstringState = false;
                vState = true;
                type = "str";
                break;
            case "VARIABLE":
                vState = false;
                eqState = true;
                break;
            case "EQUAL":
                eqState = false;
                if (type == "int") inState = true;
                else if (type == "lng") { loState = true; }
                else if (type == "flt") { inState = true; loState = true; flState = true; }
                else if (type == "str") strState = true;
                break;
            case "INTNUM":
                inState = false;
                endState = true;
                break;
            case "LONGNUM":
                inState = false;
                loState = false;
                flState = false;
                endState = true;
                break;
            case "FLOATNUM":
                inState= false;
                loState = false;
                flState = false;
                endState = true;
                break;
            case "STRINGVALUE":
                strState = false;
                endState = true;
                break;
            case "ENDLINE":
                endState = false;
                tvintState = true;
                tvlongState = true;
                tvfloatState = true;
                tvstringState = true;
                break;
        }
    }
}

public class EnterConditions
{
    bool enterState = true;
    bool obState = false;
    bool vState = false;
    bool cbState = false;
    bool endState = false;

    public bool ValuesConditions(string token)
    {
        if (token == "INPUT") return enterState;
        if (token == "OPENBRACKET") return obState;
        if (token == "VARIABLE") return vState;
        if (token == "CLOSEBRACKET") return cbState;
        if (token == "ENDLINE") return endState;
        else return false;
    }

    public void StateMachine(string token)
    {
        switch (token)
        {
            case "INPUT":
                enterState = false;
                obState = true;
                break;
            case "OPENBRACKET":
                obState = false;
                vState = true;
                break;
            case "VARIABLE":
                vState = false;
                cbState = true;
                break;
            case "CLOSEBRACKET":
                cbState = false;
                endState = true;
                break;
            case "ENDLINE":
                endState = false;
                enterState = true;
                break;
        }
    }

}

public class OutputConditions
{
    bool outputState = true;
    bool obState = false;
    bool vState = false;
    bool cbState = false;
    bool endState = false;

    public bool ValuesConditions(string token)
    {
        if (token == "OUTPUT") return outputState;
        if (token == "OPENBRACKET") return obState;
        if (token == "VARIABLE") return vState;
        if (token == "CLOSEBRACKET") return cbState;
        if (token == "ENDLINE") return endState;
        else return false;
    }

    public void StateMachine(string token)
    {
        switch (token)
        {
            case "OUTPUT":
                outputState = false;
                obState = true;
                break;
            case "OPENBRACKET":
                obState = false;
                vState = true;
                break;
            case "VARIABLE":
                vState = false;
                cbState = true;
                break;
            case "CLOSEBRACKET":
                cbState = false;
                endState = true;
                break;
            case "ENDLINE":
                endState = false;
                outputState = true;
                break;
        }
    }
}

public class IncludeConditions
{
    bool incState = true;
    bool incfileState = false;
    bool endState = false;

    public bool ValuesConditions(string token)
    {
        if (token == "INCLUDE") return incState;
        if (token == "INCLUDEFILE") return incfileState;
        if (token == "ENDLINE") return endState;
        else return false;
    }

    public void StateMachine(string token)
    {
        switch (token)
        {
            case "INCLUDE":
                incState = false;
                incfileState = true;
                break;
            case "INCLUDEFILE":
                incfileState = false;
                endState = true;
                break;
            case "ENDLINE":
                endState = false;
                incState = true;
                break;
        }
    }
}

public class WhileConditions
{
    bool whState = true;
    bool obState = false;
    bool cbState = false;
    bool vState = false;
    bool inState = false;
    bool flState = false;
    bool loState = false;
    bool opState = false;
    bool opblockState = false;
    bool clblockState = false;

    int variableCount = 0;

    public bool ValuesConditions(string token)
    {
        if (token == "WHILE") return whState;
        if (token == "OPENBRACKET") return obState;
        if (token == "CLOSEBRACKET") return cbState;
        if (token == "VARIABLE") return vState;
        if (token == "INTNUM") return inState;
        if (token == "FLOATNUM") return flState;
        if (token == "LONGNUM") return loState;
        if (token == "OPERATOR") return opState;
        if (token == "OPENBLOCK") return opblockState;
        if (token == "CLOSEBLOCK") return clblockState;
        else return false;
    }

    public void StateMachine(string token)
    {
        switch (token)
        {
            case "WHILE":
                whState = false;
                obState = true;
                variableCount = 0;
                break;
            case "OPENBRACKET":
                obState = false;
                vState = true;
                break;
            case "VARIABLE":
                vState = false;
                if (variableCount == 1)
                {
                    inState = false;
                    flState = false;
                    loState = false;
                    cbState = true;
                    variableCount--;
                }
                else if (variableCount == 0)
                {
                    opState = true;
                    variableCount++;
                }
                break;
            case "OPERATOR":
                opState = false;
                vState = true;
                inState = true;
                flState = true;
                loState = true;
                break;
            case "INTNUM":
                vState = false;
                inState = false;
                loState = false;
                flState = false;
                cbState = true;
                break;
            case "LONGNUM":
                vState = false;
                inState = false;
                loState = false;
                flState = false;
                cbState = true;
                break;
            case "FLOATNUM":
                vState = false;
                inState = false;
                loState = false;
                flState = false;
                cbState = true;
                break;
            case "CLOSEBRACKET":
                cbState = false;
                opblockState = true;
                break;
            case "OPENBLOCK":
                opblockState = false;
                clblockState = true;
                break;
            case "CLOSEBLOCK":
                clblockState = false;
                obState = false;
                cbState = false;
                vState = false;
                inState = false;
                flState = false;
                loState = false;
                opState = false;
                opblockState = false;
                whState = true;
                break;
        }
    }
}

public class HeadConditions
{
    bool headState = true;
    bool opblocksState = false;
    bool clblocksState = false;

    public bool ValuesConditions(string token)
    {
        if (token == "HEAD") return headState;
        if (token == "OPENBLOCK") return opblocksState;
        if (token == "CLOSEBLOCK") return clblocksState;
        else return false;
    }

    public void StateMachine(string token)
    {
        switch (token)
        {
            case "HEAD":
                headState = false;
                opblocksState = true;
                break;
            case "OPENBLOCK":
                opblocksState = false;
                clblocksState = true;
                break;
            case "CLOSEBLOCK":
                clblocksState = false;
                break;
        }
    }
}

public class IfElseConditions
{
    bool ifState = true;
    bool elseState = false;
    bool opblocksState = false;
    bool clblocksState = false;
    bool obState = false;
    bool cbState = false;
    bool vState = false;
    bool opState = false;
    bool inState = false;
    bool flState = false;
    bool loState = false;   

    int variableCount = 0;
    int ifBlocksStatus = 0;

    public bool ValuesConditions(string token)
    {
        if (token == "IF") return ifState;
        if (token == "ELSE") return elseState;
        if (token == "OPENBRACKET") return obState;
        if (token == "CLOSEBRACKET") return cbState;
        if (token == "VARIABLE") return vState;
        if (token == "INTNUM") return inState;
        if (token == "FLOATNUM") return flState;
        if (token == "LONGNUM") return loState;
        if (token == "OPERATOR") return opState;
        if (token == "OPENBLOCK") return opblocksState;
        if (token == "CLOSEBLOCK") return clblocksState;
        else return false;
    }

    public void StateMachine(string token)
    {
        switch (token)
        {
            case "IF":
                ifState = false;
                obState = true;
                variableCount = 0;
                ifBlocksStatus = 0;
                break;
            case "OPENBRACKET":
                obState = false;
                vState = true;
                break;
            case "VARIABLE":
                vState = false;
                if (variableCount == 1)
                {
                    inState = false;
                    flState = false;
                    loState = false;
                    cbState = true;
                    variableCount--;
                }
                else if (variableCount == 0)
                {
                    opState = true;
                    variableCount++;
                }
                break;
            case "OPERATOR":
                opState = false;
                vState = true;
                inState = true;
                flState = true;
                loState = true; 
                break;
            case "INTNUM":
                vState = false;
                inState = false;
                loState = false;
                flState = false;
                cbState = true;
                break;
            case "LONGNUM":
                vState = false;
                inState = false;
                loState = false;
                flState = false;
                cbState = true;
                break;
            case "FLOATNUM":
                vState = false;
                inState = false;
                loState = false;
                flState = false;
                cbState = true;
                break;
            case "CLOSEBRACKET":
                cbState = false;
                opblocksState = true;
                break;
            case "OPENBLOCK":
                opblocksState = false;
                clblocksState = true;
                break;
            case "CLOSEBLOCK":
                clblocksState = false;
                if (ifBlocksStatus == 0)
                {
                    elseState = true;
                    ifBlocksStatus = 1;
                }
                else if (ifBlocksStatus == 1)
                {                         
                    ifState = true;
                    ifBlocksStatus = 0;
                }
                break;
            case "ELSE":
                elseState = false;
                opblocksState = true;
                break;
        }
    }
}

public class FunctionConditions
{
    bool funcState = true;
    bool funcnameState = false;
    bool opblocksState = false;
    bool clblocksState = false;
    bool obState = false;
    bool cbState = false;
    bool tvintState = false;
    bool tvfloatState = false;
    bool tvlongState = false;
    bool tvstringState = false;
    bool vState = false;
    bool retState = false;
    bool endState = false;

    //int retYep = 0;
    int varStatus = 0;

    //string type = string.Empty;

    public bool ValuesConditions(string token)
    {
        if (token == "FUNCTION") return funcState;
        if (token == "FUNCTIONNAME") return funcnameState;
        if (token == "OPENBRACKET") return obState;
        if (token == "CLOSEBRACKET") return cbState;
        if (token == "TYPEVARIABLEINT") return tvintState;
        if (token == "TYPEVARIABLEFLOAT") return tvfloatState;
        if (token == "TYPEVARIABLELONG") return tvlongState;
        if (token == "TYPEVARIABLESTRING") return tvstringState;
        if (token == "VARIABLE") return vState;
        if (token == "OPENBLOCK") return opblocksState;
        if (token == "CLOSEBLOCK") return clblocksState;
        if (token == "ENDLINE") return endState;
        if (token == "RETURN") return retState;
        else return false;
    }

    public void StateMachine(string token)
    {
        switch (token)
        {
            case "FUNCTION":
                funcState = false;
                funcnameState = true;
                break;
            case "FUNCTIONNAME":
                funcnameState = false;
                obState = true;
                break;
            case "OPENBRACKET":
                obState = false;
                tvintState = true;
                tvfloatState = true;
                tvlongState = true;
                tvstringState = true;
                break;
            case "TYPEVARIABLEINT":
                tvintState = false;
                tvlongState = true;
                tvfloatState = false;
                tvstringState = false;
                vState = true;
                break;
            case "TYPEVARIABLELONG":
                tvintState = false;
                tvlongState = true;
                tvfloatState = false;
                tvstringState = false;
                vState = true;
                break;
            case "TYPEVARIABLEFLOAT":
                tvintState = false;
                tvlongState = true;
                tvfloatState = false;
                tvstringState = false;
                vState = true;
                break;
            case "TYPEVARIABLESTRING":
                tvintState = false;
                tvlongState = true;
                tvfloatState = false;
                tvstringState = false;
                vState = true;
                break;
            case "VARIABLE":
                vState = false;
                if (varStatus == 0)
                {
                    cbState = true;
                    varStatus++;
                }
                else if (varStatus == 1)
                {
                    endState = true;
                    varStatus--;
                }
                break;
            case "CLOSEBRACKET":
                cbState = false;
                opblocksState = true;
                break;
            case "OPENBLOCK":
                opblocksState= false;
                retState = true;
                break;
            case "RETURN":
                retState = false;
                vState = true;
                break;
            case "ENDLINE":
                endState = false;
                retState = true;
                clblocksState = true;
                break;
            case "CLOSEBLOCK":
                retState = false;
                clblocksState = false;
                funcState = true;
                break;
        }
    }
}
public class FunctionNameConditions
{
    bool funcnameState = true;
    bool vState = false;
    // bool eqState = false;
    bool obState = false;
    bool cbState = false;
    bool endState = false;

    int variablesCount = 0;

    public bool ValuesConditions(string token)
    {
        if (token == "VARIABLE") return vState;
        if (token == "FUNCTIONNAME") return funcnameState;
       // if (token == "EQUAL") return eqState;
        if (token == "OPENBRACKET") return obState;
        if (token == "CLOSEBRACKET") return cbState;
        if (token == "ENDLINE") return endState;
        else return false;
    }

    public void StateMachine(string token)
    {
        switch (token)
        {
            case "FUNCTIONNAME":
                funcnameState = false;
                obState = true;
                break;   
            case "OPENBRACKET":
                obState = false;
                vState = true;
                break;
            case "VARIABLE":
                vState = false;
                cbState = true;
                break;
            case "CLOSEBRACKET":
                cbState = false;
                endState = true;
                break;
            case "ENDLINE":
                endState = false;
                funcnameState = true;
                break;
        }
    }
}
/*
public class successfulEndlinesDone
{
    bool thisDone = false;

    public bool DoneToken (string token)
    {
        if (token == "ENDLINE") thisDone = true;
        else thisDone = false;
        return thisDone;
    }
}
*/

public class successfulBlocksDone
{
    //bool thisDone = false;
    int countBlocks = 0;
    /*
    public bool DoneToken(string token)
    {
        if (token == "CLOSEBLOCK") thisDone = true;
        else thisDone = false;
        return thisDone;
    }
    */
    public int BlocksCount(string token)
    {
        if (token == "OPENBLOCK") { countBlocks++; }
        else if (token == "CLOSEBLOCK") { countBlocks--; }

        return countBlocks;
    }
}