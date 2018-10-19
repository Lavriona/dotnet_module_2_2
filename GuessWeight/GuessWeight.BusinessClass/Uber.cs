﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessWeight.BusinessClass
{
    public class Uber : User
    {
        public byte MakeMove(byte[] moves)
        {
            byte move = 39;
       
            if (moves != null && moves.Length > 0)
            {
                move = moves[moves.Length - 1];
            }
            return ++move;
        }
    }
}
