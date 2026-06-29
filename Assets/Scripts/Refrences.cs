using UnityEngine;

public class Refrences : MonoBehaviour
{
    public static volatile Refrences @r;

    public ObjectManager @o;
    public Spawner @s;
    public LogPanel @l;
    public CameraDrag @d;
    public MouseGrabber @g;
    public UIManager @ui;
    public DotGrid @grid;

    void Awake()
    {
        @r=this;
    }
}
