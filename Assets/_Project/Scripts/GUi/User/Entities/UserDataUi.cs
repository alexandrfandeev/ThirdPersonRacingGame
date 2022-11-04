using _Project.Scripts.GUi.Utilities;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.GUi.User.Entities
{
    public class UserDataUi : View
    {
        [SerializeField] private TextMeshProUGUI _coinsAmountText;
        [SerializeField] private TextMeshProUGUI _usernameText;

        public void Initialize(int amount, string username)
        {
            Enable();
            _coinsAmountText.text = amount.ToString();
            _usernameText.text = username;
        }

        public void UpdateCoins(int amount)
        {
            _coinsAmountText.text = amount.ToString();
        }
    }
}
