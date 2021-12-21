using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceBehaviour : MonoBehaviour
{
    [SerializeField] private Piece piece;
    [SerializeField] private ElectricityGame parentScript;

    private int collidersCount = 0;

    private void Start()
    {
        piece = GetComponent<Piece>();
        parentScript = GetComponentInParent<ElectricityGame>();

        int x = Random.Range(0, 4);
        for (int a = 0; a <= x; a++)
            Rotate();
    }

    private void OnTriggerEnter(Collider other)
    {
        collidersCount++;
        if(other.transform.gameObject.tag == "Player" && collidersCount == 1)
        {
            Debug.Log(other.transform.gameObject.tag);
            Rotate();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        collidersCount--;
    }

    public void Rotate()
    {
        //ANIMATION ROTATION
        /* delete Later */

        transform.Rotate(-90, 0, 0);

        bool _up = false;
        bool _right = false;
        bool _down = false;
        bool _left = false;

        if (piece.up)
            _right = true;
        if (piece.right)
            _down = true;
        if (piece.down)
            _left = true;
        if (piece.left)
            _up = true;

        piece.up = _up;
        piece.right = _right;
        piece.down = _down;
        piece.left = _left;

        parentScript.DoACycle();
    }
}
