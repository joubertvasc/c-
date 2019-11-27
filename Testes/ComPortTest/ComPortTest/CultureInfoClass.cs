
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace ComPortTest
{
    public class CultureInfoClass
    {
        int _index;
        string _name;
        int _identifier;
        string _culture;

        public int Index
        {
            get { return _index; }
            set { _index = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public int Identifier
        {
            get { return _identifier; }
            set { _identifier = value; }
        }

        public string Culture
        {
            get { return _culture; }
            set { _culture = value; }
        }
    }
}
