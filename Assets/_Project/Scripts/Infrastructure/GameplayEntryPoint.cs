// using UnityEngine;
//
// namespace _Project.Scripts.Infrastructure
// {
//     public class GameplayEntryPoint : MonoBehaviour
//     {
//         [SerializeField] private GameplayCompositionRoot _gameplayCompositionRoot;
//
//         private Game _game;
//         
//         private void Awake()
//         {
//             _game = _gameplayCompositionRoot.Compose();
//         }
//
//         private void Start()
//         {
//             _game.Initialize();
//         }
//
//         private void Update()
//         {
//             _game.UpdateSpawn(Time.deltaTime);
//         }
//
//         private void OnDestroy()
//         {
//             _game.Dispose();
//         }
//     }
// }