using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Cube : Item
{
    [SerializeField] private Color _detonationColor = Color.red;

    private bool _isFirstHit = true;

    public event UnityAction<Cube> Deleted;

    private void OnEnable() =>
        _isFirstHit = true;

    private void OnCollisionEnter(Collision collision)
    {
        if (_isFirstHit && collision.collider.TryGetComponent(out Platform platform))
        {
            _isFirstHit = false;
            Renderer.material.color = _detonationColor;
            float destroyDelay = GetRandomDelay();

            StartCoroutine(DeleteWithDelay(destroyDelay));
        }
    }

    private IEnumerator DeleteWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        Deleted?.Invoke(this);
    }
}