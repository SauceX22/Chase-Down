using UnityEngine;
using System.Collections.Generic;
[System.Serializable]
public class Wave
{
    /// <summary>
    /// Event invoked when the wave has been cleared
    /// </summary>
    public event System.Action<Wave> OnCleared;
    /// <summary>
    /// Event invoked when an enemy of the wave has been spawned
    /// </summary>
    public event System.Action<Wave, Transform> OnEnemySpawned;
    /// <summary>
    /// Event invoked when an enemy of the wave has been killed
    /// </summary>
    public event System.Action<Wave, Transform> OnEnemyKilled;
    [SerializeField]
    public Flock agentFlock;
    [SerializeField]
    private string name;
    [SerializeField]
    public Transform enemyPrefab;
    [SerializeField, Range(1, 100)]
    public int count;
    [SerializeField, Range(0.01f, 1f)]
    public float rate;
    private List<OnDestroyEventSender> enemies;
    public bool IsCleared
    {
        get { return enemies != null && enemies.Count == 0; }
    }
    public string Name
    {
        get { return "Wave "; }
    }
    public int Count
    {
        get { return count; }
    }
    public float Rate
    {
        get { return rate; }
    }
    public int RemainingEnemiesToInstantiate
    {
        get; private set;
    }
    public int RemainingEnemiesAlive
    {
        get { return enemies.Count; }
    }
    /// <summary>
    /// Resets the wave
    /// </summary>
    public void Reset()
    {
        RemainingEnemiesToInstantiate = count;
    }
    /// <summary>
    /// Instantiate an enemy at the given position and rotation
    /// </summary>
    public void InstantiateEnemy(Vector3 position, Quaternion rotation)
    {
        if (RemainingEnemiesToInstantiate == 0)
            throw new System.InvalidOperationException("No remaining enemy to instantiate. Have you called `Reset`?");

        if (enemies == null) enemies = new List<OnDestroyEventSender>(count);

        Transform enemy = Object.Instantiate(enemyPrefab, position, rotation);

        FlockAgent newAgent = enemy.GetComponent<FlockAgent>();

        newAgent.Initialize(agentFlock);

        agentFlock.agents.Add(newAgent);

        OnDestroyEventSender onDestroyEventDispatcher = enemy.gameObject.AddComponent<OnDestroyEventSender>();

        onDestroyEventDispatcher.OnDestroyed += OnEnemyDestroyed;
        enemies.Add(onDestroyEventDispatcher);
        RemainingEnemiesToInstantiate--;
        if (OnEnemySpawned != null)
            OnEnemySpawned(this, enemy);
    }
    /// <summary>
    /// Called when an enemy has been destroyed
    /// </summary>
    private void OnEnemyDestroyed(OnDestroyEventSender destroyedEnemy)
    {
        destroyedEnemy.OnDestroyed -= OnEnemyDestroyed;
        enemies.Remove(destroyedEnemy);
        if (OnEnemyKilled != null)
            OnEnemyKilled(this, destroyedEnemy.transform);
        if (RemainingEnemiesToInstantiate == 0 && enemies.Count == 0 && OnCleared != null)
            OnCleared(this);
    }
}