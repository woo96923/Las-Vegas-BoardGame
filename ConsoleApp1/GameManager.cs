using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices.ComTypes;

namespace Las_Vegas
{
    public class Dice
    {
        private int playerNum;
        private int color;
        private int colerDiceCount;
        private int whiteDiceCount;
        private int[] arrColerDice = new int[6];
        private int[] arrWhiteDice = new int[6];
       //private ArrayList arrColerDice = new ArrayList();
        //private ArrayList arrWhiteDice = new ArrayList();

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

        public int readWhiteDices()//흰색 주사위 갯수
        {
            return this.whiteDiceCount;
        }
        public void rollDice()//주사위 굴리기(내가 가진 주사위 갯수만큼)
        {
            int temp;
            Array.Clear(arrColerDice, 0, 6);
            Array.Clear(arrWhiteDice, 0, 6);

            for (int i = 0; i < (this.colerDiceCount); i++)//칼라 주사위 굴리지
            {
                temp = rand.Next(1, 6);
                this.arrColerDice[temp] += 1;
            }

            
            for (int i = 0; i < ( this.whiteDiceCount); i++)//흰색(공통)주사위 굴리기
            {
                temp = rand.Next(1, 6);
                this.arrWhiteDice[temp] += 1;
            }

            
        }

        public int pickColorDice(int diceNumber)//고른 주사위 갯수만큼 주사위를 뺴고 해당 갯수를 반환
        {
            //Debug.Assert(!this.arrColerDice.Contains(diceNumber)|| !this.arrWhiteDice.Contains(diceNumber), "Player don't have that number dice");
            int diceCount = 0;
            diceCount = this.arrColerDice[diceNumber];
            this.colerDiceCount -= diceCount;
            return diceCount;
        }

        public int pickWhiteDice(int diceNumber)//고른 주사위 갯수만큼 주사위를 뺴고 해당 갯수를 반환
        {
            //Debug.Assert(!this.arrColerDice.Contains(diceNumber) || !this.arrWhiteDice.Contains(diceNumber), "Player don't have that number dice");
            int diceCount = 0;
            diceCount = this.arrWhiteDice[diceNumber];
            this.whiteDiceCount -= diceCount;
            return diceCount;

           
        }

        public int getColor()
        {
            return this.color;
        }

        public void printDices()
        {
            Console.WriteLine("      Dice");
            for (int i = 0; i < 6; i++)
            {
                Console.WriteLine("    "+i);
            }
            Console.WriteLine("\n");
            Console.WriteLine("Yours     ");
            for (int i = 0; i < 6; i++)
            {
                Console.WriteLine("    " + this.arrColerDice[i]);
                           
            }
            Console.WriteLine("\n");
            Console.WriteLine("White     ");
            for (int i = 0; i < 6; i++)
            {
                Console.WriteLine("    " + this.arrWhiteDice[i]);

            }

        }

    }


    public class Board
    {
        private Random rand = new Random();
        private List<int> money = new List<int>();
        private List<List<int>> casinoMoney = new List<List<int>>();
        private int[,] casinoDice = new int[6,5];
        //private ArrayList[] casinoDice = new ArrayList[6];

        public Board(List<List<int>> cards)
        {
            
            this.initBoard(cards);
            this.initDice();
        }
        
        public void placeDice(int casinoNum, int selectedNum, Dice dice)//배팅할 카지노, 주사위 갯수를 받음
        {
            this.casinoDice[casinoNum,dice.getColor()] += dice.pickColorDice(selectedNum);
            this.casinoDice[casinoNum, 4] += dice.pickWhiteDice(selectedNum);

        }



        public void initBoard(List<List<int>> cards)
        {
            this.casinoMoney.Clear();
            for (int i = 0; i < 6; i++)
            {
                this.casinoMoney.Add(new List<int>());
                foreach (var ele in cards[i])
                {
                    this.casinoMoney[i].Add(ele);
                }
                
            }

        }
        public void initDice()
        {
            for (int i = 0; i < 6; i++)
            {
                for(int j=0; j < 5; j++)
                {
                    this.casinoDice[i,j] = 0;//빨, 초, 파, 검, 흰 순서임 R, G, BU, BL, W, 
                }
                

            }
        }





        public void printBoard()
        {
            for(int i =0; i < 50; i++)
            {
                Console.Write("*");
            }
            Console.Write("\n");
            Console.Write("Casino Num");
            for (int i = 0; i < 6; i++)
            {
                Console.Write("     "+(i+1));
            }
            Console.Write("\n");
            //빨, 초, 파, 검, 흰 순서임 R, G, BU, BL, W, 
            for (int i = 0; i < 5; i++)
            {
                if (i == 0) { Console.Write("Red       "); }
                else if (i == 1) { Console.Write("Green     "); }
                else if (i==2) { Console.Write("Blue      "); }
                else if (i == 3) { Console.Write("Black     "); }
                else if (i == 4) { Console.Write("White     "); }
                else { Debug.Assert(true, "Wroung color Number"); }
                for (int j = 0; j < 6; j++)
                {
                    Console.Write("     " +this.casinoDice[j,i]);
                }

                Console.Write("\n");
            }
            Console.Write("\n");

            Console.Write("Moneys    ");

            for (int i=0; i < 6; i++)
            {
                Console.Write("   ");
                foreach (var money in casinoMoney[i])
                {
                    Console.Write(" "+money);
                }
            }
            Console.Write("\n");

            for (int i = 0; i < 50; i++)
            {
                Console.Write("*");
            }
            Console.Write("\n");
        }

    }
    class MoneyCards
    {
        private Random rand = new Random();
        private List<int> money = new List<int>();

        public void initCard()
        {
            List<int> tempmoney = new List<int>();

            for (int i = 0; i < 5; i++)
            {
                tempmoney.Add(6);
                tempmoney.Add(7);
                tempmoney.Add(8);
                tempmoney.Add(9);
            }
            for (int i = 0; i < 6; i++)
            {
                tempmoney.Add(1);
                tempmoney.Add(4);
                tempmoney.Add(5);
            }
            for (int i = 0; i < 8; i++)
            {
                tempmoney.Add(2);
                tempmoney.Add(3);
            }

            //카드 섞기
            int random1;
            int random2;

            int tmp;

            for (int index = 0; index < tempmoney.Count; ++index)
            {
                random1 = this.rand.Next(0, tempmoney.Count);
                random2 = this.rand.Next(0, tempmoney.Count);

                tmp = tempmoney[random1];
                tempmoney[random1] = tempmoney[random2];
                tempmoney[random2] = tmp;
            }
            //카드 섞기 출처: https://minhyeokism.tistory.com/16 [programmer-dominic.kim]
            this.money = tempmoney;
            //return this.money;

        }

        public List <List<int>> drawCards()
        {
            List<List<int>> temp = new List<List<int>>();
            int sum = 0;

            for (int i = 0; i<6; i++)
            {
                // temp[i].Clear();
                temp.Add(new List<int>());
                sum = 0;
                while (sum<5)
                {
                    temp[i].Add(this.money[0]);
                    sum += this.money[0];
                    this.money.RemoveAt(0);//pop money
                }

            }
            return temp;
        }

    }

 }
