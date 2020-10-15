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
            
            //board.initBoard();
            board.printBoard();

            
        }
    }
}