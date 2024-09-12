using TMPro;
using UnityEngine;

public abstract class SpawnStatster<T> : MonoBehaviour where T : Item
{
    [SerializeField] private Spawner<T> _spawner;
    [SerializeField] private TextMeshProUGUI _textCountSpawned;
    [SerializeField] private TextMeshProUGUI _textCountCreated;
    [SerializeField] private TextMeshProUGUI _textCountActive;

    private string _descriptionCountSpawned = "1) количество заспавненных: ";
    private string _descriptionCountCreated = "2) количество созданных: ";
    private string _descriptionCountActive = "3) количество активных: ";

    private void OnEnable()
    {
        _spawner.InstanceCreated += UpdateAtSpawn;
        _spawner.PoolChanged += UpdateActiveStats;
    }

    private void OnDisable()
    {
        _spawner.InstanceCreated -= UpdateAtSpawn;
        _spawner.PoolChanged -= UpdateActiveStats;
    }

    private void Start()
    {
        UpdateSpawnedStats();
        UpdateCreatedStats();
        UpdateActiveStats();
    }

    private void UpdateAtSpawn()
    {
        UpdateSpawnedStats();
        UpdateCreatedStats();
    }

    private void UpdateSpawnedStats() =>
        _textCountSpawned.text = _descriptionCountSpawned + _spawner.CountSpawned;

    private void UpdateCreatedStats() =>
        _textCountCreated.text = _descriptionCountCreated + _spawner.CountCreated;

    private void UpdateActiveStats() =>
        _textCountActive.text = _descriptionCountActive + _spawner.CountActive;
}