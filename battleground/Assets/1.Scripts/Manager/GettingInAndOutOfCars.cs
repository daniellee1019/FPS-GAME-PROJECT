using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityStandardAssets.Vehicles.Car;

public class GettingInAndOutOfCars : MonoBehaviour
{
    [Header("Camera")]
    //[SerializeField] AutoCam mCamera = null;
    public GameObject CarCam;
    public GameObject HumanCam;

    [Header("Human")]
    public GameObject human;


    [SerializeField] float closeDistance = 15f;

    [Space, Header("Car Stuff")]
    public GameObject car;

    [Header("Input")]
    [SerializeField] KeyCode enterExitKey = KeyCode.F;

    public CarController carEngine;

    bool inCar = false;


    // Start is called before the first frame update
    void Start()
    {
        
        car.GetComponent<CarController>();
        car.GetComponent<CarUserControl>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(enterExitKey))
        {
            if (inCar)
            {
                GetOutOfCar();

            }
            else if (Vector3.Distance(car.transform.position, human.transform.position) < closeDistance)
            {
                GetInroCar();
            }

        }
    }

    void GetOutOfCar()
    {
        inCar = false;

        human.SetActive(true);
        HumanCam.SetActive(true);

        human.transform.position = car.transform.position + car.transform.TransformDirection(Vector3.left);

        CarCam.SetActive(false);
        car.GetComponent<CarController>().enabled = false;
        car.GetComponent<CarUserControl>().enabled = false;

        carEngine.Move(0, 0, 1, 1);
    }


    void GetInroCar()
    {
        inCar = true;

        CarCam.SetActive(true);

        human.SetActive(false);
        HumanCam.SetActive(false);

        car.GetComponent<CarController>().enabled = true;
        car.GetComponent<CarUserControl>().enabled = true;

    }
}