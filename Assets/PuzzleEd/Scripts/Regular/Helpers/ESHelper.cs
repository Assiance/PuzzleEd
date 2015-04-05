using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.PuzzleEd.Scripts.Regular.Helpers
{
    public static class ESHelper
    {
        public static IEnumerator CallIn(float seconds, Action<int, Vector3> method)
        {
            yield return new WaitForSeconds(seconds);

            method.Invoke(6, new Vector3());
        }
    }
}
