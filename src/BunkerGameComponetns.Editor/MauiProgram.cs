using BunkerGameComponents.Infrastructure.Database.GameComponentContext;
using BunkerGameComponents.Infrastructure.Domain;
using BunkerGameComponents.Infrastructure.UnitOfWork;
using BunkerGameComponetns.Editor.Services;
using BunkerGameComponetns.Editor.View;
using BunkerGameComponetns.Editor.ViewModel;
using BunkerGameComponetns.Editor.ViewModel.DetailsModels;

namespace BunkerGameComponetns.Editor
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            builder.Services.AddScoped(c => new GameComponentJsonContext(FileSystem.AppDataDirectory));
            builder.Services.AddScoped<IGameComponentsRepository, GameComponentsRepositoryJson>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWorkJson>();
            builder.Services.AddScoped<GameComponentsService>();
            ConfigureViewModels(builder.Services);
            ConfigureViews(builder.Services);
            return builder.Build();
        }
        public static void ConfigureViews(IServiceCollection services)
        {
            services.AddSingleton<GameComponentsViewModel>();
            services.AddSingleton<MainPage>();
            services.AddTransient<CatastropheDetails>();
            services.AddTransient<BunkerWallDetails>();
            services.AddTransient<CharacterItemDetails>();
            services.AddTransient<AdditionalInformationDetails>();
            services.AddTransient<ProfessionDetails>();
            services.AddTransient<BunkerEnviromentDetails>();
            services.AddTransient<BunkerObjectDetails>();
            services.AddTransient<BunkerItemDetails>();
            services.AddTransient<PhobiaDetails>();
            services.AddTransient<CardDetails>();
            services.AddTransient<HobbyDetails>();
            services.AddTransient<HealthDetails>();
            services.AddTransient<TraitDetails>();
            services.AddTransient<ExternalSurroundingDetails>();
        }
        public static void ConfigureViewModels(IServiceCollection services)
        {
            services.AddTransient<CatastropheDetailsViewModel>();
            services.AddTransient<BunkerWallDetailsViewModel>();
            services.AddTransient<AdditionalInformationDetailsViewModel>();
            services.AddTransient<ProfessionDetailsViewModel>();
            services.AddTransient<CharacterItemDetailsViewModel>();
            services.AddTransient<BunkerEnviromentDetailsViewModel>();
            services.AddTransient<BunkerObjectDetailsViewModel>();
            services.AddTransient<BunkerItemDetailsViewModel>();
            services.AddTransient<PhobiaDetailsViewModel>();
            services.AddTransient<CardDetailsViewModel>();
            services.AddTransient<HobbyDetailsViewModel>();
            services.AddTransient<HealthDetailsViewModel>();
            services.AddTransient<TraitDetailsViewModel>();
            services.AddTransient<ExternalSurroundingDetailsViewModel>();
        }
    }
}