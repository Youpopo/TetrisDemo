using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour
{  
    public Vector3 RotationAnchor = Vector3.zero;
    public static float FallTimeStep = .5f;
    private float previousTime = 0;
    public static int height = 20,width = 10;
    private static Transform[,] Grid = new Transform[width,height];
    
    

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow)){
            transform.position -=Vector3.right;
            if(!IsLastMoveValid())
                transform.position +=Vector3.right;

        }
        else if(Input.GetKeyDown(KeyCode.RightArrow)){
            transform.position +=Vector3.right;
            if(!IsLastMoveValid())
                transform.position -=Vector3.right;
        }
        else if(Input.GetKeyDown(KeyCode.UpArrow)){
            transform.RotateAround(transform.TransformPoint(RotationAnchor),Vector3.forward,90);
            if(!IsLastMoveValid())
                transform.RotateAround(transform.TransformPoint(RotationAnchor),Vector3.forward,-90);
        }

        if(Time.time - previousTime > (Input.GetKeyDown(KeyCode.DownArrow)? FallTimeStep/10 : FallTimeStep)){
            transform.position += Vector3.down;
            if(!IsLastMoveValid()){
                transform.position -= Vector3.down;
                AddThisToGrid();
                CheckForFullLines();
                FindObjectOfType<Spawner>().Spawn();
                this.enabled = false;
                gameObject.GetComponent<AudioSource>().Play();
            }
            previousTime = Time.time;
        }
    }

    bool IsLastMoveValid(){
        foreach (Transform child in transform)
        {
            int roundedX = Mathf.RoundToInt(child.position.x);
            int roundedY = Mathf.RoundToInt(child.position.y);

            if(roundedX <0||roundedX >=width||roundedY<0||roundedY>height){
                return false;
            }

            if(Grid[roundedX,roundedY] != null)
                return false;
        }
        return true;
    }

    void AddThisToGrid(){
        foreach (Transform child in transform)
        {
            int roundedX = Mathf.RoundToInt(child.position.x);
            int roundedY = Mathf.RoundToInt(child.position.y);

            Grid[roundedX,roundedY] = child;
        }
    }

    void CheckForFullLines(){
        for (int i = height -1; i >= 0; i--)
        {
            if(LineIsFull(i)){
                DeleteLine(i);
                RowDown(i);
                GameManager.instance.DeleteLine();
                SceneFunction.inst.UpdateScore();
            }
        }
    }

    bool LineIsFull(int i){
        for (int j = 0; j < width; j++)
        {
            if(Grid[j,i] == null)
            return false;
        }
        return true;
    }

    void DeleteLine(int i){
        for (int j = 0; j < width; j++)
        {
            Destroy(Grid[j,i].gameObject);
            Grid[j,i] = null;
        }
    }

    void RowDown(int i){
        for (int y = i; y < height; y++)
        {
            for (int j = 0; j < width; j++)
            {
                if(Grid[j,y] != null){
                    Grid[j,y-1] = Grid[j,y];
                    Grid[j,y] = null;
                    Grid[j,y-1].position += Vector3.down;
                }
            }
        }
    }

}
