using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Gun : MonoBehaviour
{
    [SerializeField] private ParticleSystem _smoke;

    [SerializeField] private ParticleSystem _bulletCasing;

    [SerializeField] private ParticleSystem _muzzleFlashSide;

    [SerializeField] private ParticleSystem _Muzzle_Flash_Front;

    [SerializeField] private AudioClip _gunShotAudioClip;

    [SerializeField] private AudioSource _audioSource;

    [SerializeField] private XRSocketInteractor _socketInteractor;
    [SerializeField] private ConfigurableJoint _gunSlider;
    private GameObject _previousGunClip;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void FireGun()
    {
        _smoke.Play();
        _bulletCasing.Play();
        _muzzleFlashSide.Play();
        _Muzzle_Flash_Front.Play();
        _audioSource.pitch = Random.Range(0.9f, 1.1f);
        _audioSource.PlayOneShot(_gunShotAudioClip);
    }

    public void ChangeClipLayer()
    {
        IXRSelectInteractable gunClip = _socketInteractor.GetOldestInteractableSelected();
        if(gunClip != null)
        {
            gunClip.transform.gameObject.layer = LayerMask.NameToLayer("Gun Clip");
            _previousGunClip = gunClip.transform.gameObject;
        }
        else
        {
            _previousGunClip.layer = LayerMask.NameToLayer("Default");
        }
    }
}
