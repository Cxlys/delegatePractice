using System;
using System.Collections.Generic;

namespace p
{
    class Program
    {
        static void Main(string[] args)
        {
            Environment dynamicProgram = new Environment();
            Game newGame = new Game();
        }
    }

    class Game
    {
        private delegate void AttackType();
        private Enemy currentEnemy;
        private string currentPlayerWeapon;
        AttackType attackMethod = null;

        public Game()
        {
            Console.WriteLine("Welcome to the game.");
            Console.WriteLine("Please choose a weapon to start with. 1 for sword, 2 for bow, 3 for gun.");
            string choice = Console.ReadLine();

            ChooseWeapon(choice);

            bool run = false;
            string input;
            while (!run)
            {
                if (currentEnemy is null)
                {
                    currentEnemy = new Enemy();
                    Console.WriteLine("You have encountered a " + currentEnemy.name);
                }

                DisplayMenu();
                input = Console.ReadLine();

                if (input == "1")
                {
                    attackMethod();
                }
                if (input == "2")
                {
                    Console.WriteLine("Which weapon would you like to switch to? 1 for sword, 2 for bow, 3 for gun");
                    input = Console.ReadLine();

                    ChooseWeapon(input);
                }

                if (currentEnemy.currentHealth <= 0)
                {
                    Console.WriteLine("You have slain the enemy! Prepare for the next");
                    currentEnemy = null;
                }
            }
        }

        public void ChooseWeapon(string input)
        {
            switch (input)
            {
                case "1":
                    attackMethod = SwordAttack;
                    break;

                case "2":
                    attackMethod = BowAttack;
                    break;

                case "3":
                    attackMethod = GunAttack;
                    break;
            }
        }

        public void DisplayMenu()
        {
            Console.WriteLine("\nEnemy: " + currentEnemy.name);
            Console.WriteLine("HP: " + currentEnemy.currentHealth + "/" + currentEnemy.maxHealth);
            Console.WriteLine("This enemy resists " + currentEnemy.elementResistance);
            Console.WriteLine("1. Attack");
            Console.WriteLine("2. Switch weapon");
        }

        private void DealDamage(int weaponDamage, int enemyDefense)
        {
            int damageDealt = weaponDamage / enemyDefense;
            Console.WriteLine("You have dealt " + damageDealt + " damage!");
            currentEnemy.currentHealth -= damageDealt;
        }

        private void SwordAttack()
        {
            Console.WriteLine("You swing using your sword!");
            DealDamage(100, currentEnemy.defense);
        }

        private void GunAttack()
        {
            Console.WriteLine("You spray wildly with your gun!");
            DealDamage(500, currentEnemy.defense);
        }

        private void BowAttack()
        {
            Console.WriteLine("You aim, pull back and fire your bow!");
            DealDamage(100, currentEnemy.defense);
        }
    }

    class Enemy
    {
        Random rnd = new Random();
        public int defense = 10;
        public int maxHealth = 1000;
        public int currentHealth = 1000;
        public int attack = 100;
        public string name;

        public string elementResistance;
        private string[] weaponArray = new string[3] { "Sword", "Gun", "Bow"};
        private string[] enemyType = new string[3] { "Slime", "Ogre", "God" };

        public Enemy ()
        {
            var randomGen = rnd.Next(0, 2);
            elementResistance = weaponArray[randomGen];

            randomGen = rnd.Next(0, 2);
            name = enemyType[randomGen];
        }
    }

    class Environment
    {
        public delegate void TestDelegate();

        public void TestFunction1 ()
        {
            Console.WriteLine("This is test function 1");
        }

        public void TestFunction2()
        {
            Console.WriteLine("This is test function 2");
        }

        public void TestFunction3()
        {
            Console.WriteLine("This is test function 3");
        }

        public delegate void WeaponDelegate();

        public void SwordFunction()
        {
            Console.WriteLine("You swing your sword!");
        }

        public void BowFunction()
        {
            Console.WriteLine("You aim your bow and fire wildly!");
        }

        public void GunFunction()
        {
            Console.WriteLine("You spray your gun in a thousand directions!");
        }

        public void DefaultWeapFunction()
        {
            Console.WriteLine("You swing your... fist?");
        }
        public Environment ()
        {
            TestDelegate newDelegate = TestFunction1;
            newDelegate += TestFunction2;
            newDelegate += TestFunction3;

            Delegate[] delegateList = newDelegate.GetInvocationList();
            foreach (var del in delegateList)
            {
                Console.WriteLine(del.Method);
            }

            newDelegate();
            bool end = false;
            while (!end)
            {
                Console.WriteLine("\nType 1 to choose a sword, 2 for a bow and 3 for a gun");
                string input = Console.ReadLine();
                WeaponDelegate weaponFunction = DefaultWeapFunction;

                if (input == "1")
                {
                    weaponFunction = SwordFunction;
                }
                else if (input == "2")
                {
                    weaponFunction = BowFunction;
                }
                else if (input == "3")
                {
                    weaponFunction = GunFunction;
                }
                else
                {
                    Console.WriteLine("You equip... nothing?");
                }

                Console.WriteLine("Would you like to attack?");
                string input2 = Console.ReadLine();

                if (input2 == "Y")
                {
                    weaponFunction();
                }
                else
                {
                    Console.WriteLine("You died from muscle atrophy. No stop next time. Only attack.");
                    end = true;
                }
            }
        }
    }
}
