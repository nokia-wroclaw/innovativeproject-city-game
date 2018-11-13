using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets
{

    class GPSManager : MonoBehaviour
    {
        public GameManager gameManager;

        //that let us use GPS services wherever we want
        public static GPSManager Instance { set; get; }

        public float latitude, longitude;

        private void Start()
        {
            Instance = this;
            //to have unbreakable connection between app and GPS services
            DontDestroyOnLoad(gameObject);
            StartCoroutine(InitializationOfLocationService());
            InvokeRepeating("updateCoordinates", 2.0f, 2.0f);
        }

        private void updateCoordinates()
        {
            latitude = Input.location.lastData.latitude;
            longitude = Input.location.lastData.longitude;

            gameManager.OnLocationChanged(longitude, latitude);
        }

        private void Update()
        {

        }

        private IEnumerator InitializationOfLocationService()
        {
            if (!Input.location.isEnabledByUser)
            {
                Debug.Log("Location services have been disabled");
                yield break; //==return;
            }

            Input.location.Start();
            int maxWaitTime = 10;
            while (Input.location.status == LocationServiceStatus.Initializing && maxWaitTime > 10)
            {
                yield return new WaitForSeconds(1);
                maxWaitTime--;
            }
            if (maxWaitTime <= 0)
            {
                Debug.Log("Timed out. Unable to get loaction.");
                yield break;
            }
            if (Input.location.status == LocationServiceStatus.Failed)
            {
                Debug.Log("Unable to determine device location");
                yield break;
            }
            updateCoordinates();

            yield break;
        }
    }
}