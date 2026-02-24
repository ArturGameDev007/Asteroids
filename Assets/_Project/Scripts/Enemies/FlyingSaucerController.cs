using UnityEngine;

namespace _Project.Scripts.Enemies
{
    public class FlyingSaucerController : Enemy
    {
        [SerializeField] private Transform _target;
        [SerializeField] private float _rotationSpeed = 30f;
        
        public void Construct(Transform target)
        {
            _target = target;
        }
        
        protected override void Move()
        {
            Vector2 currentPosition = Head2D.position;
            Vector2 targetPosition = _target.position;
            
            Vector2 directionToPlayer = (targetPosition - currentPosition).normalized;
            RotateTowardPlayer(directionToPlayer);
            
            Vector2 directionMove = Vector2.MoveTowards(currentPosition, targetPosition, Speed * Time.deltaTime);
            Head2D.MovePosition(directionMove);
        }
        
        private void RotateTowardPlayer(Vector2 directionToPlayer)
        {
            float rotateX = 0;
            float rotateY = 0;
        
            float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
        
            Quaternion targetRotation = Quaternion.Euler(new Vector3(rotateX, rotateY, angle));
        
            transform.rotation =
                Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
        }
    }
}