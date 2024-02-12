using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float rotationSpeed = 200f;
    public float flySpeed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //dodaj do wspolrzednych wartosc x=1, y=0, z=0 pomozone przez czas
        //mierzony w sekundach od ostatniej klatki
        // transform.position += new Vector3(1, 0, 0) * Time.deltaTime;

        //prezentacja dzialania wygladzonego sterownika (emulacja joysticka)
        // Debug.Log(Input.GetAxis("Vertical"));

        //sterowanie predkoscia
        //stworz nowy wektor przesuniecia o wartosci 1 do przodu 
        Vector3 movement = transform.forward;
        //pomnoz go przez czas od ostatniej klatki
        movement *= Time.deltaTime;
        //pomnoz go przez "wychylenie joysticka"
        movement *= Input.GetAxis("Vertical");
        //pomnoz przez predkosc lotu
        movement *= flySpeed;
        //dodaj ruch obiektu
        transform.position += movement;

        //obrot
        //modyfikuj os Y obiektu player 
        Vector3 rotation = Vector3.up;
        //przemnoz przez czas
        rotation *= Time.deltaTime;
        //przemnoz przez klawiature
        rotation *= Input.GetAxis("Horizontal");
        //pomnoz przez czas obrotu
        rotation *= rotationSpeed;
        //dodaj obrot do obiektu
        //nie mozemy uzyc += poniewaz unity uzywa quaternionow do zapisu rotacji
        transform.Rotate(rotation);

    }
}
