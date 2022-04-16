using UnityEngine;

public class Ship : MonoBehaviour, IPooledObject
{
    [Header("Explosion")]
    [SerializeField] private GameObject prefabExplosion;

    [Space]
    [Space]
    [Header("Random Speed")]
    [SerializeField] private float maxSpeed = 20;
    [SerializeField] private float minSpeed = 1;
    public float Speed { get; private set; }

    public void OnDisabledFromPool()
    {
        PlayExplosion();
    }

    public void OnEnabledFromPool()
    {
        RandomizeShips();
    }

    public void OnInstantiated()
    {
        Speed = Random.Range(minSpeed, maxSpeed);
        prefabExplosion.SetActive(false);
    }

    private void PlayExplosion()
    {
        prefabExplosion.SetActive(true);
        var explosion = Instantiate(prefabExplosion, transform.position, Quaternion.identity);
        Destroy(explosion, 1f);
    }

    private void RandomizeShips()
    {
        Vector3 randomPositiom = new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), Random.Range(-5f, 5f));
        transform.position = randomPositiom;
    }
}
