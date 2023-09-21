
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business.Abstract;
using Business.Concrete;
using Business.DepencenyResolvers.Autofac;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;

namespace WebAPI
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			//Autofac, Ninject,CastleWindsor,StructureMap,LightInect,DryInject -->IoC Container Altyap�s� sunarlar.
			//AOP -- Autofac Aop imkan� sunuyor.
			//Postsharp 

			builder.Services.AddControllers();
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();
			//builder.Services.AddSingleton<IProductService,ProductManager>();
			//builder.Services.AddSingleton<IProductDal,EfProductDal>();
			
			//Farkl� bir IoC ortam� kullanmak istiyorsak <Autofac> bu syntax � kullan�r�z.
			builder.Host.UseServiceProviderFactory(services => new AutofacServiceProviderFactory())
				.ConfigureContainer<ContainerBuilder>
				(builder => { builder.RegisterModule(new AutofacBusinessModule()); });
			
			var app = builder.Build();


			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}