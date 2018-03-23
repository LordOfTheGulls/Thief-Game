using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogManager : MonoBehaviour {

    public float FadeDuration;

    public GameObject[] FloorBounds;
    public Material[] FloorMaterials;

    public Color TargetColor;

    private Color[] InitialFloorColor;

    public Camera Camera;

    private int Iteration;

    private float LastUpperBound;
    private float LastLowerBound;

    private void Start () {
        InitialFloorColor = new Color[FloorMaterials.Length];
        Iteration = 0;

        SetInitialFog();
    }
	private void Update () {
 
        float CameraPosY = Camera.transform.localPosition.y;

            foreach (GameObject Obj in FloorBounds)
            {
                float UpperBound = Obj.transform.GetChild(0).transform.position.y;
                float LowerBound = Obj.transform.GetChild(1).transform.position.y;

                if (CameraPosY <= UpperBound && CameraPosY >= LowerBound){
                    SetColor(InitialFloorColor[Iteration], Color.white, Iteration);
                }
                else{
                    SetColor(InitialFloorColor[Iteration], TargetColor, Iteration);
                }

                Iteration++;
            }

            Iteration = 0;
    }
    private void SetInitialFog()
    {
        foreach (Material Mat in FloorMaterials){Mat.color = TargetColor;}
    }
    private void SetColor(Color InitialColor, Color TargetColor, int Index)
    {
        InitialFloorColor[Iteration] = FloorMaterials[Iteration].color;
        StartCoroutine(ColorTransition(InitialFloorColor[Iteration], TargetColor, Iteration));
        FloorMaterials[Iteration].color = InitialFloorColor[Iteration];
    }

    IEnumerator ColorTransition(Color InitialColor, Color TargetColor, int Index)
    {
       InitialFloorColor[Index] = Color.Lerp(InitialColor, TargetColor, FadeDuration * Time.deltaTime);
       yield return null;
    }
}
