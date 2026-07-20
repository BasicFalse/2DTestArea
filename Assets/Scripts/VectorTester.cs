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
    public bool TwoStep = false;

    public VectorTestType testSelection2;
    public Transform obj1, obj2, obj3;

    public Vector3 GetLocalVector(int num)
    {
        Vector3 Solved = Vector3.up;
        if(num == 0)
        {
            Solved = obj1.position - transform.position;
        }
        else if(num == 1)
        {
            Solved = obj2.position - transform.position;
        }
        else
        {
            Solved = obj3.position - transform.position;
        }
        return Solved;
    }


    public void OnDrawGizmos()
    {
        Vector3 solve1 = new();
        Vector3 solve2 = new();
        Gizmos.color = Color.blue;
        if(TwoStep)Gizmos.DrawLine(transform.position, transform.position + GetLocalVector(2));
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + GetLocalVector(1));
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + GetLocalVector(0));

        {
            if(testSelection == VectorTestType.Cross || testSelection == VectorTestType.XVectorBasedX)
                solve1 = Vector3.Cross(GetLocalVector(1), GetLocalVector(0));

            if(testSelection == VectorTestType.Project || testSelection == VectorTestType.XVectorBasedX)
                solve1 =  Vector3.Project(GetLocalVector(1), GetLocalVector(0));

            if(testSelection == VectorTestType.ProjectOnPlane || testSelection == VectorTestType.XVectorBasedX)
                solve1 = Vector3.ProjectOnPlane(GetLocalVector(1), GetLocalVector(0));

            if(testSelection == VectorTestType.Reflect || testSelection == VectorTestType.XVectorBasedX)
                solve1 = Vector3.Reflect(GetLocalVector(1), GetLocalVector(0));

            if(testSelection == VectorTestType.Scale || testSelection == VectorTestType.XVectorBasedX)
                solve1 = Vector3.Scale(GetLocalVector(1), GetLocalVector(0));
        }

        if (TwoStep)
        {
            if(testSelection2 == VectorTestType.Cross || testSelection2 == VectorTestType.XVectorBasedX)
                solve2 = Vector3.Cross(solve1, GetLocalVector(2));

            if(testSelection2 == VectorTestType.Project || testSelection2 == VectorTestType.XVectorBasedX)
                solve2 =  Vector3.Project(solve1, GetLocalVector(2));

            if(testSelection2 == VectorTestType.ProjectOnPlane || testSelection2 == VectorTestType.XVectorBasedX)
                solve2 = Vector3.ProjectOnPlane(solve1, GetLocalVector(2));

            if(testSelection2 == VectorTestType.Reflect || testSelection2 == VectorTestType.XVectorBasedX)
                solve2 = Vector3.Reflect(solve1, GetLocalVector(2));

            if(testSelection2 == VectorTestType.Scale || testSelection2 == VectorTestType.XVectorBasedX)
                solve2 = Vector3.Scale(solve1, GetLocalVector(2));
            
        }

        Gizmos.color = Color.bisque;
        Gizmos.DrawLine(transform.position, transform.position + solve1);
        Gizmos.color = Color.coral;
        Gizmos.DrawLine(transform.position, transform.position + solve2);
    }
}
