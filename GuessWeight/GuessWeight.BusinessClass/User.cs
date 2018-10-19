using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessWeight.BusinessClass
{
    public class User
    {
        public string Name { get; set; }

        public enum EnumType
        {
            Usual,
            Notepad,
            Uber,
            Cheater,
            UberCheater
        }

        public virtual byte MakeMove(byte min = 40, byte max = 140)
        {
            return (byte)new Random().Next(min, max);
        }
    }
}
