using UnityEngine;

namespace _Project.Scripts.UI.StartMenu
{
    public class LoadingView : MonoBehaviour,  ILoadingView
    {
        public void Show()
        {
            gameObject.SetActive(true);
        }
    }
}