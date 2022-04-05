using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CalculatorView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _calculationTextBox;
    [SerializeField] private List<CalculatorButton> _calculatorButtons;

    private CalculatorViewModel _calculatorViewModel;
    
    private void Awake()
    {
        
        _calculatorViewModel = new CalculatorViewModel();
        
        _calculatorViewModel.Notify += OnPropertyChanged;
        
        _calculatorButtons.ForEach(calculatorButton =>
        {
            calculatorButton.button.onClick.AddListener(() =>
            {
                switch (calculatorButton.buttonType)
                {
                    case ButtonType.Plus:
                        _calculatorViewModel.SetOperation('+');
                        break;
                    case ButtonType.Minus:
                        _calculatorViewModel.SetOperation('-');
                        break;
                    case ButtonType.Multiplication:
                        _calculatorViewModel.SetOperation('*');
                        break;
                    case ButtonType.Division:
                        _calculatorViewModel.SetOperation('/');
                        break;
                    case ButtonType.Clear:
                        _calculatorViewModel.SetOperation('C');
                        break;
                    case ButtonType.Equality:
                        _calculatorViewModel.SetOperation('=');
                        break;
                    case ButtonType.Digit:
                        _calculatorViewModel.SetOperand(calculatorButton.value);
                        break;
                }
            });
        });
    }
    
    private void OnPropertyChanged(string text)
    {
        _calculationTextBox.text = text;
    }

    private void OnDestroy()
    {
        if(_calculationTextBox)
            _calculatorViewModel.Notify -= OnPropertyChanged;
    }

}

[System.Serializable]
public class CalculatorButton
{
    public Button button;
    public ButtonType buttonType;
    public char value;
}

public enum ButtonType 
{
    Plus,
    Minus,
    Multiplication,
    Division,
    Clear,
    Equality,
    Digit
}