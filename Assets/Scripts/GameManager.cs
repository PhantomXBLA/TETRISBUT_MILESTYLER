using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public List<GameObject> Tetrominos = new List<GameObject>();

    public List<GameObject> unusedTetrominos = new List<GameObject>();
    public List<GameObject> usedTetrominos = new List<GameObject>();

     GameObject currentPiece;
     GameObject nextPiece;

    //[SerializeField]
    //GameObject previousPiece1;
    //[SerializeField]
    //GameObject previousPiece2;


    Vector3 spawn;
    Vector3 nextPieceSpawn;

    float leftBoundary;
    float rightBoundary;

    bool currentPieceRotated;

    Vector3 raycastHitPoint;

    // Start is called before the first frame update
    void Start()
    {

        spawn = new Vector3(-5, 19, 0);
        nextPieceSpawn = new Vector3(8.5f, 14f, 0f);

        unusedTetrominos = Tetrominos;

        //InvokeRepeating("spawnPiece", 1f, 1f);
        currentPiece = spawnPiece(spawn);

        //InvokeRepeating("movePieceDown", 1f, 0.5f);

        if(currentPiece.gameObject.tag == "SZ" || currentPiece.gameObject.tag == "T" || currentPiece.gameObject.tag == "Line")
        {
            currentPiece.transform.position = new Vector3(-4.5f, spawn.y, spawn.z); //setup spawn offset for irregular pieces
        }

        nextPiece = spawnPiece(nextPieceSpawn);


    }

    // Update is called once per frame
    void Update()
    {

       if(currentPiece.transform.position.y < 14)
        {
            foreach (BoxCollider collider in currentPiece.GetComponents<Collider>())
            {
                collider.enabled = true;
            }
        }

        if (Input.GetKeyDown("e"))
        {
            currentPiece.transform.Rotate(new Vector3(0, 0, Vector3.up.x + -90), Space.World); // had to use vector 3 up since it would not rotate cleanly 
        }


        if (Input.GetKeyDown("q"))
        {
            currentPiece.transform.Rotate(new Vector3(0, 0, Vector3.up.x + 90), Space.World); // had to use vector 3 up since it would not rotate cleanly 
        }


        if (Input.GetKeyDown("space"))
        {
            foreach(BoxCollider collider in currentPiece.GetComponents<Collider>())
            {
                collider.enabled = true;
            }
            currentPiece.GetComponent<Rigidbody>().useGravity = true;

            convertNextPieceToActive();
        }
    }

    GameObject spawnPiece(Vector3 spawnLocation)
    {

        GameObject pieceToSpawn;


        pieceToSpawn = Instantiate(unusedTetrominos[Random.Range(0, unusedTetrominos.Count)], new Vector3(spawnLocation.x, spawnLocation.y, spawnLocation.z), Quaternion.identity);


        unusedTetrominos.Remove(currentPiece);
        usedTetrominos.Add(currentPiece);

        if (usedTetrominos[0] != null && usedTetrominos.Count == 3)
        {
            unusedTetrominos.Add(usedTetrominos[0]);
            usedTetrominos.Remove(usedTetrominos[0]);

            
        }

        return pieceToSpawn;

    }

    GameObject generatePiece()
    {
        GameObject piece = unusedTetrominos[Random.Range(0, Tetrominos.Count)];

        return piece;

    }

    void generatePieceBoundaryNegative()
    {

        switch (currentPiece.gameObject.tag)
        {
            case "SZ":
                leftBoundary = -8.5f;
                rightBoundary = -1.5f;
                break;

            case "Square":
                leftBoundary = -9f;
                rightBoundary = -1f;
                break;

            case "T":
                leftBoundary = -8.5f;
                rightBoundary = -1.5f;
                break;

            case "LJ":
                leftBoundary = -9f;
                rightBoundary = -1f;
                break;

            case "Line":
                leftBoundary = -9.5f;
                rightBoundary = -0.5f;
                break;
        }

    }

    void movePieceDown()
    {
        currentPiece.transform.position = new Vector3(currentPiece.transform.position.x, currentPiece.transform.position.y - 1, currentPiece.transform.position.z);
    }

   public void OnMoveRight()
    {
        //Debug.Log(currentPiece.gameObject);
        generatePieceBoundaryNegative();

        if (currentPiece.transform.position.x < rightBoundary)
        {
            currentPiece.transform.position = new Vector3(currentPiece.transform.position.x + 1f, currentPiece.transform.position.y, currentPiece.transform.position.z);
        }
    }

    public void OnMoveLeft()
    {
        //Debug.Log(currentPiece.gameObject);
        generatePieceBoundaryNegative();

        if (currentPiece.transform.position.x > leftBoundary)
        {
            currentPiece.transform.position = new Vector3(currentPiece.transform.position.x + -1f, currentPiece.transform.position.y, currentPiece.transform.position.z);
        }
    }

  

    void convertNextPieceToActive()
    {
        currentPiece = nextPiece;

        Debug.Log(currentPiece.gameObject);

        currentPiece.transform.position = spawn;

        if (currentPiece.gameObject.tag == "SZ" || currentPiece.gameObject.tag == "T" || currentPiece.gameObject.tag == "Line")
        {
            currentPiece.transform.position = new Vector3(-4.5f, spawn.y, spawn.z); //setup spawn offset for irregular pieces
        }

        nextPiece = spawnPiece(nextPieceSpawn);

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(raycastHitPoint, 0.2f);
    }
}
