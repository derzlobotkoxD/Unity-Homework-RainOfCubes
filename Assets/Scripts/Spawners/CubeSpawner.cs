using UnityEngine;

public class CubeSpawner : Spawner<Cube>
{
    [SerializeField] private BombSpawner _bombSpawner;

    protected override void ActivateInstance(Cube cube)
    {
        cube.Deleted += ReleaseInstance;
        base.ActivateInstance(cube);
    }

    protected override void ReleaseInstance(Cube cube)
    {
        _bombSpawner.GetInstance(cube.transform.position);
        cube.Deleted -= ReleaseInstance;
        base.ReleaseInstance(cube);
    }
}