using BunkerGameComponetns.Editor.View;

namespace BunkerGameComponetns.Editor
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(CatastropheDetails), typeof(CatastropheDetails));
            Routing.RegisterRoute(nameof(BunkerWallDetails), typeof(BunkerWallDetails));
            Routing.RegisterRoute(nameof(CharacterItemDetails), typeof(CharacterItemDetails));
            Routing.RegisterRoute(nameof(AdditionalInformationDetails), typeof(AdditionalInformationDetails));
            Routing.RegisterRoute(nameof(ProfessionDetails), typeof(ProfessionDetails));
            Routing.RegisterRoute(nameof(BunkerEnviromentDetails), typeof(BunkerEnviromentDetails));
            Routing.RegisterRoute(nameof(BunkerObjectDetails), typeof(BunkerObjectDetails));
            Routing.RegisterRoute(nameof(BunkerItemDetails), typeof(BunkerItemDetails));
            Routing.RegisterRoute(nameof(PhobiaDetails), typeof(PhobiaDetails));
            Routing.RegisterRoute(nameof(CardDetails), typeof(CardDetails));
            Routing.RegisterRoute(nameof(HobbyDetails), typeof(HobbyDetails));
            Routing.RegisterRoute(nameof(HealthDetails), typeof(HealthDetails));
            Routing.RegisterRoute(nameof(TraitDetails), typeof(TraitDetails));
            Routing.RegisterRoute(nameof(ExternalSurroundingDetails), typeof(ExternalSurroundingDetails));
        }
    }
}