using System;
using System.Threading;
class Trans
{
    public char replace;
    public Schag schag;
    public int newState;
    public Trans(char replace, Schag schag, int newState)
    {
        this.replace = replace; 
        this.schag = schag;   
        this.newState = newState;   
    }

}
internal enum Schag
{
    Left,
    Right
}
class Roww
{
    public char symbol;
    public Trans[] trans;
    public Roww(char symbol, Trans[] trans)
    {
        this.symbol = symbol;
        this.trans = trans; 
    }
}
class TransMatrica
{
    public Roww[] rowws;
    public TransMatrica(Roww[]rowws)
    {
        this.rowws = rowws;
    }
    public Trans GerRoww(char symbol, int state)
    {
        foreach(var roww in rowws )
        {
            if(roww.symbol == symbol)
            {
                return roww.trans[state];
            }
        }
        throw new NotImplementedException();
    }
}
class Maschinkaa_no_ne_BMW
{
    public int sHead = 0;
    public int Head { get; set; }
    public int State { get; set; }
    public char[] tapes = new char[5];
    public char[] headPos = new char[5];
    public readonly TransMatrica _transMatrica;
    public Maschinkaa_no_ne_BMW(TransMatrica transMatrica)
    {
        _transMatrica = transMatrica;
        Array.Fill(tapes, '_');
        Array.Fill(headPos, '_');
    }
    public void Inputs(int index, string str)
    {
        Pointing1:
        for(int i = 0; i<str.Length; i++)
        {
            if(i + index + tapes.Length / 4 < str.Length)
            {
                Size();
                goto Pointing1;
            }
            tapes[i + index + tapes.Length / 4] = str[i];
        }
        headPos = new char[tapes.Length];
        Array.Fill(headPos, '_');
        headPos[tapes.Length / 4] = '|';
    }
    public string Pokazstr(int index1, int length)
    {
        return new string(tapes, index1+tapes.Length/4, length);   
    }
    public string PokazHead(int index, int length, int temp)
    {
        if(temp == 0)
        {
            return new string(headPos, index+tapes.Length/4, length); 
        }
        else if(temp >= 1)
        {
            headPos[temp - 1 + index + tapes.Length / 4] = '_';
            headPos[temp + index + tapes.Length / 4] = '|';
            return new string(headPos, index+tapes.Length/4, length);
        }
        return new string(headPos, index+tapes.Length/4, length);   
    }
    public void Running()
    {
        Trans trans = _transMatrica.GerRoww(tapes[Head], State);
        tapes[Head] = trans.replace;
        if(trans.schag == Schag.Left)
        {
            Head--;
            if(Head < 0)
            {
                Size();
            }
        }
        else if(trans.schag == Schag.Right)
        {
            Head++;
            if(Head >= tapes.Length)
            {
                Size();
            }       
        }
        else
        {
            throw new NotImplementedException();
        }
        State = trans.newState;
    }
    public void SetHead(int head)
    {
        Head = head + tapes.Length / 4;
    }
    public void Size()
    {
        char[] bmw = tapes;
        tapes = new char[bmw.Length * 2];
        Array.Fill(tapes, '_');
        for(int i = tapes.Length/4; i < bmw.Length+tapes.Length/4; i++)
        {
            tapes[i] = bmw[i-tapes.Length/4];
        }
        SetHead(sHead);
        headPos = new char[tapes.Length];
        Array.Fill(tapes, '_');
        headPos[tapes.Length/4] = '|';
    }
    public string Alfa(string sTapes, char[] alfavitik)
    {
        for(int i = 0; i < sTapes.Length; i++)
        {
            for(int j = 0; j < alfavitik.Length; j++)
            {
                if(sTapes[i] == alfavitik[j])
                {
                    break;
                }else if(j == alfavitik.Length - 1 && sTapes[i] != alfavitik[j])
                {
                    Console.WriteLine("Norm dannue vvedi");
                    Console.WriteLine("Bydet takaya strochka = 1001001110110");
                    sTapes = "1001001110110";
                    return sTapes;  
                }
            }
        }
        return sTapes;
    }
}
class Program
{
    static void Main(string[] args)
    {
        char[] alfavitik = new char[] { '0', '1', '_' };
        var maschinka_no_ne_BMW = new Maschinkaa_no_ne_BMW
            (
              new TransMatrica
              (
               new Roww[]
               {
                   new Roww('0', new Trans[]{new Trans('1', Schag.Right, 0)}),
                   new Roww('1', new Trans[]{new Trans('0', Schag.Right, 0)}),
                   new Roww('_', new Trans[]{new Trans('_', Schag.Right, -1)})
                }
               )
              );

        maschinka_no_ne_BMW.SetHead(maschinka_no_ne_BMW.sHead);
        maschinka_no_ne_BMW.State = 0;
        string str = "1001001110110";
        str = maschinka_no_ne_BMW.Alfa(str, alfavitik);
        maschinka_no_ne_BMW.Inputs(0, str);
        int time = 2000;
        int temp = 0;
        Console.WriteLine(maschinka_no_ne_BMW.PokazHead(0, str.Length+1, temp));
        Console.WriteLine(maschinka_no_ne_BMW.Pokazstr(0, str.Length+1));
        while(maschinka_no_ne_BMW.State != 1)
        {
            Thread.Sleep(time);
            Console.Clear();
            maschinka_no_ne_BMW.Running();
            Console.WriteLine(maschinka_no_ne_BMW.PokazHead(0, str.Length+1, temp));
            Console.WriteLine(maschinka_no_ne_BMW.Pokazstr(0, str.Length+1));
            Console.WriteLine("");
            Thread.Sleep(time);
            Console.Clear();
            Console.WriteLine(maschinka_no_ne_BMW.PokazHead(0, str.Length+1, temp));
            Console.WriteLine(maschinka_no_ne_BMW.Pokazstr(0, str.Length+1));
            temp++;
                
        }
    }
}