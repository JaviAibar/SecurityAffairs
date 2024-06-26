using Zenject;
using NUnit.Framework;
using SecurityAffairs;
using UnityEngine;
using UnityEditor;

[TestFixture]
public class TestClockAudioManager : ZenjectUnitTestFixture
{
    ClockAudioManager clockAudioManager;
    AudioSource audioSource1;
    AudioSource audioSource2;

    [SetUp]
    public void CommonInstall()
    {
        var audioManagerGO = new GameObject();

        audioSource1 = audioManagerGO.AddComponent<AudioSource>();
        audioSource1.clip = AudioClip.Create("clip1", 1, 1, 1000, false);

        audioSource2 = audioManagerGO.AddComponent<AudioSource>();
        audioSource2.clip = AudioClip.Create("clip2", 1, 1, 1000, false);

        clockAudioManager = audioManagerGO.AddComponent<ClockAudioManager>();

        var so = new SerializedObject(clockAudioManager);
        so.FindProperty("_clockAudioSource").objectReferenceValue = audioSource1;
        so.FindProperty("_thunderAudioSource").objectReferenceValue = audioSource2;
        so.ApplyModifiedProperties();
        
        Object.Instantiate(audioManagerGO);
        Container.Bind<AudioService>().AsTransient();
        
        Container.Bind<ClockAudioManager>().FromInstance(clockAudioManager).AsSingle();
    }

    [Test]
    public void _00_BothAudioSourcesPlay()
    {
        Assert.IsFalse(audioSource1.isPlaying); 
        Assert.IsFalse(audioSource2.isPlaying);
        var clockAudioManager = Container.Resolve<ClockAudioManager>();
        clockAudioManager.PlayClock();

        Assert.IsTrue(audioSource1.isPlaying);
        Assert.IsFalse(audioSource2.isPlaying);

        clockAudioManager.PlayThunder();

        Assert.IsTrue(audioSource1.isPlaying);
        Assert.IsTrue(audioSource2.isPlaying);


        clockAudioManager.StopThunder();

        Assert.IsTrue(audioSource1.isPlaying);
        Assert.IsFalse(audioSource2.isPlaying);
    }
}