using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] string Vert;
    [SerializeField] string Horiz;
    public float speed;

    Rigidbody rb;
    GameObject Player;
    CharacterController Char;


    // Start is called before the first frame update
    void Start()
    {
        Player = this.gameObject;
        rb = Player.GetComponent<Rigidbody>();
        Char = Player.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Move());
    }





    IEnumerator Move()
    {
        yield return new WaitForFixedUpdate();

        float VertInput = Input.GetAxis(Vert);
        float HorizInput = Input.GetAxis(Horiz);
        Vector3 moveValues = new Vector3(HorizInput, 0, VertInput) * speed;

        Char.SimpleMove(moveValues);

    }

}
