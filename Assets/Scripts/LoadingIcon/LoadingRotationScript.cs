using UnityEngine;

public class LoadingRotationScript : MonoBehaviour
{
    public RectTransform _mainIcon;
    public float _timeStep;
    public float _oneStepAngle;

    float _startTime;
    void Start()
    {
        _startTime = Time.time;
    }

    void Update()
    {
        if (Time.time - _startTime >= _timeStep)
        {
            Vector3 iconAngle = _mainIcon.localEulerAngles;
            iconAngle.z += _oneStepAngle;

            _mainIcon.localEulerAngles = iconAngle;

            _startTime = Time.time;
        }
    }
}
