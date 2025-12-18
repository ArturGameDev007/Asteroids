using Assets.Scripts.EnemySpace;
using System;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(HandlerCrashWithEnemy))]
public class Player : MonoBehaviour
{
    private PlayerController _player;
    private HandlerCrashWithEnemy _collisionHandler;

    public event Action GameOver;

    private void Awake()
    {
        _player = GetComponent<PlayerController>();
        _collisionHandler = GetComponent<HandlerCrashWithEnemy>();
    }

    private void OnEnable()
    {
        _collisionHandler.CollisionHandler += ProcessCollision;
    }

    private void OnDisable()
    {

        _collisionHandler.CollisionHandler -= ProcessCollision;
    }


    public void Reset()
    {
        _player.Reset();
    }

    private void ProcessCollision(IEnemy enemy)
    {
        if (enemy is Enemy)
        {
            GameOver?.Invoke();
        }
    }
}
