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
        private readonly string _initialState;
        private List<Computed> _reactiveNodeList;
        public ReactiveEngine(string initialState)
        {
            _initialState = initialState;
            SetReactiveNodes();
        }

        public bool IsExpNode(string exp)
        {
            if (exp.Trim().StartsWith("="))
            {
                return true;
            }

            return false;
        }

        private void SetReactiveNodes()
        {
            var nodes = _initialState.Split(',');
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
                if (IsExpNode(node.NodeVal))
                {
                    resComputedArray.Add((int)getComputedVal(i));
                }
                else
                {
                    resComputedArray.Add(int.Parse(_reactiveNodeList[i].NodeVal));
                }
                i++;
            });

            return string.Join(",", resComputedArray.Select((node, index) => { return $"[{index}:{node}]"; }));
        }

        public int? getComputedVal(int index)
        {
            if (IsExpNode(_reactiveNodeList[index].NodeVal))
            {
                var refIndexes = getIndexesListFromExp(_reactiveNodeList[index].NodeVal);
                var exp = _reactiveNodeList[index].NodeVal;
                refIndexes.ForEach(index => { 
                    var computed = getComputedVal(index);
                    exp = exp.Replace(string.Concat("{", index, "}"), computed.ToString());
                });
                var res = Convert.ToInt32(new DataTable().Compute(exp.Replace("=",string.Empty), null));
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
