using System.Collections.Generic;

public class CalculatorViewModel
{

    public delegate void OnPropertyChanged(string message);
    
    public event OnPropertyChanged Notify;

    private readonly CalculatorModel _calculatorModel;

    private string FirstOperand 
    {
        get => _calculatorModel.operand1;
        set => _calculatorModel.operand1 = value;
    }

    private string SecondOperand
    {
        get => _calculatorModel.operand2;
        set => _calculatorModel.operand2 = value;
    }

    private char Operation
    {
        get => _calculatorModel.operation;
        set => _calculatorModel.operation = value;
    }

    public CalculatorViewModel()
    {
        _calculatorModel = new CalculatorModel();
    }
    


    public void SetOperation(char operation)
    {
        switch (operation)
        {
            case 'C':
                FirstOperand = string.Empty;
                SecondOperand = string.Empty;
                Operation = ' ';
                Notify?.Invoke(string.Empty);
                break;
            case '=':
            {
                if (int.TryParse(FirstOperand, out _) && int.TryParse(SecondOperand, out _))
                {
                    _calculatorModel.Calculate();
                    Notify?.Invoke(_calculatorModel.calculationResult);
                }
                break;
            }
            default:
            {
                if (int.TryParse(FirstOperand, out _) && new List<char>{'/', '*','+', '-'}.Contains(operation) && SecondOperand.Equals(string.Empty))
                {
                    Operation = operation;
                    Notify?.Invoke($"{FirstOperand}{Operation}");
                }
                break;
            }
        }
    }

    public void SetOperand(char operand)
    {
        if (char.IsDigit(operand))
        {
            if(Operation.Equals(' '))
                FirstOperand += operand;
            else
                SecondOperand += operand;
            Notify?.Invoke($"{FirstOperand}{Operation}{SecondOperand}");
        }
    }
}