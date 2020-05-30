using Unity.Entities;
using UnityEngine;
using Unity.Transforms;
using Unity.Rendering;
using Unity.Mathematics;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Material _material;
    [SerializeField] private GameObject _prefab;

    [Header("Grid settings")]
    [SerializeField] private float _xSize = 10;
    [SerializeField] private float _ySize = 10;
    [SerializeField] private float _spacing = 0.5f;

    private Entity _entityPrefab;
    private World _defaultWorld;
    private EntityManager _entityManager;
    
    void Start()
    {
        _defaultWorld = World.DefaultGameObjectInjectionWorld;
        _entityManager = _defaultWorld.EntityManager;
        
        GameObjectConversionSettings settings = GameObjectConversionSettings.FromWorld(_defaultWorld, null);
        _entityPrefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(_prefab, settings);

        InstantiateEntityGrid(_xSize,_ySize,_spacing);
    }

    private void InstantiateEntity(float3 position)
    {
        Entity newEntity = _entityManager.Instantiate(_entityPrefab);
        _entityManager.SetComponentData(newEntity, new Translation
        {
            Value = position
        });
    }

    private void InstantiateEntityGrid(float dimX, float dimY, float spacing = 1f)
    {
        for (int i = 0; i < dimX; i++)
        {
            for (int j = 0; j < dimY; j++)
            {
                InstantiateEntity(new float3(i * spacing, j * spacing, 0));
            }
        }
    }
}
