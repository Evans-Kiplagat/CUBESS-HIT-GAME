
using UnityEngine;
using TMPro;

public class CUbeId : MonoBehaviour
{
    static int staticID = 0;
    [HideInInspector] public int CubeID;

    [SerializeField] private TMP_Text[] TxtNumbers;

    [HideInInspector] public Color CubeColor;
    [HideInInspector] public int CubeNumber;
    [HideInInspector] public Rigidbody CubeRigidBody;
    [HideInInspector] public bool IsMainCube;

    private MeshRenderer CubeMeshRender;

    private void Awake()
    {
        CubeID = staticID++;
        CubeMeshRender = GetComponent<MeshRenderer>();
        CubeRigidBody = GetComponent<Rigidbody>();
    }
    public void SetColor(Color color)
    {
        CubeColor = color;
        CubeMeshRender.material.color = color;
    }

    public void SetCubeNumber( int number)
    {
        CubeNumber = number;
        for(int i = 0; i < 6; i++)
        {
            TxtNumbers[i].text = number.ToString();
        }
    }
   
}
