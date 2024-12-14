using TMPro;

namespace CodeBase.UI.Windows
{
    public class ShopWindow : WindowBase
    {
        public TextMeshProUGUI moneyText;
        protected override void Initialize()
        {
            base.Initialize();
            RefreshMoneyText();
        } 
        private void RefreshMoneyText()
        {
            moneyText.text = PlayerProgress.WorldData.LootData.Collected + "";
        } 
        protected override void CleanUp()
        {
            base.CleanUp();
            PlayerProgress.WorldData.LootData.Changed -= RefreshMoneyText;
        }
        protected override void SubscribeUpdate()
        {
            base.SubscribeUpdate();
            PlayerProgress.WorldData.LootData.Changed += RefreshMoneyText;
        }
       
    }
}