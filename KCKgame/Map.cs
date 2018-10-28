using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCKgame
{
    class Map
    {
        Player character = new Player();

        Item currentSword;
        Item currentArmor;
        Item currentArtefact;

        public Map() { }

        public static int HEIGHT = 100;                 //Uustalenie wielkosci mapy
        public static int WIDTH = 100;
        Terrain[][] stage = new Terrain[HEIGHT][];      //Stworzenie tablicy przechowujacej teren
        Entity[][] beings = new Entity[HEIGHT][];       //Stworzenie tablicy przechowujacej istoty zywe
        int currentPosX = 5;                            //Inicjalizowanie podstawowych wartosci
        int currentPosY = 5;                            
        int allInventory = 1;
        int currentInventory = 1;
        Item[] inventory = new Item[99];

        private void AddRoom(int a1, int b1, int a2, int b2) //Funkcja ta przyjmuje wspolrzedne gornego lewego i dolnego prawego rogu
        {                                                    //i rysuje na tej podstawie pokoj, otaczajac go scianami i wypelniajac wolna przestrzenia
            for (int i = b1; i < b2 + 1; i++)
                stage[a1][i] = new Wall();
            for (int i = a1 + 1; i < a2 + 1; i++)
                stage[i][b1] = new Wall();
            for (int i = a1 + 1; i < a2 + 1; i++)
                stage[i][b2] = new Wall();
            for (int i = b1 + 1; i < b2; i++)
                stage[a2][i] = new Wall();

            for (int i = a1 + 1; i < a2; i++)
                for (int j = b1 + 1; j < b2; j++)
                    stage[i][j] = new Empty();


        }

        public void MovePlayerY(int y)  //Funkcja sluzy do przemieszczania postaci w gore lub w dol
        {
            if (!(currentPosY == HEIGHT - 1 && y > 0)                       //sprawdzenie, czy gracz chce przejsc za bardzo w dol
                && !(currentPosY == 0 && y < 0)                             //sprawdzenie, czy gracz chce przejsc za bardzo w gore
                && (stage[currentPosY+y][currentPosX].Enterable())          //sprawdzenie, czy mozna wchodzic na teren, na ktory chce wejsc gracz
                && (beings[currentPosY + y][currentPosX]==null))            //sprawdzenie, czy nie ma zadnej innej istoty na terenie, na ktory chce wejsc gracz
            {
                if ((stage[currentPosY + y][currentPosX].GetType() == typeof(SecretPassageEntrance))) //sprawdzenie, czy gracz chce wejsc w ukryte przejscie
                {
                    SecretPassageEntrance temp = (SecretPassageEntrance)stage[currentPosY + y][currentPosX];
                    beings[currentPosY][currentPosX] = null;
                    currentPosX = temp.GiveExitX();
                    currentPosY = temp.GiveExitY();
                    beings[currentPosY][currentPosX] = new Player();

                }
                else
                {
                    beings[currentPosY][currentPosX] = null;
                    currentPosY += y;
                    beings[currentPosY][currentPosX] = new Player();
                }
            }
        }

        public void MovePlayerX(int x)
        {
            if (!(currentPosX == WIDTH-1 && x > 0) 
                && !(currentPosX == 0 && x < 0)
                && (stage[currentPosY][currentPosX+x].Enterable())
                && (beings[currentPosY][currentPosX+x] == null))
            {
                if ((stage[currentPosY][currentPosX+x].GetType() == typeof(SecretPassageEntrance))) //sprawdzenie, czy gracz chce wejsc w ukryte przejscie
                {
                    SecretPassageEntrance temp = (SecretPassageEntrance)stage[currentPosY][currentPosX+x];
                    beings[currentPosY][currentPosX] = null;
                    currentPosX = temp.GiveExitX();
                    currentPosY = temp.GiveExitY();
                    beings[currentPosY][currentPosX] = new Player();

                }
                else
                {
                    beings[currentPosY][currentPosX] = null;
                    currentPosX += x;
                    beings[currentPosY][currentPosX] = new Player();
                }
            }
        }

        public void InitializeMap() //Funkcja ta sluzy do 'narysowania' mapy zgodnie ze schematem, nastepnie wypelnia konkretne pola istotami zywymi
        {                           //Funkcja dodatkowo inicjalizuje wszystkie podstawowe wartosci (np poczatkowy ekwipunek)


            for (int i = 0; i < HEIGHT; i++)
                stage[i] = new Terrain[WIDTH];

            for (int i = 0; i < HEIGHT; i++)
                for (int j = 0; j < WIDTH; j++)
                    stage[i][j] = new Terrain();

            for (int i = 0; i < HEIGHT; i++)
                beings[i] = new Entity[WIDTH];

            for (int i = 0; i < HEIGHT; i++)
                for (int j = 0; j < WIDTH; j++)
                    beings[i][j] = null;

            AddRoom(0, 0, 10, 10);
            AddRoom(10, 5, 25, 15);
            AddRoom(3, 10, 7, 40);
            AddRoom(0, 40, 20, 55);
            AddRoom(20, 15, 30, 50);
            stage[10][7] = new Empty();
            stage[4][10] = new Empty();
            stage[5][40] = new Empty();
            stage[20][45] = new Empty();
            stage[22][15] = new Empty();
            stage[1][1] = RandomizeEquipment(0);
            stage[1][2] = RandomizeEquipment(0);
            stage[1][3] = RandomizeEquipment(2);
            stage[1][4] = RandomizeEquipment(2);
            stage[1][5] = RandomizeEquipment(4);
            stage[1][6] = RandomizeEquipment(4);
            stage[1][7] = RandomizeEquipment(10);
            Item mieczZawalistosci = new Item(1, -5, 25, 0);
            mieczZawalistosci.ChangeName("Spoko mieczyk");
            mieczZawalistosci.ChangeDescription("Zabiera sporo zycia, ale za to mocno wali");
            stage[1][8] = mieczZawalistosci;
            stage[2][1] = RandomizePotion(0);
            stage[2][2] = RandomizePotion(0);
            stage[2][3] = RandomizePotion(2);
            stage[2][4] = RandomizePotion(2);
            stage[2][5] = RandomizePotion(4);
            stage[2][6] = RandomizePotion(4);
            stage[2][7] = RandomizePotion(10);
            Item eliksirZelaza = new Item(4, 0, 0, 5, 0, 1);
            eliksirZelaza.ChangeName("Eliksir zelaza");
            eliksirZelaza.ChangeDescription("Sprawia, ze skora staje sie tak mocna jak najwytrzymalsza stal");
            stage[2][8] = eliksirZelaza;
            for (int i = 8; i < 20; i++)
                stage[i][28] = new Wall();
            stage[7][28] = new SecretPassageExit(7, 28);
            stage[20][28] = new SecretPassageEntrance((SecretPassageExit)stage[7][28]);
            beings[currentPosX][currentPosY] = new Player();
            stage[8][55] = new SecretPassageExit(8, 55);
            stage[17][5] = new SecretPassageEntrance((SecretPassageExit)stage[8][55]);
            stage[14][5] = new SecretPassageExit(14, 5);
            stage[5][55] = new SecretPassageEntrance((SecretPassageExit)stage[14][5]);

            beings[8][8] = new Enemy();
            beings[6][6] = new Boss();


            currentSword = new Item(1, 1, 1, 0);
            character.WearItem(currentSword);
            currentArmor = new Item(2, 1, 0, 2);
            character.WearItem(currentArmor);
            currentArtefact = new Item(3, 3, 0, 0);
            character.WearItem(currentArtefact);
            inventory[0] = new Item(0);
            inventory[1] = new Item(4, 0, 0, 0, 2, 5);

        }

        public void NextTurn()
        {

        }

        public void Attack()
        {

        }

        public void UseItem()
        {
            if(inventory[currentInventory].GetItemType() == 1)  //Funkcja sprawdza, czy uzywany przedmiot jest mieczem, jesli tak, to
            { Item temp = currentSword;                         //zamienia go miejscami z obecnym
                character.UnwearItem(currentSword);
                character.WearItem(inventory[currentInventory]);
                currentSword = inventory[currentInventory];
                inventory[currentInventory] = temp;
            }
            else if (inventory[currentInventory].GetItemType() == 2)
            {
                Item temp = currentArmor;
                character.UnwearItem(currentArmor);
                character.WearItem(inventory[currentInventory]);
                currentArmor = inventory[currentInventory];
                inventory[currentInventory] = temp;
            }
            else if (inventory[currentInventory].GetItemType() == 3)
            {
                Item temp = currentArtefact;
                character.UnwearItem(currentArtefact);
                character.WearItem(inventory[currentInventory]);
                currentArtefact = inventory[currentInventory];
                inventory[currentInventory] = temp;
            }
            else if (inventory[currentInventory].GetItemType() == 4)    //Jesli uzywany przedmiot jest eliksirem, dodawane sa jego statystyki
            {                                                           //(funkcja WearItem) lub odnawiane jest zycie. Nastepnie zmniejszana
                                                                        //jest ilosc ladunkow. Jesli wynosi ona 0, przedmiot jest usuwany
                Item temp = inventory[currentInventory];
                character.WearItem(temp);
                if (character.GiveCurrentLife() + temp.GetRestoration() > character.GiveLife())
                    character.SetCurrentLife(character.GiveLife());
                else
                    character.ChangeCurrentLife(temp.GetRestoration());
                temp.ChangeCharges(-1);
                temp.ActualizeCharges();
                if (temp.GetCharges() == 0)
                {
                    DeleteCurrentItem();
                 }

            }

        }

        public void PickupItem()
        {
            if (allInventory < 99 && stage[currentPosY][currentPosX].GetType() == typeof(Item))
            {
                allInventory++;
                inventory[allInventory] = (Item)stage[currentPosY][currentPosX];
                stage[currentPosY][currentPosX] = new Empty();
                currentInventory = allInventory;
            }

        }

        public void PreviousItem()
        {
            if (allInventory == 0)
                currentInventory = 0;
            else if (allInventory == 1)
                currentInventory = 1;
            else if (currentInventory == 1)
                currentInventory = allInventory;
            else
                currentInventory--;

        }

        public void NextItem()
        {
            if (allInventory == 0)
                currentInventory = 0;
            else if (allInventory == 1)
                currentInventory = 1;
            else if (currentInventory == allInventory)
                currentInventory = 1;
            else
                currentInventory++;

        }

        public void GetHelp()
        {

        }

        public void DeleteItem(Item i)
        {
            if (allInventory == 1)
            {
                inventory[1] = null;
                currentInventory = 0;
            }
            else
            {
                for (int j = currentInventory; j < allInventory; j++)
                {
                    inventory[j] = inventory[j + 1];

                }
                inventory[allInventory] = null;
            }
            allInventory--;
        }

        public void DeleteCurrentItem()
        {
            if (allInventory == 0)
                return;
            else
            {
                if (allInventory == 1)
                    currentInventory = 0; 
                DeleteItem(inventory[currentInventory]);
            }
            
        }

        public String SplitString(String s, int start, int end)     //Funkcja uzywana do dzielenia opisu na mniejsze czesci
        {
            String temp = "";
            for (int i = start; i < end; i++)
                if(i<s.Length)
                    temp += s[i];
            return temp;
        }

        Item RandomizeEquipment(int tier)       //Funkcja do losowania przedmiotow typu miecz/zbroja/artefakt
        {
            int a1 = 0;
            int a2 = 0;
            Random r = new Random(Guid.NewGuid().GetHashCode());

            int points = r.Next(2, 7);          //Losuje ilosc 'punktow' do rozdania, ujemne statystyki dodaja do punktow, dodatnie odejmuja
            points += tier;                     //w praktyce, przedmiot tier 0 moze miec max 6 dodatkowych statow, ale za to jesli odejmuje
            for(int j=0; j<1000 && points>0; j++)   //jakies, to moze miec wiecej (np +8 atak -2 zycie)
            {
                int chance1 = r.Next(1, 200);
                int chance2 = r.Next(1, 300);
                if (chance1 < 100)
                {
                    if (chance2 < 100)
                    { a1--; points++; }
                    else
                    { a1++; points--; }
                }
                else
                {
                    if (chance2 < 100)
                    { a2--; points++; }
                    else
                    { a2++; points--; }
                }
            }
            int itemType = r.Next(1, 4);
            int choose = r.Next(1, 300);
            if(a2>a1)           //Upewnia sie, ze w a1 jest najwyzszy atrybut
            {
                int temp = a1;
                a1 = a2;
                a2 = temp;
            }
            if(itemType == 1)
            {
                if (choose < 150) return new Item(1, a2, a1, 0);    //Jesli przedmiot jest typu miecz, to najwyzszy atrybut to zawsze atak
                else return new Item(1, 0, a1, a2);
            }
            if (itemType == 2)
            {
                if (choose < 150) return new Item(2, 0, a2, a1);    //Jesli zbroja, to armor
                else return new Item(2, a2, 0, a1);
            }
            if (itemType == 3)
            {
                if (choose < 50) return new Item(3, a1, a2, 0);      //Jesli artefakt, to nie ma glownego atrybutu, wiec jest 6 kombinacji
                else if (choose < 100) return new Item(3, a1, 0, a2);
                else if (choose < 150) return new Item(3, a2, a1, 0);
                else if (choose < 200) return new Item(3, 0, a1, a2);
                else if (choose < 250) return new Item(3, a2, 0, a1);
                else return new Item(3, 0, a2, a1);
            }
            else return new Item(4, 0, 0, 0, 1, 1);

        }

        Item RandomizePotion(int tier)
        {
            Random r = new Random(Guid.NewGuid().GetHashCode());
            int chance = r.Next(1, 100);
            if (chance < 10)                        //jest 10% szansy na potion +2 do max zycia
                return new Item(4, 2, 0, 0, 0, 1);
            else if (chance < 20)                   //10% szansy na +1 ataku
                return new Item(4, 0, 1, 0, 0, 1);
            else if (chance < 30)                   //10% szansy na +1 pancerza
                return new Item(4, 0, 0, 1, 0, 1);
            else
            {                                       //70% szansy na eliksir odnowienia zycia,
                int points = r.Next(5, 10);         //na tier 0 odnawia miedzy 5-9 zycia
                points += 2 * tier;
                int charges = r.Next(1, 6);         //losowane sa ladunki (1-5)
                return new Item(4, 0, 0, 0, points / charges, charges); //odnowienie zycia to punkty/ladunki

            }

        }

        public void DrawConsoleWindow()
        {
            //Ponizej przeprowadzone jest aktualizowanie interfejsu w zaleznosci od tego, jakie wartosci znajduja sie w kazdym z pol
            if (inventory[currentInventory] == null)
                PreviousItem();
            consoleInterface[0] = ("<3 x " + (character.GiveCurrentLife().ToString()) + "/" + character.GiveLife().ToString()).PadRight(12)
                + "|" + ("   Poziom: " + character.GiveLevel().ToString()).PadRight(15)
                + "|" + ("    Atak: " + character.GiveAttack().ToString()).PadRight(15)
                + "|" + ("    Pancerz: " + character.GiveArmor().ToString()).PadRight(17)
                + "|  (X) = Pomoc/Instrukcja";
            consoleInterface[3] = "| =|=>   Miecz:  " + currentSword.GetFirstProperty().PadRight(7);
            consoleInterface[4] = "|                " + currentSword.GetSecondProperty().PadRight(10);
            consoleInterface[8] = "| /[]\\  Zbroja:  " + currentArmor.GetFirstProperty().PadRight(7);
            consoleInterface[9] = "|                " + currentArmor.GetSecondProperty().PadRight(10);
            consoleInterface[13] = "| <O> Artefakt:  " + currentArtefact.GetFirstProperty().PadRight(7);
            consoleInterface[14] = "|                " + currentArtefact.GetSecondProperty().PadRight(10);
            consoleInterface[20] = "| " + inventory[currentInventory].GetName();
            consoleInterface[22] = "|   <- (F)" + currentInventory.ToString().PadLeft(4) + "/" + allInventory.ToString().PadRight(4) + "(G) ->";
            consoleInterface[25] = "| " + inventory[currentInventory].GetFirstProperty();
            consoleInterface[26] = "| " + inventory[currentInventory].GetSecondProperty();
            consoleInterface[28] = "| Opis: " + SplitString(inventory[currentInventory].GetDescription(), 0, 20);
            consoleInterface[29] = "| " + SplitString(inventory[currentInventory].GetDescription(), 20, 45);
            consoleInterface[30] = "| " + SplitString(inventory[currentInventory].GetDescription(), 45, 70);
            consoleInterface[31] = "| " + SplitString(inventory[currentInventory].GetDescription(), 70, 95);





            System.Console.WriteLine(consoleInterface[0]);
            System.Console.Write(consoleInterface[1]);

            int counter = 2;
            for (int i = currentPosY-15; i < currentPosY+16; i++)           //Rysowanie mapy i interfejsu
            {
                
                System.Console.WriteLine("");
                for (int j = currentPosX-15; j < currentPosX+16; j++)       //Gra rysuje obszar w zakresie 15 pól w lewo, w prawo, w górę i w dół od bohatera
                {
                    if (i > -1 && i < WIDTH && j > -1 && j < HEIGHT)        //Gra sprawdza, czy kamera nie wychodzi poza zakres mapy
                    {                                                       //Wszedzie tam, gdzie w tablicy terenu i istot nic nie ma, gra rysuje '- '
                        if (beings[i][j] == null)
                            System.Console.Write(stage[i][j].Draw());       //Jesli nie ma na polu nic zywego, rysowany jest teren tego pola
                        else System.Console.Write(beings[i][j].Draw());     //A jesli jest cos zywego, rysowana jest ta istota, przykrywajac teren pod nia
                     }
                    else System.Console.Write("- ");
                }
                System.Console.Write(consoleInterface[counter]);            //Rysowanie interfejsu, niezalezne od rysowania mapy (dlatego potrzebny jest counter)
                counter++;

            }


        }

        //Ponizej inicjalizowana jest tablica stringow, ktora pelni role interfejsu

        String[] consoleInterface = new String[33]
{
            "",
            "--------------------------------------------------------------|----------------------------",
            "|",
            "",
            "",
            "|",
            "|----------------------------",
            "|",
            "",
            "",
            "|",
            "|----------------------------",
            "|",
            "",
            "",
            "|",
            "|----------------------------",
            "|         Ekwipunek:",
            "|----------------------------",
            "|",
            "",
            "|",
            "",
            "|----------------------------",
            "|",
            "",
            "",
            "|",
            "",
            "",
            "",
            "",
            "|",
};

        
    }
}
