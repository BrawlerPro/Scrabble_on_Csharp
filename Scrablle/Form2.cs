using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scrablle
{
    public partial class GameLobby : Form
    {
        //Класс букв
        class letters
        {
            public int price;//Показывает цену буквы
            public int use = 1;//Это проверка на то была ли буква использована
            public string name;//Имя самой буквы

            //При создании экзепляра класса дает ему значение в ячейках
            public letters(string name, int price)
            {
                this.price = price;
                this.name = name;
            }
        }

        //Мешок со всеми буквами в наборе
        letters[] mas = new letters[104] {
            new letters("А", 1), new letters("А", 1), new letters("А", 1), new letters("А", 1), new letters("А", 1),
            new letters("А", 1), new letters("А", 1), new letters("А", 1), new letters("Б", 3), new letters("Б", 3),
            new letters("В", 1), new letters("В", 1), new letters("В", 1), new letters("В", 1), new letters("Г", 3),
            new letters("Г", 3), new letters("Д", 2), new letters("Д", 2), new letters("Д", 2), new letters("Д", 2),
            new letters("Е", 1), new letters("Е", 1), new letters("Е", 1), new letters("Е", 1), new letters("Е", 1),
            new letters("Е", 1), new letters("Е", 1), new letters("Е", 1), new letters("Ё", 3), new letters("Ж", 5),
            new letters("З", 5), new letters("З", 5), new letters("И", 1), new letters("И", 1), new letters("И", 1),
            new letters("И", 1), new letters("И", 1), new letters("Й", 4), new letters("К", 2), new letters("К", 2),
            new letters("К", 2), new letters("К", 2), new letters("Л", 2), new letters("Л", 2), new letters("Л", 2),
            new letters("Л", 2), new letters("М", 2), new letters("М", 2), new letters("М", 2), new letters("Н", 1),
            new letters("Н", 1), new letters("Н", 1), new letters("Н", 1), new letters("Н", 1), new letters("О", 1),
            new letters("О", 1), new letters("О", 1), new letters("О", 1), new letters("О", 1), new letters("О", 1),
            new letters("О", 1), new letters("О", 1), new letters("О", 1), new letters("О", 1), new letters("П", 2),
            new letters("П", 2), new letters("П", 2), new letters("П", 2), new letters("Р", 1), new letters("Р", 1),
            new letters("Р", 1), new letters("Р", 1), new letters("Р", 1), new letters("С", 1), new letters("С", 1),
            new letters("С", 1), new letters("С", 1), new letters("С", 1), new letters("Т", 1), new letters("Т", 1),
            new letters("Т", 1), new letters("Т", 1), new letters("Т", 1), new letters("У", 2), new letters("У", 2),
            new letters("У", 2), new letters("У", 2), new letters("Ф", 10),new letters("Х", 5), new letters("Ц", 5),
            new letters("Ч", 5), new letters("Ш", 8), new letters("Щ", 10),new letters("Ъ", 10),new letters("Ы", 4),
            new letters("Ы", 4), new letters("Ь", 3), new letters("Ь", 3), new letters("Э", 8), new letters("Ю", 8),
            new letters("Я", 3), new letters("Я", 3), new letters("_", 0), new letters("_", 0)

        };

        //Счетскик пропусков
        int skip_move = 0;

        //Счетчик использованных букв
        int k = 0;

        //Переход хода
        int move = 1;

        //Счетчик очков 
        int counter1 = 0, counter2 = 0;

        //Буквы игроков
        letters[] player1_letters = new letters[7];
        letters[] player2_letters = new letters[7];

        //Счетчик ходов
        int move_counter = 0;
        bool first_position_is_middle=false;

        //Число возможных слов за игру
        static int N = 100;
        string[] UsedWord = new string[N];
        int CountUsedWord = 0;

        //Имя файла строки
        string path = @"C:\Users\Admin\source\repos\Scrablle\resources\vocabulary.txt";


        //Пауза хода
        bool stop = false;


        //Переменные для поиска новых слов
        public string newWord = "";
        public int newWordPrice = 0;

        public GameLobby()
        {
            InitializeComponent();

            //Случайные значения для первого игрока
            for (int i = 0; i < 7; i++)
            {
                //Случайнок число
                Random random = new Random();
                int ran = random.Next(mas.Length);
                //Если случайная буква уже исользована то бери другую
                while (mas[ran].use == 0)
                {
                    ran = random.Next(mas.Length);
                }
                //Присваивает случайное число 
                player1_letters[i] = mas[ran];
                //Показывается что это буква стала использованной
                mas[ran].use = 0;
                //k - это количество использованых букв
                k++;

            }


            //Случайные значения для второго игрока
            for (int i = 0; i < 7; i++)
            {
                Random random = new Random();
                int ran = random.Next(mas.Length);

                while (mas[ran].use == 0)
                {
                    ran = random.Next(mas.Length);
                }
                player2_letters[i] = mas[ran];
                mas[ran].use = 0;
                k++;
            }

            playerbox1.Text = player1_letters[0].name;
            playerbox2.Text = player1_letters[1].name;
            playerbox3.Text = player1_letters[2].name;
            playerbox4.Text = player1_letters[3].name;
            playerbox5.Text = player1_letters[4].name;
            playerbox6.Text = player1_letters[5].name;
            playerbox7.Text = player1_letters[6].name;

            playerlabel1.Text = player1_letters[0].price.ToString();
            playerlabel2.Text = player1_letters[1].price.ToString();
            playerlabel3.Text = player1_letters[2].price.ToString();
            playerlabel4.Text = player1_letters[3].price.ToString();
            playerlabel5.Text = player1_letters[4].price.ToString();
            playerlabel6.Text = player1_letters[5].price.ToString();
            playerlabel7.Text = player1_letters[6].price.ToString();

            move_counter++;
            MoveCounter.Text = "Сейчас " + move_counter + " ход";
        }

       
        private void StartButton_Click_1(object sender, EventArgs e)
        {
            WordFinder();
            
                if (stop == false)
                {
                    string slovo = "";
                    for (int i = 1; i <= CountUsedWord; i++)
                        slovo += i + "-ое слово " + UsedWord[i - 1] + "\r\n";
                    richTextBox1.Text = slovo;

                    //move показывает какому игроку принадлежит ход
                    if (move == 1)
                    {
                        //Это пока не важно
                        counter1 += newWordPrice;
                        move = 2;
                        label1.Text = "Ход 2-ого игрока";
                        labelCounter1.Text = "Счет игрока 1: " + counter1;
                        labelCounter2.Text = "Счет игрока 2: " + counter2;

                        //Если первая ячейка пустая 
                        if (playerbox1.Text == "")
                        {
                            //Случайное число
                            Random random = new Random();
                            int ran = random.Next(mas.Length);
                            //Проверка на использованость
                            while (mas[ran].use == 0)
                            {
                                ran = random.Next(mas.Length);
                            }
                            //Присваивание значений и помечай букву как использованую
                            player1_letters[0] = mas[ran];
                            mas[ran].use = 0;
                            k++;
                        }
                        if (playerbox2.Text == "")
                        {
                            Random random = new Random();
                            int ran = random.Next(mas.Length);

                            while (mas[ran].use == 0)
                            {
                                ran = random.Next(mas.Length);
                            }
                            player1_letters[1] = mas[ran];
                            mas[ran].use = 0;
                            k++;
                        }
                        if (playerbox3.Text == "")
                        {
                            Random random = new Random();
                            int ran = random.Next(mas.Length);

                            while (mas[ran].use == 0)
                            {
                                ran = random.Next(mas.Length);
                            }
                            player1_letters[2] = mas[ran];
                            mas[ran].use = 0;
                            k++;
                        }
                        if (playerbox4.Text == "")
                        {
                            Random random = new Random();
                            int ran = random.Next(mas.Length);

                            while (mas[ran].use == 0)
                            {
                                ran = random.Next(mas.Length);
                            }
                            player1_letters[3] = mas[ran];
                            mas[ran].use = 0;
                            k++;
                        }
                        if (playerbox5.Text == "")
                        {
                            Random random = new Random();
                            int ran = random.Next(mas.Length);

                            while (mas[ran].use == 0)
                            {
                                ran = random.Next(mas.Length);
                            }
                            player1_letters[4] = mas[ran];
                            mas[ran].use = 0;
                            k++;
                        }
                        if (playerbox6.Text == "")
                        {
                            Random random = new Random();
                            int ran = random.Next(mas.Length);

                            while (mas[ran].use == 0)
                            {
                                ran = random.Next(mas.Length);
                            }
                            player1_letters[5] = mas[ran];
                            mas[ran].use = 0;
                            k++;
                        }
                        if (playerbox7.Text == "")
                        {
                            Random random = new Random();
                            int ran = random.Next(mas.Length);

                            while (mas[ran].use == 0)
                            {
                                ran = random.Next(mas.Length);
                            }
                            player1_letters[6] = mas[ran];
                            mas[ran].use = 0;
                            k++;
                        }

                        LetterChange(player2_letters);
                    }
                    else
                   if (move == 2)
                    {
                        counter2 += newWordPrice;
                        move = 1;
                        label1.Text = "Ход 1-ого игрока";
                        labelCounter1.Text = "Счет игрока 1: " + counter1;
                        labelCounter2.Text = "Счет игрока 2: " + counter2;
                        if (playerbox1.Text == "")
                        {
                            Random random = new Random();
                            int ran = random.Next(mas.Length);

                            while (mas[ran].use == 0)
                            {
                                ran = random.Next(mas.Length);
                            }
                            player2_letters[0] = mas[ran];
                            mas[ran].use = 0;
                            k++;
                        }
                        if (playerbox2.Text == "")
                        {
                            Random random = new Random();
                            int ran = random.Next(mas.Length);

                            while (mas[ran].use == 0)
                            {
                                ran = random.Next(mas.Length);
                            }
                            player2_letters[1] = mas[ran];
                            mas[ran].use = 0;
                            k++;
                        }
                        if (playerbox3.Text == "")
                        {
                            Random random = new Random();
                            int ran = random.Next(mas.Length);

                            while (mas[ran].use == 0)
                            {
                                ran = random.Next(mas.Length);
                            }
                            player2_letters[2] = mas[ran];
                            mas[ran].use = 0;
                            k++;
                        }
                        if (playerbox4.Text == "")
                        {
                            Random random = new Random();
                            int ran = random.Next(mas.Length);

                            while (mas[ran].use == 0)
                            {
                                ran = random.Next(mas.Length);
                            }
                            player2_letters[3] = mas[ran];
                            mas[ran].use = 0;
                            k++;
                        }
                        if (playerbox5.Text == "")
                        {
                            Random random = new Random();
                            int ran = random.Next(mas.Length);

                            while (mas[ran].use == 0)
                            {
                                ran = random.Next(mas.Length);
                            }
                            player2_letters[4] = mas[ran];
                            mas[ran].use = 0;
                            k++;
                        }
                        if (playerbox6.Text == "")
                        {
                            Random random = new Random();
                            int ran = random.Next(mas.Length);

                            while (mas[ran].use == 0)
                            {
                                ran = random.Next(mas.Length);
                            }
                            player2_letters[5] = mas[ran];
                            mas[ran].use = 0;
                            k++;
                        }
                        if (playerbox7.Text == "")
                        {
                            Random random = new Random();
                            int ran = random.Next(mas.Length);

                            while (mas[ran].use == 0)
                            {
                                ran = random.Next(mas.Length);
                            }
                            player2_letters[6] = mas[ran];
                            mas[ran].use = 0;
                            k++;
                        }

                        LetterChange(player1_letters);
                    }
                    skip_move = 0;
                    move_counter++;
                    MoveCounter.Text = "Сейчас " + move_counter + " ход";
                }
                stop = false;
           

        }

        private void ChangeButton_Click_1(object sender, EventArgs e)
        {
            //Проверка на каличие буквы
            if (104 - k < 7) {
                k = 0;
                for (int i = 0; i < mas.Length; i++)
                {
                    mas[i].use = 1;
                }
                EndGame();
            }

            if (move == 1)
            {
                for (int i = 0; i < 7; i++)
                {
                    Random random = new Random();
                    int ran = random.Next(mas.Length);

                    while (mas[ran].use == 0)
                    {
                        ran = random.Next(mas.Length);
                    }
                    player1_letters[i] = mas[ran];
                    mas[ran].use = 0;
                    k++;

                }
                move = 2;
                label1.Text = "Ход 2-ого игрока";
                labelCounter1.Text = "Счет игрока 1: " + counter1;
                labelCounter2.Text = "Счет игрока 2: " + counter2;

                LetterChange(player2_letters);
            }
            else
           if (move == 2)
            {
                for (int i = 0; i < 7; i++)
                {
                    Random random = new Random();
                    int ran = random.Next(mas.Length);

                    while (mas[ran].use == 0)
                    {
                        ran = random.Next(mas.Length);
                    }
                    player2_letters[i] = mas[ran];
                    mas[ran].use = 0;
                    k++;


                }
                move = 1;
                label1.Text = "Ход 1-ого игрока";
                labelCounter1.Text = "Счет игрока 1: " + counter1;
                labelCounter2.Text = "Счет игрока 2: " + counter2;

                LetterChange(player1_letters);
            }
            move_counter++;
            MoveCounter.Text = "Сейчас " + move_counter + " ход";
        }

        private void SkipButton_Click_1(object sender, EventArgs e)
        {
            
            skip_move++;
            if (skip_move == 4)
            {
                skip_move = 0;
                EndGame();
            }

            if (move == 1)
            {
                move = 2;
                label1.Text = "Ход 2-ого игрока";
                labelCounter1.Text = "Счет игрока 1: " + counter1;
                labelCounter2.Text = "Счет игрока 2: " + counter2;

                LetterChange(player2_letters);
            }
            else
            if (move == 2)
            {
                move = 1;
                label1.Text = "Ход 1-ого игрока";
                labelCounter1.Text = "Счет игрока 1: " + counter1;
                labelCounter2.Text = "Счет игрока 2: " + counter2;

                LetterChange(player1_letters);
            }
            move_counter++;
            MoveCounter.Text = "Сейчас " + move_counter + " ход";
        }


        //Смена игровых карт на руке игрока-------------------------------------====
        private void LetterChange(letters[] player)
        {
            playerbox1.Text = player[0].name;
            playerbox2.Text = player[1].name;
            playerbox3.Text = player[2].name;
            playerbox4.Text = player[3].name;
            playerbox5.Text = player[4].name;
            playerbox6.Text = player[5].name;
            playerbox7.Text = player[6].name;

            playerlabel1.Text = player[0].price.ToString();
            playerlabel2.Text = player[1].price.ToString();
            playerlabel3.Text = player[2].price.ToString();
            playerlabel4.Text = player[3].price.ToString();
            playerlabel5.Text = player[4].price.ToString();
            playerlabel6.Text = player[5].price.ToString();
            playerlabel7.Text = player[6].price.ToString();
        }
        //--------------------------------------------------------------------------


        //Создает массив игровых ячеек------------------====
        public TextBox[,] TextBoxMasiv()
        {
            TextBox[,] textBoxes = new TextBox[15, 15];
            //Это первая строка моего поля, первые 15 элементов
            textBoxes[0, 0] = r1;
            textBoxes[0, 1] = r2;
            textBoxes[0, 2] = r3;
            textBoxes[0, 3] = r4;
            textBoxes[0, 4] = r5;
            textBoxes[0, 5] = r6;
            textBoxes[0, 6] = r7;
            textBoxes[0, 7] = r8;
            textBoxes[0, 8] = r9;
            textBoxes[0, 9] = r10;
            textBoxes[0, 10] = r11;
            textBoxes[0, 11] = r12;
            textBoxes[0, 12] = r13;
            textBoxes[0, 13] = r14;
            textBoxes[0, 14] = r15;

            textBoxes[1, 0] = r16;
            textBoxes[1, 1] = r17;
            textBoxes[1, 2] = r18;
            textBoxes[1, 3] = r19;
            textBoxes[1, 4] = r20;
            textBoxes[1, 5] = r21;
            textBoxes[1, 6] = r22;
            textBoxes[1, 7] = r23;
            textBoxes[1, 8] = r24;
            textBoxes[1, 9] = r25;
            textBoxes[1, 10] = r26;
            textBoxes[1, 11] = r27;
            textBoxes[1, 12] = r28;
            textBoxes[1, 13] = r29;
            textBoxes[1, 14] = r30;

            textBoxes[2, 0] = r31;
            textBoxes[2, 1] = r32;
            textBoxes[2, 2] = r33;
            textBoxes[2, 3] = r34;
            textBoxes[2, 4] = r35;
            textBoxes[2, 5] = r36;
            textBoxes[2, 6] = r37;
            textBoxes[2, 7] = r38;
            textBoxes[2, 8] = r39;
            textBoxes[2, 9] = r40;
            textBoxes[2, 10] = r41;
            textBoxes[2, 11] = r42;
            textBoxes[2, 12] = r43;
            textBoxes[2, 13] = r44;
            textBoxes[2, 14] = r45;

            textBoxes[3, 0] = r46;
            textBoxes[3, 1] = r47;
            textBoxes[3, 2] = r48;
            textBoxes[3, 3] = r49;
            textBoxes[3, 4] = r50;
            textBoxes[3, 5] = r51;
            textBoxes[3, 6] = r52;
            textBoxes[3, 7] = r53;
            textBoxes[3, 8] = r54;
            textBoxes[3, 9] = r55;
            textBoxes[3, 10] = r56;
            textBoxes[3, 11] = r57;
            textBoxes[3, 12] = r58;
            textBoxes[3, 13] = r59;
            textBoxes[3, 14] = r60;

            textBoxes[4, 0] = r61;
            textBoxes[4, 1] = r62;
            textBoxes[4, 2] = r63;
            textBoxes[4, 3] = r64;
            textBoxes[4, 4] = r65;
            textBoxes[4, 5] = r66;
            textBoxes[4, 6] = r67;
            textBoxes[4, 7] = r68;
            textBoxes[4, 8] = r69;
            textBoxes[4, 9] = r70;
            textBoxes[4, 10] = r71;
            textBoxes[4, 11] = r72;
            textBoxes[4, 12] = r73;
            textBoxes[4, 13] = r;
            textBoxes[4, 14] = r75;

            textBoxes[5, 0] = r76;
            textBoxes[5, 1] = r77;
            textBoxes[5, 2] = r78;
            textBoxes[5, 3] = r79;
            textBoxes[5, 4] = r80;
            textBoxes[5, 5] = r81;
            textBoxes[5, 6] = r82;
            textBoxes[5, 7] = r83;
            textBoxes[5, 8] = r84;
            textBoxes[5, 9] = r85;
            textBoxes[5, 10] = r86;
            textBoxes[5, 11] = r87;
            textBoxes[5, 12] = r88;
            textBoxes[5, 13] = r89;
            textBoxes[5, 14] = r90;


            textBoxes[6, 0] = r91;
            textBoxes[6, 1] = r92;
            textBoxes[6, 2] = r93;
            textBoxes[6, 3] = r94;
            textBoxes[6, 4] = r95;
            textBoxes[6, 5] = r96;
            textBoxes[6, 6] = r97;
            textBoxes[6, 7] = r98;
            textBoxes[6, 8] = r99;
            textBoxes[6, 9] = r100;
            textBoxes[6, 10] = r101;
            textBoxes[6, 11] = r102;
            textBoxes[6, 12] = r103;
            textBoxes[6, 13] = r104;
            textBoxes[6, 14] = r105;

            textBoxes[7, 0] = r106;
            textBoxes[7, 1] = r107;
            textBoxes[7, 2] = r108;
            textBoxes[7, 3] = r109;
            textBoxes[7, 4] = r110;
            textBoxes[7, 5] = r111;
            textBoxes[7, 6] = r112;
            textBoxes[7, 7] = r113;
            textBoxes[7, 8] = r114;
            textBoxes[7, 9] = r115;
            textBoxes[7, 10] = r116;
            textBoxes[7, 11] = r117;
            textBoxes[7, 12] = r118;
            textBoxes[7, 13] = r119;
            textBoxes[7, 14] = r120;

            textBoxes[8, 0] = r121;
            textBoxes[8, 1] = r122;
            textBoxes[8, 2] = r123;
            textBoxes[8, 3] = r124;
            textBoxes[8, 4] = r125;
            textBoxes[8, 5] = r126;
            textBoxes[8, 6] = r127;
            textBoxes[8, 7] = r128;
            textBoxes[8, 8] = r129;
            textBoxes[8, 9] = r130;
            textBoxes[8, 10] = r131;
            textBoxes[8, 11] = r132;
            textBoxes[8, 12] = r133;
            textBoxes[8, 13] = r134;
            textBoxes[8, 14] = r135;

            textBoxes[9, 0] = r136;
            textBoxes[9, 1] = r137;
            textBoxes[9, 2] = r138;
            textBoxes[9, 3] = r139;
            textBoxes[9, 4] = r140;
            textBoxes[9, 5] = r141;
            textBoxes[9, 6] = r142;
            textBoxes[9, 7] = r143;
            textBoxes[9, 8] = r144;
            textBoxes[9, 9] = r145;
            textBoxes[9, 10] = r146;
            textBoxes[9, 11] = r147;
            textBoxes[9, 12] = r148;
            textBoxes[9, 13] = r149;
            textBoxes[9, 14] = r150;

            textBoxes[10, 0] = r151;
            textBoxes[10, 1] = r152;
            textBoxes[10, 2] = r153;
            textBoxes[10, 3] = r154;
            textBoxes[10, 4] = r155;
            textBoxes[10, 5] = r156;
            textBoxes[10, 6] = r157;
            textBoxes[10, 7] = r158;
            textBoxes[10, 8] = r159;
            textBoxes[10, 9] = r160;
            textBoxes[10, 10] = r161;
            textBoxes[10, 11] = r162;
            textBoxes[10, 12] = r163;
            textBoxes[10, 13] = r164;
            textBoxes[10, 14] = r165;

            textBoxes[11, 0] = r166;
            textBoxes[11, 1] = r167;
            textBoxes[11, 2] = r168;
            textBoxes[11, 3] = r169;
            textBoxes[11, 4] = r170;
            textBoxes[11, 5] = r171;
            textBoxes[11, 6] = r172;
            textBoxes[11, 7] = r173;
            textBoxes[11, 8] = r174;
            textBoxes[11, 9] = r175;
            textBoxes[11, 10] = r176;
            textBoxes[11, 11] = r177;
            textBoxes[11, 12] = r178;
            textBoxes[11, 13] = r179;
            textBoxes[11, 14] = r180;

            textBoxes[12, 0] = r181;
            textBoxes[12, 1] = r182;
            textBoxes[12, 2] = r183;
            textBoxes[12, 3] = r184;
            textBoxes[12, 4] = r185;
            textBoxes[12, 5] = r186;
            textBoxes[12, 6] = r187;
            textBoxes[12, 7] = r188;
            textBoxes[12, 8] = r189;
            textBoxes[12, 9] = r190;
            textBoxes[12, 10] = r191;
            textBoxes[12, 11] = r192;
            textBoxes[12, 12] = r193;
            textBoxes[12, 13] = r194;
            textBoxes[12, 14] = r195;

            textBoxes[13, 0] = r196;
            textBoxes[13, 1] = r197;
            textBoxes[13, 2] = r198;
            textBoxes[13, 3] = r199;
            textBoxes[13, 4] = r200;
            textBoxes[13, 5] = r201;
            textBoxes[13, 6] = r202;
            textBoxes[13, 7] = r203;
            textBoxes[13, 8] = r204;
            textBoxes[13, 9] = r205;
            textBoxes[13, 10] = r206;
            textBoxes[13, 11] = r207;
            textBoxes[13, 12] = r208;
            textBoxes[13, 13] = r209;
            textBoxes[13, 14] = r210;

            textBoxes[14, 0] = r211;
            textBoxes[14, 1] = r212;
            textBoxes[14, 2] = r213;
            textBoxes[14, 3] = r214;
            textBoxes[14, 4] = r215;
            textBoxes[14, 5] = r216;
            textBoxes[14, 6] = r217;
            textBoxes[14, 7] = r218;
            textBoxes[14, 8] = r219;
            textBoxes[14, 9] = r220;
            textBoxes[14, 10] = r221;
            textBoxes[14, 11] = r222;
            textBoxes[14, 12] = r223;
            textBoxes[14, 13] = r224;
            textBoxes[14, 14] = r225;

            return textBoxes;
        }
        //------------------------------------------------


        //Ищет слова и добавляет их в массив UsedWord---------------------------====
        public void WordFinder()
        {
            TextBox[,] tbmas = TextBoxMasiv();

            int WordPrice = 0, x2=0, x3=0;
            newWord = "";
            
            //Цикл для прохода всего игрового поля по горизонтали
            for (int i = 0; i < 15; i++)
                for (int j = 0; j < 15; j++)
                {
                    //если ячейка не пустая то сохраняем ее
                    if (tbmas[i, j].Text != "")
                    {
                        newWord += tbmas[i, j].Text;
                        WordPrice += WordPoints(tbmas[i, j],ref x2, ref x3);
                    }
                     
                    //Если предыдущая ячейка была с каким-то элементом а это оказалась пустой
                    if (tbmas[i, j].Text == "" && newWord != "")
                    {
                        //Если слово это один (Такое может произойди при наличии вертикальных слов)
                        if (newWord.Length > 1)
                        {
                            //Проверка на прохождение центральной точки
                            if (first_position_is_middle == true)
                            {
                                //Проверка на наличие слова в базе данных
                                if (CheckWordInBase(newWord) == true)
                                {
                                    if (CountUsedWord == 0)
                                    {
                                        UsedWord[CountUsedWord] = newWord;
                                        CountUsedWord++;
                                        if (x2 > 0)
                                            WordPrice *= 2;
                                        if (x3 > 0)
                                            WordPrice *= 3;
                                        newWordPrice = WordPrice;
                                        x2 = 0;
                                        x3 = 0;
                                        WordPrice = 0;
                                    }
                                    else
                                    {
                                        int CheckNewWord = 0;
                                        for (int a = 0; a < CountUsedWord; a++)
                                        {
                                            if (UsedWord[a] == newWord)
                                                CheckNewWord++;
                                        }

                                        if (CheckNewWord == 0)
                                        {
                                            UsedWord[CountUsedWord] = newWord;
                                            CountUsedWord++;
                                            if (x2 > 0)
                                                WordPrice *= 2;
                                            if (x3 > 0)
                                                WordPrice *= 3;
                                            newWordPrice = WordPrice;
                                            x2 = 0;
                                            x3 = 0;
                                            WordPrice = 0;
                                        }
                                    }
                                }
                                else
                                {
                                    DialogResult result = MessageBox.Show($"Такого слово как {newWord} нет, но если вы уверены что оно есть то, хотите ли добавить это слово в список сущестующих слов?", "Ошибка в слове!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                    if (result == DialogResult.Yes)
                                    {
                                        using (StreamWriter stream = new StreamWriter(path, true))
                                            stream.WriteLine(newWord);
                                        if (CheckWordInBase(newWord) == true)
                                        {
                                            if (CountUsedWord == 0)
                                            {
                                                UsedWord[CountUsedWord] = newWord;
                                                CountUsedWord++;
                                                if (x2 > 0)
                                                    WordPrice *= 2;
                                                if (x3 > 0)
                                                    WordPrice *= 3;
                                                newWordPrice = WordPrice;
                                                x2 = 0;
                                                x3 = 0;
                                                WordPrice = 0;
                                                stop = false;
                                            }
                                            else
                                            {
                                                int CheckNewWord = 0;
                                                for (int a = 0; a < CountUsedWord; a++)
                                                {
                                                    if (UsedWord[a] == newWord)
                                                        CheckNewWord++;
                                                }

                                                if (CheckNewWord == 0)
                                                {
                                                    UsedWord[CountUsedWord] = newWord;
                                                    CountUsedWord++;
                                                    if (x2 > 0)
                                                        WordPrice *= 2;
                                                    if (x3 > 0)
                                                        WordPrice *= 3;
                                                    newWordPrice = WordPrice;
                                                    x2 = 0;
                                                    x3 = 0;
                                                    WordPrice = 0;
                                                }
                                            }
                                        }
                                    }
                                    else if (result == DialogResult.No)
                                    {
                                        ClearArea();
                                        if (move == 1)
                                            LetterChange(player1_letters);
                                        if (move == 2)
                                            LetterChange(player2_letters);
                                        stop = true;
                                    }

                                }

                            }
                            else
                            {
                                LetterChange(player1_letters);

                                for (int s = 0; s < CountUsedWord; s++)
                                {
                                    UsedWord[s] = "";
                                }
                                CountUsedWord = 0;
                                newWord = "";
                                newWordPrice = 0;
                                ClearArea();
                                stop=true;  
                                MessageBox.Show("Первое слово должно проходить через центральный элемент! Пожалуйста переделайте!", "Нарушение правил", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            
                        }
                            
                            newWord = "";
                            x2 = 0;
                            x3 = 0;
                            WordPrice = 0;
                    }
                }

            WordPrice = 0; x2 = 0; x3 = 0;
            newWord = "";
            //Цикл для прохода всего игрового поля по вертикали
            for (int j = 0; j < 15; j++)
                for (int i = 0; i < 15; i++)
                {

                    if (tbmas[i, j].Text != "")
                    {
                        newWord += tbmas[i, j].Text;
                        WordPrice += WordPoints(tbmas[i, j], ref x2, ref x3);
                    }


                    if (tbmas[i, j].Text == "" && newWord != "")
                    {
                        if (newWord.Length > 1)
                        {
                            if (first_position_is_middle == true)
                            {
                                if (CheckWordInBase(newWord) == true)
                                {
                                    if (CountUsedWord == 0)
                                    {
                                        UsedWord[CountUsedWord] = newWord;
                                        CountUsedWord++;
                                        if (x2 > 0)
                                            WordPrice *= 2;
                                        if (x3 > 0)
                                            WordPrice *= 3;
                                        newWordPrice = WordPrice;
                                        x2 = 0;
                                        x3 = 0;
                                        WordPrice = 0;
                                    }
                                    else
                                    {
                                        int CheckNewWord = 0;
                                        for (int a = 0; a < CountUsedWord; a++)
                                        {
                                            if (UsedWord[a] == newWord)
                                                CheckNewWord++;
                                        }

                                        if (CheckNewWord == 0)
                                        {
                                            UsedWord[CountUsedWord] = newWord;
                                            CountUsedWord++;
                                            if (x2 > 0)
                                                WordPrice *= 2;
                                            if (x3 > 0)
                                                WordPrice *= 3;
                                            newWordPrice = WordPrice;
                                            x2 = 0;
                                            x3 = 0;
                                            WordPrice = 0;
                                        }
                                    }
                                }
                                else
                                {
                                    DialogResult result = MessageBox.Show($"Такого слово как {newWord} нет, но если вы уверены что оно есть то, хотите ли добавить это слово в список сущестующих слов?", "Ошибка в слове!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                    if (result == DialogResult.Yes)
                                    {
                                        using (StreamWriter stream = new StreamWriter(path, true))
                                            stream.WriteLine(newWord);
                                        if (CheckWordInBase(newWord) == true)
                                        {
                                            if (CountUsedWord == 0)
                                            {
                                                UsedWord[CountUsedWord] = newWord;
                                                CountUsedWord++;
                                                if (x2 > 0)
                                                    WordPrice *= 2;
                                                if (x3 > 0)
                                                    WordPrice *= 3;
                                                newWordPrice = WordPrice;
                                                x2 = 0;
                                                x3 = 0;
                                                WordPrice = 0;
                                                stop = false;
                                            }
                                            else
                                            {
                                                int CheckNewWord = 0;
                                                for (int a = 0; a < CountUsedWord; a++)
                                                {
                                                    if (UsedWord[a] == newWord)
                                                        CheckNewWord++;
                                                }

                                                if (CheckNewWord == 0)
                                                {
                                                    UsedWord[CountUsedWord] = newWord;
                                                    CountUsedWord++;
                                                    if (x2 > 0)
                                                        WordPrice *= 2;
                                                    if (x3 > 0)
                                                        WordPrice *= 3;
                                                    newWordPrice = WordPrice;
                                                    x2 = 0;
                                                    x3 = 0;
                                                    WordPrice = 0;
                                                }
                                            }
                                        }
                                    }
                                    else if (result == DialogResult.No)
                                    {
                                        ClearArea();
                                        if (move == 1)
                                            LetterChange(player1_letters);
                                        if (move == 2)
                                            LetterChange(player2_letters);
                                        stop = true;
                                    }

                                }

                            }
                            else
                            {
                                LetterChange(player1_letters);

                                for (int s = 0; s < CountUsedWord; s++)
                                {
                                    UsedWord[s] = "";
                                }
                                CountUsedWord = 0;
                                newWord = "";
                                newWordPrice = 0;
                                ClearArea();
                                stop = true;
                                MessageBox.Show("Первое слово должно проходить через центральный элемент! Пожалуйста переделайте!", "Нарушение правил", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }

                        }

                        newWord = "";
                        x2 = 0;
                        x3 = 0;
                        WordPrice = 0;
                    }
                }

        }
        //-------------------------------------------------------------------------


        //Дает сумму балов за слово---------------------------------------------====
        public int WordPoints(TextBox let, ref int x2, ref int x3) {
            //Проверка на первый ход в золотой клетке
            if (move_counter == 1)
            {
                if (let == r113)
                    first_position_is_middle = true;
            }

            int price = 0;
            //Нахождение цены буквы
            for(int i=0; i<mas.Length; i++)
            {
                if (let.Text == mas[i].name)
                {
                    price = mas[i].price;
                    i = mas.Length;
                }
            }
            //Проверка на прохождение ячейки с синим цветом
            if (let == r4  || let == r12  || let == r46  || let == r39  || let == r93  ||
               let == r97  || let == r60  || let == r37  || let == r53  || let == r117 ||
               let == r99  || let == r123 || let == r127 || let == r129 || let == r133 ||
               let == r103 || let == r166 || let == r173 || let == r180 || let == r187 ||
               let == r109 || let == r189 || let == r214 || let == r222)
                price *= 2;
            //Проверка на прохождение ячейки с красным цветом
            if (let == r21 || let == r25  || let == r77  ||
               let == r81  || let == r85  || let == r89  || 
               let == r137 || let == r141 || let == r145 ||
               let == r149 || let == r201 || let == r204)
                price *= 3;
            //Проверка на прохождение ячейки с желтым цветом
            if (let == r17 || let == r29  || let == r33  || let == r43  || 
               let == r49  || let == r57  || let == r65  || let == r71  ||
               let == r155 || let == r161 || let == r169 || let == r177 ||
               let == r183 || let == r193 || let == r197 || let == r209)
                x2++;
            //Проверка на прохождение ячейки с зеленым цветом
            if (let == r1  || let == r8   || let == r15  || let == r106 ||
               let == r120 || let == r211 || let == r218 || let == r225)
                x3++;

            return price;
        }
        //-------------------------------------------------------------------------


        //Проверяет есть ли слово в базе---------------------------------------====
        public bool CheckWordInBase(string slovo)
        {
            using(StreamReader stream = new StreamReader(path))
            {
                while(stream.ReadLine() != null)
                {
                    if(slovo == stream.ReadLine())
                        return true;
                }

            }

            return false;
        }
        //-------------------------------------------------------------------------


        //Временное хранилище букв
        string Saved_letters = "", Saved_Price="";


        //Взятие буквы с игровых полей во временное хранилище-------------------====
        private void playerbox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (playerbox1.Text != "" && Saved_letters == "")
            {
                Saved_letters = playerbox1.Text;
                Saved_Price = playerlabel1.Text;
                playerbox1.Text = "";
                playerlabel1.Text = "";
            }
            else
            if (playerbox1.Text == "" && Saved_letters != "")
            {
                playerbox1.Text = Saved_letters;
                playerlabel1.Text = Saved_Price;
                Saved_letters = "";
                Saved_Price = "";
            }
        }
        private void playerbox2_MouseClick(object sender, MouseEventArgs e)
        {
            if (playerbox2.Text != "" && Saved_letters == "")
            {
                Saved_letters = playerbox2.Text;
                Saved_Price = playerlabel2.Text;
                playerbox2.Text = "";
                playerlabel2.Text = "";
            }
            else
             if (playerbox2.Text == "" && Saved_letters != "")
            {
                playerbox2.Text = Saved_letters;
                playerlabel2.Text = Saved_Price;
                Saved_letters = "";
                Saved_Price = "";
            }

        }
        private void playerbox3_MouseClick(object sender, MouseEventArgs e)
        {
            if (playerbox3.Text != "" && Saved_letters == "")
            {
                Saved_letters = playerbox3.Text;
                Saved_Price = playerlabel3.Text;
                playerbox3.Text = "";
                playerlabel3.Text = "";
            }
            else
            if (playerbox3.Text == "" && Saved_letters != "")
            {
                playerbox3.Text = Saved_letters;
                playerlabel3.Text = Saved_Price;
                Saved_letters = "";
                Saved_Price = "";
            }
        }
        private void playerbox4_MouseClick(object sender, MouseEventArgs e)
        {
            if (playerbox4.Text != "" && Saved_letters == "")
            {
                Saved_letters = playerbox4.Text;
                Saved_Price = playerlabel4.Text;
                playerbox4.Text = "";
                playerlabel4.Text = "";
            }
            else
            if (playerbox4.Text == "" && Saved_letters != "")
            {
                playerbox4.Text = Saved_letters;
                playerlabel4.Text = Saved_Price;
                Saved_letters = "";
                Saved_Price = "";
            }
        }
        private void playerbox5_MouseClick(object sender, MouseEventArgs e)
        {
            if (playerbox5.Text != "" && Saved_letters == "")
            {
                Saved_letters = playerbox5.Text;
                Saved_Price = playerlabel5.Text;
                playerbox5.Text = "";
                playerlabel5.Text = "";
            }
            else
            if (playerbox5.Text == "" && Saved_letters != "")
            {
                playerbox5.Text = Saved_letters;
                playerlabel5.Text = Saved_Price;
                Saved_letters = "";
                Saved_Price = "";
            }
        }
        private void playerbox6_MouseClick(object sender, MouseEventArgs e)
        {
            if (playerbox6.Text != "" && Saved_letters == "")
            {
                Saved_letters = playerbox6.Text;
                Saved_Price = playerlabel6.Text;
                playerbox6.Text = "";
                playerlabel6.Text = "";
            }
            else
            if (playerbox6.Text == "" && Saved_letters != "")
            {
                playerbox6.Text = Saved_letters;
                playerlabel6.Text = Saved_Price;
                Saved_letters = "";
                Saved_Price = "";
            }
        }
        private void playerbox7_MouseClick(object sender, MouseEventArgs e)
        {
            if (playerbox7.Text != "" && Saved_letters == "")
            {
                Saved_letters = playerbox7.Text;
                Saved_Price = playerlabel7.Text;
                playerbox7.Text = "";
                playerlabel7.Text = "";
            }
            else
             if (playerbox7.Text == "" && Saved_letters != "")
            {
                playerbox7.Text = Saved_letters;
                playerlabel7.Text = Saved_Price;
                Saved_letters = "";
                Saved_Price = "";
            }
        }
        //--------------------------------------------------------------------------


        //Позваляет менять позицию букву только во время своего хода
        int[] move_counter_letters = new int[226];


        //Вызывает завершение игры----------------------------------------------====
        public void EndGame()
        {
            //Сравнивает количеств очков у двух игроков
            if (counter1 == counter2)
                MessageBox.Show("Ничья", "Результат", MessageBoxButtons.OK);
            if (counter1 > counter2)
                MessageBox.Show("Выиграл 1-ый игрок", "Результат", MessageBoxButtons.OK);
            if (counter1 < counter2)
                MessageBox.Show("Выиграл 2-ой игрок", "Результат", MessageBoxButtons.OK);


            this.Hide();     //Скрывается из виду игровое окно
            Form1 form1 = new Form1(); // Создается экзепляр для первого поля
            form1.Show();    //Окно правил становиться видимым

        }
        //--------------------------------------------------------------------------


        //Очистка поля---------------------------------------------------------====
        public void ClearArea()
        {
            if (move_counter_letters[1] == move_counter)
                r1.Text = "";
            if (move_counter_letters[2] == move_counter)
                r2.Text = "";
            if (move_counter_letters[3] == move_counter)
                r3.Text = "";
            if (move_counter_letters[4] == move_counter)
                r4.Text = "";
            if (move_counter_letters[5] == move_counter)
                r5.Text = "";
            if (move_counter_letters[6] == move_counter)
                r6.Text = "";
            if (move_counter_letters[7] == move_counter)
                r7.Text = "";
            if (move_counter_letters[8] == move_counter)
                r8.Text = "";
            if (move_counter_letters[9] == move_counter)
                r9.Text = "";
            if (move_counter_letters[10] == move_counter)
                r10.Text = "";
            if (move_counter_letters[11] == move_counter)
                r11.Text = "";
            if (move_counter_letters[12] == move_counter)
                r12.Text = "";
            if (move_counter_letters[13] == move_counter)
                r13.Text = "";
            if (move_counter_letters[14] == move_counter)
                r14.Text = "";
            if (move_counter_letters[15] == move_counter)
                r15.Text = "";
            if (move_counter_letters[16] == move_counter)
                r16.Text = "";
            if (move_counter_letters[17] == move_counter)
                r17.Text = "";
            if (move_counter_letters[18] == move_counter)
                r18.Text = "";
            if (move_counter_letters[19] == move_counter)
                r19.Text = "";
            if (move_counter_letters[20] == move_counter)
                r20.Text = "";
            if (move_counter_letters[21] == move_counter)
                r21.Text = "";
            if (move_counter_letters[22] == move_counter)
                r22.Text = "";
            if (move_counter_letters[23] == move_counter)
                r23.Text = "";
            if (move_counter_letters[24] == move_counter)
                r24.Text = "";
            if (move_counter_letters[25] == move_counter)
                r25.Text = "";
            if (move_counter_letters[26] == move_counter)
                r26.Text = "";
            if (move_counter_letters[27] == move_counter)
                r27.Text = "";
            if (move_counter_letters[28] == move_counter)
                r28.Text = "";
            if (move_counter_letters[29] == move_counter)
                r29.Text = "";
            if (move_counter_letters[30] == move_counter)
                r30.Text = "";
            if (move_counter_letters[31] == move_counter)
                r31.Text = "";
            if (move_counter_letters[32] == move_counter)
                r32.Text = "";
            if (move_counter_letters[33] == move_counter)
                r33.Text = "";
            if (move_counter_letters[34] == move_counter)
                r34.Text = "";
            if (move_counter_letters[35] == move_counter)
                r35.Text = "";
            if (move_counter_letters[36] == move_counter)
                r36.Text = "";
            if (move_counter_letters[37] == move_counter)
                r37.Text = "";
            if (move_counter_letters[38] == move_counter)
                r38.Text = "";
            if (move_counter_letters[39] == move_counter)
                r39.Text = "";
            if (move_counter_letters[40] == move_counter)
                r40.Text = "";
            if (move_counter_letters[41] == move_counter)
                r41.Text = "";
            if (move_counter_letters[42] == move_counter)
                r42.Text = "";
            if (move_counter_letters[43] == move_counter)
                r43.Text = "";
            if (move_counter_letters[44] == move_counter)
                r44.Text = "";
            if (move_counter_letters[45] == move_counter)
                r45.Text = "";
            if (move_counter_letters[46] == move_counter)
                r46.Text = "";
            if (move_counter_letters[47] == move_counter)
                r47.Text = "";
            if (move_counter_letters[48] == move_counter)
                r48.Text = "";
            if (move_counter_letters[49] == move_counter)
                r49.Text = "";
            if (move_counter_letters[50] == move_counter)
                r50.Text = "";
            if (move_counter_letters[51] == move_counter)
                r51.Text = "";
            if (move_counter_letters[52] == move_counter)
                r52.Text = "";
            if (move_counter_letters[53] == move_counter)
                r53.Text = "";
            if (move_counter_letters[54] == move_counter)
                r54.Text = "";
            if (move_counter_letters[55] == move_counter)
                r55.Text = "";
            if (move_counter_letters[56] == move_counter)
                r56.Text = "";
            if (move_counter_letters[57] == move_counter)
                r57.Text = "";
            if (move_counter_letters[58] == move_counter)
                r58.Text = "";
            if (move_counter_letters[59] == move_counter)
                r59.Text = "";
            if (move_counter_letters[60] == move_counter)
                r60.Text = "";
            if (move_counter_letters[61] == move_counter)
                r61.Text = "";
            if (move_counter_letters[62] == move_counter)
                r62.Text = "";
            if (move_counter_letters[63] == move_counter)
                r63.Text = "";
            if (move_counter_letters[64] == move_counter)
                r64.Text = "";
            if (move_counter_letters[65] == move_counter)
                r65.Text = "";
            if (move_counter_letters[66] == move_counter)
                r66.Text = "";
            if (move_counter_letters[67] == move_counter)
                r67.Text = "";
            if (move_counter_letters[68] == move_counter)
                r68.Text = "";
            if (move_counter_letters[69] == move_counter)
                r69.Text = "";
            if (move_counter_letters[70] == move_counter)
                r70.Text = "";
            if (move_counter_letters[71] == move_counter)
                r71.Text = "";
            if (move_counter_letters[72] == move_counter)
                r72.Text = "";
            if (move_counter_letters[73] == move_counter)
                r73.Text = "";
            if (move_counter_letters[74] == move_counter)
                r.Text = "";
            if (move_counter_letters[75] == move_counter)
                r75.Text = "";
            if (move_counter_letters[76] == move_counter)
                r76.Text = "";
            if (move_counter_letters[77] == move_counter)
                r77.Text = "";
            if (move_counter_letters[78] == move_counter)
                r78.Text = "";
            if (move_counter_letters[79] == move_counter)
                r79.Text = "";
            if (move_counter_letters[80] == move_counter)
                r80.Text = "";
            if (move_counter_letters[81] == move_counter)
                r81.Text = "";
            if (move_counter_letters[82] == move_counter)
                r82.Text = "";
            if (move_counter_letters[83] == move_counter)
                r83.Text = "";
            if (move_counter_letters[84] == move_counter)
                r84.Text = "";
            if (move_counter_letters[85] == move_counter)
                r85.Text = "";
            if (move_counter_letters[86] == move_counter)
                r86.Text = "";
            if (move_counter_letters[87] == move_counter)
                r87.Text = "";
            if (move_counter_letters[88] == move_counter)
                r88.Text = "";
            if (move_counter_letters[89] == move_counter)
                r89.Text = "";
            if (move_counter_letters[90] == move_counter)
                r90.Text = "";
            if (move_counter_letters[91] == move_counter)
                r91.Text = "";
            if (move_counter_letters[92] == move_counter)
                r92.Text = "";
            if (move_counter_letters[93] == move_counter)
                r93.Text = "";
            if (move_counter_letters[94] == move_counter)
                r94.Text = "";
            if (move_counter_letters[95] == move_counter)
                r95.Text = "";
            if (move_counter_letters[96] == move_counter)
                r96.Text = "";
            if (move_counter_letters[97] == move_counter)
                r97.Text = "";
            if (move_counter_letters[98] == move_counter)
                r98.Text = "";
            if (move_counter_letters[99] == move_counter)
                r99.Text = "";
            if (move_counter_letters[100] == move_counter)
                r100.Text = "";
            if (move_counter_letters[101] == move_counter)
                r101.Text = "";
            if (move_counter_letters[102] == move_counter)
                r102.Text = "";
            if (move_counter_letters[103] == move_counter)
                r103.Text = "";
            if (move_counter_letters[104] == move_counter)
                r104.Text = "";
            if (move_counter_letters[105] == move_counter)
                r105.Text = "";
            if (move_counter_letters[106] == move_counter)
                r106.Text = "";
            if (move_counter_letters[107] == move_counter)
                r107.Text = "";
            if (move_counter_letters[108] == move_counter)
                r108.Text = "";
            if (move_counter_letters[109] == move_counter)
                r109.Text = "";
            if (move_counter_letters[110] == move_counter)
                r110.Text = "";
            if (move_counter_letters[111] == move_counter)
                r111.Text = "";
            if (move_counter_letters[112] == move_counter)
                r112.Text = "";
            if (move_counter_letters[113] == move_counter)
                r113.Text = "";
            if (move_counter_letters[114] == move_counter)
                r114.Text = "";
            if (move_counter_letters[115] == move_counter)
                r115.Text = "";
            if (move_counter_letters[116] == move_counter)
                r116.Text = "";
            if (move_counter_letters[117] == move_counter)
                r117.Text = "";
            if (move_counter_letters[118] == move_counter)
                r118.Text = "";
            if (move_counter_letters[119] == move_counter)
                r119.Text = "";
            if (move_counter_letters[120] == move_counter)
                r120.Text = "";
            if (move_counter_letters[121] == move_counter)
                r121.Text = "";
            if (move_counter_letters[122] == move_counter)
                r122.Text = "";
            if (move_counter_letters[123] == move_counter)
                r123.Text = "";
            if (move_counter_letters[124] == move_counter)
                r124.Text = "";
            if (move_counter_letters[125] == move_counter)
                r125.Text = "";
            if (move_counter_letters[126] == move_counter)
                r126.Text = "";
            if (move_counter_letters[127] == move_counter)
                r127.Text = "";
            if (move_counter_letters[128] == move_counter)
                r128.Text = "";
            if (move_counter_letters[129] == move_counter)
                r129.Text = "";
            if (move_counter_letters[130] == move_counter)
                r130.Text = "";
            if (move_counter_letters[131] == move_counter)
                r131.Text = "";
            if (move_counter_letters[132] == move_counter)
                r132.Text = "";
            if (move_counter_letters[133] == move_counter)
                r133.Text = "";
            if (move_counter_letters[134] == move_counter)
                r134.Text = "";
            if (move_counter_letters[135] == move_counter)
                r135.Text = "";
            if (move_counter_letters[136] == move_counter)
                r136.Text = "";
            if (move_counter_letters[137] == move_counter)
                r137.Text = "";
            if (move_counter_letters[138] == move_counter)
                r138.Text = "";
            if (move_counter_letters[139] == move_counter)
                r139.Text = "";
            if (move_counter_letters[140] == move_counter)
                r140.Text = "";
            if (move_counter_letters[141] == move_counter)
                r141.Text = "";
            if (move_counter_letters[142] == move_counter)
                r142.Text = "";
            if (move_counter_letters[143] == move_counter)
                r143.Text = "";
            if (move_counter_letters[144] == move_counter)
                r144.Text = "";
            if (move_counter_letters[145] == move_counter)
                r145.Text = "";
            if (move_counter_letters[146] == move_counter)
                r146.Text = "";
            if (move_counter_letters[147] == move_counter)
                r147.Text = "";
            if (move_counter_letters[148] == move_counter)
                r148.Text = "";
            if (move_counter_letters[149] == move_counter)
                r149.Text = "";
            if (move_counter_letters[150] == move_counter)
                r150.Text = "";
            if (move_counter_letters[151] == move_counter)
                r51.Text = "";
            if (move_counter_letters[152] == move_counter)
                r152.Text = "";
            if (move_counter_letters[153] == move_counter)
                r153.Text = "";
            if (move_counter_letters[154] == move_counter)
                r154.Text = "";
            if (move_counter_letters[155] == move_counter)
                r155.Text = "";
            if (move_counter_letters[156] == move_counter)
                r156.Text = "";
            if (move_counter_letters[157] == move_counter)
                r157.Text = "";
            if (move_counter_letters[158] == move_counter)
                r158.Text = "";
            if (move_counter_letters[159] == move_counter)
                r159.Text = "";
            if (move_counter_letters[160] == move_counter)
                r160.Text = "";
            if (move_counter_letters[161] == move_counter)
                r161.Text = "";
            if (move_counter_letters[162] == move_counter)
                r162.Text = "";
            if (move_counter_letters[163] == move_counter)
                r163.Text = "";
            if (move_counter_letters[164] == move_counter)
                r164.Text = "";
            if (move_counter_letters[165] == move_counter)
                r165.Text = "";
            if (move_counter_letters[166] == move_counter)
                r166.Text = "";
            if (move_counter_letters[167] == move_counter)
                r167.Text = "";
            if (move_counter_letters[168] == move_counter)
                r168.Text = "";
            if (move_counter_letters[169] == move_counter)
                r169.Text = "";
            if (move_counter_letters[170] == move_counter)
                r170.Text = "";
            if (move_counter_letters[171] == move_counter)
                r171.Text = "";
            if (move_counter_letters[172] == move_counter)
                r172.Text = "";
            if (move_counter_letters[173] == move_counter)
                r173.Text = "";
            if (move_counter_letters[174] == move_counter)
                r.Text = "";
            if (move_counter_letters[175] == move_counter)
                r175.Text = "";
            if (move_counter_letters[176] == move_counter)
                r176.Text = "";
            if (move_counter_letters[177] == move_counter)
                r177.Text = "";
            if (move_counter_letters[178] == move_counter)
                r178.Text = "";
            if (move_counter_letters[179] == move_counter)
                r179.Text = "";
            if (move_counter_letters[180] == move_counter)
                r80.Text = "";
            if (move_counter_letters[181] == move_counter)
                r181.Text = "";
            if (move_counter_letters[182] == move_counter)
                r182.Text = "";
            if (move_counter_letters[183] == move_counter)
                r183.Text = "";
            if (move_counter_letters[184] == move_counter)
                r184.Text = "";
            if (move_counter_letters[185] == move_counter)
                r185.Text = "";
            if (move_counter_letters[186] == move_counter)
                r186.Text = "";
            if (move_counter_letters[187] == move_counter)
                r187.Text = "";
            if (move_counter_letters[188] == move_counter)
                r188.Text = "";
            if (move_counter_letters[189] == move_counter)
                r189.Text = "";
            if (move_counter_letters[190] == move_counter)
                r190.Text = "";
            if (move_counter_letters[191] == move_counter)
                r191.Text = "";
            if (move_counter_letters[192] == move_counter)
                r192.Text = "";
            if (move_counter_letters[193] == move_counter)
                r193.Text = "";
            if (move_counter_letters[194] == move_counter)
                r194.Text = "";
            if (move_counter_letters[195] == move_counter)
                r195.Text = "";
            if (move_counter_letters[196] == move_counter)
                r196.Text = "";
            if (move_counter_letters[197] == move_counter)
                r197.Text = "";
            if (move_counter_letters[198] == move_counter)
                r198.Text = "";
            if (move_counter_letters[199] == move_counter)
                r199.Text = "";
            if (move_counter_letters[200] == move_counter)
                r200.Text = "";
            if (move_counter_letters[201] == move_counter)
                r201.Text = "";
            if (move_counter_letters[202] == move_counter)
                r202.Text = "";
            if (move_counter_letters[203] == move_counter)
                r203.Text = "";
            if (move_counter_letters[204] == move_counter)
                r204.Text = "";
            if (move_counter_letters[205] == move_counter)
                r205.Text = "";
            if (move_counter_letters[206] == move_counter)
                r206.Text = "";
            if (move_counter_letters[207] == move_counter)
                r207.Text = "";
            if (move_counter_letters[208] == move_counter)
                r208.Text = "";
            if (move_counter_letters[209] == move_counter)
                r209.Text = "";
            if (move_counter_letters[210] == move_counter)
                r210.Text = "";
            if (move_counter_letters[211] == move_counter)
                r211.Text = "";
            if (move_counter_letters[212] == move_counter)
                r212.Text = "";
            if (move_counter_letters[213] == move_counter)
                r213.Text = "";
            if (move_counter_letters[214] == move_counter)
                r214.Text = "";
            if (move_counter_letters[215] == move_counter)
                r215.Text = "";
            if (move_counter_letters[216] == move_counter)
                r216.Text = "";
            if (move_counter_letters[217] == move_counter)
                r217.Text = "";
            if (move_counter_letters[218] == move_counter)
                r218.Text = "";
            if (move_counter_letters[219] == move_counter)
                r219.Text = "";
            if (move_counter_letters[220] == move_counter)
                r220.Text = "";
            if (move_counter_letters[221] == move_counter)
                r221.Text = "";
            if (move_counter_letters[222] == move_counter)
                r222.Text = "";
            if (move_counter_letters[223] == move_counter)
                r223.Text = "";
            if (move_counter_letters[224] == move_counter)
                r224.Text = "";
            if (move_counter_letters[225] == move_counter)
                r225.Text = "";
        }
        //-------------------------------------------------------------------------


        //Перенос текста с сейва в любую точку на поле--------------------------====
        public void letter_wrap(TextBox name, int n)
        {
            if (name.Text == "" && Saved_letters != "")
            {
                name.Text = Saved_letters;
                Saved_letters = "";
                move_counter_letters[n] = move_counter;
            }
            else
            if (name.Text != "" && Saved_letters == "" && move_counter_letters[n] == move_counter)
            {
                Saved_letters = name.Text;
                name.Text = "";
            }
        }

        private void r1_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r1,1);
        }
        private void r2_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r2,2);
        }
        private void r3_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r3,3);
        }
        private void r4_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r4,4);
        }
        private void r5_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r5,5);
        }
        private void r6_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r6,6);
        }
        private void r7_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r7,7);
        }
        private void r8_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r8,8);
        }
        private void r9_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r9,9);
        }
        private void r10_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r10,10);
        }
        private void r11_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r11, 11);
        }
        private void r12_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r12, 12);
        }
        private void r13_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r13, 13);
        }
        private void r14_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r14, 14);
        }
        private void r15_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r15, 15);
        }
        private void r16_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r16, 16);
        }
        private void r17_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r17, 17);
        }
        private void r18_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r18, 18);
        }
        private void r19_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r19, 19);
        }
        private void r20_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r20, 20);
        }
        private void r21_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r21, 21);
        }
        private void r22_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r22, 22);
        }
        private void r23_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r23, 23);
        }
        private void r24_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r24, 24);
        }
        private void r25_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r25, 25);
        }
        private void r26_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r26, 26);
        }
        private void r27_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r27, 27);
        }
        private void r28_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r28, 28);
        }
        private void r29_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r29, 29);
        }
        private void r30_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r30, 30);
        }
        private void r31_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r31, 31);
        }
        private void r32_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r32, 32);
        }
        private void r33_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r33, 33);
        }
        private void r34_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r34, 34);
        }
        private void r35_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r35, 35);
        }
        private void r36_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r36, 36);
        }
        private void r37_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r37, 37);
        }
        private void r38_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r38, 38);
        }
        private void r39_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r39, 39);
        }
        private void r40_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r40, 40);
        }
        private void r41_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r41, 41);
        }
        private void r42_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r42, 42);
        }
        private void r43_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r43, 43);
        }
        private void r44_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r44, 44);
        }
        private void r45_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r45, 45);
        }
        private void r46_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r46, 46);
        }
        private void r47_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r47, 47);
        }
        private void r48_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r48, 48);
        }
        private void r49_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r49, 49);
        }
        private void r50_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r50, 50);
        }
        private void r51_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r51, 51);
        }
        private void r52_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r52, 52);
        }
        private void r53_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r53, 53);
        }
        private void r54_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r54, 54);
        }
        private void r55_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r55, 55);
        }
        private void r56_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r56, 56);
        }
        private void r57_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r57, 57);
        }
        private void r58_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r58, 58);
        }
        private void r59_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r59, 59);
        }
        private void r60_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r60, 60);
        }
        private void r61_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r61, 61);
        }
        private void r62_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r62, 62);
        }
        private void r63_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r63, 63);
        }
        private void r64_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r64, 64);
        }
        private void r65_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r65, 65);
        }
        private void r66_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r66, 66);
        }
        private void r67_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r67, 67);
        }
        private void r68_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r68, 68);
        }
        private void r69_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r69, 69);
        }
        private void r70_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r70, 70);
        }
        private void r71_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r71, 71);
        }
        private void r72_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r72, 72);
        }
        private void r73_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r73, 73);
        }
        private void r_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r, 74);
        }
        private void r75_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r75, 75);
        }
        private void r76_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r76, 76);
        }
        private void r77_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r77, 77);
        }
        private void r78_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r78, 78);
        }
        private void r79_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r79, 79);
        }
        private void r80_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r80, 80);
        }
        private void r81_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r81, 81);
        }
        private void r82_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r82, 82);
        }
        private void r83_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r83, 83);
        }
        private void r84_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r84, 84);
        }
        private void r85_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r85, 85);
        }
        private void r86_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r86, 86);
        }
        private void r87_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r87, 87);
        }
        private void r88_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r88, 88);
        }
        private void r89_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r89, 89);
        }
        private void r90_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r90, 90);
        }
        private void r91_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r91, 91);
        }
        private void r92_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r92, 92);
        }
        private void r93_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r93, 93);
        }
        private void r94_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r94, 94);
        }
        private void r95_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r95, 95);
        }
        private void r96_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r96, 96);
        }
        private void r97_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r97, 97);
        }
        private void r98_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r98, 98);
        }
        private void r99_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r99, 99);
        }
        private void r100_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r100, 100);
        }
        private void r101_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r101, 101);
        }
        private void r102_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r102, 102);
        }
        private void r103_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r103, 103);
        }
        private void r104_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r104, 104);
        }
        private void r105_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r105, 105);
        }
        private void r106_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r106, 106);
        }
        private void r107_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r107, 107);
        }
        private void r108_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r108, 108);
        }
        private void r109_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r109, 109);
        }
        private void r110_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r110, 110);
        }
        private void r111_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r111, 111);
        }
        private void r112_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r112, 112);
        }
        private void r113_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r113, 113);
        }
        private void r114_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r114, 114);
                    }
        private void r115_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r115, 115);
        }
        private void r116_MouseClick(object sender, MouseEventArgs e)
        {

            letter_wrap(r116, 116);
        }
        private void r117_MouseClick(object sender, MouseEventArgs e)
        {

            letter_wrap(r117, 117);
        }
        private void r118_MouseClick(object sender, MouseEventArgs e)
        {

            letter_wrap(r118, 118);
        }
        private void r119_MouseClick(object sender, MouseEventArgs e)
        {

            letter_wrap(r119, 119);
        }
        private void r120_MouseClick(object sender, MouseEventArgs e)
        {

            letter_wrap(r120, 120);
        }
        private void r121_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r121, 121);

        }
        private void r122_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r122, 122);
        }
        private void r123_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r123, 123);
        }
        private void r124_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r124, 124);
        }
        private void r125_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r125, 125);
        }
        private void r126_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r126, 126);
        }
        private void r127_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r127, 127);
        }
        private void r128_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r128, 128);
        }
        private void r129_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r129, 129);
        }
        private void r130_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r130, 130);
        }
        private void r131_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r131, 131);
        }
        private void r132_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r132, 132);
        }
        private void r133_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r133, 133);
        }
        private void r134_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r134, 134);
        }   
        private void r135_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r135, 135);
        }
        private void r136_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r136, 136);
        }
        private void r137_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r137, 137);
        }
        private void r138_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r138, 1358);
        }
        private void r139_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r139, 139);
        }
        private void r140_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r140, 140);
        }
        private void r141_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r141, 141);
        }
        private void r142_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r142, 142);
        }
        private void r143_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r143, 143);
        }
        private void r144_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r144, 144);
        }
        private void r145_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r145, 145);
        }
        private void r146_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r146, 146);
        }
        private void r147_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r147, 147);
        }
        private void r148_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r148, 148);
        }
        private void r149_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r149, 149);
        }
        private void r150_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r150, 150);
        }
        private void r151_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r151, 151);
        }
        private void r152_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r152, 152);
        }
        private void r153_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r153, 153);
        }
        private void r154_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r154, 154);
        }
        private void r155_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r155, 155);
        }
        private void r156_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r156, 156);
        }
        private void r157_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r157, 157);
        }
        private void r158_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r158, 158);
        }
        private void r159_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r159, 159);
        }
        private void r160_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r160, 160);
        }
        private void r161_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r161, 161);
        }
        private void r162_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r162, 162);
        }
        private void r163_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r163, 163);
        }
        private void r164_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r164, 164);
        }
        private void r165_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r165, 165);
        }
        private void r166_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r166, 166);
        }
        private void r167_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r167, 167);
        }
        private void r168_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r168, 168);
        }
        private void r169_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r169, 169);
        }
        private void r170_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r170, 170);
        }
        private void r171_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r171, 171);
        }
        private void r172_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r172, 172);
        }
        private void r173_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r173, 173);
        }
        private void r174_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r174, 174);
        }
        private void r175_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r175, 175);
        }
        private void r176_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r176, 176);
        }
        private void r177_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r177, 177);
        }
        private void r178_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r178, 178);
        }
        private void r179_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r179, 179);
        }
        private void r180_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r180, 180);
        }
        private void r181_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r181, 181);
        }
        private void r182_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r182, 182);
        }
        private void r183_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r183, 183);
        }
        private void r184_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r184, 184);
        }
        private void r185_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r185, 185);

        }
        private void r186_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r186, 186);
        }
        private void r187_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r187, 187);
        }
        private void r188_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r188, 188);
        }
        private void r189_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r189, 189);
        }
        private void r190_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r190, 190);
        }
        private void r191_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r191, 191);
        }
        private void r192_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r192, 192);
        }
        private void r193_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r193, 193);
        }
        private void r194_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r194, 194);
        }
        private void r195_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r195, 195);
        }
        private void r196_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r196, 196);
        }
        private void r197_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r197, 197);
        }
        private void r198_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r198, 198);
        }
        private void r199_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r199, 199);
        }
        private void r200_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r200, 200);
        }
        private void r201_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r201, 201);
        }
        private void r202_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r202, 202);
        }
        private void r203_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r203, 203);
        }
        private void r204_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r204, 204);
        }
        private void r205_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r205, 205);
        }
        private void r206_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r206, 206);
        }
        private void r207_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r207, 207);
        }
        private void r208_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r208, 208);
        }
        private void r209_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r209, 209);
        }
        private void r210_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r210, 210);
        }
        private void r211_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r211, 211);
        }
        private void r212_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r212, 212);
        }
        private void r213_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r213, 213);
        }
        private void r214_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r214, 214);
        }
        private void r215_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r215, 215);
        }
        private void r216_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r216, 216);
        }
        private void r217_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r217, 217);
        }
        private void r218_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r218, 218);
        }
        private void r219_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r219, 219);
        }
        private void r220_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r220, 220);
        }
        private void r221_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r221, 221);
        }
        private void r222_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r222, 222);
        }
        private void r223_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r223, 223);
        }
        private void r224_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r224, 224);
        }
        private void r225_MouseClick(object sender, MouseEventArgs e)
        {
            letter_wrap(r225, 225);
        }
        
        //-----------------------------------------------------------------------------------------
        

        private void GameLobby_Load(object sender, EventArgs e)
        {

        }
       
    }
}
