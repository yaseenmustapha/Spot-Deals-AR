using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touch : MonoBehaviour
{
    private Ray ray;

    private RaycastHit hit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {

            ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

            Debug.DrawRay(ray.origin, ray.direction * 20, Color.red);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity) && hit.collider.tag == ("LocationBox"))
            {
                Application.OpenURL("http://maps.google.com/?q=Starbucks");
            }
            else if (Physics.Raycast(ray, out hit, Mathf.Infinity) && hit.collider.tag == ("DeliveryBox"))
            {
                Application.OpenURL("http://maps.google.com/?q=Starbucks");
            }
            else if (Physics.Raycast(ray, out hit, Mathf.Infinity) && hit.collider.tag == ("MenuBox"))
            {
                Application.OpenURL("https://delivery.starbucks.com/");
            }
            else if (Physics.Raycast(ray, out hit, Mathf.Infinity) && hit.collider.tag == ("RewardsBox"))
            {
                Application.OpenURL("https://www.starbucks.com/menu");
            }
            else if (Physics.Raycast(ray, out hit, Mathf.Infinity) && hit.collider.tag == ("DealsBox"))
            {
                Application.OpenURL("https://www.starbucks.com/rewards/");
            }
            else if (Physics.Raycast(ray, out hit, Mathf.Infinity) && hit.collider.tag == ("LocationBox"))
            {
                Application.OpenURL("https://www.redflagdeals.com/canada/starbucks-deals-coupons-sales/");
            }
        }
    }
}
