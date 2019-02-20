using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(handleAnimation());
    }

    private IEnumerator handleAnimation()
    {
        yield return 1.5f;
        gameObject.SetActive(false);
    }
}
