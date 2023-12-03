using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    [SerializeField] WordBubble wordBubble;
    [SerializeField] ParticleSystem P_Particle;
    [SerializeField] ParticleSystem G_Particle;
    [SerializeField] ParticleSystem M_Particle;
    [SerializeField] EmoteAnimate emote;

    public FlatEffect[] flatEffects;
    Vector3 endPos;
    public void EffectInit()
    {
        emote.EmoteInit(GameManager.Instance.currentStage);
        wordBubble.WordInit(GameManager.Instance.currentStage);
    }
    public void PlayPerfectEffect()
    {
        emote.EmoteSetting(0);
        emote.StartEmoteAnimation();
        P_Particle.Play();
    }
    public void PlayGoodEffect()
    {
        emote.EmoteSetting(0);
        emote.StartEmoteAnimation();
        G_Particle.Play();
    }
    public void PlayMissEffect()
    {
        emote.EmoteSetting(1);
        emote.StartEmoteAnimation();   
        M_Particle.Play();  
    }
    public void PlayWarningEffect()
    {
        emote.EmoteSetting(2);
        emote.StartEmoteAnimation();
        wordBubble.SpeechStart();
        M_Particle.Play();
    }
    public void SetEndPos()
    {
        endPos =  new Vector3(Random.Range(-500f, 500f), Random.Range(200f, 500f), 0);
    }
    public Vector3 GetEndPos()
    {
        return endPos;
    }
}
