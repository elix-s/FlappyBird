using System;
using System.Reflection.Emit;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Random = UnityEngine.Random;

public class WallSpawner : MonoBehaviour
{
    public GameObject wallPrefab;
    public int spawnInterval = 3000;
    public float minY = -1f;
    public float maxY = 2f;

    private GameSessionService _gameSessionService;
    private IObjectResolver _container;
    private CancellationTokenSource _cancellationToken;

    [Inject]
    private void Construct(GameSessionService gameSessionService,  IObjectResolver container)
    {
        _gameSessionService = gameSessionService;
        _container = container;
        _cancellationToken = new CancellationTokenSource();
    }

    private async void Start()
    {
        await UniTask.WaitUntil(()=>_gameSessionService.GameRunning);
        await SpawnWall(_cancellationToken);
    }

    private async UniTask SpawnWall(CancellationTokenSource token)
    {
        while (!token.IsCancellationRequested)
        {
            float randomY = Random.Range(minY, maxY);
            Vector3 spawnPosition = new Vector3(10.0f, randomY, 0);
            _container.Instantiate(wallPrefab, spawnPosition, Quaternion.identity);
            await UniTask.Delay(spawnInterval);
        }
    }

    private void OnDestroy()
    {
        _cancellationToken.Cancel();
    }
}