using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace castlers.Models
{
    [Keyless]
    public class DeveloperAmenities
    {
        public DeveloperAmenities()
        {
            DeveloperAmenitiesDetails = new DeveloperAmenitiesDetails();
        }
        public int AmenitiesId { get; set; }
        public int DeveloperId { get; set; }
        public int DeveloperTenderId { get; set; }
        public bool LiftElivator { get; set; }
        public bool GeneratorBackup { get; set; }
        public bool SwimmingPool { get; set; }
        public bool Gym { get; set; }
        public bool ClubHouse { get; set; }
        public bool GamingRoom { get; set; }
        public bool OutdoorPlayCourt { get; set; }
        public bool ChildrenPlayArea { get; set; }
        public bool SeniorCitizenPark { get; set; }
        public bool JoggingTrack { get; set; }
        public bool LandscapeGarden { get; set; }
        public bool SolarWaterSystem { get; set; }
        public bool SolarBackup { get; set; }
        public bool EvChargingStation { get; set; }
        public bool SecurityCabin { get; set; }
        public bool CctvCoverage { get; set; }
        public bool SocietyOffice { get; set; }
        public bool DecorativeEntranceGate { get; set; }
        public bool RainWaterHarvesting { get; set; }
        public bool FireFightingSystem { get; set; }
        public bool TerraceGarden { get; set; }
        public bool LetterBox { get; set; }
        public bool GasPipeline { get; set; }
        public bool SecuritySystem { get; set; }
        public bool DishConnection { get; set; }
        public bool VideoDoorPhone { get; set; }
        public bool IntercomSystem { get; set; }
        public bool EasyDryerSystem { get; set; }
        public bool AirCondition { get; set; }
        public bool WaterPurifier { get; set; }
        public bool ModulerKitchen { get; set; }
        public bool SmartEnergyConsumption { get; set; }
        public bool IntilligentDetectionWaterManagement { get; set; }
        public bool SmokeDetectionAlert { get; set; }
        public bool HomeAutomation { get; set; }
        public bool AdditionAmenities { get; set; }
        [NotMapped]
        public IFormFile? UserAmenitiesPdf { get; set; }
        public string? UserAmenitiesPdfUrl { get; set; }
        [NotMapped]
        public IFormFile? AmenitiesPdf { get; set; }
        public string? AmenitiesPdfUrl { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdationDate { get; set; }
        [NotMapped]
        public DeveloperAmenitiesDetails DeveloperAmenitiesDetails { get; set; }
    }
}
