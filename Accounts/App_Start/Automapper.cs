using Accounts.ApiInfrastructure.ApiModels.Account;
using Accounts.ApiInfrastructure.ApiModels.AccountingPeriod;
using Accounts.ApiInfrastructure.ApiModels.AccountType;
using Accounts.ApiInfrastructure.ApiModels.AssetType;
using Accounts.ApiInfrastructure.ApiModels.Transaction;
using Accounts.Models.Account;
using Accounts.Models.AccountingPeriod;
using Accounts.Models.AccountType;
using Accounts.Models.AssetType;
using Accounts.Models.Transaction;
using AutoMapper;
using System.Web.Mvc;

namespace Accounts.App_Start
{
    public static class Automapper
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<AccountApiModel, AccountViewModel>()
                    .ForMember(dest => dest.AccountTypes, opt => opt.Ignore())
                    ;

                cfg.CreateMap<AccountTypeApiModel, AccountTypeViewModel>();

                cfg.CreateMap<AccountingPeriodApiModel, AccountingPeriodViewModel>();

                cfg.CreateMap<AssetTypeApiModel, AssetTypeViewModel>();

                cfg.CreateMap<DepositApiModel, DepositViewModel>()
                    .ForMember(dest => dest.AccountingPeriods, opt => opt.Ignore())
                    .ForMember(dest => dest.Accounts, opt => opt.Ignore())
                    .ForMember(dest => dest.AssetTypes, opt => opt.Ignore())
                    .ForMember(dest => dest.ID, opt => opt.Ignore())
                    .ForMember(dest => dest.TransactionID, opt => opt.Ignore())
                    ;

                cfg.CreateMap<DepositViewModel, DepositApiModel>()
                    .ForMember(dest => dest.CashBookTransactionID, opt => opt.Ignore())
                    .ForMember(dest => dest.AccountTransactionID, opt => opt.Ignore())
                    .ForMember(dest => dest.JournalID, opt => opt.Ignore())
                    ;

                cfg.CreateMap<AccountApiModel, SelectListItem>()
                    .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.AccountID.ToString()))
                    .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Name))
                    .ForMember(dest => dest.Group, opt => opt.Ignore())
                    .ForMember(dest => dest.Selected, opt => opt.Ignore())
                    .ForMember(dest => dest.Disabled, opt => opt.Ignore())
                    ;

                cfg.CreateMap<AccountTypeApiModel, SelectListItem>()
                    .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.AccountTypeID.ToString()))
                    .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Name))
                    .ForMember(dest => dest.Group, opt => opt.Ignore())
                    .ForMember(dest => dest.Selected, opt => opt.Ignore())
                    .ForMember(dest => dest.Disabled, opt => opt.Ignore())
                    ;

                cfg.CreateMap<AccountingPeriodApiModel, SelectListItem>()
                    .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.AccountingPeriodID.ToString()))
                    .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Name))
                    .ForMember(dest => dest.Group, opt => opt.Ignore())
                    .ForMember(dest => dest.Selected, opt => opt.Ignore())
                    .ForMember(dest => dest.Disabled, opt => opt.Ignore())
                    ;

                cfg.CreateMap<AssetTypeApiModel, SelectListItem>()
                    .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.AssetTypeID.ToString()))
                    .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Name))
                    .ForMember(dest => dest.Group, opt => opt.Ignore())
                    .ForMember(dest => dest.Selected, opt => opt.Ignore())
                    .ForMember(dest => dest.Disabled, opt => opt.Ignore())
                    ;
            });

            Mapper.AssertConfigurationIsValid();

        }
    }
}