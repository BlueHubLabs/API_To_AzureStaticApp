using API.DataAccess.Interfaces;
using API.DataAccess.Repository;
using API.Services.Config;
using API.Services.Interfaces;
using API.Services.Services;

namespace WebApplication2
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services

                // Repositories
                .AddScoped<ICityMasterRepository, CityMasterRepository>()
                .AddScoped<ICountryMasterRepository, CountryMasterRepository>()
                .AddScoped<IEmailScheduleRepository, EmailScheduleRepository>()
                .AddScoped<IFundRepository, FundRepository>()
                .AddScoped<IQuestionAnswerRepository, QuestionAnswerRepository>()
                .AddScoped<IQuestionBankRepository, QuestionBankRepository>()
                .AddScoped<IStateMasterRepository, StateMasterRepository>()
                .AddScoped<IUserBankDetailsRepository, UserBankDetailsRepository>()
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<IUserProfileRepository, UserProfileRepository>()
                .AddScoped<IUserRoleRepository, UserRoleRepository>()
                .AddScoped<IFundSubscriptionRepository, FundSubscriptionRepository>()
                .AddScoped<IMasterDataRepository, MasterDataRepository>()
                .AddScoped<IAzureBlobStorageActionsRepository, AzureBlobStorageActionsRepository>()


                // Services
                .AddScoped<ICountryMasterService, CountryMasterService>()
                .AddScoped<IEmailService, EmailService>()
                .AddScoped<IFundService, FundService>()
                .AddScoped<IQuestionBankService, QuestionBankService>()
                .AddScoped<IStateCityService, StateCityService>()
                .AddScoped<IUserBankDetailsService, UserBankDetailsService>()
                .AddScoped<IUserService, UserService>()
                .AddScoped<IUserProfileService, UserProfileService>()
                .AddScoped<IUserRoleService, UserRoleService>()
                .AddScoped<IFundSubscriptionService, FundSubscriptionService>()
                .AddScoped<IMasterDataService, MasterDataService>()

                .Configure<AppSettings>(Configuration.GetSection("AppSettings"))

                .AddCors(options =>
                {
                    options.AddPolicy("AllowOrigin", builder =>
                    {
                        builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                    });
                });
        }

        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            app.UseStaticFiles();
            app.UseCors("AllowOrigin");
            app.UseSwagger();
            app.UseSwaggerUI();
        }
    }
}
