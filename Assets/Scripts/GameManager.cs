using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start()
    {

        spawn = new Vector3(-5, 19, 0);
        nextPieceSpawn = new Vector3(8.5f, 14f, 0f);

        unusedTetrominos = Tetrominos;

        //InvokeRepeating("spawnPiece", 1f, 1f);
        currentPiece = spawnPiece(spawn);
        nextPiece = spawnPiece(nextPieceSpawn);


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("a")) // move left (change later)
        {
            Debug.Log(currentPiece.gameObject);

            if(currentPiece.transform.position.x > -9)
            {
                currentPiece.transform.position = new Vector3(currentPiece.transform.position.x + -1f, currentPiece.transform.position.y, currentPiece.transform.position.z);
            }
           
        }

        if (Input.GetKeyDown("d")) // move right (change later)
        {
            Debug.Log(currentPiece.gameObject);

            if (currentPiece.transform.position.x < -1)
            {
                currentPiece.transform.position = new Vector3(currentPiece.transform.position.x + 1f, currentPiece.transform.position.y, currentPiece.transform.position.z);
            }

        }

        if (Input.GetKeyDown("space"))
        {
            convertNextPieceToActive();
        }
    }

    GameObject spawnPiece(Vector3 spawnLocation)
    {

        GameObject pieceToSpawn;


        pieceToSpawn = Instantiate(unusedTetrominos[Random.Range(0, Tetrominos.Count)], new Vector3(spawnLocation.x, spawnLocation.y, spawnLocation.z), Quaternion.identity);

        unusedTetrominos.Remove(pieceToSpawn);
        usedTetrominos.Add(pieceToSpawn);

        if(usedTetrominos[0] != null && usedTetrominos.Count == 3)
        {
            unusedTetrominos.Add(usedTetrominos[0]);
            usedTetrominos.Remove(usedTetrominos[0]);

            
        }

        return pieceToSpawn;

    }

    //GameObject generatePiece()
    //{
    //    GameObject piece = unusedTetrominos[Random.Range(0, Tetrominos.Count)];

    //    return piece;

    //}

    //float generatePieceBoundaryNegative()
    //{

    //    if(currentPiece.gameObject.tag == "L" || currentPiece.gameObject.tag == "J" || currentPiece.gameObject.tag == "O")
    //    {
    //        return -9;
    //    }

    //    if (currentPiece.gameObject.tag == "L" || currentPiece.gameObject.tag == "J" || currentPiece.gameObject.tag == "O")
    //    {
    //        return -9;
    //    }


    //}

    void convertNextPieceToActive()
    {
        currentPiece = nextPiece;

        Debug.Log(currentPiece.gameObject);

        currentPiece.transform.position = spawn;

        nextPiece = spawnPiece(nextPieceSpawn);

    }
}
