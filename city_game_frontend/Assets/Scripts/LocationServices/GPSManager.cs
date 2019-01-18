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

        public float latitude = 0, longitude = 0, rotation = 0;

#if UNITY_EDITOR
        public float fakeLatitude;
        public float fakeLongitude;
        public float fakeRotation;
        public bool fakeLocation = false;
#endif

        private void Start()
        {
            Instance = this;
            //to have unbreakable connection between app and GPS services
            DontDestroyOnLoad(gameObject);
            StartCoroutine(InitializationOfLocationService());
            InvokeRepeating("updateCoordinates", 5.0f, 5.0f);
        }

        private void updateCoordinates()
        {
            if (Application.platform == RuntimePlatform.WindowsPlayer)
            {
                gameManager.OnLocationChanged(longitude, latitude, rotation);
                return;
            }


            latitude = Input.location.lastData.latitude;
            longitude = Input.location.lastData.longitude;
            rotation = -Input.compass.trueHeading;

#if UNITY_EDITOR
            if (fakeLocation)
            {
                longitude = fakeLongitude;
                latitude = fakeLatitude;
                rotation = fakeRotation;
            }
#endif

            if (latitude != 0.0F && longitude != 0.0F) { 
                gameManager.OnLocationChanged(longitude, latitude, rotation);
                
            } else
            {
                Debug.Log("No gps connection!");
            }


            //Debug.Log("GPS: " + latitude + ", " + longitude);
        }

        private void Update()
        {

        }

        private IEnumerator InitializationOfLocationService()
        {

            // Wait until the editor and unity remote are connected before starting a location service
            
                
            yield return new WaitForSeconds(5);


            if (Application.platform == RuntimePlatform.WindowsPlayer)
            {
                string[] args = System.Environment.GetCommandLineArgs();

                try
                {
                    longitude = float.Parse(args[1]);
                    latitude = float.Parse(args[2]);
                }
                catch (Exception e)
                {
                    Debug.LogError("ERROR IN ARGUMENTS PARSING");
                    Debug.LogError(e.Data.ToString());
                }

                yield break;
            }

            if (!Input.location.isEnabledByUser)
            {
                Debug.Log("Location services have been disabled");
                yield break; //==return;
            }

            Input.location.Start();
            Input.compass.enabled = true;
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