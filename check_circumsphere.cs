
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using System.IO;
using System.Linq;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class check_circumsphere : MonoBehaviour
{
	public int x1, y1, z1, x2, y2, z2, x3, y3, z3, x4, y4, z4;
    public int test_x, test_y, test_z;
	private Mesh mesh;
	private Vector3[] vertices;
    //Different from creating vertex, this program takes in one more vertex (test)
    // the goal is to check if the vertex lies in the tetrahedral formed by the other four vertices. 

    void in_or_out(){
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


    }

    void Start(){
        in_or_out();
    }

    
/***
     # Define the position vectors of the vertices of the tetrahedron
     a = [1, 2, 3]
     b = [4, 5, 6]
     c = [7, 8, 9]
     d = [10, 11, 12]

# Define the position vector of the point
p = [13, 14, 15]

# Import numpy for vector operations
import numpy as np

# Calculate the scalar triple product of a, b and c
stp = np.dot(a, np.cross(b, c))

# Calculate the position vector of the center of the circumsphere
r = (np.dot(a, a) * np.cross(b, c) + np.dot(b, b) * np.cross(c, a) + np.dot(c, c) * np.cross(a, b)) / (2 * stp)

# Calculate the radius of the circumsphere
radius = np.linalg.norm(r)

# Calculate the distance of the point from the center of the circumsphere
distance = np.linalg.norm(p - r)

# Compare the distance with the radius
if distance < radius:
    print("The point is inside the circumsphere.")
elif distance == radius:
    print("The point is on the circumsphere.")
else:
    print("The point is outside the circumsphere.")

    private void Awake () {
		Generate();
	}

	private void Generate () {
		GetComponent<MeshFilter>().mesh = mesh = new Mesh();
		mesh.name = "Procedural Cube";
		CreateMesh ();
    }

    // Start is called before the first frame update
	private void setVertex (int i, float x, float y, float z) {
		vertices[i] = new Vector3(x, y, z);
	}

    private void CreateMesh()
    {
  
        //Set up vertices
        vertices = new Vector3[4];
        setVertex(0, x1, y1, z1 );
        setVertex(1, x2, y2, z2);
        setVertex(2, x3, y3, z3);
        setVertex(3, x4, y4, z4);
        mesh.vertices = vertices;

        //Set up faces/triangles
        int[] triangles = new int[12];
        //create a random plane: 
        Plane plane1 = new Plane(vertices[0], vertices[1], vertices[2]);
        Vector3 norm1 = plane1.normal;
        Vector3 ad = new Vector3(x4-x1, y4-y1, z4-z1);
        float dotProduct = norm1[0]*ad[0]+norm1[1]*ad[1]+norm1[2]*ad[2];
        if (dotProduct > 0)
        {
            triangles[0] = 2 ;
            triangles[1] = 1 ;
            triangles[2] = 0 ;
            plane1 = new Plane(vertices[2], vertices[1], vertices[0]);
            norm1 = plane1.normal;
        }
        else{
            triangles[0] = 0 ;
            triangles[1] = 1 ;
            triangles[2] = 2 ;
        }

        Plane plane2 = new Plane(vertices[0], vertices[1], vertices[3]);
        Vector3 norm2 = plane2.normal;       
        float dotProduct1 = norm1[0]*norm2[0]+norm1[1]*norm2[1]+norm1[2]*norm2[2];
        if (dotProduct1 > 0)
        {
            triangles[3] = 3 ;
            triangles[4] = 1 ;
            triangles[5] = 0 ;
        }
        else{
            triangles[3] = 0 ;
            triangles[4] = 1 ;
            triangles[5] = 3 ;
        }

        Plane plane3 = new Plane(vertices[0], vertices[2], vertices[3]);
        Vector3 norm3 = plane3.normal;       
        float dotProduct2 = norm1[0]*norm3[0]+norm1[1]*norm3[1]+norm1[2]*norm3[2];
        if (dotProduct2 > 0)
        {
            triangles[6] = 3 ;
            triangles[7] = 2 ;
            triangles[8] = 0 ;
        }
        else{
            triangles[6] = 0 ;
            triangles[7] = 2 ;
            triangles[8] = 3 ;
        }

        Plane plane4 = new Plane(vertices[1], vertices[2], vertices[3]);
        Vector3 norm4 = plane4.normal;       
        float dotProduct3 = norm1[0]*norm4[0]+norm1[1]*norm4[1]+norm1[2]*norm4[2];
        if (dotProduct3 > 0)
        {
            triangles[9] = 3 ;
            triangles[10] = 2 ;
            triangles[11] = 1 ;
        }
        else{
            triangles[9] = 1 ;
            triangles[10] = 2 ;
            triangles[11] = 3 ;
        }

        mesh.triangles = triangles;
        //first have to determine the number of triangles\
        //here we make an assumple that one face only features one triangle

    }

***/

}