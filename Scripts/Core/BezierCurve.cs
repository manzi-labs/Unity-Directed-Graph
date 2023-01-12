using System;
using UnityEngine;

namespace Core
{
    [Serializable]
    public class BezierCurve
    {
        public Vector3 p0, p1, p2, p3;

        public BezierCurve(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
        {
            this.p0 = p0;
            this.p1 = p1;
            this.p2 = p2;
            this.p3 = p3;
        }

    }
}