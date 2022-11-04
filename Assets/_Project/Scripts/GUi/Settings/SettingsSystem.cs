using UnityEngine;
public class SettingsSystem : MonoBehaviour
{
    [SerializeField] private SettingsPanel _panel;

    private void Awake()
    {
        _panel.Initialize();
    }
}
