using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EndlessGenerator : MonoBehaviour
{
    [SerializeField] private Pool<Ship> shipPool;
    [SerializeField] [Range(1, 20)] private int sizeOfFibonacci = 4;

    public List<Ship> ShipsList { get; private set; } = new List<Ship>();
    public int CountOfShips { get; private set; } = 0;
    public int RemaningShips { get; private set; } 

    private void Awake()
    {
        shipPool.Initialize();
    }

    private void Start()
    {
        StartCoroutine(SpawShipFibonacci());
        StartCoroutine(DestroyAfterOneSecond());
    }

    private void Update()
    {
        RemaningShips = ShipsList.Count;
    }

    // Fibonacci sem utilizar Laços
    private int Fibonacci(int n)
    {
        if (n == 1)
        {
            return 0;
        }
        if (n == 2)
        {
            return 1;
        }
        return Fibonacci(n - 1) + Fibonacci(n - 2);
    }

    private IEnumerator SpawShipFibonacci()
    {
        int count = 1;
        while (true)
        {
            int fib = Fibonacci(count);
            SpawShip(fib);
            yield return new WaitForSeconds(2);
            count++;
            if (count > sizeOfFibonacci)
            {
                break;
            }
        }
    }

    private IEnumerator DestroyAfterOneSecond()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (ShipsList.Count > 0)
            {
                var ship = ShipsList[Random.Range(0, ShipsList.Count)];
                shipPool.ReturnToPool(ship);
                ShipsList.Remove(ship);
            }
        }
    }

    private void SpawShip(int quantity)
    {
        for (int i = 0; i < quantity; i++)
        {
            CountOfShips++;
            Ship ship = shipPool.GetFromPool(Vector3.zero, Quaternion.identity, transform);
            ship.name = $"Ship [{ShipsList.Count:D4}]";
            ShipsList.Add(ship);
        }
    }
}
