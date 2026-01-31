using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Utils
{
    public class ButtonScanner
    {
        public bool TryGetInStack<T>(Transform root, out T component) where T : Component
        {
            Stack<Transform> stack = new Stack<Transform>();
            stack.Push(root);

            while (stack.Count > 0)
            {
                Transform current = stack.Pop();

                if (current != root && current.TryGetComponent(out component))
                    return true;

                for (int i = 0; i < current.childCount; i++)
                    stack.Push(current.GetChild(i));
            }

            component = null;
            return false;
        }
    }
}