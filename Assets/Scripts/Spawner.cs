using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform _platform;
    [SerializeField] private Cube _prefabCube;
    [SerializeField][Range(0f, 3f)] private float _delay = 1f;
    [SerializeField][Range(0f, 20f)] private int _startSizePool = 5;
    [SerializeField][Range(0f, 20f)] private int _maxSizePool = 10;

    private float _maximumOffsetX;
    private float _maximumOffsetZ;
    private ObjectPool<Cube> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Cube>(
            createFunc: () => CreateCube(),
            actionOnGet: (obj) => ActionOnGet(obj),
            actionOnRelease: (obj) => obj.gameObject.SetActive(false),
            actionOnDestroy: (obj) => Delete(obj),
            collectionCheck: true,
            defaultCapacity: _startSizePool,
            maxSize: _maxSizePool);
    }

    private void Start()
    {
        _maximumOffsetX = _platform.localScale.x / 2;
        _maximumOffsetZ = _platform.localScale.z / 2;

        InvokeRepeating(nameof(GetCube), 0f, _delay);
    }

    private void ActionOnGet(Cube cube)
    {
        cube.transform.position = GetRandomPosition();
        cube.transform.rotation = Quaternion.identity;
        cube.Rigidbody.velocity = Vector3.zero;
        cube.gameObject.SetActive(true);
    }

    private Cube CreateCube()
    {
        Cube cube = Instantiate(_prefabCube);
        cube.Deleted += Release;

        return cube;
    }

    private void GetCube() =>
        _pool.Get();

    private void Release(Cube cube) =>
        _pool.Release(cube);

    private void Delete(Cube cube)
    {
        cube.Deleted -= Release;
        Destroy(cube);
    }

    private Vector3 GetRandomPosition()
    {
        float positionOffsetX = Random.Range(-_maximumOffsetX, _maximumOffsetX);
        float positionOffsetZ = Random.Range(-_maximumOffsetZ, _maximumOffsetZ);

        float positionX = _platform.position.x + positionOffsetX;
        float positionZ = _platform.position.z + positionOffsetZ;

        return new Vector3(positionX, transform.position.y, positionZ);
    }

    private void OnDestroy() =>
        _pool.Dispose();
}