using System;
using UnityEngine;
using UnityEngine.UI;

public class CalculatorOperandButtonView : MonoBehaviour, IInputButtonView
{ 
    
    [SerializeField] private string _value;
    
    private Button _button;

    private void Awake() => _button = GetComponent<Button>();

    public void SetListener(CalculatorViewModel calculatorViewModel) =>
        _button.onClick.AddListener(() => calculatorViewModel?.SetValue(_value));
}