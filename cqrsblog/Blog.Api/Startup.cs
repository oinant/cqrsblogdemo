using System;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Blog.Domain.Drivers;
using Blog.Domain.FunctionalCore;
using Blog.Infrastructure;
using Blog.Query;
using Blog.Query.DB;
using MediatR;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Blog.Api
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }

        public IContainer ApplicationContainer { get; private set; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddEnvironmentVariables();

            Configuration = builder.Build();

            
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.Converters.Add(new StringEnumConverter());
            });

            services.AddCors();
            
            var containerBuilder = new ContainerBuilder();
            containerBuilder.Populate(services);
            containerBuilder.ConfigureIocContainer()
                .ConfigureMediatR();
            ApplicationContainer = containerBuilder.Build();

            DataInitializer.InitData(ApplicationContainer);

            return new AutofacServiceProvider(ApplicationContainer);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }

    }

    internal static class ContainerBuilderIocExtensions
    {
        public static ContainerBuilder ConfigureIocContainer(this ContainerBuilder populatedContainerBuilder)
        {
            var db = DbInitializer.Init();
            var repo = new PostRepository();
            populatedContainerBuilder.RegisterInstance<InMemoryQueryDB>(db);
            populatedContainerBuilder.RegisterInstance<PostRepository>(repo).As<IPostRepository>();

            return populatedContainerBuilder;
        }

        public static ContainerBuilder ConfigureMediatR(this ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly).AsImplementedInterfaces();

            var mediatrOpenTypes = new[]
            {
                typeof(IRequestHandler<,>),
                typeof(IRequestHandler<>),

                typeof(INotificationHandler<>),
            };

            var assembliesToScan = new[]
            {
                typeof(Post).GetTypeInfo().Assembly,
                typeof(PostStartedEventHandler).GetTypeInfo().Assembly
            };

            foreach (var mediatrOpenType in mediatrOpenTypes)
            {
                foreach (var assembly in assembliesToScan)
                {
                    builder
                    .RegisterAssemblyTypes(assembly)
                    .AsClosedTypesOf(mediatrOpenType)
                    .AsImplementedInterfaces();
                }
            }


            // It appears Autofac returns the last registered types first
            builder.RegisterGeneric(typeof(RequestPostProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            builder.RegisterGeneric(typeof(RequestPreProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));


            builder.Register<ServiceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });

            return builder;
        }
    }
}
