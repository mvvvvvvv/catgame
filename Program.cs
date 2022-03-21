using System;
using System.Collections.Generic;
using System.Linq;

namespace catGame
{

    public class Program
    {
        public delegate void activities();

        static void Main(string[] args)
        {
            Cat myCat = new Cat();
            //introducing cat game and asking whether to load or start new game
            int loadOrNew = Introduction();

            //if user wants a new cat
            if (loadOrNew == 1)
            {
                Console.WriteLine("\n~*~*~*~* Congratulations! You have a new cat! *~*~*~*~");
                myCat.RandColour();
                catDisplay(myCat);
                //setting up the cat
                myCat.FavouriteFood();
                myCat.NamingCat();
                myCat.GeneratePersonality();
            }
            //if user wants to load a cat
            else
            {
                myCat = Cat.loadCat();
                Console.WriteLine($"Here is {myCat.Name}!");
                catDisplay(myCat);
            }

            List<string> catActivityNames = new List<string> { "Display Stats", "Pet Cat", "Feed Cat", "Play with Cat", "Scissors Paper Rock", "Wash Cat" };
            List<activities> catActivities = new List<activities> { myCat.DisplayStats, myCat.PetCat, myCat.FeedCat, myCat.Play, myCat.scissorsPaperRock, myCat.Wash };

            myCat.Unlockables(catActivities, catActivityNames);

            //main program with loop (interacting with cat)
            int quit = 1;

            Console.WriteLine("\nSelect an activity:");
            displayHelp(catActivityNames);

            int input;
            while(quit>=0)
            {
                Console.WriteLine("\nType 100 to display all options again, or -1 to save and quit.");
                Console.Write(">>");
                    try
                    {
                        input = int.Parse(Console.ReadLine());
                        if (input == 100)
                        {
                            displayHelp(catActivityNames);
                        }
                        else
                        { 
                            if (input == -1)
                            {
                                quit = -1;
                            }
                            else
                            {
                                catActivities[input]();
                            }
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Incorrect input, Please select a number from the list:");
                        displayHelp(catActivityNames);
                    }
                myCat.StatsCounter();
            }

            //exiting game + saving
            myCat.SaveAndExit();

            Console.WriteLine("See you next time!");
        }


        static int Introduction()
        {
            Console.WriteLine("Hello! Welcome to the Cat Game!");
            Console.WriteLine("Would you like to load a cat or adopt a new one?");
            Console.WriteLine("1. New Cat");
            Console.WriteLine("2. Load Cat");
            Console.Write(">>");

            int result;

            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out result) && (result == 1 || result ==2))
                {
                    return result;
                }
                else
                {
                    Console.WriteLine("Please input a valid number!");
                    Console.WriteLine("1. New Cat");
                    Console.WriteLine("2. Load Cat");
                    Console.Write(">>");
                }
            }
        }

        public static void catDisplay(Cat cat)
        {
            cat.ChangeConsoleColour(cat.Colour);
            Console.WriteLine(@"                   |\ |\   ");
            Console.WriteLine(@"                   \ . .\  ");
            Console.WriteLine(@"             __,.-  =  w = ");
            Console.WriteLine(@"            .           )  ");
            Console.WriteLine(@"      _    /   ,    \/\_   ");
            Console.WriteLine(@"     ((____|    )_-\ \_-`  ");
            Console.WriteLine(@$"     `-----'`-----` `--`    ");
            Console.ResetColor();
        }

        static void displayHelp(List<string> helpList) 
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            int i = 0;
            foreach (string activity in helpList) 
            {
                Console.WriteLine($"{i}  {activity}");
                i++;
            }
            Console.ResetColor();
        }
    }

    public class Cat
    {
        Random rnd = new Random();

        //info for "world":
        string[] availFoods = { "tuna", "salmon", "pork", "beef", "random bird outside", "dry food", "milk"};
        string[] availToys = { "mouse on string", "ribbon", "ball", "your hand" };

        //personality
        Dictionary<string, bool> personality = new Dictionary<string, bool>
        {
            { "Hates to Lose", false },
            { "Dislikes being Pet", false },
            { "Shy", false },
            { "Likes Water", false },
            { "Hungry", false }
        };

        //timers
        Dictionary<string, int> timers = new Dictionary<string, int>
        {
            { "Park", 0 },
        };

        string colour = "brown";
        string name = "cat";
        int age = 0;
        int love = 0;
        string faveFood = "";
        int hunger = 0;
        int overstimulation;
        int fun;
        int dirt;

        public Dictionary<string, bool> Personality
        {
            get { return personality; }
            set { personality = value; }
        }

        public Dictionary<string, int> Timers
        {
            get { return timers; }
            set { timers = value; }
        }
        public string Colour
        {
            get { return colour; }
            set { colour = value; }
        }
        public int Age
        {
            get { return age; }
            set { age = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public int Love
        {
            get { return love; }
            set { love = value; }
        }

        public string FaveFood
        {
            get { return faveFood; }
            set { faveFood = value; }
        }

        public int Hunger
        {
            get { return hunger; }
            set { hunger = value;  }
        }
        public int Overstimulation
        {
            get { return overstimulation; }
            set 
            { if (value <= 60 && value >= 0)
                {
                    overstimulation = value;
                }
                else
                {
                    if (value > 60)
                    {
                        overstimulation = 60;
                    }
                    else
                    {
                        overstimulation = 0;
                    }
                }
            }
        }
        public int Fun
        {
            get { return fun; }
            set { fun = value; }
        }

        public int Dirt
        {
            get { return dirt; }
            set { dirt = value; }
        }

        public void NamingCat()
        {
            Console.WriteLine("Please input a new name for the cat:");
            Console.Write("Name >>");
            name = Console.ReadLine();
            Console.WriteLine($"The cat's new name is {name}!");
        }

        public void FavouriteFood()
        {
            FaveFood = availFoods[rnd.Next(availFoods.Length)];
        }

        public void GeneratePersonality()
        {
            foreach(string trait in Personality.Keys.ToList())
            {
                int i = rnd.Next(2);
                if (i == 0)
                {
                    Personality[trait] = false;
                }
                else
                {
                    Personality[trait] = true;
                }
            }
        }

        public void RandColour()
        {
            int n;

            string[] colours = {"white", "black", "brown", "tabby", "orange"};

            do
            {
                Console.WriteLine($"\nPlease pick a number from 0 to {colours.Length - 1}!");
                Console.Write("Your number >>");
                n = int.Parse(Console.ReadLine());

            } while ( n < 0 || n >= colours.Length);

            colour = colours[n];
            Console.WriteLine($"Your cat is {colour}!\n");
        }

        //change the stats
        public void StatsCounter()
        {
            //chance 1% per step
            int one = rnd.Next(100);
            //chance 15% per step
            int fifteen = rnd.Next(7);

            //15% chance to increase
            if (fifteen == 5)
            {
                hunger++;
            }
            if(hunger >= 50)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nYour cat is very hungry! Please feed it now!");
                Console.ResetColor();
                DecreaseLove();
            }
            if (fifteen == 4)
            {
                Overstimulation = Overstimulation - 1;
            }
            if(overstimulation >= 50)
            {
                Console.WriteLine("\nYour cat is overstimulated! Don't touch!");
                DecreaseLove();
            }
            if (fifteen == 3)
            {
                fun--;
            }
            if (fun < 0)
            {
                Console.WriteLine("\nYour cat is bored! Please play!");
                DecreaseLove();
            }
            if (fifteen == 6)
            {
                love--;
            }
            if (fifteen == 7 && Dirt>0)
            {
                Dirt--;
            }

            //age 1% chance to increase
            if (one == 5)
            {
                age++;
            }

            foreach (string activity in Timers.Keys.ToList())
            {
                if (Timers[activity] > 0)
                {
                    Timers[activity] = Timers[activity] - 1;
                }
            }
        }

        public void PetCat()
        {
            if (personality["Dislikes being Pet"] == false)
            {
                Console.WriteLine("You have pet your cat!");
                IncreaseLove();
                Overstimulation++;
            }
            else
            {
                Console.WriteLine("You have pet your cat!");
                Console.WriteLine($"Oh no! {this.name} doesn't like being pet!");
                DecreaseLove();
                Overstimulation = Overstimulation + 30;
            }
        }

        public void FeedCat()
        {
            Console.WriteLine("\nThese are the available foods:\n");
            int n = 0;
            foreach (string food in availFoods)
            {
                Console.WriteLine($"{n} {food}");
                n++;
            }

            Console.WriteLine("\nWhich food do you choose?");

            do
            {
                Console.Write(">>");
                n = int.Parse(Console.ReadLine());
            } while (n < 0 || n >= availFoods.Length);

            Console.WriteLine($"You picked {availFoods[n]}!");

            if(hunger >= 0)
            {
                if (availFoods[n] == faveFood)
                {
                    Console.WriteLine("The cat loved this food!\nLove increased by 2!");
                    IncreaseLove();
                    IncreaseLove();
                }
                else
                {
                    IncreaseLove();
                }

                hunger = hunger - 5;
            }
            else
            {
                Console.WriteLine("The cat is full!");
                DecreaseLove();
            }
            
        }

        public void DisplayStats()
        {
            Program.catDisplay(this);
            Console.WriteLine($"Colour: {colour}");
            Console.WriteLine($"Love: {love}");
            Console.WriteLine($"Hunger: {hunger}");
            Console.WriteLine($"Age: {age}");
            Console.WriteLine($"Fun: {fun}");
            Console.WriteLine($"Overstimulation: {overstimulation}");
            Console.WriteLine($"Dirt: {dirt}");
        }

        void IncreaseLove()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            love++;
            Console.WriteLine("Love increased!");
            Console.ResetColor();
        }

        void DecreaseLove()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            love--;
            Console.WriteLine("Love decreased");
            Console.ResetColor();
        }

        public void Play()
        {
            Console.WriteLine("\nThese are the available toys:\n");
            int n = 0;
            foreach (string toy in availToys)
            {
                Console.WriteLine($"{n} {toy}");
                n++;
            }

            Console.WriteLine("\nWhich toy do you choose?");

            do
            {
                Console.Write(">>");
                n = int.Parse(Console.ReadLine());
            } while (n < 0 || n >= availToys.Length);

            Console.WriteLine($"You picked {availToys[n]}!");
            IncreaseLove();
            fun = fun + 3;
            Overstimulation++;
        }

        public void Park()
        {
            if (timers["Park"] == 0)
            {
                ChangeConsoleColour(this.Colour);
                Console.WriteLine(@"             /\ /\    ");
                Console.WriteLine(@"            / . . \   ");
                Console.WriteLine(@"            \= w =/   ");
                Console.WriteLine(@"            .     .      ");
                Console.WriteLine(@"      _    /       \    ");
                Console.WriteLine(@"     ((____|   |   |    ");
                Console.WriteLine(@$"     `-----'W----W`    ");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine(@$"\\\/||||/, | \| ||  | ////|///");
                Console.ResetColor();
                IncreaseLove();
                Overstimulation = Overstimulation - 20;
                timers["Park"] = 10;

                Dirt = rnd.Next(5);
            }
            else
            {
                Console.WriteLine("You have just been to the park!");
            }
        }

        public void Wash()
        {
            if (Dirt > 0)
            {
                Dirt = 0;
                Console.WriteLine("Your cat is now clean!");
                if (Personality["Likes Water"] == false)
                {
                    Console.WriteLine($"Oh no! {this.name} hates water!");
                    DecreaseLove();
                }
                else
                {
                    Console.WriteLine($"{this.name} loves being clean!");
                    IncreaseLove();
                }
            }
            else
            {
                Console.WriteLine("Can't wash - your cat is already clean!");
            }
        }
        public void Unlockables(List<Program.activities> activities, List<string> names)
        {
            if (love > 20)
            {
                activities.Add(this.Park);
                names.Add("Go to Park");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Park unlocked!");
                Console.ResetColor();
            }
            if (love > 100)
            {
                activities.Add(this.TicTacToe);
                names.Add("Tic Tac Toe");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Tic Tac Toe unlocked!");
                Console.ResetColor();
            }
        }

        public void ChangeConsoleColour(string catColour)
        {
            ConsoleColor colour;
            ConsoleColor bColour;

            switch (catColour)
            {
                case "white":
                    colour = ConsoleColor.White;
                    bColour = ConsoleColor.Black;
                    break;
                case "black":
                    colour = ConsoleColor.Black;
                    bColour = ConsoleColor.White;
                    break;
                case "brown":
                    colour = ConsoleColor.DarkYellow;
                    bColour = ConsoleColor.Black;
                    break;
                case "tabby":
                    colour = ConsoleColor.DarkGray;
                    bColour = ConsoleColor.Black;
                    break;
                case "orange":
                    colour = ConsoleColor.Red;
                    bColour = ConsoleColor.Black;
                    break;
                default:
                    colour = ConsoleColor.White;
                    bColour = ConsoleColor.Black;
                    break;

            }
            Console.BackgroundColor = bColour;
            Console.ForegroundColor = colour;
        }

        /////////////////////////////////////
        //              Games              //
        /////////////////////////////////////
        public void scissorsPaperRock()
        {
            Console.WriteLine("\nWelcome to Scissors Paper Rock!");
            //0 = S, 1 = P, 2 = R
            string[] options = { "scissors", "paper", "rock"};
            Console.WriteLine("\nScissors beat Paper");
            Console.WriteLine("Paper beats Scissors");
            Console.WriteLine("Paper beats Rock");
            Console.WriteLine("\nPlease Pick:");
            Console.WriteLine("1. Scissors");
            Console.WriteLine("2. Paper");
            Console.WriteLine("3. Rock");

            int human;

            while (true)
            {
                Console.Write(">>");
                if (int.TryParse(Console.ReadLine(), out human) && (human <= 3 && human > 0))
                {
                    human = human - 1;
                    break;
                }
                else
                {
                    Console.WriteLine("Please pick a valid number!");
                }
            }

            int cat = rnd.Next(3);
            Console.WriteLine($"\n  ^ ^");
            Console.WriteLine($" ('w')Cat picks {options[cat]}!\n");
            Console.WriteLine($" ('u')");
            Console.WriteLine($"  /|\\ You picked {options[human]}!\n");

            bool catLose = false;

            if(cat == human)
            {
                Console.WriteLine("You Draw!");
            }
            else
            {
                if (human == 0)
                {
                    if(cat == 1)
                    {
                        Console.WriteLine("You Win!");
                        catLose = true;
                    }
                    else
                    {
                        Console.WriteLine("You Lose!");
                    }
                }
                if (human == 1)
                {
                    if(cat == 0)
                    {
                        Console.WriteLine("You Lose!");
                    }
                    else
                    {
                        Console.WriteLine("You Win!");
                        catLose = true;
                    }

                }
                if (human == 2)
                {
                    if(cat == 0)
                    {
                        Console.WriteLine("You Win!");
                        catLose = true;
                    }
                    else
                    {
                        Console.WriteLine("You Lose!");
                    }
                }
            }
            //increase/decrease love
            if (personality["Hates to Lose"] == true && catLose == true)
            {
                DecreaseLove();
                Console.WriteLine($"{this.name} hates losing!\n");
            }
            else
            {
                IncreaseLove();
            }

            fun = fun + 5;
        }

        public void TicTacToe()
        {
            //cat arrays
            bool[][] catArrs =
            {
                new bool[] { false, false, false },
                new bool[] { false, false, false },
                new bool[] { false, false, false },
            };
            //human arrays
            bool[][] humArrs =
            {
                new bool[] { false, false, false },
                new bool[] { false, false, false },
                new bool[] { false, false, false },
            };

            //game loop
            bool cont = true;
            bool valid = false;
            int row;
            int column;
            bool catLose = false;
            while (cont == true)
            {
                //cat chooses
                valid = false;
                while (valid == false)
                {
                    //random number for row
                    row = rnd.Next(3);
                    //random number for column
                    column = rnd.Next(3);
                    //check if valid
                    if (!catArrs[row][column] && !humArrs[row][column])
                    {
                        catArrs[row][column] = true;
                        valid = true;
                        Console.WriteLine($"\n  ^ ^");
                        Console.WriteLine($" ('w')Cat picks cell in row {row} and column {column}!\n");
                    }
                }
                row = 0;
                //display board
                Console.WriteLine("    0    1    2  ");
                bool finished = true; 
                foreach (Array r in catArrs)
                {
                    column = 0;
                    Console.WriteLine("   ___  ___  ___ ");
                    Console.Write($"{row} ");
                    foreach (bool cell in r)
                    {
                        if (cell == true)
                        {
                            Console.Write("| O |");
                        }
                        else
                        {
                            if (humArrs[row][column])
                            {
                                Console.Write("| X |");
                            }
                            else
                            {
                                Console.Write("|   |");
                                finished = false;
                            }
                        }
                        column++;
                    }

                    Console.WriteLine("\n   ---  ---  --- ");
                    row++;
                }

                //check for cat win
                if (true == catArrs[0][0] && catArrs[1][1] && catArrs[2][2])
                {
                    Console.WriteLine($"{name} has won the game!");
                    cont = false;
                }
                else
                {
                    if (true == catArrs[0][2] && catArrs[1][1] && catArrs[2][0])
                    {
                        Console.WriteLine($"{name} has won the game!");
                        cont = false;
                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            if (catArrs[i][0] && catArrs[i][1] && catArrs[i][2])
                            {
                                Console.WriteLine($"{name} has won the game!");
                                cont = false;
                            }
                            else
                            {
                                if (true == catArrs[0][i] && catArrs[1][i] && catArrs[2][i])
                                {
                                    Console.WriteLine($"{name} has won the game!");
                                    cont = false;
                                }
                            }
                        }
                    }
                }

                if (finished)
                {
                    Console.WriteLine($"It's a draw!");
                    break;
                }

                //human chooses
                valid = false;
                while (valid == false && cont == true)
                {
                    Console.WriteLine("Please input your cell:");
                    Console.Write("Row >>");
                    string y = Console.ReadLine();
                    Console.Write("Column >>");
                    string x = Console.ReadLine();
                    try
                    {
                        row = int.Parse(y);
                        column = int.Parse(x);
                        if (!catArrs[row][column] && !humArrs[row][column])
                        {
                            humArrs[row][column] = true;
                            valid = true;
                            Console.WriteLine($" ('u')");
                            Console.WriteLine($"  /|\\ You picked cell in row {row} and column {column}!\n");
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Please input a valid cell!");
                    }
                }

                //display board
                row = 0;
                //display board
                Console.WriteLine("    0    1    2  ");
                foreach (Array r in catArrs)
                {
                    column = 0;
                    Console.WriteLine("   ___  ___  ___ ");
                    Console.Write($"{row} ");
                    foreach (bool cell in r)
                    {
                        if (cell == true)
                        {
                            Console.Write("| O |");
                        }
                        else
                        {
                            if (humArrs[row][column])
                            {
                                Console.Write("| X |");
                            }
                            else
                            {
                                Console.Write("|   |");
                            }
                        }
                        column++;
                    }

                    Console.WriteLine("\n   ---  ---  --- ");
                    row++;
                }

                //check for human win
                if (true == humArrs[0][0] && humArrs[1][1] && humArrs[2][2])
                {
                    Console.WriteLine($"You have won the game!");
                    catLose = true;
                    cont = false;
                }
                else
                {
                    if (true == humArrs[0][2] && humArrs[1][1] && humArrs[2][0])
                    {
                        Console.WriteLine($"You have won the game!");
                        catLose = true;
                        cont = false;
                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            if (true == humArrs[i][0] && humArrs[i][1] && humArrs[i][2])
                            {
                                Console.WriteLine($"You have won the game!");
                                catLose = true;
                                cont = false;
                            }
                            else
                            {
                                if (true == humArrs[0][i] && humArrs[1][i] && humArrs[2][i])
                                {
                                    Console.WriteLine($"You have won the game!");
                                    catLose = true;
                                    cont = false;
                                }
                            }
                        }
                    }
                }
            }

            //increase/decrease love
            if (personality["Hates to Lose"] == true && catLose == true)
            {
                DecreaseLove();
                Console.WriteLine($"{this.name} hates losing!\n");
            }
            else
            {
                IncreaseLove();
            }

            fun = fun + 5;
        }

        /////////////////////////////////////
        //            Load/Save            //
        /////////////////////////////////////
        public static Cat loadCat()
        {
            //making array of list of files
            string[] listOfFiles;

            if (System.IO.Directory.Exists("saves"))
            {
                listOfFiles = System.IO.Directory.GetFiles("saves");
            }
            else
            {
                Console.WriteLine("An error has occured: There is no saves folder.");
                Console.ReadLine();
                return null;
            }

            //displaying cats in folder:
            if (listOfFiles.Length > 0)
            {
                Console.WriteLine("Please select a cat to load:");

                int i = 0;
                foreach(string cat in listOfFiles)
                {
                    Console.WriteLine($"{i}: {listOfFiles[i]}");
                    i++;
                }

                Console.Write(">>");
                    
            }
            else
            {
                Console.WriteLine("There are no cats saved. Please restart the game and make a new cat.");
                Console.ReadLine();
                return null;
            }

            string catName = "";

            while (catName == "")
            {
                try
                {
                    catName = listOfFiles[int.Parse(Console.ReadLine())];
                }
                catch
                {
                    Console.WriteLine("Please enter a valid number!");
                    Console.Write(">>");
                }
            }

            //takes text from save file with cats name
            string text = System.IO.File.ReadAllText(catName);
            //put the cat save file in our temporary object
            Cat tempcat = System.Text.Json.JsonSerializer.Deserialize<Cat>(text);

            return tempcat;
        }
        public void SaveAndExit()
        {
            //generate a string from object attributes
            string text = System.Text.Json.JsonSerializer.Serialize(this);

            //checking if saves dir has been created, making one if not
            if (!System.IO.Directory.Exists("saves"))
            {
                System.IO.Directory.CreateDirectory("saves");
            }

            //creating/overwriting text file with cat's name
            System.IO.StreamWriter writer = System.IO.File.CreateText($"saves/{this.name}");

            //writing to file
            writer.Write(text);
            //gets rid from buffer in memory, puts inside file
            writer.Flush();
            writer.Close();
        }
    }
}
