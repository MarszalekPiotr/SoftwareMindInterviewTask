using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NegotiationService.Application.Interfaces.Services;
using NegotiationService.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NegotiationService.Application.Logic.Product.Handlers;
using NegotiationService.Application.Logic.Product.Validators;
using NegotiationService.Application.Logic.Abstract;
using NegotiationService.Application.Logic.PurchaseOffer.Handlers;
using NegotiationService.Application.Logic.PurchaseOffer.Validators;
using NegotiationService.Application.Logic.StatusManagers;
using NegotiationService.Application.Logic.PurchaseOffer.StatusUpdate;

namespace NegotiationService.Application.Extensions
{
    public static  class AddApplicationExtension
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient < BaseCommandHandler >();
            services.AddTransient<ProductCommandHandler>();
            services.AddTransient<ProductQueryHandler>();
            services.AddTransient<ProductValidator>();


            services.AddTransient<PurchaseOfferCommandHandler>();
            services.AddTransient<PurchaseOfferValidator>();

            services.AddTransient<ProductStatusManager>();
            services.AddTransient<TransactionStatusManager>();

            services.AddTransient<IStatusUpdateService, StatusUpdateService>();


            return services;


        }
    }
}
