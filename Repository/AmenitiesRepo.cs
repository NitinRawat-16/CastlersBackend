﻿using castlers.Models;
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
                    new("@TenderId", developerAmenities.TenderId),
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
                    new("@AdditionalAmenities", developerAmenities.AdditionAmenities),
                    new("@AmenitiesPdfUrl", developerAmenities.AmenitiesPdfUrl),
                    new("@CreationDate", developerAmenities.CreationDate),
                    new("@UpdationDate", developerAmenities.UpdationDate),
                    new("@IsActive", developerAmenities.IsActive),
                    new("@AmenitiesId", developerAmenities.AmenitiesId),
                };

                prmArray[prmArray.Length - 1].Direction = System.Data.ParameterDirection.Output;

                //await _dbContext.Database.ExecuteSqlRawAsync(@"EXEC usp_AddDeveloperAmenities
                //@DeveloperId, @TenderId, @LiftElivator, @GeneratorBackup, @SwimmingPool, @Gym, @ClubHouse, @GamingRoom,
                //@OutdoorPlayCourt, @ChildrenPlayArea, @SeniorCitizenPark, @JoggingTrack, @LandscapeGarden, @SolarWaterSystem, @SolarBackup, @EvChargingStation, @SecurityCabin, @CctvCoverage, @SocietyOffice,
                //@DecorativeEntranceGate, @RainWaterHarvesting, @FireFightingSystem, @TerraceGarden, @LetterBox, 
                //@GasPipeLine, @SecuritySystem, @DishConnection, @VideoDoorPhone, @IntercomSystem, @EasyDryerSystem, 
                //@AirCondition, @WaterPurifier, @ModulerKitchen, @SmartEnergyConsumption, @IntilligentDetectionWaterManagement, @SmokeDetectionAlert, @HomeAutomation, @AmenitiesPdfUrl, 
                //@CreationDate, @UpdationDate, @IsActive, @AmenitiesId OUTPUT", prmArray);

                await _dbContext.DeveloperAmenities.AddAsync(developerAmenities);
                var result = await _dbContext.SaveChangesAsync();
                return result;
            }
            catch (Exception) { throw; }
        }

        public Task<int> AddDeveloperAmenitiesDetails(DeveloperAmenitiesDetails developerAmenitiesDetails)
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

                //await _dbContext.Database.ExecuteSqlRawAsync(@"EXEC usp_AddDeveloperAmenitiesDetails
                //@DeveloperAmenitiesId, @LiftElivator, @GeneratorBackup, @SwimmingPool, @Gym, @ClubHouse, @GamingRoom,
                //@OutdoorPlayCourt, @ChildrenPlayArea, @SeniorCitizenPark, @JoggingTrack, @LandscapeGarden, @SolarWaterSystem, @SolarBackup, @EvChargingStation, @SecurityCabin, @CctvCoverage, @SocietyOffice,
                //@DecorativeEntranceGate, @RainWaterHarvesting, @FireFightingSystem, @TerraceGarden, @LetterBox, 
                //@GasPipeLine, @SecuritySystem, @DishConnection, @VideoDoorPhone, @IntercomSystem, @EasyDryerSystem, 
                //@AirCondition, @WaterPurifier, @ModulerKitchen, @SmartEnergyConsumption, @IntilligentDetectionWaterManagement, @SmokeDetectionAlert, @HomeAutomation, @AdditionalAmenities, 
                //@AmenitiesDetailsId OUTPUT", prmArray);
                return null;
            }
            catch (Exception) { throw; }
        }

        public Task<int> AddDeveloperConstructionSpecs(DeveloperConstructionSpec developerConstructionSpec)
        {
            try
            {
                return null;
            }
            catch (Exception) { throw; }
        }
    }
}
