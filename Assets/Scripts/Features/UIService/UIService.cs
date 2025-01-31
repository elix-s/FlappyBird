using Common.AssetsSystem;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class UIService 
{
    private readonly IAssetProvider _assetProvider;
    private IAssetUnloader _assetUnloader;
    private IObjectResolver _container;

    public UIService(IAssetProvider assetProvider, IAssetUnloader assetUnloader, IObjectResolver container)
    {
        _assetProvider = assetProvider;
        _assetUnloader = assetUnloader;
        _container = container;
    }

    public async UniTask ShowMainMenu()
    {
        var panel = await _assetProvider.GetAssetAsync<GameObject>("MainMenu");
        _assetUnloader.AddResource(panel);
        var prefab = _container.Instantiate(panel);
        _assetUnloader.AttachInstance(prefab);
    }

    public async UniTask HideUI()
    {
        _assetUnloader.Dispose();
    }
    
    public async UniTask ShowReloadLevelWindow()
    {
        var panel = await _assetProvider.GetAssetAsync<GameObject>("ReloadLevelCanvas");
        _assetUnloader.AddResource(panel);
        var prefab = _container.Instantiate(panel);
        _assetUnloader.AttachInstance(prefab);
    }
}
