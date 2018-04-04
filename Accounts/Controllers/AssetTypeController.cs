using Accounts.ApiInfrastructure.ApiModels.AssetType;
using Accounts.ApiInfrastructure.Client.AssetType;
using Accounts.Attributes;
using Accounts.Models.AssetType;
using ApiHelper.Client;
using AutoMapper;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Accounts.Controllers
{
    [Authentication]
    public class AssetTypeController : BaseController
    {
        #region ctor

        private readonly IAssetTypeClient _assetTypeClient;
        private readonly IApiClient _apiClient;
        private readonly HttpClient _httpClient;

        public AssetTypeController(IAssetTypeClient assetTypeClient, IApiClient apiClient, HttpClient httpClient)
        {
            _httpClient = httpClient;
            _assetTypeClient = assetTypeClient;
            _apiClient = apiClient;
        }

        #endregion

        #region Index

        // GET: AssetType/Index
        public async Task<ActionResult> Index()
        {
            var assetTypes = await _assetTypeClient.GetAssetType();

            var viewModel = new AssetTypesViewModel();
            viewModel.AssetTypes = Mapper.Map<List<AssetTypeApiModel>, List<AssetTypeViewModel>>(assetTypes.Data);

            return View(viewModel);
        }

        #endregion

        #region Details

        // GET: AssetType/AssetType/1
        public async Task<ActionResult> Details(int id)
        {
            return await GetAssetType(id);
        }

        #endregion

        #region Create

        public ActionResult Create()
        {
            var model = new AssetTypeViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AssetTypeViewModel model)
        {
            var response = await _assetTypeClient.CreateAssetType(model);

            if (response.StatusIsSuccessful)
            {
                var assetTypeId = response.Data;
                return RedirectToAction("Details", new { id = assetTypeId });
            }

            AddResponseErrorsToModelState(response);
            return View(model);
        }

        #endregion

        #region Edit

        public async Task<ActionResult> Edit(int id)
        {
            return await GetAssetType(id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(AssetTypeViewModel model)
        {
            var response = await _assetTypeClient.UpdateAssetType(model);

            if (response.StatusIsSuccessful)
            {
                var assetTypeId = response.Data;
                return RedirectToAction("Index");
            }

            AddResponseErrorsToModelState(response);
            return View(model);
        }

        #endregion

        #region Delete

        public async Task<ActionResult> Delete(int id)
        {
            var response = await _assetTypeClient.DeleteAssetType(id);

            if (response.StatusIsSuccessful)
            {
                var assetTypeId = response.Data;
                return RedirectToAction("Index");
            }

            AddResponseErrorsToModelState(response);
            return RedirectToAction("Index");
        }

        #endregion

        #region Private methods

        private async Task<ActionResult> GetAssetType(int id)
        {
            var response = await _assetTypeClient.GetAssetType(id);
            var model = new AssetTypeViewModel();

            if (response.StatusIsSuccessful)
            {
                model = Mapper.Map<AssetTypeViewModel>(response.Data);
                return View(model);
            }

            AddResponseErrorsToModelState(response);
            return View(model);
        }

        #endregion
    }
}