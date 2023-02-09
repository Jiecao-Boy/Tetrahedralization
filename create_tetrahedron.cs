using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using System.IO;
using System.Linq;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class create_tetrahedron : MonoBehaviour
{
	public int x1, y1, z1, x2, y2, z2, x3, y3, z3, x4, y4, z4;
	private Mesh mesh;
	private Vector3[] vertices;

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



}

