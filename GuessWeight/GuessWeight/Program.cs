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
            maxMove = 100
        }

        static PlayerBase[] users = new PlayerBase[0];
        static byte[][] moveUsers = new byte[users.Length][];
        static byte basket = (byte)new Random().Next(40, 140);
        static bool isRunPlay;

        static void Main(string[] args)
        {

            Console.WriteLine($"Вес корзины: {basket} кг");

            isRunPlay = true;

            while (isRunPlay)
            {
                Console.WriteLine($"Введите кол-во игроков (от {(byte)EnumCount.minUsers} до {(byte)EnumCount.maxUsers}):");

                string inputData = Console.ReadLine();

                if (byte.TryParse(inputData, out byte countUsers) && countUsers >= (byte)EnumCount.minUsers && countUsers <= (byte)EnumCount.maxUsers)
                {
                    FillUsers(countUsers);

                    Array.Resize(ref moveUsers, users.Length);

                    RunPlay();
                break;
                }
                else
                {
                    Console.WriteLine("Вы ввели не допустимое значение!");
                }
            }

            Console.ReadLine();
        }

        public static void FillUsers( int count)
        {
            Array.Resize(ref users, count);

            for (byte i = 0; i < users.Length; i++)
            {
                while (true)
                {
                    Console.WriteLine($"Выберите тип игрока #{i + 1}: ");
                    byte j = 0;
                    foreach (var type in Enum.GetNames(typeof(PlayerType)))
                    {
                        Console.WriteLine($"{++j} - {type}");
                    }
                    string typeInput = Console.ReadLine();
                    if (byte.TryParse(typeInput, out byte userType) && Enum.GetName(typeof(PlayerType), --userType) != null)
                    {
                        switch (userType)
                        {
                            case ((byte)PlayerType.Cheater):
                                users[i] = new Cheater();
                                users[i].PlayerType = PlayerType.Cheater;
                                break;
                            case ((byte)PlayerType.Notepad):
                                users[i] = new Notepad();
                                users[i].PlayerType = PlayerType.Notepad;
                                break;
                            case ((byte)PlayerType.Uber):
                                users[i] = new Uber();
                                users[i].PlayerType = PlayerType.Uber;
                                break;
                            case ((byte)PlayerType.UberCheater):
                                users[i] = new UberCheater();
                                users[i].PlayerType = PlayerType.UberCheater;
                                break;
                            case ((byte)PlayerType.Usual):
                                users[i] = new Usual();
                                users[i].PlayerType = PlayerType.Usual;
                                break;
                        }

                        Console.WriteLine($"Введите имя игрока #{i + 1}: ");
                        users[i].Name = Console.ReadLine();
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Вы ввели не допустимое значение!");
                    }
                }
            }
        }

        public static void FindWinner()
        {
            byte userIndex = 0;
            byte moveIndex = 0;

            for (byte k = 0; k < moveUsers.Length; k++)
            {
                for (byte l = 0; l < moveUsers[k].Length; l++)
                {
                    int markedDiff = Math.Abs(basket - moveUsers[userIndex][moveIndex]);
                    int currentDiff = Math.Abs(basket - moveUsers[k][l]);

                    if (currentDiff < markedDiff)
                    {
                        moveIndex = l;
                        userIndex = k;
                    }
                }
            }
            Console.WriteLine($"Победил игрок {users[userIndex].Name} (тип {users[userIndex].PlayerType.ToString()})! (с ближайшим значением {moveUsers[userIndex][moveIndex]})");
        }

        public static void RunPlay()
        {
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

                    move = users[j].MakeMove(moveUsers, j);

                    Array.Resize(ref moveUsers[j], moveUsers[j].Length + 1);

                    moveUsers[j][moveUsers[j].Length - 1] = move;

                    countMoves++;

                    if (move == basket)
                    {
                        Console.WriteLine($"Выиграл игрок {users[j].Name} (тип {users[j].PlayerType.ToString()})!");
                        isRunPlay = false;
                        break;
                    }
                    else if (countMoves == (byte)EnumCount.maxMove)
                    {
                        FindWinner();
                        isRunPlay = false;
                        break;
                    }

                }

                if (!isRunPlay) break;
            }
        }
    }

}
