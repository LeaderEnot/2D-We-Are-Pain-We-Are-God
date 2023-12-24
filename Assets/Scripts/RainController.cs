using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainController : MonoBehaviour
{
    public ParticleSystem rainParticleSystem;
    public AudioSource rainAudioSource; // Add an AudioSource component for rain sound.
    public float stopTime = 2.0f; // Time in seconds to gradually stop the rain.
    public Animator characterAnimator; // Reference to the Animator component.

    private float toggleTimer;
    private float initialEmissionRate;
    private float initialVolume;

    private void Start()
    {
        // Ensure the Particle System is initially playing.
        rainParticleSystem.Play();
        initialEmissionRate = rainParticleSystem.emission.rateOverTime.constant;

        // Save the initial volume of the rain audio.
        initialVolume = rainAudioSource.volume;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (toggleTimer <= 0)
            {
                // If toggleTimer is not active, start the gradual rain stop.
                toggleTimer = stopTime;

                // Set the "isRain" parameter to true in the Animator.
                characterAnimator.SetBool("isRain", true);

                // Gradually reduce the audio volume over time.
                StartCoroutine(FadeOutAudio(stopTime));
            }
            else
            {
                // If toggleTimer is active, restart the rain.
                toggleTimer = 0;
                rainParticleSystem.Play();

                // Set the "isRain" parameter to false in the Animator.
                characterAnimator.SetBool("isRain", false);

                // Gradually increase the audio volume over time.
                StartCoroutine(FadeInAudio(stopTime));
            }
        }

        if (toggleTimer > 0)
        {
            // Gradually reduce the emission rate over time.
            var emission = rainParticleSystem.emission;
            emission.rateOverTime = Mathf.Lerp(0, initialEmissionRate, 1 - (toggleTimer / stopTime));
            toggleTimer -= Time.deltaTime;

            if (toggleTimer <= 0)
            {
                // Stop the rain when the emission rate reaches zero.
                rainParticleSystem.Stop();

                // Set the "isRain" parameter to false in the Animator.
                characterAnimator.SetBool("isRain", false);
            }
        }
    }

    IEnumerator FadeOutAudio(float duration)
    {
        float timer = 0;
        while (timer < duration)
        {
            rainAudioSource.volume = Mathf.Lerp(initialVolume, 0, timer / duration);
            timer += Time.deltaTime;
            yield return null;
        }
        rainAudioSource.volume = 0;
        rainAudioSource.Stop();
    }

    IEnumerator FadeInAudio(float duration)
    {
        float timer = 0;
        rainAudioSource.Play();
        while (timer < duration)
        {
            rainAudioSource.volume = Mathf.Lerp(0, initialVolume, timer / duration);
            timer += Time.deltaTime;
            yield return null;
        }
        rainAudioSource.volume = initialVolume;
    }
}
