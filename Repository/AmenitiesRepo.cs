using castlers.Models;
using castlers.DbContexts;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using castlers.Common.Utilities;

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
                     SqlHelper.AddNullableParameter("@DeveloperId", developerAmenities.DeveloperId),
                     SqlHelper.AddNullableParameter("@TenderId", developerAmenities.DeveloperTenderId),
                     SqlHelper.AddNullableParameter("@LiftElivator", developerAmenities.LiftElivator),
                     SqlHelper.AddNullableParameter("@GeneratorBackup", developerAmenities.GeneratorBackup),
                     SqlHelper.AddNullableParameter("@SwimmingPool", developerAmenities.SwimmingPool),
                     SqlHelper.AddNullableParameter("@Gym", developerAmenities.Gym),
                     SqlHelper.AddNullableParameter("@ClubHouse", developerAmenities.ClubHouse),
                     SqlHelper.AddNullableParameter("@GamingRoom", developerAmenities.GamingRoom),
                     SqlHelper.AddNullableParameter("@OutdoorPlayCourt", developerAmenities.OutdoorPlayCourt),
                     SqlHelper.AddNullableParameter("@ChildrenPlayArea", developerAmenities.ChildrenPlayArea),
                     SqlHelper.AddNullableParameter("@SeniorCitizenPark", developerAmenities.SeniorCitizenPark),
                     SqlHelper.AddNullableParameter("@JoggingTrack", developerAmenities.JoggingTrack),
                     SqlHelper.AddNullableParameter("@LandscapeGarden", developerAmenities.LandscapeGarden),
                     SqlHelper.AddNullableParameter("@SolarWaterSystem", developerAmenities.SolarWaterSystem),
                     SqlHelper.AddNullableParameter("@SolarBackup", developerAmenities.SolarBackup),
                     SqlHelper.AddNullableParameter("@EvChargingStation", developerAmenities.EvChargingStation),
                     SqlHelper.AddNullableParameter("@SecurityCabin", developerAmenities.SecurityCabin),
                     SqlHelper.AddNullableParameter("@CctvCoverage", developerAmenities.CctvCoverage),
                     SqlHelper.AddNullableParameter("@SocietyOffice", developerAmenities.SocietyOffice),
                     SqlHelper.AddNullableParameter("@DecorativeEntranceGate", developerAmenities.DecorativeEntranceGate),
                     SqlHelper.AddNullableParameter("@RainWaterHarvesting", developerAmenities.RainWaterHarvesting),
                     SqlHelper.AddNullableParameter("@FireFightingSystem", developerAmenities.FireFightingSystem),
                     SqlHelper.AddNullableParameter("@TerraceGarden", developerAmenities.TerraceGarden),
                     SqlHelper.AddNullableParameter("@LetterBox", developerAmenities.LetterBox),
                     SqlHelper.AddNullableParameter("@GasPipeLine", developerAmenities.GasPipeline),
                     SqlHelper.AddNullableParameter("@SecuritySystem", developerAmenities.SecuritySystem),
                     SqlHelper.AddNullableParameter("@DishConnection", developerAmenities.DishConnection),
                     SqlHelper.AddNullableParameter("@VideoDoorPhone", developerAmenities.VideoDoorPhone),
                     SqlHelper.AddNullableParameter("@IntercomSystem", developerAmenities.IntercomSystem),
                     SqlHelper.AddNullableParameter("@EasyDryerSystem", developerAmenities.EasyDryerSystem),
                     SqlHelper.AddNullableParameter("@AirCondition", developerAmenities.AirCondition),
                     SqlHelper.AddNullableParameter("@WaterPurifier", developerAmenities.WaterPurifier),
                     SqlHelper.AddNullableParameter("@ModulerKitchen", developerAmenities.ModulerKitchen),
                     SqlHelper.AddNullableParameter("@SmartEnergyConsumption", developerAmenities.SmartEnergyConsumption),
                     SqlHelper.AddNullableParameter("@IntilligentDetectionWaterManagement", developerAmenities.IntilligentDetectionWaterManagement),
                     SqlHelper.AddNullableParameter("@SmokeDetectionAlert", developerAmenities.SmokeDetectionAlert),
                     SqlHelper.AddNullableParameter("@HomeAutomation", developerAmenities.HomeAutomation),
                     SqlHelper.AddNullableParameter("@AdditionalAmenities", developerAmenities.AdditionalAmenities),
                     SqlHelper.AddNullableParameter("@AmenitiesPdfUrl", developerAmenities.AmenitiesPdfUrl),
                     SqlHelper.AddNullableParameter("@CreationDate", DateTime.Now),
                     SqlHelper.AddNullableParameter("@UpdationDate", DateTime.Now),
                     SqlHelper.AddNullableParameter("@IsActive", developerAmenities.IsActive),
                     SqlHelper.AddNullableParameter("@UserAmenitiesPdfUrl", developerAmenities.UserAmenitiesPdfUrl),
                     SqlHelper.AddOutputParameter("@AmenitiesId"),
                };

                //prmArray[prmArray.Length - 1].Direction = System.Data.ParameterDirection.Output;

                await _dbContext.Database.ExecuteSqlRawAsync(@"EXEC usp_AddDeveloperAmenities
                @DeveloperId, @TenderId, @LiftElivator, @GeneratorBackup, @SwimmingPool, @Gym, @ClubHouse, @GamingRoom,
                @OutdoorPlayCourt, @ChildrenPlayArea, @SeniorCitizenPark, @JoggingTrack, @LandscapeGarden, @SolarWaterSystem, @SolarBackup, @EvChargingStation, @SecurityCabin, @CctvCoverage, @SocietyOffice,
                @DecorativeEntranceGate, @RainWaterHarvesting, @FireFightingSystem, @TerraceGarden, @LetterBox, 
                @GasPipeLine, @SecuritySystem, @DishConnection, @VideoDoorPhone, @IntercomSystem, @EasyDryerSystem, 
                @AirCondition, @WaterPurifier, @ModulerKitchen, @SmartEnergyConsumption, @IntilligentDetectionWaterManagement, @SmokeDetectionAlert, @HomeAutomation, @AdditionalAmenities, @AmenitiesPdfUrl, 
                @CreationDate, @UpdationDate, @IsActive, @UserAmenitiesPdfUrl, @AmenitiesId OUTPUT", prmArray);

                return Convert.ToInt32(prmArray[prmArray.Length - 1].Value == DBNull.Value ? -1 : prmArray[prmArray.Length - 1].Value);
            }
            catch (Exception ex) {
                throw ex;

            }
        }

        public async Task<int> AddDeveloperAmenitiesDetails(DeveloperAmenitiesDetails developerAmenitiesDetails)
        {
            try
            {
                SqlParameter[] prmArray = new SqlParameter[]
                {
                    SqlHelper.AddNullableParameter("@DeveloperAmenitiesId", developerAmenitiesDetails.DeveloperAmenitiesId),
                    SqlHelper.AddNullableParameter("@LiftElivator", developerAmenitiesDetails.LiftElivator),
                    SqlHelper.AddNullableParameter("@GeneratorBackup", developerAmenitiesDetails.GeneratorBackup),
                    SqlHelper.AddNullableParameter("@SwimmingPool", developerAmenitiesDetails.SwimmingPool),
                    SqlHelper.AddNullableParameter("@Gym", developerAmenitiesDetails.Gym),
                    SqlHelper.AddNullableParameter("@ClubHouse", developerAmenitiesDetails.ClubHouse),
                    SqlHelper.AddNullableParameter("@GamingRoom", developerAmenitiesDetails.GamingRoom),
                    SqlHelper.AddNullableParameter("@OutdoorPlayCourt", developerAmenitiesDetails.OutdoorPlayCourt),
                    SqlHelper.AddNullableParameter("@ChildrenPlayArea", developerAmenitiesDetails.ChildrenPlayArea),
                    SqlHelper.AddNullableParameter("@SeniorCitizenPark", developerAmenitiesDetails.SeniorCitizenPark),
                    SqlHelper.AddNullableParameter("@JoggingTrack", developerAmenitiesDetails.JoggingTrack),
                    SqlHelper.AddNullableParameter("@LandscapeGarden", developerAmenitiesDetails.LandscapeGarden),
                    SqlHelper.AddNullableParameter("@SolarWaterSystem", developerAmenitiesDetails.SolarWaterSystem),
                    SqlHelper.AddNullableParameter("@SolarBackup", developerAmenitiesDetails.SolarBackup),
                    SqlHelper.AddNullableParameter("@EvChargingStation", developerAmenitiesDetails.EvChargingStation),
                    SqlHelper.AddNullableParameter("@SecurityCabin", developerAmenitiesDetails.SecurityCabin),
                    SqlHelper.AddNullableParameter("@CctvCoverage", developerAmenitiesDetails.CctvCoverage),
                    SqlHelper.AddNullableParameter("@SocietyOffice", developerAmenitiesDetails.SocietyOffice),
                    SqlHelper.AddNullableParameter("@DecorativeEntranceGate", developerAmenitiesDetails.DecorativeEntranceGate),
                    SqlHelper.AddNullableParameter("@RainWaterHarvesting", developerAmenitiesDetails.RainWaterHarvesting),
                    SqlHelper.AddNullableParameter("@FireFightingSystem", developerAmenitiesDetails.FireFightingSystem),
                    SqlHelper.AddNullableParameter("@TerraceGarden", developerAmenitiesDetails.TerraceGarden),
                    SqlHelper.AddNullableParameter("@LetterBox", developerAmenitiesDetails.LetterBox),
                    SqlHelper.AddNullableParameter("@GasPipeLine", developerAmenitiesDetails.GasPipeline),
                    SqlHelper.AddNullableParameter("@SecuritySystem", developerAmenitiesDetails.SecuritySystem),
                    SqlHelper.AddNullableParameter("@DishConnection", developerAmenitiesDetails.DishConnection),
                    SqlHelper.AddNullableParameter("@VideoDoorPhone", developerAmenitiesDetails.VideoDoorPhone),
                    SqlHelper.AddNullableParameter("@IntercomSystem", developerAmenitiesDetails.IntercomSystem),
                    SqlHelper.AddNullableParameter("@EasyDryerSystem", developerAmenitiesDetails.EasyDryerSystem),
                    SqlHelper.AddNullableParameter("@AirCondition", developerAmenitiesDetails.AirCondition),
                    SqlHelper.AddNullableParameter("@WaterPurifier", developerAmenitiesDetails.WaterPurifier),
                    SqlHelper.AddNullableParameter("@ModulerKitchen", developerAmenitiesDetails.ModulerKitchen),
                    SqlHelper.AddNullableParameter("@SmartEnergyConsumption", developerAmenitiesDetails.SmartEnergyConsumption),
                    SqlHelper.AddNullableParameter("@IntilligentDetectionWaterManagement", developerAmenitiesDetails.IntilligentDetectionWaterManagement),
                    SqlHelper.AddNullableParameter("@SmokeDetectionAlert", developerAmenitiesDetails.SmokeDetectionAlert),
                    SqlHelper.AddNullableParameter("@HomeAutomation", developerAmenitiesDetails.HomeAutomation),
                    SqlHelper.AddNullableParameter("@AdditionalAmenities", developerAmenitiesDetails.AdditionAmenities),
                    SqlHelper.AddOutputParameter("@AmenitiesDetailsId"),
                };


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
                    SqlHelper.AddNullableParameter("@DeveloperId", developerConstructionSpec.DeveloperId),
                    SqlHelper.AddNullableParameter("@TenderId", developerConstructionSpec.DeveloperTenderId),
                    SqlHelper.AddNullableParameter("@Structure", developerConstructionSpec.Structure),
                    SqlHelper.AddNullableParameter("@WallBrickType", developerConstructionSpec.WallBrickType),
                    SqlHelper.AddNullableParameter("@WallSize", developerConstructionSpec.WallSize),
                    SqlHelper.AddNullableParameter("@FlooringTilesHouse", developerConstructionSpec.FlooringTilesHouse),
                    SqlHelper.AddNullableParameter("@FlooringTilesTerrace", developerConstructionSpec.FlooringTilesTerrace),
                    SqlHelper.AddNullableParameter("@FlooringTilesDryBalcony", developerConstructionSpec.FlooringTilesDryBalcony),
                    SqlHelper.AddNullableParameter("@FlooringTilesBathroom", developerConstructionSpec.FlooringTilesBathroom),
                    SqlHelper.AddNullableParameter("@FlooringTilesToilet", developerConstructionSpec.FlooringTilesToilet),
                    SqlHelper.AddNullableParameter("@WallTilesKitchen", developerConstructionSpec.WallTilesKitchen),
                    SqlHelper.AddNullableParameter("@WallTilesBathroom", developerConstructionSpec.WallTilesBathroom),
                    SqlHelper.AddNullableParameter("@WallTilesToilets", developerConstructionSpec.WallTilesToilets),
                    SqlHelper.AddNullableParameter("@WallTilesTerrace", developerConstructionSpec.WallTilesTerrace),
                    SqlHelper.AddNullableParameter("@WallTilesSitoutArea", developerConstructionSpec.WallTilesSitoutArea),
                    SqlHelper.AddNullableParameter("@KitchenPlatformSpec", developerConstructionSpec.KitchenPlatformSpec),
                    SqlHelper.AddNullableParameter("@KitchenPlatformType", developerConstructionSpec.KitchenPlatformType),
                    SqlHelper.AddNullableParameter("@MainDoorSpecification", developerConstructionSpec.MainDoorSpecification),
                    SqlHelper.AddNullableParameter("@InternalDoorSpecification", developerConstructionSpec.InternalDoorSpecification),
                    SqlHelper.AddNullableParameter("@BathroomDoorSpecification", developerConstructionSpec.BathroomDoorSpecification),
                    SqlHelper.AddNullableParameter("@TerraceDoorSpecification", developerConstructionSpec.TerraceDoorSpecification),
                    SqlHelper.AddNullableParameter("@Windows", developerConstructionSpec.Windows),
                    SqlHelper.AddNullableParameter("@Electrical", developerConstructionSpec.Electrical),
                    SqlHelper.AddNullableParameter("@WaterSupply", developerConstructionSpec.WaterSupply),
                    SqlHelper.AddNullableParameter("@ConstructionSpecPdfUrl", developerConstructionSpec.ConstructionSpecPdfUrl),
                    SqlHelper.AddNullableParameter("@CreationDate", DateTime.Now),
                    SqlHelper.AddNullableParameter("@UpdationDate", DateTime.Now),
                    SqlHelper.AddNullableParameter("@IsActive", developerConstructionSpec.IsActive),
                    SqlHelper.AddNullableParameter("@UserConstructionSpecPdfUrl", developerConstructionSpec.UserConstructionSpecPdfUrl),
                    SqlHelper.AddOutputParameter("@ConstructionSpecId")
              };


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
