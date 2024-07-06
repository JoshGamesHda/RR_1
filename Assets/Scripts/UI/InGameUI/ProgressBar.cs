using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] Transform valueMeter;

    private Vector3 originalScale;
    private float originalValue;
    private float value;

    //Can really only be called ONCE, else it will change it's originalScale and always reset to an empty bar
    public void InitializeBar(float maxValue)
    {
        originalValue = maxValue;
        originalScale = valueMeter.localScale;
    }
    public void ResetValue()
    {
        valueMeter.localScale = originalScale;
        value = originalValue;
    }
    public void UpdateBar(float newValue)
    {
        value = newValue;

        float ratio = 0;
        if(originalValue != 0) ratio = value / originalValue;

        if(ratio != 0) valueMeter.localScale = new Vector3(ratio * originalScale.x, originalScale.y, originalScale.z);
    }
}
