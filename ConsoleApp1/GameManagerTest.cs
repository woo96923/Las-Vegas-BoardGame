using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

using Las_Vegas;

namespace Las_Vegas
{
    class GameManagerTest
    {
        static void Main(string[] args)
        {

            Dice test = new Dice(4, 0);
            MoneyCards cards = new MoneyCards();
            cards.initCard();
            Board board = new Board(cards.drawCards());

            string temp;
            int selectDice;
            int selectCasino;
            int WDiceNum;
            int CDiceNum;

            //board.initBoard();
            while (true)
            {
                WDiceNum = 0;
                CDiceNum = 0;
                selectDice = 0;
                selectCasino = 0;

                board.printBoard();

                test.rollDice();
                test.printDices();

                Console.WriteLine("주사위를 선택해 주세요");
                temp = Console.ReadLine();
                selectDice = Convert.ToInt32(temp);

                Console.WriteLine("수자위를 넣을 카지노를 선택해 주세요");
                temp = Console.ReadLine();
                selectCasino = Convert.ToInt32(temp);

                board.placeDice(selectCasino-1, selectDice-1, test);
                Console.WriteLine(selectCasino+"번 카지노에 "+ selectDice+" 주사위 올렸습니다.");
                Console.Clear();

            }

        }
    }
}