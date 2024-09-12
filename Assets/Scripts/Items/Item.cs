using UnityEngine;

public abstract class Item : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;
    [SerializeField] private Rigidbody _rigidbody;

    private float _minimumDestroyDelay = 2f;
    private float _maximumDestroyDelay = 5f;

    private Color _startColor;

    public Renderer Renderer => _renderer;
    public Rigidbody Rigidbody => _rigidbody;

    private void Awake() =>
        _startColor = Renderer.material.color;

    private void OnDisable() =>
        Renderer.material.color = _startColor;

    protected float GetRandomDelay() =>
        Random.Range(_minimumDestroyDelay, _maximumDestroyDelay);
}
