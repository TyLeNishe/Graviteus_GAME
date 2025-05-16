using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class CounterButton : MonoBehaviour
{
    [SerializeField] private Text _targetText;
    [SerializeField] private int _step = 1;
    [SerializeField] private int _defaultValue = 0;

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(ModifyValue);

        if (!int.TryParse(_targetText.text, out _))
        {
            _targetText.text = _defaultValue.ToString();
        }
    }

    private void ModifyValue()
    {
        if (int.TryParse(_targetText.text, out int currentValue))
        {
            if (currentValue + _step >= 0)
            {
                currentValue += _step;
                _targetText.text = currentValue.ToString();
            }
        }
        else
        {
            _targetText.text = _defaultValue.ToString();
        }
    }
}