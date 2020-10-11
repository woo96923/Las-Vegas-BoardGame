using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Las_Vegas
{
    public class Dice
    {
        private int playerNum;
        private int color;
        private int colerDiceCount;
        private int whiteDiceCount;
        private ArrayList arrColerDice = new ArrayList();
        private ArrayList arrWhiteDice = new ArrayList();

        Random rand = new Random();

        public Dice(int playerNum, int color)//생성자
        {
            Debug.Assert(playerNum > 0 && playerNum < 6,"Player number is Wrong");
            this.color = color; //색을 클래스를 생성할 때 지정해 주자
            this.playerNum = playerNum;
            if (this.playerNum <= 4)
            {
                this.colerDiceCount = 8;
                this.whiteDiceCount = 2;
            }
            else if (this.playerNum == 5)
            {
                this.colerDiceCount = 8;
            }
        }

        public int whiteDiceIs()//흰색 주사위 갯수
        {
            return this.whiteDiceCount;
        }
        public void rollDice()//주사위 굴리기(내가 가진 주사위 갯수만큼)
        {
            int temp;
            this.arrColerDice.Clear();
            this.arrWhiteDice.Clear();

            for (int i = 0; i < (this.colerDiceCount); i++)//칼라 주사위 굴리지
            {
                temp = rand.Next(1, 6);
                this.arrColerDice.Add(temp);
            }

            
            for (int i = 0; i < ( this.whiteDiceCount); i++)//흰색(공통)주사위 굴리기
            {
                temp = rand.Next(1, 6);
                this.arrWhiteDice.Add(temp);
            }

            
        }

        public int pickDice(int diceNumber)//고른 주사위 갯수만큼 주사위를 뺴고 해당 갯수를 반환
        {
            Debug.Assert(!this.arrColerDice.Contains(diceNumber)|| !this.arrWhiteDice.Contains(diceNumber), "Player don't have that number dice");
            int diceCount = 0;
            while (this.arrColerDice.Contains(diceNumber))
            {
                this.arrColerDice.Remove(diceNumber);
                this.colerDiceCount--;
                diceCount++;
            }

            while (this.arrWhiteDice.Contains(diceNumber))
            {
                this.arrWhiteDice.Remove(diceNumber);
                this.whiteDiceCount--;
                diceCount++;
            }

            return diceCount;
        }



    }

    public class Player
    { 
        
    }

    public class Board
    {
        private Random rand = new Random();
        private List<int> money = new List<int>();
        private ArrayList[] casinoMoney = new ArrayList[6];
        private ArrayList[] casinoDice = new ArrayList[6];

        public Board()
        {
            this.initCard();
            this.initBoard();
            this.initDice();
        }

        public void placeDice(int casinoNum, int diceCount)//배팅할 카지노, 주사위 갯수를 받음
        {
            //this.casinoDice[casinoNum + 1] += diceCount;
        }



        public void initBoard()
        {
            for(int i = 0; i < 6; i++)
            {
                this.casinoMoney[i].AddRange(this.drawCards());
            }

        }
        public void initDice()
        {
            for (int i = 0; i < 6; i++)
            {
                this.casinoDice[i].AddRange(new int[5] { 0, 0, 0, 0, 0 });//빨, 초, 파, 검, 흰 순서임 R, G, BU, BL, W, 

            }
        }


        public List<int> initCard()
        {

            for (int i = 0; i < 5; i++)
            {
                this.money.Add(6);
                this.money.Add(7);
                this.money.Add(8);
                this.money.Add(9);
            }
            for (int i = 0; i < 6; i++)
            {
                this.money.Add(1);
                this.money.Add(4);
                this.money.Add(5);
            }
            for (int i = 0; i < 8; i++)
            {
                this.money.Add(2);
                this.money.Add(3);
            }

            //카드 섞기
            int random1;
            int random2;

            int tmp;

            for (int index = 0; index < this.money.Count; ++index)
            {
                random1 = this.rand.Next(0, this.money.Count);
                random2 = this.rand.Next(0, this.money.Count);

                tmp = this.money[random1];
                this.money[random1] = this.money[random2];
                this.money[random2] = tmp;
            }
            //카드 섞기 출처: https://minhyeokism.tistory.com/16 [programmer-dominic.kim]

            return this.money;

        }

        public List<int> drawCards()
        {
            List<int> temp = new List<int>();
            int sum = 0;
            while (sum < 5)
            {
                temp.Add(money[0]);
                sum += money[0];
                money.RemoveAt(0);//pop money
            }

            return temp;
        }

    }
    class MoneyCards
    {
        private Random rand = new Random();
        private List<int> money = new List<int>();

        public List<int> initCard()
        {
            
            for (int i = 0; i < 5; i++)
            {
                this.money.Add(6);
                this.money.Add(7);
                this.money.Add(8);
                this.money.Add(9);
            }
            for (int i = 0; i < 6; i++)
            {
                this.money.Add(1);
                this.money.Add(4);
                this.money.Add(5);
            }
            for (int i = 0; i < 8; i++)
            {
                this.money.Add(2);
                this.money.Add(3);
            }

            //카드 섞기
            int random1;
            int random2;

            int tmp;

            for (int index = 0; index < this.money.Count; ++index)
            {
                random1 = this.rand.Next(0, this.money.Count);
                random2 = this.rand.Next(0, this.money.Count);

                tmp = this.money[random1];
                this.money[random1] = this.money[random2];
                this.money[random2] = tmp;
            }
            //카드 섞기 출처: https://minhyeokism.tistory.com/16 [programmer-dominic.kim]

            return this.money;

        }

        public List<int> drawCards()
        {
            List<int> temp = new List<int>();
            int sum = 0;
            while (sum<5)
            {
                temp.Add(money[0]);
                sum += money[0];
                money.RemoveAt(0);//pop money
            }

            return temp;
        }

    }

    class GameManager
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
