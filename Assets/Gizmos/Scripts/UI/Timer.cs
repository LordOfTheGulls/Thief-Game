using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [Header("Timer Settings")]
    public int AnimDelaySec;
    public int[] TimeCounter;

    [Header("Timer Dependancies")]
    public Text TimerCounter;
    public Image ClockPointer;
    public Animation PointerObject;

    private float Counter = 0, TotalTime = 0;
    private string CurrentTime, PointerAnimationName;
    private bool isTimerActive, isTimerSet, isColorSet;

    private void Awake(){PointerObject = PointerObject.GetComponent<Animation>();}

    private void Start()
    {
        PointerAnimationName = PointerObject.clip.name;

        TotalTime = TimeCounter[0] * 60 + TimeCounter[1];
        Counter = TotalTime;

        isTimerActive = true;
    }

    private void FixedUpdate() { RunTimer(true); }

    private void RunTimer(bool isTimerOn)
    {

        //[SET THE TIMER TO THE INITIAL STATE AND RUN -> THE TIMER ANIMATION]
        if (isTimerOn == true && isTimerSet == false){

            TimerCounter.color = Color.white;    Counter = TotalTime;      isTimerSet = true;      RunTimerAnimation(true);     
        }

        //[UPDATE AND SET THE TIMER]
        if (isTimerOn == true)
        {
            if (Counter > 0)
            {

                //[SET THE TIMER COLOR TO RED WHEN THE COUNTER IS <= 20% OF THE TOTAL TIME]
                if (Counter <= (TotalTime * 0.2f) && isColorSet == false)
                {
                    TimerCounter.color = Color.red; isColorSet = true;
                }

                //[UPDATE AND SET THE TIMER]
                CurrentTime = ((Counter -= Time.deltaTime) / 60).ToString("00 ") + ":" + (Counter % 60).ToString(" 00");
                TimerCounter.text = CurrentTime;
            }
            else
            {

                //[SET THE TIMER AND REWIND THE ANIMATION -> WHEN OUT OF TIME]
                TimerCounter.text = "00 : 00";  isTimerActive = isTimerOn = false;      RunTimerAnimation(false);     
            }
        }
    }

    //[RUN THE ANIMATION WHEN THE TIMER IS RUNNING OR STOP IF NOT]
    private void RunTimerAnimation(bool isAnimationActive)
    {
        if (isAnimationActive == true)
        {
            PointerObject[PointerAnimationName].speed = 1f / AnimDelaySec;
            PointerObject.Play(PointerAnimationName);
        }
        else
        {
            PointerObject.Rewind();
        }
    }
}
