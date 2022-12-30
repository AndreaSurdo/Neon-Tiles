using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextColorAnimation : MonoBehaviour
{
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void SlideIn(){
        animator.SetTrigger("TrIn");
    }
     public void SlideOut(){
        animator.SetTrigger("TrOut");
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
