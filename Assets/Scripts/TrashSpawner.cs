using UnityEngine;

public class TrashSpawner : MonoBehaviour
{
    [SerializeField] private UnityElementPool [] _trashes;

    [SerializeField] private float _spawnDistance = 12f;
    [SerializeField] private float _spawnRate = 1f;
    [SerializeField] private int _amountPerSpawn = 1;
    [Range(0f, 45f)]
    [SerializeField] private float _trajectoryVariance = 15f;

    private void Start()
    {
        InvokeRepeating(nameof(Spawn), _spawnRate, _spawnRate);
    }

    public void Spawn()
    {
        for (int i = 0; i < _amountPerSpawn; i++)
        {
            // Choose a random direction from the center of the spawner and
            // spawn the trash a distance away
            Vector3 spawnDirection = Random.insideUnitCircle.normalized;
            Vector3 spawnPoint = transform.position + (spawnDirection * _spawnDistance);

            // Calculate a random variance in the trash's rotation which will
            // cause its trajectory to change
            float variance = Random.Range(-_trajectoryVariance, _trajectoryVariance);
            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);

            // Create the new trash by asking the random pool and set a random
            // size within the range
            int index = Random.Range(0, _trashes.Length);
            GameObject go = _trashes[index].Get(spawnPoint);
            Trash t = go.GetComponent<Trash>();

            t.Init(rotation, _trashes[index]);

            // Set the trajectory to move in the direction of the spawner
            Vector2 trajectory = rotation * -spawnDirection;
            t.SetTrajectory(trajectory);
        }
    }
}
