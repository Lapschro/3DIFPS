using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-100)]
public class CustomEventEmitter : MonoBehaviour
{
    public static CustomEventEmitter instance;

    //FMOD instances
    private FMOD.Studio.EventInstance backgroundMusic;
    private List<FMOD.Studio.EventInstance> instancesList = new List<FMOD.Studio.EventInstance>();
    //private FMOD.Studio.EventInstance[] instancesArray;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        // ChangeBackgroundMusic(FMODEvents.events[(int)FMODEvents.Music.SWEET]);
        //StartEventInstance(FMODEvents.events[(int)FMODEvents.Chars.CHLOE_SLIDING]);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            //ChangeBackgroundMusic(FMODEvents.events[(int)FMODEvents.Music.CHILLROOM]);
            // SetFMODGlobalParameter(FMODEvents.Parameters.GameState.ToString(), 1);
            StopInstance(0, true);
        }
    }

    //Toca o SFX passado em one-shot. Pode ser passada a posição do evento, se não for passada o evento tocará normalmente
    public void PlaySFXOneShot(string sfx, Vector3 position = default(Vector3))
    {
        FMODUnity.RuntimeManager.PlayOneShot(sfx, position != Vector3.zero ? position : transform.position);
    }

    public void SetFMODGlobalParameter(string parameter, float value)
    {
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName(parameter, value);
    }

    public void ChangeBackgroundMusic(string musicEvent)
    {
        backgroundMusic.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        backgroundMusic = FMODUnity.RuntimeManager.CreateInstance(musicEvent);
        backgroundMusic.start();
    }

    //Começa uma instância de áudio, toca a mesma e retorna a posição dela no array de instâncias
    public int StartEventInstance(string eventString)
    {
        FMOD.Studio.EventInstance eventInstance = FMODUnity.RuntimeManager.CreateInstance(eventString);
        // eventInstance.start();

        instancesList.Add(eventInstance);

        return instancesList.IndexOf(eventInstance);
    }

    public void PlayEventInstance(int eventIndex){
        instancesList[eventIndex].start();
    }

    //Para a instância no índice de array dado, e escolhe modo de parada
    public void StopInstance(int index, bool isFadeOut)
    {
        FMOD.Studio.STOP_MODE stopMode = FMOD.Studio.STOP_MODE.IMMEDIATE;

        if (isFadeOut)
        {
            stopMode = FMOD.Studio.STOP_MODE.ALLOWFADEOUT;
        }

        instancesList[index].stop(stopMode);
        instancesList.RemoveAt(index);

    }

    public void Set3DAttributes(int eventIndex, GameObject go){
        instancesList[eventIndex].set3DAttributes (FMODUnity.RuntimeUtils.To3DAttributes (go.transform));
    }

    //Toca um evento que segue o GameObject passado
    public int StartEventThatFollows(string eventString, GameObject go, Rigidbody rb){
        FMOD.Studio.EventInstance eventInstance = FMODUnity.RuntimeManager.CreateInstance(eventString);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject (eventInstance, go.transform,rb);
        // eventInstance.start();

        instancesList.Add(eventInstance);

        return instancesList.IndexOf(eventInstance);
    }

    public void SetLocalParameter(string parameter, float value, int eventIndex){
        instancesList[eventIndex].setParameterByName(parameter, value);
    }
}
