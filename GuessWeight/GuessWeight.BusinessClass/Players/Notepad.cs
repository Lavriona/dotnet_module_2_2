using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessWeight.BusinessClass
{
    public class Notepad : PlayerBase
    {
        public override byte MakeMove(byte[][] allMoves, byte index)
        {
            var moves = allMoves[index];
            byte number = 0;
            bool tryMove = true;

            while (tryMove)
            {
                number = base.MakeMove();

                if (moves == null || moves.Length == 0)
                    break;
              
                for (byte i = 0; i < moves.Length; i++)
                {
                    if (moves[i] == number)
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
