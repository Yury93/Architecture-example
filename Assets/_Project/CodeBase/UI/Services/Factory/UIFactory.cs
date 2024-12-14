
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Services;
using CodeBase.Services.PersistantProgress;
using CodeBase.UI.Services.Windows;
using CodeBase.UI.Windows;
using UnityEngine;

namespace CodeBase.UI.Services.Factory
{
    public class UIFactory : IUIFactory
    {
        private const string UI_ROOT_PATH = "UI/UIRoot";
        private readonly IAsset _asset;
        private readonly IStaticDataService _staticData;
        private readonly IPersistentProgressService _persistentProgressService;
        private Transform _uiRoot;

        public UIFactory(IAsset asset, IStaticDataService staticData, IPersistentProgressService persistentProgressService)
        {
            _asset = asset;
            _staticData = staticData;
            _persistentProgressService = persistentProgressService;
        }
        public void CreateShop()
        {
            var config = _staticData.ForWindow(WindowId.Shop); 
           WindowBase windowBase = Object.Instantiate(config.Prefab, _uiRoot);
            windowBase.Construct(persistentProgressService: _persistentProgressService);
        }
        public void CreateUIRoot()
        {
          _uiRoot = _asset.Instatiate(UI_ROOT_PATH).transform;
        }
    }
}