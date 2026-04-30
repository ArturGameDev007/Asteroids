using System;
using System.Collections.Generic;
using _Project.Scripts.Services.Save;
using Cysharp.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;
using SaveData = _Project.Scripts.Services.Save.SaveData;


public class CloudSaveService : ISaveService
{
    private const string DATA_KEY = "PlayerSave";
    
    public event Action OnSaved;

    public async UniTask Save(SaveData saveData)
    {
        if (UnityServices.State != ServicesInitializationState.Initialized)
            return;
            
        if (!AuthenticationService.Instance.IsSignedIn)
            return;

        var playerData = new Dictionary<string, object> { { DATA_KEY, saveData } };
        var save = Unity.Services.CloudSave.CloudSaveService.Instance.Data.Player.SaveAsync(playerData);

        await save;
        
        OnSaved?.Invoke();

        Debug.Log($"Saved data {string.Join(',', playerData)}");
    }

    public async UniTask<SaveData> Load()
    {
        if (UnityServices.State != ServicesInitializationState.Initialized)
        {
            Debug.LogWarning("Облачнве сервисы не иницилизированные - офлайн.");
            return null;
        }

        if (!AuthenticationService.Instance.IsSignedIn)
            return null;

        try
        {
            var playerData = await Unity.Services.CloudSave.CloudSaveService.Instance.Data.Player.LoadAsync(
                new HashSet<string> { DATA_KEY });

            if (playerData.TryGetValue(DATA_KEY, out var item))
                return item.Value.GetAs<SaveData>();
        }
        catch (Exception e)
        {
            Debug.Log($"Не удалось загрузить, нет сети - {e.Message}");
        }

        return null;
    }
}