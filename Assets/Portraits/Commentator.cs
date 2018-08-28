using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(AudioSource))]
public class Commentator : MonoBehaviour {

    [System.Serializable]
    public class VoiceLine
    {
        [TextArea]
        public string subtitles = "-Insert-Subtitles-Here-";
        public Audio audio;
        public SpriteSheet spriteSheet;

    }
    [System.Serializable]
    public class SpriteSheet
    {
        public Sprite[] sprites;
        public int index = 0;
        public float cycleSpeed = 0.01f;
        public float cycleTimer = 0f;
        public bool animationFinished = false;
    }
    [System.Serializable]
    public class Audio
    {
        public bool audioStarted = false;
        public AudioClip audioClip;
        public bool audioFinished = false;
    }

    public VoiceLine[] positiveReactions;
    public VoiceLine[] negativeReactions;
    public VoiceLine currentReaction;
    public VoiceLine[] passiveReactions;
    public float timeUntilPassiveReaction;
    public float passiveReactionTimer;
    public bool reacting;

    public Text subtitleField;
    public Image portrait;

    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void ReactPositively()
    {
        if (!reacting)
        ChooseAReaction(positiveReactions);
    }

    public void ReactNegatively()
    {
        if (!reacting)
        ChooseAReaction(negativeReactions);
    }

    void ChooseAReaction(VoiceLine[] vls)
    {
        currentReaction = vls[Random.Range(0, vls.Length)];
        passiveReactionTimer = timeUntilPassiveReaction;
        reacting = true;
    }

    private void Update()
    {
        if (reacting && currentReaction != null)
        {
            PlayAudio();
            CycleSpriteSheet();
            DisplaySubtitles();
            CheckFinished();
        }
        else if (currentReaction == null)
        {
            reacting = false;
        }
        else if (!reacting)
        {
            if (passiveReactionTimer < 0)
            {
                ChooseAReaction(passiveReactions);
            }
            else
            {
                passiveReactionTimer -= Time.deltaTime;
            }
        }
    }
    void PlayAudio()
    {
        if (currentReaction.audio.audioClip != null)
        {
            if (!audioSource.isPlaying && !currentReaction.audio.audioStarted && !currentReaction.audio.audioFinished)
            {
                currentReaction.audio.audioStarted = true;
                audioSource.clip = currentReaction.audio.audioClip;
                audioSource.Play();
            }
            else if (!audioSource.isPlaying && currentReaction.audio.audioStarted)
            {
                currentReaction.audio.audioFinished = true;
                currentReaction.audio.audioStarted = false;
            }
        }
        else
        {
            currentReaction.audio.audioFinished = true;
        }

    }
    void DisplaySubtitles()
    {
        if (subtitleField.text != currentReaction.subtitles)
            subtitleField.text = currentReaction.subtitles;

        if (currentReaction.audio.audioClip != null)
        { 
            if (currentReaction.audio.audioFinished)
            {
                subtitleField.text = "";
            }
        }
        else if (currentReaction.spriteSheet.sprites.Length > 0)
        {
            if (currentReaction.spriteSheet.animationFinished)
            subtitleField.text = "";
        }
    }
    void CycleSpriteSheet()
    {
        portrait.sprite = currentReaction.spriteSheet.sprites[currentReaction.spriteSheet.index];
        if (currentReaction.spriteSheet.cycleTimer <= 0 && !currentReaction.spriteSheet.animationFinished)
        {
            
            if (currentReaction.spriteSheet.index + 1 < currentReaction.spriteSheet.sprites.Length)
            {
                currentReaction.spriteSheet.index++;
            }
            else
            {
                currentReaction.spriteSheet.animationFinished = true;
            }
            
            currentReaction.spriteSheet.cycleTimer = currentReaction.spriteSheet.cycleSpeed;
        }
        else if (currentReaction.spriteSheet.cycleTimer >0 && !currentReaction.spriteSheet.animationFinished)
        {
            currentReaction.spriteSheet.cycleTimer -= Time.deltaTime;
        }
    }
    void CheckFinished()
    {
        if (currentReaction.spriteSheet.animationFinished && currentReaction.audio.audioFinished)
        {
            reacting = false;
            currentReaction.spriteSheet.animationFinished = false;
            currentReaction.spriteSheet.index = 0;

            currentReaction.audio.audioFinished = false;
        }
    }
}
