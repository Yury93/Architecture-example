
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Services;
using CodeBase.UI.Services.Windows;
using UnityEngine;

namespace CodeBase.UI.Services.Factory
{
    public class UIFactory : IUIFactory
    {
        private const string UIRootPath = "UI/UIRoot";
        private readonly IAsset _asset;
        private readonly IStaticDataService _staticData;
        private Transform _uiRoot;

        public UIFactory(IAsset asset, IStaticDataService staticData)
        {
            _asset = asset;
            _staticData = staticData;
        }
        public void CreateShop()
        {
            var config = _staticData.ForWindow(WindowId.Shop);

            Object.Instantiate(config.Prefab, _uiRoot);
        }
        public void CreateUIRoot()
        {
          _uiRoot = _asset.Instatiate(UIRootPath).transform;
        }
    }
}