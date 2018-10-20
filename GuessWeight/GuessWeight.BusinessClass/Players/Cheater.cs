using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessWeight.BusinessClass
{
    public class Cheater : PlayerBase
    {
        public override byte MakeMove(byte[][] moves, byte index)
        {
            byte number = 0;
            bool tryMove = true;
            bool isRepeated = false;

            while (tryMove)
            {
                number = base.MakeMove();

                if (moves == null || moves.Length == 0)
                    break;
               
                for (byte i = 0; i < moves.Length; i++)
                {
                    if ( i == index || moves[i] == null || moves[i].Length == 0 )
                    {
                        if (i == moves.Length - 1)
                        {
                            tryMove = false;

                        } else
                        {
                           continue;
                        }          
                    }

                    if (moves[i] == null || moves[i].Length == 0)
                    {
                        break;
                    }
                    
                    for (byte j = 0; j < moves[i].Length; j++)
                    {
                        if (moves[i][j] == number)
                        {
                            isRepeated = true;
                            tryMove = false;
                            break;
                        }
                    }

                    if (isRepeated)
                    {
                        break;
                    }
                    else if (i == moves.Length - 1)
                    {
                        tryMove = false;
                    }  
                } 
            }
            return number;
        }
    }
}
