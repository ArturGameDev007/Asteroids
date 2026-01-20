using UnityEngine;

namespace _Project.Scripts.UI.GameScreen
{
    public class ScoreController : MonoBehaviour, IEnemyDeathListener
    {
        [SerializeField] private ScoreData _scoreData;
        
        public void Construct(ScoreData scoreData)
        {
            _scoreData = scoreData;
        }
        
        public void NotifyEnemyKilled()
        {
            _scoreData.AddScore();
        }
    }
}