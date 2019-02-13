using UnityEngine;
using UnityEngine.UI;

public class VelocityDampingCalculations : MonoBehaviour
{
    [Header("Object References")]
    [SerializeField]
    private MyScrollRect _scrollRect;
    [SerializeField]
    private Text _expectedDurationLbl;
    [SerializeField]
    private Text _expectedDistanceLbl;
    [SerializeField]
    private Text _actualDurationLbl;
    [SerializeField]
    private Text _actualDistanceLbl;

    [Header("Parameters")]
    [SerializeField]
    private float _initialVelocity = 1000f;

    private float _timeCounter;
    private float _startPos;


    private void LateUpdate()
    {
        if(Mathf.Abs(_scrollRect.velocity.y) >= 1)
        {
            _timeCounter += Time.deltaTime;
            _actualDurationLbl.text = "Actual Duration: " + _timeCounter;
            _actualDistanceLbl.text = "Actual Distance: " + (_scrollRect.content.anchoredPosition.y - _startPos);
        }
    }

    public void RunTest()
    {
        CalculateExpectedDistanceAndDuration(_initialVelocity, 1f, _scrollRect.decelerationRate);

        if(_initialVelocity > 0)
        {
            _scrollRect.verticalNormalizedPosition = 1f;
        }
        else
        {
            _scrollRect.verticalNormalizedPosition = 0f;
        }
        
        _scrollRect.velocity = Vector2.up * _initialVelocity;
        _startPos = _scrollRect.content.anchoredPosition.y;
        _timeCounter = 0f;
    }

    // Distance should only refer to positive values.
    // I misused the term "distance" in the tutorial videos because my first
    // iteration of the tutorial only used positive velocity values.
    // A vector term such as "displacement" or "movementDelta" would be better.
    public void CalculateExpectedDistanceAndDuration(float v0, float vf, float r)
    {
        float duration = 0f;
        float distance = 0f;

        // We can't take the log of a negative number so save the sign
        // and use the absolute value for the calculations.
        float sign = Mathf.Sign(v0);
        v0 = Mathf.Abs(v0);

        // Write equations here.
        duration = Mathf.Log(vf / v0) / Mathf.Log(r);
        distance = (vf - v0) / Mathf.Log(r) * sign;

        _expectedDurationLbl.text = "Expected Duration: " + duration;
        _expectedDistanceLbl.text = "Expected Distance: " + distance;
    }
}