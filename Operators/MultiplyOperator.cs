using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Operators
{
    public class MultiplyOperator : IOperand
    {
        

        public string Symbol
        {
            get { return "*"; }
        }
        /// <summary>
        /// The executrion to provide a result
        /// </summary>
        /// <param name="leftPart">left part of the operatoin</param>
        /// <param name="rightPart">right part of teh operation</param>
        /// <returns>value or NULL</returns>
        public string GetResult(string leftPart, string rightPart)
        {
            double dLeftPart, dRightPart;
            dLeftPart = dRightPart = 0;
            if (double.TryParse(leftPart, out dLeftPart))
            {
                if (double.TryParse(rightPart, out dRightPart))
                {
                    return (dRightPart * dLeftPart).ToString();
                }
            }
            return null;
        }
    }
}
