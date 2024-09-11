using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;

public abstract class Spawner<T> : MonoBehaviour where T : Item
{
    [SerializeField] protected T Prefab;

    private int _defaultCapacitPool = 5;
    private int _maxSizePool = 5;
    private Vector3 _spawnPosition;

    public event UnityAction InstanceCreated;
    public event UnityAction PoolChanged;

    public ObjectPool<T> Pool { get; private set; }

    private void Awake()
    {
        Pool = new ObjectPool<T>(
            createFunc: () => Instantiate(Prefab),
            actionOnGet: (obj) => ActivateInstance(obj),
            actionOnRelease: (obj) => obj.gameObject.SetActive(false),
            actionOnDestroy: (obj) => Destroy(obj),
            collectionCheck: true,
            defaultCapacity: _defaultCapacitPool,
            maxSize: _maxSizePool);
    }

    public void GetInstance(Vector3 position)
    {
        _spawnPosition = position;
        Pool.Get();
    }

    protected virtual void ActivateInstance(T item)
    {
        item.Rigidbody.angularVelocity = Vector3.zero;
        item.Rigidbody.velocity = Vector3.zero;
        item.transform.position = _spawnPosition;
        item.transform.rotation = Quaternion.identity;
        item.gameObject.SetActive(true);

        InstanceCreated?.Invoke();
        PoolChanged?.Invoke();
    }

    protected virtual void ReleaseInstance(T item)
    {
        Pool.Release(item);
        PoolChanged?.Invoke();
    }

    private void OnDestroy() =>
        Pool.Dispose();
}