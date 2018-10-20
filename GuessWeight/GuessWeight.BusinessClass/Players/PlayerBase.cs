using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessWeight.BusinessClass
{
    public abstract class PlayerBase
    {
        public string Name { get; set; }

        public PlayerType PlayerType { get; set; }

        public virtual byte MakeMove(byte[][] moves = null, byte index = 0)
        {
            return (byte)new Random().Next();
        }
    }
}
