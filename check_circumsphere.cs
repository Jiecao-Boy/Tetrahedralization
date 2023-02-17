
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using System.IO;
using System.Linq;
using static check_surface;

//[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class check_circumsphere : MonoBehaviour
{
	public float x1, y1, z1, x2, y2, z2, x3, y3, z3, x4, y4, z4;
    public float test_x, test_y, test_z;
    public check_surface c; 
    //Different from creating vertex, this program takes in one more vertex (test)
    // the goal is to check if the vertex lies in the tetrahedral formed by the other four vertices. 

    public void in_or_out(){
        //First regenterate the five points we have 
        Vector3 a = new Vector3(x1,y1,z1);
        Vector3 b = new Vector3(x2,y2,z2);
        Vector3 c = new Vector3(x3,y3,z3);
        Vector3 d = new Vector3(x4,y4,z4);        
        Vector3 test = new Vector3(test_x, test_y,test_z); 

        //Calculate the position vector of the center of circumsphere       
        //I don't have any knowledge on finding the center of a cirumspehre of tetrahedron
        //I follow the information on this website: 
        // https://math.stackexchange.com/questions/1517047/prove-that-the-center-of-the-sphere-circumscribing-the-tetrahedron-is-given-by-p#:~:text=If%20O%20A%20B%20C%20is%20a%20tetrahedron,2%20%5B%20a%20%E2%86%92%20b%20%E2%86%92%20c%20%E2%86%92%5D
        Vector3 A = a-d; 
        Vector3 B = b-d;
        Vector3 C = c-d;
        Vector3 test1 = Vector3.Cross(B,C);
        float stp = Vector3.Dot(A, Vector3.Cross(B,C));
        Vector3 center = (Vector3.Dot(A,A) * Vector3.Cross(B,C) + Vector3.Dot(B,B)* Vector3.Cross(C,A) + Vector3.Dot(C,C)*Vector3.Cross(A,B)) / (2*stp);
        //Donn't forget to transfer the center vector back to the universal frame!
        center = center + d;
        Debug.Log(center);

        //check the distance between the test point and the center 
        Vector3 vec1 = test - center;
        float dis1 = vec1[0]*vec1[0] + vec1[1]*vec1[1] + vec1[2]*vec1[2]; 
        Vector3 vec2 = a - center;
        float dis2 = vec2[0]*vec2[0] + vec2[1]*vec2[1] + vec2[2]*vec2[2]; 

        if (dis1 <= dis2){
            Debug.Log("in");
        }
        else {
            Debug.Log("out");
        }
    }

    void Start(){
        in_or_out();
        //check_surface c = new check_surface();
        GameObject gameobject= new GameObject("check_surface");
        c=gameobject.AddComponent<check_surface>(); 
        c.in_surface();
    }



}