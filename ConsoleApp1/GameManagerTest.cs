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

            Dice red = new Dice(4, 0);
            Dice green = new Dice(4, 1);
            Dice[] players = { red, green };
            MoneyCards cards = new MoneyCards();
            cards.initCard();
            Board board = new Board(cards.drawCards());

            string temp;
            int selectDice;
            int selectCasino;
            int WDiceNum;
            int CDiceNum;
            int playerCount = 2;
            int turn = 0;

            
            while (true)
            {
                cards.initCard();

                board.initBoard(cards.drawCards());
                WDiceNum = 0;
                CDiceNum = 0;
                selectDice = 0;
                selectCasino = 0;

                int remainPlayer = playerCount;




                for (int i =0; i < playerCount; i= (++i)% playerCount )
                {
                    if (players[i].getDiceNum() == 0) {
                        remainPlayer--;
                        if (remainPlayer == 0) break;
                        continue;
                    }
                    remainPlayer = playerCount;
                    board.printBoard();

                    if (i==0) Console.WriteLine("Red's turn");
                    else if (i == 1) Console.WriteLine("Green's turn");
                    players[i].rollDice();
                    players[i].printDices();

                    Console.WriteLine("주사위를 선택해 주세요");
                    temp = Console.ReadLine();
                    selectDice = Convert.ToInt32(temp);

                    board.placeDice(selectDice - 1, selectDice - 1, players[i]);
                    Console.WriteLine(selectDice + "번 카지노에 " + selectDice + " 주사위 올렸습니다.");
                    Console.Clear();

                }
                board.printBoard();
                Console.WriteLine("게임이 끝났습니다.");

                board.endGame(players);

                board.printScore(playerCount, players);


                Console.WriteLine("게임을 다시 시작하시겠습니까?? 1. 재시작 2. 종료");
                temp = Console.ReadLine();
                int select = Convert.ToInt32(temp);
                if (select == 2) break; 



            }
        }
    }
}