using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class ScreenFaderMixerBehaviour : PlayableBehaviour
{
    Color m_DefaultColor;
    float m_Saturation;

    Image m_TrackBinding;
    bool m_FirstFrameHappened;

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        m_TrackBinding = playerData as Image;

        if (m_TrackBinding == null)
            return;

        if (!m_FirstFrameHappened)
        {
            m_DefaultColor = m_TrackBinding.color;
            if (m_TrackBinding.material.HasFloat("_Saturation"))
            {
                m_Saturation = m_TrackBinding.material.GetFloat("_Saturation");
            }
            m_FirstFrameHappened = true;
        }

        int inputCount = playable.GetInputCount ();
        Color blendedColor = Color.clear;
        float blendedSaturation = 0;
        float totalWeight = 0f;
        float greatestWeight = 0f;
        int currentInputs = 0;

        for (int i = 0; i < inputCount; i++)
        {
            float inputWeight = playable.GetInputWeight(i);
            ScriptPlayable<ScreenFaderBehaviour> inputPlayable = (ScriptPlayable<ScreenFaderBehaviour>)playable.GetInput(i);
            ScreenFaderBehaviour input = inputPlayable.GetBehaviour ();
            
            blendedColor += input.color * inputWeight;
            blendedSaturation+= input.saturation * inputWeight;
            totalWeight += inputWeight;

            if (inputWeight > greatestWeight)
            {
                greatestWeight = inputWeight;
            }

            if (!Mathf.Approximately (inputWeight, 0f))
                currentInputs++;
        }

        m_TrackBinding.color = blendedColor + m_DefaultColor * (1f - totalWeight);
        if (m_TrackBinding.material.HasFloat("_Saturation"))
        {
            m_TrackBinding.material.SetFloat("_Saturation", blendedSaturation + m_Saturation * (1f - totalWeight));
        }
    }

    public override void OnPlayableDestroy (Playable playable)
    {
        m_FirstFrameHappened = false;

        if (m_TrackBinding == null)
            return;

        m_TrackBinding.color = m_DefaultColor;
        if (m_TrackBinding.material.HasFloat("_Saturation"))
        {
            m_TrackBinding.material.SetFloat("_Saturation", m_Saturation);
        }
    }
}
