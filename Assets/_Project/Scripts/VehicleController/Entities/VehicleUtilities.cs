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
            _renderers.ForEach(x =>
            {
                x.GetPropertyBlock(_propertyBlock);
                _propertyBlock.SetColor(Color1, changeableColor);
                x.SetPropertyBlock(_propertyBlock);
            });
        }
    }
}
