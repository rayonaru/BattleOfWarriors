using BattleOfWarriors.Code;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BattleOfWarriors
{
    class Program
    {
        static void Main()
        {
            String[] defaultNames = { "Afrodita", "Odin", "Raygen", "Aid", "Zeus", "Tor" };

            StartFight(CreateWarriors(defaultNames));
        }

        /// <summary>
        /// Создание воинов по именам
        /// </summary>
        /// <param name="names"></param>
        private static List<Warrior> CreateWarriors(String[] names)
        {
            Random random = new Random();
            List<Warrior> warriors = new List<Warrior>();

            foreach (var name in names)
            {
                warriors.Add(new Warrior(name, random.Next(10, 30)));
            }

            return warriors;
        }

        /// <summary>
        /// Начало боя
        /// </summary>
        /// <param name="warriors"></param>
        private static void StartFight(List<Warrior> warriors)
        {
            ShowWarriorsInformation(warriors);

            Console.WriteLine($" --- Fight started! --- ");

            while (!(warriors.Where(x => x.Helath <= 0).Count() == warriors.Count() - 1))
                RandomizeAttacks(warriors);

            ShowWarriorsInformation(warriors, true);
        }

        /// <summary>
        /// Рандомизация атак
        /// </summary>
        /// <param name="warriors"></param>
        private static void RandomizeAttacks(List<Warrior> warriors)
        {
            Random random = new Random();

            var attacker = random.Next(warriors.Count());
            var attacked = random.Next(warriors.Count());

            if (attacker != attacked && warriors[attacker].Helath >= 0 && warriors[attacked].Helath >= 0)
            {
                warriors[attacker].Attack(warriors[attacked]);
                
                if (random.Next(0, 10) == 5)
                    warriors[attacker].MirrorAttack(warriors[attacked]);
            }
        }

        /// <summary>
        /// Информация о воине (имя, здоровье, урон)
        /// </summary>
        /// <param name="warriors"></param>
        /// <param name="afterFight"></param>
        private static void ShowWarriorsInformation(List<Warrior> warriors, Boolean afterFight = false)
        {
            if (!afterFight)
                Console.WriteLine($" --- Warriors ready to fight! --- {Environment.NewLine}");
            else
                Console.WriteLine($"{Environment.NewLine} --- Fight finished! Winner: {warriors.SingleOrDefault(x => x.Helath > 0).Name} --- {Environment.NewLine}");

            Console.WriteLine($" --- Warriors information --- ");
            foreach (var warrior in warriors)
            {
                var status = warrior.Helath <= 0 ? "died" : $"{warrior.Helath}%";
                if (!afterFight)
                    Console.WriteLine($"Name: {warrior.Name}. Health: {status}. Damage: {warrior.Damage}%");
                else
                    Console.WriteLine($"Name: {warrior.Name}. Health: {status}.");
            }
            Console.WriteLine(Environment.NewLine);
        }
    }
}
