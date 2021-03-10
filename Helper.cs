using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ReactiveSystem
{
    public static class Helper
    {
        public static int computeExp(string exp)
        {
            string tempExp = exp;
            int res = 0;
            string[] numbers = exp.Split(new Char[] { '+', '-', '*', '/' });
            res = int.Parse(numbers[0]);
            int operand2;

            for (int i = 1; i < numbers.Length; i++)
            {
                int indexOperator = tempExp.IndexOfAny(new Char[] { '+', '-', '*', '/' });
                operand2 = int.Parse(numbers[i]);

                switch (tempExp[indexOperator])
                {
                    case '+':
                        res = res + operand2;
                        break;
                    case '-':
                        res = res - operand2;
                        break;
                    case '*':
                        res = res * operand2;
                        break;
                    case '/':
                        res = res / operand2;
                        break;
                }

                tempExp = tempExp.Substring(indexOperator + 1);
            }

            return res;
        }
    }
}
