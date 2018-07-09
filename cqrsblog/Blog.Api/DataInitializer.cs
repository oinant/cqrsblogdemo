using System.Linq;
using Autofac;
using Blog.Domain.UseCases;
using Blog.Query.DB;
using MediatR;

namespace Blog.Api
{
    internal static class DataInitializer
    {
        internal static void InitData(IContainer applicationContainer)
        {
            var mediator = applicationContainer.Resolve<IMediator>();
            var queryDb = applicationContainer.Resolve<InMemoryQueryDB>();

            mediator.Publish(new StartPostCommand("Vincent BOURDON",
                "Obtenir un site performant avec Accelerated Mobile Page, Progressive Web App et un content delivery network (PART-1 AMP)"))
                .Wait();

            var post1 = queryDb.GetAllPostSummaries().First();
            mediator.Publish(new ChangeTitleCommand(post1.PostId, post1.Author, post1.Title + " [rev1]"))
                .Wait();


            mediator.Publish(new StartPostCommand("Joel PINTO RIBEIRO",
                    "À la découverte du nouveau type «Span» introduit dans C# 7.2"))
                .Wait();
        }
    }
}