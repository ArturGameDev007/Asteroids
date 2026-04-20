using System;
using Cysharp.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;

namespace _Project.Scripts.Infrastructure
{
    public class CloudInitializer : ICloudInitialize
    {
        public async UniTask InitializeCloud()
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                Debug.Log("Интернет отсутсвует, игра сохраняет прогресс в офлайн-режиме.");
                return;
            }

            try
            {
                if (UnityServices.State == ServicesInitializationState.Uninitialized)
                    await UnityServices.InitializeAsync().AsUniTask();

                if (!AuthenticationService.Instance.IsSignedIn)
                {
                    await AuthenticationService.Instance.SignInAnonymouslyAsync().AsUniTask();
                    Debug.Log($"Успешный вход. ID - {AuthenticationService.Instance.PlayerId}");
                }
                else
                {
                    Debug.LogWarning("Игрок уже авторизован, повторный вход не требуется.");
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Ошибка авторизации - {e.Message}");
            }
        }
    }
}