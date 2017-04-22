using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace RudeExample
{
    public class DynamicRules
    {
        private Rude _rude;
        private int _value;
        private Random _rnd;

        public string Title { get { return "Dynamic Rules"; } }
        public string Result { get { return "The result is: " + _value; } }

        public DynamicRules(Rude rude, int value)
        {
            _rude = rude;
            _value = value;
            _rnd = new Random();

            AddRules();
        }

        private void AddRules()
        {
            var rules = new List<Rude.JsonRule>();

            rules.Add(new Rude.JsonRule("IsEven", "Decreased", "IncreasedByValue"));
            rules.Add(new Rude.JsonRule("IsRandomEven", "Increased", "MultipliedByRandom"));
            rules.Add(new Rude.JsonRule("IsDivisibleBy3", "IsOdd", "IsRandomEven"));
            rules.Add(new Rude.JsonRule("IsDivisibleBy5", "Increased", "MultipliedByRandom"));
            rules.Add(new Rude.JsonRule("IsOdd", "DecreasedByValue", "IsDivisibleBy5"));
            rules.Add(new Rude.JsonRule("Increased", "IsEven", null));
            rules.Add(new Rude.JsonRule("IncreasedByValue", "IsDivisibleBy3", null));
            rules.Add(new Rude.JsonRule("Decreased", "IsRandomEven", null));
            rules.Add(new Rude.JsonRule("DecreasedByValue", "IsRandomEven", null));
            rules.Add(new Rude.JsonRule("MultipliedByRandom", null, null));

            _rude.AddRules(this, JsonConvert.SerializeObject(rules));
        }

        public void CheckConditions()
        {
            _rude.CheckConditions(IsRandomEven);
        }

        public bool? IsEven()
        {
            return _value % 2 == 0;
        }
        public bool? IsRandomEven()
        {
            return _rnd.Next(50) % 2 == 0;
        }
        public bool? IsOdd()
        {
            return !IsEven();
        }
        public bool? IsDivisibleBy3()
        {
            return _value % 3 == 0;
        }
        public bool? IsDivisibleBy5()
        {
            return _value % 5 == 0;
        }
        public bool? Increased()
        {
            _value++;
            return true;
        }
        public bool? IncreasedByValue()
        {
            _value += _value;
            return true;
        }
        public bool? Decreased()
        {
            _value--;
            return true;
        }
        public bool? DecreasedByValue()
        {
            _value -= _value;
            return true;
        }
        public bool? MultipliedByRandom()
        {
            _value *= _rnd.Next(300);
            return null;
        }
    }
}
