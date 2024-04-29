#nullable enable
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    //gracz (jego pozycja)
    Transform player;

    //prefab statycznej asteroidy
    public GameObject staticAsteroid;

    //czas od ostatio wygenerowanej asteoidy
    float timeSinceSpawn;

    //odleg�o� w jakiej spawnuj� si� asteroidy
    public float spawnDistance = 10;

    //odleg�o�� pomi�dzy asteroidami
    public float safeDistance = 10;

    //odst�p pomiedzy spawnem kolejnych asteroid
    public float cooldown = 0;

    // Start is called before the first frame update
    void Start()
    {
        //znajdz gracz i przypisz do zmiennej
        player = GameObject.FindWithTag("Player").transform;

        //zeruj czas
        timeSinceSpawn = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeSinceSpawn > cooldown)
        {
            SpawnAsteroid(staticAsteroid);
            timeSinceSpawn = 0;
        }


        AsteroidCountControll();

        timeSinceSpawn += Time.deltaTime;
    }

    void SpawnAsteroid(GameObject prefab)
    {
        //generyczna funkcja sluzaca do wylosowania wspolrzednych i umieszczenia
        //w tym miejscu asteroidy z prefaba

        //stworz losow� pozycj� na okr�gu (x,y)
        Vector2 randomCirclePosition = Random.insideUnitCircle.normalized;

        //losowa pozycja w odleg�o�ci 10 jednostek od �rodka �wiata
        //mapujemy x->x, y->z, a y ustawiamy 0
        Vector3 randomPosition = new Vector3(randomCirclePosition.x, 0, randomCirclePosition.y) * spawnDistance;

        //na�� pozycj� gracza - teraz mamy pozycje 10 jednostek od gracza
        randomPosition += player.position;

        //sprawdz czy miejsce jest wolne
        //! oznacza "nie" czyli nie ma nic w promieniu jednostek od miejsca randomPosition
        if (!Physics.CheckSphere(randomPosition, safeDistance))
        {
            //stworz zmienn� asteroid, zespawnuj nowy asteroid korzystaj�c z prefaba
            // w losowym miejscu, z rotacj� domy�ln� (Quaternion.identity)
            GameObject asteroid = Instantiate(staticAsteroid, randomPosition, Quaternion.identity);
        }

    }
    void AsteroidCountControll()
    {
        //przygotuj tablic� wszystkich asteroid�w na scenie
        GameObject[] asteroids = GameObject.FindGameObjectsWithTag("Asteroid");

        //przejd� p�tl� przez wyszystkie
        foreach (GameObject asteroid in asteroids)
        {
            //odleg�o�� od gracza

            //wektor przesuni�cia mi�dzy graczem a asteroid�
            //(o ile musze przesun�c gracza, �eby znalaz� si� w miejscu asteroidy
            Vector3 delta = player.position - asteroid.transform.position;

            //magnitude to dugo�� wektora = odleg�o�� od gracza
            float distanceToPlayer = delta.magnitude;

            if (distanceToPlayer > 30)
            {
                Destroy(asteroid);
            }
        }
    }
}