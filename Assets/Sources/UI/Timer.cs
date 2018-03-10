using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    public Text TimerCounter;
    public Image ClockPointer;

    public float AnimSecDelay;
    public Animation PointerObject;

    public int Minutes;
    public int Seconds;

    private float Counter = 0;

    private void Start(){
        string PointerAnimation = PointerObject.clip.name;

        PointerObject = PointerObject.GetComponent<Animation>();
        PointerObject.Play(PointerAnimation);

        PointerObject[PointerAnimation].speed = 1 / AnimSecDelay;

        Counter = Minutes * 60 + Seconds + 1;
    }
    private void FixedUpdate(){
        TimerCounter.text = Mathf.Floor((Counter -= Time.deltaTime) / 60).ToString("00 ") + ":" + Mathf.Floor(Counter % 60).ToString(" 00");
    }

}
