﻿using System;

namespace RudeExample
{
    public class SimpleStatic
    {
        public static string Title = "Clever Creature Identifier";
        public static string Result { get; set; }
        public static bool? IsAnimal()
        {
            var animal = DateTime.Now.Second % 2 == 0;
            if (!animal) Result = "a cherry tree";
            return animal;
        }
        public static bool? HasLegs()
        {
            var legs = true;
            if (!legs) Result = "a snake";
            return legs;
        }
        public static bool? HasTwoLegs()
        {
            return DateTime.Now.Millisecond % 2 == 0;
        }
        private static bool IsSuperhuman() { return true; }
        public static bool? CanCountToInfinity()
        {
            var superhuman = IsSuperhuman();
            Result = superhuman ? "Chuck Norris" : "a caveman";
            return superhuman;
        }
        public static bool? HasHorns() { return true; }
        public static bool? HasOneHorn()
        {
            var one = DateTime.Now.Second % 2 == 0;
            Result = one ? "a unicorn" : "a stag";
            return one;
        }
        public static bool? Poodle()
        {
            Result = "OK, so it's a Poodle!";
            return null;
        }
        public static bool? CreatureFound()
        {
            Result = $"It must be {Result}!";
            return null;
        }
        public static void AddRulesAndCheck(Rude rude)
        {
            rude.AddRule(IsAnimal, HasLegs, CreatureFound);
            rude.AddRule(HasLegs, HasTwoLegs, CreatureFound);
            rude.AddRule(HasTwoLegs, CanCountToInfinity, HasHorns);
            rude.AddRule(CanCountToInfinity, CreatureFound, CreatureFound);
            rude.AddRule(HasHorns, HasOneHorn, Poodle);
            rude.AddRule(HasOneHorn, CreatureFound, CreatureFound);
            rude.AddRule(Poodle, null, null);
            rude.AddRule(CreatureFound, null, null);

            rude.CheckConditions(IsAnimal);
        }
    }
}
