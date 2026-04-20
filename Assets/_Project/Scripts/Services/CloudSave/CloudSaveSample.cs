using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using _Project.Scripts.Services.CloudSave;
using Unity.Services.CloudSave;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;
using SaveData = _Project.Scripts.Services.Save.SaveData;


public class CloudSaveSample : ICloudSaveSample
{
    private const string DATA_KEY = "PlayerSave";

    public async Task Save(SaveData saveData)
    {
        if (UnityServices.State != ServicesInitializationState.Initialized)
            return;
            
        if (!AuthenticationService.Instance.IsSignedIn)
            return;

        var playerData = new Dictionary<string, object> { { DATA_KEY, saveData } };
        var save = CloudSaveService.Instance.Data.Player.SaveAsync(playerData);

        await save;

        Debug.Log($"Saved data {string.Join(',', playerData)}");
    }

    public async Task<SaveData> Load()
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
            var playerData = await CloudSaveService.Instance.Data.Player.LoadAsync(
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