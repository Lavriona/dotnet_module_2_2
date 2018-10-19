using System;
using GuessWeight.BusinessClass;

namespace GuessWeight
{
    class Program
    {
        enum EnumCount : byte
        {
            minUsers = 2,
            maxUsers = 8,
            maxMove = 3
        }

        static void Main(string[] args)
        {
            byte basket = (byte)new Random().Next(40, 140);

            Console.WriteLine($"Вес корзины: {basket} кг");

            while (true)
            {
                Console.WriteLine($"Введите кол-во игроков (от {(byte)EnumCount.minUsers} до {(byte)EnumCount.maxUsers}):");

                string inputData = Console.ReadLine();

                if (byte.TryParse(inputData, out byte countUsers) && countUsers >= (byte)EnumCount.minUsers && countUsers <= (byte)EnumCount.maxUsers)
                {
                    User[] users = new User[countUsers];
                    for (byte i = 0; i < users.Length; i++)
                    {
                        while (true)
                        {
                            Console.WriteLine($"Выберите тип игрока #{i+1}: ");
                            byte j = 0;
                            foreach (var type in Enum.GetNames(typeof(User.EnumType)))
                            {
                                Console.WriteLine($"{++j} - {type}");
                            }
                            string typeInput = Console.ReadLine();
                            if (byte.TryParse(typeInput, out byte userType) && Enum.GetName(typeof(User.EnumType), --userType) != null)
                            {
                                switch (userType)
                                {
                                    case ((byte)User.EnumType.Cheater):
                                        users[i] = new Cheater();                                     
                                        break;
                                    case ((byte)User.EnumType.Notepad):
                                        users[i] = new Notepad();
                                        break;
                                    case ((byte)User.EnumType.Uber):
                                        users[i] = new Uber();
                                        break;
                                    case ((byte)User.EnumType.UberCheater):
                                        users[i] = new UberCheater();
                                        break;
                                    case ((byte)User.EnumType.Usual):
                                        users[i] = new User();
                                        break;
                                }

                                Console.WriteLine($"Введите имя игрока #{i+1}: ");
                                users[i].Name = Console.ReadLine();
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Вы ввели не допустимое значение!");
                            }
                        }
                    }

                    byte[][] moveUsers = new byte[users.Length][];

                    byte countMoves = 0;

                    for (byte i = 1; i <= (byte)EnumCount.maxMove; i++)
                    {
                        for (byte j = 0; j < users.Length; j++)
                        {
                            byte move;

                            if (i == 1)
                            {
                                moveUsers[j] = new byte[0];
                            }

                            if ( users[j] is Uber userUber )
                            {
                                move = userUber.MakeMove(moveUsers[j]);
                            }
                            else if (users[j] is Notepad notepadUser)
                            {
                                move = notepadUser.MakeMove(moveUsers[j]);
                            }
                            else if ( users[j] is Cheater cheaterUser )
                            {
                                move = cheaterUser.MakeMove(moveUsers, j);
                            } else if ( users[j] is UberCheater uberChiterUser )
                            {
                                move = uberChiterUser.MakeMove(moveUsers, j);
                            } else
                            {
                                move = users[j].MakeMove();
                            }

                            Array.Resize(ref moveUsers[j], moveUsers[j].Length + 1);
                         
                            moveUsers[j][moveUsers[j].Length - 1] = move;
                            countMoves++;

                            if (move == basket)
                            {
                                Console.WriteLine($"Выиграл игрок {users[j].Name}!");
                                break;
                            } else if (countMoves == (byte)EnumCount.maxMove)
                            {
                                byte userIndex = 0;
                                byte moveIndex = 0;

                                for ( byte k = 0; k < moveUsers.Length; k++)
                                { 
                                    for (byte l = 0; l < moveUsers[k].Length; l++)
                                    {
                                        int markedDiff  = Math.Abs(basket - moveUsers[userIndex][moveIndex]);
                                        int currentDiff = Math.Abs(basket - moveUsers[k][l]); 

                                         if (currentDiff < markedDiff)
                                        {
                                            moveIndex = l;
                                            userIndex = k;
                                        }
                                    }
                                }
                                Console.WriteLine($"Победил игрок {users[userIndex].Name}! (с ближайшим значением {moveUsers[userIndex][moveIndex]})");
                                break;
                            }
                        }
                    }

                break;
                }
                else
                {
                    Console.WriteLine("Вы ввели не допустимое значение!");
                }
            }

            Console.ReadLine();
        }
    }
}
