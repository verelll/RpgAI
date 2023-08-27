using Sirenix.OdinInspector;
using Test.Architecture;
using UnityEngine;

namespace Test.Effects
{
    public class BottleEffects : SerializedMonoBehaviour
    {
        private const string LIQUID_COLOR_NAME = "_LiquidColor";
        private const string SURFACE_COLOR_NAME = "_SurfaceColor";
        private const string FRESNEL_COLOR_NAME = "_FresnelColor";
        
        private const string WOBBLE_X_NAME = "_WobbleX";
        private const string WOBBLE_Z_NAME = "_WobbleZ";
        
        [SerializeField, BoxGroup("Bottle Effects")]
        private MeshRenderer bottleMeshRenderer;
        
        [SerializeField, BoxGroup("Liquid Effects")]
        private MeshRenderer liquidMeshRenderer;
        
        [SerializeField, BoxGroup("Liquid Effects")]
        private float MaxWobble = 0.03f;
        
        [SerializeField, BoxGroup("Liquid Effects")]
        private float WobbleSpeed = 1f;
        
        [SerializeField, BoxGroup("Liquid Effects")]
        private float Recovery = 1f;
        
        private MaterialPropertyBlock _bottlePropertyBlock;
        private MaterialPropertyBlock _liquidPropertyBlock;

        private Vector3 _lastPos;
        private Vector3 _velocity;
        private Vector3 _lastRot;
        private Vector3 _angularVelocity;

        private float _wobbleAmountX;
        private float _wobbleAmountZ;
        private float _wobbleAmountToAddX;
        private float _wobbleAmountToAddZ;
        private float _pulse;
        private float _time = 0.5f;
        
        private Color _liquidColor;
        private Color _surfaceColor;
        private Color _fresnelColor;

        private float _deltaTime => Time.deltaTime;
        
        public void Init(Color liquidColor, Color surfaceColor, Color fresnelColor)
        {
            _liquidPropertyBlock = new MaterialPropertyBlock();
            _bottlePropertyBlock = new MaterialPropertyBlock();
            
            GetPropertyBlock(liquidMeshRenderer, _liquidPropertyBlock);
            _liquidPropertyBlock.SetColor(LIQUID_COLOR_NAME, liquidColor);
            _liquidPropertyBlock.SetColor(SURFACE_COLOR_NAME, surfaceColor);
            _liquidPropertyBlock.SetColor(FRESNEL_COLOR_NAME, fresnelColor);
            SetPropertyBlock(liquidMeshRenderer, _liquidPropertyBlock);
            
            UnityEventProvider.OnUpdate += HandleUpdate;
        }

        private void OnDestroy()
        {
            UnityEventProvider.OnUpdate -= HandleUpdate;
        }

        private void HandleUpdate()
        {
            //LiquidWobbleUpdate();
        }

        private void LiquidWobbleUpdate()
        {
            GetPropertyBlock(liquidMeshRenderer, _liquidPropertyBlock);
            
            _time += _deltaTime;
            _wobbleAmountToAddX = Mathf.Lerp(_wobbleAmountToAddX, 0, _deltaTime * (Recovery));
            _wobbleAmountToAddZ = Mathf.Lerp(_wobbleAmountToAddZ, 0, _deltaTime * (Recovery));

            _pulse = 2 * Mathf.PI * WobbleSpeed;
            _wobbleAmountX = _wobbleAmountToAddX * Mathf.Sin(_pulse * _time);
            _wobbleAmountZ = _wobbleAmountToAddZ * Mathf.Sin(_pulse * _time);
            
            _velocity = (_lastPos - transform.position) / _deltaTime;
            _angularVelocity = transform.rotation.eulerAngles - _lastRot;
            
            _wobbleAmountToAddX +=
                Mathf.Clamp((_velocity.x + (_angularVelocity.z * 0.2f)) * MaxWobble, -MaxWobble, MaxWobble);
            _wobbleAmountToAddZ +=
                Mathf.Clamp((_velocity.z + (_angularVelocity.x * 0.2f)) * MaxWobble, -MaxWobble, MaxWobble);

            _lastPos = transform.position;
            _lastRot = transform.rotation.eulerAngles;
            
            _liquidPropertyBlock.SetFloat(WOBBLE_X_NAME, _wobbleAmountX);
            _liquidPropertyBlock.SetFloat(WOBBLE_Z_NAME, _wobbleAmountZ);
            SetPropertyBlock(liquidMeshRenderer, _liquidPropertyBlock);
        }

        private void SetPropertyBlock(MeshRenderer renderer, MaterialPropertyBlock propertyBlock)
        {
            renderer?.SetPropertyBlock(propertyBlock);
        }
        
        private void GetPropertyBlock(MeshRenderer renderer, MaterialPropertyBlock propertyBlock)
        {
            renderer?.GetPropertyBlock(propertyBlock);
        }
    }
}