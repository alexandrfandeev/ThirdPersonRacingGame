using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Core.SignalBus;
using _Project.Scripts.GUi.ColorChanger;
using _Project.Scripts.SaveSystem;
using UnityEngine;

namespace _Project.Scripts.VehicleController.Upgrades.Entities
{
    public class VehicleColor : UpgradeEntity
    {
        [SerializeField] private Material _changeableMaterial;
        [SerializeField] private List<Color> _upgradableColors;
        public override UpgradeType Type => UpgradeType.Color;
        public override int Level => UpgradesSaveSystem.GetUpgradeLevel(Type);


        private void Awake()
        {
            _changeableMaterial.color = _upgradableColors.First();
        }

        public override void Initialize()
        {
            _changeableMaterial.color = _upgradableColors[Level - 1];
            Signal.Current.Fire<StartChangeColor>(new StartChangeColor{CurrentID = Level, OnChangeColor = OnUpgradeWithSelection});
        }

        private void OnUpgradeWithSelection(int value)
        {
            UpgradesSaveSystem.SetUpgradeLevel(Type, value);
            OnUpgrade();
        }

        public override void OnUpgrade()
        {
            _changeableMaterial.color = _upgradableColors[Level - 1];
        }
    }
}
