using System;
using UnityEngine;

public class CalculatorModel
{
    public Action OnChangedInput;
    
    public Action OnErrorInput;

    private float? _calculationResult;

    private float? _operand1;

    private float? _operand2;

    private ICalculable _currentOperation;

    public CalculatorModel() => Clear();

    public ICalculable CurrentOperation
    {
        get => _currentOperation;
        private set
        {
            _currentOperation = value;
            OnChangedInput?.Invoke();
        }
    }

    public float? CalculationResult
    {
        get => _calculationResult;
        private set
        {
            if (value == null) return;
            _calculationResult = value.Value;
            OnChangedInput?.Invoke();
        }
    }

    public float? Operand1
    {
        get => _operand1;
        private set
        {
            if (value == null) return;
            _operand1 = value.Value;
            OnChangedInput?.Invoke();
        }
    }

    public float? Operand2
    {
        get => _operand2;
        private set
        {
            if (value == null) return;
            _operand2 = value.Value;
            OnChangedInput?.Invoke();
        }
    }

    public void ResetFirstOperand(float fValue)
    {
        if (Operand1.HasValue)
        {
            Operand1 = Operand1 * 10 + fValue;
        }
        else
        {
            Operand1 = fValue;
        }
    }

    public void ResetSecondOperand(float fValue)
    {
        if (Operand2.HasValue)
        {
            Operand2 = Operand2 * 10 + fValue;
        }
        else
        {
            Operand2 = fValue;
        }
    }

    public void Clear()
    {
        _operand1 = 0;
        _operand2 = null;
        _currentOperation = null;
        _calculationResult = null;
        OnChangedInput?.Invoke();
    }

    public void Calculate()
    {
        if (!Operand2.HasValue || !Operand1.HasValue) return;

        if (CurrentOperation.Validation(Operand1.Value, Operand2.Value))
        {
            CalculationResult = CurrentOperation.Calculation(Operand1.Value, Operand2.Value);
        }
        else
        {
            OnErrorInput?.Invoke();
        }

    }

    public void SetOperation(ICalculable operation)
    {
        CurrentOperation = operation;
    }
}


public interface ICalculable
{
    public string Symbol { get; }

    public float Calculation(float operand1, float operand2);

    public bool Validation(float operand1, float operand2);
}

public class Multiplication : ICalculable
{
    public string Symbol => "*";

    public float Calculation(float operand1, float operand2)
    {
        return operand1 * operand2;
    }

    public bool Validation(float operand1, float operand2)
    {
        return true;
    }
}

public class Difference : ICalculable
{
    public string Symbol => "-";

    public float Calculation(float operand1, float operand2)
    {
        return operand1 - operand2;
    }

    public bool Validation(float operand1, float operand2)
    {
        return true;
    }
}

public class Addition : ICalculable
{
    public string Symbol => "+";
    
    public float Calculation(float operand1, float operand2)
    {
        return operand1 + operand2;
    }

    public bool Validation(float operand1, float operand2)
    {
        return true;
    }
}

public class Division : ICalculable
{
    public string Symbol => "/";
    
    public float Calculation(float operand1, float operand2)
    {
        return operand1 / operand2;
    }

    public bool Validation(float operand1, float operand2)
    {
        return Math.Abs(operand2) > float.Epsilon;
    }
}