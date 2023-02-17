using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static check_circumsphere;
using static check_surface; 
public class main : MonoBehaviour
{
	public float x1, y1, z1, x2, y2, z2, x3, y3, z3, x4, y4, z4;
    public float test_x, test_y, test_z;
    public check_surface c; 
    public check_circumsphere s; 
    public int[] triangles;  
    public Vector3[] vertices; 


    // Start is called before the first frame update
    
    void Start()
    {
        triangles = new int[12];
        triangles[0]=2;
        triangles[1]=1;
        triangles[2]=0;
        triangles[3]=0;
        triangles[4]=1;
        triangles[5]=3;
        triangles[6]=3;
        triangles[7]=2;
        triangles[8]=0;
        triangles[9]=1;
        triangles[10]=2;
        triangles[11]=3;

        vertices = new Vector3[4];
        vertices[0]=new Vector3(x1,y1,z1);
        vertices[1]=new Vector3(x2,y2,z3);
        vertices[2]=new Vector3(x3,y3,z3);
        vertices[3]=new Vector3(x4,y4,z4);      

        GameObject gameobject1= new GameObject("check_surface");
        c=gameobject1.AddComponent<check_surface>(); 
        
        Vector3 point = new Vector3(test_x, test_y, test_z); 
        c.in_surface(triangles,vertices, point);
        
        GameObject gameobject2= new GameObject("check_circumsphere");
        s=gameobject2.AddComponent<check_circumsphere>(); 
        s.in_or_out(x1, y1, z1, x2, y2, z2, x3, y3, z3, x4, y4, z4,test_x, test_y, test_z);           
    }

    // Update is called once per frame
    void Update()
    {

    }
}
