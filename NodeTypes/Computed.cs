using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ReactiveSystem
{
    public class Computed
    {
        private string _nodeVal;

        public Computed(string expression)
        {
            _nodeVal = expression;
        }
        public string NodeVal
        {
            get { return _nodeVal; }
            set { _nodeVal = value; }
        }

    }
}
