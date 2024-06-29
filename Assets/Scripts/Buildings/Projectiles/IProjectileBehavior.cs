using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface IProjectileBehaviour
{
    public abstract void UpdateTrajectory(GameObject projectile, Vector3 origin, Vector3 target, float speed, float launchTime);
}

public class StraightShot : IProjectileBehaviour
{
    public void UpdateTrajectory(GameObject projectile, Vector3 origin, Vector3 target, float speed, float launchTime)
    {
        Vector3 direction = (target - origin).normalized;

        projectile.transform.position += direction * speed;

        projectile.transform.rotation = Quaternion.LookRotation(direction);
    }
}

public class Trajectory : IProjectileBehaviour
{
    private const float peakHeight = 10.0f; // Maximum height of the parabola

    public void UpdateTrajectory(GameObject projectile, Vector3 origin, Vector3 target, float speed, float launchTime)
    {
        float distance = Vector3.Distance(origin, target);

        float totalTime = distance / speed;

        float elapsedTime = Time.time - launchTime;

        float t = elapsedTime / totalTime;

        if (t > 1.0f) t = 1.0f;

        Vector3 midPoint = (origin + target) / 2;
        midPoint.y = Mathf.Max(origin.y, target.y) + peakHeight;

        Vector3 currentPosition = CalculateBezierPoint(t, origin, midPoint, target);

        projectile.transform.position = currentPosition;

        Vector3 nextPosition = CalculateBezierPoint(t + 0.01f, origin, midPoint, target); 
        Vector3 direction = (nextPosition - currentPosition).normalized;
        projectile.transform.rotation = Quaternion.LookRotation(direction);
    }

    private Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;

        Vector3 point = uu * p0;
        point += 2 * u * t * p1;
        point += tt * p2;        

        return point;
    }
}