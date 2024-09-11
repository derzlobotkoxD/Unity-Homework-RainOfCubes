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

    private int _countSpawned = 0;

    private void OnEnable()
    {
        _spawner.InstanceCreated += AddSpawned;
        _spawner.PoolChanged += UpdateActiveStats;
    }

    private void Start()
    {
        UpdateSpawnedStats();
        UpdateCreatedStats();
        UpdateActiveStats();
    }

    private void AddSpawned()
    {
        _countSpawned++;
        UpdateSpawnedStats();
        UpdateCreatedStats();
    }

    private void UpdateSpawnedStats() =>
        _textCountSpawned.text = _descriptionCountSpawned + _countSpawned.ToString();

    private void UpdateCreatedStats() =>
        _textCountCreated.text = _descriptionCountCreated + _spawner.Pool.CountAll;

    private void UpdateActiveStats() =>
        _textCountActive.text = _descriptionCountActive + _spawner.Pool.CountActive;
}