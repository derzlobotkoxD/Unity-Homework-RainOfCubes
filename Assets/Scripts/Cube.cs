using UnityEngine;
using UnityEngine.Events;

public class Cube : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;
    [SerializeField] private Rigidbody _rigidbody;

    private float _minimumDestroyDelay = 2f;
    private float _maximumDestroyDelay = 5f;
    private bool _isFirstHit = true;
    private Color _detonation—olor = Color.red;
    private Color _start—olor;

    public event UnityAction<Cube> Deleted;

    public Renderer Renderer => _renderer;
    public Rigidbody Rigidbody => _rigidbody;

    private void Awake()
    {
        _start—olor = Renderer.material.color;
    }

    private void OnEnable()
    {
        _isFirstHit = true;
        Renderer.material.color = _start—olor;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<Cube>() == null && _isFirstHit)
        {
            _isFirstHit = false;
            float destroyDelay = Random.Range(_minimumDestroyDelay, _maximumDestroyDelay);
            Renderer.material.color = _detonation—olor;

            Invoke(nameof(Delete), destroyDelay);
        }
    }

    private void Delete()
    {
        if (Deleted != null)
            Deleted.Invoke(this);
        else
            Destroy(gameObject);
    }
}