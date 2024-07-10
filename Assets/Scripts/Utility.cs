using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility
{

    public static Vector3Int Vec2IntToVec3(Vector2Int v2Int)
    {
        return new Vector3Int(v2Int.x, 0, v2Int.y);
    }
    public static Vector2 Vec3ToVec2(Vector3 vec)
    {
        return new Vector2(vec.x, vec.z);
    }
    public static Vector3 IntersectionPointRayWithXZPlane(Ray ray)
    {
        Vector3 startingPoint = ray.origin;
        Vector3 direction = ray.direction;

        direction = Vector3.Normalize(direction);

        if (Mathf.Approximately(direction.y, 0f))
        {
            Debug.Log("Ray parallel to ground, how tf did you do this");
            return Vector3.zero;
        }

        float t = -startingPoint.y / direction.y;

        float xIntersection = startingPoint.x + direction.x * t;
        float zIntersection = startingPoint.z + direction.z * t;

        return new Vector3(xIntersection, 0, zIntersection);
    }

    // What is this
    public static bool HasComponent<T>(this GameObject obj) where T : Component
    {
        return obj.GetComponent<T>() != null;
    }

    public static Vector2 RandPosOnCircle(Vector2 pos, float radius)
    {
        float randAngle = Random.Range(0, 2 * Mathf.PI);
        float randRadius = Mathf.Sqrt(Random.Range(0, 1f)) * radius;

        return new Vector2(pos.x + Mathf.Cos(randAngle) * randRadius, pos.y + Mathf.Sin(randAngle) * randRadius);
    }

    public static float RoundTo2Decimals(float value)
    {
        float t = value * 100;
        return Mathf.Round(t)/100;
    }
}
