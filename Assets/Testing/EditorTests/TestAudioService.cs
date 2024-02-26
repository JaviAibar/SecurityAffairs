using Zenject;
using NUnit.Framework;
using SecurityAffairs;

[TestFixture]
public class TestAudioService : ZenjectUnitTestFixture
{
    [SetUp]
    public void CommonInstall()
    {
        Container.Bind<AudioService>().AsTransient();
    }

    [Test]
    public void RunTest1()
    {
        // TODO
    }
}