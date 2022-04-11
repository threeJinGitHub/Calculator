using System;
using System.Collections.Generic;
using System.Linq;

public class CalculatorViewModel
{

    public Action<string> OnPropertyChanged;

    private readonly CalculatorModel _calculatorModel;

    private readonly List<ICalculable> _operations;
    
    private void InputChanged()
    {
        var calculationResult = _calculatorModel.CalculationResult;
        if (calculationResult.HasValue)
        {
            OnPropertyChanged.Invoke(calculationResult.Value.ToString("F3"));
        }
        else if(_calculatorModel.Operand2.HasValue)
        {
            OnPropertyChanged?.Invoke(_calculatorModel.Operand1 + _calculatorModel.CurrentOperation.Symbol +
                                      _calculatorModel.Operand2);
        }
        else if (_calculatorModel.CurrentOperation != null)
        {
            OnPropertyChanged?.Invoke(_calculatorModel.Operand1 + _calculatorModel.CurrentOperation.Symbol);
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

    public CalculatorViewModel(List<ICalculable> operations, CalculatorModel calculatorModel)
    {
        _calculatorModel = calculatorModel;
        _operations = operations;
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
            else if (_calculatorModel.Operand2.HasValue && value.ToString().Equals("="))
            {
                _calculatorModel.Calculate();
            }
            else if(_calculatorModel.Operand1.HasValue)
            {
                var operation = _operations.FirstOrDefault(op => op.Symbol.Equals(value.ToString()));
                if (operation != null)
                    _calculatorModel.SetOperation(operation);
            }
        }
    }
}