using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animplay : MonoBehaviour
{
    public Animation anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = anim.GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D)) anim.Play("mixamo.com");
    }
}
