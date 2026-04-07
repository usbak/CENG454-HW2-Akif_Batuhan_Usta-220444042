using UnityEngine;

using UnityEngine;
using TMPro;

public class FlightExamManager : MonoBehaviour
{
    [Header("Ekran Yazilari")]
    [SerializeField] TMP_Text warningText;
    [SerializeField] TMP_Text missionText;

    [Header("Sesler")]
    [SerializeField] AudioSource alarmSfx;
    [SerializeField] AudioSource victorySfx;

    // gorev asamalari
    bool tookOff;
    bool zoneEntered;
    bool zoneSurvived;
    bool finished;

    void Awake()
    {
        if (warningText != null) warningText.text = string.Empty;
        RefreshMissionLabel();
    }

    public void OnTakeoff()
    {
        if (tookOff) return;
        tookOff = true;
        RefreshMissionLabel();
    }

    public void EnterDangerZone()
    {
        zoneEntered = true;

        if (warningText != null)
        {
            warningText.color = Color.red;
            warningText.text = "Entered a Dangerous Zone!";
        }

        if (alarmSfx != null && !alarmSfx.isPlaying)
            alarmSfx.Play();

        RefreshMissionLabel();
    }

    public void ExitDangerZone()
    {
        zoneSurvived = true;

        if (warningText != null)
        {
            warningText.color = Color.green;
            warningText.text = "Zone Cleared - Find the Landing Strip!";
        }

        RefreshMissionLabel();
    }

    public void OnMissileHit()
    {
        if (warningText != null)
        {
            warningText.color = Color.red;
            warningText.text = "HIT! Mission Failed - Resetting...";
        }

        // tehlike state'ini geri al, oyuncu bolgeyi tekrar gecmek zorunda
        zoneEntered = false;
        zoneSurvived = false;

        RefreshMissionLabel();
    }

    public void OnLanding()
    {
        // erken inisi engelle
        if (finished) return;
        if (!tookOff) return;
        if (!zoneSurvived) return;

        finished = true;

        if (warningText != null)
        {
            warningText.color = Color.green;
            warningText.text = "Mission Complete! Safe Landing!";
        }

        if (victorySfx != null) victorySfx.Play();

        RefreshMissionLabel();
    }

    public bool IsThreatCleared()  => zoneSurvived;
    public bool HasTakenOff()      => tookOff;
    public bool IsMissionComplete()=> finished;

    void RefreshMissionLabel()
    {
        if (missionText == null) return;

        string label;
        if (finished)            label = "Mission: COMPLETE!";
        else if (zoneSurvived)   label = "Mission: Land Safely";
        else if (zoneEntered)    label = "Mission: Survive the Threat!";
        else if (tookOff)        label = "Mission: Enter the Danger Zone";
        else                     label = "Mission: Take Off";

        missionText.text = label;
    }
}
