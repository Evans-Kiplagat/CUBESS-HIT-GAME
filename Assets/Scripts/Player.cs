
using UnityEngine;


public class Player : MonoBehaviour
{
    [SerializeField] private float MoveSpeed;
    [SerializeField] private float CubePosX;
    [SerializeField] private float PushForce;
    [Space]
    [SerializeField] private TouchHolder touchSlider;

     private CUbeId MainCube;

    private bool IsPionterDown;
    private Vector3 cubePos;

    
    void Start()
    {
        //TODO: Spawn new Object
        Spawncube();

        // Listen to slider events
        touchSlider.OnPionterDownEvent += OnPointerDown;
        touchSlider.OnPointerDragEvent += OnPointerDrag;
        touchSlider.OnPointerUpEvent += OnPointerUp;
        
    }
    private void Update()
    {
        if (IsPionterDown)
            MainCube.transform.position = Vector3.Lerp(MainCube.transform.position, cubePos, MoveSpeed * Time.deltaTime);
    }

    private void OnPointerDown()
    {
        IsPionterDown = true;
    }
    private void OnPointerDrag( float Xmovement )
    {
        if (IsPionterDown)
        {
            cubePos = MainCube.transform.position;
            cubePos.x = Xmovement * CubePosX;
        }
    }
    private void OnPointerUp()
    {if (IsPionterDown)
            IsPionterDown = false;

        //  push the cube
        MainCube.CubeRigidBody.AddForce(Vector3.forward * PushForce, ForceMode.Impulse);

        // TODo:Spawn A new cube after .3 s
        Invoke("SpawnNewCube", 0.3f);

    }

    private void SpawnNewCube()
    {
        MainCube.IsMainCube = false;
        Spawncube();
    }
    private void Spawncube()
    {
        MainCube = CubeSpawner.Instance.SpawnRandom();
        MainCube.IsMainCube = true;
        //reset cube
        cubePos = MainCube.transform.position;
    }

    void OnDestory()
    {
        // remove all listener

        touchSlider.OnPionterDownEvent -= OnPointerDown;
        touchSlider.OnPointerDragEvent -= OnPointerDrag;
        touchSlider.OnPointerUpEvent -= OnPointerUp;


    }
}
