using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
using System.Data;

namespace ReactiveSystem
{
    public class ReactiveEngine
    {
        private List<Computed> _reactiveNodeList;
        public ReactiveEngine(string initialState)
        {
            SetReactiveNodes(initialState);
        }

        private void SetReactiveNodes(string initialState)
        {
            var nodes = initialState.Split(',');
            _reactiveNodeList = new List<Computed>();
            for (int i = 0; i < nodes.Length; i++)
            {
                _reactiveNodeList.Add(new Computed(nodes[i].Trim()));
            }
        }

        public string calculateCurrentState()
        {
            var resComputedArray = new List<int>();
            int i = 0;

            _reactiveNodeList.ForEach(node =>
            {
                resComputedArray.Add((int)getComputedVal(i));
                i++;
            });

            return string.Join(",", resComputedArray.Select((node, index) => { return $"[{index}:{node}]"; }));
        }

        public int? getComputedVal(int index)
        {
            if (_reactiveNodeList[index].ComputedVal != null)
            {
                return _reactiveNodeList[index].ComputedVal;
            }

            if (_reactiveNodeList[index].IsExpNode())
            {
                var refIndexes = getIndexesListFromExp(_reactiveNodeList[index].NodeVal);
                var exp = _reactiveNodeList[index].NodeVal;
                refIndexes.ForEach(index => { 
                    var computed = getComputedVal(index);
                    _reactiveNodeList[index].ComputedVal = computed;
                    exp = exp.Replace(string.Concat("{", index, "}"), computed.ToString());
                });
                var res = Helper.computeExp(exp);
                return res;
            }
            else
            {
                return int.Parse(_reactiveNodeList[index].NodeVal);
            }
        }

        private List<int> getIndexesListFromExp(string exp)
        {
            var resList = new List<int>();
            var regex = new Regex("{(.*?)}");
            var matches = regex.Matches(exp);
            foreach (Match match in matches)
            {
                resList.Add(int.Parse(match.Groups[1].Value));
            }
            return resList;
        }

        public void setNode(int index, string value)
        {
            _reactiveNodeList[index].NodeVal = value;
        }
    }
}
