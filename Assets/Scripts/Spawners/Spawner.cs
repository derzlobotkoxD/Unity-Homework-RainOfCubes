using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;

public abstract class Spawner<T> : MonoBehaviour where T : Item
{
    [SerializeField] private T _prefab;

    private int _defaultCapacitPool = 5;
    private int _maxSizePool = 5;
    private Vector3 _spawnPosition;
    private ObjectPool<T> _pool;

    public event UnityAction InstanceCreated;
    public event UnityAction PoolChanged;

    public float CountActive => _pool.CountActive;
    public float CountCreated => _pool.CountAll;
    public float CountSpawned { get; private set; } = 0;

    private void Awake()
    {
        _pool = new ObjectPool<T>(
            createFunc: () => Instantiate(_prefab),
            actionOnGet: (obj) => ActivateInstance(obj),
            actionOnRelease: (obj) => obj.gameObject.SetActive(false),
            actionOnDestroy: (obj) => Destroy(obj.gameObject),
            collectionCheck: true,
            defaultCapacity: _defaultCapacitPool,
            maxSize: _maxSizePool);
    }

    public void GetInstance(Vector3 position)
    {
        _spawnPosition = position;
        _pool.Get();
    }

    protected virtual void ActivateInstance(T item)
    {
        item.Rigidbody.angularVelocity = Vector3.zero;
        item.Rigidbody.velocity = Vector3.zero;
        item.transform.position = _spawnPosition;
        item.transform.rotation = Quaternion.identity;
        item.gameObject.SetActive(true);

        CountSpawned++;
        InstanceCreated?.Invoke();
        PoolChanged?.Invoke();
    }

    protected virtual void ReleaseInstance(T item)
    {
        _pool.Release(item);
        PoolChanged?.Invoke();
    }
}