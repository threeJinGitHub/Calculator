using UnityEngine;
using UnityEngine.UI;

public class CalculatorOperationButtonView : MonoBehaviour, IInputButtonView
{
    [SerializeField] private string _operation;
    
    private Button _button;

    private void Awake() => _button = GetComponent<Button>();

    public void SetListener(CalculatorViewModel calculatorViewModel) =>
        _button.onClick.AddListener(() => calculatorViewModel?.SetValue(_operation));
}