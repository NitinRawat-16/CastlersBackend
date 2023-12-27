using castlers.Models;
using castlers.DbContexts;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace castlers.Repository
{
    public class AmenitiesRepo : IAmenitiesRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public AmenitiesRepo(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> AddDeveloperAmenities(DeveloperAmenities developerAmenities)
        {
            try
            {
                SqlParameter[] prmArray = new SqlParameter[]
                {
                    new("@DeveloperId", developerAmenities.DeveloperId),
                    new("@TenderId", developerAmenities.DeveloperTenderId),
                    new("@LiftElivator", developerAmenities.LiftElivator),
                    new("@GeneratorBackup", developerAmenities.GeneratorBackup),
                    new("@SwimmingPool", developerAmenities.SwimmingPool),
                    new("@Gym", developerAmenities.Gym),
                    new("@ClubHouse", developerAmenities.ClubHouse),
                    new("@GamingRoom", developerAmenities.GamingRoom),
                    new("@OutdoorPlayCourt", developerAmenities.OutdoorPlayCourt),
                    new("@ChildrenPlayArea", developerAmenities.ChildrenPlayArea),
                    new("@SeniorCitizenPark", developerAmenities.SeniorCitizenPark),
                    new("@JoggingTrack", developerAmenities.JoggingTrack),
                    new("@LandscapeGarden", developerAmenities.LandscapeGarden),
                    new("@SolarWaterSystem", developerAmenities.SolarWaterSystem),
                    new("@SolarBackup", developerAmenities.SolarBackup),
                    new("@EvChargingStation", developerAmenities.EvChargingStation),
                    new("@SecurityCabin", developerAmenities.SecurityCabin),
                    new("@CctvCoverage", developerAmenities.CctvCoverage),
                    new("@SocietyOffice", developerAmenities.SocietyOffice),
                    new("@DecorativeEntranceGate", developerAmenities.DecorativeEntranceGate),
                    new("@RainWaterHarvesting", developerAmenities.RainWaterHarvesting),
                    new("@FireFightingSystem", developerAmenities.FireFightingSystem),
                    new("@TerraceGarden", developerAmenities.TerraceGarden),
                    new("@LetterBox", developerAmenities.LetterBox),
                    new("@GasPipeLine", developerAmenities.GasPipeline),
                    new("@SecuritySystem", developerAmenities.SecuritySystem),
                    new("@DishConnection", developerAmenities.DishConnection),
                    new("@VideoDoorPhone", developerAmenities.VideoDoorPhone),
                    new("@IntercomSystem", developerAmenities.IntercomSystem),
                    new("@EasyDryerSystem", developerAmenities.EasyDryerSystem),
                    new("@AirCondition", developerAmenities.AirCondition),
                    new("@WaterPurifier", developerAmenities.WaterPurifier),
                    new("@ModulerKitchen", developerAmenities.ModulerKitchen),
                    new("@SmartEnergyConsumption", developerAmenities.SmartEnergyConsumption),
                    new("@IntilligentDetectionWaterManagement", developerAmenities.IntilligentDetectionWaterManagement),
                    new("@SmokeDetectionAlert", developerAmenities.SmokeDetectionAlert),
                    new("@HomeAutomation", developerAmenities.HomeAutomation),
                    new("@AdditionalAmenities", developerAmenities.AdditionalAmenities),
                    new("@AmenitiesPdfUrl", developerAmenities.AmenitiesPdfUrl),
                    new("@CreationDate", DateTime.Now),
                    new("@UpdationDate", DateTime.Now),
                    new("@IsActive", developerAmenities.IsActive),
                    new("@UserAmenitiesPdfUrl", developerAmenities.UserAmenitiesPdfUrl),
                    new("@AmenitiesId", developerAmenities.AmenitiesId),
                };

                prmArray[prmArray.Length - 1].Direction = System.Data.ParameterDirection.Output;

                await _dbContext.Database.ExecuteSqlRawAsync(@"EXEC usp_AddDeveloperAmenities
                @DeveloperId, @TenderId, @LiftElivator, @GeneratorBackup, @SwimmingPool, @Gym, @ClubHouse, @GamingRoom,
                @OutdoorPlayCourt, @ChildrenPlayArea, @SeniorCitizenPark, @JoggingTrack, @LandscapeGarden, @SolarWaterSystem, @SolarBackup, @EvChargingStation, @SecurityCabin, @CctvCoverage, @SocietyOffice,
                @DecorativeEntranceGate, @RainWaterHarvesting, @FireFightingSystem, @TerraceGarden, @LetterBox, 
                @GasPipeLine, @SecuritySystem, @DishConnection, @VideoDoorPhone, @IntercomSystem, @EasyDryerSystem, 
                @AirCondition, @WaterPurifier, @ModulerKitchen, @SmartEnergyConsumption, @IntilligentDetectionWaterManagement, @SmokeDetectionAlert, @HomeAutomation, @AdditionalAmenities, @AmenitiesPdfUrl, 
                @CreationDate, @UpdationDate, @IsActive, @UserAmenitiesPdfUrl, @AmenitiesId OUTPUT", prmArray);

                return Convert.ToInt32(prmArray[prmArray.Length - 1].Value == DBNull.Value ? -1 : prmArray[prmArray.Length - 1].Value);
            }
            catch (Exception) { throw; }
        }

        public async Task<int> AddDeveloperAmenitiesDetails(DeveloperAmenitiesDetails developerAmenitiesDetails)
        {
            try
            {
                SqlParameter[] prmArray = new SqlParameter[]
                {
                    new("@DeveloperAmenitiesId", developerAmenitiesDetails.DeveloperAmenitiesId),
                    new("@LiftElivator", developerAmenitiesDetails.LiftElivator),
                    new("@GeneratorBackup", developerAmenitiesDetails.GeneratorBackup),
                    new("@SwimmingPool", developerAmenitiesDetails.SwimmingPool),
                    new("@Gym", developerAmenitiesDetails.Gym),
                    new("@ClubHouse", developerAmenitiesDetails.ClubHouse),
                    new("@GamingRoom", developerAmenitiesDetails.GamingRoom),
                    new("@OutdoorPlayCourt", developerAmenitiesDetails.OutdoorPlayCourt),
                    new("@ChildrenPlayArea", developerAmenitiesDetails.ChildrenPlayArea),
                    new("@SeniorCitizenPark", developerAmenitiesDetails.SeniorCitizenPark),
                    new("@JoggingTrack", developerAmenitiesDetails.JoggingTrack),
                    new("@LandscapeGarden", developerAmenitiesDetails.LandscapeGarden),
                    new("@SolarWaterSystem", developerAmenitiesDetails.SolarWaterSystem),
                    new("@SolarBackup", developerAmenitiesDetails.SolarBackup),
                    new("@EvChargingStation", developerAmenitiesDetails.EvChargingStation),
                    new("@SecurityCabin", developerAmenitiesDetails.SecurityCabin),
                    new("@CctvCoverage", developerAmenitiesDetails.CctvCoverage),
                    new("@SocietyOffice", developerAmenitiesDetails.SocietyOffice),
                    new("@DecorativeEntranceGate", developerAmenitiesDetails.DecorativeEntranceGate),
                    new("@RainWaterHarvesting", developerAmenitiesDetails.RainWaterHarvesting),
                    new("@FireFightingSystem", developerAmenitiesDetails.FireFightingSystem),
                    new("@TerraceGarden", developerAmenitiesDetails.TerraceGarden),
                    new("@LetterBox", developerAmenitiesDetails.LetterBox),
                    new("@GasPipeLine", developerAmenitiesDetails.GasPipeline),
                    new("@SecuritySystem", developerAmenitiesDetails.SecuritySystem),
                    new("@DishConnection", developerAmenitiesDetails.DishConnection),
                    new("@VideoDoorPhone", developerAmenitiesDetails.VideoDoorPhone),
                    new("@IntercomSystem", developerAmenitiesDetails.IntercomSystem),
                    new("@EasyDryerSystem", developerAmenitiesDetails.EasyDryerSystem),
                    new("@AirCondition", developerAmenitiesDetails.AirCondition),
                    new("@WaterPurifier", developerAmenitiesDetails.WaterPurifier),
                    new("@ModulerKitchen", developerAmenitiesDetails.ModulerKitchen),
                    new("@SmartEnergyConsumption", developerAmenitiesDetails.SmartEnergyConsumption),
                    new("@IntilligentDetectionWaterManagement", developerAmenitiesDetails.IntilligentDetectionWaterManagement),
                    new("@SmokeDetectionAlert", developerAmenitiesDetails.SmokeDetectionAlert),
                    new("@HomeAutomation", developerAmenitiesDetails.HomeAutomation),
                    new("@AdditionalAmenities", developerAmenitiesDetails.AdditionAmenities),
                    new("@AmenitiesDetailsId", developerAmenitiesDetails.AmenitiesDetailsId),
                };

                prmArray[prmArray.Length - 1].Direction = System.Data.ParameterDirection.Output;

                await _dbContext.Database.ExecuteSqlRawAsync(@"EXEC usp_AddDeveloperAmenitiesDetails
                @DeveloperAmenitiesId, @LiftElivator, @GeneratorBackup, @SwimmingPool, @Gym, @ClubHouse, @GamingRoom,
                @OutdoorPlayCourt, @ChildrenPlayArea, @SeniorCitizenPark, @JoggingTrack, @LandscapeGarden, @SolarWaterSystem, @SolarBackup, @EvChargingStation, @SecurityCabin, @CctvCoverage, @SocietyOffice,
                @DecorativeEntranceGate, @RainWaterHarvesting, @FireFightingSystem, @TerraceGarden, @LetterBox, 
                @GasPipeLine, @SecuritySystem, @DishConnection, @VideoDoorPhone, @IntercomSystem, @EasyDryerSystem, 
                @AirCondition, @WaterPurifier, @ModulerKitchen, @SmartEnergyConsumption, @IntilligentDetectionWaterManagement, @SmokeDetectionAlert, @HomeAutomation, @AdditionalAmenities, 
                @AmenitiesDetailsId OUTPUT", prmArray);

                return Convert.ToInt32(prmArray[prmArray.Length - 1].Value == DBNull.Value ? -1 : prmArray[prmArray.Length - 1].Value);
            }
            catch (Exception) { throw; }
        }

        public async Task<int> AddDeveloperConstructionSpecs(DeveloperConstructionSpec developerConstructionSpec)
        {
            try
            {
                SqlParameter[] prmArray = new SqlParameter[]
              {
                    new("@DeveloperId", developerConstructionSpec.DeveloperId),
                    new("@TenderId", developerConstructionSpec.DeveloperTenderId),
                    new("@Structure", developerConstructionSpec.Structure),
                    new("@WallBrickType", developerConstructionSpec.WallBrickType),
                    new("@WallSize", developerConstructionSpec.WallSize),
                    new("@FlooringTilesHouse", developerConstructionSpec.FlooringTilesHouse),
                    new("@FlooringTilesTerrace", developerConstructionSpec.FlooringTilesTerrace),
                    new("@FlooringTilesDryBalcony", developerConstructionSpec.FlooringTilesDryBalcony),
                    new("@FlooringTilesBathroom", developerConstructionSpec.FlooringTilesBathroom),
                    new("@FlooringTilesToilet", developerConstructionSpec.FlooringTilesToilet),
                    new("@WallTilesKitchen", developerConstructionSpec.WallTilesKitchen),
                    new("@WallTilesBathroom", developerConstructionSpec.WallTilesBathroom),
                    new("@WallTilesToilets", developerConstructionSpec.WallTilesToilets),
                    new("@WallTilesTerrace", developerConstructionSpec.WallTilesTerrace),
                    new("@WallTilesSitoutArea", developerConstructionSpec.WallTilesSitoutArea),
                    new("@KitchenPlatformSpec", developerConstructionSpec.KitchenPlatformSpec),
                    new("@KitchenPlatformType", developerConstructionSpec.KitchenPlatformType),
                    new("@MainDoorSpecification", developerConstructionSpec.MainDoorSpecification),
                    new("@InternalDoorSpecification", developerConstructionSpec.InternalDoorSpecification),
                    new("@BathroomDoorSpecification", developerConstructionSpec.BathroomDoorSpecification),
                    new("@TerraceDoorSpecification", developerConstructionSpec.TerraceDoorSpecification),
                    new("@Windows", developerConstructionSpec.Windows),
                    new("@Electrical", developerConstructionSpec.Electrical),
                    new("@WaterSupply", developerConstructionSpec.WaterSupply),
                    new("@ConstructionSpecPdfUrl", developerConstructionSpec.ConstructionSpecPdfUrl),
                    new("@CreationDate", DateTime.Now),
                    new("@UpdationDate", DateTime.Now),
                    new("@IsActive", developerConstructionSpec.IsActive),
                    new("@UserConstructionSpecPdfUrl", developerConstructionSpec.UserConstructionSpecPdfUrl),
                    new("@ConstructionSpecId", developerConstructionSpec.ConstructionSpecId)
              };

                prmArray[prmArray.Length - 1].Direction = System.Data.ParameterDirection.Output;

                await _dbContext.Database.ExecuteSqlRawAsync(@"EXEC usp_AddDeveloperConstructionSpec 
                @DeveloperId, @TenderId, @Structure, @WallBrickType, @WallSize, @FlooringTilesHouse,
                @FlooringTilesTerrace, @FlooringTilesDryBalcony, @FlooringTilesBathroom, @FlooringTilesToilet, 
                @WallTilesKitchen, @WallTilesBathroom, @WallTilesToilets, @WallTilesTerrace, @WallTilesSitoutArea, 
                @KitchenPlatformSpec, @KitchenPlatformType, @MainDoorSpecification, @InternalDoorSpecification, 
                @BathroomDoorSpecification, @TerraceDoorSpecification, @Windows, @Electrical, @WaterSupply, 
                @ConstructionSpecPdfUrl, @CreationDate, @UpdationDate, @IsActive, @UserConstructionSpecPdfUrl,
                @ConstructionSpecId OUTPUT", prmArray);

                return Convert.ToInt32(prmArray[prmArray.Length - 1].Value == DBNull.Value ? -1 : prmArray[prmArray.Length - 1].Value);
            }
            catch (Exception) { throw; }
        }

        public async Task<DeveloperAmenities> GetDeveloperAmenitiesAsync(int developerId)
        {
            try
            {
                var amenitiesDetails = await _dbContext.DeveloperAmenities
                    .FromSqlRaw(@"SELECT tenderId AS DeveloperTenderId, * FROM DeveloperAmenities WHERE developerId = @DeveloperId", new SqlParameter("@DeveloperId", developerId))
                    .FirstOrDefaultAsync();
                return amenitiesDetails ?? new();
            }
            catch (Exception) { throw; }
        }

        public async Task<DeveloperConstructionSpec> GetDeveloperConstructionSpecAsync(int developerId)
        {
            try
            {
                var constructionSpecDetails = await _dbContext.DeveloperConstructionSpecs
                    .FromSqlRaw(@"SELECT tenderId AS DeveloperTenderId, * FROM DeveloperConstructionSpec WHERE developerId = @DeveloperId", new SqlParameter("@DeveloperId", developerId))
                    .FirstOrDefaultAsync();
                return constructionSpecDetails ?? new();
            }
            catch (Exception) { throw; }
        }
    }
}
