using UnityEditor;
using UnityEngine;

public enum VectorTestType
{
    Disabled,
    XVectorBasedX,
    Cross,
    Project,
    ProjectOnPlane,
    Reflect,
    Scale,
    XFloatBasedX
}

public class VectorTester : MonoBehaviour
{
    public VectorTestType testSelection;
    public Transform obj1;
    public Transform obj2;

    public void Update()
    {
        
    }

    public Vector3 GetLocalVector(int num)
    {
        Vector3 Solved = Vector3.up;
        if(num > 0)
        {
            Solved = obj1.position - transform.position;
        }
        else
        {
            Solved = obj2.position - transform.position;
        }
        return Solved;
    }


    public void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + GetLocalVector(1));
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + GetLocalVector(0));

        Gizmos.color = Color.blue;

        if(testSelection == VectorTestType.Cross || testSelection == VectorTestType.XVectorBasedX)
            Gizmos.DrawLine(transform.position, transform.position +  Vector3.Cross(GetLocalVector(1), GetLocalVector(0)));

        if(testSelection == VectorTestType.Project || testSelection == VectorTestType.XVectorBasedX)
            Gizmos.DrawLine(transform.position, transform.position +  Vector3.Project(GetLocalVector(1), GetLocalVector(0)));
        
        if(testSelection == VectorTestType.ProjectOnPlane || testSelection == VectorTestType.XVectorBasedX)
            Gizmos.DrawLine(transform.position, transform.position +  Vector3.ProjectOnPlane(GetLocalVector(1), GetLocalVector(0)));

        if(testSelection == VectorTestType.Reflect || testSelection == VectorTestType.XVectorBasedX)
            Gizmos.DrawLine(transform.position, transform.position +  Vector3.Reflect(GetLocalVector(1), GetLocalVector(0)));

        if(testSelection == VectorTestType.Scale || testSelection == VectorTestType.XVectorBasedX)
            Gizmos.DrawLine(transform.position, transform.position +  Vector3.Scale(GetLocalVector(1), GetLocalVector(0)));
    }
}
