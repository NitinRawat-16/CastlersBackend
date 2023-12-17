using System.ComponentModel.DataAnnotations.Schema;

namespace castlers.Dtos
{
    public class DeveloperConstructionSpecDto
    {
        public int ConstructionSpecId { get; set; }
        public int DeveloperId { get; set; }
        public int TenderId { get; set; }
        public string? Structure { get; set; }
        public string? WallBrickType { get; set; }
        public string? WallSize { get; set; }
        public string? FlooringTilesHouse { get; set; }
        public string? FlooringTilesTerrace { get; set; }
        public string? FlooringTilesDryBalcony { get; set; }
        public string? FlooringTilesBathroom { get; set; }
        public string? FlooringTilesToilet { get; set; }
        public string? WallTilesKitchen { get; set; }
        public string? WallTilesBathroom { get; set; }
        public string? WallTilesToilets { get; set; }
        public string? WallTilesTerrace { get; set; }
        public string? WallTilesSitoutArea { get; set; }
        public string? KitchenPlatformSpec { get; set; }
        public string? KitchenPlatformType { get; set; }
        public string? MainDoorSpecification { get; set; }
        public string? InternalDoorSpecification { get; set; }
        public string? BathroomDoorSpecification { get; set; }
        public string? TerraceDoorSpecification { get; set; }
        public string? Windows { get; set; }
        public string? Electrical { get; set; }
        public string? WaterSupply { get; set; }
        [NotMapped]
        public IFormFile? ConstructionSpecPdf { get; set; }
        public string? ConstructionSpecPdfUrl { get; set; }
        public bool isActive { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdationDate { get; set; }
    }
}
