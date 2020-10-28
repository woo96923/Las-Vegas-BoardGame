using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices.ComTypes;

namespace Las_Vegas
{
    public class Dice
    {
        /*
         주사위 클래스
         - 주사위와 관련된 데이터들을 관리하는 클래스이다.
           주사위를 굴리고 고르고하는 등 주사위의 데이터를 해당 클래스에서 전부 관리한다.
            + 돈도 가진다.
         */
        private int playerCount;
        private int color;
        private int colerDiceCount;
        private int whiteDiceCount;
        private int[] arrColerDice = new int[6];
        private int[] arrWhiteDice = new int[6];
        private int playerMoney;

        Random rand = new Random();//무작위 변수생성을 위한 선언

        public Dice(int playerCount, int color)//생성자
        {
            /*
             이와같이 플레이어 수가 정해지면 그에따라 주사위 분배가 달라진다. 이는 원작 라스베가스를 따른다.
             
             */
            Debug.Assert(playerCount > 0 && playerCount < 6,"Player number is Wrong");
            this.color = color; //색을 클래스를 생성할 때 지정해 주자. 순서대로 빨(0), 초(1), 파(2), 검(3), 흰(4) 순서임 R, G, BU, BL, W, 
            this.playerCount = playerCount; //플레이어 숫자에 따라 주사위 분배가 달라진다.
            if (this.playerCount <= 4)
            {
                this.colerDiceCount = 8;
                this.whiteDiceCount = 2;
            }
            else if (this.playerCount == 5)
            {
                this.colerDiceCount = 8;
            }
            this.playerMoney = 0;
        }

        public void rollDice()//주사위 굴리기(내가 가진 주사위 갯수만큼)
        {
            /*
             이 메소드를 실행하면 해당 클래스가 가지고있는 주사위 숫자만큼(칼라, 흰색 따로) 주사위를 굴려
             arrColerDice, arrWhiteDice에 주사위를 넣어준다. 이때 배열의 위치는 주사위의 눈의 값이고 해당 index에 있는 정수값은 주사위의 갯수를 의미한다.
             ex) arrWhiteDice = {0,0,1,0,0,1} => 3주사위 한개, 6주사위 1개
             */
            int temp;
            Array.Clear(arrColerDice, 0, 6);
            Array.Clear(arrWhiteDice, 0, 6);

            for (int i = 0; i < (this.colerDiceCount); i++)//칼라 주사위 굴리기
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

        /*
        아래 두 메소드는 고른 숫자의 주사위를 전체 가지고있는 주사위 숫자에서 빼주고 그 숫자만큼 반환하는 역할을 한다.
        직접적으로 이용하는 일은 없고 주사위 클래스를 Board에 인자로 넘겨서 자동으로 연산을 이루어지도록 한다.
        Board클래스의 placeDice메소드를 참고하라
         */
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

        /*
         내 클래스의 색을 반환하는 메소드이다.
         */
        public int getColor()
        {
            return this.color;
        }

        public int getDiceNum()
        {
            return this.colerDiceCount + this.whiteDiceCount;
        }
        public int getMoney()
        {
            return this.playerMoney;
        }


        public void addMoney(int money)
        {
            this.playerMoney += money;
        }
        /*
         이는 GameManagerTest를 통해서 디버깅을 하기위한 메소드이다.
         게임을 플레이할 수 있도록 콘솔창에 정보들을 출력해주는 메소드이므로
         게임로직과는 크게 상관이없다.
         */
        public void printDices()
        {
            Console.Write("Dice      ");
            for (int i = 0; i < 6; i++)
            {
                Console.Write("    "+(i+1));
            }
            Console.Write("\n");
            Console.Write("Yours     ");
            for (int i = 0; i < 6; i++)
            {
                Console.Write("    " + this.arrColerDice[i]);
                           
            }
            Console.Write("\n");
            Console.Write("White     ");
            for (int i = 0; i < 6; i++)
            {
                Console.Write("    " + this.arrWhiteDice[i]);

            }
            Console.Write("\n");
            Console.WriteLine("칼라 주사위 갯수 : " + colerDiceCount);
            Console.WriteLine("흰색 주사위 갯수 : " + whiteDiceCount);
        }

    }

    /*
    보드와 관련된 클래스. 각 카지노에 어떤색의 주사위가 얼만큼 있는지, 얼마만큼의 돈이 있는지
    관리하는 클래스이다.
     */
    public class Board
    {
        private Random rand = new Random();
        private List<int> money = new List<int>();
        private List<List<int>> casinoMoney = new List<List<int>>();
        private int[,] casinoDice = new int[6,5];
        //private ArrayList[] casinoDice = new ArrayList[6];

        /*
         MoneyCards에서 카드를 섞은다음 무작위로 초기화된 카드 리스트를 받아 적절히 카지노에 분배한다.
         이때 분배하는 방법은 원작 라스베가스의 룰을 따른다.
        */
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

        private int findMaxMoney(int casioNum)
        {
            int max=0;
            foreach (var money in casinoMoney[casioNum])
            {
                if (max < money) max = money;
            }
            casinoMoney[casioNum].Remove(max);
            return max;
        }

        private int findSameDice(int casioNum)
        {
            int max = 0;
            int diceCount;
            for (int i = 0; i < 5; i++)
            {
                diceCount = this.casinoDice[casioNum, i];
                if (max < diceCount&& diceCount != 0) max = diceCount;
                else if (max == diceCount && diceCount != 0) return diceCount;
            }
            return -1;
        }

        private int findMaxDice(int casioNum)
        {
            int max = 0;
            int coler = -1;
            for (int i = 0; i < 5; i++)
            {
                int diceCount;
                diceCount = this.casinoDice[casioNum, i];
                if (max < diceCount) 
                {
                    max = diceCount;
                    coler = i;
                }
                
                
            }
            if(coler != -1) this.casinoDice[casioNum, coler] = 0;
            return coler;
        }

        public void endGame(Dice[] players)
        {
            for (int i = 0; i < 6; i++)//주사위 중복되는거 지우는 과정
            {
                while( findSameDice(i)!= -1)
                {
                    int temp = findSameDice(i);
                    for (int j = 0; j < 5; j++)
                    {
                        if (this.casinoDice[i, j] == temp) this.casinoDice[i, j] = 0;
                    }
                    
                }
            }

            for (int i = 0; i < 6; i++) 
            {
                if (casinoMoney[i].Count == 0)
                {
                    continue;
                }
                int temp = this.findMaxDice(i);
                if(temp == -1)
                {
                    continue;
                }
                if (temp == 4) continue;
                players[temp].addMoney(findMaxMoney(i));

            }//주사위 중복되는거 지우는 과정
                

        }

        public void printScore(int playerCount, Dice[] players)
        {
            for (int i = 0; i < playerCount; i++)
            {
                if (i == 0) { Console.Write("Red       "); }
                else if (i == 1) { Console.Write("Green     "); }
                else if (i == 2) { Console.Write("Blue      "); }
                else if (i == 3) { Console.Write("Black     "); }
                else if (i == 4) { Console.Write("White     "); }
                else { Debug.Assert(true, "Wroung color Number"); }
                Console.Write("     " + players[i].getMoney());
                

                Console.Write("\n");
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

    /*
     * 카드들을 섞어주는 클래스. 
     * 보드를 초기화해주는데 사용하고 게임 도중에는 크게 사용되는 일이 없는 클래스이다.
     * 카드를 섞는 로직은 사용 언어마다, 툴 마다 다르므로 이러한 클래스와 메소드가 필요한정도로 넘어가면 될 듯 하다.
     */
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
