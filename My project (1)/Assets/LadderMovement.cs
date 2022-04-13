using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderMovement : MonoBehaviour
{
 [SerializeField] float movementv;
    [SerializeField] Rigidbody2D rigid;

 [SerializeField] bool isLadder = false;
[SerializeField] bool isBadLadder = false; 
   [SerializeField] bool isClimbing = false;
 [SerializeField] bool isFalling = false;
     [SerializeField] int speed;

    // Start is called before the first frame update
    void Start()
    {
        if (rigid == null)
            rigid = GetComponent<Rigidbody2D>();
        speed = 8;
    }

    // Update is called once per frame
    void Update()
    {
         movementv = Input.GetAxis("Vertical");
     if (isLadder && Mathf.Abs(movementv) > 0f || isBadLadder && Mathf.Abs(movementv) > 0f)
        {
            isClimbing = true;
        }
    }

  private void FixedUpdate()
    {
if(isFalling){
fall();
 StartCoroutine(fall2());
   isFalling  = false;
}else{

        if (isClimbing)
        {
 
	

            rigid.gravityScale = 0f;
            rigid.velocity = new Vector2(rigid.velocity.x, movementv * speed);
if( isBadLadder){

 fall();
}		
        }
        else 
        {
            rigid.gravityScale = 1f;
        }

    }
}
  private void fall(){
if(Random.Range(1, 100)<5){
 isBadLadder = false;
		isLadder = false;
            isClimbing = false;
rigid.gravityScale = 1f;
   isFalling  = true;
Debug.Log("we fell");

}
}

 IEnumerator fall2(){
 yield return new WaitForSecondsRealtime(4);


}



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ladder"))
        {
            isLadder = true;
        }

 if (collision.CompareTag("bad") )
        {
            isBadLadder = true;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("ladder"))
        {
            isBadLadder = false;
		isLadder = false;
            isClimbing = false;
        }
    }


}
