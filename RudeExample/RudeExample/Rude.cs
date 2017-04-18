using System.Collections.Generic;

namespace RudeExample
{
    public class Rude
    {
        public delegate bool? Callback();

        public Dictionary<string, Rule> Rules;

        private Dictionary<string, bool?> _path;

        public Rude()
        {
            Rules = new Dictionary<string, Rule>();
            _path = new Dictionary<string, bool?>();
        }

        public void AddRule(Callback condition, Callback yes, Callback no)
        {
            var n = condition.Method.Name;
            var r = new Rule { Condition = condition, Yes = yes, No = no };

            Rules.Add(n, r);
        }

        public void CheckConditions(Callback condition)
        {
            var loop = true;
            while (loop)
            {
                var rule = Rules[condition.Method.Name];
                var result = rule.Condition();

                _path.Add(condition.Method.Name, result);

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
    }
}
