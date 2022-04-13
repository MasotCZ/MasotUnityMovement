using Masot.Standard.Input;
using UnityEngine;

namespace Masot.Standard.Movement
{
    [CreateAssetMenu(fileName = "InputMovementSettings", menuName = "Input/InputMovementSettings")]
    public class InputMovementSettings : ScriptableObject
    {
        public MovementInputDefine[] definedInput = {
            new MovementInputDefine(new InputDefine(KeyCode.W, GetKeyType.Hold), MovementType.Up),
            new MovementInputDefine(new InputDefine(KeyCode.A, GetKeyType.Hold), MovementType.Left),
            new MovementInputDefine(new InputDefine(KeyCode.S, GetKeyType.Hold), MovementType.Down),
            new MovementInputDefine(new InputDefine(KeyCode.D, GetKeyType.Hold), MovementType.Right)
        };
    }
}