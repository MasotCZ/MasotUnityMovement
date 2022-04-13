using Masot.Standard.Utility;
using UnityEngine;

namespace Masot.Standard.Movement
{
    [CreateAssetMenu(fileName = "RotateWrapper2D", menuName = "Movement/Rotation2D")]
    public class RotateWrapper2DSettings : ScriptableObject
    {
        public float maximumEulerRotation = 360.0f;
        public float speedMultiplier = 1.0f;
        public bool instantRotation = false;
    }

    public static class RotateWrapper2DExtension
    {
        //---new
        public static void RotateBy(this Transform transform, RotateWrapper2DSettings settings, float deltaEuler)
        {
            if (settings.instantRotation)
            {
                transform._RotateBy(deltaEuler);
            }

            transform._RotateBy(settings, deltaEuler);
        }

        public static void RotateTo(this Transform transform, RotateWrapper2DSettings settings, float angleEuler)
        {
            if (settings.instantRotation)
            {
                transform._RotateTo(angleEuler);
            }

            var delta = Mathf.DeltaAngle(transform.rotation.eulerAngles.z, angleEuler);
            transform._RotateBy(settings, delta);
        }

        public static void RotateTo(this Transform transform, RotateWrapper2DSettings settings, Vector2 target)
        {
            var direction = MathMethods.Direction2D(transform.position, target);

            if (settings.instantRotation)
            {
                _RotateTo(transform, direction);
                return;
            }

            var delta = Mathf.DeltaAngle(transform.rotation.eulerAngles.z, MathMethods.AngleEuler2D(direction));
            transform._RotateBy(settings, delta);
        }

        private static void _RotateBy(this Transform transform, RotateWrapper2DSettings settings, float delta)
        {
            var maxRotation = settings.maximumEulerRotation * settings.speedMultiplier;
            delta = Mathf.Min(Mathf.Max(delta, -maxRotation), maxRotation);
            transform._RotateBy(delta);
        }

        private static void _RotateBy(this Transform transform, float delta)
        {
            transform.rotation *= Quaternion.Euler(0, 0, delta);
        }

        private static void _RotateTo(this Transform transform, Vector2 direction)
        {
            transform.right = direction;
        }

        private static void _RotateTo(this Transform transform, float angleEuler)
        {
            transform.rotation = Quaternion.Euler(0, 0, angleEuler);
        }
    }
}
