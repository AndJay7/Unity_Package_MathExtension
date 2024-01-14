using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace And.Math.Standard
{
    public interface IEquation<T>
    {
        T Evaluate(float variable);
    }
}
