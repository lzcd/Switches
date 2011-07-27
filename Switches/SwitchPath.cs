using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Switches
{
    public class SwitchPath
    {
        private string tokenName;
        private Dictionary<string, Action<string>> actionByTokenName = new Dictionary<string, Action<string>>();

        public SwitchPath On(string token)
        {
            tokenName = token;
            return this;
        }

        public SwitchPath Do(Action action)
        {
            actionByTokenName[tokenName] = (s) => { action(); };
            return this;
        }

        public SwitchPath Do(Action<string> action)
        {
            actionByTokenName[tokenName] = (s) => { action(s); };
            return this;
        }

        public SwitchPath Ignore()
        {
            actionByTokenName[tokenName] = (s) => { };
            return this;
        }

        private Action<string> onRemainder;

        public SwitchPath OnRemainderDo(Action<string> action)
        {
            onRemainder = action;
            return this;
        }

        public void Parse(string[] args)
        {
            var lastName = default(string);
            var remaining = new List<string>();

            foreach (var token in args)
            {
                Action<string> action = null;
                if (token.StartsWith("-"))
                {
                    if (lastName != null)
                    {
                        if (actionByTokenName.TryGetValue(lastName, out action))
                        {
                            action("");
                            action = null;
                        }
                        else 
                        {
                            remaining.Add(token);
                        }
                    }

                    lastName = token.Substring(1);
                    continue;
                }


                
                if (lastName != null)
                {

                    if (!actionByTokenName.TryGetValue(lastName, out action))
                    {
                        remaining.Add(lastName);
                        lastName = null;
                        continue;
                    }
                }
                else
                {
                    if (!actionByTokenName.TryGetValue(token, out action))
                    {
                        remaining.Add(token);
                        continue;
                    }
                }
                action(token);
                lastName = null;

            }

            foreach (var remainder in remaining)
            {
                onRemainder(remainder);
            }
        }


    }
}
