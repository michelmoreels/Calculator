using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculator.Operators;

namespace Calculator
{
    public class Calculator
    {
        //Default constructor
        public Calculator()
        {
            //Initialize the operators
            InitializeOperators();
        }
        /// <summary>
        /// Calculate teh expressions
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public string Calculate(string expression)
        {
            //Split the string into numbers and operators
            List<string> splitString = SplitString(expression);
            return Calculate(splitString);


        }
        /// <summary>
        /// Splits the expression into different numbers and operators.
        /// Assumes teh first number is positive.
        /// //Todo, accompany for negatives
        /// </summary>
        /// <param name="expression">the expression as string</param>
        /// <returns>a list of strings that cotains the numbers and operators</returns>
        private List<string> SplitString(string expression)
        {
            List<string> valuesAndOperators = new List<string>();
            string currentItem = string.Empty;
            //Go thru the characters
            foreach(char character in expression)
            {

                if(IsValidNumberPart(character))
                {
                    //The character is part of a value (0-9 , or .)
                    currentItem += character;
                }
                else
                {
                    //We encountered a character that is not a number or decimal seperator
                    if (currentItem != string.Empty)
                    {
                        //The currentItem has a value, add it
                        valuesAndOperators.Add(currentItem);
                    }
                    if (IsValidOperator(character))
                    {
                        //It is an operator
                        valuesAndOperators.Add(character.ToString());
                    }
                    else
                    {
                        //The encountered item is a space, or something else
                        currentItem = string.Empty;
                    }
                }
            }
            //return the list
            return valuesAndOperators;
        }
        /// <summary>
        /// Check if the character is part of a valid number.
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        private  bool IsValidNumberPart(char character)
        {
            if (character >= '0' && character <= '9') return true;
            if (character == '.') return true;
            if (character == ',') return true;
            return false;
        }
        /// <summary>
        /// Check if the symbol is a valid operator
        /// </summary>
        /// <param name="character">the character to evaluate.</param>
        /// <returns></returns>
        private bool IsValidOperator(char character)
        {
            foreach(IOperand oper in operatorPriority.Values)
            {
                if(oper.Symbol == character.ToString())
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Recursive function to run thru each of the values in the split list of elements
        /// </summary>
        /// <param name="expressionParts">Split list of numbers and operators. This should be number, Operator, number, operator, number</param>
        /// <returns></returns>
        private string Calculate(List<string> expressionParts)
        {
            //temporary list that stores intermediate results
            List<string> tempContainer = new List<string>();
            //go thru the different operators that determine teh priority of execution
            foreach (int prio in operatorPriority.Keys)
            {
                //Determine position ion the list
                int currentIndex = 0;
                foreach(string strCurrentOperator in expressionParts)
                {
                    //Yes, we need to process 
                    if(strCurrentOperator == operatorPriority[prio].Symbol)
                    {
                        //We matched a symbol in this priority, execute the operation
                        string tmpResult = operatorPriority[prio].GetResult(expressionParts[currentIndex - 1], expressionParts[currentIndex + 1]);
                        //Check div by zero
                        if (tmpResult == null) { return null; }
                        //Add the result
                        tempContainer.Add(tmpResult);
                        //Add the rest of the expression to the tempContainer
                        tempContainer.AddRange(expressionParts.GetRange(currentIndex + 2, expressionParts.Count() - (currentIndex + 2)));
                        //make another pass on the tempCOntainer
                        return Calculate(tempContainer);
                    }
                    //Not the desired operator, add the item to the tempContainer
                    tempContainer.Add(strCurrentOperator);
                    currentIndex++;
                }
            }
            //There is only one left, and no more operators.
            return tempContainer.First();
        }
        #region Private functions and members
        Dictionary<int, IOperand> operatorPriority = null;
        //Set the priority of the different operators
        private void InitializeOperators()
        {

            operatorPriority = new Dictionary<int, IOperand>();
            int prio = 0;
            operatorPriority[prio] = new MultiplyOperator();
            prio++;
            operatorPriority[prio] = new DivideOperator();
            prio++;
            operatorPriority[prio] = new PlusOperator();
            prio++;
            operatorPriority[prio] = new MinusOperator();


        }
        #endregion
    }


    
}
