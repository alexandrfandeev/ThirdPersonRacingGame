using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.VehicleController.Entities
{
    public class VehicleUtilities : MonoBehaviour
    {
        [SerializeField] private List<MeshRenderer> _renderers = new List<MeshRenderer>();

        private MaterialPropertyBlock _propertyBlock;
        private static readonly int Color1 = Shader.PropertyToID("_Color");
        
        public void Initialize()
        {
            _propertyBlock = new MaterialPropertyBlock();
            
        }

        public void ChangeColor(Color changeableColor)
        {
            StartCoroutine(Set(changeableColor));
        }

        private IEnumerator Set(Color color)
        {
            for (int i = 0; i < _renderers.Count; i++)
            {
                _renderers[i].GetPropertyBlock(_propertyBlock);
                _propertyBlock.SetColor(Color1, color);
                _renderers[i].SetPropertyBlock(_propertyBlock);
                yield return null;
            }
        }
    }
}
