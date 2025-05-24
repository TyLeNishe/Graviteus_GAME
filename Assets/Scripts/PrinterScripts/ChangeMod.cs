using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ModeSwitcher : MonoBehaviour
{
    [Header("Text Settings")]
    [SerializeField] private Text _targetText;

    [Header("Mode Settings")]
    [SerializeField] private string _firstMode = "Переработка";
    [SerializeField] private string _secondMode = "Печать";
    private Button _button;
    private bool _isFirstMode = true;
    public string CurrentModeTag { get; private set; }

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(SwitchMode);

        UpdateMode();
    }

    private void SwitchMode()
    {
        _isFirstMode = !_isFirstMode;
        UpdateMode();
    }

    private void UpdateMode()
    {
        if (_isFirstMode)
        {
            _targetText.text = $"Режим: {_firstMode}";
            CurrentModeTag = "Recycle";
        }
        else
        {
            _targetText.text = $"Режим: {_secondMode}";
            CurrentModeTag = "Print";
        }
    }

    private void OnDestroy()
    {
        if (_button != null)
        {
            _button.onClick.RemoveListener(SwitchMode);
        }
    }
    public bool IsRecycleMode() => _isFirstMode;
}