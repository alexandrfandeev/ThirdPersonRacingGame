using System;
using _Project.Scripts.Core.LocatorServices;
using _Project.Scripts.Core.SignalBus;
using _Project.Scripts.VehicleController.Upgrades;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthPointsHandler : MonoBehaviour
{
   [SerializeField] private Button _upgradeButton;
   [SerializeField] private TextMeshProUGUI _buttonInscription;
   [SerializeField] private TextMeshProUGUI _currentHP;
   [SerializeField] private TextMeshProUGUI _currentPrice;
   [SerializeField] private TextMeshProUGUI _priceDescription;

   private IUpgradeSystem _upgradeSystem;
   private Action _onBuy;

   [Sub]
   public void Initialize(HealthPointsUpgradable reference)
   {
      _onBuy = reference.OnUpgrade;
      _upgradeSystem = ServiceLocator.Current.Get<IUpgradeSystem>();
      _currentPrice.text = _upgradeSystem.UpgradePrice(UpgradeType.HealthPoints).ToString();
      _currentHP.text = _upgradeSystem.CurrentValue(UpgradeType.HealthPoints).ToString();
      CheckMoney();
      CheckLevel();
   }

   public void CheckMoney()
   {
      _upgradeButton.interactable = _upgradeSystem.HaveMoney(UpgradeType.HealthPoints);
      if (_upgradeSystem.IsMaxLevel(UpgradeType.HealthPoints)) _upgradeButton.interactable = false;
   }

   private bool CheckLevel()
   {
      if (_upgradeSystem.IsMaxLevel(UpgradeType.HealthPoints))
      {
         _priceDescription.text = string.Empty;
         _currentPrice.text = string.Empty;
         _upgradeButton.interactable = false;
         _buttonInscription.text = "MAX";
         return true;
      }

      return false;
   }

   public void Upgrade()
   {
      if (CheckLevel()) return;
      _onBuy?.Invoke();
      _currentPrice.text = _upgradeSystem.UpgradePrice(UpgradeType.HealthPoints).ToString();
      _currentHP.text = _upgradeSystem.CurrentValue(UpgradeType.HealthPoints).ToString();
      CheckMoney();
      CheckLevel();
   }
}

public struct HealthPointsUpgradable
{
   public Action OnUpgrade;
}
