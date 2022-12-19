using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.ARFoundation;
using HtmlAgilityPack;
using TMPro;


public class ImageRecognition : MonoBehaviour {
    private ARTrackedImageManager m_TrackedImageManager;
    [SerializeField] private GameObject[] objects;
    private Dictionary<string, GameObject> spawnablePrefabs = new Dictionary<string, GameObject>();
    [SerializeField] private Text currImageText;
    private MenuAnimation menuAnimation;
    private bool animPlayed = false;

    void Awake() {
        StartCoroutine(StarbucksRoutine());

        m_TrackedImageManager = GetComponent<ARTrackedImageManager>();
        
        // add GameObjects from inspector to Dictionary
        foreach (GameObject go in objects) {
            GameObject newARObject = Instantiate(go, Vector3.zero, Quaternion.identity);
            newARObject.name = go.name;
            newARObject.SetActive(false);
            spawnablePrefabs.Add(go.name, newARObject);
        }
    }

    void OnEnable() {
        m_TrackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    void OnDisable() {
        m_TrackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs args) {
        foreach (var trackedImage in args.added) {
            GetData(trackedImage.referenceImage.name);
            
        }

        foreach (var trackedImage in args.updated) {
            // if an image is being properly tracked
            if (trackedImage.trackingState == TrackingState.Tracking) {
                // set prefab corresponding to image as active
                spawnablePrefabs[trackedImage.referenceImage.name].SetActive(true);
                spawnablePrefabs[trackedImage.referenceImage.name].transform.position = trackedImage.transform.position;
                menuAnimation = GameObject.FindWithTag("TopBoxTag").GetComponent<MenuAnimation>();
                menuAnimation.StartAnimation();

                currImageText.text = "Tracking: " + trackedImage.referenceImage.name;

                //GetData(trackedImage.referenceImage.name);
            } else {
                spawnablePrefabs[trackedImage.referenceImage.name].SetActive(false);
                currImageText.text = "Tracking: none";

                //dealText.text = "";
                /*
                foreach (GameObject go in spawnablePrefabs.Values) {
                    // otherwise, set all prefabs as inactive
                    
                    go.SetActive(false);
                    currImageText.text = "Tracking: none";
                }*/
            }
        }

    }

    // starts web request routine based on image name in the ReferenceImageLibrary
    private void GetData(string imageName) {
        switch (imageName) {
            case "Starbucks":
                StartCoroutine(StarbucksRoutine());
                break;
            case "KFC":
                KFC();
                break;
                
        }

    }

    IEnumerator StarbucksRoutine() {
        string url = "https://www.redflagdeals.com/canada/starbucks-deals-coupons-sales/";
        var request = UnityWebRequest.Get(url);

        // wait for web request to complete
        yield return request.SendWebRequest();
        var html = request.downloadHandler.text;

        // parse data
        Debug.Log(html);

        HtmlDocument doc = new HtmlDocument();
        doc.LoadHtml(html);

        HtmlNodeCollection paragraphs = doc.DocumentNode.SelectNodes("//a[@class= 'offer_title']");

        
        TextMeshPro dealText = GameObject.FindWithTag("DealText").GetComponent<TextMeshPro>();
        dealText.text = paragraphs[0].InnerText;

        // TODO: add business hours functionality; see: https://developers.google.com/places/web-service/details#PlaceDetailsResults

        //dealText.text = paragraphs[0].InnerText;
        //Debug.Log(paragraphs[0].InnerText);
    }

    private void KFC() {

    }

}
