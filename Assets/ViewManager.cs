using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ViewManager : MonoBehaviour
{
    [SerializeField] private CalculationView _input;
    [SerializeField] private List<CalculatorOperationButtonView> _operationButtons;
    [SerializeField] private List<CalculatorOperandButtonView> _operandButtons;
    
    private void Start()
    {
        var calculatorViewModel =
            new CalculatorViewModel(
                new List<ICalculable>
                {
                    new Addition(), 
                    new Difference(),
                    new Division(),
                    new Multiplication()
                },
                new CalculatorModel());
        
        _input.SetSubscription(ref calculatorViewModel.OnPropertyChanged);

        _operandButtons.Union<IInputButtonView>(_operationButtons).ToList().ForEach(inputButton =>
            inputButton.SetListener(calculatorViewModel));
    }
}