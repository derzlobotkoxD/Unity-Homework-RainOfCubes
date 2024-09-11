using UnityEngine;
using UnityEngine.Events;
using static UnityEditor.Progress;

public abstract class Item : MonoBehaviour
{
    protected float MinimumDestroyDelay = 2f;
    protected float MaximumDestroyDelay = 5f;

    [SerializeField] private Renderer _renderer;
    [SerializeField] private Rigidbody _rigidbody;

    private Color _startColor;

    public Renderer Renderer => _renderer;
    public Rigidbody Rigidbody => _rigidbody;

    private void Awake() =>
        _startColor = Renderer.material.color;

    private void OnDisable() =>
        Renderer.material.color = _startColor;
}
