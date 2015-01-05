using UnityEngine;

namespace Assets.PuzzleEd.Scripts.Regular.Extensions
{
    public static class TransformExtensions
    {
        public static void Translate(this Transform target, Vector2 vector2, float z)
        {
            target.Translate(vector2.x, vector2.y, z);
        }
    }
}
