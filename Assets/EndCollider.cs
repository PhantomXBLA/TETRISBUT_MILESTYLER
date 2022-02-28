using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCollider : MonoBehaviour
{
    public GameObject gameManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Line" || other.gameObject.tag == "LJ" || other.gameObject.tag == "Square" || other.gameObject.tag == "SZ" || other.gameObject.tag == "T")
        {
            gameManager.GetComponent<GameManager>().endGame();
        }
    }
}
