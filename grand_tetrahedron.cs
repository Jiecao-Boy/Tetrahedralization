using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grand_tetrahedron : MonoBehaviour
{
    // SThe goal is to form a tetrahedron that contains all the vertices to start with
    //the function takes in all the vertices
    public void form_grand (Vector3[] vertices){
        //find the limit values: 
        float max_x = -Mathf.Infinity;
        float max_y = -Mathf.Infinity; 
        float max_z = -Mathf.Infinity;
        float min_x = Mathf.Infinity; 
        float min_y = Mathf.Infinity; 
        float min_z = Mathf.Infinity; 
        int len = vertices.Length;
        for (int k = 0; k < len; k++){
            //Traverse through the vertices to find limit values
            if (vertices[k].x > max_x){
                max_x = vertices[k].x;
            }
            if (vertices[k].y > max_y){
                max_y = vertices[k].y;
            }
            if (vertices[k].z > max_z){
                max_z = vertices[k].z;
            }
            if (vertices[k].x < min_x){
                min_x = vertices[k].x;
            }
            if (vertices[k].y < min_y){
                min_y = vertices[k].y;
            }
            if (vertices[k].z < min_z){
                min_z = vertices[k].z;
            }

        }
        Debug.Log(max_x);
        Debug.Log(max_y);
        Debug.Log(max_z);
        Debug.Log(min_x);   
        Debug.Log(min_y);
        Debug.Log(min_z);
    }
}
