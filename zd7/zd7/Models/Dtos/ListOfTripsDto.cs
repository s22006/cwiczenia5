namespace zd7.Models.Dtos
{
    public class ListOfTripsDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }

        public int MaxPeople { get; set; }
 
        public IEnumerable<TripCountriesDto> Countries { get; set; }

        public IEnumerable<TripClientsDto> Clients { get; set; }
    }
}
