using System.Collections;
using UnityEngine;

public class CubeSpawnPlatform : MonoBehaviour
{
    [SerializeField] private Spawner<Cube> _spawner;
    [SerializeField][Range(0f, 3f)] private float _delay = 1f;

    private float _maximumOffsetX;
    private float _maximumOffsetZ;
    private float _divider = 2f;

    private void Start()
    {
        _maximumOffsetX = transform.localScale.x / _divider;
        _maximumOffsetZ = transform.localScale.z / _divider;

        StartCoroutine(SpawnWithDelay(_delay));
    }

    private Vector3 GetRandomPosition()
    {
        float positionOffsetX = Random.Range(-_maximumOffsetX, _maximumOffsetX);
        float positionOffsetZ = Random.Range(-_maximumOffsetZ, _maximumOffsetZ);

        float positionX = transform.position.x + positionOffsetX;
        float positionZ = transform.position.z + positionOffsetZ;

        return new Vector3(positionX, transform.position.y, positionZ);
    }

    private IEnumerator SpawnWithDelay(float delay)
    {
        WaitForSeconds wait = new WaitForSeconds(delay);

        while (enabled)
        {
            _spawner.GetInstance(GetRandomPosition());
            yield return wait;
        }
    }
}