using Core.Makeup;
using Core.Makeup.Settings;
using Core.Makeup.View;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private MakeupStepStaticSettings makeupStepStaticSettings;
        [SerializeField] private MakeupWindowReferences makeupWindowReferences;
        [SerializeField] private HandPresentation handPresentation;
        [SerializeField] private FaceMakeupRenderer faceMakeupRenderer;
        [SerializeField] private FaceZoneChecker faceZoneChecker;

        public override void InstallBindings()
        {
            // Data
            Container.BindInstance(makeupStepStaticSettings).AsSingle();
            Container.BindInstance(makeupWindowReferences).AsSingle();

            // Providers
            Container.BindInterfacesAndSelfTo<MakeupStepResolver>().AsSingle();
            
            // View + Renderer
            Container.Bind<IHandPresentation>().FromInstance(handPresentation).AsSingle();
            Container.Bind<IMakeupResultRenderer>().FromInstance(faceMakeupRenderer).AsSingle();
            Container.Bind<IFaceZoneChecker>().FromInstance(faceZoneChecker).AsSingle();

            // Flow
            Container.BindInterfacesAndSelfTo<MakeupSequenceController>().AsSingle();
        }
    }
}