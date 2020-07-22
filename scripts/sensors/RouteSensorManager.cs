using UnityEngine;

public class RouteSensorManager : MonoBehaviour {


    [SerializeField]
    GameObject leftSideObj, rightSideObj;

    BoxCollider2D bc;

    Vector2 left, right;

    Vector3 leftV3, rightV3;


	void Start () {

        bc = GetComponent<BoxCollider2D>();

        left.Set(leftSideObj.transform.position.x, leftSideObj.transform.position.y);
        right.Set(rightSideObj.transform.position.x, rightSideObj.transform.position.y);

        leftV3.Set(leftSideObj.transform.position.x, leftSideObj.transform.position.y, 0);
        rightV3.Set(rightSideObj.transform.position.x, rightSideObj.transform.position.y, 0);

	}



    public Vector2 getLeft(){
        return left;
    }

    public Vector2 getRight(){
        return right;
    }

    public Vector3 getLeftV3(){
        return leftV3;
    }

    public Vector3 getRightV3(){
        return rightV3;
    }

}
