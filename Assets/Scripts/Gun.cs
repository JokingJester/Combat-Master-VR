using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Gun : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _gunShot;
    [SerializeField] private AudioClip _insertMag;
    [SerializeField] private AudioClip _outOfAmmo;
    [SerializeField] private AudioClip _gunSlideSound;

    [Header("Particles")]
    [SerializeField] private ParticleSystem _smoke;
    [SerializeField] private ParticleSystem _bulletCasing;
    [SerializeField] private ParticleSystem _muzzleFlashSide;
    [SerializeField] private ParticleSystem _Muzzle_Flash_Front;

    [Header("Gun Slider")]
    [SerializeField] private Transform _gunSlider;
    [SerializeField] private Transform _sliderTarget;
    [Tooltip("When the distance from the gun slider and slider target reaches this value, it will register that you pulled back the slider")]
    [SerializeField] private float _distanceThreshold;

    [Header("Socket Interactor")]
    [SerializeField] private XRSocketInteractor _socketInteractor;

    private bool _hasGunClip;
    private bool _playedSlideBackSound;
    private bool _pulledBackSlider;

    private GunClip _gunClip;

    private GameObject _previousGunClip;

    void Update()
    {
        CheckGunSliderThreshold();
    }

    public void FireGun()
    {
        if(_gunClip != null)
        {
            if (_gunClip.ammoCount == 0 || _pulledBackSlider == false || _hasGunClip == false)
            {
                PlaySoundEffet(_outOfAmmo, 1);
            }
            else
            {
                _gunClip.ammoCount--;
                _smoke.Play();
                _bulletCasing.Play();
                _muzzleFlashSide.Play();
                _Muzzle_Flash_Front.Play();
                PlaySoundEffet(_gunShot, Random.Range(0.9f, 1.1f));
            }
        }
        else
            PlaySoundEffet(_outOfAmmo, 1);
    }

    public void AddOrRemoveGunClip()
    {
        IXRSelectInteractable gunClip = _socketInteractor.GetOldestInteractableSelected();
        if(gunClip != null)
        {
            PlaySoundEffet(_insertMag, 1);
            gunClip.transform.gameObject.layer = LayerMask.NameToLayer("Gun Clip");
            _hasGunClip = true;
            _pulledBackSlider = false;
            GunClip pistolClip = gunClip.transform.gameObject.GetComponent<GunClip>();
            if (pistolClip != null)
                _gunClip = pistolClip;
            _previousGunClip = gunClip.transform.gameObject;
        }
        else
        {
            PlaySoundEffet(_insertMag, 0.5f);
            _hasGunClip = false;
            _previousGunClip.layer = LayerMask.NameToLayer("Default");
        }
    }

    public void PlaySoundEffet(AudioClip clip, float pitch)
    {
        _audioSource.PlayOneShot(clip);
        _audioSource.pitch = pitch;
    }

    public void CheckGunSliderThreshold()
    {
        float distance = Vector3.Distance(_gunSlider.position, _sliderTarget.position);
        if (distance < _distanceThreshold)
        {
            if (_playedSlideBackSound == false)
            {
                _pulledBackSlider = true;
                _playedSlideBackSound = true;
                PlaySoundEffet(_gunSlideSound, 1.9f);
            }
        }
        else if (distance > 0.13f)
        {
            if (_playedSlideBackSound == true)
            {
                PlaySoundEffet(_gunSlideSound, 1.5f);
                _playedSlideBackSound = false;
            }
        }
    }
}
