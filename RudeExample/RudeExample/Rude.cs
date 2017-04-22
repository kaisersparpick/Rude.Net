using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace RudeExample
{
    public class Rude
    {
        public delegate bool? Callback();

        public Dictionary<string, Rule> Rules;

        private List<KeyValuePair<string, bool?>> _path;

        public Rude()
        {
            Rules = new Dictionary<string, Rule>();
            _path = new List<KeyValuePair<string, bool?>>();
        }

        public void AddRule(Callback condition, Callback yes, Callback no)
        {
            var n = condition.Method.Name;
            var r = new Rule { Condition = condition, Yes = yes, No = no };

            Rules.Add(n, r);
        }

        public void AddRules(object target, string json)
        {
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("JSON:");
            Console.WriteLine(json);

            var jsonRules = JsonConvert.DeserializeObject<List<JsonRule>>(json);

            foreach(var jRule in jsonRules)
            {
                AddRule(
                    jRule.ToCallback(target, jRule.Condition),
                    jRule.ToCallback(target, jRule.Yes), 
                    jRule.ToCallback(target, jRule.No));
            }
        }

        public void CheckConditions(Callback condition)
        {
            var loop = true;
            while (loop)
            {
                if (condition == null) break;

                var rule = Rules[condition.Method.Name];
                var result = rule.Condition();

                _path.Add(new KeyValuePair<string, bool?> (condition.Method.Name, result));

                switch (result)
                {
                    case true:
                        condition = rule.Yes;
                        break;
                    case false:
                        condition = rule.No;
                        break;
                    default:
                        loop = false;
                        break;
                }
            }
        }

        public string GetPath()
        {
            var ret = "";
            foreach (var p in _path)
            {
                var pre = p.Value == false ? "!" : "";
                var post = p.Value == null ? "" : " > ";
                ret += pre + p.Key + post;
            }
            return ret;
        }

        public class Rule
        {
            public Callback Condition;
            public Callback Yes;
            public Callback No;
        }

        public class JsonRule
        {
            public string Condition;
            public string Yes;
            public string No;

            public JsonRule(string condition, string yes, string no)
            {
                Condition = condition;
                Yes = yes;
                No = no;
            }

            public Callback ToCallback(object target, string field)
            {
                return field == null ? null : (Callback)Delegate.CreateDelegate(typeof(Callback), target, field);
            }
        }
    }
}
