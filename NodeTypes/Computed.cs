using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ReactiveSystem
{
    public class Computed
    {
        private string _nodeVal;
        private int? _computedVal;

        public Computed(string nodeVal)
        {
            _nodeVal = nodeVal.Replace("=", "");
        }
        public bool IsExpNode()
        {
            return !int.TryParse(_nodeVal, out _);
        }
        public string NodeVal
        {
            get { return _nodeVal; }
            set { _nodeVal = value; _computedVal = null; }
        }

        public int? ComputedVal
        {
            get { return _computedVal; }
            set { _computedVal = value; }
        }

    }
}
