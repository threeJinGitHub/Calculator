using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculatorModel
{
    public char operation;

    public string calculationResult;

    public string operand1;

    public string operand2;

    public CalculatorModel()
    {
        operand1 = string.Empty;
        operand2 = string.Empty;
        operation = ' ';
        calculationResult = string.Empty;
    }

    public void Calculate()
    {
        calculationResult = operation switch
        {
            '*' => (int.Parse(operand1) * int.Parse(operand2)).ToString(),
            '/' => (int.Parse(operand1) / int.Parse(operand2)).ToString(),
            '-' => (int.Parse(operand1) - int.Parse(operand2)).ToString(),
            '+' => (int.Parse(operand1) + int.Parse(operand2)).ToString(),
        };
        operand1 = string.Empty;
        operand2 = string.Empty;
        operation = ' ';
    }
}
