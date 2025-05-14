using LocaCraft.Models;
using Newtonsoft.Json;
using System.Diagnostics;
using System.IO;

namespace LocaCraft.DataServices
{
    public class RealEstateDataService
    {
        #region ATTRIBUTES
        // Path to the json file
        private readonly string _filePath;

        // Folder and file names
        private readonly string _folderName = "RealEstate";
        private readonly string _fileName = "realEstate.json";

        private List<RealEstateAssetModel> _realEstateDataCache = new List<RealEstateAssetModel>();

        private bool _dataLoaded = false;
        private readonly SemaphoreSlim _semaphore = new(1, 1);
        #endregion

        #region CONSTRUCTOR
        /// <summary>
        /// Constructor for the RealEstateDataService class.
        /// </summary>
        public RealEstateDataService()
        {
            _dataLoaded = false;

            // Get the path to the user's AppData folder
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            // Get the folder path for the application in roaming
            string appFolderPath = Path.Combine(appDataPath, _folderName);

            // Get the data folder inside the app
            string dataFolder = Path.Combine(appFolderPath, "data");

            // Check if the data folder exist and create it otherwise.
            if (!Directory.Exists(dataFolder))
                Directory.CreateDirectory(dataFolder);

            // Define the path of the json
            _filePath = Path.Combine(dataFolder, _fileName);
            InitializeFile();
        }
        #endregion

        /// <summary>
        /// Initialize the json file if it does not exist.
        /// </summary>
        private void InitializeFile()
        {
            // Check if the file exist and create it if otherwise
            try
            {
                if (!File.Exists(_filePath))
                    File.WriteAllText(_filePath, JsonConvert.SerializeObject(new List<RealEstateAssetModel>()));
            }
            catch (Exception ex)
            {
                // Handle the exception
                Console.WriteLine($"Error creating file: {ex.Message}");
            }

#if DEBUG
            try
            {
                // For debug purpose
                string folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), _folderName);
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = folderPath,
                    UseShellExecute = true
                };
                Process.Start(startInfo);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating file: {ex.Message}");
            }

#endif
        }

        /// <summary>
        /// Get the list of RealEstateAssetModel from the json file.
        /// </summary>
        /// <param name="forceReload">Condition for forcing the loading of the json</param>
        /// <returns> List of the real estate loaded or and empty list otherwise </returns>
        public async Task<List<RealEstateAssetModel>> GetRealEstateAssetsAsync(bool forceReload = false)
        {
            if (!_dataLoaded || forceReload)
            { 
                try
                {
                    // Read the file content
                    string json = await File.ReadAllTextAsync(_filePath);
                    // Deserialize the json to a list of RealEstateAssetModel
                    List<RealEstateAssetModel>? realEstateList = JsonConvert.DeserializeObject<List<RealEstateAssetModel>>(json);
                    // Check if the deserialized list is null
                    _realEstateDataCache = realEstateList ?? new List<RealEstateAssetModel>();
                    // Set the data loaded flag to true
                    _dataLoaded = true;
                }
                catch (Exception ex)
                {
                    // Handle the exception
                    Console.WriteLine($"Error loading file: {ex.Message}");
                    // Initialize the cache to an empty list
                    _realEstateDataCache = new List<RealEstateAssetModel>();
                }
            }
            return _realEstateDataCache;
        }

        /// <summary>
        /// Force the reload of the json file.
        /// </summary>
        public async Task ReloadAsync() => await GetRealEstateAssetsAsync(forceReload: true);

        /// <summary>
        /// Save the list of RealEstateAssetModel to the json file.
        /// </summary>
        /// <param name="realEstateAssets">List of the real estate to save</param>
        /// <returns></returns>
        public async Task SaveRealEstateAssets(List<RealEstateAssetModel> realEstateAssets)
        {
            try
            {
                // Serialize the list of RealEstateAssetModel to json
                string json = JsonConvert.SerializeObject(realEstateAssets, Formatting.Indented);
                // Write the json to the file
                await File.WriteAllTextAsync(_filePath, json);
            }
            catch (Exception ex)
            {
                // Handle the exception
                Console.WriteLine($"Error saving file: {ex.Message}");
            }
        }

        /// <summary>
        /// Add a new RealEstateAssetModel to the list and save it to the json file.
        /// </summary>
        /// <param name="realEstateAsset"></param>
        public async Task AddRealEstateAsset(RealEstateAssetModel realEstateAsset)
        {
            // Wait for the semaphore to be available
            await _semaphore.WaitAsync();
            _realEstateDataCache = await GetRealEstateAssetsAsync();
            try
            {
                // Add the new RealEstateAssetModel to the list
                realEstateAsset.Id = GenerateRealEstateId();
                _realEstateDataCache.Add(realEstateAsset);
                // Save the updated list to the file
                await SaveRealEstateAssets(_realEstateDataCache);
            }
            finally
            {
                // Release the semaphore
                _semaphore.Release();
            }

        }

        /// <summary>
        /// Update an existing RealEstateAssetModel in the list and save it to the json file.
        /// </summary>
        /// <param name="realEstateAsset">The real estate to update</param>
        /// <returns></returns>
        public async Task UpdateRealEstateAsset(RealEstateAssetModel realEstateAsset)
        {
            // Find the index of the RealEstateAssetModel to update
            var index = _realEstateDataCache.FindIndex(x => x.Id == realEstateAsset.Id);
            if (index != -1)
            {
                // Update the RealEstateAssetModel in the list
                _realEstateDataCache[index] = realEstateAsset;
                // Save the updated list to the file
                await SaveRealEstateAssets(_realEstateDataCache);
            }
        }

        /// <summary>
        /// Delete a RealEstateAssetModel from the list and save it to the json file.
        /// </summary>
        /// <param name="id">The id of the real estate to delete</param>
        /// <returns></returns>
        public async Task DeleteRealEstateAsset(int id)
        {
            // Get the current list of RealEstateAssetModel
            var realEstateAssets = _realEstateDataCache;
            // Find the RealEstateAssetModel to delete
            var realEstateAsset = realEstateAssets.FirstOrDefault(x => x.Id == id);
            if (realEstateAsset != null)
            {
                // Remove the RealEstateAssetModel from the list
                realEstateAssets.Remove(realEstateAsset);
                // Save the updated list to the file
                await SaveRealEstateAssets(realEstateAssets);
            }
        }

        /// <summary>
        /// Generate a new id for the RealEstateAssetModel.
        /// </summary>
        /// <returns>The new ID</returns>
        public int GenerateRealEstateId()
        {
            // Get the current list of RealEstateAssetModel
            var realEstateAssets = _realEstateDataCache;
            // Generate a new id for the RealEstateAssetModel
            int newId = realEstateAssets.Count > 0 ? realEstateAssets.Max(x => x.Id) + 1 : 1;
            // Return the new id
            return newId;
        }
    }
}
