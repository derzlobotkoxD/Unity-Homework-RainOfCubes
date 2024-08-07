using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody), typeof(Renderer))]
public class Cube : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;
    [SerializeField] private Rigidbody _rigidbody;

    private float _minimumDestroyDelay = 2f;
    private float _maximumDestroyDelay = 5f;
    private bool _isFirstHit = true;
    private Color _detonationColor = Color.red;
    private Color _startColor;

    public event UnityAction<Cube> Deleted;

    public Renderer Renderer => _renderer;
    public Rigidbody Rigidbody => _rigidbody;

    private void Awake()
    {
        _startColor = Renderer.material.color;
    }

    private void OnEnable()
    {
        _isFirstHit = true;
        Renderer.material.color = _startColor;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent<Platform>(out Platform platform) && _isFirstHit)
        {
            _isFirstHit = false;
            float destroyDelay = Random.Range(_minimumDestroyDelay, _maximumDestroyDelay);
            Renderer.material.color = _detonationColor;

            StartCoroutine(Delete(destroyDelay));
        }
    }

    private IEnumerator Delete(float delay)
    {
        yield return new WaitForSeconds(delay);

        Deleted?.Invoke(this);
    }
}