using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Dynamic;

namespace Switches
{
    public class SwitchSet : DynamicObject
    {
        private Dictionary<string, string> settingByName;
        private HashSet<string> switches;

        public SwitchSet(string[] args)
        {
            settingByName = new Dictionary<string, string>();
            switches = new HashSet<string>();

            var lastName = default(string);

            foreach (var token in args)
            {
                if (token.StartsWith("-"))
                {
                    lastName = token.Substring(1);
                    switches.Add(lastName);
                }
                else
                {
                    switches.Remove(lastName);
                    settingByName[lastName] = token;
                }
            }
        }

        private const string settingExistsSuffix = "settingexists";
        private const string settingSuffix = "setting";

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            var bindingName = binder.Name.ToLower();

            
            if (bindingName.EndsWith(settingExistsSuffix))
            {
                var settingName = binder.Name.Substring(0, binder.Name.Length - settingExistsSuffix.Length);
                result = switches.Contains(settingName) || settingByName.ContainsKey(settingName);
                return true;
            }

            if (bindingName.EndsWith(settingSuffix))
            {
                var settingName = binder.Name.Substring(0, binder.Name.Length - settingSuffix.Length);

                if (switches.Contains(settingName))
                {
                    result = true;
                    return true;
                }

                var setting = default(string);
                if (settingByName.TryGetValue(settingName, out setting))
                {
                    result = setting;
                    return true;
                }
            }

            result = null;
            return false;
        }
    }
}
