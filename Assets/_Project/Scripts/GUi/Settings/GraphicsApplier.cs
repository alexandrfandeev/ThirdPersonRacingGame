using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GraphicsApplier : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown _dropdownList;

    public void Initialize()
    {
        int graphicsValue = GraphicsSaveSystem.GetGraphicsLevel();
        QualitySettings.SetQualityLevel(graphicsValue);
        _dropdownList.value = graphicsValue;
    }

    public void ChangeGraphics(int graphicsValue)
    {
        QualitySettings.SetQualityLevel(graphicsValue);
        GraphicsSaveSystem.SetGraphics(graphicsValue);
    }
}
