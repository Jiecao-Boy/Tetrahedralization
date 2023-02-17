//So far this progam still does not detect any point overlap with vertices (and on some triangles?)
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using System.IO;
using System.Linq;

public class check_surface: MonoBehaviour
{
    // Different from check_circumsphere, this program should be able to take in multiple surface vertices and triangles
    // the goal is to check if the vertex lies in a set of surface triangles
    public void in_surface(int[] triangles,Vector3[] vertices, Vector3 point){
        //Find helpful reference from here: 
        //https://courses.cs.washington.edu/courses/cse557/09au/lectures/extras/triangle_intersection.pdf#:~:text=To%20solve%20for%20the%20intersection%20of%20ray%20R%28t%29,the%20plane%20%28i.e.%2C%20the%20intersection%20is%20at%20infinity%29.
        //We only need a ray from the point pointing at random direction
        Vector3 ray_dir = new Vector3(0,1,0);
        Ray ray= new Ray(point,ray_dir); 
        //for any given triangle     
        int num_triangle = triangles.Length; 
        bool in_out = false; 
        float p_dist = Mathf.Infinity; 
        for (int i=0; i < num_triangle; i+=3){
            int a=triangles[i];
            int b=triangles[i+1];
            int c=triangles[i+2];
           
           //find the normal
            Plane plane=new Plane(vertices[a],vertices[b],vertices[c]); 
            Vector3 norm = plane.normal; 

            //now test if the ray intersects with the plane
            //enter is the distance along the array
            float enter = 0.0f; 
            if (plane.Raycast(ray,out enter)){
                //now we want to check if the intersection point lies on the triangle
                Vector3 p_inter = ray.GetPoint(enter);
                Vector3 d=vertices[b]-vertices[a];
                Vector3 e=vertices[c]-vertices[a];
                if (Mathf.Approximately(e.y, 0))
                {
                    e.y = 0.0001f;
                }
                
                double w1 = (e.x * (vertices[a].y - p_inter.y) + e.y * (p_inter.x - vertices[a].x)) / (d.x * e.y - d.y * e.x);
                double w2 = (p_inter.y - vertices[a].y - w1 * d.y) / e.y;
                if ((w1 >= 0f) && (w2 >= 0.0) && ((w1 + w2) <= 1.0)){
                    //if p_inter is in the trianfle, check if the distance from the point
                    //to the tirangle is the shorest so far
                    if (enter<p_dist){
                        //meaning this is the closest triangle so far
                        //test of the ray is pointing at the same direction with the normal
                        p_dist = enter; //over write p_dist with enter
                        if (Vector3.Dot(norm, ray_dir)>0){
                            in_out = true;
                        }
                        else{
                            in_out = false;
                        }
                    }
                }
            }
            }
        if (in_out == true){
            Debug.Log("true");
        }
        else{
            Debug.Log("false"); 
        }
           
        }

}




