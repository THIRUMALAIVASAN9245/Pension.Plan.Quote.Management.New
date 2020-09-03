namespace Quote.Servive.API
{
    using AutoMapper;
    using FluentValidation;
    using FluentValidation.AspNetCore;
    using MediatR;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Quote.Service.API.Data.Database;
    using Quote.Service.API.Data.Repository;
    using Quote.Service.API.Domain.Entities;
    using Quote.Service.API.Models.ResquestModels;
    using Quote.Service.API.Service.Handlers.CommandHandlers;
    using Quote.Service.API.Service.Handlers.QueryHandlers;
    using Quote.Service.API.Service.RequestHandlers.CommandHandlers;
    using Quote.Service.API.Service.RequestHandlers.QueryHandlers;
    using Quote.Service.API.Validators;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Reflection;
    using System.Text.Json.Serialization;

    ///<Summary>
    /// Startup class
    ///</Summary>
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        ///<Summary>
        /// Startup class constructor
        ///</Summary>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        ///<Summary>
        /// Configuration
        ///</Summary>
        public IConfiguration Configuration { get; }

        ///<Summary>
        /// ConfigureServices method
        ///</Summary>
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson()
                .AddJsonOptions(options =>
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });

            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddAutoMapper(typeof(Startup));

            string defaultConnectionString = Environment.GetEnvironmentVariable("QuoteInfoDb") ??
                                            Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<QuoteDbContext>(options => options.UseSqlServer(defaultConnectionString));

            services.AddSwaggerGen();

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var actionExecutingContext =
                        actionContext as ActionExecutingContext;

                    if (actionContext.ModelState.ErrorCount > 0
                        && actionExecutingContext?.ActionArguments.Count == actionContext.ActionDescriptor.Parameters.Count)
                    {
                        return new UnprocessableEntityObjectResult(actionContext.ModelState);
                    }

                    return new BadRequestObjectResult(actionContext.ModelState);
                };
            });

            services.AddScoped<IRepository, Repository>();

            services.AddMvc().AddFluentValidation();

            services.AddTransient<IValidator<CreateQuoteModel>, CreateQuoteModelValidator>();
            services.AddTransient<IValidator<UpdateQuoteModel>, UpdateQuoteModelValidator>();

            services.AddTransient<IRequestHandler<CreateQuoteRequest, Quote>, CreateQuoteHandler>();
            services.AddTransient<IRequestHandler<DeleteQuoteRequest, Quote>, DeleteQuoteHandler>();
            services.AddTransient<IRequestHandler<UpdateQuoteRequest, Quote>, UpdateQuoteHandler>();
            services.AddTransient<IRequestHandler<GetQuoteByIdRequest, Quote>, GetQuoteByIdHandler>();
            services.AddTransient<IRequestHandler<GetQuoteRequest, List<Quote>>, GetQuoteHandler>();
        }

        ///<Summary>
        /// Configure method
        ///</Summary>
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("CorsPolicy");

            // Quote Service api's ui view 
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Quote Service API V1");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}