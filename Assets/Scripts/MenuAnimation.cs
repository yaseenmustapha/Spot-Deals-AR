using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAnimation : MonoBehaviour
{
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartAnimation()
    {
        anim.SetBool("TopBoxAnim", true);
    }
    public void StopAnimation()
    {
        anim.SetBool("TopBoxAnim", false);
    }

}
