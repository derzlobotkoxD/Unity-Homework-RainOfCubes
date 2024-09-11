using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private float _radius = 6f;
    [SerializeField] private float _power = 2000f;
    [SerializeField] private LayerMask _layerMask;

    public void Explode(Vector3 position)
    {
        RaycastHit[] hits = Physics.SphereCastAll(position, _radius, Vector3.one, 0f, _layerMask);

        foreach (RaycastHit hit in hits)
            hit.rigidbody.AddExplosionForce(_power, position, _radius);
    }
}