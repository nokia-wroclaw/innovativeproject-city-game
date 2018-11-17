﻿using System;
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

#if UNITY_EDITOR
        public float fakeLatitude;
        public float fakeLongitude;
        public bool fakeLocation = false;
#endif

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

#if UNITY_EDITOR
            if (fakeLocation)
            {
                longitude = fakeLongitude;
                latitude = fakeLatitude;
            }
#endif

            if (latitude != 0.0F && longitude != 0.0F) { 
                gameManager.OnLocationChanged(longitude, latitude);
                Debug.Log("No gps connection!");
            }

            Debug.Log("GPS: " + latitude + ", " + longitude);
        }

        private void Update()
        {

        }

        private IEnumerator InitializationOfLocationService()
        {

            // Wait until the editor and unity remote are connected before starting a location service
#if UNITY_EDITOR
            if (UnityEditor.EditorApplication.isRemoteConnected)
            {
                yield return new WaitForSeconds(5);
            }
#endif

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