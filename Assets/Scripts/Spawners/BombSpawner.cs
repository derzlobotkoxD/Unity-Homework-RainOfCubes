using UnityEngine;

public class BombSpawner : Spawner<Bomb>
{
    [SerializeField] private Exploder _exploder;

    protected override void ActivateInstance(Bomb bomb)
    {
        bomb.Deleted += ReleaseInstance;
        base.ActivateInstance(bomb);
    }

    protected override void ReleaseInstance(Bomb bomb)
    {
        _exploder.Explode(bomb.transform.position);
        bomb.Deleted -= ReleaseInstance;
        base.ReleaseInstance(bomb);
    }
}