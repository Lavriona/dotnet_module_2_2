using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessWeight.BusinessClass
{
    public class UberCheater : User
    {
        public byte MakeMove(byte[][] moves, byte index)
        {
            byte number = 39;
            bool tryMove = true;
            bool isRepeated = false; 

            while (tryMove)
            {
                number = moves != null && moves[index]  != null ? ++moves[index][moves.Length-1] : ++number;

                if ( moves != null && moves.Length > 0 )
                {
                    for (byte i = 0; i < moves.Length; i++)
                    {
                        if (i == index) continue;

                        if ( moves[i] != null && moves[i].Length > 0 )
                        {
                            for (byte j = 0; j < moves[i].Length; j++)
                            {
                                if (moves[i][j] == number)
                                {
                                    isRepeated = true;
                                    break;
                                }
                            }
                        }
                       
                        if (isRepeated)
                            break;
                        else if (i == moves.Length - 1)
                        {
                            tryMove = false;
                        }
                    }
                }
                
            }
            return number;
        }
    }
}
