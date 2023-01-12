using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class BezierUtilities
    {
        public static Vector3 GetPoint(BezierCurve curve, float t)
        {
            t = Mathf.Clamp01(t);
            float oneMinusT = 1f - t;
            return oneMinusT * oneMinusT * oneMinusT * curve.p0 +
                   3f * oneMinusT * oneMinusT * t * curve.p1 +
                   3f * oneMinusT * t * t * curve.p2 +
                   t * t * t * curve.p3;
        }
        
        public static List<Vector3> GetWaypoints(BezierCurve curve, float sample)
        {
            List<Vector3> waypoints = new List<Vector3>();
            float step = 1f / (sample - 1);
            for (float i = 0; i <= 1; i += step)
            {
                waypoints.Add(GetPoint(curve, i));
            }
            return waypoints;
        }
        
        
    }
}