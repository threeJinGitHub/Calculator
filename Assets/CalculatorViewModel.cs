using System;
using UnityEngine;

public class CalculatorViewModel
{

    public Action<string> OnPropertyChanged;

    private readonly CalculatorModel _calculatorModel;
    
    private void InputChanged()
    {
        var calculationResult = _calculatorModel.CalculationResult;
        if (calculationResult.HasValue)
        {
            OnPropertyChanged.Invoke(calculationResult.Value.ToString("F3"));
        }
        else if(_calculatorModel.Operand2.HasValue)
        {
            OnPropertyChanged?.Invoke(_calculatorModel.Operand1 +
                                      $"{GetSymbolByOperation(_calculatorModel.CurrentOperation)}" +
                                      _calculatorModel.Operand2);
        }
        else if (_calculatorModel.CurrentOperation != null)
        {
            OnPropertyChanged?.Invoke(_calculatorModel.Operand1 +
                                      $"{GetSymbolByOperation(_calculatorModel.CurrentOperation)}");
        }
        else if (_calculatorModel.Operand1.HasValue)
        {
            OnPropertyChanged?.Invoke(_calculatorModel.Operand1.ToString());
        }
        else if (!_calculatorModel.Operand1.HasValue)
        {
            OnPropertyChanged?.Invoke("0");
        }
    }

    private void ErrorInput()
    {
        OnPropertyChanged?.Invoke("error");        
    }

    private static string GetSymbolByOperation(ICalculable operation) =>
        operation switch
        {
            Division _ => "/",
            Difference _ => "-",
            Multiplication _ => "*",
            Addition _ => "+",
            _ => throw new AggregateException()
        };
    

    public CalculatorViewModel()
    {
        _calculatorModel = new CalculatorModel();
        _calculatorModel.OnChangedInput += InputChanged;
        _calculatorModel.OnErrorInput += ErrorInput;
    }

    public void SetValue(object value)
    {
        
        if (value is string sValue && sValue.Equals("C"))
        {
            _calculatorModel.Clear();
        }
        else if(!_calculatorModel.CalculationResult.HasValue)
        {
            if (float.TryParse(value.ToString(), out var fValue))
            {
                if(_calculatorModel.CurrentOperation == null)
                    _calculatorModel.ResetFirstOperand(fValue);
                else
                    _calculatorModel.ResetSecondOperand(fValue);
            }
            else if(_calculatorModel.Operand1.HasValue)
            {
                ICalculable operation = null;
                switch (value.ToString())
                {
                    case "=":
                        _calculatorModel.Calculate();
                        break;
                    case "/":
                        operation = new Division();
                        break;
                    case "-":
                        operation = new Difference();
                        break;
                    case "*":
                        operation = new Multiplication();
                        break;
                    case "+":
                        operation = new Addition();
                        break;
                }
                if (operation != null)
                    _calculatorModel.SetOperation(operation);
            }
        }
    }
}