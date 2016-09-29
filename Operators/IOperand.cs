using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Operators
{
    //Interface for Operator
    public interface IOperand
    {
        /// <summary>
        /// The symbol representing the operation
        /// </summary>
        string Symbol { get; }
        /// <summary>
        /// The executrion to provide a result
        /// </summary>
        /// <param name="leftPart">left part of the operatoin</param>
        /// <param name="rightPart">right part of teh operation</param>
        /// <returns>value or NULL</returns>
        string GetResult(string leftPart, string rightPart);

    }
}
