using System;
using TMPro;
using UnityEngine;

public class CalculationView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _calculationTextBox;

    public void SetSubscription(ref Action<string> onPropertyChanged) => onPropertyChanged += OnPropertyChanged;

    public void RemoveSubscription(ref Action<string> onPropertyChanged) => onPropertyChanged -= OnPropertyChanged;

    private void OnPropertyChanged(string text) => _calculationTextBox.text = text;
}