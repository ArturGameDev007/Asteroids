using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI.GameScreen
{
    public class RestartButton : MonoBehaviour
    {
        //public Button Button => GetComponent<Button>();

        [field: SerializeField] public Button Button { get; private set; }

        private void OnValidate()
        {
            Button = GetComponent<Button>();
        }
    }
}
