using IconsApp.Services;
using Unity;

namespace IconsApp
{
    public class IconsAppBootstrapper : Bootsrtrapper
    {
        public override void Run() => (Container.Resolve<MainWindow>()).Show();

        protected override void OnInitialize()
        {
            Container.RegisterType<IIconService, IconService>();
            Container.RegisterType<IOpenFileService, OpenFileService>();
            Container.RegisterType<ISaveFileService, SaveFileService>();
            Container.RegisterType<ISaveImageService, SavePngService>();
            Container.RegisterType<ISaveImageService, SaveJpegService>();
            Container.RegisterType<ISaveImageService, SaveBmpService>();
        }
    }
}
