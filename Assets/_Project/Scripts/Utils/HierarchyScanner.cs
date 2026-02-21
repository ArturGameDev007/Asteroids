using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Utils
{
    public class HierarchyScanner
    {
        public bool TryGetInStack<T>(Transform root, out T component) where T : Component
        {
            component = null;
            
            if (root == null)
                return false;

            Stack<Transform> stack = new Stack<Transform>();
            stack.Push(root);

            while (stack.TryPop(out Transform current))
            {
                if (current.TryGetComponent(out component))
                    return true;

                for (int i = 0; i < current.childCount; i++)
                    stack.Push(current.GetChild(i));
            }

            return false;
        }
    }
}