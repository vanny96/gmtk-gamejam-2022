using UnityEngine;
using UnityEngine.EventSystems;

namespace Util.ExtensionMethods
{
    public static class MoveDirectionExtension
    {
        public static Vector2 GetVector(this MoveDirection direction)
        {
            return direction switch
            {
                MoveDirection.Left => Vector2.left,
                MoveDirection.Up => Vector2.up,
                MoveDirection.Right => Vector2.right,
                MoveDirection.Down => Vector2.down,
                _ => Vector2.zero
            };
        }
    }
}