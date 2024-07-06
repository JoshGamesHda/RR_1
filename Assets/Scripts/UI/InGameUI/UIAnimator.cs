using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAnimator : MonoBehaviour
{
    enum Interpolation
    {
        Linear,
        Exponential,
        InverseExponential,
        Lerp,
    }

    [SerializeField] private Vector3 originalPos;
    [SerializeField] private Vector3 startFromPos;

    [SerializeField] private Interpolation interpolationType;
    [SerializeField] private float animationSpeed;

    public void StartAnimation()
    {
        StartCoroutine(AnimatePosition());
    }

    private IEnumerator AnimatePosition()
    {
        Vector3 startPosition = startFromPos;
        Vector3 endPosition = originalPos;
        float elapsedTime = 0;

        while (elapsedTime < animationSpeed)
        {
            float t = elapsedTime / animationSpeed;
            switch (interpolationType)
            {
                case Interpolation.Linear:
                    transform.position = Vector3.Lerp(startPosition, endPosition, t);
                    break;
                case Interpolation.Exponential:
                    transform.position = Vector3.Lerp(startPosition, endPosition, t * t);
                    break;
                case Interpolation.InverseExponential:
                    transform.position = Vector3.Lerp(startPosition, endPosition, Mathf.Sqrt(t));
                    break;
                case Interpolation.Lerp:
                    transform.position = Vector3.Lerp(startPosition, endPosition, t);
                    break;
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = endPosition; // Ensure the final position is set
    }
}
