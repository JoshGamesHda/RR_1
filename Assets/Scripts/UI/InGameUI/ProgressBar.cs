using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] Transform valueMeter;

    private Vector3 originalScale;
    private float originalValue;
    private float value;

    public void InitializeBar(float origValue)
    {
        originalValue = origValue;
        originalScale = valueMeter.localScale;
    }

    public void ResetValue()
    {
        valueMeter.localScale = originalScale;
    }
    public void UpdateBar(float newValue)
    {
        value = newValue;

        float ratio = value / originalValue;

        Debug.Log("ratio: " + ratio);

        valueMeter.localScale = new Vector3(ratio * originalScale.x, originalScale.y, originalScale.z);
    }
}
