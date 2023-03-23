namespace castlers.Dtos
{
    public class UpdateTechnicalDetailsRegisteredSocietyDto
    {
        public int registeredSocietyId { get; set; }
        public int sizeOfPlot { get; set; }
        public string plotDimensions { get; set; }
        public bool residentialUse { get; set; }
        public bool commercialUse { get; set; }
        public bool mixedUse { get; set; }
        public int numberOfWings { get; set; }
        public int numberOfCommercialTenaments { get; set; }
        public int numberOfResidentialTenaments { get; set; }
        public float totalCommercialBuiltUpBldgArea { get; set; }
        public float totalResidentialBuiltUpBldgArea { get; set; }
        public float totalBuiltUpArea { get; set; }
        public float approchRoadWidth { get; set; }
        public Guid createdBy { get; set; }
        public Guid updatedBy { get; set; }
    }
}
